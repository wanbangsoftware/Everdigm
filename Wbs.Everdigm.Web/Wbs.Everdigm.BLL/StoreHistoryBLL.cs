using System;
using System.Linq;

using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.BLL
{
    /// <summary>
    /// 设备出入库历史记录业务处理
    /// </summary>
    public class StoreHistoryBLL : BaseService<TB_EquipmentStockHistory>
    {
        /// <summary>
        /// 生成一个新的出入库历史记录业务处理实体
        /// </summary>
        public StoreHistoryBLL()
            : base(new BaseRepository<TB_EquipmentStockHistory>())
        { }
        /// <summary>
        /// 查找与指定设备指定入库次数中的初入库信息记录
        /// </summary>
        /// <param name="equipment"></param>
        /// <param name="status"></param>
        /// <param name="storeTimes"></param>
        /// <param name="storeIn">true=入库，false=出库</param>
        /// <returns></returns>
        public TB_EquipmentStockHistory GetStoreInfo(int equipment, int storeTimes, bool storeIn)
        {
            // 获取相同出入库次数中最先出入库的记录
            var list = FindList<TB_EquipmentStockHistory>(f => f.Equipment == equipment && f.StoreTimes == storeTimes &&
                (storeIn ? (f.TB_EquipmentStatusName.IsItInventory == true || f.TB_EquipmentStatusName.IsItOverhaul == true) :
                (f.TB_EquipmentStatusName.IsItOutstorage == true || f.TB_EquipmentStatusName.IsItRental == true)), "Savetime", !storeIn);
            return list.FirstOrDefault();
        }
        /// <summary>
        /// 获取库存状态信息
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string GetStatus(TB_EquipmentStockHistory obj)
        {
            if (null == obj) return "-";
            return obj.TB_EquipmentStatusName.Code;
        }
        /// <summary>
        /// 获取设备库存状态信息描述
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string GetStatusTitle(TB_EquipmentStockHistory obj)
        {
            if (null == obj) return "-";
            return obj.TB_EquipmentStatusName.Name;
        }
        /// <summary>
        /// 生成一个空的出入库历史记录实例
        /// </summary>
        /// <returns></returns>
        public override TB_EquipmentStockHistory GetObject()
        {
            return new TB_EquipmentStockHistory()
            {
                id = 0,
                Equipment = null,
                Status = null,
                StockNumber = "",
                Savetime = DateTime.Now,
                Stocktime = DateTime.Now,
                StoreTimes = 0,
                Warehouse = null
            };
        }
        public override string ToString(TB_EquipmentStockHistory entity)
        {
            return string.Format("{0},{1},{2},{3},{4}", entity.Stocktime.Value.ToString("yyyy/MM/dd"),
                (entity.TB_Equipment.TB_EquipmentModel.Code + entity.TB_Equipment.Number),
                (entity.TB_EquipmentStatusName.Name),
                entity.StoreTimes, entity.TB_Warehouse.Name);
        }
    }
}
