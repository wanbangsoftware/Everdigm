using System;

using System.Configuration;

namespace Wbs.Everdigm.Web.main
{
    public partial class center_frame : System.Web.UI.Page
    {
        public string _cols_ = ConfigurationManager.AppSettings["MenuAreaWidth"];
        public string _rows_ = ConfigurationManager.AppSettings["TopAreaHeight"];
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}