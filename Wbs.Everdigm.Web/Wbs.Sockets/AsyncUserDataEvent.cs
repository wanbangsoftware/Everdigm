using System;
using System.Collections.Generic;
using System.Text;

namespace Wbs.Sockets
{
    /// <summary>
    /// 用户数据消息
    /// </summary>
    public class AsyncUserDataEvent : EventArgs, IDisposable
    {
        /// <summary>
        /// 用户数据
        /// </summary>
        public AsyncUserDataBuffer Data { get; set; }
        /// <summary>
        /// 实例化一个用户数据消息体
        /// </summary>
        public AsyncUserDataEvent()
        {
            Data = null;
        }
        /// <summary>
        /// 实例化一个用户数据消息体并指定数据内容
        /// </summary>
        /// <param name="data"></param>
        public AsyncUserDataEvent(AsyncUserDataBuffer data)
        {
            Data = data;
        }
        ~AsyncUserDataEvent()
        {
            Dispose();
        }
        /// <summary>
        /// 销毁消息体所占用的资源
        /// </summary>
        public void Dispose()
        {
            if (null != Data)
            {
                Data.Buffer = null;
                Data.IP = null;
                Data = null;
            }
        }
    }
}
