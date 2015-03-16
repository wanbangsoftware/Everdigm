using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;

namespace Wbs.Everdigm.Web.main
{
    public partial class center_frame : System.Web.UI.Page
    {
        public string _cols_ = ConfigurationManager.AppSettings["MenuAreaWidth"];
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}