using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace Wbs.Sockets
{
    /// <summary>
    /// 实现 SocketAsyncEvent 异步收发的服务端
    /// </summary>
    public class SocketAsyncEventServer : IDisposable
    {
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
        /// 缓冲区
        /// </summary>
        private BufferManager bm_bufferManager;
        /// <summary>
        /// 服务器监听 Socket。
        /// </summary>
        private Socket listenSocket;
        /// <summary>
        /// 异步接受缓冲池
        /// </summary>
        private SocketAsyncEventArgsPool saea_readWritePool;
        /// <summary>
        /// 最大连接数量信号灯，超过连接量时新连接需要等待处理。
        /// </summary>
        private Semaphore m_maxNumberAcceptedClients;
        /// <summary>
        /// 最大连接数量。
        /// </summary>
        private int dwMaxConnections = 5000;
        /// <summary>
        /// 服务器开放的端口号码。
        /// </summary>
        private int dwLocalPort = 31875;
        /// <summary>
        /// 每个 Socket 的 I/O 操作所需的缓冲区大小。
        /// </summary>
        private int dwReceiveBufferSize = 2048;
        /// <summary>
        /// 读、写两个操作。
        /// </summary>
        private const int preAlloc = 2;
        /// <summary>
        /// 创建一个伺服器并指定端口
        /// </summary>
        public SocketAsyncEventServer(int port)
        {
            Port = port;
        }
        /// <summary>
        /// 设置本地开放的服务端口号码
        /// </summary>
        public int Port
        {
            get { return dwLocalPort; }
            set { dwLocalPort = value; }
        }
        /// <summary>
        /// 设置服务允许的最大链接客户端数量
        /// </summary>
        public int MaxConnections
        {
            get { return dwMaxConnections; }
            set { dwMaxConnections = value; }
        }
        /// <summary>
        /// 设置链接的 IO 缓冲区大小
        /// </summary>
        public int ReceiveBufferSize
        {
            get { return dwReceiveBufferSize; }
            set { dwReceiveBufferSize = value; }
        }
        /// <summary>
        /// 初始化参数并准备开启服务
        /// </summary>
        private void Initialize()
        {
            bm_bufferManager = new BufferManager(dwReceiveBufferSize * dwMaxConnections * preAlloc, dwReceiveBufferSize);
            saea_readWritePool = new SocketAsyncEventArgsPool(dwMaxConnections);
            m_maxNumberAcceptedClients = new Semaphore(dwMaxConnections, dwMaxConnections);

            bm_bufferManager.InitBuffer();
            // 初始化
            SocketAsyncEventArgs saea;
            for (int i = 0; i < dwMaxConnections; i++)
            {
                saea = new SocketAsyncEventArgs();
                saea.Completed += new EventHandler<SocketAsyncEventArgs>(IO_Completed);
                saea.UserToken = new AsyncUserToken();

                bm_bufferManager.SetBuffer(saea);
                saea_readWritePool.Push(saea);
            }

            // 创建数据缓冲区池
            _bufferPool = new ObjectPool<AsyncUserDataBuffer>(() => new AsyncUserDataBuffer(), act => {
                act.Buffer = null;
                act.DataType = AsyncUserDataType.None;
                act.IP = "";
                act.Port = 0;
                act.ReceiveTime = DateTime.Now;
                act.SocketHandle = 0;
            });

            _clients = new List<AsyncUserToken>();

            Start(new IPEndPoint(IPAddress.Any, dwLocalPort));
        }
        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="localEndPoint"></param>
        private void Start(IPEndPoint localEndPoint)
        {
            listenSocket = new Socket(localEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            listenSocket.Bind(localEndPoint);
            // 监听服务器侧端口，允许 100 个等待队列。
            listenSocket.Listen(100);

            //history(Now() + " server has been start and listen on port: " + dwLocalPort.ToString());

            // 投递 Accept 信息。
            StartAccept(null);
        }
        /// <summary>
        /// 投递 Accept 操作以便允许客户端连接进来。
        /// </summary>
        /// <param name="acceptEventArg"></param>
        private void StartAccept(SocketAsyncEventArgs acceptEventArg)
        {
            if (acceptEventArg == null)
            {
                acceptEventArg = new SocketAsyncEventArgs();
                acceptEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(AcceptEventArg_Completed);
            }
            else
            {
                // 上下文对象被重新利用时必须清除掉 socket 对象，否则接收到的都是重复数据，而且会造成内存泄漏。
                acceptEventArg.AcceptSocket = null;
            }

            // 当达到最大连接数量时，新进入的连接需要在队列中等待。
            m_maxNumberAcceptedClients.WaitOne();
            // 在监听端口上开始异步 Accept 操作。
            bool willRaiseEvent = listenSocket.AcceptAsync(acceptEventArg);
            // 当有客户端链接进来时执行客户端节点连接操作并绑定客户端节点信息。
            if (!willRaiseEvent)
            {
                ProcessAccept(acceptEventArg);
            }
        }
        /// <summary>
        /// 客户端连接成功的回调过程。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AcceptEventArg_Completed(object sender, SocketAsyncEventArgs e)
        {
            ProcessAccept(e);
        }
        /// <summary>
        /// 客户端连接成功的处理过程。
        /// 客户端的基本信息在此绑定到某一个 SocketAsyncEventArgs 中。
        /// </summary>
        /// <param name="e"></param>
        private void ProcessAccept(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                // 从 SocketAsyncEventArgsPool 池中取出一个空节点来保存新连接进来的客户端。
                SocketAsyncEventArgs readEventArgs = saea_readWritePool.Pop();
                // 将客户端信息附加到用户信息上。
                var aut = readEventArgs.UserToken as AsyncUserToken;
                aut.Socket = e.AcceptSocket;

                // 将新入的链接加入队列
                lock (_clients)
                {
                    _clients.Add(aut);
                }

                // 通知处理客户端连接事件
                if (null != OnConnected)
                {

                    var aude = new AsyncUserDataEvent();
                    aude.Data = _bufferPool.Get();
                    aude.Data.SetDataEvent(readEventArgs, true);
                    OnConnected(this, aude);
                }

                // 向新连接进来的客户端投递 receive 操作。
                bool willRaiseEvent = e.AcceptSocket.ReceiveAsync(readEventArgs);
                // 如果客户端此时发送数据则需要立即处理。
                if (!willRaiseEvent)
                {
                    ProcessReceive(readEventArgs);
                }

                // 继续等待新的客户端连接。
                StartAccept(e);
            }
        }
        /// <summary>
        /// SocketAsyncEventArgs 完成一个 I/O 操作。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IO_Completed(object sender, SocketAsyncEventArgs e)
        {
            var aut = e.UserToken as AsyncUserToken;
            try
            {
                lock (aut)//避免同一个userToken同时有多个线程操作
                {
                    // determine which type of operation just completed and call the associated handler
                    // 根据不同的 I/O 操作进行不同的处理过程。
                    switch (e.LastOperation)
                    {
                        case SocketAsyncOperation.Receive:// 接收到信息。
                            ProcessReceive(e);
                            break;
                        case SocketAsyncOperation.Send:// 发送信息。
                            ProcessSend(e);
                            break;
                        default:// 其他未知操作时释放异常信息。
                            throw new ArgumentException("The last operation completed on the socket was not a receive or send");
                    }
                }
            }
            catch (Exception error)
            { }
        }
        /// <summary>
        /// 接收客户端的数据或客户端的断开信息。
        /// </summary>
        /// <param name="e"></param>
        private void ProcessReceive(SocketAsyncEventArgs e)
        {
            if (null == (e.UserToken as AsyncUserToken).Socket) return;
            // 如果 BytesTransferred 不为 0 且没有发送 SocketError，则说明收到客户端发送的信息。
            if (e.BytesTransferred > 0 && e.SocketError == SocketError.Success)
            {
                if (null != OnReceivedData)
                {
                    var aude = new AsyncUserDataEvent();
                    aude.Data = _bufferPool.Get();
                    aude.Data.SetDataEvent(e, 0);
                    OnReceivedData(this, aude);
                }

                //回传收到的信息。
                //e.SetBuffer(e.Offset, e.BytesTransferred);
                //e.SetBuffer(0, 0);
                // 数据接收完毕之后重新在连接上投递 receive 操作，如此循环，就可以不断的接收到客户端发送的数据。
                bool willRaiseEvent = (e.UserToken as AsyncUserToken).Socket.ReceiveAsync(e);//token.Socket.SendAsync(e);
                if (!willRaiseEvent)
                {
                    ProcessReceive(e);
                }
            }
            else
            {
                CloseClientSocket(e);
            }
        }

        /// <summary>
        /// 处理数据发送完毕之后的操作。
        /// </summary>
        /// <param name="e"></param>
        private void ProcessSend(SocketAsyncEventArgs e)
        {
            // 如果数据发送成功则重新投递一个 receive 操作。
            if (e.SocketError == SocketError.Success)
            {
                // 重新投递接收操作。
                bool willRaiseEvent = (e.UserToken as AsyncUserToken).Socket.ReceiveAsync(e);
                if (!willRaiseEvent)
                {
                    ProcessReceive(e);
                }
            }
            // 否则关闭出现异常的客户端连接。
            else
            {
                CloseClientSocket(e);
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
        /// 关闭一个连接。
        /// </summary>
        /// <param name="e"></param>
        private void CloseClientSocket(SocketAsyncEventArgs e)
        {
            var aut = e.UserToken as AsyncUserToken;
            // socket 已经关闭过了，直接返回
            if (null == aut.Socket) return;

            // 通知客户端断开事件
            if (null != OnDisconnected)
            {
                var aude = new AsyncUserDataEvent();
                aude.Data = _bufferPool.Get();
                aude.Data.SetDataEvent(e, false);
                OnDisconnected(this, aude);
            }

            // 关闭 socket 关联的发送操作。
            try
            {
                aut.Socket.Shutdown(SocketShutdown.Both);
            }
            // 屏蔽当 socket 已经关闭的异常。
            catch (Exception) { }
            aut.Socket.Close();
            aut.Socket = null;

            // 释放一个等待队列信号，以便让在连接 accept 队列中等待处理的新连接得到处理。
            m_maxNumberAcceptedClients.Release();

            // 将该连接节点的 SocketAsyncEventArgs 回收到 SocketAsyncEventArgsPool 池中等待继续使用。
            saea_readWritePool.Push(e);

            // 从链接队列中移除节点
            lock (_clients)
            {
                _clients.Remove(aut);
            }
        }
        /// <summary>
        /// 开启服务
        /// </summary>
        public void Start()
        {
            Initialize();
        }
        /// <summary>
        /// 关闭服务并销毁所占用的任何资源
        /// </summary>
        public void Stop()
        {
            // 关闭监听端口以便阻止新的客户端连接进来。
            if (null != listenSocket)
            {
                listenSocket.Close();
            }
            // 关闭所有链接
            lock(_clients)
            {
                while (_clients.Count > 0)
                {
                    try
                    {
                        _clients[0].Socket.Shutdown(SocketShutdown.Both);
                    }
                    catch { }
                    _clients[0].Socket.Close();
                    _clients[0].Socket = null;
                    _clients.RemoveAt(0);
                }
            }
            m_maxNumberAcceptedClients.Dispose();
        }
        ~SocketAsyncEventServer()
        {
            Dispose();
        }
        /// <summary>
        /// 销毁系统所占用的任何资源
        /// </summary>
        public void Dispose()
        {
            Stop();
        }
    }
}
