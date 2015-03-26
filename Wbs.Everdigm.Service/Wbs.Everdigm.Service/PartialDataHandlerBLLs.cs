using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Wbs.Everdigm.BLL;

namespace Wbs.Everdigm.Service
{
    /// <summary>
    /// DataHandler的BLL集合
    /// </summary>
    public partial class DataHandler
    {
        /// <summary>
        /// 异常处理逻辑
        /// </summary>
        private ErrorBLL ErrorInstance { get { return new ErrorBLL(); } }
        /// <summary>
        /// 设备的BLL
        /// </summary>
        private EquipmentBLL EquipmentInstance { get { return new EquipmentBLL(); } }
        /// <summary>
        /// 终端的BLL
        /// </summary>
        private TerminalBLL TerminalInstance { get { return new TerminalBLL(); } }
        /// <summary>
        /// TX数据历史记录的BLL
        /// </summary>
        private DataBLL DataInstance { get { return new DataBLL(); } }
        /// <summary>
        /// 命令的BLL
        /// </summary>
        private CommandBLL CommandInstance { get { return new CommandBLL(); } }
        /// <summary>
        /// 位置信息记录的BLL
        /// </summary>
        private PositionBLL PositionInstance { get { return new PositionBLL(); } }
    }
}