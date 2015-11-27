using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Wbs.Everdigm.BLL;
using Wbs.Everdigm.Database;
using Wbs.Protocol;
using Wbs.Protocol.TX300;
using Wbs.Protocol.TX300.Analyse;
using Wbs.Utilities;
using Newtonsoft.Json;

namespace Wbs.Everdigm.Web.ajax
{
    /// <summary>
    /// satellite 的摘要说明
    /// </summary>
    public class satellite : BaseHttpHandler
    {
        private string FMT_RETURN = "\"status\":{0},\"desc\":\"{1}\",\"date\":\"{2}\"";
        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);
            HandleRequest();
        }
        /// <summary>
        /// 获取返回数据
        /// </summary>
        /// <param name="status"></param>
        /// <param name="desc"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private string GetResponseMessage(int status,string desc,string data)
        {
            return string.Format(FMT_RETURN, status, desc, data);
        }
        /// <summary>
        /// 处理卫星通讯的请求
        /// </summary>
        private void HandleRequest()
        {
            switch (type)
            { 
                case "data":
                    HandleDataRequest();
                    break;
                case "command":
                    // 查找卫星方式连接的终端的命令
                    HandleCommandRequest();
                    break;
            }
        }

        class Command
        {
            public Command(TB_Command obj)
            {
                Status = null == obj ? 0 : 1;
                Id = null == obj ? 0 : obj.id;
                Destination = null == obj ? "" : obj.TB_Terminal.TB_Satellite.CardNo;
                Content = null == obj ? "" : obj.Content;
            }
            /// <summary>
            /// 状态0=没有命令1=有命令
            /// </summary>
            public int Status { get; set; }
            /// <summary>
            /// 命令的Id
            /// </summary>
            public int Id { get; set; }
            /// <summary>
            /// 目的号码
            /// </summary>
            public string Destination { get; set; }
            /// <summary>
            /// 命令内容
            /// </summary>
            public string Content { get; set; }
        }
        /// <summary>
        /// 处理查询命令的请求
        /// </summary>
        private void HandleCommandRequest()
        {
            var CommandInstance = new CommandBLL();
            if (string.IsNullOrEmpty(cmd))
            {
                var ret = "[]";
                var obj = CommandInstance.Find(f => f.ScheduleTime >= DateTime.Now.AddSeconds(-60) &&
                    f.Status <= (byte)CommandStatus.WaitingForSMS &&
                    f.TB_Terminal.OnlineStyle == (byte)LinkType.SATELLITE);
                var command = new Command(obj);
                ret = JsonConvert.SerializeObject(command);

                ResponseJson(ret);
            }
            else
            { 
                // 更新命令发送状态
                CommandInstance.Update(f => f.id == int.Parse(cmd), act =>
                {
                    act.Status = (byte)CommandStatus.SentBySAT;
                    act.ActualSendTime = DateTime.Now;
                });
            }
        }
        /// <summary>
        /// 处理卫星服务器发过来的通讯信息数据。type=data,cmd=destination,data=数据内容
        /// </summary>
        /// <returns></returns>
        private string HandleDataRequest()
        {
            var ret = "";
            TX300 x300 = new TX300();
            x300.Content = CustomConvert.GetBytes(data);
            x300.package_to_msg();
            HandleDataStatus(x300);
            return ret;
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
        /// 终端业务BLL
        /// </summary>
        private TerminalBLL TerminalInstance { get { return new TerminalBLL(); } }
        /// <summary>
        /// 设备业务BLL
        /// </summary>
        private EquipmentBLL EquipmentInstance { get { return new EquipmentBLL(); } }
        /// <summary>
        /// 处理数据并更新终端和设备的状态
        /// </summary>
        /// <param name="obj"></param>
        private void HandleDataStatus(TX300 obj)
        {
            var sim = GetSimFromData(obj);
            var terminal = TerminalInstance.Find(f => f.Sim.Equals(sim));
            if (null == terminal) return;

            var equipment = EquipmentInstance.Find(f => f.TB_Terminal.Sim.Equals(sim) && f.Deleted == false);

            HandleOnline(sim, obj.CommandID);
            SaveTX300History(obj, (null == equipment ? "" : EquipmentInstance.GetFullNumber(equipment)));
            // 卫星数据只有0x1000和0x6007(0x6007会有0xEE00存在)两种
            switch (obj.CommandID)
            {
                case 0x1000:
                    Handle0x1000(obj, equipment);
                    break;
                case 0x6007:
                    Handle0x6007(obj, equipment);
                    break;
                case 0xEE00:
                    Handle0xEE00(obj, equipment);
                    // 483130433130372007600140A5
                    // 5831304331303530076001403F
                    break;
            }
        }
        /// <summary>
        /// 更新在线时间和在线状态
        /// </summary>
        private void HandleOnline(string sim, ushort CommandID)
        {
            // 更新设备在线状态为卫星通信
            EquipmentInstance.Update(f => f.TB_Terminal.Sim.Equals(sim) && f.Deleted == false, act =>
            {
                act.IP = "";
                act.Port = 0;
                act.Socket = 0;
                act.OnlineTime = DateTime.Now;
                act.OnlineStyle = (byte)(LinkType.SATELLITE);
                act.LastAction = "0x" + CustomConvert.IntToDigit(CommandID, CustomConvert.HEX, 4);
                act.LastActionBy = "SAT";
                act.LastActionTime = DateTime.Now;
            });
            // 更新终端在线状态为卫星通信
            TerminalInstance.Update(f => f.Sim.Equals(sim), act =>
            {
                act.Socket = 0;
                act.OnlineStyle = (byte)(LinkType.SATELLITE);
                act.OnlineTime = DateTime.Now;
            });
        }
        /// <summary>
        /// 保存数据到历史记录表
        /// </summary>
        /// <param name="tx300"></param>
        private void SaveTX300History(TX300 tx300, string mac_id)
        {
            var DataInstance = new DataBLL();
            TB_HISTORIES obj = DataInstance.GetObject();
            obj.command_id = "0x" + CustomConvert.IntToDigit(tx300.CommandID, CustomConvert.HEX, 4);
            obj.mac_id = mac_id;
            obj.message_content = CustomConvert.GetHex(tx300.MsgContent);
            obj.message_type = 1;
            obj.package_id = tx300.PackageID;
            obj.protocol_type = tx300.ProtocolType;
            obj.protocol_version = tx300.ProtocolVersion;
            obj.receive_time = DateTime.Now;
            obj.sequence_id = tx300.SequenceID.ToString();
            obj.server_port = 31875;
            obj.terminal_id = tx300.TerminalID;
            obj.terminal_type = tx300.TerminalType;
            obj.total_length = (short)tx300.TotalLength;
            obj.total_package = tx300.TotalPackage;
            DataInstance.Add(obj);
        }
        /// <summary>
        /// 保存位置信息
        /// </summary>
        /// <param name="obj"></param>
        private void SaveGpsInfo(GPSInfo obj, TB_Equipment equipment, string terminal,string type)
        {
            var PositionInstance = new PositionBLL();
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
        /// 处理0x1000命令
        /// </summary>
        /// <param name="obj"></param>
        private void Handle0x1000(TX300 obj, TB_Equipment equipment)
        {
            _0x1000 x1000 = new _0x1000();
            x1000.Content = obj.MsgContent;
            x1000.Unpackage();
            if (null != equipment)
            {
                if (x1000.GPSInfo.Available)
                {
                    EquipmentInstance.Update(f => f.id == equipment.id && f.Deleted == false, act =>
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
        /// 处理保安命令
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="equipment"></param>
        /// <param name="terminal"></param>
        private void Handle0x6007(TX300 obj, TB_Equipment equipment)
        {
            _0x6007 x6007 = new _0x6007();
            x6007.Content = obj.MsgContent;
            x6007.Unpackage();
            if (null != equipment)
            {
                EquipmentInstance.Update(f => f.id == equipment.id && f.Deleted == false, act =>
                {
                    act.LockStatus = CustomConvert.GetHex(x6007.Code);
                });
            }
        }
        /// <summary>
        /// 处理0xEE00传回来的0x6007命令
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="equipment"></param>
        /// <param name="terminal"></param>
        private void Handle0xEE00(TX300 obj, TB_Equipment equipment)
        {
            _0xEE00 xee00 = new _0xEE00();
            xee00.Content = obj.MsgContent;
            xee00.Unpackage();
            if (xee00.ErrorCommand == "0x6007")
            {
                if (null != equipment)
                {
                    EquipmentInstance.Update(f => f.id == equipment.id && f.Deleted == false, act =>
                    {
                        act.LockStatus = xee00.ErrorParamenter;
                    });
                }
            }
        }
    }
}