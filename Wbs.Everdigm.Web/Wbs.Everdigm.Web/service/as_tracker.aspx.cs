using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wbs.Everdigm.Web.service
{
    public partial class as_tracker : BaseTrackerPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (!HasSessionLose)
            {
                hidKey.Value = Utility.UrlEncode(_key);
            }
        }
    }
}