using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Wbs.Everdigm.BLL;
using Wbs.Everdigm.Database;
using System.Web.UI.WebControls;

namespace Wbs.Everdigm.Web
{
    public class BaseRolePage : BaseBLLPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (HasSessionLose)
            {
                ShowNotification("../default.aspx", "Your session has expired, please login again.", false, true);
            }
        }
        protected RoleBLL RoleInstance { get { return new RoleBLL(); } }

        protected PermissionBLL PermissionInstance { get { return new PermissionBLL(); } }

        protected void Update(TB_Role role)
        {
            RoleInstance.Update(f => f.id == role.id, action => 
            {
                action.Delete = role.Delete;
                action.Description = role.Description;
                action.IsAdministrator = role.IsAdministrator;
                action.IsDefault = role.IsDefault;
                action.Name = role.Name;
                action.Permission = role.Permission;
            });
        }

        protected void ShowRolesInTreeView(TreeView tree)
        {
            var list = RoleInstance.FindList(f => f.Delete == false).OrderBy(o => o.Name);
            foreach (var role in list)
            {
                TreeNode node = new TreeNode();
                node.Text = role.Name;
                node.SelectAction = TreeNodeSelectAction.Expand;
                node.NavigateUrl = "#"+role.id;
                node.Target = "right_frame";
                node.ToolTip = role.Description;
                tree.Nodes.Add(node);
            }
        }
    }
}