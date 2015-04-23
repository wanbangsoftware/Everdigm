using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Concurrent;

namespace Wbs.Everdigm.Desktop
{
    /// <summary>
    /// 铱星DirectIP本地服务
    /// </summary>
    public class IridiumServer
    {
        private bool IsStop = false;
        private Thread _handleThread = null;
        /// <summary>
        /// 消息
        /// </summary>
        public EventHandler<string> OnMessage = null;
        /// <summary>
        /// 处理数据时的消息
        /// </summary>
        /// <param name="message"></param>
        private void HandleOnMessage(string message)
        {
            if (null != OnMessage)
            {
                OnMessage(this, message);
            }
        }
        /// <summary>
        /// 当前监听的服务端口
        /// </summary>
        private TcpListener _listener = null;
        /// <summary>
        /// 接收到的信息
        /// </summary>
        private static ConcurrentQueue<IridiumBuffer> _pool;

        public IridiumServer()
        {
            _pool = new ConcurrentQueue<IridiumBuffer>();
            _handleThread = new Thread(new ThreadStart(HandleIridiumPackage));
            _handleThread.Start();
        }

        private int _port;
        /// <summary>
        /// 设置本地开放给铱星网管的端口
        /// </summary>
        public int Port { get { return _port; } set { _port = value; } }
        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="port"></param>
        public void Start(int port)
        {
            _port = port;
            _listener = new TcpListener(IPAddress.Any, _port);
            _listener.Start();
            _listener.BeginAcceptSocket(new AsyncCallback(Listen_Callback), _listener);
            HandleOnMessage(Now + "Iridium server has listen on port " + _port);
        }
        /// <summary>
        /// 停止服务
        /// </summary>
        public void Stop()
        {
            IsStop = true;
            _listener.Stop();
            if (null != _handleThread)
            {
                _handleThread.Abort();
                _handleThread.Join();
                _handleThread = null;
            }
            HandleOnMessage(Now + "Iridium server has stoped.");
        }
        private static void Listen_Callback(IAsyncResult ar)
        {
            TcpListener listener = (TcpListener)ar.AsyncState;
            if (null != listener)
            {
                Socket ss = listener.EndAcceptSocket(ar);
                StateObject so = new StateObject();
                so.socket = ss;
                ss.BeginReceive(so.buffer, 0, StateObject.BUFFER_SIZE, SocketFlags.None, new AsyncCallback(Read_Callback), so);
            }
        }
        private static void Read_Callback(IAsyncResult ar)
        {
            StateObject so = (StateObject)ar.AsyncState;
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
                if (so.length >= so.Received[1])
                { 
                    // 加入队列并等待处理
                    var b = new IridiumBuffer();
                    b.length = so.length;
                    b.buffer = new byte[so.length];
                    Buffer.BlockCopy(so.Received, 0, b.buffer, 0, so.length);
                    b.socket = s;
                    so.length = 0;
                    so.Received = null;
                    _pool.Enqueue(b);
                }
            }
            else
            {
                so.socket.Shutdown(SocketShutdown.Both);
                so.socket.Close();
            }
        }
        /// <summary>
        /// 将数据加入待处理队列
        /// </summary>
        /// <param name="buffer"></param>
        public void AddQueue(IridiumBuffer buffer)
        {
            _pool.Enqueue(buffer);
        }
        /// <summary>
        /// 获取当前系统时间的字符串
        /// </summary>
        private string Now { get { return DateTime.Now.ToString("[yyyy/MM/dd HH:mm:ss.fff] "); } }
        /// <summary>
        /// 处理接收到的铱星数据包
        /// </summary>
        private void HandleIridiumPackage()
        {
            int sleep = 100;
            int index = 0;
            IridiumBuffer obj;
            bool NeedConfirmation = false;

            while (!IsStop)
            {
                Thread.Sleep(sleep);

                obj = null;
                if (!_pool.TryDequeue(out obj)) obj = null;

                if (null != obj)
                {
                    try
                    {
                        NeedConfirmation = false;
                        HandleOnMessage(Now + "Iridium package(len: " + obj.length + "): " + Wbs.Utilities.CustomConvert.GetHex(obj.buffer));
                        // 分析整包铱星数据
                        Iridium iridum = new Iridium();
                        iridum.PackageContent = obj.buffer;
                        iridum.Unpackage();
                        try
                        {
                            // 从铱星数据体中分别解析各个IE字段
                            index = 0;
                            int length = 0, total = iridum.OverallMessageLength;
                            while (index < iridum.OverallMessageLength)
                            {
                                IE ie = new IE();
                                ie.Content = new byte[total - index];
                                Buffer.BlockCopy(iridum.Content, index, ie.Content, 0, total - index);
                                ie.Unpackage();
                                length = ie.Length + Iridium.HEADER_SIZE;
                                switch (ie.IEI)
                                {
                                    case IE.MO_HEADER:
                                        MOHeader moh = new MOHeader();
                                        moh.Content = new byte[length];
                                        Buffer.BlockCopy(ie.Content, 0, moh.Content, 0, length);
                                        moh.Unpackage();
                                        HandleOnMessage(Now + moh.ToString());
                                        moh = null;
                                        NeedConfirmation = true;
                                        break;
                                    case IE.MO_LOCATION:
                                        MOLocation mol = new MOLocation();
                                        mol.Content = new byte[length];
                                        Buffer.BlockCopy(ie.Content, 0, mol.Content, 0, length);
                                        mol.Unpackage();
                                        HandleOnMessage(Now + mol.ToString());
                                        mol = null;
                                        NeedConfirmation = true;
                                        break;
                                    case IE.MO_PAYLOAD:
                                        MOPayload mop = new MOPayload();
                                        mop.Content = new byte[length];
                                        Buffer.BlockCopy(ie.Content, 0, mop.Content, 0, length);
                                        mop.Unpackage();
                                        HandleOnMessage(Now + mop.ToString());
                                        mop = null;
                                        NeedConfirmation = true;
                                        break;
                                    case IE.MO_CONFIRMATION:
                                        break;
                                    case IE.MT_CONFIRMATION:
                                        break;
                                    case IE.MT_HEADER:
                                        break;
                                    case IE.MT_PAYLOAD:
                                        break;
                                    case IE.MT_PRIORITY:
                                        break;
                                }
                                ie = null;
                                index += length;
                            }
                        }
                        finally
                        {
                            // 反馈MO Confirmation包
                            if (NeedConfirmation)
                            {
                                MOConfirmation confirm = new MOConfirmation();
                                confirm.Package();
                                iridum.OverallMessageLength = 0;
                                iridum.Content = confirm.Content;
                                iridum.Package();
                                try
                                {
                                    obj.socket.Send(iridum.PackageContent);
                                }
                                catch { }
                                HandleOnMessage(Now + "MO confirmation: " + Wbs.Utilities.CustomConvert.GetHex(iridum.PackageContent));
                                iridum = null;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        HandleOnMessage(Now + "HandleIridiumPackage error: " + e.Message + Environment.NewLine + e.StackTrace);
                    }
                    finally
                    {
                        obj.Dispose();
                        obj = null;
                    }
                }
            }
        }
    }
    public class StateObject
    {
        public Socket socket = null;
        public int length = 0;
        public const int BUFFER_SIZE = 2048;
        public byte[] buffer = new byte[BUFFER_SIZE];
        public byte[] Received = null;
    }
    public class IridiumBuffer : IDisposable
    {
        public Socket socket = null;
        public int length = 0;
        public byte[] buffer = null;

        ~IridiumBuffer()
        { Dispose(); }
        public void Dispose()
        {
            socket = null;
            length = 0;
            buffer = null;
        }
    }
}
