using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wbs.Everdigm.Web.mobile
{
    public partial class my_devices : BaseMobilePage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (SessionLosed)
            {
                Response.Redirect("default.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    ShowMineInfo();
                    ShowMyDevices();
                }
            }
        }
        private void ShowMineInfo()
        {
            if (null != me)
            {
                account.InnerText = "Welcome " + me.Name;
            }
        }
        /// <summary>
        /// 显示我作为购买者名下的所有设备列表
        /// </summary>
        private void ShowMyDevices()
        { 

        }

        protected void Submit_Click(object sender, EventArgs e)
        {

        }
    }
}