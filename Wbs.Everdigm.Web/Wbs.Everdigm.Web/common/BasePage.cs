using System;
using System.Collections.Generic;

using System.Configuration;

namespace Wbs.Everdigm.Web
{
    /// <summary>
    /// 所有页面的基类，提供一些公共的方法
    /// </summary>
    public class BasePage : System.Web.UI.Page
    {
        /// <summary>
        /// 其他页面传过来的key值
        /// </summary>
        protected string _key = "";

        protected virtual void Page_Load(object sender, EventArgs e)
        {
            InitializeParamenters();
        }

        private int _pageSize = -1;
        /// <summary>
        /// 获取列表页大小
        /// </summary>
        protected int PageSize
        {
            get {
                if (-1 == _pageSize)
                {
                    _pageSize = int.Parse(ConfigurationManager.AppSettings["PageSize"]);
                }
                return _pageSize; 
            }
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
        /// 获取加密后的id列表
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        protected List<int> GetIdList(string[] str)
        {
            var list = new List<int>();
            foreach (var s in str)
            {
                list.Add(int.Parse(Utility.Decrypt(s)));
            }
            return list;
        }
        /// <summary>
        /// 获取id列表
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        protected List<int> GetIdList(string str)
        {
            var s = str.Split(new char[] { ',' });
            var list = new List<int>();
            foreach (var t in s)
            {
                list.Add(ParseInt(t));
            }
            return list;
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

        /// <summary>
        /// 显示分页信息
        /// </summary>
        /// <param name="_pageIndex">当前页码</param>
        /// <param name="_totalPages">总页数</param>
        /// <param name="_url">跳转 URL </param>
        /// <param name="_contaner">分页信息的显示控件</param>
        protected void ShowPaggings(int _pageIndex, int _totalPages, int _totalCount, string _url,
            System.Web.UI.HtmlControls.HtmlGenericControl _contaner)
        {
            string html = "<span style=\"cursor: pointer;\" title=\"\">Click here</span> go to page " +
                "<input type=\"text\" id=\"txtPage\" style=\"width: 20px;\" value=\"" +
                _pageIndex + "\">/" + _totalPages + ", total " + _totalCount + " records <a href=\"" + _url + "\" title=\"First\" class=\"_1\">&laquo;</a>";
            bool little = false, bigger = false;
            for (int i = 1; i <= _totalPages; i++)
            {
                if (i == _pageIndex)
                    html += "<a href=\"#\" class=\"number current\" title=\"page " + i + "\">" + i + "</a> ";
                else if (i < _pageIndex - 2)
                {
                    if (!little)
                    {
                        little = true;
                        html += ".. ";
                    }
                }
                else if (i >= _pageIndex - 2 && i <= _pageIndex + 2)
                {
                    html += "<a href=\"" + _url + "\" class=\"number\" title=\"page " + i + " \">" + i + "</a> ";
                }
                else
                {
                    if (!bigger)
                    {
                        html += " ..";
                        bigger = true;
                    }
                }
            }
            html += "<a href=\"" + _url + "\" title=\"Last\" class=\"_" + _totalPages + "\">&raquo;</a> ";
            _contaner.InnerHtml = html;
        }
    }
}