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
                Deleted = false,
                BookedTerminal = (int?)null,
                Type = 0,
                Details = "",
                Equipment = (int?)null,
                id = 0,
                Priority = 0,
                Work = (int?)null
            };
        }

        public override string ToString(TB_WorkDetail entity)
        {
            return string.Format("{0} for {1}, terminal: {2}", GetWorkType(entity.Type.Value),
                GetEquipmentNumber(entity), ((int?)null == entity.BookedTerminal ? "" : entity.TB_Terminal.Number));
        }

        private string GetEquipmentNumber(TB_WorkDetail entity)
        {
            return entity.TB_Equipment.TB_EquipmentStatusName.Code + entity.TB_Equipment.Number;
        }
        /// <summary>
        /// 获取工作的类别
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetWorkType(byte type)
        {
            var ret = "";
            switch (type)
            {
                case 0: ret = "Install terminal"; break;
                case 1: ret = "Displace terminal"; break;
                case 2: ret = "Equipment Service"; break;
            }
            return ret;
        }
    }
}
