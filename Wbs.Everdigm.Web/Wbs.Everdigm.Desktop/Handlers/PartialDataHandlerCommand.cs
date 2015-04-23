using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wbs.Everdigm.Common;
using Wbs.Everdigm.Database;
using Wbs.Protocol.TX300;
using Wbs.Utilities;

namespace Wbs.Everdigm.Desktop
{
    /// <summary>
    /// 处理命令相关的部分
    /// </summary>
    public partial class DataHandler
    {
        /// <summary>
        /// 检索是否有TCP在线终端的命令待发送。
        /// 条件：
        /// 1、预定发送时间在30s以内
        /// 2、命令的状态为等待发送状态(等待发送或等待重发)
        /// 3、终端为TCP链接状态的
        /// </summary>
        /// <returns></returns>
        public void CheckCommand()
        {
            if (null == _server) return;

            var list = CommandInstance.FindList(f => f.ScheduleTime >= DateTime.Now.AddSeconds(-30) &&
                f.Status <= (byte)CommandStatus.ReSending &&
                f.TB_Terminal.OnlineStyle == (byte)LinkType.TCP).ToList();
            foreach (var cmd in list)
            {
                // 0==链接不存在1=发送成功2=网络处理错误
                var ret = _server.Send(cmd.TB_Terminal.Socket.Value, Wbs.Utilities.CustomConvert.GetBytes(cmd.Content));
                if (ret == 0)
                {
                    // TCP链接丢失，重新用SMS方式发送
                    CommandUtility.SendSMSCommand(cmd);
                    ShowUnhandledMessage(Now + "Send Command(SentBySMS): " + cmd.Content);
                }
                else
                {
                    ShowUnhandledMessage(Now + "Send command(" + (1 == ret ? CommandStatus.SentByTCP : CommandStatus.TCPNetworkError) + "): " + cmd.Content);
                    UpdateCommand(cmd, (1 == ret ? CommandStatus.SentByTCP : CommandStatus.TCPNetworkError));
                }
            }
            // 待发送的命令发送完之后，清理超时的命令
            ClearTimedoutCommands();
        }
        /// <summary>
        /// 清理超时的命令为发送失败状态
        /// </summary>
        private void ClearTimedoutCommands()
        {
            CommandInstance.Update(f => f.ScheduleTime <= DateTime.Now.AddMinutes(-5) &&
                f.Status >= (byte)CommandStatus.Waiting && f.Status <= (byte)CommandStatus.SentToDest, act =>
                {
                    act.Status = (byte)CommandStatus.Timedout;
                });
        }
        /// <summary>
        /// 更新命令的发送状态
        /// </summary>
        /// <param name="obj"></param>
        private void UpdateCommand(TB_Command obj, CommandStatus status)
        {
            CommandInstance.Update(f => f.id == obj.id, act =>
            {
                act.Status = (byte)status;
                if (status == CommandStatus.SentByTCP)
                {
                    act.ActualSendTime = DateTime.Now;
                }
            });
        }
        /// <summary>
        /// 处理命令回复状态
        /// </summary>
        /// <param name="tx300"></param>
        private void HandleCommandResponsed(TX300 tx300)
        {
            CommandInstance.Update(f => f.DestinationNo == tx300.TerminalID &&
                f.Command == "0x" + CustomConvert.IntToDigit(tx300.CommandID, CustomConvert.HEX, 4) &&
                f.ScheduleTime >= DateTime.Now.AddMinutes(-3) &&
                f.Status >= (byte)CommandStatus.SentByTCP && f.Status <= (byte)CommandStatus.SentToDest,
                act => { act.Status = (byte)CommandStatus.Returned; });
        }
    }
}
