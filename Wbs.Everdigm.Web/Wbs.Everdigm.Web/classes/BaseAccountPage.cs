using System;

using Wbs.Everdigm.BLL;
using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.Web
{
    /// <summary>
    /// 用户信息管理基类
    /// </summary>
    public class BaseAccountPage : BaseBLLPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (HasSessionLose)
            {
                ShowNotification("../default.aspx", "Your session has expired, please login again.", false, true);
            }
        }
        protected DepartmentBLL DepartmentInstance { get { return new DepartmentBLL(); } }
        protected RoleBLL RoleInstance { get { return new RoleBLL(); } }
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="obj"></param>
        protected void Update(TB_Account obj)
        {
            AccountInstance.Update(f => f.id == obj.id, action =>
            {
                action.Delete = obj.Delete;
                action.Answer = obj.Answer;
                action.Code = obj.Code;
                action.Department = obj.Department;
                action.Email = obj.Email;
                action.LandlineNumber = obj.LandlineNumber;
                action.LastLoginIp = obj.LastLoginIp;
                action.LastLoginTime = obj.LastLoginTime;
                action.Locked = obj.Locked;
                action.LoginTimes = obj.LoginTimes;
                action.Name = obj.Name;
                action.Password = obj.Password;
                action.Phone = obj.Phone;
                action.Question = obj.Question;
                action.RegisterTime = obj.RegisterTime;
                action.Role = obj.Role;
            });
        }
    }
}