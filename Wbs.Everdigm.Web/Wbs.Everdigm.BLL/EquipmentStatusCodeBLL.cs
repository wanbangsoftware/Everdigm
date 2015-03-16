using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.BLL
{
    /// <summary>
    /// 设备所处状态码业务处理
    /// </summary>
    public class EquipmentStatusCodeBLL : BaseService<TB_EquipmentStatusCode>
    {
        public EquipmentStatusCodeBLL()
            : base(new BaseRepository<TB_EquipmentStatusCode>())
        { }
        /// <summary>
        /// 生成一个新的设备状态码实例
        /// </summary>
        /// <returns></returns>
        public override TB_EquipmentStatusCode GetObject()
        {
            return new TB_EquipmentStatusCode() { Code = "", Name = "", Status = (int?)null, id = 0 };
        }
        public override string ToString(TB_EquipmentStatusCode entity)
        {
            return string.Format("id: {0}, name: {1}", entity.id, entity.Name);
        }
    }
}
