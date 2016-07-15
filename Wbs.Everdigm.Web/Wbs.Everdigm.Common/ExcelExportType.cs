using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wbs.Everdigm.Common
{
    /// <summary>
    /// Excel导出类型
    /// </summary>
    public enum ExcelExportType : byte
    {
        /// <summary>
        /// TMS工作指派单
        /// </summary>
        TMSWork,
        /// <summary>
        /// 设备日工作时间
        /// </summary>
        DailyWork,
        /// <summary>
        /// 设备列表
        /// </summary>
        Equipments,
        /// <summary>
        /// 终端列表
        /// </summary>
        Terminals
    }
}
