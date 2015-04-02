using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Satellite
{
    /// <summary>
    /// 从服务器上获取下来的需要发送的命令
    /// </summary>
    public class Command
    {
        /// <summary>
        /// 状态0=没有命令1=有命令
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 命令的id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 目的号码
        /// </summary>
        public string Destination { get; set; }
        /// <summary>
        /// 命令内容
        /// </summary>
        public string Content { get; set; }
    }
}
