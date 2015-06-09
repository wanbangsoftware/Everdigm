using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wbs.Protocol.TX300;
using Wbs.Protocol.TX300.Analyse;
using Wbs.Protocol.WbsDateTime;
using Wbs.Everdigm.Database;
using Wbs.Utilities;
using Wbs.Sockets;

namespace Wbs.Everdigm.Desktop
{
    /// <summary>
    /// 处理数据的部分
    /// </summary>
    public partial class DataHandler
    {
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
                temp = null;
                x300.package_to_msg();
                // 保存历史记录
                //SaveTX300History(x300, data.ReceiveTime);
                // 更新设备状态
                HandleTX300Status(x300, data);
                // SMS消息不需要返回包
                if (x300.ProtocolType >= Protocol.ProtocolTypes.SMS) return;

                if (null != _server)
                {
                    if (x300.CommandID == 0xCC00)
                    {
                        byte[] cc00 = new byte[23];
                        Buffer.BlockCopy(x300.Content, 0, cc00, 0, 17);
                        cc00[0] = 0x17;
                        cc00[7] = 0xFF;
                        cc00[8] = 0xFF;
                        cc00[17] = 0x07;
                        WbsDateTime dt = new WbsDateTime(DateTime.Now);
                        Buffer.BlockCopy(dt.DateTimeToByte, 0, cc00, 19, 4);
                        dt = null;
                        var ret = 0;
                        if (data.PackageType == AsyncDataPackageType.TCP)
                            ret = _server.Send(data.SocketHandle, cc00);
                        else
                            ret = _server.Send(Port, IP, cc00);
                        if (1 != ret)
                        {
                            ShowUnhandledMessage(Now + string.Format("Cannot send data to {0}:{1}: {2} [{3}]",
                                IP, Port, CustomConvert.GetHex(cc00), data.PackageType));
                        }
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
                        var ret = 0;
                        if (data.PackageType == AsyncDataPackageType.TCP)
                            ret = _server.Send(data.SocketHandle, resp.Content);
                        else
                            ret = _server.Send(Port, IP, resp.Content);
                        if (1 != ret)
                        {
                            ShowUnhandledMessage(Now + string.Format("Cannot send data to {0}:{1}: {2} [{3}]",
                                IP, Port, CustomConvert.GetHex(resp.Content), data.PackageType));
                        }
                    }
                }
                HandleGsmCommandResponsed(x300);
                index += x300.TotalLength;
                x300 = null;
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
        /// 从协议中获取Sim卡号码
        /// </summary>
        /// <param name="tx300"></param>
        /// <returns></returns>
        private string GetSimFromData(TX300 tx300)
        {
            return (tx300.TerminalID[0] == '8' && tx300.TerminalID[1] == '9' ? tx300.TerminalID.Substring(0, 8) : tx300.TerminalID);
        }
        private bool IsTracker(ushort cmd)
        {
            return cmd >= 0x7000 && cmd <= 0x7040;
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
            if (!IsTracker(tx300.CommandID))
            {
                if (null == terminal) return;
            }

            HandleOnline(sim, tx300.CommandID, data);

            if (tx300.CommandID != 0xAA00)
            {

                // TX10G的数据
                if (tx300.CommandID >= 0x7000 && tx300.CommandID <= 0x7040)
                {
                    HandleTX10G(tx300, data);
                }
                else
                {
                    SaveTX300History(tx300, data.ReceiveTime,
                        (null == equipment ? "" : EquipmentInstance.GetFullNumber(equipment)));

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
                case 0x3000:
                    Handle0x3000(obj, equipment, terminal);
                    break;
                case 0x5000:
                    Handle0x5000(obj, equipment, terminal);
                    break;
                case 0x6000:
                    Handle0x6000(obj, equipment, terminal);
                    break;
                case 0x6001: break;
                case 0x6004:
                    Handle0x6004(obj, equipment, terminal);
                    break;
                case 0x6007:
                    Handle0x6007(obj, equipment, terminal);
                    break;
                case 0x600B:
                    Handle0x600B(obj, equipment, terminal);
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
                    Handle0xDD00(obj, equipment, terminal);
                    break;
                case 0xDD02:
                    break;
                case 0xEE00:
                    Handle0xEE00(obj, equipment, terminal);
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
            if (obj.CommandID == 0xDD00)
            {
                Buffer.BlockCopy(obj.MsgContent, 1, version, 0, 7);
            }
            else if (obj.CommandID == 0x1001)
            {
                Buffer.BlockCopy(obj.MsgContent, 25, version, 0, 7);
                // revision
                rev = obj.MsgContent[32];
            }
            else
            {
                Buffer.BlockCopy(obj.MsgContent, 0, version, 0, 7);
            }
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
        
        private string GetPackageType(byte packageType) {
            return packageType == Protocol.ProtocolTypes.SATELLITE ? "(SAT)" : "(GSM)";
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
                SaveGpsInfo(x1000.GPSInfo, equipment, obj.TerminalID, "Position command" + GetPackageType(obj.ProtocolType));
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
            //info.Type = "Period report" + GetPackageType(obj.ProtocolType);
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
            if (null != equipment)
            {
                EquipmentInstance.Update(f => f.id == equipment.id, act =>
                {
                    act.Signal = (x1001.CSQ_1 > 0 && x1001.CSQ_1 <= 31) ? x1001.CSQ_1 :
                        ((x1001.CSQ_2 > 0 && x1001.CSQ_2 <= 31) ? x1001.CSQ_2 : byte.MinValue);
                    // 去掉 0x1001 里面的运转时间更新
                    //if (x1001.WorkTime > 0)
                    //{
                    //    act.Runtime = (int)x1001.WorkTime;
                    //}
                });
            }

            var pos = GetGpsinfoFrom1001(x1001, true);
            if ((pos.Longitude > 0 && pos.Longitude < 180) && (pos.Latitude > 0 && pos.Latitude < 90))
            {
                pos.Equipment = null == equipment ? (int?)null : equipment.id;
                pos.Terminal = obj.TerminalID;
                pos.StoreTimes = null == equipment ? 0 : equipment.StoreTimes;
                pos.Type = "Period report" + GetPackageType(obj.ProtocolType);
                PositionInstance.Add(pos);
            }

            pos = GetGpsinfoFrom1001(x1001, false);
            if ((pos.Longitude > 0 && pos.Longitude < 180) && (pos.Latitude > 0 && pos.Latitude < 90))
            {
                pos.Equipment = null == equipment ? (int?)null : equipment.id;
                pos.Terminal = obj.TerminalID;
                pos.StoreTimes = null == equipment ? 0 : equipment.StoreTimes;
                pos.Type = "Period report" + GetPackageType(obj.ProtocolType);
                PositionInstance.Add(pos);
            }
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
            long gps = -1;
            if (x2000.GPSInfo.Available)
                gps = SaveGpsInfo(x2000.GPSInfo, equipment, obj.TerminalID, "Alarm report" + GetPackageType(obj.ProtocolType));
            // 保存报警信息
            SaveAlarm(equipment, obj.TerminalID, gps, x2000.AlarmBIN);
        }
        /// <summary>
        /// 保存报警记录
        /// </summary>
        /// <param name="equipment"></param>
        /// <param name="terminal"></param>
        /// <param name="posId"></param>
        /// <param name="alarm"></param>
        private void SaveAlarm(TB_Equipment equipment, string terminal_id, long posId, string alarm)
        {
            var obj = AlarmInstance.GetObject();
            obj.Code = alarm;
            obj.Equipment = null == equipment ? (int?)null : equipment.id;
            obj.Position = (0 >= posId ? (long?)null : posId);
            obj.StoreTimes = null == equipment ? 0 : equipment.StoreTimes;
            obj.Terminal = terminal_id;
            AlarmInstance.Add(obj);
        }
        private void Handle0x3000(TX300 obj, TB_Equipment equipment, TB_Terminal terminal)
        {
            _0x3000 x3000 = new _0x3000();
            x3000.Content = obj.MsgContent;
            x3000.Unpackage();
            if (null != equipment)
            {
                EquipmentInstance.Update(f => f.id == equipment.id, act => {
                    act.LockStatus = CustomConvert.GetHex(x3000.Type);
                    if (x3000.GPSInfo.Available)
                    {
                        act.Latitude = x3000.GPSInfo.Latitude;
                        act.Longitude = x3000.GPSInfo.Longitude;
                        act.GpsUpdated = false;
                    }
                });
            }
            if (x3000.GPSInfo.Available)
            {
                SaveGpsInfo(x3000.GPSInfo, equipment, obj.TerminalID, ("Security: " + x3000.Flag + GetPackageType(obj.ProtocolType)));
            }
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
            x5000.Type = obj.TerminalType;
            x5000.Content = obj.MsgContent;
            x5000.Unpackage();
            if (null != equipment)
            {
                EquipmentInstance.Update(f => f.id == equipment.id, act =>
                {
                    act.Voltage = string.Format("G{0}0", ((int)Math.Floor(x5000.GeneratorVoltage * 10)).ToString("000"));
                    if (x5000.GeneratorVoltage < 20)
                    { act.Rpm = 0; }
                    if (x5000.WorkTime > 0)
                    {
                        // 如果总运转时间大于等于当前服务器中保存的时间则更新，否则不更新
                        if (x5000.WorkTime >= act.Runtime)
                        {
                            act.Runtime = (int)x5000.WorkTime;
                        }
                    }
                    if (x5000.GPSInfo.Available)
                    {
                        act.Latitude = x5000.GPSInfo.Latitude;
                        act.Longitude = x5000.GPSInfo.Longitude;
                        act.GpsUpdated = false;
                    }
                });
            }
            if (x5000.GPSInfo.Available)
                SaveGpsInfo(x5000.GPSInfo, equipment, obj.TerminalID, "Eng.: " + x5000.State + GetPackageType(obj.ProtocolType));
        }
        /// <summary>
        /// 处理仪表盘数据
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="equipment"></param>
        /// <param name="terminal"></param>
        private void Handle0x6000(TX300 obj, TB_Equipment equipment, TB_Terminal terminal)
        {
            _0x6000 x6000 = new _0x6000();
            x6000.Content = obj.MsgContent;
            x6000.Unpackage();
            if (null != equipment)
            {
                EquipmentInstance.Update(f => f.id == equipment.id, act =>
                {
                    act.Rpm = (short)x6000.RPM;
                });
            }
        }
        /// <summary>
        /// 处理运转时间消息
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="equipment"></param>
        /// <param name="terminal"></param>
        private void Handle0x6004(TX300 obj, TB_Equipment equipment, TB_Terminal terminal)
        {
            _0x6004DX x6004 = new _0x6004DX();
            x6004.Type = obj.TerminalType;
            x6004.Content = obj.MsgContent;
            x6004.Unpackage();
            if (null != equipment)
            {
                if (x6004.TotalWorkTime > 0)
                {
                    EquipmentInstance.Update(f => f.id == equipment.id, act =>
                    {
                        // 如果回来的运转时间大于等于当前服务器上的运转时间则更新，否则不更新
                        if (x6004.TotalWorkTime >= act.Runtime)
                        {
                            act.Runtime = (int)x6004.TotalWorkTime;
                        }
                    });
                }
            }
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
                EquipmentInstance.Update(f => f.id == equipment.id, act =>
                {
                    act.LockStatus = CustomConvert.GetHex(x6007.Code);
                });
            }
        }
        /// <summary>
        /// 处理装载机的0x600B初始化运转时间的回复
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="equipment"></param>
        /// <param name="terminal"></param>
        private void Handle0x600B(TX300 obj, TB_Equipment equipment, TB_Terminal terminal)
        {
            _0x600B x600B = new _0x600B();
            x600B.Content = obj.MsgContent;
            x600B.Unpackage();
            if (null != equipment)
            {
                EquipmentInstance.Update(f => f.id == equipment.id, act =>
                {
                    act.Runtime = (int)x600B.Worktime;
                });
            }
        }
        /// <summary>
        /// 处理DD00命令
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="equipment"></param>
        /// <param name="terminal"></param>
        private void Handle0xDD00(TX300 obj, TB_Equipment equipment, TB_Terminal terminal)
        {
            _0xDD00 xdd00 = new _0xDD00();
            xdd00.Content = obj.MsgContent;
            xdd00.Unpackage();
            if (null != equipment)
            {
                EquipmentInstance.Update(f => f.id == equipment.id, act => { act.Signal = xdd00.CSQ; });
            }
            //if (null != terminal)
            //{
            //    TerminalInstance.Update(f => f.id == terminal.id, act => { act.Firmware = xdd00.Firmware; });
            //}
        }
        /// <summary>
        /// 处理EE00命令
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="equipment"></param>
        /// <param name="terminal"></param>
        private void Handle0xEE00(TX300 obj, TB_Equipment equipment, TB_Terminal terminal)
        {
            _0xEE00 xee00 = new _0xEE00();
            xee00.Content = obj.MsgContent;
            xee00.Unpackage();
            // 更新终端发送命令的结果
            if (null != equipment && xee00.ErrorCommand.Equals("0x6007"))
            {
                // 更新锁车状态
                EquipmentInstance.Update(f => f.id == equipment.id, act =>
                {
                    act.LockStatus = xee00.ErrorParamenter;
                });
            }
            // 更新命令的发送状态
            CommandInstance.Update(f => f.DestinationNo == obj.TerminalID &&
                     f.Command == xee00.ErrorCommand &&
                     f.ScheduleTime >= DateTime.Now.AddMinutes(-3) &&
                     f.Status >= (byte)CommandStatus.SentByTCP && f.Status <= (byte)CommandStatus.SentToDest,
                     act =>
                     {
                         byte b = 0x10;
                         switch (xee00.Error)
                         {
                             case 0x20:
                                 b = (byte)CommandStatus.EposFail;
                                 break;
                             case 0x30:
                                 b = (byte)CommandStatus.EngNotStart;
                                 break;
                             default:
                                 b = (byte)CommandStatus.NoFunction;
                                 break;
                         }
                         act.Status = b;
                     });
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
                SaveGpsInfo(xff00.GPSInfo, equipment, obj.TerminalID, "Battery OFF" + GetPackageType(obj.ProtocolType));
        }
        /// <summary>
        /// 保存位置信息
        /// </summary>
        /// <param name="obj"></param>
        private long SaveGpsInfo(GPSInfo obj, TB_Equipment equipment, string terminal, string type)
        {
            //if (!obj.Available) return -1;
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
            pos = PositionInstance.Add(pos);
            // 更新定位信息
            ShowUnhandledMessage("position: " + pos.id);
            return pos.id;
            //HandleGpsAddress(pos);
        }
        //private void HandleGpsAddress(TB_Data_Position pos)
        //{
        //    // 查询、更新地理位置
        //    GoogleGeocoder geocoder = new GoogleGeocoder() { ApiKey = GOOGLE_API_KEY, Language = "en" };
        //    //var addr = geocoder.ReverseGeocode(pos.Latitude.Value, pos.Longitude.Value);
        //    var addr = geocoder.ReverseGeocode(pos.Latitude.Value, pos.Longitude.Value);
        //    //addr.Start();
        //    //addr.Wait();
        //    var add = addr.First();
        //    PositionInstance.Update(f => f.id == pos.id, act =>
        //    {
        //        act.Address = addr.First().FormattedAddress;
        //        act.Updated = 2;
        //    });
        //}
    }
}
