using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Concurrent;

using Wbs.Sockets;

namespace Wbs.Everdigm.Desktop
{
    public class MTMSN : IEquatable<MTMSN>
    {
        public DateTime time { get; set; }
        public ushort mtmsn { get; set; }
        public byte HandleTimes { get; set; }
        public MTMSN(ushort mtmsn)
        {
            time = DateTime.Now;
            this.mtmsn = mtmsn;
            HandleTimes = 0;
        }
        public override bool Equals(object obj)
        {
            if (null == obj) return false;
            MTMSN m = obj as MTMSN;
            if (null == m) return false;
            return Equals(m);
        }
        public bool Equals(MTMSN other)
        {
            if (null == other) return false;
            return other.mtmsn == this.mtmsn;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    /// <summary>
    /// 铱星DirectIP本地服务
    /// </summary>
    public class IridiumServer
    {
        private bool IsStop = false;
        private Thread _handleThread = null;
        private bool _showPackage = false;
        private object lock_momsn = new object();
        private List<MTMSN> MOMSNs = new List<MTMSN>();
        /// <summary>
        /// 清理MOMSN列表的时间
        /// </summary>
        private long MOMSNTicks = DateTime.Now.Ticks;
        /// <summary>
        /// 将服务器新生成的MTMSN加入监控队列
        /// </summary>
        /// <param name="mtmsn"></param>
        public void AddMTMSN(ushort mtmsn)
        {
            var m = new MTMSN(mtmsn);
            lock (lock_momsn)
            {
                if (MOMSNs.Contains(m))
                    MOMSNs.Remove(m);

                MOMSNs.Add(m);
            }
        }
        /// <summary>
        /// 设置是否显示铱星数据拆包结果
        /// </summary>
        public bool ShowPackageInformation
        {
            get { return _showPackage; }
            set { _showPackage = value; }
        }
        /// <summary>
        /// 需要显示的消息
        /// </summary>
        public EventHandler<UIEventArgs> OnMessage = null;
        /// <summary>
        /// 需要处理的数据
        /// </summary>
        public EventHandler<IridiumDataEvent> OnIridiumReceive = null;
        /// <summary>
        /// 处理数据时的消息
        /// </summary>
        /// <param name="message"></param>
        private void HandleOnMessage(string message)
        {
            //if (_showPackage)
            {
                if (null != OnMessage)
                {
                    OnMessage(this, new UIEventArgs() { Message = message });
                }
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
            HandleOnMessage(Now + "Iridium server has stopped.");
        }
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
                    if (_showPackage)
                        HandleOnMessage(Environment.NewLine + Now + "Iridium server [" + so.point.Address.ToString() + ":" + so.point.Port + "] connected.");
                    _listener.BeginAcceptSocket(new AsyncCallback(Listen_Callback), _listener);
                }
            }
            catch (ObjectDisposedException e)
            {
                if (!IsStop)
                {
                    HandleOnMessage(format("{0} Listen_Callback error: {1}, Trace: {2}", Now, e.Message, e.StackTrace));
                }
            }
        }
        private string format(string format, params object[] args)
        {
            return DataHandler.format(format, args);
        }
        private void Read_Callback(IAsyncResult ar)
        {
            try
            {
                StateObject so = (StateObject)ar.AsyncState;
                if (null == so) return;
                Socket s = so.socket;
                // 增加判断socket是否还正常连接的判断 2015/09/16 12:40
                if (null == s) return;
                if (!s.Connected) return;

                int read = s.EndReceive(ar);
                if (read > 0)
                {
                    if (_showPackage)
                        HandleOnMessage(format("{0}Received data(length:{1}) from {2}:{3}", Now, read, so.point.Address.ToString(), so.point.Port));
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
                    if (_showPackage)
                        HandleOnMessage(format("{0}Iridium server {1}:{2} disconnected.", Now, so.point.Address.ToString(), so.point.Port));
                    //so.socket.Shutdown(SocketShutdown.Both);
                    //so.socket.Close();
                }
            }
            catch (Exception e)
            {
                if (e.Message.IndexOf("forcibly") < 0)
                    HandleOnMessage(format("{0} Read_Callback error: {1}, Trace: {2}",Now,e.Message,e.StackTrace));
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
        public void AddQueue(byte[] buffer)
        {
            IridiumBuffer b = new IridiumBuffer();
            b.length = buffer.Length;
            b.buffer = new byte[b.length];
            Buffer.BlockCopy(buffer, 0, b.buffer, 0, b.length);
            b.socket = null;
            _pool.Enqueue(b);
        }
        /// <summary>
        /// 获取当前系统时间的字符串
        /// </summary>
        private string Now { get { return DateTime.Now.ToString("[yyyy/MM/dd HH:mm:ss.fff] "); } }
        private List<string> _history = new List<string>();
        /// <summary>
        /// 处理接收到的铱星数据包
        /// </summary>
        private void HandleIridiumPackage()
        {
            int sleep = 100;
            int index = 0;
            ushort momsn = 0;
            IridiumBuffer obj;
            bool NeedConfirmation = false;

            while (!IsStop)
            {
                Thread.Sleep(sleep);

                obj = null;
                if (!_pool.TryDequeue(out obj)) obj = null;

                if (null != obj)
                {
                    bool showPayload = false;
                    try
                    {
                        NeedConfirmation = false;
                        momsn = 0;

                        _history.Add(Now + "Iridium package(length: " + obj.length + "): " + Utilities.CustomConvert.GetHex(obj.buffer));

                        // 分析整包铱星数据
                        Iridium iridum = new Iridium();
                        iridum.PackageContent = obj.buffer;
                        iridum.Unpackage();
                        try
                        {
                            // 从铱星数据体中分别解析各个IE字段
                            index = 0;
                            IridiumDataEvent data = new IridiumDataEvent();
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
                                        _history.Add(Now + moh.ToString());
                                        data.Data = new IridiumData();
                                        // 默认为终端接收数据的状态
                                        data.Data.Type = IridiumDataType.MTModelReceiveStatus;
                                        data.Data.IMEI = moh.IMEI;
                                        data.Data.MTMSN = moh.MTMSN;
                                        momsn = moh.MTMSN;
                                        data.Data.Time = moh.Time;
                                        data.Data.Status = moh.SessionStatus;
                                        moh = null;
                                        NeedConfirmation = true;
                                        break;
                                    case IE.MO_LOCATION:
                                        MOLocation mol = new MOLocation();
                                        mol.Content = new byte[length];
                                        Buffer.BlockCopy(ie.Content, 0, mol.Content, 0, length);
                                        mol.Unpackage();
                                        _history.Add(Now + mol.ToString());
                                        mol = null;
                                        NeedConfirmation = true;
                                        break;
                                    case IE.MO_PAYLOAD:
                                        MOPayload mop = new MOPayload();
                                        mop.Content = new byte[length];
                                        Buffer.BlockCopy(ie.Content, 0, mop.Content, 0, length);
                                        mop.Unpackage();
                                        _history.Add(Now + mop.ToString());
                                        // 终端发送了数据上来
                                        data.Data.Type = IridiumDataType.MOPayload;
                                        data.Data.Payload = mop.Payload;
                                        mop = null;
                                        showPayload = true;
                                        NeedConfirmation = true;
                                        break;
                                    case IE.MO_CONFIRMATION:
                                        MOConfirmation moc = new MOConfirmation();
                                        moc.Content = new byte[length];
                                        Buffer.BlockCopy(ie.Content, 0, moc.Content, 0, length);
                                        moc.Unpackage();
                                        _history.Add(Now + moc.ToString());
                                        moc = null;
                                        break;
                                    case IE.MT_CONFIRMATION:
                                        MTConfirmation mtc = new MTConfirmation();
                                        mtc.Content = new byte[length];
                                        Buffer.BlockCopy(ie.Content, 0, mtc.Content, 0, length);
                                        mtc.Unpackage();
                                        // MTConfirmation消息没有Header结构
                                        data.Data = new IridiumData();
                                        data.Data.Type = IridiumDataType.MTServerSendStatus;
                                        data.Data.MTMSN = (ushort)mtc.UniqueID;
                                        momsn = (ushort)mtc.UniqueID;
                                        data.Data.IMEI = mtc.IMEI;
                                        data.Data.Status = mtc.Status;
                                        _history.Add(Now + mtc.ToString());
                                        mtc = null;
                                        break;
                                    case IE.MT_HEADER:
                                        MTHeader mth = new MTHeader();
                                        mth.Content = new byte[length];
                                        Buffer.BlockCopy(ie.Content, 0, mth.Content, 0, length);
                                        mth.Unpackage();
                                        momsn = (ushort)mth.UniqueID;
                                        _history.Add(Now + mth.ToString());
                                        mth = null;
                                        break;
                                    case IE.MT_PAYLOAD:
                                        MTPayload mtp = new MTPayload();
                                        mtp.Content = new byte[length];
                                        Buffer.BlockCopy(ie.Content, 0, mtp.Content, 0, length);
                                        mtp.Unpackage();
                                        _history.Add(Now + mtp.ToString());
                                        mtp = null;
                                        break;
                                    case IE.MT_PRIORITY:
                                        break;
                                }
                                ie = null;
                                index += length;
                            }
                            // 分析完毕之后看是否为终端发送的数据
                            if (null != data)
                            {
                                if (null != data.Data)
                                {
                                    OnIridiumReceive?.Invoke(this, data);
                                }
                                else {
                                    data = null;
                                }
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
                                    _history.Add(Now + "Send MO Confirmation: " + Wbs.Utilities.CustomConvert.GetHex(iridum.PackageContent));
                                }
                                catch (Exception send)
                                {
                                    HandleOnMessage(format("{0} Send iridiu package error: {1}, Trace: {2}", Now, send.Message, send.StackTrace));
                                }
                                AddQueue(iridum.PackageContent);
                                //HandleOnMessage(Now + "MO confirmation: " + Wbs.Utilities.CustomConvert.GetHex(iridum.PackageContent));
                                iridum = null;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        HandleOnMessage(format("{0} HandleIridiumPackage error: {1}, Trace: {2}", Now, e.Message, e.StackTrace));
                    }
                    finally
                    {
                        obj.Dispose();
                        obj = null;
                    }
                    // 检测是否可以显示当前分析的数据包
                    string text = "";
                    lock (lock_momsn)
                    {
                        if (!_showPackage)
                        {
                            var m = MOMSNs.FirstOrDefault(f => f.mtmsn == momsn);
                            if (null != m)
                            {
                                showPayload = true;
                                m.HandleTimes++;
                            }
                        }
                        else
                        {
                            // 如果显示数据包数据则直接显示
                            showPayload = true;
                        }
                        if (showPayload)
                        {
                            foreach (var s in _history)
                                text += s + Environment.NewLine;
                        }
                        // 移除超时的MTMSN监控记录
                        MOMSNs.RemoveAll(r => r.HandleTimes >= 3 || r.time < DateTime.Now.AddDays(-7));
                    }
                    if(!string.IsNullOrEmpty(text))
                        HandleOnMessage(text);

                    // 清空分析的历史记录
                    _history.Clear();
                }
            }
        }
    }
    public class StateObject
    {
        public Socket socket = null;
        public IPEndPoint point = null;
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
