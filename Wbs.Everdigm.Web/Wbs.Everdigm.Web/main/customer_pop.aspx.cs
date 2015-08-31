using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.Web.main
{
    public partial class customer_pop : BaseCustomerPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            _cookie_name_ = "_customer_pop_list_page_";
            cookieName.Value = _cookie_name_;
            base.Page_Load(sender, e);
            if (!HasSessionLose)
            {
                if (!IsPostBack)
                {
                    hidPageIndex.Value = null == Request.Cookies[_cookie_name_] ? "" : Request.Cookies[_cookie_name_].Value;
                    ShowCustomers();
                }
            }
        }

        private void ShowCustomers()
        {
            var query = txtName.Value.Trim();
            var totalRecords = 0;
            var pageIndex = "" == hidPageIndex.Value ? 1 : int.Parse(hidPageIndex.Value);
            pageIndex = (0 >= pageIndex ? 1 : pageIndex);
            var list = CustomerInstance.FindPageList<TB_Customer>(pageIndex, PageSize, out totalRecords,
                f => f.Name.IndexOf(query) >= 0 && f.Delete == false, "Name");
            var totalPages = totalRecords / PageSize + (totalRecords % PageSize > 0 ? 1 : 0);
            pageIndex = 0 == pageIndex ? totalPages : pageIndex;
            if (totalRecords > 0 && pageIndex > totalPages)
            {
                pageIndex = totalPages;
                list = CustomerInstance.FindPageList<TB_Customer>(pageIndex, PageSize, out totalRecords,
                    f => f.Name.IndexOf(query) >= 0 && f.Delete == false, "Name");
            }

            string html = "";
            if (totalRecords < 1)
            {
                html = "<tr><td colspan=\"7\">No records, You can change the condition and try again.</td></tr>";
            }
            else
            {
                var cnt = (pageIndex - 1) * PageSize;
                foreach (var obj in list)
                {
                    cnt++;
                    var id = Utility.UrlEncode(Utility.Encrypt(obj.id.ToString()));
                    html += "<tr>" +
                        "<td style=\"text-align: center;\"><input type=\"radio\" name=\"satId\" id=\"cb_" + id + "\" /></td>" +
                        "<td style=\"text-align: center;\">" + cnt + "</td>" +
                        "<td><a>" + obj.Name + "</a></td>" +
                        "<td>" + obj.Code + "</td>" +
                        "<td>" + obj.Phone + "</td>" +
                        "<td>" + obj.Fax + "</td>" +
                        "<td></td>" +
                        "</tr>";
                }
            }
            tbodyBody.InnerHtml = html;
            divPagging.InnerHtml = "";
            if (totalRecords > 0)
                ShowPaggings(pageIndex, totalPages, totalRecords, "./customer_pop.aspx", divPagging);
        }

        protected void btQuery_Click(object sender, EventArgs e)
        {
            if (!HasSessionLose)
                ShowCustomers();
        }
    }
}