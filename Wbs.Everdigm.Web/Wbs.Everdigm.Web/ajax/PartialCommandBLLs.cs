using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Wbs.Everdigm.BLL;

namespace Wbs.Everdigm.Web.ajax
{
    /// <summary>
    /// 命令相关的BLL
    /// </summary>
    public partial class command
    {
        /// <summary>
        /// 数据历史记录处理
        /// </summary>
        private DataBLL DataInstance { get { return new DataBLL(); } }
        /// <summary>
        /// 命令相关业务处理逻辑实体
        /// </summary>
        private CommandBLL CommandInstance { get { return new CommandBLL(); } }
        /// <summary>
        /// 设备信息相关业务处理逻辑
        /// </summary>
        private EquipmentBLL EquipmentInstance { get { return new EquipmentBLL(); } }
        /// <summary>
        /// 终端信息业务处理逻辑
        /// </summary>
        private TerminalBLL TerminalInstance { get { return new TerminalBLL(); } }
    }
}