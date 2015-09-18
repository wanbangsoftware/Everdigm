using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Wbs.Everdigm.Database;
using Wbs.Protocol.TX300.Analyse;

namespace Wbs.Everdigm.Web
{
    /// <summary>
    /// 自定义报警信息
    /// </summary>
    public class CustomAlarm
    {
        /// <summary>
        /// 报警时间
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// 警报的描述信息
        /// </summary>
        public string Alarm { get; set; }
        /// <summary>
        /// 实例化一个自定义报警信息
        /// </summary>
        /// <param name="obj"></param>
        public CustomAlarm(TB_Data_Alarm obj)
        {
            this.Time = (int?)null == obj.Position ? obj.AlarmTime.Value : obj.TB_Data_Position.ReceiveTime.Value;
            this.Alarm = _0x2000.GetAlarm(obj.Code);
        }
    }
}