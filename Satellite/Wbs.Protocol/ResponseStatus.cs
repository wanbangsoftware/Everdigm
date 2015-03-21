using System;
using System.Collections.Generic;
using System.Text;

namespace Wbs.Protocol
{
    /// <summary>
    /// 服务器反馈给终端的状态码。
    /// </summary>
    public class ResponseStatus
    {
        /// <summary>
        /// 数据正常。
        /// </summary>
        public const byte ERROR_NONE = 0x00;
        /// <summary>
        /// 数据总长度与服务器收到的数据长度不一致。
        /// </summary>
        public const byte ERROR_TOTAL_LENGTH = 0x01;
        /// <summary>
        /// 数据包中的数据类型与当前通讯方式不一致。
        /// </summary>
        public const byte ERROR_PROTOCOL_TYPE = 0x02;
        /// <summary>
        /// 数据包中的终端类型不是约定的代码。
        /// </summary>
        public const byte ERROR_TERMINAL_TYPE = 0x03;
        /// <summary>
        /// 未知的命令代码。
        /// </summary>
        public const byte ERROR_COMMAND_ID = 0x04;
        /// <summary>
        /// TX 协议版本不正确。
        /// </summary>
        public const byte ERROR_PROTOCOL_VERSION = 0x05;
        /// <summary>
        /// 终端号码不正确。
        /// </summary>
        public const byte ERROR_TERMINAL_ID = 0x06;
        /// <summary>
        /// 数据帧序号不正确。
        /// </summary>
        public const byte ERROR_PACKAGE_ID = 0x07;
        /// <summary>
        /// 校验和错误。
        /// </summary>
        public const byte ERROR_CHECK_SUM = 0x08;
        /// <summary>
        /// 其他未知的错误类型。
        /// </summary>
        public const byte ERROR_UNKNOW = 0xFF;
        /// <summary>
        /// 获取状态码集合。
        /// </summary>
        /// <returns>返回各种状态码。</returns>
        public static string[] GetResponseStatus()
        {
            string[] s = new string[] { "数据正常，无错误", "包长度与收到的数据长度不一致", "数据类型与当前通讯方式不一致", "终端类型错误", "命令码错误", "协议版本错误", 
            "终端号码错误", "数据帧序号错误", "校验和错误"};
            return s;
        }
        /// <summary>
        /// 获取状态码内容。
        /// </summary>
        /// <param name="b">状态码。</param>
        /// <returns>状态码内容。</returns>
        public static string GetResponseStatus(byte b)
        {
            string s = "";
            if ((b == ERROR_UNKNOW) || (b > 0x08))
                s = "未知错误码";
            else
                s = GetResponseStatus()[b];
            return s;
        }
    }
}
