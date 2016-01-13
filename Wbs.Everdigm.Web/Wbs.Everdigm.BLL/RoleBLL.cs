using System;
using System.Collections.Generic;

using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.BLL
{
    /// <summary>
    /// 系统用户角色业务处理
    /// </summary>
    public class RoleBLL : BaseService<TB_Role>
    {
        /// <summary>
        /// 生成一个新的用户角色业务处理逻辑实体
        /// </summary>
        public RoleBLL()
            : base(new BaseRepository<TB_Role>())
        { }

        public override TB_Role GetObject()
        {
            return new TB_Role
            {
                AddTime = DateTime.Now,
                Delete = false,
                Description = "",
                IsAdministrator = false,
                IsDefault = false,
                Name = "",
                Permission = ""
            };
        }
        public override string ToString(TB_Role entity)
        {
            return string.Format("id: {0}, name: {1}, isAdmin: {2}, isDefault: {3}",
                entity.id, entity.Name, entity.IsAdministrator, entity.IsDefault);
        }
        /// <summary>
        /// 获取所有角色的id列表
        /// </summary>
        /// <returns></returns>
        public List<int> GetAllRole()
        {
            var list = new List<int>();
            var roles = FindList(f => f.Delete == false);
            foreach (var role in roles)
                list.Add(role.id);
            return list;
        }
    }
}
