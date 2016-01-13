
using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.BLL
{
    public class ActionBLL : BaseService<TB_AccountAction>
    {
        public ActionBLL()
            : base(new BaseRepository<TB_AccountAction>())
        { }

        public override TB_AccountAction GetObject()
        {
            return new TB_AccountAction
            {
                Description = "",
                Name = "",
                id = 0
            };
        }
        public override string ToString(TB_AccountAction entity)
        {
            return string.Format("id: {0}, name: {1}", entity.id, entity.Name);
        }
    }
}
