using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Wbs.Everdigm.BLL;
using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.Web.mobile
{
    /// <summary>
    /// 移动端页面基类
    /// </summary>
    public class BaseMobilePage : BasePage
    {
        /// <summary>
        /// 当前登录的客户的信息
        /// </summary>
        protected TB_Customer me = null;
        /// <summary>
        /// 当前session是否已丢失
        /// </summary>
        protected bool SessionLosed = false;
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            me = Session[Utility.SessionNameCustomer] as TB_Customer;
            SessionLosed = me == null;
        }
        /// <summary>
        /// 客户信息业务处理实体
        /// </summary>
        protected CustomerBLL CustomerInstance { get { return new CustomerBLL(); } }
        /// <summary>
        /// 设备业务
        /// </summary>
        protected EquipmentBLL EquipmentInstance { get { return new EquipmentBLL(); } }
    }
}