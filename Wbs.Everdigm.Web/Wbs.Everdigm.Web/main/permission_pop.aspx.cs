using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wbs.Everdigm.Web.main
{
    public partial class permission_pop : BasePermissionPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            ShowPermissions();
        }

        private void ShowPermissions()
        {
            tvNavigations.Attributes.Add("onclick", "return OnClientTreeNodeChecked(event);");
            tvNavigations.Nodes.Clear();
            ShowPermissionsInTreeView(tvNavigations, 0, 1, GetIdList(Account.TB_Role.Permission));
        }
    }
}