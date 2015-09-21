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
        public EventHandler<UIEventArgs> OnMessage;
        /// <summary>
        /// 铱星要发送命令的事件
        /// </summary>
        public EventHandler<IridiumDataEvent> OnIridiumSend;
        /// <summary>
        /// 提供TCP/UDP服务
        /// </summary>
        private SocketService _server;
        /// <summary>
        /// IOCP网络服务
        /// </summary>
        //private SocketAsyncEventServer _tcpServer;
        /// <summary>
        /// 待处理数据缓冲区(这个是线程安全的，所以不必再费心自己加读写锁)
        /// </summary>
        private static ConcurrentQueue<AsyncUserDataBuffer> _dataPool;
        private static ConcurrentQueue<IridiumData> _iridiumPool;
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
            _iridiumPool = new ConcurrentQueue<IridiumData>();
        }
        /// <summary>
        /// 初始化服务端
        /// </summary>
        private void InitializeServer()
        {
            _server = new SocketService(Port);
            //_tcpServer = new SocketAsyncEventServer(Port);
            _server.OnConnected += new EventHandler<AsyncUserDataEvent>(OnClientConnected);
            _server.OnDisconnected += new EventHandler<AsyncUserDataEvent>(OnClientDisconnected);
            _server.OnReceivedData += new EventHandler<AsyncUserDataEvent>(OnReceivedData);
            //_tcpServer.StartUDP = StartUDP;
        }
        /// <summary>
        /// 初始化线程池
        /// </summary>
        private void InitializeThreadPool()
        {
            var count = Environment.ProcessorCount;
            for (int i = 0; i < count; i++)
            {
                ThreadPool.QueueUserWorkItem(ThreadHandlePorc, i);
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
                _server.Start();
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
            _server.Stop();
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
        /// <summary>
        /// 将接收到的卫星数据放入待处理缓冲区
        /// </summary>
        /// <param name="e"></param>
        public void EnqueueIridiumData(IridiumDataEvent e)
        {
            _iridiumPool.Enqueue(e.Data);
        }
        private string DateTime(DateTime dt)
        {
            return dt.ToString("[yyyy/MM/dd HH:mm:ss.fff] ");
        }
        /// <summary>
        /// 当前系统时间
        /// </summary>
        private string Now { get { return System.DateTime.Now.ToString("[yyyy/MM/dd HH:mm:ss.fff] "); } }
        /// <summary>
        /// 数据处理时出错的信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataHandler_OnUnhandledMessage(object sender, UIEventArgs e)
        {
            HandleDisplayMessage(e.Message);
        }
        /// <summary>
        /// 向主界面提交显示信息申请
        /// </summary>
        /// <param name="message"></param>
        private void HandleDisplayMessage(string message)
        {
            if (null != OnMessage)
            {
                OnMessage(this, new UIEventArgs() { Message = message });
            }
        }
        private void DataHandler_OnIridiumSend(object sender, IridiumDataEvent e)
        {
            if (null != OnIridiumSend)
            {
                OnIridiumSend(this, e);
            }
        }
        /// <summary>
        /// 线程的执行过程
        /// </summary>
        private void ThreadHandlePorc(Object state)
        {
            var stat = (int)state;
            AsyncUserDataBuffer obj;
            IridiumData iridium;
            // 处理数据的Handler
            DataHandler _handler = new DataHandler();
            _handler.OnUnhandledMessage += new EventHandler<UIEventArgs>(DataHandler_OnUnhandledMessage);
            _handler.OnIridiumSend += new EventHandler<IridiumDataEvent>(DataHandler_OnIridiumSend);
            _handler.Server = _server;
            int timer = 0, sleeper = 10, gpsHandler = 0;

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
                                message = string.Format("{0}client {1}:{2} connected. [{3}]", DateTime(obj.ReceiveTime), obj.IP, obj.Port, obj.PackageType);
                                break;
                            case AsyncUserDataType.ClientDisconnected:
                                message = string.Format("{0}client {1}:{2} disconnected. [{3}]", DateTime(obj.ReceiveTime), obj.IP, obj.Port, obj.PackageType);
                                break;
                            case AsyncUserDataType.ReceivedData:
                                message = string.Format("{0}received data(length: {1}): {2} [{3}]", DateTime(obj.ReceiveTime),
                                    (null == obj.Buffer ? 0 : obj.Buffer.Length), CustomConvert.GetHex(obj.Buffer), obj.PackageType);
                                break;
                        }

                        // 处理数据
                        if (null != obj.Buffer)
                        {
                            _handler.HandleData(obj);
                        }

                        if (!string.IsNullOrEmpty(message))
                        {
                            HandleDisplayMessage(message);
                        }
                    }
                    finally
                    {
                        _server.RecycleBuffer(obj);
                    }
                }
                else
                {
                    if (stat == 0)
                    {
                        try
                        {
                            // 处理未处理的SMS信息
                            _handler.CheckSMSData();
                        }
                        catch (Exception sms)
                        { HandleDisplayMessage(string.Format("{0} Cannot handle CheckSMSData: {1}, Trace: {2}", Now, sms.Message, sms.StackTrace)); }
                    }
                }

                // 查找是否有铱星数据待处理
                iridium = null;
                if (!_iridiumPool.TryDequeue(out iridium)) iridium = null;
                if (null != iridium)
                {
                    try
                    {
                        // 处理铱星数据
                        _handler.HandleIridiumData(iridium);
                    }
                    catch (Exception iri)
                    { HandleDisplayMessage(string.Format("{0} Cannot handle HandleIridiumData: {1}, Trace: {2}", Now, iri.Message, iri.StackTrace)); }
                }
                // 只有第一个线程有权处理命令后面的数据
                if (stat > 0)
                {
                    timer = 0;
                    continue;
                }
                // 处理TCP下发送的命令
                if (timer % 1000 == 0)
                {
                    // 每秒钟增加1
                    gpsHandler++;

                    lock (Locker)
                    {
                        try
                        {
                            // 每5秒处理一次未处理的excel请求
                            if (gpsHandler % 5 == 0)
                                _handler.HandleWebRequestExcel();

                            // 每2秒处理一次未发的命令
                            if (gpsHandler % 2 == 0)
                            {
                                try
                                {
                                    _handler.CheckGSMCommand();
                                    _handler.CheckIridiumCommand();
                                }
                                catch (Exception e)
                                {
                                    HandleDisplayMessage(string.Format("{0} Can not handle CheckTcpCommand/CheckIridiumCommand: {1}, Trace: {2}", Now, e.Message, e.StackTrace));
                                }
                            }

                            // 10秒处理一次获取GPS地理位置
                            if (gpsHandler % sleeper == 0) {
                                gpsHandler = 0;
                                // 处理一次定位信息
                                if (stat == 0)
                                {
                                    _handler.HandleGpsAddress();
                                }
                            }
                        }
                        catch(Exception e)
                        {
                            HandleDisplayMessage(string.Format("{0} Cannot handle CheckCommand: {1}, Trace: {2}", Now, e.Message, e.StackTrace));
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
                            { HandleDisplayMessage(string.Format("{0} Cannot handle OlderClients: {1}, Trace: {2}", Now, e.Message, e.StackTrace)); }
                        }
                    }
                }
            }
        }
    }
}