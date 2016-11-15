using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wbs.Everdigm.Common
{
    /// <summary>
    /// Everdigm 中的一些常用的跨进程使用的方法集合
    /// </summary>
    public class EverdigmUtils
    {
        /// <summary>
        /// 获取设备的功能类别
        /// </summary>
        /// <param name="functional"></param>
        /// <returns></returns>
        public static string GetEquipmentFunctional(byte functional)
        {
            var ret = "";
            EquipmentFunctional f = (EquipmentFunctional)functional;
            switch (f)
            {
                case EquipmentFunctional.Mechanical: ret = "Mechanical"; break;
                case EquipmentFunctional.Electric: ret = "Electric"; break;
                case EquipmentFunctional.Loader: ret = "Loader"; break;
            }
            return ret;
        }

        public static string GetOnlineStyle(byte? type, DateTime? time, bool forShort = true)
        {
            var ret = "";
            if (null == type)
            {
                ret = "<span class=\"text-danger\"><i class=\"fa fa-question\" title=\"Unknown\"></i></span>";
            }
            else
            {
                switch ((LinkType)type.Value)
                {
                    case LinkType.OFF:// OFF
                        ret = "<span class=\"label label-default\" title=\"Battery Off\">" + (forShort ? "OFF" : "Battery Off") + "</span>";
                        break;
                    case LinkType.TCP:// TCP
                        ret = "<span class=\"label label-info\">TCP</span>";
                        break;
                    case LinkType.UDP:// UDP
                        ret = "<span class=\"label label-success\">UDP</span>";
                        break;
                    case LinkType.SMS:// SMS
                        ret = "<span class=\"label label-warning\">SMS</span>";
                        break;
                    case LinkType.SLEEP:// SLEEP
                        ret = "<span class=\"label label-warning\" title=\"Sleep\">" + (forShort ? "SLP" : "Sleep") + "</span>";
                        break;
                    case LinkType.BLIND:// BLIND
                        ret = "<span class=\"label label-danger\" title=\"Blind\">" + (forShort ? "BLD" : "Blind") + "</span>";
                        break;
                    case LinkType.SATELLITE:// SATELLITE
                                            // 卫星链接状态下根据最后信息时间改变链接的背景颜色
                        var n = (DateTime?)null;
                        DateTime then = n == time ? DateTime.Now.AddDays(-10) : time.Value;
                        var span = DateTime.Now.Subtract(then).Duration().TotalMinutes;
                        var cls = "primary";
                        var title = "";
                        // 大于3天没有信号则改变颜色  2016/08/15 15:17
                        if (span > 60 * 24 * 3)
                        {
                            cls = "danger";
                            title = "(Activated 3 days ago)";
                        }
                        else if (span > 60 * 3)
                        {
                            // 超过3小时没有信号则警示  2016/08/15 15:17
                            cls = "warning";
                            title = "(Activated 3 days ago)";
                        }
                        ret = "<span class=\"label label-" + cls + "\" title=\"Satellite" + title + "\">" + (forShort ? "SAT" : "Satellite") + "</span>";
                        break;
                    case LinkType.SATELLITE_STOP:
                        // 卫星暂停使用状态  2016/11/14
                        ret = "<span class=\"label label-warning\" title=\"Satellite function stop using\">" + (forShort ? "STOP" : "SAT stopped") + "</span>";
                        break;
                    case LinkType.OTHER:// TROUBLE
                        ret = "<span class=\"label label-danger\" title=\"Trouble\">" + (forShort ? "TRB" : "Trouble") + "</span>";
                        break;
                    default:
                        ret = "<span class=\"text-danger\"><i class=\"fa fa-question\" title=\"Unknown\"></i></span>";
                        break;
                }
            }
            return ret;
        }
    }
}
