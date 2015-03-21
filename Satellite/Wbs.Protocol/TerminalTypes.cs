using System;
using System.Collections.Generic;
using System.Text;

namespace Wbs.Protocol
{
    /// <summary>
    /// TX300 协议中包涵的终端类型。
    /// </summary>
    public class TerminalTypes
    {
        /// <summary>
        /// 斗山 DH 类中大型车载终端。
        /// </summary>
        public const byte DH = 0x0A;
        /// <summary>
        /// 斗山 DX 类中大型车载终端。
        /// </summary>
        public const byte DX = 0x14;
        /// <summary>
        /// 斗山装载机车载终端。
        /// </summary>
        public const byte LD = 0x1E;
        /// <summary>
        /// TX10G 类型终端。
        /// </summary>
        public const byte TX10G = 0x30;
        /// <summary>
        /// TX800 类型终端。
        /// </summary>
        public const byte TX800 = 0x80;
        /// <summary>
        /// 获取字符串描述的终端类型。
        /// </summary>
        /// <param name="type">二进制流表示的终端类型。</param>
        /// <returns>字符串描述的终端类型。</returns>
        public static string GetTerminalType(byte type)
        {
            string s = "";
            switch (type)
            {
                case DH: s = "DH 中大型"; break;
                case DX: s = "DX 中大型"; break;
                case LD: s = "装载机(LD)"; break;
                case TX10G: s = "TX10G"; break;
                case TX800: s = "TX800"; break;
                default: s = "N/A"; break;
            }
            return s;
        }
        /// <summary>
        /// 检测是否是 TX300 协议中约定的终端类型。
        /// </summary>
        /// <param name="b">终端类型。</param>
        /// <returns>返回是否是 TX300 通讯规约中约定的终端类型值。</returns>
        public static bool IsTX300(byte b)
        {
            bool ret = false;
            switch (b)
            {
                case DH:
                case DX:
                case LD:
                case TX10G:
                case TX800:
                    ret = true;
                    break;
                default:
                    ret = false;
                    break;
            }
            return ret;
        }
    }
}
