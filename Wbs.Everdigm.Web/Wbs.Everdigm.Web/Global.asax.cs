using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

using System.Security.Cryptography;

namespace Wbs.Everdigm.Web
{
    public class Global : System.Web.HttpApplication
    {

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
            StaticService.StopService();
        }
    }
}