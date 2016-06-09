using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wbs.Everdigm.Web.ajax
{
    /// <summary>
    /// Api相关类
    /// </summary>
    public partial class api
    {
        /// <summary>
        /// ajax post过来的内容
        /// </summary>
        private class Api
        {
            /// <summary>
            /// 命令字
            /// </summary>
            public string cmd { get; set; }
            /// <summary>
            /// 内容
            /// </summary>
            public string content { get; set; }
        }
        /// <summary>
        /// api 中所用到的简单的account类
        /// </summary>
        private class Account
        {
            /// <summary>
            /// 账户名
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// 已加密的账户密码
            /// </summary>
            public string md5 { get; set; }
            /// <summary>
            /// 用户的设备号码
            /// </summary>
            public string device { get; set; }
            /// <summary>
            /// 用户登录的session id
            /// </summary>
            public string session { get; set; }
            /// <summary>
            /// 其他需要一起上报的数据
            /// </summary>
            public string data { get; set; }

            public Account()
            {
                name = "";
                md5 = "";
                device = "";
                session = "";
                data = "";
            }
        }
    }
}