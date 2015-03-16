using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Wbs.Everdigm.BLL;
using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.Web.main
{
    public partial class permission_add : BasePermissionPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (!HasSessionLose)
            {
                // key 值不为空时表示编辑
                if (!string.IsNullOrEmpty(_key))
                {
                    hidID.Value = _key;
                    if (!IsPostBack)
                    {
                        // 初始化编辑源
                        init();
                    }
                }
            }
        }
        private void init()
        {
            var id = ParseInt(Utility.Decrypt(_key));
            if (id <= 0)
            {
                ShowNotification("./permission_list.aspx", "Error: paramenter error, cannot edit the permission.", false);
            }
            else
            {
                var per = PermissionInstance.Find(p => p.id == id);
                if (null == per)
                {
                    ShowNotification("./permission_list.aspx", "Error: Cannot edit permission, object not exist.", false);
                }
                else
                {
                    txtDescription.Value = per.Description;
                    txtName.Value = per.Name;
                    hidParent.Value = per.Parent.ToString();
                    if (per.Parent > 0)
                    {
                        var parent = PermissionInstance.Find(f => f.id == per.Parent);
                        txtParent.Value = parent.Name;
                    }
                    isDefault.Checked = per.IsDefault.Value;
                    txtURL.Value = per.Url;
                    imgImage.Src = per.Image;
                    hidImage.Value = per.Image;
                }
            }
        }
        private void New()
        {
            var per = new TB_Permission();
            per.Name = txtName.Value.Trim();
            per.Image = hidImage.Value;
            per.Delete = false;
            per.IsDefault = isDefault.Checked;
            per.Url = txtURL.Value.Trim();
            // 父级菜单，为空时默认为顶级菜单
            var parent = int.Parse("" == hidParent.Value ? "0" : hidParent.Value);
            per.Parent = parent;
            var brothers = PermissionInstance.FindList(p => p.Parent == parent);
            per.DisplayOrder = brothers.Count();
            per.AddTime = DateTime.Now;
            per.Description = txtDescription.Value.Trim();
            PermissionInstance.Add(per);

            // 记录历史
            SaveHistory(new TB_AccountHistory
            {
                ActionId = ActionInstance.Find(f => f.Name.Equals("AddPermission")).id,
                ObjectA = "[id=" + per.id + "] " + per.Name
            });
            UpdateRole(per);
            ShowNotification("./permission_list.aspx", "Success: You added a new menu.", true);
        }
        private RoleBLL RoleInstance { get { return new RoleBLL(); } }
        /// <summary>
        /// 根据编辑的菜单项更新角色的访问
        /// </summary>
        /// <param name="obj"></param>
        private void UpdateRole(TB_Permission obj)
        {
            // 更新默认角色可以访问的
            if (obj.IsDefault == true)
            {
                var dftPermission = PermissionInstance.GetDefaultMenus();
                // 查找非管理角色
                var roles = RoleInstance.FindList(f => f.IsAdministrator == false && f.Delete == false);
                foreach (var role in roles)
                {
                    var pers = role.Permission.Split(new char[] { ',' });
                    if (!pers.Contains(obj.id.ToString()))
                    {
                        RoleInstance.Update(f => f.id == role.id, act => act.Permission = dftPermission);
                    }
                }
            }
            // 查找更新管理员角色的访问权限
            RoleInstance.Update(f => f.IsAdministrator == true && f.Delete == false, 
                act => act.Permission = PermissionInstance.GetAdministratorsMenus());

            // 重置当前登陆者的session
            Account = AccountInstance.Find(f => f.id == Account.id);
            Session[Utility.SessionName] = Account;
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            if (hidID.Value != "")
            {
                var per = PermissionInstance.Find(f => f.id == int.Parse(Utility.Decrypt(hidID.Value)));
                if (null == per)
                {
                    // 无法进行更新
                    ShowNotification("./permission_list.aspx", "Cannot edit permission: object not exist.", false);
                    return;
                }
                else
                {
                    per.Name = txtName.Value.Trim();
                    per.Image = hidImage.Value;
                    per.Description = txtDescription.Value.Trim();
                    per.IsDefault = isDefault.Checked;
                    per.Url = txtURL.Value.Trim();
                    Edit(int.Parse(hidParent.Value), per);
                    UpdateRole(per);
                }
            }
            else {
                New();
            }
        }
    }
}