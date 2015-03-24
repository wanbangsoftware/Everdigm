using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wbs.Sockets
{
    /// <summary>
    /// 服务器接收到的数据包类型
    /// </summary>
    public enum AsyncDataPackageType : byte
    {
        /// <summary>
        /// TCP数据包
        /// </summary>
        TCP = 0x00,
        /// <summary>
        /// UDP数据包
        /// </summary>
        UDP = 0x10
    }
}
