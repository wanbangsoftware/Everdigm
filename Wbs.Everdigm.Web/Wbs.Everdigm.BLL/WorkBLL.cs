using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.BLL
{
    /// <summary>
    /// 工作的BLL
    /// </summary>
    public class WorkBLL:BaseService<TB_Work>
    {
        public WorkBLL()
            : base(new BaseRepository<TB_Work>())
        { }


        public override TB_Work GetObject()
        {
            return new TB_Work()
            {
                Deleted = false,
                Description = "",
                Director = "",
                RealEnd = (DateTime?)null,
                RealStart = (DateTime?)null,
                RegisterTime = DateTime.Now,
                ScheduleEnd = (DateTime?)null,
                ScheduleStart = (DateTime?)null,
                Title = "",
                Tracker = (int?)null,
                id = 0
            };
        }

        public override string ToString(TB_Work entity)
        {
            return string.Format("Title: {0}", entity.Title);
        }
    }
}
