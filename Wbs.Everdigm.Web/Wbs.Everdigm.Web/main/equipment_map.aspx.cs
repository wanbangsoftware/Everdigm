using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace Wbs.Everdigm.Web.main
{
    public partial class equipment_map : System.Web.UI.Page
    {
        public string TopHeight = ConfigurationManager.AppSettings["TopAreaHeight"].ToString().Replace(",*,21", "");
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}