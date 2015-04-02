using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wbs.Everdigm.Web.main
{
    public partial class left_frame : BasePermissionPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (!HasSessionLose)
            {
                if (!IsPostBack)
                {
                    ShowPermissions();
                }
            }
        }
        private void ShowPermissions()
        {
            name.InnerText = Account.Name;
            tvSystemMenu.Nodes.Clear();
            ShowPermissionsInTreeView(tvSystemMenu, 0, -1, GetIdList(Account.TB_Role.Permission));
        }
    }
}