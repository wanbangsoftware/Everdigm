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
        /// 命令相关业务处理逻辑实体
        /// </summary>
        private CommandBLL CommandInstance { get { return new CommandBLL(); } }
        /// <summary>
        /// 设备信息相关业务处理逻辑
        /// </summary>
        private EquipmentBLL EquipmentInstance { get { return new EquipmentBLL(); } }
    }
}