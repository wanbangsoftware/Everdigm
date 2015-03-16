using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.BLL
{
    /// <summary>
    /// 权限菜单的业务处理
    /// </summary>
    public class PermissionBLL : BaseService<TB_Permission>
    {
        public PermissionBLL()
            : base(new BaseRepository<TB_Permission>())
        { }

        public override TB_Permission GetObject()
        {
            return new TB_Permission
            {
                AddTime = DateTime.Now,
                Delete = false,
                Description = "",
                DisplayOrder = 0,
                Image = "",
                IsDefault = false,
                Name = "",
                Parent = 0,
                Url = ""
            };
        }
        public override string ToString(TB_Permission entity)
        {
            return string.Format("id: {0}, name: {1}, isDefault: {2}, url: {3}", 
                entity.id, entity.Name, entity.IsDefault, entity.Url);
        }
        /// <summary>
        /// 获取可以公开访问的菜单项id列表
        /// </summary>
        /// <returns></returns>
        public string GetDefaultMenus()
        {
            var list = FindList(f => f.IsDefault == true && f.Delete == false);
            var ret = "";
            foreach (var m in list)
                ret += ("" == ret ? "" : ",") + m.id.ToString();
            return ret;
        }
        /// <summary>
        /// 获取所有菜单项id列表
        /// </summary>
        /// <returns></returns>
        public string GetAdministratorsMenus()
        {
            var list = FindList(f => f.Delete == false);
            var ret = "";
            foreach(var m in list)
                ret += ("" == ret ? "" : ",") + m.id.ToString();
            return ret;
        }
        /// <summary>
        /// 获取所有菜单的id列表
        /// </summary>
        /// <returns></returns>
        public List<int> GetAllMenus()
        {
            var list = new List<int>();
            var menus = FindList(f => f.Delete == false);
            foreach (var menu in menus)
                list.Add(menu.id);
            return list;
        }
        /// <summary>
        /// 获取指定菜单和其下属所有菜单的id列表
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public List<int> GetSubmenus(int menu)
        {
            var list = new List<int>();
            var thisMenu = Find(f => f.id == menu && f.Delete == false);
            if (null != thisMenu)
            {
                list.Add(menu);
                var menus = FindList(f => f.Parent == menu && f.Delete == false);
                foreach (var m in menus)
                {
                    list.AddRange(GetSubmenus(m.id));
                }
            }
            return list;
        }
    }
}
