using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.Web.main
{
    public partial class user_history : BaseAccountPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            _cookie_name_ = "_user_history_list_page_";
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
            if (!HasSessionLose)
                ShowHistory();
        }

        private string CheckQueryString(string query, string obj)
        {
            return string.IsNullOrEmpty(query) ? obj : obj.Replace(query, ("<font style=\"color: #FF0000;\">" + query + "</font>"));
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

            var name = txtName.Value.Trim();
            var login = -1;
            if (cbIgnoreLogin.Checked)
            {
                login = ActionInstance.Find(f => f.Name.Equals("Login")).id;
            }
            var totalRecords = 0;
            var pageIndex = "" == hidPageIndex.Value ? 1 : int.Parse(hidPageIndex.Value);

            Expression<Func<TB_AccountHistory, bool>> expression = PredicateExtensions.True<TB_AccountHistory>();
            // 时间查询
            expression = expression.And(a => a.ActionTime >= then && a.ActionTime <= now);
            // 姓名模糊查询
            if (!string.IsNullOrEmpty(name))
            {
                pageIndex = 1;
                expression = expression.And(a => a.TB_Account.Name.Contains(name));
            }
            // 屏蔽登录信息
            if (login > 0)
            {
                expression = expression.And(a => a.ActionId != login);
            }
            // Summary模糊查询
            var summary = txtSummary.Value.Trim();
            if (!string.IsNullOrEmpty(summary))
            {
                pageIndex = 1;
                expression = expression.And(a => a.ObjectA.Contains(summary));
            }

            var list = HistoryInstance.FindPageList<TB_AccountHistory>(pageIndex, PageSize, out totalRecords, expression, "ActionTime", true);
            var totalPages = totalRecords / PageSize + (totalRecords % PageSize > 0 ? 1 : 0);
            string html = "";
            if (totalRecords < 1)
            {
                html = "<tr><td colspan=\"6\">No records.</td></tr>";
            }
            else
            {
                var cnt = (pageIndex - 1) * PageSize;
                foreach (var obj in list)
                {
                    cnt++;
                    html += "<tr>" +
                        "<td style=\"width: 60px; text-align: center; height: 25px;\">" + cnt + "</td>" +
                        "<td style=\"width: 150px;\">" + obj.ActionTime.Value.ToString("yyyy/MM/dd HH:mm:ss") + "</td>" +
                        "<td style=\"width: 150px;\">" + CheckQueryString(name, obj.TB_Account.Name) + "</td>" +
                        "<td style=\"width: 100px;\"><a href=\"#u\">" + obj.TB_AccountAction.Description + "</a></td>" +
                        "<td>" + obj.Ip + "</td>" +
                        "<td>" + CheckQueryString(summary, obj.ObjectA) + "</td>" +
                        "</tr>";
                }
            }
            tbodyBody.InnerHtml = html;
            divPagging.InnerHtml = "";
            if (totalRecords > 0)
                ShowPaggings(pageIndex, totalPages, totalRecords, "./user_history.aspx", divPagging);
        }
    }
}