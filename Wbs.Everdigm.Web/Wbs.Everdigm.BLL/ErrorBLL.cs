using System;

using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.BLL
{
    /// <summary>
    /// 程序异常的业务处理
    /// </summary>
    public class ErrorBLL:BaseService<TB_Error>
    {
        public ErrorBLL()
            : base(new BaseRepository<TB_Error>())
        { }

        public override TB_Error GetObject()
        {
            return new TB_Error() { ErrorData = "", ErrorMessage = "", ErrorTime = DateTime.Now };
        }

        public override string ToString(TB_Error entity)
        {
            return "";
        }
    }
}
