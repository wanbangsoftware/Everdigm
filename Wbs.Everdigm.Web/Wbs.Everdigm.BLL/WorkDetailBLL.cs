using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.BLL
{
    /// <summary>
    /// 工作详情
    /// </summary>
    public class WorkDetailBLL : BaseService<TB_WorkDetail>
    {
        public WorkDetailBLL()
            : base(new BaseRepository<TB_WorkDetail>())
        { }

        public override TB_WorkDetail GetObject()
        {
            return new TB_WorkDetail()
            {
                BookedTerminal = (int?)null,
                Details = "",
                Equipment = (int?)null,
                id = 0,
                Priority = 0,
                Work = (int?)null
            };
        }

        public override string ToString(TB_WorkDetail entity)
        {
            return string.Format("Details: {0}", entity.Details);
        }
    }
}
