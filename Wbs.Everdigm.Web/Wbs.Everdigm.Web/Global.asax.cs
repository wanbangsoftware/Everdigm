using System;
using System.Web;
using System.Configuration;
using System.Security.Cryptography;

namespace Wbs.Everdigm.Web
{
    public class Global : System.Web.HttpApplication
    {
        private static string ERR_KEY = ConfigurationManager.AppSettings["APP_ERROR_PLACEHOLDER"];
        protected void Application_Start(object sender, EventArgs e)
        {
            // 初始化全局加密解密类
            if (null == Utility._RijndaelManaged)
            {
                Utility._RijndaelManaged = new RijndaelManaged();
                Utility._RijndaelManaged.KeySize = 128;
            }

            // 开启系统服务接受终端发送的数据
            //StaticService.StartService();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            if (null != exception)
            {
                Application[ERR_KEY] = exception.ToString();
                if (null != exception.InnerException)
                {
                    string title = exception.Message;
                    if (title.Contains("System.Web.HttpUnhandledException"))
                    {
                        title = "Cannot handle your request";
                    }
                    error.DialogTitle = title;
                    string content = exception.InnerException.Message;
                    if (content.Contains("(provider: Named Pipes Provider, error: 40 ") ||
                        content.Contains("The timeout period elapsed prior to obtaining a connection"))
                    {
                        content = "Cannot connect Database Server, please contact System Administrator.";
                    }
                    error.DialogContent = content;
                }
                else
                {
                    error.DialogTitle = "Cannot handle your request";
                    error.DialogContent = exception.Message;
                }
                Server.ClearError();
                HttpContext.Current.Response.Redirect(HttpContext.Current.Request.ApplicationPath + "error.aspx");
            }
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
            if (null != Utility._RijndaelManaged)
            {
                Utility._RijndaelManaged.Clear();
                Utility._RijndaelManaged.Dispose();
            }
            //StaticService.StopService();
        }
    }
}