using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wbs.Everdigm.Web
{
    /// <summary>
    /// 设备类型
    /// </summary>
    public enum EquipmentFunctional : byte
    {
        /// <summary>
        /// 机械式挖掘机
        /// </summary>
        Mechanical = 10,
        /// <summary>
        /// 电子式挖掘机
        /// </summary>
        Electric = 11,
        /// <summary>
        /// 装载机
        /// </summary>
        Loader = 20
    }
}