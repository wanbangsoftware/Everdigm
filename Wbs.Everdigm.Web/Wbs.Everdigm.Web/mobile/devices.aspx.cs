using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wbs.Everdigm.Web.mobile
{
    /// <summary>
    /// 我的设备列表
    /// </summary>
    public partial class devices : BaseMobilePage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (!IsPostBack)
            { }
        }
        private void ShowMyDevicesList()
        { }

        protected void Submit_Click(object sender, EventArgs e)
        {

        }
    }
}