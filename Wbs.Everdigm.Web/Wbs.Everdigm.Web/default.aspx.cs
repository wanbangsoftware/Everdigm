using System;
using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.Web
{
    public partial class _default : BaseBLLPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            bool isMobile = Utility.IsMobile(this.Context);
            bool toDesktop = true;
            if (isMobile)
            {
                if (!string.IsNullOrEmpty(_key) && _key.ToLower().Equals("desktop"))
                { toDesktop = true; }
                else
                { toDesktop = false; }
            }
            if (toDesktop)
            {
                var account = Session[Utility.SessionName] as TB_Account;
                if (null != account)
                {
                    updateAccount(account);
                }
                else
                {
                    if (!IsPostBack)
                    {
                        Session.Clear();
                        Request.Cookies.Clear();
                        Response.Cookies.Clear();
                    }
                }
            }
            else
            {
                // 如果是移动端则转移动端登陆界面
                Response.Redirect("./mobile/default.aspx");
            }
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            var md5 = Utility.MD5(password.Value).ToLower();
            var name = username.Value.Trim();
            name = name.Length >= 20 ? name.Substring(0, 20) : name;
            var account = AccountInstance.Find(name, md5);
            if (null != account)
            {
                if (account.Locked == true)
                {
                    SaveHistory(new TB_AccountHistory()
                    {
                        Account = account.id,
                        ActionId = ActionInstance.Find(f => f.Name.Equals("Login")).id,
                        ObjectA = Utility.GetClientBrowser(Request) + ", login blocked: account has locked"
                    });
                    ShowNotification("../default.aspx", "You cannot login: your account has been locked.", false);
                }
                else
                {
                    SaveHistory(new TB_AccountHistory()
                    {
                        Account = account.id,
                        ActionId = ActionInstance.Find(f => f.Name.Equals("Login")).id,
                        ObjectA = Utility.GetClientBrowser(Request)
                    });
                    updateAccount(account);
                }
            }
            else
            {
                ShowNotification("../default.aspx", "Login fail: Maybe your password is not correct?", false);
            }
        }
        /// <summary>
        /// 更新最后登陆的信息
        /// </summary>
        /// <param name="account"></param>
        private void updateAccount(TB_Account account)
        {
            AccountInstance.Update(f => f.id == account.id, a =>
            {
                a.LastLoginTime = DateTime.Now;
                a.LoginTimes++;
                a.LastLoginIp = Utility.GetClientIP(this.Context);
            });
            Session[Utility.SessionName] = account;

            ShowNotification("../main/main.aspx", "Welcome <a>" + account.Name + "</a>, You have login successfully.");
            //Response.Redirect("./main/main.aspx");
        }
    }
}