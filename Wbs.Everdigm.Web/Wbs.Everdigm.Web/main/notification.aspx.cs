using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wbs.Everdigm.Web.main
{
    public partial class notification : BasePage
    {
        /// <summary>
        /// 需要转到的地址
        /// </summary>
        public static string RedirectUrl = "";
        /// <summary>
        /// 要显示的信息内容
        /// </summary>
        public static string ErrorString = "no text here";
        /// <summary>
        /// 标记是成功还是失败。0=成功1=错误
        /// </summary>
        public static bool IsSuccess = true;
        /// <summary>
        /// 标记是否最顶层转到相应的页面。0=否1=是
        /// </summary>
        public static string TopestRedirect = "0";
        /// <summary>
        /// 等待的时间，成功时2秒，失败时5秒
        /// </summary>
        public static int _maxTimes = 2;

        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            //_maxTimes = IsSuccess ? 2 : 5;
            notification_title.InnerHtml = IsSuccess ? "SUCCESS" : "FAILED";
            Title = notification_title.InnerHtml;
            url.Value = RedirectUrl;
            notification_content.Attributes["class"] = IsSuccess ? "notification success" : "notification error";
            notification_content.InnerHtml = "<div>" + ErrorString + "</div>";
            topest.Value = TopestRedirect;
        }
    }
}