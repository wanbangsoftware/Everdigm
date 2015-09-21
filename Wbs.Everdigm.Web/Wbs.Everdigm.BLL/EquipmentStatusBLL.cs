using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.BLL
{
    /// <summary>
    /// 设备所处状态
    /// </summary>
    public class EquipmentStatusBLL : BaseService<TB_EquipmentStatusName>
    {
        public EquipmentStatusBLL()
            : base(new BaseRepository<TB_EquipmentStatusName>())
        { }
        /// <summary>
        /// 生成一个新的设备状态类实例
        /// </summary>
        /// <returns></returns>
        public override TB_EquipmentStatusName GetObject()
        {
            return new TB_EquipmentStatusName()
            {
                Code = "",
                Name = "",
                id = 0,
                IsItInventory = false,
                IsItOutstorage = false,
                IsItOverhaul = false,
                IsItWaiting = false,
                IsItRental = false,
                IsItTesting = false
            };
        }
        public override string ToString(TB_EquipmentStatusName entity)
        {
            return string.Format("id: {0}, name: {1}, code: {2}", entity.id, entity.Name, entity.Code);
        }
    }
}
