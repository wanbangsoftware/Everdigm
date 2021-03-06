﻿using System;

using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.BLL
{
    /// <summary>
    /// 锁车影响状态
    /// </summary>
    public enum LockEffect : byte
    {
        /// <summary>
        /// 未锁车
        /// </summary>
        NotLock = 0x00,
        /// <summary>
        /// 已锁
        /// </summary>
        Locked = 0x01,
        /// <summary>
        /// 已锁且已关机
        /// </summary>
        LockedAndEngineOff = 0x02,
        /// <summary>
        /// 已锁但设备还在工作中
        /// </summary>
        LockedAndStillWork = 0x03,
        /// <summary>
        /// 已锁但未影响设备
        /// </summary>
        LockedAndNoEffect = 0x04
    }
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
                Customer = null,
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
                LastActionTime = null,
                Latitude = 0.0,
                StoreTimes = 0,
                LockStatus = "00",
                LockEffected = 0,
                Longitude = 0.0,
                Rpm = 0,
                Model = null,
                Number = "",
                OutdoorTime = null,
                ReclaimTime = null,
                OutdoorWorktime = 0,
                OnlineStyle = null,
                OnlineTime = null,
                AddedHours = 0.0,
                CompensatedHours = 0.0,
                HourWorkEfficiency = 0.0,
                UsedHours = 0,
                WorkHours = 0.0,
                Port = 0,
                RegisterTime = DateTime.Now,
                Runtime = 0,
                AccumulativeRuntime = 0,
                InitializedRuntime = 0,
                ServerName = "",
                Signal = 0,
                Socket = 0,
                Alarm = "0000000000000000",
                Status = null,
                Terminal = null,
                Voltage = "G0000",
                Warehouse = null
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
            return string.Format("{0}, Terminal: {1}, Satellite: {2}", GetFullNumber(entity),
                (t ? "" : entity.TB_Terminal.Number), 
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
            // 售出
            if (entity.TB_EquipmentStatusName.IsItOutstorage == true)
                return "<span class=\"label label-primary\">" + entity.TB_EquipmentStatusName.Code + "</span>";
            // 租赁
            if (entity.TB_EquipmentStatusName.IsItRental == true)
                return "<span class=\"label label-info\">" + entity.TB_EquipmentStatusName.Code + "</span>";
            // 维修
            if (entity.TB_EquipmentStatusName.IsItOverhaul == true)
                return "<span class=\"label label-danger\">" + entity.TB_EquipmentStatusName.Code + "</span>";
            // 测试
            if (entity.TB_EquipmentStatusName.IsItTesting == true)
                return "<span class=\"label label-warning\">" + entity.TB_EquipmentStatusName.Code + "</span>";
            // 等待
            if (entity.TB_EquipmentStatusName.IsItWaiting == true || entity.TB_EquipmentStatusName.IsItVehicle == true)
                return "<span class=\"label label-default\">" + entity.TB_EquipmentStatusName.Code + "</span>";

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
            if (null == entity.OutdoorTime)
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
            return GetRuntime(times, entity.CompensatedHours.Value, true);
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

        public static string eng_off = "<span class=\"text-custom-gray\" title=\"Eng. Off\"><span class=\"signal cell-engine\" style=\"font-size: 130%;\"></span></span>";
        public static string eng_on = "<span class=\"text-custom-success\" title=\"Eng. On\"><span class=\"signal cell-engine\" style=\"font-size: 130%;\"></span></span>";
        public static string eng_lock = "<span class=\"text-custom-warning\" title=\"Locked\"><span class=\"glyphicon glyphicon-lock\"></span></span>";
        /// <summary>
        /// 获取发动机的启动状态(开、关、锁定)
        /// </summary>
        /// <param name="voltage"></param>
        /// <returns></returns>
        public string GetEngStatus(TB_Equipment obj)
        {
            string lk = obj.LockStatus;
            if (lk.Equals("40") || lk.Equals("0F") || lk.Equals("FF")) return eng_lock;
            var voltage = obj.Voltage;
            if (null == voltage) return eng_off;
            if (voltage.IndexOf("G2") >= 0) return eng_on;
            return eng_off;
        }
        /// <summary>
        /// 获取设备的锁车状态
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string GetLockEffectedStatus(TB_Equipment obj)
        {
            string result = null;
            LockEffect state = (LockEffect)obj.LockEffected.Value;
            switch (state) {
                case LockEffect.Locked:// 锁车
                case LockEffect.LockedAndEngineOff:// 锁车且已关机
                    result = eng_lock;//.Replace("glyphicon-lock", "glyphicon-lock faa-horizontal animated");
                    break;
                case LockEffect.LockedAndStillWork:// 锁车，但还未关机
                    result = "<span class=\"text-custom-warning\" title=\"Locked, but still working since it began lock until now\"><i class=\"fa fa-exclamation-triangle faa-shake animated\"></i></span>";
                    break;
                case LockEffect.LockedAndNoEffect:
                    result = "<span class=\"text-custom-attention\" title=\"Locked, but take no effect\"><i class=\"fa fa-exclamation-triangle faa-flash animated\"></i></span>";
                    break;
                default:
                    break;
            }
            return result;
        }
        /// <summary>
        /// 获取发动机状态的文字描述
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string GetEngineState(TB_Equipment obj)
        {
            if (obj.LockStatus == "40" || obj.LockStatus == "0F" || obj.LockStatus == "FF") return "Locked";
            var voltage = obj.Voltage;
            if (null == voltage) return "Off";
            if (voltage.IndexOf("G2") >= 0) return "On";
            return "Off";
        }
        private static string alarm_none = "0000000000000000";
        //private static string alarm_available = "<span class=\"text-custom-attention\"><i class=\"fa fa-bell\"></i></span>";
        private static string alarm_invalid = "<span class=\"text-custom-gray\" title=\"No Alarm\"><i class=\"fa fa-bell-o\"></i></span>";
        /// <summary>
        /// 获取报警状态
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string GetAlarmStatus(string alarm)
        {
            if (string.IsNullOrEmpty(alarm)) return alarm_invalid;
            string arms = Protocol.TX300.Analyse._0x2000.GetAlarm(alarm, true);
            if (alarm.Equals(alarm_none) || arms.Equals("No Alarm")) return alarm_invalid;
            return "<span class=\"text-custom-attention\" title=\"" + arms + "\"><i class=\"fa fa-bell faa-ring animated\"></i></span>";
        }
        /// <summary>
        /// 获取链接状态
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="forShort"></param>
        /// <returns></returns>
        public string GetOnlineStyle(TB_Equipment obj, bool forShort = true)
        {
            var type = obj.OnlineStyle;
            var ret = "";
            if ((byte?)null == type)
                ret = "<span class=\"text-danger\"><i class=\"fa fa-question\" title=\"Unknown\"></i></span>";

            switch (type)
            {
                case 0x00:// OFF
                    ret = "<span class=\"label label-default\" title=\"Battery Off\">" + (forShort ? "OFF" : "Battery Off") + "</span>";
                    break;
                case 0x10:// TCP
                    ret = "<span class=\"label label-info\">TCP</span>";
                    break;
                case 0x20:// UDP
                    ret = "<span class=\"label label-success\">UDP</span>";
                    break;
                case 0x30:// SMS
                    ret = "<span class=\"label label-warning\">SMS</span>";
                    break;
                case 0x40:// SLEEP
                    ret = "<span class=\"label label-warning\" title=\"Sleep\">" + (forShort ? "SLP" : "Sleep") + "</span>";
                    break;
                case 0x50:// BLIND
                    ret = "<span class=\"label label-danger\" title=\"Blind\">" + (forShort ? "BLD" : "Blind") + "</span>";
                    break;
                case 0x60:// SATELLITE
                    // 卫星链接状态下根据最后信息时间改变链接的背景颜色
                    var span = DateTime.Now.Subtract(obj.OnlineTime.Value).Duration().TotalMinutes;
                    var cls = "primary";
                    var title = "";
                    // 大于3天没有信号则改变颜色  2016/08/15 15:17
                    if (span > 60 * 24 * 3)
                    {
                        cls = "danger";
                        title = "(Activated 5 days ago)";
                    }
                    else if (span > 60 * 3)
                    {
                        // 超过3小时没有信号则警示  2016/08/15 15:17
                        cls = "warning";
                        title = "(Activated 3 hours ago)";
                    }
                    ret = "<span class=\"label label-" + cls + "\" title=\"Satellite" + title + "\">" + (forShort ? "SAT" : "Satellite") + "</span>";
                    break;
                case 0xFF:// TROUBLE
                    ret = "<span class=\"label label-danger\" title=\"Trouble\">" + (forShort ? "TRB" : "Trouble") + "</span>";
                    break;
            }
            return ret;
        }
        /// <summary>
        /// 获取运转时间
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string GetRuntime(int? time, double compensate = 0.0, bool showHour = false)
        {
            var ret = "";
            if ((int?)null == time || 0 == time)
                ret = "0";
            else
            {
                double tm = time.Value / 60.0 + compensate;
                if (tm < 10)
                    ret = string.Format("{0:0.00}", tm);
                else if (tm < 100)
                    ret = string.Format("{0:00.00}", tm);
                else
                    ret = string.Format("{0:0,00.00}", tm);
            }
            return ret;
        }
    }
}
