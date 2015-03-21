using System;
using System.Collections.Generic;
using System.Text;

namespace Wbs.Protocol
{
    /// <summary>
    /// 用户登录类型：0，普通登录；1，web登录；2，生产测试平台登录；3，TX系列测试平台登录，4，测卡平台登录。
    /// </summary>
    public class UserLoginTypes
    {
        /// <summary>
        /// 普通登录（未知客户端登录方式）。
        /// </summary>
        public const byte LOGIN_NORMAL = 0x00;
        /// <summary>
        /// WEB 登录方式。
        /// </summary>
        public const byte LOGIN_WEB = 0x01;
        /// <summary>
        /// 生产测试平台登录方式。
        /// </summary>
        public const byte LOGIN_PRODUCE = 0x02;
        /// <summary>
        /// TX 系列测试平台登录方式。
        /// </summary>
        public const byte LOGIN_TX = 0x03;
        /// <summary>
        /// 测卡平台登录方式。
        /// </summary>
        public const byte LOGIN_CARD_TEST = 0x04;
        /// <summary>
        /// 万邦 IMS 系统登录。
        /// </summary>
        public const byte LOGIN_IMS = 0x05;
        /// <summary>
        /// 万邦新版终端测试系统登录方式。
        /// </summary>
        public const byte LOGIN_NEW_PRODUCE = 0x06;
        /// <summary>
        /// 万邦软件在线办公系统登录方式
        /// </summary>
        public const byte LOGIN_WBS_WEB = 0x10;
        /// <summary>
        /// 获取用户登录的类型。
        /// </summary>
        /// <param name="b">用户登录类型代码。</param>
        /// <returns>返回用户登录类型。</returns>
        public static string GetUserLoginType(byte b)
        {
            string ss = "";
            switch (b)
            {
                case LOGIN_NORMAL: ss = "未知方式"; break;
                case LOGIN_WEB: ss = "售后服务系统 web 平台方式"; break;
                case LOGIN_PRODUCE: ss = "生产测试平台方式"; break;
                case LOGIN_TX: ss = "TX 测试平台方式"; break;
                case LOGIN_CARD_TEST: ss = "卡/模块测试平台方式"; break;
                case LOGIN_IMS: ss = "万邦 IMS 系统方式"; break;
                case LOGIN_NEW_PRODUCE: ss = "新版万邦终端测试平台方式"; break;
                case LOGIN_WBS_WEB: ss = "万邦在线办公系统"; break;
                default: ss = "其他登录方式"; break;
            }
            return ss;
        }
    }
}
