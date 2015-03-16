using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wbs.Everdigm.Web
{
    public partial class logout : BasePage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            Session.Clear();
            ShowNotification("../default.aspx", "You have logout successfully.", true, true);
        }
    }
}