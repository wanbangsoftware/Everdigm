using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.BLL
{
    public class IririumMTMSNBLL : BaseService<TB_IridiumMTMSN>
    {
        public IririumMTMSNBLL()
            : base(new BaseRepository<TB_IridiumMTMSN>())
        { }

        public override TB_IridiumMTMSN GetObject()
        {
            return new TB_IridiumMTMSN()
            {
                Year = 0,
                Month = 0,
                Day = 0,
                Number = 0
            };
        }

        public override string ToString(TB_IridiumMTMSN entity)
        {
            return string.Format("{0}/{1}/{2}: {3}", entity.Year, entity.Month, entity.Day, entity.Number);
        }
    }
}
