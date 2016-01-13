using System;

using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.Web.main
{
    public partial class change_password : BaseAccountPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            var Old = Utility.MD5(txtOldPassword.Value.Trim());
            var New = Utility.MD5(txtNewPassword.Value.Trim());
            if (Old.Equals(Account.Password.ToUpper()))
            {
                Account.Password = New;
                AccountInstance.Update(f => f.id == Account.id, a => { a.Password = Account.Password; });

                // 保存历史记录
                SaveHistory(new TB_AccountHistory
                {
                    ActionId = ActionInstance.Find(f => f.Name.Equals("ChangePassword")).id,
                    Ip = Utility.GetClientIP(this.Context)
                });
                ShowNotification("./change_password.aspx", "Your have changed your password, it's take effective when you next login.");
            }
            else
            {
                ShowNotification("./change_password.aspx", "Your old password is incorrect, please try again.", false);
            }
        }
    }
}