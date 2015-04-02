using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.BLL
{
    /// <summary>
    /// 仓库信息业务处理
    /// </summary>
    public class WarehouseBLL:BaseService<TB_Warehouse>
    {
        /// <summary>
        /// 生成一个新的仓库信息业务处理逻辑实体
        /// </summary>
        public WarehouseBLL()
            : base(new BaseRepository<TB_Warehouse>())
        { }
        /// <summary>
        /// 生成一个新的仓库信息实例
        /// </summary>
        /// <returns></returns>
        public override TB_Warehouse GetObject()
        {
            return new TB_Warehouse { Address = "", Code = "", Name = "", id = 0, Delete = false };
        }
        public override string ToString(TB_Warehouse entity)
        {
            return string.Format("id: {0}, name: {1}, code: {2}", entity.id, entity.Name, entity.Code);
        }
    }
}
