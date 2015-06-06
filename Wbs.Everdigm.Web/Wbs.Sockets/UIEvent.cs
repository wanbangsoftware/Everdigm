using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wbs.Sockets
{
    public class UIEventArgs : EventArgs
    {
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
    }
}
