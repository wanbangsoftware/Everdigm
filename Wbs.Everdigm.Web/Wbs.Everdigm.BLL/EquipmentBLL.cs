using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.BLL
{
    /// <summary>
    /// 设备仓储业务处理
    /// </summary>
    public class EquipmentBLL : BaseService<TB_Equipment>
    {
        public EquipmentBLL()
            : base(new BaseRepository<TB_Equipment>())
        { }
        /// <summary>
        /// 生成一个新的默认实例
        /// </summary>
        /// <returns></returns>
        public override TB_Equipment GetObject()
        {
            return new TB_Equipment()
            {
                Customer = (int?)null,
                Deleted = false,
                GpsAddress = "",
                // 加入卫星功能确认 2015/09/18 14:00
                SatelliteStatus = false,
                GpsUpdated = true,
                Functional = 10,
                id = 0,
                IP = "",
                LastAction = "",
                LastActionBy = "",
                LastActionTime = (DateTime?)null,
                Latitude = 0.0,
                StoreTimes = 0,
                LockStatus = "00",
                Longitude = 0.0,
                Rpm = 0,
                Model = (int?)null,
                Number = "",
                OutdoorTime = (DateTime?)null,
                ReclaimTime = (DateTime?)null,
                OutdoorWorktime = 0,
                OnlineStyle = (byte?)null,
                OnlineTime = (DateTime?)null,
                Port = 0,
                RegisterTime = DateTime.Now,
                Runtime = 0,
                InitializedRuntime = 0,
                ServerName = "",
                Signal = 0,
                Socket = 0,
                Alarm = "0000000000000000",
                Status = (int?)null,
                Terminal = (int?)null,
                Voltage = "G0000",
                Warehouse = (int?)null
            };
        }
        /// <summary>
        /// 将实体生成字符串(如：设备号，终端号，Sim号，Sat号)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string ToString(TB_Equipment entity)
        {
            // 判断终端是否为空
            var n = (int?)null;
            var t = n == entity.Terminal;
            return string.Format("{0}, Sim: {1}, Satellite: {2}", (entity.TB_EquipmentModel.Code + entity.Number),
                (t ? "" : entity.TB_Terminal.Sim), 
                (t ? "" : (n == entity.TB_Terminal.Satellite ? "" : entity.TB_Terminal.TB_Satellite.CardNo)));
        }
        /// <summary>
        /// 生成设备的完整号码
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string GetFullNumber(TB_Equipment entity)
        {
            if (null == entity) return "";
            return entity.TB_EquipmentModel.Code + entity.Number;
        }
        /// <summary>
        /// 获取设备的状态信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string GetStatus(TB_Equipment entity)
        {
            if (entity.TB_EquipmentStatusName.IsItOutstorage == true || entity.TB_EquipmentStatusName.IsItRental == true)
            {
                return "<span class=\"label label-warning\">" + entity.TB_EquipmentStatusName.Code + "</span>";
            }
            else if (entity.TB_EquipmentStatusName.IsItOverhaul == true)
                return "<span class=\"label label-danger\">" + entity.TB_EquipmentStatusName.Code + "</span>";

            return "<span class=\"label label-success\">" + entity.TB_EquipmentStatusName.Code + "</span>";
        }
        /// <summary>
        /// 获取设备的状态信息描述
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string GetStatusTitle(TB_Equipment entity)
        {
            return entity.TB_EquipmentStatusName.Name;
        }
        /// <summary>
        /// 计算设备出厂之时与今日的天数
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int GetOutdoorDays(TB_Equipment entity)
        {
            if ((DateTime?)null == entity.OutdoorTime)
                return 0;

            var ts1 = new TimeSpan(entity.OutdoorTime.Value.Ticks);
            var ts2 = new TimeSpan(DateTime.Now.Ticks);
            return (int)ts1.Subtract(ts2).Duration().TotalDays;
        }
        /// <summary>
        /// 获取设备出厂之后的每日运转时间
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string GetAverageWorktime(TB_Equipment entity)
        {
            var days = GetOutdoorDays(entity);
            if (days < 1) return "-";
            var newTime = (int?)null == entity.Runtime ? 0 : entity.Runtime;
            var oldTime = (int?)null == entity.OutdoorWorktime ? 0 : entity.OutdoorWorktime;
            // 加上初始化的运转时间
            int times = (int)((newTime - oldTime + entity.InitializedRuntime) / days);
            return GetRuntime((int?)times, true);
        }

        /// <summary>
        /// 获取终端信息的title
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string GetTerinalTitleInfo(TB_Equipment obj)
        {
            var n = (int?)null;
            return n == obj.Terminal ? "" : ("Terminal: " + obj.TB_Terminal.Number);
        }
        /// <summary>
        /// 获取终端的卫星模块号码作为title
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string GetSatelliteTitleInfo(TB_Equipment obj)
        {
            var n = (int?)null;
            bool no = (n == obj.Terminal || n == obj.TB_Terminal.Satellite);
            return no ? "no satellite model" : obj.TB_Terminal.TB_Satellite.CardNo;
        }
        /// <summary>
        /// 获取信号强度标识
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string GetSignal(TB_Equipment obj)
        {
            //string signal=obj.Signal
            return "<span class=\"glyphicon glyphicon-signal label label-primary\" aria-hidden=\"true\"></span>";
        }

        /// <summary>
        /// 获取发动机的启动状态
        /// </summary>
        /// <param name="voltage"></param>
        /// <returns></returns>
        public string GetEngStatus(TB_Equipment obj)
        {
            var voltage = obj.Voltage;
            if (null == voltage) return "OFF";
            if (voltage.IndexOf("G2") >= 0) return "ON";
            return "OFF";
        }
        /// <summary>
        /// 获取运转时间
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string GetRuntime(int? time, bool showHour = false)
        {
            var ret = "";
            if ((int?)null == time || 0 == time)
                ret = "0:00";
            else
            {
                //if (time.Value < 60) return "00:"+time.Value.ToString() + "min";
                int hour = time.Value / 60, minute = time.Value % 60;
                if (hour < 1000)
                    ret = string.Format("{0:00}:{1:00}", hour, minute);
                ret = string.Format("{0:0,0}:{1:00}", hour, minute);
            }
            return showHour ? (ret.Replace(":", " hr ") + " min") : ret;
        }
    }
}
