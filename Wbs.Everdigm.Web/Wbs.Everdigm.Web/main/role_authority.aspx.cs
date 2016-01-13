using System;
using System.Web.UI.WebControls;

using Wbs.Everdigm.BLL;
using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.Web.main
{
    public partial class role_authority : BasePermissionPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (!HasSessionLose)
            {
                if (!IsPostBack)
                {
                    // key不为空才可以进行角色权限操作
                    if (!string.IsNullOrEmpty(_key))
                    {
                        hidID.Value = _key;
                        init();
                    }
                    else
                    {
                        ShowNotification("./role_list.aspx", "Cannot edit role's permission, paramenter error.", false);
                    }
                }
            }
        }
        private RoleBLL RoleInstance { get { return new RoleBLL(); } }

        private TB_Role GetRole()
        {
            var id = int.Parse(Utility.Decrypt(hidID.Value));
            var role = RoleInstance.Find(f => f.id == id && f.Delete == false);
            if (null == role)
            {
                ShowNotification("./role_list.aspx", "Error: the role object is null.", false);
            }
            return role;
        }
        private void init()
        {
            var role = GetRole();
            if (null != role)
            {
                thTitle.InnerHtml = "System Menus for: " + role.Name;
                tvMenus.Nodes.Clear();
                //tvMenus.Attributes.Add("onclick", "return OnClientTreeNodeChecked(event);");
                //var list = GetIdList(role.Permission);
                ShowPermissionsInTreeView(tvMenus, 0, 0, GetIdList(role.Permission));
            }
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            var role = GetRole();
            if (null != role)
            {
                string rights = "";
                foreach (TreeNode tn in tvMenus.CheckedNodes)
                {
                    var id = tn.NavigateUrl.Replace("#", "");
                    rights += "" == rights ? id : ("," + id);
                }
                RoleInstance.Update(f => f.id == role.id && f.Delete == false, action => 
                {
                    action.Permission = rights;
                });

                // 记录历史
                var his = new TB_AccountHistory();
                his.ActionId = ActionInstance.Find(f => f.Name.Equals("RolePermission")).id;
                his.Ip = Utility.GetClientIP(this.Context);
                his.ObjectA = "[id=" + role.id + "] " + role.Name;
                SaveHistory(his);

                ShowNotification("./role_list.aspx", "You have changed permission of role \"" + role.Name + "\".");
            }
        }
    }
}