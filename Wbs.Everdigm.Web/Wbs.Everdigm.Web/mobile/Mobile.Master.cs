using System;

namespace Wbs.Everdigm.Web.mobile
{
    public partial class Mobile : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            nYear.InnerText = DateTime.Now.Year.ToString();
        }
    }
}