using System;

using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.BLL
{
    /// <summary>
    /// 卫星模块业务处理逻辑
    /// </summary>
    public class SatelliteBLL : BaseService<TB_Satellite>
    {
        public SatelliteBLL()
            : base(new BaseRepository<TB_Satellite>())
        { }

        /// <summary>
        /// 生成一个新的空记录
        /// </summary>
        /// <returns></returns>
        public override TB_Satellite GetObject()
        {
            return new TB_Satellite()
            {
                Bound = false,
                LabelPrinted = 0,
                LabelPrintSchedule = (DateTime?)null,
                LabelPrintStatus = 0,
                CardNo = "",
                FWVersion = "",
                ManufactureDate = "",
                Manufacturer = "",
                PcbNumber = "",
                RatedVoltage = "",
                RegisterDate = DateTime.Now
            };
        }
        /// <summary>
        /// 显示字符串
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string ToString(TB_Satellite entity)
        {
            return string.Format("id: {0}, card: {1}, bound: {2}", entity.id, entity.CardNo, entity.Bound);
        }
    }
}
