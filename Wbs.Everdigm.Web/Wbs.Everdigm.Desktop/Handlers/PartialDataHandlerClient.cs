using System;
using Wbs.Sockets;
using Wbs.Utilities;
using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.Desktop
{
    /// <summary>
    /// 终端连接相关部分
    /// </summary>
    public partial class DataHandler
    {
        /// <summary>
        /// 最后一次清理旧链接的时间
        /// </summary>
        private DateTime LastOlderLinkHandledTime = DateTime.Now;
        /// <summary>
        /// 是否已经到达清理旧链接记录的时间
        /// </summary>
        public bool CanClearOlderLinks
        {
            get
            {
                return LastOlderLinkHandledTime.Subtract(DateTime.Now).Duration().TotalSeconds >= 120;
            }
        }

        /// <summary>
        /// 更新客户端断开连接之后的在线状况
        /// </summary>
        /// <param name="socket"></param>
        private void HandleClientDisconnect(int socket)
        {
            // 更新设备的在线状态
            EquipmentInstance.Update(f => f.Socket == socket, act =>
            {
                act.Socket = 0;
                act.OnlineStyle = (byte)LinkType.SMS;
                act.Voltage = "G0000";
            });
            // 更新终端的在线状态
            TerminalInstance.Update(f => f.Socket == socket, act =>
            {
                act.Socket = 0;
                act.OnlineStyle = (byte)LinkType.SMS;
            });
        }
        /// <summary>
        /// 处理旧的链接或长时间未动的客户端节点
        /// </summary>
        public void HandleOlderClients()
        {
            LastOlderLinkHandledTime = DateTime.Now;

            try
            {
                // 清理旧链接的方式改为用存储过程 2015/10/01 22:30
                EverdigmDataContext edc = new EverdigmDataContext();
                int? arm2sleep = 0, sleep2blind = 0, sms2sleep = 0, tcpudp2sms = 0, satellite = 0, terminals = 0;
                var ret = edc.sp_ClearOldLinks(DateTime.Now, ref arm2sleep, ref sleep2blind, ref sms2sleep, ref tcpudp2sms, ref satellite, ref terminals);
                if (arm2sleep > 0 || sleep2blind > 0 || sms2sleep > 0 || tcpudp2sms > 0 || satellite > 0 || terminals > 0)
                {
                    ShowUnhandledMessage(
                        string.Format("{0}HandleOlderClients(ret: {1}): ARM to Sleep: {2}, Sleep to Blind: {3}, SMS to Sleep: {4}, TCP/UDP to SMS: {5}",
                        Now, ret, arm2sleep, sleep2blind, sms2sleep, tcpudp2sms));
                }
                // 所有更新集合到一个事务中2015/08/14
                // 更新设备状态的同时也更新终端的状态 2015/09/22 09:30
                // 更新设备连接状态
                //EquipmentInstance.Update(f => f.OnlineStyle > (byte)LinkType.OFF && f.OnlineTime < DateTime.Now.AddMinutes(-75), act =>
                //{
                //    act.Socket = 0;
                //    act.Voltage = "G0000";
                //    act.Rpm = 0;
                //    act.Signal = 0;
                //    // 长时间主电报警的睡眠转为Off
                //    if (act.OnlineTime < DateTime.Now.AddMinutes(-4320) && act.OnlineStyle == (byte)LinkType.SLEEP &&
                //        (act.Alarm.Substring(0, 1) == "1" || act.Alarm.Substring(15, 1) == "1"))
                //    {
                //        act.OnlineStyle = (byte)LinkType.OFF;
                //        if ((int?)null != act.Terminal)
                //            act.TB_Terminal.OnlineStyle = (byte)LinkType.OFF;
                //    }
                //    // 长时间睡眠的转为盲区
                //    else if (act.OnlineTime < DateTime.Now.AddMinutes(-10800) && act.OnlineStyle < (byte)LinkType.BLIND)
                //    {
                //        act.OnlineStyle = (byte)LinkType.BLIND;
                //        if ((int?)null != act.Terminal)
                //            act.TB_Terminal.OnlineStyle = (byte)LinkType.BLIND;
                //    }
                //    // 长时间SMS的转为睡眠
                //    else if (act.OnlineTime < DateTime.Now.AddMinutes(-720) && act.OnlineStyle < (byte)LinkType.SLEEP)
                //    {
                //        act.OnlineStyle = (byte)LinkType.SLEEP;
                //        if ((int?)null != act.Terminal)
                //            act.TB_Terminal.OnlineStyle = (byte)LinkType.SLEEP;
                //    }
                //    // 长时间没有UDP、TCP信息的转为SMS
                //    else if (act.OnlineStyle < (byte)LinkType.SMS)
                //    {
                //        act.OnlineStyle = (byte)LinkType.SMS;
                //        if ((int?)null != act.Terminal)
                //            act.TB_Terminal.OnlineStyle = (byte)LinkType.SMS;
                //    }
                //    // 卫星超过2小时没有数据的，Eng. On更新为Eng. Off
                //    else if (act.OnlineTime < DateTime.Now.AddMinutes(-120) && act.OnlineStyle == (byte)LinkType.SATELLITE
                //        && act.Voltage != "G0000")
                //    { act.Voltage = "G0000"; }
                //});
            }
            catch (Exception e)
            {
                ShowUnhandledMessage(string.Format("{0}HandleOlderClients error: {1}{2}StackTrace: {3}", Now, e.Message, Environment.NewLine, e.StackTrace));
            }
            // 更新终端连接状态
            //TerminalInstance.Update(f => f.OnlineStyle > (byte)LinkType.OFF && f.OnlineTime < DateTime.Now.AddMinutes(-75), act =>
            //{
            //    act.Socket = 0;
            //    if (act.OnlineTime < DateTime.Now.AddMinutes(-10800) && act.OnlineStyle < (byte)LinkType.BLIND)
            //    { act.OnlineStyle = (byte)LinkType.BLIND; }
            //    else if (act.OnlineTime < DateTime.Now.AddMinutes(-720) && act.OnlineStyle < (byte)LinkType.SLEEP)
            //    { act.OnlineStyle = (byte)LinkType.SLEEP; }
            //    else if (act.OnlineStyle < (byte)LinkType.SMS)
            //    { act.OnlineStyle = (byte)LinkType.SMS; }
            //});

            // 2015-04-08 16:40 更新：不处理已经为OFF状态的列表
            // 处理旧的TCP链接为SMS链接(1小时15分钟之前有TCP数据来的链接会被置为SMS)
            //EquipmentInstance.Update(f => f.OnlineStyle > (byte)LinkType.OFF && f.OnlineStyle < (byte)LinkType.SMS &&
            //    f.OnlineTime < DateTime.Now.AddMinutes(-75), act =>
            //    {
            //        act.Socket = 0;
            //        act.OnlineStyle = (byte)LinkType.SMS;
            //        act.Voltage = "G0000";
            //        act.Rpm = 0;
            //    });
            // 处理终端连接
            //TerminalInstance.Update(f => f.OnlineStyle > (byte)LinkType.OFF && f.OnlineStyle < (byte)LinkType.SMS &&
            //    f.OnlineTime < DateTime.Now.AddMinutes(-75), act =>
            //    {
            //        act.Socket = 0;
            //        act.OnlineStyle = (byte)LinkType.SMS;
            //    });
            // 清理TX10G的旧链接记录
            TrackerInstance.Update(f => f.State > 0 && f.LastActionAt < DateTime.Now.AddMinutes(-15), act =>
            {
                // 删除socket
                act.Socket = 0;
                // 状态标记为offline状态
                act.State = 0;
            });
            // 处理旧的SMS连接为SLEEP状态(SMS链接超过12小时的)
            //EquipmentInstance.Update(f => f.OnlineStyle > (byte)LinkType.OFF && f.OnlineStyle < (byte)LinkType.SLEEP &&
            //    f.OnlineTime < DateTime.Now.AddMinutes(-720), act =>
            //    {
            //        act.OnlineStyle = (byte)LinkType.SLEEP;
            //        act.Signal = 0;
            //        act.Voltage = "G0000";
            //    });
            // 处理终端连接
            //TerminalInstance.Update(f => f.OnlineStyle > (byte)LinkType.OFF && f.OnlineStyle < (byte)LinkType.SLEEP &&
            //    f.OnlineTime < DateTime.Now.AddMinutes(-720), act =>
            //    {
            //        act.OnlineStyle = (byte)LinkType.SLEEP;
            //    });
            // 处理旧的SLEEP连接为盲区(SLEEP超过7天的)
            //EquipmentInstance.Update(f => f.OnlineStyle > (byte)LinkType.OFF && f.OnlineStyle < (byte)LinkType.BLIND &&
            //    f.OnlineTime < DateTime.Now.AddMinutes(-10080), act =>
            //    {
            //        act.OnlineStyle = (byte)LinkType.BLIND;
            //        act.Signal = 0;
            //        act.Voltage = "G0000";
            //    });
        }
        private byte GetOnlineStyleByPackage(AsyncDataPackageType type)
        {
            byte ret = (byte)LinkType.TCP;
            switch (type)
            {
                case AsyncDataPackageType.SAT: ret = (byte)LinkType.SATELLITE; break;
                case AsyncDataPackageType.SMS: ret = (byte)LinkType.SMS; break;
                case AsyncDataPackageType.TCP: ret = (byte)LinkType.TCP; break;
                case AsyncDataPackageType.UDP: ret = (byte)LinkType.UDP; break;
            }
            return ret;
        }
        private string GetOnlineStyle(AsyncDataPackageType type)
        {
            string ret = "TCP";
            switch (type)
            {
                case AsyncDataPackageType.SAT: ret = "SAT"; break;
                case AsyncDataPackageType.SMS: ret = "SMS"; break;
                case AsyncDataPackageType.TCP: ret = "TCP"; break;
                case AsyncDataPackageType.UDP: ret = "UDP"; break;
            }
            return ret;
        }
        /// <summary>
        /// 更新在线时间和在线状态
        /// </summary>
        private void HandleOnline(string sim, ushort CommandID, AsyncUserDataBuffer data)
        {
            EquipmentInstance.Update(f => f.TB_Terminal.Sim.Equals(sim), act =>
            {
                act.IP = data.IP;
                act.Port = data.Port;
                act.Socket = data.SocketHandle;
                act.OnlineTime = data.ReceiveTime;
                // 处理当发送报警信息时设备已经是OFF状态的情况：不处理
                if (act.OnlineStyle == (byte)LinkType.OFF && CommandID == 0x2000)
                {
                    // 收到报警时不处理已经是OFF状态
                }
                else
                {
                    act.OnlineStyle = GetOnlineStyleByPackage(data.PackageType);
                }
                act.LastAction = "0x" + CustomConvert.IntToDigit(CommandID, CustomConvert.HEX, 4);
                act.LastActionBy = GetOnlineStyle(data.PackageType);
                act.LastActionTime = data.ReceiveTime;
            });
            TerminalInstance.Update(f => f.Sim.Equals(sim), act =>
            {
                act.Socket = data.SocketHandle;
                if (act.OnlineStyle == (byte)LinkType.OFF && CommandID == 0x2000)
                {
                    // 收到报警但此时已经是OFF状态时，不更新在线状态
                }
                else
                {
                    act.OnlineStyle = GetOnlineStyleByPackage(data.PackageType);
                }
                act.OnlineTime = data.ReceiveTime;
            });
            // 查看是否为TX10G的命令
            if (CommandID >= 0x7000 && CommandID <= 0x7040)
            {
                TrackerInstance.Update(f => f.SimCard.Equals(sim), act =>
                {
                    // 标记为在线状态
                    act.State = 1;
                    act.LastActionAt = DateTime.Now;
                    act.Socket = data.SocketHandle;
                });
            }
        }
    }
}
