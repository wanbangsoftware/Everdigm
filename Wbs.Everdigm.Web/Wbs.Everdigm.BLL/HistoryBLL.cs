using System;

using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.BLL
{
    public class HistoryBLL : BaseService<TB_AccountHistory>
    {
        public HistoryBLL()
            : base(new BaseRepository<TB_AccountHistory>())
        { }

        public override TB_AccountHistory GetObject()
        {
            return new TB_AccountHistory
            {
                Account = 0,
                ActionId = 0,
                ActionTime = DateTime.Now,
                Ip = "",
                ObjectA = "",
                ObjectB = "",
                ObjectC = ""
            };
        }
        public override string ToString(TB_AccountHistory entity)
        {
            return "";
        }
    }
}
