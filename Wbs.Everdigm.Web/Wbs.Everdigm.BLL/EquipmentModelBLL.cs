using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.BLL
{
    /// <summary>
    /// 设备型号业务处理逻辑
    /// </summary>
    public class EquipmentModelBLL : BaseService<TB_EquipmentModel>
    {
        /// <summary>
        /// 实例化一个新的设备型号业务处理实体
        /// </summary>
        public EquipmentModelBLL()
            : base(new BaseRepository<TB_EquipmentModel>())
        { }
        /// <summary>
        /// 生成一个新的设备型号实体
        /// </summary>
        /// <returns></returns>
        public override TB_EquipmentModel GetObject()
        {
            return new TB_EquipmentModel { Code = "", id = 0, Name = "", Type = (int?)null, Delete = false };
        }
        public override string ToString(TB_EquipmentModel entity)
        {
            return string.Format("id: {0}, name: {1}, code: {2}", entity.id, entity.Name, entity.Code);
        }
    }
}
