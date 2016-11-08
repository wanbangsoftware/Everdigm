using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Wbs.Utilities;

namespace Wbs.Sockets
{
    /// <summary>
    /// 提供TCP/UDP监听服务
    /// </summary>
    public class SocketService
    {
        private TcpListener _listener = null;

        /// <summary>
        /// 监听UDP的Socket
        /// </summary>
        private Socket udpSocket;
        /// <summary>
        /// 处理UDP数据的线程
        /// </summary>
        private Thread udpSocketHandler = null;
        /// <summary>
        /// UDP数据接收缓冲区
        /// </summary>
        private Dictionary<string, AsyncUserDataBuffer> _UDP_Buffer_Received = new Dictionary<string, AsyncUserDataBuffer>();

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
        private List<StateObject> _clients;
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
        public SocketService()
        {
            _port = 31875;
            initializeItems();
        }
        /// <summary>
        /// 指定特定的端口
        /// </summary>
        /// <param name="port"></param>
        public SocketService(int port)
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

            _clients = new List<StateObject>();
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

            var localEndPoint = new IPEndPoint(IPAddress.Any, _port);
            udpSocket = new Socket(localEndPoint.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
            udpSocket.Bind(localEndPoint);
            udpSocketHandler = new Thread(new ThreadStart(handleUdpReceive));
            udpSocketHandler.Start();
            HandleOnMessage(Now + "Server has listen on port " + _port);
        }
        public void Stop()
        {
            IsStop = true;
            if (null != udpSocket)
            {
                udpSocket.Close();
            }
            if (null != udpSocketHandler)
            {
                udpSocketHandler.Abort();
                udpSocketHandler.Join();
                udpSocketHandler = null;
            }
            if (null != _listener)
            {
                _listener.Stop();
            }
            lock (_clients)
            {
                while (_clients.Count > 0)
                {
                    try
                    {
                        _clients[0].Dispose();
                    }
                    catch { }
                    _clients.RemoveAt(0);
                }
            }
        }
        /// <summary>
        /// 处理UDP接受的线程
        /// </summary>
        private void handleUdpReceive()
        {
            int len = 0;
            byte[] buffer = new byte[1024];
            while (!IsStop)
            {
                try
                {
                    if (null != udpSocket)
                    {
                        len = 0;
                        EndPoint clientEP = new IPEndPoint(IPAddress.Any, 0);
                        len = udpSocket.ReceiveFrom(buffer, ref clientEP);

                        lock (udpSocket)
                        {
                            var ip = (clientEP as IPEndPoint).Address.ToString();
                            var port = (clientEP as IPEndPoint).Port;
                            var token = ip + ":" + port.ToString();
                            lock (_UDP_Buffer_Received)
                            {
                                if (_UDP_Buffer_Received.ContainsKey(token))
                                {
                                    // 组包
                                    var data = _UDP_Buffer_Received[token];
                                    data.Buffer = CustomConvert.expand(data.Buffer, data.Buffer.Length + len);
                                    Buffer.BlockCopy(buffer, 0, data.Buffer, data.Buffer.Length - len, len);
                                    if (data.Buffer.Length >= data.Buffer[0])
                                    {
                                        // 包长度足够之后从暂存缓存中移除节点
                                        _UDP_Buffer_Received.Remove(token);
                                        // 发送消息
                                        OnReceivedData(this, new AsyncUserDataEvent() { Data = data });
                                    }
                                }
                                else
                                {
                                    if (len >= buffer[0])
                                    {
                                        // 如果收到的是完整的包则直接发给前台处理
                                        if (null != OnReceivedData)
                                        {
                                            var aude = new AsyncUserDataEvent();
                                            aude.Data = _bufferPool.Get();
                                            aude.Data.SetDataEvent(ip, port, buffer, len);
                                            aude.Data.PackageType = AsyncDataPackageType.UDP;
                                            OnReceivedData(this, aude);
                                        }
                                    }
                                    else
                                    {
                                        //var aude = new AsyncUserDataEvent();
                                        var data = _bufferPool.Get();
                                        data.SetDataEvent(ip, port, buffer, len);
                                        data.PackageType = AsyncDataPackageType.UDP;
                                        _UDP_Buffer_Received.Add(token, data);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    string a = e.Message;
                }
            }
        }
        /// <summary>
        /// 将buffer放回缓冲池中回收等待利用
        /// </summary>
        /// <param name="buffer"></param>
        public void RecycleBuffer(AsyncUserDataBuffer buffer)
        {
            // 缓冲池已满时销毁该对象
            if (!_bufferPool.Push(buffer))
                buffer.Dispose();
        }
        /// <summary>
        /// 发送数据到客户端(返回0==链接不存在1=发送成功2=网络处理错误)
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="buffer"></param>
        /// <returns>0==链接不存在1=发送成功2=网络处理错误</returns>
        public byte Send(int socket, byte[] buffer)
        {
            byte ret = 0;
            lock (_clients)
            {
                foreach (var obj in _clients)
                {
                    if (obj.socket.Handle.ToInt32() == socket)
                    {
                        try
                        {
                            obj.socket.Send(buffer);
                            ret = 1;
                        }
                        catch
                        { ret = 2; }
                        break;
                    }
                }
            }
            return ret;
        }
        /// <summary>
        /// 通过UDP方式发送出去
        /// </summary>
        /// <param name="port"></param>
        /// <param name="ip"></param>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public byte Send(int port, string ip, byte[] buffer)
        {
            byte ret = 0;
            try
            {
                IPEndPoint iep = new IPEndPoint(IPAddress.Parse(ip), port);
                Socket socket = new Socket(iep.Address.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
                socket.SendTo(buffer, buffer.Length, SocketFlags.None, iep);
                socket.Close();
                //udpSocket.SendTo(buffer, iep);
                ret = 1;
            }
            catch
            { ret = 2; }
            return ret;
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
                    lock (_clients)
                    {
                        _clients.Add(so);
                    }
                    if (null != OnConnected)
                    {
                        var aud = new AsyncUserDataEvent();
                        aud.Data = _bufferPool.Get();
                        aud.Data.SetDataEvent(so, 0);
                        OnConnected(this, aud);
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
                lock (so)
                {
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
                        if (so.length >= so.Received[0])
                        {
                            // 加入队列并等待处理
                            if (null != OnReceivedData)
                            {
                                var aud = new AsyncUserDataEvent();
                                aud.Data = _bufferPool.Get();
                                aud.Data.SetDataEvent(so, 2);
                                OnReceivedData(this, aud);
                            }
                            so.ClearReceived();
                        }
                    }
                    else
                    {
                        HandleOnMessage(string.Format("{0}Iridium server {1}:{2} disconnected.", Now, so.point.Address.ToString(), so.point.Port));
                        if (null != OnDisconnected)
                        {
                            var aud = new AsyncUserDataEvent();
                            aud.Data = _bufferPool.Get();
                            aud.Data.SetDataEvent(so, 1);
                            OnDisconnected(this, aud);
                        }
                        //so.socket.Shutdown(SocketShutdown.Both);
                        //so.socket.Close();
                    }
                }
            }
            catch (Exception e)
            { HandleOnMessage(Now + "Read_Callback error: " + e.Message + Environment.NewLine + e.StackTrace); }
        }
    }
    /// <summary>
    /// 异步接受消息的承载体
    /// </summary>
    public class StateObject:IDisposable
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
        /// <summary>
        /// 清空接收缓冲区
        /// </summary>
        public void ClearReceived()
        {
            Received = null;
            length = 0;
        }

        public void Dispose()
        {
            try
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                socket = null;
            }
            catch { }
            point = null;
            length = 0;
            buffer = null;
            Received = null;
        }
    }
}
