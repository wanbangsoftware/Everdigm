using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wbs.Everdigm.Common;
using Wbs.Everdigm.Database;
using Wbs.Utilities;

namespace Wbs.Everdigm.Desktop
{
    /// <summary>
    /// 提供处理铱星数据的集合
    /// </summary>
    public partial class DataHandler
    {
        /// <summary>
        /// 处理掉铱星发过来的数据
        /// </summary>
        /// <param name="data"></param>
        public void HandleIridiumData(IridiumData data)
        {
            switch (data.Type)
            {
                case IridiumDataType.MTServerSendStatus:
                    HandleIridiumMTServerSendStatus(data);
                    break;
                case IridiumDataType.MTModelReceiveStatus:
                    HandleIridiumMTModelReceiveStatus(data);
                    break;
                case IridiumDataType.MOPayload:
                    HandleIridiumModelMOPayload(data);
                    break;
            }
        }
        /// <summary>
        /// 处理铱星命令发送状态
        /// </summary>
        /// <param name="data"></param>
        private void HandleIridiumMTServerSendStatus(IridiumData data)
        {
            CommandInstance.Update(f => f.TB_Terminal.TB_Satellite.CardNo.Equals(data.IMEI) &&
                f.Status == (byte)CommandStatus.SatelliteHandled, action =>
                {
                    // 更新等待铱星处理的终端的命令为已发送状态
                    action.Status = (byte)CommandStatus.SentBySAT;
                    action.IridiumMTMSN = data.MTMSN;
                });
        }
        /// <summary>
        /// 处理铱星终端接收命令的状态
        /// </summary>
        /// <param name="data"></param>
        private void HandleIridiumMTModelReceiveStatus(IridiumData data)
        {
            CommandInstance.Update(f => f.TB_Terminal.TB_Satellite.CardNo.Equals(data.IMEI) &&
                f.IridiumMTMSN == data.MTMSN && f.Status == (byte)CommandStatus.SentBySAT, act =>
            {
                act.Status = (byte)CommandStatus.SentToDestBySAT;
            });
        }
        /// <summary>
        /// 处理铱星模块发送回来的MO信息
        /// </summary>
        /// <param name="data"></param>
        private void HandleIridiumModelMOPayload(IridiumData data)
        {
            // 更新命令的MTMSN状态为返回状态
            HandleData(new Sockets.AsyncUserDataBuffer()
            {
                Buffer = data.Payload,
                DataType = Sockets.AsyncUserDataType.ReceivedData,
                IP = "",
                PackageType = Sockets.AsyncDataPackageType.SAT,
                Port = 0,
                ReceiveTime = data.Time,
                SocketHandle = 0
            });
            data.Dispose();
        }
        /// <summary>
        /// 检测需要用铱星方式发送的命令
        /// </summary>
        public void CheckIridiumCommand()
        {
            if (null == _server) return;

            var list = CommandInstance.FindList(f => f.ScheduleTime >= DateTime.Now.AddSeconds(-30) &&
                f.Status == (byte)CommandStatus.WaitingForSatellite).ToList();
            if (null != list && list.Count > 0)
            {
                HandleIridiumCommand(list.First());
            }
        }
        private void HandleIridiumCommand(TB_Command obj)
        {
            if (null != OnIridiumSend)
            {
                IridiumDataEvent e = new IridiumDataEvent();
                e.Data = new IridiumData() { Payload = CustomConvert.GetBytes(obj.Content) };
                OnIridiumSend(this, e);
                // 更新命令发送状态
                CommandInstance.Update(f => f.id == obj.id, act =>
                {
                    act.Status = (byte)CommandStatus.SatelliteHandled;
                });
            }
        }

        public EventHandler<IridiumDataEvent> OnIridiumSend;
    }
}
