using System;

namespace Wbs.Everdigm.Web.main
{
    public partial class role_pop : BaseRolePage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (!HasSessionLose)
                ShowRoles();
        }

        private void ShowRoles()
        {
            tvRoles.Attributes.Add("onclick", "return OnClientTreeNodeChecked(event);");
            tvRoles.Nodes.Clear();
            ShowRolesInTreeView(tvRoles);
        }
    }
}