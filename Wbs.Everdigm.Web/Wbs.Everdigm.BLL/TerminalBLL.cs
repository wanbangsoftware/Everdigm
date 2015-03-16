using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                Delete = false,
                Firmware = "",
                Number = "",
                ProductionDate = DateTime.Now,
                Revision = 1,
                Satellite = "",
                Sim = "",
                Type = "DX"
            };
        }
        /// <summary>
        /// 将终端实例显示为字符串
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string ToString(TB_Terminal entity)
        {
            return string.Format("id: {0}, No.: {1}, Sim: {2}, Sat: {3}, Type: {4}, F/W: {5}, Rev.: {6}",
                entity.id, entity.Number, entity.Sim, entity.Satellite, entity.Type, entity.Firmware, entity.Revision);
        }
    }
}
