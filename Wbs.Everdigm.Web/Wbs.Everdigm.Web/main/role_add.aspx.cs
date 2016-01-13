using System;

using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.Web.main
{
    public partial class role_add : BaseRolePage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (!HasSessionLose)
            {
                if (!IsPostBack)
                    checkCheckbox();
                // key 不为空时表示编辑role信息
                if (!string.IsNullOrEmpty(_key))
                {
                    hidID.Value = _key;
                    if (!IsPostBack)
                    {
                        showEdit();
                    }
                }
            }
        }
        /// <summary>
        /// 检测default和administrator是否可以选中
        /// </summary>
        private void checkCheckbox()
        {
            var def = RoleInstance.Find(f => f.IsDefault == true && f.Delete == false);
            cbIsDefault.Enabled = null == def;
            var admin = RoleInstance.Find(f => f.IsAdministrator == true && f.Delete == false);
            cbIsAdmin.Enabled = null == admin;
        }

        private void showEdit()
        {
            var role = RoleInstance.Find(f => f.id == int.Parse(Utility.Decrypt(hidID.Value)) && f.Delete == false);
            if (null != role)
            {
                txtDescription.Value = role.Description;
                txtName.Value = role.Name;
                cbIsAdmin.Checked = role.IsAdministrator.Value;
                // 是管理员角色时，设置复选框可用
                if (role.IsAdministrator == true)
                {
                    cbIsAdmin.Enabled = true;
                }
                cbIsDefault.Checked = role.IsDefault.Value;
                // 是默认角色是，设置复选框可用
                if (role.IsDefault == true)
                {
                    cbIsDefault.Enabled = true;
                }
            }
            else
            {
                ShowNotification("./role_list.aspx", "Error: paramenter error, cannot edit the role information.", false);
            }
        }

        private void NewRole()
        {
            var role = new TB_Role();
            role.AddTime = DateTime.Now;
            role.Description = txtDescription.Value.Trim();
            role.IsAdministrator = cbIsAdmin.Checked;
            role.IsDefault = cbIsDefault.Checked;
            role.Name = txtName.Value.Trim();
            role.Delete = false;
            role.Permission = PermissionInstance.GetDefaultMenus();
            RoleInstance.Add(role);

            // 记录历史
            var his = new TB_AccountHistory();
            his.Account = Account.id;
            his.ActionId = ActionInstance.Find(f => f.Name.Equals("AddRole")).id;
            his.Ip = Utility.GetClientIP(this.Context);
            his.ObjectA = "[id=" + role.id + "] " + role.Name;
            SaveHistory(his);

            ShowNotification("./role_list.aspx", "Success: You added a new role.", true);
        }

        private void Edit()
        {
            // 记录历史
            var his = new TB_AccountHistory();
            his.Account = Account.id;
            his.ActionId = ActionInstance.Find(f => f.Name.Equals("EditRole")).id;
            his.Ip = Utility.GetClientIP(this.Context);

            var id = int.Parse(Utility.Decrypt(hidID.Value));
            var role = RoleInstance.Find(f => f.id == id && f.Delete == false);
            role.Description = txtDescription.Value.Trim();
            role.IsAdministrator = cbIsAdmin.Checked;
            role.IsDefault = cbIsDefault.Checked;
            var name = txtName.Value.Trim();
            if (!role.Name.Equals(name))
            {
                his.ObjectA = "[id=" + role.id + "] " + role.Name + " to " + name;
            }
            else
            {
                his.ObjectA = "[id=" + role.id + "] " + role.Name;
            }
            role.Name = name;
            Update(role);

            SaveHistory(his);

            ShowNotification("./role_list.aspx", "Success: You have changed role \"" + role.Name + "\".", true);
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            if (hidID.Value != "")
            {
                Edit();
            }
            else
            {
                NewRole();
            }
        }
    }
}