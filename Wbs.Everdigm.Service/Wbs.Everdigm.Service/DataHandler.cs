using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

using Wbs.Everdigm.Database;
using Wbs.Protocol.TX300;
using Wbs.Protocol.TX300.Analyse;
using Wbs.Protocol.WbsDateTime;
using Wbs.Sockets;
using Wbs.Everdigm.Common;
using Wbs.Utilities;

namespace Wbs.Everdigm.Service
{
    /// <summary>
    /// 数据处理类
    /// </summary>
    public partial class DataHandler
    {
        /// <summary>
        /// 当前的Socket服务
        /// </summary>
        private SocketAsyncEventServer _server = null;
        /// <summary>
        /// 设置Socket服务
        /// </summary>
        public SocketAsyncEventServer Server { set { _server = value; } }
        /// <summary>
        /// 最后一次清理旧链接的时间
        /// </summary>
        private DateTime LastOlderLinkHandledTime;
        /// <summary>
        /// 是否已经到达清理旧链接记录的时间
        /// </summary>
        public bool CanClearOlderLinks
        {
            get
            {
                return LastOlderLinkHandledTime <= DateTime.Now.AddMinutes(-1);
            }
        }

        public DataHandler()
        {
            LastOlderLinkHandledTime = DateTime.Now;
        }
        /// <summary>
        /// 处理收到的数据
        /// </summary>
        /// <param name="data"></param>
        public void HandleData(AsyncUserDataBuffer data)
        {
            try
            {
                switch (data.DataType)
                {
                    case AsyncUserDataType.ClientConnected: break;
                    case AsyncUserDataType.ClientDisconnected:
                        // 处理客户端断开连接的情况
                        HandleClientDisconnect(data.SocketHandle);
                        break;
                    case AsyncUserDataType.ReceivedData:
                        var len = data.Buffer.Length;
                        // 如果收到的数据长度小于TX300的包头则不用处理数据了
                        if (len >= TX300Items.header_length &&
                            Wbs.Protocol.ProtocolTypes.IsTX300(data.Buffer[2]) &&
                            Wbs.Protocol.TerminalTypes.IsTX300(data.Buffer[3]))
                        {
                            if (data.Buffer[0] != len)
                            {
                                HandleException("Data length(package length: " +
                                    data.Buffer[0] + ", buffer length: " +
                                    len + ") error.", CustomConvert.GetHex(data.Buffer));
                            }
                            else
                            {
                                HandleReceivedData(data);
                            }
                        }
                        break;
                }
            }
            catch (Exception e)
            {
                HandleException(e.StackTrace, CustomConvert.GetHex(data.Buffer));
            }
        }
        /// <summary>
        /// 将异常保存在数据库中备查
        /// </summary>
        /// <param name="trace"></param>
        /// <param name="data"></param>
        private void HandleException(string trace, string data)
        {
            var obj = ErrorInstance.GetObject();
            obj.ErrorData = data;
            obj.ErrorMessage = trace;
            ErrorInstance.Add(obj);
        }
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
                if (ret == 0) { 
                    // TCP链接丢失，重新用SMS方式发送
                    CommandUtility.SendSMSCommand(cmd);
                }
                else
                {
                    UpdateCommand(cmd, (1 == ret ? CommandStatus.SentByTCP : CommandStatus.TCPNetworkError));
                }
            }
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
            TerminalInstance.Update(f => f.Socket == socket, act => {
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

            // 处理旧的TCP链接为SMS链接(1小时15分钟之前有TCP数据来的链接会被置为SMS)
            EquipmentInstance.Update(f => f.OnlineStyle < (byte)LinkType.SMS && f.OnlineTime < DateTime.Now.AddMinutes(-75), act =>
            {
                act.Socket = 0;
                act.OnlineStyle = (byte)LinkType.SMS;
                act.Voltage = "G0000";
            });
            // 处理终端连接
            TerminalInstance.Update(f => f.OnlineStyle < (byte)LinkType.SMS && f.OnlineTime < DateTime.Now.AddMinutes(-75), act => {
                act.Socket = 0;
                act.OnlineStyle = (byte)LinkType.SMS;
            });
            // 处理旧的SMS连接为SLEEP状态(SMS链接超过12小时的)
            EquipmentInstance.Update(f => f.OnlineStyle < (byte)LinkType.SLEEP && f.OnlineTime < DateTime.Now.AddMinutes(-720), act =>
            {
                act.OnlineStyle = (byte)LinkType.SLEEP;
            });
            // 处理终端连接
            TerminalInstance.Update(f => f.OnlineStyle < (byte)LinkType.SLEEP && f.OnlineTime < DateTime.Now.AddMinutes(-720), act =>
            {
                act.OnlineStyle = (byte)LinkType.SLEEP;
            });
            // 处理旧的SLEEP连接为盲区(SLEEP超过7天的)
            EquipmentInstance.Update(f => f.OnlineStyle < (byte)LinkType.BLIND && f.OnlineTime < DateTime.Now.AddMinutes(-10080), act =>
            {
                act.OnlineStyle = (byte)LinkType.BLIND;
            });
        }
        /// <summary>
        /// 处理接收到的数据
        /// </summary>
        /// <param name="data"></param>
        private void HandleReceivedData(AsyncUserDataBuffer data)
        {
            int index = 0, len = data.Buffer.Length;
            while (index < len)
            {
                byte[] temp = new byte[len - index];
                Buffer.BlockCopy(data.Buffer, index, temp, 0, len - index);
                TX300 x300 = new TX300();
                x300.Content = temp;
                x300.package_to_msg();
                // 保存历史记录
                //SaveTX300History(x300, data.ReceiveTime);
                // 更新设备状态
                HandleTX300Status(x300, data);
                if (null != _server)
                {
                    if (x300.CommandID == 0xCC00) {
                        byte[] cc00 = new byte[23];
                        Buffer.BlockCopy(x300.Content, 0, cc00, 0, 17);
                        cc00[0] = 0x17;
                        cc00[7] = 0xFF;
                        cc00[8] = 0xFF;
                        cc00[17] = 0x07;
                        WbsDateTime dt = new WbsDateTime(DateTime.Now);
                        Buffer.BlockCopy(dt.DateTimeToByte, 0, cc00, 19, 4);
                        dt = null;
                        if (data.PackageType == AsyncDataPackageType.TCP)
                            _server.Send(data.SocketHandle, cc00);
                        else
                            _server.Send(data.Port, data.IP, cc00);
                        cc00 = null;
                    }
                    else
                    {
                        // 返回回执
                        TX300Resp resp = new TX300Resp();
                        resp.CommandID = x300.CommandID;
                        resp.PackageID = x300.PackageID;
                        resp.SequenceID = x300.SequenceID;
                        resp.Status = 0;
                        resp.Package();
                        if (data.PackageType == AsyncDataPackageType.TCP)
                            _server.Send(data.SocketHandle, resp.Content);
                        else
                            _server.Send(data.Port, data.IP, resp.Content);
                    }
                }
                HandleCommandResponsed(x300);
                index += x300.TotalLength;
            }
        }
        /// <summary>
        /// 保存数据到历史记录表
        /// </summary>
        /// <param name="tx300"></param>
        private void SaveTX300History(TX300 tx300, DateTime receiveTime, string mac_id)
        {
            TB_HISTORIES obj = DataInstance.GetObject();
            obj.command_id = "0x" + CustomConvert.IntToDigit(tx300.CommandID, CustomConvert.HEX, 4);
            obj.mac_id = mac_id;
            obj.message_content = CustomConvert.GetHex(tx300.MsgContent);
            obj.message_type = 1;
            obj.package_id = tx300.PackageID;
            obj.protocol_type = tx300.ProtocolType;
            obj.protocol_version = tx300.ProtocolVersion;
            obj.receive_time = receiveTime;
            obj.sequence_id = tx300.SequenceID.ToString();
            obj.server_port = 31875;
            obj.terminal_id = tx300.TerminalID;
            obj.terminal_type = tx300.TerminalType;
            obj.total_length = (short)tx300.TotalLength;
            obj.total_package = tx300.TotalPackage;
            DataInstance.Add(obj);
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
                act.OnlineStyle = (byte)(data.PackageType == AsyncDataPackageType.TCP ? LinkType.TCP : LinkType.UDP);
                act.LastAction = "0x" + CustomConvert.IntToDigit(CommandID, CustomConvert.HEX, 4);
                act.LastActionBy = data.PackageType == AsyncDataPackageType.TCP ? "TCP" : "UDP";
                act.LastActionTime = data.ReceiveTime;
            });
            TerminalInstance.Update(f => f.Sim.Equals(sim), act =>
            {
                act.Socket = data.SocketHandle;
                act.OnlineStyle = (byte)(data.PackageType == AsyncDataPackageType.TCP ? LinkType.TCP : LinkType.UDP);
                act.OnlineTime = data.ReceiveTime;
            });
        }
        /// <summary>
        /// 从协议中获取Sim卡号码
        /// </summary>
        /// <param name="tx300"></param>
        /// <returns></returns>
        private string GetSimFromData(TX300 tx300)
        {
            return (tx300.TerminalID[0] == '8' && tx300.TerminalID[1] == '9' ? tx300.TerminalID.Substring(0, 8) : tx300.TerminalID);
        }
        /// <summary>
        /// 根据TX300数据包更新终端和设备的在线状态
        /// </summary>
        /// <param name="tx300"></param>
        private void HandleTX300Status(TX300 tx300, AsyncUserDataBuffer data)
        {
            var sim = GetSimFromData(tx300);
            var equipment = EquipmentInstance.Find(f => f.TB_Terminal.Sim.Equals(sim));
            var terminal = TerminalInstance.Find(f => f.Sim.Equals(sim));
            // 终端不存在的话，不用再继续处理数据了
            if (null == terminal) return;

            HandleOnline(sim, tx300.CommandID, data);

            if (tx300.CommandID != 0xAA00)
            {
                SaveTX300History(tx300, data.ReceiveTime,
                    (null == equipment ? "" : EquipmentInstance.GetFullNumber(equipment)));

                // TX10G的数据
                if (tx300.CommandID == 0x7010 || tx300.CommandID == 0x7020 || tx300.CommandID == 0x7030)
                {
                    HandleTX10G(tx300, data);
                }
                else
                {
                    // 根据命令的不同处理各个命令详情
                    HandleCommand(tx300, equipment, terminal);
                }
            }
        }
        private void HandleCommand(TX300 obj, TB_Equipment equipment, TB_Terminal terminal)
        {
            switch (obj.CommandID)
            {
                case 0x1000: 
                    Handle0x1000(obj, equipment, terminal); 
                    break;
                case 0x1001:
                    HandleTerminalVersion(obj, terminal);
                    Handle0x1001(obj, equipment, terminal);
                    break;
                case 0x2000: 
                    Handle0x2000(obj, equipment, terminal); 
                    break;
                case 0x5000: 
                    Handle0x5000(obj, equipment, terminal); 
                    break;
                case 0x6000: break;
                case 0x6001: break;
                case 0x6004: break;
                case 0x6007: 
                    Handle0x6007(obj, equipment, terminal); 
                    break;
                case 0xBB00:
                    HandleTerminalVersion(obj, terminal);
                    break;
                case 0xBB0F: 
                    break;
                case 0xCC00:
                    HandleTerminalVersion(obj, terminal);
                    break;
                case 0xDD00:
                    HandleTerminalVersion(obj, terminal);
                    break;
                case 0xDD02: 
                    break;
                case 0xEE00:
                    HandleTerminalVersion(obj, terminal);
                    break;
                case 0xFF00: 
                    Handle0xFF00(obj, equipment, terminal); 
                    break;
            }
        }
        /// <summary>
        /// 处理终端版本的信息
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="terminal"></param>
        private void HandleTerminalVersion(TX300 obj, TB_Terminal terminal)
        {
            // 处理终端版本信息
            byte[] version = new byte[7];
            byte rev = 0;
            if (obj.CommandID == 0xDD00) { Buffer.BlockCopy(obj.MsgContent, 0, version, 0, 7); }
            else if (obj.CommandID == 0x1001)
            {
                Buffer.BlockCopy(obj.MsgContent, 25, version, 0, 7);
                // revision
                rev = obj.MsgContent[32];
            }
            else { Buffer.BlockCopy(obj.MsgContent, 0, version, 0, 7); }
            string ver = ASCIIEncoding.ASCII.GetString(version);
            version = null;
            if (obj.CommandID == 0xBB00)
            {
                // revision
                string s = ASCIIEncoding.ASCII.GetString(obj.MsgContent, 7, 2);
                rev = byte.Parse(s);
            }
            TerminalInstance.Update(f => f.id == terminal.id, act =>
            {
                act.Firmware = ver;
                if (rev > 0)
                {
                    act.Revision = rev;
                }
            });
        }
        /// <summary>
        /// 处理命令回复的定位信息
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="equipment"></param>
        /// <param name="terminal"></param>
        private void Handle0x1000(TX300 obj, TB_Equipment equipment, TB_Terminal terminal)
        {
            _0x1000 x1000 = new _0x1000();
            x1000.Content = obj.MsgContent;
            x1000.Unpackage();
            if (null != equipment)
            {
                if (x1000.GPSInfo.Available)
                {
                    EquipmentInstance.Update(f => f.id == equipment.id, act =>
                    {
                        act.Latitude = x1000.GPSInfo.Latitude;
                        act.Longitude = x1000.GPSInfo.Longitude;
                        act.GpsUpdated = false;
                    });
                }
            }
            if (x1000.GPSInfo.Available)
                SaveGpsInfo(x1000.GPSInfo, equipment, obj.TerminalID, "0x1000");
        }
        /// <summary>
        /// 从0x1001数据中取得定位信息记录
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="first"></param>
        /// <returns></returns>
        private TB_Data_Position GetGpsinfoFrom1001(_0x1001 obj, bool first)
        {
            TB_Data_Position info = PositionInstance.GetObject();
            info.Csq = first ? obj.CSQ_1 : obj.CSQ_2;
            info.GpsTime = first ? obj.GPSTime : obj.GPSTime.AddMinutes(30);
            info.Latitude = first ? obj.Latitude_1 : obj.Latitude_2;
            info.Longitude = first ? obj.Longitude_1 : obj.Longitude_2;
            //info.SectorCode = obj.Sector;
            //info.StationCode = obj.Station;
            info.Type = "0x1001";
            return info;
        }
        /// <summary>
        /// 处理定期报告信息
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="equipment"></param>
        /// <param name="terminal"></param>
        private void Handle0x1001(TX300 obj, TB_Equipment equipment, TB_Terminal terminal)
        {
            _0x1001 x1001 = new _0x1001();
            x1001.Content = obj.MsgContent;
            x1001.Unpackage();
            if (null != equipment) {
                EquipmentInstance.Update(f => f.id == equipment.id, act => {
                    act.Signal = x1001.CSQ_1 > 0 ? x1001.CSQ_1 : (x1001.CSQ_2 > 0 ? x1001.CSQ_2 : byte.MinValue);
                    if (x1001.WorkTime > 0)
                    {
                        act.Runtime = (int)x1001.WorkTime;
                    }
                });
            }

            var pos = GetGpsinfoFrom1001(x1001, true);
            pos.Equipment = null == equipment ? (int?)null : equipment.id;
            pos.Terminal = obj.TerminalID;
            pos.StoreTimes = null == equipment ? 0 : equipment.StoreTimes;
            PositionInstance.Add(pos);

            pos = GetGpsinfoFrom1001(x1001, false);
            pos.Equipment = null == equipment ? (int?)null : equipment.id;
            pos.Terminal = obj.TerminalID;
            pos.StoreTimes = null == equipment ? 0 : equipment.StoreTimes;
            PositionInstance.Add(pos);
        }
        /// <summary>
        /// 处理报警信息
        /// </summary>
        /// <param name="obj"></param>
        private void Handle0x2000(TX300 obj, TB_Equipment equipment, TB_Terminal terminal)
        {
            // 报警
            _0x2000 x2000 = new _0x2000();
            x2000.Content = obj.MsgContent;
            x2000.Unpackage();
            if (null != equipment)
            {
                EquipmentInstance.Update(f => f.id == equipment.id, act =>
                {
                    if (x2000.AlarmBIN[0] == '1')
                    {
                        // Main Power Disconnect
                        act.Voltage = "G0000";
                        // 主电断报警之后进入睡眠状态
                        act.OnlineStyle = (byte)LinkType.SLEEP;
                    }
                    if (x2000.GPSInfo.Available)
                    {
                        act.Latitude = x2000.GPSInfo.Latitude;
                        act.Longitude = x2000.GPSInfo.Longitude;
                        act.GpsUpdated = false;
                    }
                });
            }
            if (x2000.GPSInfo.Available)
                SaveGpsInfo(x2000.GPSInfo, equipment, obj.TerminalID, "0x2000");
        }
        /// <summary>
        /// 处理设备的开关机信息
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="equipment"></param>
        /// <param name="terminal"></param>
        private void Handle0x5000(TX300 obj, TB_Equipment equipment, TB_Terminal terminal)
        {
            // 发动机启动/关闭信息
            _0x5000 x5000 = new _0x5000();
            x5000.Content = obj.MsgContent;
            x5000.Unpackage();
            if (null != equipment)
            {
                EquipmentInstance.Update(f => f.id == equipment.id, act =>
                {
                    act.Voltage = string.Format("G{0}", ((int)Math.Floor(x5000.GeneratorVoltage * 10)).ToString("0000"));

                    if (x5000.GPSInfo.Available)
                    {
                        act.Latitude = x5000.GPSInfo.Latitude;
                        act.Longitude = x5000.GPSInfo.Longitude;
                        act.GpsUpdated = false;
                    }
                });
            }
            if (x5000.GPSInfo.Available)
                SaveGpsInfo(x5000.GPSInfo, equipment, obj.TerminalID, "0x5000");
        }
        /// <summary>
        /// 处理保安命令
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="equipment"></param>
        /// <param name="terminal"></param>
        private void Handle0x6007(TX300 obj, TB_Equipment equipment, TB_Terminal terminal)
        {
            _0x6007 x6007 = new _0x6007();
            x6007.Content = obj.MsgContent;
            x6007.Unpackage();
            if (null != equipment)
            {
                EquipmentInstance.Update(f => f.id == equipment.id, act => {
                    act.LockStatus = CustomConvert.GetHex(x6007.Code);
                });
            }
        }
        /// <summary>
        /// 处理终端OFF的信息
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="equipment"></param>
        /// <param name="terminal"></param>
        private void Handle0xFF00(TX300 obj, TB_Equipment equipment, TB_Terminal terminal)
        {
            // 终端没电关机
            _0xFF00 xff00 = new _0xFF00();
            xff00.Content = obj.MsgContent;
            xff00.Unpackage();
            if (null != equipment)
            {
                EquipmentInstance.Update(f => f.id == equipment.id, act =>
                {
                    act.Voltage = "G0000";
                    act.OnlineStyle = (byte)LinkType.OFF;
                    if (xff00.GPSInfo.Available)
                    {
                        act.Latitude = xff00.GPSInfo.Latitude;
                        act.Longitude = xff00.GPSInfo.Longitude;
                        act.GpsUpdated = false;
                    }
                });
            }
            if (xff00.GPSInfo.Available)
                SaveGpsInfo(xff00.GPSInfo, equipment, obj.TerminalID, "0xFF00");
        }
        /// <summary>
        /// 保存位置信息
        /// </summary>
        /// <param name="obj"></param>
        private void SaveGpsInfo(GPSInfo obj, TB_Equipment equipment, string terminal, string type)
        {
            TB_Data_Position pos = PositionInstance.GetObject();
            pos.Altitude = obj.Altitude;
            pos.Direction = obj.Direction;
            pos.Equipment = null == equipment ? (int?)null : equipment.id;
            pos.EW = obj.EW[0];
            pos.GpsTime = obj.GPSTime;
            pos.Latitude = obj.Latitude;
            pos.Longitude = obj.Longitude;
            pos.NS = obj.NS[0];
            pos.ReceiveTime = DateTime.Now;
            pos.Speed = obj.Speed;
            pos.StoreTimes = null == equipment ? 0 : equipment.StoreTimes;
            pos.Terminal = terminal;
            pos.Type = type;
            PositionInstance.Add(pos);
        }
        /// <summary>
        /// 处理TX10G的数据
        /// </summary>
        /// <param name="tx300"></param>
        /// <param name="data"></param>
        private void HandleTX10G(TX300 tx300, AsyncUserDataBuffer data)
        { }
    }
}