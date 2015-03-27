using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;

namespace Wbs.Sockets
{
    /// <summary>
    /// 用户连接节点
    /// </summary>
    public class AsyncUserToken : IDisposable, IEquatable<AsyncUserToken>, IComparer<AsyncUserToken>
    {
        private Socket aut_socket;
        private string aut_ip;
        private int aut_port;
        private int aut_handle;
        /// <summary>
        /// 实例化一个新的用户数据结构体并指定用户节点所用的 socket 。
        /// </summary>
        public AsyncUserToken(Socket s)
        {
            //aut_socket = s;
            Socket = s;
        }
        /// <summary>
        /// 实例化一个新的空的用户数据结构体。
        /// </summary>
        public AsyncUserToken()
        {
            aut_socket = null;
        }
        /// <summary>
        /// 显示字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}:{1}(Socket: {2})", aut_ip, aut_port, aut_handle);
        }
        /// <summary>
        /// 获取或设置用户节点绑定的 Socket 句柄。
        /// </summary>
        public Socket Socket
        {
            set
            {
                aut_socket = value;
                if (aut_socket != null && aut_socket.Connected)
                {
                    aut_ip = (aut_socket.RemoteEndPoint as System.Net.IPEndPoint).Address.ToString();
                    aut_port = (aut_socket.RemoteEndPoint as System.Net.IPEndPoint).Port;
                    aut_handle = aut_socket.Handle.ToInt32();
                }
            }
            get { return aut_socket; }
        }
        /// <summary>
        /// 获取绑定的用户节点的 IP 地址。
        /// </summary>
        public string IP
        {
            get { return aut_ip; }
            set { aut_ip = value; }
        }
        /// <summary>
        /// 获取绑定的用户节点的端口号码。
        /// </summary>
        public int Port
        {
            get { return aut_port; }
            set { aut_port = value; }
        }
        /// <summary>
        /// 获取绑定的用户节点的句柄。
        /// </summary>
        public int SocketHandle
        {
            get { return aut_handle; }
        }
        /// <summary>
        /// 销毁实体所占资源
        /// </summary>
        public void Dispose()
        {
            aut_socket = null;
            aut_ip = null;
        }
        /// <summary>
        /// 比较指定对象是否与当前用户节点相同
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (null == obj) return false;
            var aut = obj as AsyncUserToken;
            if (null == aut) return false;
            return Equals(aut);
        }
        
        /// <summary>
        /// 比较两个用户节点的IP和Port是否相同
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool Equals(AsyncUserToken token)
        {
            if (null == token) return false;
            if (token.aut_ip.Equals(this.aut_ip) && token.aut_port.Equals(this.aut_port))
                return true;
            return false;//token.SocketHandle.Equals(this.SocketHandle);
        }

        public override int GetHashCode()
        {
            return aut_handle;
        }
        /// <summary>
        /// 比较两个用户节点的socket
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(AsyncUserToken x, AsyncUserToken y)
        {
            return x.aut_handle.CompareTo(y.aut_handle);
        }
    }
}
