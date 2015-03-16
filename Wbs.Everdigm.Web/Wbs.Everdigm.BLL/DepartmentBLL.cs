using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.BLL
{
    public class DepartmentBLL : BaseService<TB_Department>
    {
        public DepartmentBLL()
            : base(new BaseRepository<TB_Department>())
        { }
        /// <summary>
        /// 生成一个空的部门实例
        /// </summary>
        /// <returns></returns>
        public override TB_Department GetObject()
        {
            return new TB_Department
            {
                Delete = false,
                Address = "",
                Code = "",
                Fax = "",
                IsDefault = false,
                Name = "",
                Parent = 0,
                Phone = "",
                Room = ""
            };
        }
        public override string ToString(TB_Department entity)
        {
            return string.Format("id: {0}, name: {1}, default: {2}", entity.id, entity.Name, entity.IsDefault);
        }
        /// <summary>
        /// 获取所有部门的id列表
        /// </summary>
        /// <returns></returns>
        public List<int> GetAllDepartments()
        {
            var list = new List<int>();
            var depts = FindList(f => f.Delete == false);
            foreach (var dept in depts)
                list.Add(dept.id);
            return list;
        }
        /// <summary>
        /// 获取指定部门和其所有下级部门的id列表
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        public List<int> GetSubdepartments(int dept)
        {
            var list = new List<int>();
            var d = Find(f => f.id == dept && f.Delete == false);
            if (null != d)
            { 
                list.Add(dept);
                var depts = FindList(f => f.Parent == dept && f.Delete == false);
                foreach (var dpt in depts)
                {
                    list.AddRange(GetSubdepartments(dpt.id));
                }
            }
            return list;
        }
        /// <summary>
        /// 取得系统中的默认部门
        /// </summary>
        /// <returns></returns>
        public TB_Department GetDefaultDepartment()
        {
            return Find(f => f.IsDefault == true && f.Delete == false);
        }
    }
}
