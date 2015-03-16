using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Wbs.Everdigm.BLL;
using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.Web
{
    public class BaseCustomerPage : BaseBLLPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (HasSessionLose)
            {
                ShowNotification("../default.aspx", "Your session has expired, please login again.", false, true);
            }
        }
        /// <summary>
        /// 客户信息业务处理逻辑
        /// </summary>
        protected CustomerBLL CustomerInstance { get { return new CustomerBLL(); } }
        /// <summary>
        /// 更新客户的信息
        /// </summary>
        /// <param name="obj"></param>
        protected void Update(TB_Customer obj)
        {
            CustomerInstance.Update(f => f.id == obj.id, act =>
            {
                act.Address = obj.Address;
                act.Code = obj.Code;
                act.Delete = obj.Delete;
                act.IdCard = obj.IdCard;
                act.Name = obj.Name;
                act.Phone = obj.Phone;
            });
        }
    }
}