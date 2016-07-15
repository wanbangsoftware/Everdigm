using System;
using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.BLL
{
    public class ExcelHandlerBLL : BaseService<TB_ExcelHandler>
    {
        public ExcelHandlerBLL()
            : base(new BaseRepository<TB_ExcelHandler>())
        { }

        public override TB_ExcelHandler GetObject()
        {
            return new TB_ExcelHandler()
            {
                CreateDate = DateTime.Now,
                Deleted = false,
                Handled = false,
                Source = "",
                Target = "",
                Type = 0,
                Work = null,
                id = 0,
                Status = 0,
                Data = "",
                EndDate = "",
                Equipment = null,
                StartDate = ""
            };
        }

        public override string ToString(TB_ExcelHandler entity)
        {
            return "";
        }
    }
}
