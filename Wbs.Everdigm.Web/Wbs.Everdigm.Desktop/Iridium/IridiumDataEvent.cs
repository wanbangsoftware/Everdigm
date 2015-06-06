using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wbs.Everdigm.Desktop
{
    public enum IridiumDataType
    {
        /// <summary>
        /// 服务器的MT发送状态回执
        /// </summary>
        MTServerSendStatus,
        /// <summary>
        /// 铱星模块发送回来的MO数据
        /// </summary>
        MOPayload,
        /// <summary>
        /// 铱星模块的MT接收状态回执
        /// </summary>
        MTModelReceiveStatus
    }
    /// <summary>
    /// 铱星数据包
    /// </summary>
    public class IridiumData : IDisposable
    {
        /// <summary>
        /// 数据类别
        /// </summary>
        public IridiumDataType Type { get; set; }
        /// <summary>
        /// IMEI终端号码
        /// </summary>
        public string IMEI { get; set; }
        /// <summary>
        /// 数据收到的时间
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public short Status { get; set; }
        /// <summary>
        /// 数据长度
        /// </summary>
        public int Length { get; private set; }
        private byte[] payload = null;
        /// <summary>
        /// 数据内容
        /// </summary>
        public byte[] Payload
        {
            get { return payload; }
            set
            {
                if (null == value)
                {
                    payload = null;
                    Length = 0;
                }
                else
                {
                    Length = value.Length;
                    payload = new byte[value.Length];
                    Buffer.BlockCopy(value, 0, payload, 0, Length);
                }
            }
        }
        /// <summary>
        /// 发送方流水号码
        /// </summary>
        public ushort MTMSN { get; set; }

        public void Dispose()
        {
            payload = null;
        }
    }
    /// <summary>
    /// 接收到铱星Payload数据的事件处理
    /// </summary>
    public class IridiumDataEvent : EventArgs
    {
        /// <summary>
        /// 铱星数据包
        /// </summary>
        public IridiumData Data { get; set; }
    }
}
