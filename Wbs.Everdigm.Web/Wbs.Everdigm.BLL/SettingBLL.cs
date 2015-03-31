using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.BLL
{
    /// <summary>
    /// 基本设定
    /// </summary>
    public class SettingBLL : BaseService<TB_Setting>
    {
        public SettingBLL()
            : base(new BaseRepository<TB_Setting>())
        { }

        public override TB_Setting GetObject()
        {
            return new TB_Setting() { ColumnName = "", ColumnValue = "", AddTime = DateTime.Now };
        }

        public override string ToString(TB_Setting entity)
        {
            return string.Format("id: {0}, name: {1}, value: {2}", entity.id, entity.ColumnName, entity.ColumnValue);
        }
    }
}
