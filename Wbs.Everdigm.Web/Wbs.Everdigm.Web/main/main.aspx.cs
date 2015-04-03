using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace Wbs.Everdigm.Web.main
{
    public partial class main : System.Web.UI.Page
    {
        public string MenuWidth = ConfigurationManager.AppSettings["MenuAreaWidth"];
        public string TopHeight = ConfigurationManager.AppSettings["TopAreaHeight"];
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}