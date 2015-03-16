using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.Web.main
{
    public partial class account_history : BaseAccountPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            _cookie_name_ = "_account_history_list_page_";
            cookieName.Value = _cookie_name_;
            base.Page_Load(sender, e);
            if (!HasSessionLose)
            {
                if (!IsPostBack)
                {
                    hidPageIndex.Value = null == Request.Cookies[_cookie_name_] ? "" : Request.Cookies[_cookie_name_].Value;
                    ShowHistory();
                }
            }
        }

        protected void btQuery_Click(object sender, EventArgs e)
        {
            ShowHistory();
        }

        private void ShowHistory()
        {
            var now = DateTime.Now;
            var then = now.AddDays(-5);
            var time1 = start.Value;
            if (!string.IsNullOrEmpty(time1))
            {
                then = DateTime.Parse(time1 + " 00:00:00");
            }
            var time2 = end.Value;
            if (!string.IsNullOrEmpty(time2))
            {
                now = DateTime.Parse(time2 + " 23:59:59");
            }

            var totalRecords = 0;
            var pageIndex = "" == hidPageIndex.Value ? 1 : int.Parse(hidPageIndex.Value);
            var list = HistoryInstance.FindPageList<TB_AccountHistory>(pageIndex, PageSize, out totalRecords,
                f => f.ActionTime >= then && f.ActionTime <= now && f.Account == Account.id, "ActionTime", true);
            var totalPages = totalRecords / PageSize + (totalRecords % PageSize > 0 ? 1 : 0);
            pageIndex = 0 == pageIndex ? totalPages : pageIndex;
            if (pageIndex >= totalPages)
            {
                pageIndex = totalPages;
                list = HistoryInstance.FindPageList<TB_AccountHistory>(pageIndex, PageSize, out totalRecords,
                f => f.ActionTime >= then && f.ActionTime <= now && f.Account == Account.id, "ActionTime", true);
            }

            string html = "";
            if (totalRecords < 1)
            {
                html = "<tr><td colspan=\"5\">No records.</td></tr>";
            }
            else
            {
                var cnt = (pageIndex - 1) * PageSize;
                foreach (var obj in list)
                {
                    cnt++;
                    html += "<tr>" +
                        "<td style=\"text-align: center; height: 25px;\">" + cnt + "</td>" +
                        "<td>" + obj.ActionTime.Value.ToString("yyyy/MM/dd HH:mm:ss") + "</td>" +
                        "<td><a href=\"#u\">" + obj.TB_AccountAction.Description + "</a></td>" +
                        "<td>" + obj.Ip + "</td>" +
                        "<td>" + obj.ObjectA + "</td>" +
                        "</tr>";
                }
            }
            tbodyBody.InnerHtml = html;
            divPagging.InnerHtml = "";
            if (totalRecords > 0)
                ShowPaggings(pageIndex, totalPages, totalRecords, "./account_history.aspx", divPagging);
        }
    }
}