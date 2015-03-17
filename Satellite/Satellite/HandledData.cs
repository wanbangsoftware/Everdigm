using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Satellite
{
    /// <summary>
    /// 已处理完毕的数据
    /// </summary>
    public class HandledData : IDisposable
    {
        public HandledData()
        {
            Data = null;
            Message = new List<string>();
        }
        /// <summary>
        /// 收到的卫星数据包
        /// </summary>
        public SatellitePackage Data { get; set; }
        /// <summary>
        /// 消息记录
        /// </summary>
        public List<string> Message { get; set; }
        /// <summary>
        /// 销毁
        /// </summary>
        public void Dispose()
        {
            Message.Clear();
            Message = null;
            Data = null;
        }
        /// <summary>
        /// 析构
        /// </summary>
        ~HandledData()
        {
            Dispose();
        }
    }
}
