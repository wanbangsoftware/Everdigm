using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Satellite
{
    /// <summary>
    /// 数据类别
    /// </summary>
    public enum DataType
    { 
        /// <summary>
        /// 接收到数据
        /// </summary>
        Received,
        /// <summary>
        /// 发送数据
        /// </summary>
        Send,
        /// <summary>
        /// 空
        /// </summary>
        Null
    }
    /// <summary>
    /// 数据包
    /// </summary>
    public class DataPackage : IDisposable
    {
        private byte[] _data = null;
        /// <summary>
        /// 生成一个新的数据包实例
        /// </summary>
        public DataPackage()
        {
            Time = DateTime.Now;
            Type = DataType.Null;
        }
        /// <summary>
        /// 生成一个新的数据包实体并指定数据内容
        /// </summary>
        /// <param name="data"></param>
        public DataPackage(byte[] data)
        {
            Time = DateTime.Now;
            Data = data;
        }
        /// <summary>
        /// 设置或获取数据内容
        /// </summary>
        public byte[] Data
        {
            get { return _data; }
            set
            {
                if (null != value)
                {
                    var len = value.Length;
                    _data = new byte[len];
                    Buffer.BlockCopy(value, 0, _data, 0, len);
                }
                else
                    _data = null;
            }
        }
        /// <summary>
        /// 数据类别
        /// </summary>
        public DataType Type { get; set; }
        /// <summary>
        /// 数据接收时间
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// 析构
        /// </summary>
        ~DataPackage()
        {
            Dispose();
        }
        /// <summary>
        /// 销毁
        /// </summary>
        public void Dispose()
        {
            _data = null;
        }
    }
}
