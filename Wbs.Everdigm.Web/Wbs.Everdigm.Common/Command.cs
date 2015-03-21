using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wbs.Everdigm.Common
{
    /// <summary>
    /// 服务器上可发的命令
    /// </summary>
    public class Command
    {
        /// <summary>
        /// 命令的描述语言
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 命令的标记
        /// </summary>
        public string Flag { get; set; }
        /// <summary>
        /// 命令的代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 附加参数
        /// </summary>
        public string Param { get; set; }
        /// <summary>
        /// 是否为安保状态的命令
        /// </summary>
        public bool Security { get; set; }
        /// <summary>
        /// 命令的内容
        /// </summary>
        public string Content { get; set; }
    }
}
