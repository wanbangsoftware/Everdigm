using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.BLL
{
    /// <summary>
    /// 系统用户业务处理逻辑
    /// </summary>
    public class AccountBLL : BaseService<TB_Account>
    {
        public AccountBLL()
            : base(new BaseRepository<TB_Account>())
        { }
        public override string ToString(TB_Account entity)
        {
            return string.Format("id: {0}, name: {1}, code: {2}", entity.id, entity.Name, entity.Code);
        }
        public override TB_Account GetObject()
        {
            return new TB_Account
            {
                id = 0,
                Delete = false,
                Locked = false,
                Code = "",
                LoginTimes = 0,
                Question = "",
                Answer = "",
                Department = 0,
                Email = "",
                LandlineNumber = "",
                LastLoginIp = "",
                LastLoginTime = null,
                Name = "",
                Password = "",
                Phone = "",
                RegisterTime = DateTime.Now,
                Role = 0
            };
        }
        /// <summary>
        /// 通过用户名和密码查询
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public TB_Account Find(string name, string password)
        {
            return CurrentRepository.Find(a => a.Code.Equals(name) && a.Password.Equals(password));
        }
        /// <summary>
        /// 清除用户的部门状态为默认状态
        /// </summary>
        /// <param name="dept">需要被清理的部门id</param>
        /// <param name="defaultDept">系统中的默认部门id，小于0时表示没有默认部门，用户的部门信息会被置为null</param>
        public void ClearDeptInfo(int dept, int defaultDept)
        {
            CurrentRepository.Update(f => f.Department == dept, action =>
            {
                if (0 > defaultDept)
                    action.Department = (int?)null;
                else
                    action.Department = defaultDept;
            });
        }
        /// <summary>
        /// 清除用户的角色信息
        /// </summary>
        /// <param name="role">需要被清除的角色id</param>
        /// <param name="defaultRole">系统中默认的角色id，小于等于0时表示没有默认角色，用户的角色信息会被置为null</param>
        public void ClearRoleInfo(int role, int defaultRole)
        {
            CurrentRepository.Update(f => f.Role == role, action => {
                if (0 > defaultRole)
                    action.Role = (int?)null;
                else
                    action.Role = defaultRole;
            });
        }
    }
}
