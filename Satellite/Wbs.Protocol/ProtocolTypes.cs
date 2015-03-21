using System;
using System.Collections.Generic;
using System.Text;

namespace Wbs.Protocol
{
    /// <summary>
    /// TX300 协议中通讯包协议类型。
    /// </summary>
    public class ProtocolTypes
    {
        /// <summary>
        /// TCP 协议数据包。
        /// </summary>
        public const byte TCP = 0x00;
        /// <summary>
        /// 处于盲区中产生的 TCP 数据包。
        /// </summary>
        public const byte TCP_BLIND = 0x0F;
        /// <summary>
        /// UDP 协议数据包。
        /// </summary>
        public const byte UDP = 0x10;
        /// <summary>
        /// 处于盲区中产生的 UDP 数据包。
        /// </summary>
        public const byte UDP_BLIND = 0x1F;
        /// <summary>
        /// SMS 协议数据包。
        /// </summary>
        public const byte SMS = 0x20;
        /// <summary>
        /// 处于盲区中产生的 SMS 数据包。
        /// </summary>
        public const byte SMS_BLIND = 0x2F;
        /// <summary>
        /// 连接状态为 OFF。
        /// </summary>
        public const byte LINK_OFF = 0x00;
        /// <summary>
        /// 连接状态为 TCP。
        /// </summary>
        public const byte LINK_TCP = 0x10;
        /// <summary>
        /// 连接状态为 UDP。
        /// </summary>
        public const byte LINK_UDP = 0x20;
        /// <summary>
        /// 连接状态为 SMS。
        /// </summary>
        public const byte LINK_SMS = 0x30;
        /// <summary>
        /// 睡眠状态。
        /// </summary>
        public const byte LINK_SLEEP = 0x40;
        /// <summary>
        /// 盲区状态。
        /// </summary>
        public const byte LINK_BLIND = 0x50;
        /// <summary>
        /// 故障待定状态。
        /// </summary>
        public const byte LINK_TROUBLE = 0xFF;
        /// <summary>
        /// 获取字符串形式的协议类型表示方式。
        /// </summary>
        /// <param name="type">二进制流表示的协议方式。</param>
        /// <returns>字符串形式的协议类型表示方式。</returns>
        public static string GetProtocolType(byte type)
        {
            string s = "";
            switch (type)
            {
                case TCP: s = "TCP"; break;
                case TCP_BLIND: s = "TCP 盲区补偿数据"; break;
                case UDP: s = "UDP"; break;
                case UDP_BLIND: s = "UDP 盲区补偿数据"; break;
                case SMS: s = "SMS"; break;
                case SMS_BLIND: s = "SMS 盲区补偿数据"; break;
                default: s = "N/A"; break;
            }
            return s;
        }
        /// <summary>
        /// 获取 byte 形式的协议类型。
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static byte GetProtocolType(string type)
        {
            byte b = byte.MaxValue;
            switch (type.ToUpper())
            {
                case "TCP": b = TCP; break;
                case "TCP_BLIND": b = TCP_BLIND; break;
                case "UDP": b = UDP; break;
                case "UDP_BLIND": b = UDP_BLIND; break;
                case "SMS": b = SMS; break;
                case "SMS_BLIND": b = SMS_BLIND; break;
            }
            return b;
        }
        /// <summary>
        /// 判断是否是 TX300 协议中约定的通讯协议类型。
        /// </summary>
        /// <param name="b">通讯协议类型值。</param>
        /// <returns>返回是否是 TX300 协议中约定的通讯协议类型。</returns>
        public static bool IsTX300(byte b)
        {
            bool ret = false;
            switch (b)
            {
                case TCP:
                case TCP_BLIND:
                case UDP:
                case UDP_BLIND:
                case SMS:
                case SMS_BLIND:
                    ret = true;
                    break;
                default:
                    ret = false;
                    break;
            }
            return ret;
        }
        /// <summary>
        /// 获取终端当前的连接状态。
        /// </summary>
        /// <param name="b">状态码。</param>
        /// <returns>连接状态描述。</returns>
        public static string GetLinks(byte b)
        {
            string ret = "";
            switch (b)
            {
                case LINK_OFF: ret = "OFF"; break;
                case LINK_TCP: ret = "TCP"; break;
                case LINK_UDP: ret = "UDP"; break;
                case LINK_SMS: ret = "SMS"; break;
                case LINK_SLEEP: ret = "睡眠"; break;
                case LINK_BLIND: ret = "盲区"; break;
                case LINK_TROUBLE: ret = "故障待定"; break;
                default:
                    ret = "未知连接状态";
                    break;
            }
            return ret;
        }
    }
}
