using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wbs.Everdigm.Desktop
{
    /// <summary>
    /// Tracker推送消息
    /// </summary>
    public class TrackerChatEvent : EventArgs
    {
        /// <summary>
        /// 发送目的，Tracker的号码
        /// </summary>
        public string Target { get; set; }
        /// <summary>
        /// 推送的内容
        /// </summary>
        public string Content { get; set; }
    }
}
