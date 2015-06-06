using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.BLL
{
    /// <summary>
    /// 铱星数据收发流量统计
    /// </summary>
    public class IririumFlowBLL : BaseService<TB_IridiumFlow>
    {
        public IririumFlowBLL()
            : base(new BaseRepository<TB_IridiumFlow>())
        { }

        public override TB_IridiumFlow GetObject()
        {
            return new TB_IridiumFlow()
            {
                Iridium = (int?)null,
                Monthly = int.Parse(DateTime.Now.ToString("yyyyMM")),
                MOPayload = 0,
                MOTimes = 0,
                MTPayload = 0,
                MTTimes = 0
            };
        }

        public override string ToString(TB_IridiumFlow entity)
        {
            return string.Format("{0}: {1}, MO: {2} times, data: {3}, MT: {4} times, data: {5}",
                entity.TB_Satellite.CardNo, entity.Monthly, entity.MOTimes, entity.MOPayload, entity.MTTimes, entity.MTPayload);
        }
    }
}
