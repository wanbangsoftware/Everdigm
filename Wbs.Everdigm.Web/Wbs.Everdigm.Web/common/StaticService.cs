﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Wbs.Sockets;
using System.Configuration;

namespace Wbs.Everdigm.Web
{
    /// <summary>
    /// 全局静态网络服务类
    /// </summary>
    public static class StaticService
    {
        private static SocketServer _server;

        /// <summary>
        /// 初始化并启动服务
        /// </summary>
        public static void StartService()
        {
            var port = int.Parse(ConfigurationManager.AppSettings["SERVER_PORT"]);
            if (null == _server)
            {
                _server = new SocketServer(port);
                _server.Start();
            }
        }
        /// <summary>
        /// 关闭服务
        /// </summary>
        public static void StopService()
        {
            if (null != _server)
            {
                _server.Stop();
            }
        }
    }
}