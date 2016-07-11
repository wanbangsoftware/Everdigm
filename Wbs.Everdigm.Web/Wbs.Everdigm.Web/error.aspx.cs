using System;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;

namespace Wbs.Everdigm.Web
{
    public partial class error : System.Web.UI.Page
    {
        private static string ERR_KEY = ConfigurationManager.AppSettings["APP_ERROR_PLACEHOLDER"];
        private static string NOTIFY = ConfigurationManager.AppSettings["DBConnectExceptionNotified"];
        /// <summary>
        /// 要显示的错误内容
        /// </summary>
        public static string DialogContent = "";
        /// <summary>
        /// 对话框标题
        /// </summary>
        public static string DialogTitle = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //显示程序中的错误码
                if (Application[ERR_KEY] != null)
                {
                    pre_content.InnerText = Application[ERR_KEY].ToString();
                }
                //HandleUserNotify();
            }
        }

        private void HandleUserNotify()
        {
            if (!string.IsNullOrEmpty(DialogContent) && DialogContent.Contains("Cannot connect Database Server"))
            {
                // 如果错误类型是无法连接数据库
                object notify = Application[NOTIFY];
                if (null == notify || (bool)notify == false)
                {
                    // 没有通知过用户时向用户发送邮件通知
                    Application[NOTIFY] = true;
                    SendMail();
                }
            }
        }
        private void SendMail()
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(new MailAddress("xfeiffer@hotmail.com", "Hsiang Leekwok", Encoding.UTF8));
            string temp = ConfigurationManager.AppSettings["APP_NOTIFY_USERS"];
            if (!string.IsNullOrEmpty(temp))
            {
                string[] users = temp.Split(new char[] { '|' });
                foreach (var user in users)
                {
                    string[] one = user.Split(new char[] { ',' });
                    mail.CC.Add(new MailAddress(one[0], one[1], Encoding.UTF8));
                }
            }
            mail.From = new MailAddress("everdigm.ems@gmail.com", "Everdigm EMS System", Encoding.UTF8);
            mail.SubjectEncoding = Encoding.UTF8;
            mail.Subject = "Emergency mail from EMS system(Database connect fail)";
            mail.BodyEncoding = Encoding.UTF8;
            mail.IsBodyHtml = true;
            string body = Properties.Resources.mail_content.Replace("%stack_trace%", Application[ERR_KEY].ToString());
            mail.Body = body;
            mail.Priority = MailPriority.High;

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("everdigm.ems@gmail.com", "xlg_110004");

            //try
            {
                client.Send(mail);
                mail = null;
            }
            //catch { }
        }
    }
}