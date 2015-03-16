using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.BLL
{
    /// <summary>
    /// 设备数据历史记录业务处理逻辑
    /// </summary>
    public class DataBLL : BaseService<TB_HISTORIES>
    {
        public DataBLL()
            : base(new BaseRepository<TB_HISTORIES>())
        { }

        public override TB_HISTORIES GetObject()
        {
            return new TB_HISTORIES();
        }

        public override string ToString(TB_HISTORIES entity)
        {
            return "";
        }
    }
}
