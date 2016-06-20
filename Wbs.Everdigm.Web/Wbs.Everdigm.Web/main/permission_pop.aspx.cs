using System;
using Wbs.Everdigm.BLL;

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
            var role = new RoleBLL().Find(f => f.id == Account.Role);
            ShowPermissionsInTreeView(tvNavigations, 0, 1, GetIdList(role.Permission));
        }
    }
}