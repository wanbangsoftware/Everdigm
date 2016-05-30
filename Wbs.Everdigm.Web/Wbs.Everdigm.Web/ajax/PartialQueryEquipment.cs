using System;
using System.Collections.Generic;
using System.Linq;

using Wbs.Protocol.TX300;
using Wbs.Protocol.TX300.Analyse;
using Wbs.Everdigm.Database;
using Wbs.Everdigm.Common;
using Wbs.Protocol;
using Wbs.Utilities;

namespace Wbs.Everdigm.Web.ajax
{
    public class TempEquipment
    {
        public string Number { get; set; }
        public int Id { get; set; }
        public string Terminal { get; set; }
        public string Sim { get; set; }
        public string Satellite { get; set; }
        public string Functional { get; set; }
        public string Worktime { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Online { get; set; }
        public string Lock { get; set; }
        public long Acttime { get; set; }
        public string Voltage { get; set; }
        public string Alarm { get; set; }

        public TempEquipment(TB_Equipment obj)
        {
            Number = obj.TB_EquipmentModel.Code + obj.Number;
            Id = obj.id;
            var n = (int?)null;
            Terminal = n == obj.Terminal ? "" : obj.TB_Terminal.Number;
            Sim = n == obj.Terminal ? "" : obj.TB_Terminal.Sim;
            Satellite = n == obj.Terminal ? "" : (n == obj.TB_Terminal.Satellite ? "" : obj.TB_Terminal.TB_Satellite.CardNo);
            Functional = Utility.GetEquipmentFunctional(obj.Functional.Value);
            Worktime = Wbs.Everdigm.BLL.EquipmentBLL.GetRuntime(obj.Runtime + obj.InitializedRuntime, true);
            Latitude = obj.Latitude.Value;
            Longitude = obj.Longitude.Value;
            Online = Utility.GetOnlineStyle(obj.OnlineStyle, false);
            Lock = obj.LockStatus;
            Acttime = (DateTime?)null == obj.LastActionTime ? 0 : Utilities.CustomConvert.DateTimeToJavascriptDate(obj.LastActionTime.Value);
            Voltage = obj.Voltage;
            Alarm = obj.Alarm;
        }
    }
    /// <summary>
    /// 这里提供Equipment相关的查询方法集合
    /// </summary>
    public partial class query
    {
        /// <summary>
        /// 处理设备相关的查询
        /// </summary>
        private void HandleEquipmentQuery()
        {
            var ret = "[]";
            switch (cmd)
            {
                case "fullnumber":
                    // 查询完整的设备号码（如DX300LC-20000）
                    var numbers = EquipmentInstance.FindList(f => (f.TB_EquipmentModel.Code + f.Number).Equals(data) && f.Deleted == false).ToList();
                    ret = JsonConverter.ToJson(numbers);
                    break;
                case "query":
                    var query = JsonConverter.ToObject<TB_Equipment>(data);
                    var queryList = EquipmentInstance.FindList(f =>
                        (query.Model > 0 ? f.Model == query.Model : f.Model > 0) &&
                        f.Number.IndexOf(query.Number) >= 0 && f.Deleted == false).ToList();
                    ret = JsonConverter.ToJson(queryList);
                    break;
                case "number":
                    // 通过号码模糊查询
                    var ns = EquipmentInstance.FindList(f => f.Number.IndexOf(data) >= 0 && f.Deleted == false).ToList();
                    var tmp=new List<TempEquipment>();
                    foreach (var t in ns) {
                        tmp.Add(new TempEquipment(t));
                    }
                    ret = JsonConverter.ToJson(tmp);
                    break;
                case "notbind":
                    var obj = JsonConverter.ToObject<TB_Equipment>(data);
                    var list = EquipmentInstance.FindList(f =>
                        (obj.Model > 0 ? f.Model == obj.Model : f.Model > 0) &&
                        f.Number.IndexOf(obj.Number) >= 0 && f.Terminal == (int?)null && f.Deleted == false).ToList();
                    break;
                case "old-in-store":
                    // 2手或租赁设备入库查询
                    var queryObj = JsonConverter.ToObject<TB_Equipment>(data);
                    if (queryObj.Number.IndexOf('-') >= 0)
                        queryObj.Number = queryObj.Number.Substring(queryObj.Number.LastIndexOf('-') + 1);
                    var olds = EquipmentInstance.FindList(f => (queryObj.Model > 0 ? f.Model == queryObj.Model : f.Model > 0) &&
                        f.Number.IndexOf(queryObj.Number) >= 0 && f.Deleted == false).OrderBy(o => o.Number).Take(5).ToList();
                    ret = JsonConverter.ToJson(olds);
                    break;
                case "storage":
                    // 查询出入库记录
                    ret = HandleEquipmentStorage();
                    break;
                case "6004":
                    ret = HandleEquipment6004();
                    break;
                case "positions":
                    ret = HandleEquipmentPositionHistory();
                    break;
                case "alarms":
                    ret = HandleEquipmentAlarmHistory();
                    break;
                case "faults":
                    ret = HandleEquipmentEposFaultHistory();
                    break;
                case "worktime":
                    ret = HandleQueryEquipmentWorktime();
                    break;
                case "province":
                    ret = HandleEquipmentProvinceQuest();
                    break;
                case "worktime2excel":
                    // 工作时间导入到excel请求
                    ret = HandleQueryEquipmentWorkTime2Excel();
                    break;
                case "worktime2excelquery":
                    ret = HandleQueryWorkTime2ExcelStatus();
                    break;
                default:
                    ret = "{\"status\":-1,\"desc\":\"No function to handle your request.\"}";
                    break;
            }
            ResponseJson(ret);
        }
        /// <summary>
        /// 处理查询设备EPOS报警历史记录的请求
        /// </summary>
        /// <returns></returns>
        private string HandleEquipmentEposFaultHistory()
        {
            var ret = "[]";
            try
            {
                var id = ParseInt(Utility.Decrypt(data));
                var start = DateTime.Parse(GetParamenter("start") + " 00:00:00");
                var end = DateTime.Parse(GetParamenter("end") + " 23:59:59");
                var eposList = EposInstance.FindList<TB_Data_EposFault>(f => f.Equipment == id &&
                    f.ReceiveTime >= start && f.ReceiveTime <= end, "ReceiveTime", true);
                ret = JsonConverter.ToJson(eposList);
            }
            catch
            { }
            return ret;
        }
        /// <summary>
        /// 处理查询设备报警历史记录的请求
        /// </summary>
        /// <returns></returns>
        private string HandleEquipmentAlarmHistory()
        {
            var ret = "[]";
            try
            {
                var id = ParseInt(Utility.Decrypt(data));
                var start = DateTime.Parse(GetParamenter("start") + " 00:00:00");
                var end = DateTime.Parse(GetParamenter("end") + " 23:59:59");
                // 更改为直接查询报警时间  2015/09/18 08:30
                var armList = AlarmInstance.FindList<TB_Data_Alarm>(f => f.Equipment == id && f.AlarmTime >= start && f.AlarmTime <= end, "id", true);
                var list = new List<CustomAlarm>();
                foreach (var arm in armList)
                {
                    // 去掉 Sim 卡丢失报警  2016.04.26 10:26
                    if (arm.Code[10] != '1')
                    {
                        list.Add(new CustomAlarm(arm));
                    }
                }
                ret = JsonConverter.ToJson(list);
            }
            catch
            { }
            return ret;
        }
        /// <summary>
        /// 查询设备的定位历史记录
        /// </summary>
        /// <returns></returns>
        private string HandleEquipmentPositionHistory()
        {
            var ret = "[]";
            try
            {
                var id = ParseInt(Utility.Decrypt(data));
                var start = DateTime.Parse(GetParamenter("start") + " 00:00:00");
                var end = DateTime.Parse(GetParamenter("end") + " 23:59:59");
                var posList = PositionInstance.FindList<TB_Data_Position>(f => f.Equipment == id &&
                    f.ReceiveTime >= start && f.ReceiveTime <= end, "ReceiveTime", true);
                ret = JsonConverter.ToJson(posList);
            }
            catch
            { }
            return ret;
        }
        /// <summary>
        /// 查询设备的运转时间
        /// </summary>
        /// <returns></returns>
        private string HandleEquipment6004()
        {
            var ret = "{}";
            var id = ParseInt(Utility.Decrypt(data));
            var obj = EquipmentInstance.Find(f => f.id == id && f.Deleted == false);
            if (null != obj)
            {
                var date = DateTime.Parse(GetParamenter("date") + " 23:59:59");
                var runtimes = DataInstance.FindList(f => f.mac_id.Equals(EquipmentInstance.GetFullNumber(obj)) &&
                    f.command_id.Equals("0x6004") && f.receive_time < date).OrderByDescending(o => o.receive_time).FirstOrDefault();
                if (null != runtimes)
                {
                    _0x6004DX x6004 = new _0x6004DX();
                    x6004.Command = "0x6004";
                    x6004.HexContent = runtimes.message_content;
                    x6004.DataType = doosan.DX;
                    x6004.MacID = obj.TB_EquipmentModel.Code + obj.Number;
                    x6004.ReceiveTime = runtimes.receive_time.Value;
                    x6004.SimNo = runtimes.terminal_id;
                    x6004.Unpackage();
                    ret = JsonConverter.ToJson(x6004);
                }
            }
            return ret;
        }
        /// <summary>
        /// 处理查询设备出入库记录的请求
        /// </summary>
        /// <returns></returns>
        private string HandleEquipmentStorage()
        {
            var ret = "[]";
            try
            {
                var id = ParseInt(Utility.Decrypt(data));
                var start = DateTime.Parse(GetParamenter("start") + " 00:00:00");
                var end = DateTime.Parse(GetParamenter("end") + " 23:59:59");
                var list = StorageInstance.FindList(f => f.Equipment == id &&
                    f.Stocktime >= start && f.Stocktime <= end).OrderBy(o => o.Savetime).ToList();
                ret = JsonConverter.ToJson(list);
            }
            catch
            { }
            return ret;
        }
        /// <summary>
        /// 每天的分钟数
        /// </summary>
        private static uint DAY_MINUTES = 24 * 60;
        /// <summary>
        /// 获取时间在当日的分钟计数
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private uint GetPassedMinute(DateTime time)
        {
            return (uint)(time.Hour * 60 + time.Minute);
        }
        /// <summary>
        /// 查询指定设备在指定日期范围内每日运转时间
        /// </summary>
        /// <returns></returns>
        private string HandleEquipmentWorktime(bool averagable = true)
        {
            var ret = "{}";
            var id = ParseInt(Utility.Decrypt(data));
            var obj = EquipmentInstance.Find(f => f.id == id && f.Deleted == false);
            if (null != obj)
            {
                var date = DateTime.Parse(GetParamenter("date") + " 00:00:00");
                // 如果不是求平均值则将日期往前推一天
                //if (!averagable) { date = date.AddDays(-1); }
                var date1 = DateTime.Parse(GetParamenter("date1") + " 23:59:59");
                List<WorktimeChart> avg = new List<WorktimeChart>();
                List<WorktimeChart> work = new List<WorktimeChart>();
                DateTime dt = date;
                // 循环每天一个节点
                while (dt.Ticks < date1.Ticks)
                {
                    avg.Add(new WorktimeChart() { date = dt.ToString("yyyy/MM/dd"), x = Utility.DateTimeToJavascriptDate(dt.Date), y = 0, min = 0 });
                    work.Add(new WorktimeChart() { date = dt.ToString("yyyy/MM/dd"), x = Utility.DateTimeToJavascriptDate(dt.Date), y = 0, min = 0 });
                    dt = dt.AddDays(1);
                }
                var macid = EquipmentInstance.GetFullNumber(obj);
                var cmds = new string[] { "0x1000", "0x1001", "0x5000", "0x6004", "0x600B", "0xCC00" };
                var runtimes = DataInstance.FindList(f => f.mac_id.Equals(macid) && cmds.Contains(f.command_id) &&
                    f.receive_time >= date.AddDays(-1) && f.receive_time <= date1.AddDays(1)).OrderBy(o => o.receive_time);
                var list = new List<WorkTime>();
                if (null != runtimes)
                {
                    long today = 0;
                    foreach (var r in runtimes)
                    {
                        var t = Utility.DateTimeToJavascriptDate(r.receive_time.Value.Date);
                        // 日期不同则重置日期和运转时间
                        if (today != t) { today = t; }

                        byte[] temp = null;
                        int index = 0;
                        if (r.command_id.Equals("0x1000"))
                        {
                            if (r.protocol_type == ProtocolTypes.SATELLITE)
                            {
                                temp = CustomConvert.GetBytes(r.message_content);
                                index = 13;
                            }
                        }
                        else if (r.command_id.Equals("0x1001"))
                        {
                            temp = CustomConvert.GetBytes(r.message_content);
                            index = 37;
                        }
                        else if (r.command_id.Equals("0x5000"))
                        {
                            // 只有装载机和电装的挖掘机才能有5000命令的总运转时间
                            if (r.terminal_type >= TerminalTypes.DXE)
                            {
                                temp = CustomConvert.GetBytes(r.message_content);
                                index = 0;
                            }
                        }
                        else if (r.command_id.Equals("0x600B"))
                        {
                            temp = CustomConvert.GetBytes(r.message_content);
                            index = 0;
                        }
                        else if (r.command_id.Equals("0xCC00"))
                        {
                            temp = CustomConvert.GetBytes(r.message_content);
                            index = 12;
                        }
                        else
                        {
                            temp = CustomConvert.GetBytes(r.message_content);

                            byte tp = r.terminal_type.Value;
                            index = tp == TerminalTypes.DH ? 2 : (tp == TerminalTypes.DX ? 5 : 1);
                        }

                        // 增加一个节点
                        WorkTime wt = new WorkTime();
                        wt.date = r.receive_time.Value.ToString("yyyy/MM/dd HH:mm:ss");
                        wt.time = null == temp ? 0 : BitConverter.ToUInt32(temp, index);
                        // 如果list已经有数据则计算与上一条数据之间的差值
                        var cnt = list.Count;
                        if (cnt > 0)
                        {
                            if (wt.time < list[cnt - 1].time)
                            {
                                // 当前运转时间小于前一条时，小计为0
                                wt.subtotal = 0;
                            }
                            else
                            {
                                // 否则小计为差值
                                wt.subtotal = wt.time - list[cnt - 1].time;
                                // 每日凌晨1点之前，如果计算的时间差超过了当前时间的分钟数，则只计算分钟数
                                if (r.receive_time.Value.Hour < 1 && wt.subtotal > r.receive_time.Value.Minute)
                                {
                                    // 每日1时之前计算的时间差大于已经过去了的分钟数则将差值算到前一天最后一条数据里
                                    if (cnt > 0)
                                    {
                                        list[cnt - 1].subtotal += (uint)(wt.subtotal - r.receive_time.Value.Minute);
                                    }
                                    wt.subtotal = (uint)r.receive_time.Value.Minute;
                                }
                                // 小于0时算作0
                                if (wt.subtotal < 0) wt.subtotal = 0;
                                // 如果与上一条日期的分钟数相差12个小时以上则记为0
                                DateTime lst = DateTime.Parse(list[cnt - 1].date);
                                if ((lst - r.receive_time.Value).Duration().TotalMinutes >= DAY_MINUTES / 2) wt.subtotal = 0;
                                // 大于24小时算作0
                                if (wt.subtotal >= DAY_MINUTES) wt.subtotal = 0;
                            }
                        }
                        else
                        {
                            // 第一条数据小计为0
                            wt.subtotal = 0;
                        }
                        list.Add(wt);

                        // 更新本日最后的运转时间
                        var wk = work.FirstOrDefault(f => f.x == today);
                        if (null != wk)
                        {
                            //if (wk.min == 0) { wk.min = run; }
                            wk.min = wt.time;
                        }
                    }// end of foreach

                    // 计算每日运转时间
                    foreach (var f in work)
                    {
                        f.y = Math.Round(list.Where(w => w.date.Contains(f.date)).Sum(s => s.subtotal) / 60.0, 2);
                    }

                    // 计算平均值
                    var avgg = Math.Round(work.Sum(s => s.y) * 1.0 / work.Count, 2);
                    foreach (var a in avg)
                    { a.y = avgg; }
                }
                if (averagable)
                    ret = string.Format("{0}\"Average\":{1},\"Worktime\":{2}{3}", "{", JsonConverter.ToJson(avg), JsonConverter.ToJson(work), "}");
                else
                    ret = JsonConverter.ToJson(work);
            }
            return ret;
        }
        private double FormatTime(uint time)
        {
            //uint hour = time / 60;
            //uint minute = time % 60;
            double hour = (time * 1.0 / 60.0);
            return double.Parse(hour.ToString("0.00"));
        }
        /// <summary>
        /// 查找定位地址属于某个省份的设备列表
        /// </summary>
        /// <returns></returns>
        private string HandleEquipmentProvinceQuest() {
            var ret = "[]";
            var list = EquipmentInstance.FindList(f => f.GpsAddress.IndexOf(data) >= 0 && f.Deleted == false);
            var objs = new List<TempEquipment>();
            foreach (var obj in list) {
                objs.Add(new TempEquipment(obj));
            }
            ret = JsonConverter.ToJson(objs);
            return ret;
        }

        private class WorkTime
        {
            public string date;
            public uint time;
            public uint subtotal;
        }
    }
}