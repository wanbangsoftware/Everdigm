﻿using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Web;

using Wbs.Sockets;
using System.Threading;
using Wbs.Utilities;

namespace Wbs.Everdigm.Network
{
    /// <summary>
    /// 网络通信服务
    /// </summary>
    public class SocketServer
    {
        /// <summary>
        /// 收到消息时的事务处理过程
        /// </summary>
        public EventHandler<string> OnMessage;
        /// <summary>
        /// IOCP网络服务
        /// </summary>
        private SocketAsyncEventServer _tcpServer;
        /// <summary>
        /// 待处理数据缓冲区(这个是线程安全的，所以不必再费心自己加读写锁)
        /// </summary>
        private static ConcurrentQueue<AsyncUserDataBuffer> _dataPool;
        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// 设定是否同时启动UDP服务
        /// </summary>
        public bool StartUDP { get; set; }

        /// <summary>
        /// 全局标记服务是否已停止
        /// </summary>
        private static bool HasServiceStoped = false;
        /// <summary>
        /// 线程间共享Lock访问发送命令部分
        /// </summary>
        private object Locker = new object();

        public SocketServer(int port)
        {
            Port = port;
        }
        /// <summary>
        /// 初始化待处理数据缓冲区
        /// </summary>
        private void InitializeDataPool()
        {
            _dataPool = new ConcurrentQueue<AsyncUserDataBuffer>();
        }
        /// <summary>
        /// 初始化服务端
        /// </summary>
        private void InitializeServer()
        {
            _tcpServer = new SocketAsyncEventServer(Port);
            _tcpServer.OnConnected += new EventHandler<AsyncUserDataEvent>(OnClientConnected);
            _tcpServer.OnDisconnected += new EventHandler<AsyncUserDataEvent>(OnClientDisconnected);
            _tcpServer.OnReceivedData += new EventHandler<AsyncUserDataEvent>(OnReceivedData);
            _tcpServer.StartUDP = StartUDP;
        }
        /// <summary>
        /// 初始化线程池
        /// </summary>
        private void InitializeThreadPool()
        {
            var count = Environment.ProcessorCount;
            for (int i = 0; i < count; i++)
            {
                ThreadPool.QueueUserWorkItem(ThreadHandlePorc);
            }
        }
        /// <summary>
        /// 初始化服务
        /// </summary>
        private void Initialize()
        {
            InitializeDataPool();
            InitializeServer();
            InitializeThreadPool();
            // 启动服务
            _tcpServer.Start();
        }
        /// <summary>
        /// 初始化并启动服务
        /// </summary>
        public void Start()
        {
            Initialize();
        }
        /// <summary>
        /// 停止服务
        /// </summary>
        public void Stop()
        {
            _tcpServer.Stop();
            HasServiceStoped = true;
        }
        /// <summary>
        /// 处理客户端连接成功的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnClientConnected(object sender, AsyncUserDataEvent e)
        {
            _dataPool.Enqueue(e.Data);
        }
        /// <summary>
        /// 处理客户端断开连接的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnClientDisconnected(object sender, AsyncUserDataEvent e)
        {
            _dataPool.Enqueue(e.Data);
        }
        /// <summary>
        /// 处理接收到客户端数据的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnReceivedData(object sender, AsyncUserDataEvent e)
        {
            _dataPool.Enqueue(e.Data);
        }
        private string DateTime(DateTime dt)
        {
            return dt.ToString("[yyyy/MM/dd HH:mm:ss] ");
        }
        /// <summary>
        /// 线程的执行过程
        /// </summary>
        private void ThreadHandlePorc(Object state)
        {
            AsyncUserDataBuffer obj;
            // 处理数据的Handler
            DataHandler _handler = new DataHandler();
            _handler.Server = _tcpServer;
            int timer = 0, sleeper = 10;

            while (true)
            {
                Thread.Sleep(sleeper);
                timer += sleeper;

                // 服务已停止时跳出循环
                if (HasServiceStoped) break;

                obj = null;
                if (!_dataPool.TryDequeue(out obj)) obj = null;

                // 没有取到数据时继续循环
                if (null != obj)
                {
                    try
                    {
                        _handler.HandleData(obj);
                        if (null != OnMessage)
                        {
                            string message = "";
                            switch (obj.DataType)
                            {
                                case AsyncUserDataType.ClientConnected:
                                    message = string.Format("{0}client {1}:{2} connected. {3}",
                                        DateTime(obj.ReceiveTime), obj.IP, obj.Port, obj.PackageType);
                                    break;
                                case AsyncUserDataType.ClientDisconnected:
                                    message = string.Format("{0}client {1}:{2} disconnected. {3}",
                                        DateTime(obj.ReceiveTime), obj.IP, obj.Port, obj.PackageType);
                                    break;
                                case AsyncUserDataType.ReceivedData:
                                    message = string.Format("{0}received data from {1}:{2}, data: {3} {4}",
                                        DateTime(obj.ReceiveTime), obj.IP, obj.Port,
                                        CustomConvert.GetHex(obj.Buffer), obj.PackageType);
                                    break;
                            }
                            if (!string.IsNullOrEmpty(message))
                            {
                                OnMessage(this, message);
                            }
                        }
                    }
                    finally
                    {
                        _tcpServer.RecycleBuffer(obj);
                    }
                }
                // 处理TCP下发送的命令
                if (timer % 1000 == 0)
                {
                    lock (Locker)
                    {
                        _handler.CheckCommand();
                    }
                    //foreach (var cmd in list)
                    //{
                    //    var ret = _tcpServer.Send(cmd.TB_Terminal.Socket.Value, Wbs.Utilities.CustomConvert.GetBytes(cmd.Content));
                    //    _handler.UpdateCommand(cmd, 0 == ret ? CommandStatus.LinkLosed : 
                    //        (1 == ret ? CommandStatus.SentByTCP : CommandStatus.SentFail));
                    //}
                }
                // 检测是否应该处理旧链接，每分钟处理一次
                if (timer % 2000 == 0)
                {
                    timer = 0;
                    lock (Locker)
                    {
                        if (_handler.CanClearOlderLinks)
                        {
                            _handler.HandleOlderClients();
                        }
                    }
                }
                // 处理命令
            }
        }
    }
}