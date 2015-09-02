﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

using Wbs.Everdigm.Common;
using Wbs.Everdigm.Database;
using Wbs.Protocol.TX300;
using Wbs.Utilities;
using Wbs.Sockets;

namespace Wbs.Everdigm.Desktop
{
    /// <summary>
    /// 处理命令相关的部分
    /// </summary>
    public partial class DataHandler
    {
        /// <summary>
        /// 铱星命令超时清理时间
        /// </summary>
        private int IRIDIUM_MT_TIMEOUT = int.Parse(ConfigurationManager.AppSettings["IRIDIUM_MT_TIMEOUT"]);
        /// <summary>
        /// 检索是否有TCP在线终端的命令待发送。
        /// 条件：
        /// 1、预定发送时间在30s以内
        /// 2、命令的状态为等待发送状态(等待发送或等待重发)
        /// 3、终端为TCP链接状态的
        /// </summary>
        /// <returns></returns>
        public void CheckTcpCommand()
        {
            if (null == _server) return;

            var list = CommandInstance.FindList(f => f.ScheduleTime >= DateTime.Now.AddSeconds(-30) &&
                GsmStatus.Take(2).Contains(f.Status.Value) &&
                f.TB_Terminal.OnlineStyle == (byte)LinkType.TCP).ToList();
            foreach (var cmd in list)
            {
                var sim = cmd.DestinationNo;
                if (sim[0] == '8' || sim[0] == '9') sim = sim.Substring(0, 8);
                // 0==链接不存在1=发送成功2=网络处理错误
                var ret = _server.Send(cmd.TB_Terminal.Socket.Value, Wbs.Utilities.CustomConvert.GetBytes(cmd.Content));
                if (ret == 0)
                {
                    // TCP链接丢失，重新用SMS方式发送
                    CommandUtility.SendSMSCommand(cmd);
                    SaveTerminalData((int?)null == cmd.Terminal ? -1 : cmd.Terminal.Value, sim, AsyncDataPackageType.SMS, 1, false);
                    ShowUnhandledMessage(Now + "Send Command(SentBySMS): " + cmd.Content);
                }
                else
                {
                    SaveTerminalData((int?)null == cmd.Terminal ? -1 : cmd.Terminal.Value, sim, AsyncDataPackageType.TCP, cmd.Content.Length / 2, false);
                    ShowUnhandledMessage(Now + "Send command(" + (1 == ret ? CommandStatus.SentByTCP : CommandStatus.TCPNetworkError) + "): " + cmd.Content);
                    UpdateGsmCommand(cmd, (1 == ret ? CommandStatus.SentByTCP : CommandStatus.TCPNetworkError));
                }
            }
            // 待发送的命令发送完之后，清理超时的命令
            ClearTimedoutCommands();
        }
        private List<byte> _iridiumStatus = null;
        /// <summary>
        /// 铱星命令发送状态
        /// </summary>
        private List<byte> IridiumStatus
        {
            get
            {
                if (null == _iridiumStatus)
                {
                    _iridiumStatus = new List<byte>();
                    _iridiumStatus.Add((byte)CommandStatus.WaitingForSatellite);
                    _iridiumStatus.Add((byte)CommandStatus.SatelliteHandled);
                    _iridiumStatus.Add((byte)CommandStatus.SentBySAT);
                    _iridiumStatus.Add((byte)CommandStatus.SentToDestBySAT);
                }
                return _iridiumStatus;
            }
        }
        private List<byte> _gsmStatus = null;
        private List<byte> GsmStatus
        {
            get
            {
                if (null == _gsmStatus)
                {
                    _gsmStatus = new List<byte>();
                    _gsmStatus.Add((byte)CommandStatus.Waiting);
                    _gsmStatus.Add((byte)CommandStatus.ReSending);
                    _gsmStatus.Add((byte)CommandStatus.SentByTCP);
                    _gsmStatus.Add((byte)CommandStatus.SentBySMS);
                    _gsmStatus.Add((byte)CommandStatus.SentToDest);
                }
                return _gsmStatus;
            }
        }
        /// <summary>
        /// 清理超时的命令为发送失败状态
        /// </summary>
        private void ClearTimedoutCommands()
        {
            // 更新GSM状态下的命令超时的记录
            CommandInstance.Update(f => f.ScheduleTime <= DateTime.Now.AddMinutes(-5) && GsmStatus.Contains(f.Status.Value), act =>
            {
                act.Status = (byte)CommandStatus.Timedout;
            });
            // 更新卫星发送下的超时命令记录，超过80分钟没有回复的，当作超时
            CommandInstance.Update(f => IridiumStatus.Skip(2).Contains(f.Status.Value) &&
                f.ScheduleTime <= DateTime.Now.AddMinutes(-IRIDIUM_MT_TIMEOUT), act =>
            {
                act.Status = (byte)CommandStatus.Timedout;
            });
        }
        /// <summary>
        /// 更新命令的发送状态
        /// </summary>
        /// <param name="obj"></param>
        private void UpdateGsmCommand(TB_Command obj, CommandStatus status)
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
        /// 更新铱星方式发送命令的状态
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="status"></param>
        private void UpdateIridiumCommand(TB_Command obj, CommandStatus status)
        {
            CommandInstance.Update(f => f.id == obj.id, act =>
            {
                act.Status = (byte)status;
                act.ActualSendTime = DateTime.Now;
            });
        }
        /// <summary>
        /// 处理GSM命令回复状态
        /// </summary>
        /// <param name="tx300"></param>
        private void HandleGsmCommandResponsed(TX300 tx300)
        {
            // 查找定期时间内发送的相同命令记录，直取最后一条命令
            var cmd = CommandInstance.FindList<TB_Command>(f => f.DestinationNo.Equals(tx300.TerminalID) &&
                f.Command.Equals("0x" + CustomConvert.IntToDigit(tx300.CommandID, CustomConvert.HEX, 4)) &&
                f.ScheduleTime >= DateTime.Now.AddMinutes(tx300.ProtocolType == Protocol.ProtocolTypes.SATELLITE ? -60 : -3),
                "ScheduleTime", true).FirstOrDefault();

            if (null != cmd)
            {
                CommandInstance.Update(f => f.id == cmd.id, act =>
                {
                    act.Status = (byte)CommandStatus.Returned;
                });
            }
            //CommandInstance.Update(f => f.DestinationNo == tx300.TerminalID &&
            //    f.Command == "0x" + CustomConvert.IntToDigit(tx300.CommandID, CustomConvert.HEX, 4) &&
            //    f.ScheduleTime >= DateTime.Now.AddMinutes(-3) //&&
            //    //GsmStatus.Skip(2).Contains(f.Status.Value),
            //    , act => { act.Status = (byte)CommandStatus.Returned; });
        }
    }
}
