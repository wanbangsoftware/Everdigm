using System;

namespace Wbs.Everdigm.Web
{
    public partial class logout : BasePage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            Session.Clear();
            Response.Cookies.Clear();
            Request.Cookies.Clear();
            ShowNotification("../default.aspx", "You have logout successfully.", true, true);
        }
    }
}