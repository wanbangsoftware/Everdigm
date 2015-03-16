using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wbs.Everdigm.Web.mobile
{
    public partial class _default : BaseMobilePage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_key))
            {
                Session.Clear();
            }
            base.Page_Load(sender, e);
            // 如果之前登陆的客户的session还未失效则直接转到客户的设备列表页面
            if (null != me)
            {
                Response.Redirect("devices.aspx");
            }
            //span.InnerText = Request.ServerVariables["HTTP_USER_AGENT"];
        }
    }
}