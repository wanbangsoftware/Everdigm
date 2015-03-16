using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Wbs.Protocol.TX300;
using Wbs.Protocol.TX300.Analyse;
using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.Web.ajax
{
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
                    var numbers = EquipmentInstance.FindList(f => (f.TB_EquipmentModel.Code + f.Number).Equals(data)).ToList();
                    ret = JsonConverter.ToJson(numbers);
                    break;
                case "query":
                    var query = JsonConverter.ToObject<TB_Equipment>(data);
                    var queryList = EquipmentInstance.FindList(f =>
                        (query.Model > 0 ? f.Model == query.Model : f.Model > 0) &&
                        f.Number.IndexOf(query.Number) >= 0).ToList();
                    ret = JsonConverter.ToJson(queryList);
                    break;
                case "old-in-store":
                    // 2手或租赁设备入库查询
                    var queryObj = JsonConverter.ToObject<TB_Equipment>(data);
                    if (queryObj.Number.IndexOf('-') >= 0)
                        queryObj.Number = queryObj.Number.Substring(queryObj.Number.LastIndexOf('-') + 1);
                    var olds = EquipmentInstance.FindList(f => f.Model == queryObj.Model && 
                        f.Number.IndexOf(queryObj.Number) >= 0).Take(5).OrderBy(o => o.Number).ToList();
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
                var eposList = EposInstance.FindList<TB_EposFault>(f => f.Equipment == id &&
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
                var armList = AlarmInstance.FindList<TB_Alarm>(f => f.Equipment == id &&
                    f.TB_Position.ReceiveTime >= start && f.TB_Position.ReceiveTime <= end, null);
                var list = new List<CustomAlarm>();
                foreach (var arm in armList)
                {
                    list.Add(new CustomAlarm(arm));
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
                var posList = PositionInstance.FindList<TB_Position>(f => f.Equipment == id &&
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
            var obj = EquipmentInstance.Find(f => f.id == id);
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
                    f.Stocktime >= start && f.Stocktime <= end).OrderBy(o => o.Stocktime).ToList();
                ret = JsonConverter.ToJson(list);
            }
            catch
            { }
            return ret;
        }
    }
}