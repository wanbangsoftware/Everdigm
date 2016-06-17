using System;
using Wbs.Everdigm.BLL;
using Wbs.Everdigm.Database;

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
            var role = new RoleBLL().Find(f => f.id == Account.Role);
            ShowPermissionsInTreeView(tvSystemMenu, 0, -1, GetIdList(role.Permission));
        }
    }
}