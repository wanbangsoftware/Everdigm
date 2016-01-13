using System;

using Wbs.Protocol;
using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.BLL
{
    /// <summary>
    /// 终端信息业务处理
    /// </summary>
    public class TerminalBLL : BaseService<TB_Terminal>
    {
        public TerminalBLL()
            : base(new BaseRepository<TB_Terminal>())
        { }
        /// <summary>
        /// 生成一个新的终端信息实例
        /// </summary>
        /// <returns></returns>
        public override TB_Terminal GetObject()
        {
            return new TB_Terminal()
            {
                id = 0,
                HasBound = false,
                // 加入卫星功能确认 2015/09/18 14:00
                SatelliteStatus = false,
                Delete = false,
                Booked = false,
                Firmware = "",
                Number = "",
                ProductionDate = DateTime.Now,
                Revision = 1,
                Satellite = (int?)null,
                OnlineStyle = 0,
                OnlineTime = (DateTime?)null,
                Socket = 0,
                Sim = "",
                Type = TerminalTypes.DX
            };
        }
        /// <summary>
        /// 获取卫星信息
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="wantAdd">true=要添加卫星信息</param>
        /// <returns></returns>
        public string GetSatellite(TB_Terminal obj, bool wantAdd)
        {
            var n = (int?)null;
            if (n == obj.Satellite) {
                return wantAdd ? "<a href=\"#add_" + obj.id + "\">add</a>" : "-";
            }
            return obj.TB_Satellite.CardNo + (wantAdd ? (" (<a href=\"#del_" + obj.id + "\">Unbind</a>)") : "");
        }
        /// <summary>
        /// 将终端实例显示为字符串
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string ToString(TB_Terminal entity)
        {
            return string.Format("id: {0}, No.: {1}, Sim: {2}, Sat: {3}",
                entity.id, entity.Number, entity.Sim, ((int?)null == entity.Satellite ? "-" : entity.TB_Satellite.CardNo));
        }
    }
}
