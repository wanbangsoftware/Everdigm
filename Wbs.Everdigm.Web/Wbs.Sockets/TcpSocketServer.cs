using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Wbs.Sockets
{
    /// <summary>
    /// 提供TCP
    /// </summary>
    public class TcpSocketServer
    {
        private TcpListener _listener = null;
        private int _port = 0; 
        /// <summary>
        /// 标记是否手动停止
        /// </summary>
        private bool IsStop = false;

        /// <summary>
        /// 客户端连接成功事件
        /// </summary>
        public EventHandler<AsyncUserDataEvent> OnConnected;
        /// <summary>
        /// 客户端连接断开事件
        /// </summary>
        public EventHandler<AsyncUserDataEvent> OnDisconnected;
        /// <summary>
        /// 接收到客户端数据的事件
        /// </summary>
        public EventHandler<AsyncUserDataEvent> OnReceivedData;
        /// <summary>
        /// 客户端连接队列
        /// </summary>
        private List<AsyncUserToken> _clients;
        /// <summary>
        /// 数据结构缓冲池
        /// </summary>
        private ObjectPool<AsyncUserDataBuffer> _bufferPool;
        /// <summary>
        /// 处理异常消息的事件
        /// </summary>
        public EventHandler<UIEventArgs> OnMessage = null;
        /// <summary>
        /// 处理数据时的消息
        /// </summary>
        /// <param name="message"></param>
        private void HandleOnMessage(string message)
        {
            if (null != OnMessage)
            {
                OnMessage(this, new UIEventArgs() { Message = message });
            }
        }
        /// <summary>
        /// 默认用31875端口开启监听
        /// </summary>
        public TcpSocketServer()
        {
            _port = 31875;
            initializeItems();
        }
        /// <summary>
        /// 指定特定的端口
        /// </summary>
        /// <param name="port"></param>
        public TcpSocketServer(int port)
        {
            _port = port;
            initializeItems();
        }
        /// <summary>
        /// 初始化需要用到的缓冲区
        /// </summary>
        private void initializeItems()
        {
            // 创建数据缓冲区池
            _bufferPool = new ObjectPool<AsyncUserDataBuffer>(() => new AsyncUserDataBuffer(), act =>
            {
                act.Buffer = null;
                act.DataType = AsyncUserDataType.None;
                act.IP = "";
                act.Port = 0;
                act.ReceiveTime = DateTime.Now;
                act.SocketHandle = 0;
            });

            _clients = new List<AsyncUserToken>();
        }
        /// <summary>
        /// 监听的端口
        /// </summary>
        public int Port { get { return _port; } set { _port = value; } }
        /// <summary>
        /// 启动监听服务
        /// </summary>
        public void Start()
        {
            _listener = new TcpListener(IPAddress.Any, _port);
            _listener.Start();
            _listener.BeginAcceptSocket(new AsyncCallback(Listen_Callback), _listener);
            HandleOnMessage(Now + "TCP server has listen on port " + _port);
        }
        
        /// <summary>
        /// 获取当前系统时间的字符串
        /// </summary>
        private string Now { get { return DateTime.Now.ToString("[yyyy/MM/dd HH:mm:ss.fff] "); } }
        /// <summary>
        /// 监听客户端连接的异步回调
        /// </summary>
        /// <param name="ar"></param>
        private void Listen_Callback(IAsyncResult ar)
        {
            try
            {
                TcpListener listener = (TcpListener)ar.AsyncState;
                if (null != listener)
                {
                    Socket ss = listener.EndAcceptSocket(ar);
                    StateObject so = new StateObject();
                    so.socket = ss;
                    so.point = ss.RemoteEndPoint as IPEndPoint;
                    ss.BeginReceive(so.buffer, 0, StateObject.BUFFER_SIZE, SocketFlags.None, new AsyncCallback(Read_Callback), so);
                    if (null != OnConnected) { 

                    }
                    HandleOnMessage(Environment.NewLine + Now + "Client [" + so.point.Address.ToString() + ":" + so.point.Port + "] connected.");
                    _listener.BeginAcceptSocket(new AsyncCallback(Listen_Callback), _listener);
                }
            }
            catch (ObjectDisposedException e)
            {
                if (!IsStop)
                {
                    HandleOnMessage(Now + "Listen_Callback error: " + e.Message + Environment.NewLine + e.StackTrace);
                }
            }
        }
        /// <summary>
        /// 读取数据的回调
        /// </summary>
        /// <param name="ar"></param>
        private void Read_Callback(IAsyncResult ar)
        {
            try
            {
                StateObject so = (StateObject)ar.AsyncState;
                if (null == so) return;
                Socket s = so.socket;

                int read = s.EndReceive(ar);
                if (read > 0)
                {
                    if (null == so.Received)
                    {
                        so.Received = new byte[read];
                        so.length = read;
                    }
                    else
                    {
                        so.length += read;
                        so.Received = Wbs.Utilities.CustomConvert.expand(so.Received, so.length);
                    }
                    Buffer.BlockCopy(so.buffer, 0, so.Received, so.length - read, read);
                    s.BeginReceive(so.buffer, 0, StateObject.BUFFER_SIZE, SocketFlags.None, new AsyncCallback(Read_Callback), so);
                    if (so.length >= so.Received[2])
                    {
                        // 加入队列并等待处理
                        
                    }
                }
                else
                {
                    HandleOnMessage(string.Format("{0}Iridium server {1}:{2} disconnected.", Now, so.point.Address.ToString(), so.point.Port));
                    //so.socket.Shutdown(SocketShutdown.Both);
                    //so.socket.Close();
                }
            }
            catch (Exception e)
            { HandleOnMessage(Now + "Read_Callback error: " + e.Message + Environment.NewLine + e.StackTrace); }
        }
    }
    /// <summary>
    /// 异步接受消息的承载体
    /// </summary>
    public class StateObject
    {
        /// <summary>
        /// 当前连接的socket
        /// </summary>
        public Socket socket = null;
        /// <summary>
        /// 当前连接的节点
        /// </summary>
        public IPEndPoint point = null;
        /// <summary>
        /// 已接受的信息长度
        /// </summary>
        public int length = 0;
        /// <summary>
        /// 标准数据包长度
        /// </summary>
        public const int BUFFER_SIZE = 1024;
        /// <summary>
        /// 每次异步调用之后的接收缓冲区
        /// </summary>
        public byte[] buffer = new byte[BUFFER_SIZE];
        /// <summary>
        /// 总接收到的数据长度
        /// </summary>
        public byte[] Received = null;
    }
}
