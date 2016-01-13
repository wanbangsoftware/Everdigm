using System;
using System.Net.Sockets;

using Wbs.Utilities;

namespace Wbs.Sockets
{
    /// <summary>
    /// 用户数据结构体
    /// </summary>
    public class AsyncUserDataBuffer : IDisposable
    {
        //DateTime audb_receiveTime;
        byte[] audb_buffer;
        //bool audb_isUsed;
        //string audb_ip;
        //int audb_port;
        //int audb_socketHandle;
        //AsyncUserDataType audb_dataType;
        //AsyncDataPackageType audb_packageType = AsyncDataPackageType.TCP;
        /// <summary>
        /// 创建一个新的用户数据结构实例。
        /// </summary>
        public AsyncUserDataBuffer()
        {
            //IsUsed = true;
            init();
            //audb_receiveTime = DateTime.Now;
            //audb_buffer = null;
            //audb_dataType = AsyncUserDataType.None;
            //audb_ip = "";
            //audb_port = 0;
            //audb_socketHandle = 0;
        }
        /// <summary>
        /// 异步接收到消息
        /// </summary>
        /// <param name="args"></param>
        public AsyncUserDataBuffer(SocketAsyncEventArgs args)
        {
            SetDataEvent(args, 0);
        }
        /// <summary>
        /// 组包
        /// </summary>
        /// <param name="args"></param>
        public void ResizeData(SocketAsyncEventArgs args)
        {
            var len = audb_buffer.Length;
            audb_buffer = CustomConvert.expand(audb_buffer, len + args.BytesTransferred);
            System.Buffer.BlockCopy(args.Buffer, args.Offset, audb_buffer, len, args.BytesTransferred);
        }
        /// <summary>
        /// 扩展组包收到的UDP数据
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="len"></param>
        public void ResizeData(byte[] buffer, int len)
        {
            var old = audb_buffer.Length;
            audb_buffer = CustomConvert.expand(audb_buffer, old + len);
            System.Buffer.BlockCopy(buffer, 0, audb_buffer, old, len);
        }
        /// <summary>
        /// 设置接收到的UDP消息内容
        /// </summary>
        /// <param name="token"></param>
        /// <param name="buffer"></param>
        /// <param name="len"></param>
        public void SetDataEvent(string ip, int port, byte[] buffer, int len)
        {
            ReceiveTime = DateTime.Now;
            audb_buffer = new byte[len];
            System.Buffer.BlockCopy(buffer, 0, audb_buffer, 0, len);
            DataType = AsyncUserDataType.ReceivedData;
            IP = ip;
            Port = port;
            SocketHandle = 0;
            PackageType = AsyncDataPackageType.UDP;
        }
        public void SetDataEvent(AsyncUserToken token) {
            ReceiveTime = DateTime.Now;
            audb_buffer = new byte[token.Length];
            System.Buffer.BlockCopy(token.Buffer, 0, audb_buffer, 0, token.Length);
            DataType = AsyncUserDataType.ReceivedData;
            IP = token.IP;
            Port = token.Port;
            SocketHandle = token.SocketHandle;
            PackageType = AsyncDataPackageType.TCP;
        }
        /// <summary>
        /// 设置数据内容
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="type">0=connect, 1=disconnect, 2=data</param>
        public void SetDataEvent(StateObject obj, byte type)
        {
            ReceiveTime = DateTime.Now;
            PackageType = AsyncDataPackageType.TCP;
            IP = obj.point.Address.ToString();
            Port = obj.point.Port;
            SocketHandle = obj.socket.Handle.ToInt32();
            if (type < 2)
            {
                audb_buffer = null;
                DataType = 0 == type ? AsyncUserDataType.ClientConnected : AsyncUserDataType.ClientDisconnected;
            }
            else
            {
                audb_buffer = new byte[obj.length];
                System.Buffer.BlockCopy(obj.Received, 0, audb_buffer, 0, obj.length);
                DataType = AsyncUserDataType.ReceivedData;
            }
        }
        /// <summary>
        /// 设置异步接收到的消息内容
        /// </summary>
        /// <param name="args"></param>
        /// <param name="type">小于0为连接断开事件，大于0为连接事件，等于0为收到数据事件</param>
        public void SetDataEvent(SocketAsyncEventArgs args, int type)
        {
            ReceiveTime = DateTime.Now;
            if (type == 0)
            {
                audb_buffer = new byte[args.BytesTransferred];
                System.Buffer.BlockCopy(args.Buffer, args.Offset, audb_buffer, 0, args.BytesTransferred);
            }
            else
            {
                audb_buffer = null;
            }
            DataType = (0 == type ? AsyncUserDataType.ReceivedData :
                (0 > type ? AsyncUserDataType.ClientDisconnected : 
                AsyncUserDataType.ClientConnected));
            IP = (args.UserToken as AsyncUserToken).IP;
            Port = (args.UserToken as AsyncUserToken).Port;
            SocketHandle = (args.UserToken as AsyncUserToken).SocketHandle;
        }
        /// <summary>
        /// 客户端连接或断开事件
        /// </summary>
        /// <param name="args"></param>
        /// <param name="connected">标记是否连接</param>
        public AsyncUserDataBuffer(SocketAsyncEventArgs args, bool connected)
        {
            SetDataEvent(args, connected);
        }
        /// <summary>
        /// 设置事件内容
        /// </summary>
        /// <param name="args"></param>
        /// <param name="connected">标记是否连接事件</param>
        public void SetDataEvent(SocketAsyncEventArgs args, bool connected)
        {
            SetDataEvent(args, (connected ? 1 : -1));
        }
        ~AsyncUserDataBuffer()
        {
            Dispose();
        }
        /// <summary>
        /// 销毁类实例。
        /// </summary>
        public void Dispose()
        {
            audb_buffer = null;
        }
        /// <summary>
        /// 数据的接收时间。
        /// </summary>
        public DateTime ReceiveTime { get; set; }
        /// <summary>
        /// 数据缓冲区。
        /// </summary>
        public byte[] Buffer
        {
            get { return audb_buffer; }
            set { audb_buffer = value; }
        }
        /// <summary>
        /// 初始化所有数据。
        /// </summary>
        private void init()
        {
            audb_buffer = null;
            IP = "";
            Port = 0;
            ReceiveTime = DateTime.Now;
            DataType = AsyncUserDataType.None;
            SocketHandle = 0;
            PackageType = AsyncDataPackageType.TCP;
        }
        /// <summary>
        /// 获取或设定一个值，表示本数据已经被服务器处理过或者正在队列中等候处理。
        /// </summary>
        //public bool IsUsed
        //{
        //    get { return audb_isUsed; }
        //    set
        //    {
        //        audb_isUsed = value;
        //        if (audb_isUsed)
        //        {
        //            init();
        //        }
        //    }
        //}
        /// <summary>
        /// 本缓冲区中的数据类型。
        /// </summary>
        public AsyncUserDataType DataType { get; set; }
        /// <summary>
        /// 本缓冲区中数据的数据包类型（UDP或TCP）
        /// </summary>
        public AsyncDataPackageType PackageType { get; set; }
        /// <summary>
        /// 发送本数据包的客户端绑定 IP 地址。
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 发送本数据包的客户端绑定端口。
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// 发送本数据包的客户端 socket 句柄。
        /// </summary>
        public int SocketHandle { get; set; }
    }
}
