using System;

namespace Wbs.Everdigm.Web.mobile
{
    public partial class _default : BaseMobilePage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (!string.IsNullOrEmpty(_key))
            {
                Session.Clear();
                me = null;
            }
            // 如果之前登陆的客户的session还未失效则直接转到客户的设备列表页面
            if (null != me)
            {
                Response.Redirect("my_devices.aspx");
            }
        }
    }
}