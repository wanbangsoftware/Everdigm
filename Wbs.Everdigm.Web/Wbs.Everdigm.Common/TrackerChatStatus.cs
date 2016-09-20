using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wbs.Everdigm.Common
{
    /// <summary>
    /// Tracker推送消息的发送状态
    /// </summary>
    public enum TrackerChatStatus : byte
    {
        /// <summary>
        /// 等待推送
        /// </summary>
        Waiting = 0,
        /// <summary>
        /// 发送中
        /// </summary>
        Sending = 1,
        /// <summary>
        /// 已推送成功
        /// </summary>
        Delivered = 2,
        /// <summary>
        /// 推送超时，等待再次发送
        /// </summary>
        Timeout = 3
    }
}
