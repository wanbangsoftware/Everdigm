using System;

using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.Web.main
{
    public partial class account_add : BaseAccountPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (!HasSessionLose)
            {
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

        private void showEdit()
        {
            var account = AccountInstance.Find(f => f.id == ParseInt(Utility.Decrypt(_key)));
            if (null != account)
            {
                txtCode.Value = account.Code;
                txtDepartment.Value = (int?)null == account.Department ? "" : account.TB_Department.Name;
                txtEmail.Value = account.Email;
                txtLindline.Value = account.LandlineNumber;
                txtName.Value = account.Name;
                txtPhone.Value = account.Phone;
                txtQuestion.Value = account.Question;
                txtAnswer.Disabled = !string.IsNullOrEmpty(account.Question);
                txtRole.Value = (int?)null == account.Role ? "" : account.TB_Role.Name;
                hidDepartment.Value = (int?)null == account.Department ? "" : account.Department.ToString();
                hidRole.Value = (int?)null == account.Role ? "" : account.Role.ToString();
            }
            else
            { ShowNotification("./account_list.aspx", "Error: paramenter error, cannot edit the account.", false); }
        }

        private void BuildAccountInfo(TB_Account obj)
        {
            obj.Answer = txtAnswer.Value.Trim();
            obj.Code = txtCode.Value.Trim();
            obj.Department = ParseInt(hidDepartment.Value);
            if (obj.Department < 0) {
                obj.Department = (int?)null;
                // 获取默认部门
                var dept = DepartmentInstance.Find(f => f.IsDefault == true && f.Delete == false);
                if (null != dept)
                {
                    obj.Department = dept.id;
                }
            }
            obj.Email = txtEmail.Value.Trim();
            obj.LandlineNumber = txtLindline.Value.Trim();
            obj.Name = txtName.Value.Trim();
            //新建用户时
            if (obj.id == 0)
            {
                obj.Password = Utility.MD5("123456");
            }
            obj.Phone = txtPhone.Value.Trim();
            obj.Question = txtQuestion.Value.Trim();
            obj.Role = ParseInt(hidRole.Value);
            // 默认角色
            if (obj.Role < 0)
            {
                obj.Role = (int?)null;
                var role = RoleInstance.Find(f => f.IsDefault == true && f.Delete == false);
                if (null != role) obj.Role = role.id;
            }
        }
        private void NewAccount()
        {
            var account = AccountInstance.GetObject();
            BuildAccountInfo(account);
            // check the same login code
            var chk = AccountInstance.Find(f => f.Code.Equals(account.Code));
            if (null != chk)
            {
                ShowNotification("./account_add.aspx", "The login code \"" + account.Code + "\" is exist.", false);
            }
            else
            {
                AccountInstance.Add(account);

                // 保存历史记录
                SaveHistory(new TB_AccountHistory()
                {
                    ActionId = ActionInstance.Find(f => f.Name.Equals("AddAccount")).id,
                    ObjectA = "[id=" + account.id + "] " + account.Name + ", " + account.Code
                });

                ShowNotification("./account_add.aspx", "You add a new account: " + account.Name + "(" + account.Code + ").");
            }
        }

        private void EditAccount()
        {
            var account = AccountInstance.Find(f => f.id == ParseInt(Utility.Decrypt(hidID.Value)));
            if (null != account)
            {
                BuildAccountInfo(account);
                Update(account);

                // 保存历史记录
                SaveHistory(new TB_AccountHistory
                {
                    ActionId = ActionInstance.Find(f => f.Name.Equals("EditAccount")).id,
                    ObjectA = "[id=" + account.id + "] " + account.Name + ", " + account.Code
                });

                ShowNotification("./account_list.aspx", "You changed account: " + account.Name + "(" + account.Code + ").");
            }
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            if (!HasSessionLose)
            {
                if ("" == hidID.Value)
                {
                    NewAccount();
                }
                else
                {
                    EditAccount();
                }
            }
        }
    }
}