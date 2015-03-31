using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.BLL
{
    /// <summary>
    /// 设备类型业务处理
    /// </summary>
    public class EquipmentTypeBLL : BaseService<TB_EquipmentType>
    {
        public EquipmentTypeBLL()
            : base(new BaseRepository<TB_EquipmentType>())
        { }
        /// <summary>
        /// 生成一个新的设备类型实例
        /// </summary>
        /// <returns></returns>
        public override TB_EquipmentType GetObject()
        {
            return new TB_EquipmentType { Code = "", id = 0, Name = "", Delete = false };
        }
        public override string ToString(TB_EquipmentType entity)
        {
            return string.Format("id: {0}, name: {1}, code: {2}", entity.id, entity.Name, entity.Code);
        }
    }
}
