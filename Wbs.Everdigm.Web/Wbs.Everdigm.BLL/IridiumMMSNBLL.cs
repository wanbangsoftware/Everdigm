using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.BLL
{
    /// <summary>
    /// 设置铱星服务器下发时的MTMSN
    /// </summary>
    public class IridiumMMSNBLL : BaseService<TB_IridiumMTMSN>
    {
        public IridiumMMSNBLL()
            : base(new BaseRepository<TB_IridiumMTMSN>())
        { }

        public override TB_IridiumMTMSN GetObject()
        {
            var now = DateTime.Now;
            return new TB_IridiumMTMSN()
            {
                Year = (short)now.Year,
                Month = (byte)now.Month,
                Day = (byte)now.Day,
                Number = 0
            };
        }

        public override string ToString(TB_IridiumMTMSN entity)
        {
            return string.Format("{0}-{1}: {2}", entity.Year, entity.Month, entity.Number);
        }
    }
}
