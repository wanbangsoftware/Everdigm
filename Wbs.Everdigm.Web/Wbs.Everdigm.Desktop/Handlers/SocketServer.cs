using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Web;

using Wbs.Sockets;
using System.Threading;
using Wbs.Utilities;

namespace Wbs.Everdigm.Desktop
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

        private bool started = false;

        /// <summary>
        /// 标记服务是否已启动
        /// </summary>
        public bool Started { get { return started; } }

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
            try
            {
                // 启动服务
                _tcpServer.Start();
                started = true;
            }
            catch
            { }
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
            started = false;
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
            return dt.ToString("[yyyy/MM/dd HH:mm:ss.fff] ");
        }
        /// <summary>
        /// 数据处理时出错的信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="message"></param>
        private void DataHandler_OnUnhandledMessage(object sender, string message)
        {
            HandleDisplayMessage(message);
        }
        /// <summary>
        /// 向主界面提交显示信息申请
        /// </summary>
        /// <param name="message"></param>
        private void HandleDisplayMessage(string message)
        {
            if (null != OnMessage)
            {
                OnMessage(this, message);
            }
        }
        /// <summary>
        /// 线程的执行过程
        /// </summary>
        private void ThreadHandlePorc(Object state)
        {
            AsyncUserDataBuffer obj;
            // 处理数据的Handler
            DataHandler _handler = new DataHandler();
            _handler.OnUnhandledMessage += new EventHandler<string>(DataHandler_OnUnhandledMessage);
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
                        string message = "";
                        switch (obj.DataType)
                        {
                            case AsyncUserDataType.ClientConnected:
                                message = string.Format("{0}client {1}:{2} connected. [{3}]",
                                    DateTime(obj.ReceiveTime), obj.IP, obj.Port, obj.PackageType);
                                break;
                            case AsyncUserDataType.ClientDisconnected:
                                message = string.Format("{0}client {1}:{2} disconnected. [{3}]",
                                    DateTime(obj.ReceiveTime), obj.IP, obj.Port, obj.PackageType);
                                break;
                            case AsyncUserDataType.ReceivedData:
                                message = string.Format("{0}received data: {1} [{2}]", DateTime(obj.ReceiveTime), 
                                    CustomConvert.GetHex(obj.Buffer), obj.PackageType);
                                break;
                        }

                        // 处理数据
                        _handler.HandleData(obj);

                        if (!string.IsNullOrEmpty(message))
                        {
                            HandleDisplayMessage(message);
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
                        try
                        {
                            _handler.CheckCommand();
                        }
                        catch(Exception e)
                        {
                            HandleDisplayMessage("Cannot handle CheckCommand: " + e.Message);
                        }
                    }
                }
                // 检测是否应该处理旧链接，每分钟处理一次
                if (timer % 2000 == 0)
                {
                    timer = 0;
                    lock (Locker)
                    {
                        if (_handler.CanClearOlderLinks)
                        {
                            try
                            {
                                _handler.HandleOlderClients();
                            }
                            catch(Exception e)
                            { HandleDisplayMessage("Cannot handle OlderClients: " + e.Message); }
                        }
                    }
                }
                // 处理命令
            }
        }
    }
}