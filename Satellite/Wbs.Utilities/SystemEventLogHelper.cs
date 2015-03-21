using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Wbs.Utilities
{
    /// <summary>
    /// 提供写入系统日志的帮助类
    /// </summary>
    public static class SystemEventLogHelper
    {
        /// <summary>
        /// 事件源名称
        /// </summary>
        private static string eventSourceName = "ASP.NET 4.0.30319.0(test)";
        /// <summary>
        /// 要写入的系统日志名。可能的值包括应用程序、系统或自定义事件日志。
        /// </summary>
        private static string eventLogName = "Application";
        /// <summary>
        /// 默认的系统日志的事件类型
        /// </summary>
        private static EventLogEntryType eventLogType = EventLogEntryType.Information;

        /// <summary>
        /// 消息事件源名称
        /// </summary>
        public static string EventSourceName
        {
            set { eventSourceName = value; }
        }

        /// <summary>
        /// 消息事件类型
        /// </summary>
        public static EventLogEntryType EventLogType
        {
            set { eventLogType = value; }
        }

        /// <summary>
        /// 写入系统日志
        /// </summary>
        /// <param name="message">事件内容</param>
        public static void LogEvent(string message)
        {
            LogEvent(eventSourceName, message);
        }
        /// <summary>
        /// 写入系统日志
        /// </summary>
        /// <param name="message">事件消息内容</param>
        /// <param name="logType">事件类型</param>
        public static void LogEvent(string message, EventLogEntryType logType)
        {
            LogEvent(eventSourceName, message, logType);
        }
        /// <summary>
        /// 写入系统日志
        /// </summary>
        /// <param name="sourceName">事件源</param>
        /// <param name="message">日志详细内容</param>
        public static void LogEvent(string sourceName, string message)
        {
            LogEvent(sourceName, message, eventLogType);
        }
        /// <summary>
        /// 写入系统日志
        /// </summary>
        /// <param name="sourceName">事件源名称</param>
        /// <param name="message">日志详细内容</param>
        /// <param name="logType">日志事件类型</param>
        public static void LogEvent(string sourceName, string message, EventLogEntryType logType)
        {
            if (!EventLog.SourceExists(sourceName))
            {
                EventLog.CreateEventSource(sourceName, eventLogName);
            }
            EventLog.WriteEntry(sourceName, message, logType);
        }
    }
}
