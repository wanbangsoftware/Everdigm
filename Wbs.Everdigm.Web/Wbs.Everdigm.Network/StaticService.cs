using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.IO;
using Wbs.Sockets;
using System.Configuration;

namespace Wbs.Everdigm.Network
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
                _server.StartUDP = true;
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
        /// <summary>
        /// 写入系统日志
        /// </summary>
        /// <param name="EventString"></param>
        public static void WriteEventLog(string EventString)
        {
            string filename = DateTime.Now.ToString("yyyyMMdd");
            FileStream fs = new FileStream(System.Web.HttpContext.Current.Server.MapPath("~/exceptions/" + filename + ".txt"), FileMode.Append);
            //创建FileSteam类,参数为路径\打开文件方式\对文件进行什么样的操作
            StreamWriter ww = new StreamWriter(fs, System.Text.Encoding.Default);
            ww.WriteLine("\r\n**************" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "**************");
            ww.Write(EventString);
            ww.WriteLine("\r\n**************End**************");
            ww.Close();
            fs.Close();
        }
    }
}