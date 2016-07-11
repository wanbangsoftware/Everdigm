using System;
using System.Linq;
using Wbs.Everdigm.BLL;
using Wbs.Everdigm.Database;
using System.Configuration;

namespace Wbs.Everdigm.Web
{
    public partial class _default : BaseBLLPage
    {
        private static string NOTIFY = ConfigurationManager.AppSettings["DBConnectExceptionNotified"];
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
                        fetchingLastVersionOfApp();
                    }
                }
            }
            else
            {
                // 如果是移动端则转移动端登陆界面
                Response.Redirect("./mobile/default.aspx");
            }
        }
        /// <summary>
        /// 查找最新的app下载
        /// </summary>
        private void fetchingLastVersionOfApp()
        {
            using (var apps = new AppBLL())
            {
                var app = apps.FindList<TB_Application>(f => f.Useable == true, "CreateTime", true).FirstOrDefault();
                if (null != app)
                {
                    download.HRef = app.Download;
                    download.Title = app.Description_en;
                }
            }
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            var md5 = Utility.MD5(password.Value).ToLower();
            var name = username.Value.Trim();
            name = name.Length >= 30 ? name.Substring(0, 30) : name;
            using (var bll = new AccountBLL())
            {
                var account = bll.Find(f=>f.Code.Equals(name));
                if (null != account)
                {
                    using (var action = new ActionBLL())
                    {
                        if (!account.Password.ToLower().Equals(md5))
                        {
                            SaveHistory(new TB_AccountHistory()
                            {
                                Account = account.id,
                                ActionId = action.Find(f => f.Name.Equals("Login")).id,
                                ObjectA = Utility.GetClientBrowser(Request) + ", login fail: password error"
                            });
                            ShowNotification("../default.aspx", "Login fail: password is not correct.", false);
                        }
                        else if (account.Locked == true)
                        {
                            SaveHistory(new TB_AccountHistory()
                            {
                                Account = account.id,
                                ActionId = action.Find(f => f.Name.Equals("Login")).id,
                                ObjectA = Utility.GetClientBrowser(Request) + ", login blocked: account has locked"
                            });
                            ShowNotification("../default.aspx", "You cannot login: your account has been locked.", false);
                        }
                        else
                        {
                            SaveHistory(new TB_AccountHistory()
                            {
                                Account = account.id,
                                ActionId = action.Find(f => f.Name.Equals("Login")).id,
                                ObjectA = Utility.GetClientBrowser(Request)
                            });
                            updateAccount(account);
                        }
                    }
                }
                else
                {
                    ShowNotification("../default.aspx", "Login fail: no account exist like your input.", false);
                }
            }
        }
        /// <summary>
        /// 更新最后登陆的信息
        /// </summary>
        /// <param name="account"></param>
        private void updateAccount(TB_Account account)
        {
            using (var bll = new AccountBLL())
            {
                bll.Update(f => f.id == account.id, act =>
                {
                    act.LastLoginTime = DateTime.Now;
                    act.LoginTimes++;
                    act.LastLoginIp = Utility.GetClientIP(this.Context);
                });
            }
            Session[Utility.SessionName] = account;
            // 登录成功说明服务器数据库没问题，将全局标志改为未通知状态
            Application[NOTIFY] = false;
            ShowNotification("../main/main.aspx", "Welcome <a>" + account.Name + "</a>, You have login successfully.");
            //Response.Redirect("./main/main.aspx");
        }
    }
}