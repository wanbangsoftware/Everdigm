using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wbs.Everdigm.Web
{
    /// <summary>
    /// 所有Master页面的基类
    /// </summary>
    public class BaseMaster : System.Web.UI.MasterPage
    {
        /// <summary>
        /// 其他页面传过来的key值
        /// </summary>
        protected string _key = "";

        protected virtual void Page_Load(object sender, EventArgs e)
        {
            InitializeParamenters();
        }

        /// <summary>
        /// 初始化参数列表
        /// </summary>
        protected virtual void InitializeParamenters()
        {
            _key = GetParamenter("key");
        }
        /// <summary>
        /// 获取远程传过来的参数值
        /// </summary>
        /// <param name="key">参数的key</param>
        /// <returns>返回参数的值</returns>
        protected virtual string GetParamenter(string key)
        {
            return Request.Params[key];
        }
        /// <summary>
        /// 将字符串转换成对应的int值
        /// </summary>
        /// <param name="value">需要转换成int的字符串</param>
        /// <returns>转换失败时返回-1</returns>
        protected int ParseInt(string value)
        {
            var i = 0;
            if (!int.TryParse(value, out i))
                i = -1;
            return i;
        }
        /// <summary>
        /// 显示警告信息
        /// </summary>
        /// <param name="url">显示完警告之后需要跳转的页面url</param>
        /// <param name="text">需要显示的警告信息内容</param>
        /// <param name="success">标记显示成功信息还是现实失败信息</param>
        /// <param name="topest">跳转url时是整个窗口页面跳转还是只当前页面跳转</param>
        protected void ShowNotification(string url, string text, bool success = true, bool topest = false)
        {
            Wbs.Everdigm.Web.main.notification.IsSuccess = success;
            Wbs.Everdigm.Web.main.notification.ErrorString = text;
            Wbs.Everdigm.Web.main.notification.RedirectUrl = url;
            Wbs.Everdigm.Web.main.notification.TopestRedirect = topest ? "1" : "0";
            Wbs.Everdigm.Web.main.notification._maxTimes = string.IsNullOrEmpty(url) ? -1 : (success ? 1 : 1);
            Response.Write("<script type=\"text/javascript\">location.href=\"/main/notification.aspx\";</script>");
        }
    }
}