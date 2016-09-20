using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wbs.Sockets;
using System.Configuration;

namespace Wbs.Everdigm.Desktop
{
    /// <summary>
    /// 终端TCP/UDP连接处理部分
    /// </summary>
    public partial class FormMain
    {
        /// <summary>
        /// 服务
        /// </summary>
        private SocketServer _server;
        /// <summary>
        /// 启动socket服务以便接收TCP/UDP信息
        /// </summary>
        private void StartSocketServer()
        {
            if (null == _server)
            {
                var port = int.Parse(ConfigurationManager.AppSettings["SERVER_PORT"]);
                _server = new SocketServer(port);
                _server.OnMessage += new EventHandler<UIEventArgs>(OnServerMessage);
                _server.OnIridiumSend += new EventHandler<IridiumDataEvent>(OnIridiumSend);
                _server.OnTrackerChating += new EventHandler<TrackerChatEvent>(Mqtt_Publish);
                _server.StartUDP = true;
                _server.Start();
                tsmiStartService.Enabled = !_server.Started;
                tsmiStopService.Enabled = _server.Started;
                tsslServerState.Text = "Service Start at: " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            }
        }
        /// <summary>
        /// 停止socket服务
        /// </summary>
        private void StopSocketServer()
        {
            if (null != _server)
            {
                _server.Stop();
            }
        }
    }
}
