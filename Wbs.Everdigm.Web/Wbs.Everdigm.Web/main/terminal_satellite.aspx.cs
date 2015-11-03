using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.Web.main
{
    public partial class terminal_satellite : BaseSatellitePage
    {
        private static int _page_size = 5;

        protected override void Page_Load(object sender, EventArgs e)
        {
            _cookie_name_ = "_terminal_satellite_page_";
            cookieName.Value = _cookie_name_;
            base.Page_Load(sender, e);
            if (!IsPostBack)
            {
                hidID.Value = _key;
                hidPageIndex.Value = null == Request.Cookies[_cookie_name_] ? "" : Request.Cookies[_cookie_name_].Value;
                ShowSatellites();
            }
        }
        /// <summary>
        /// 显示未绑定终端的卫星号码列表
        /// </summary>
        private void ShowSatellites()
        {
            var totalRecords = 0;
            var pageIndex = "" == hidPageIndex.Value ? 1 : int.Parse(hidPageIndex.Value);
            var list = SatelliteInstance.FindPageList<TB_Satellite>(pageIndex, _page_size, out totalRecords,
                f => f.Bound == false, "CardNo");
            var totalPages = totalRecords / _page_size + (totalRecords % _page_size > 0 ? 1 : 0);

            string html = "";
            if (totalRecords < 1)
            {
                html = "<tr><td colspan=\"6\">No records</td></tr>";
            }
            else
            {
                var cnt = (pageIndex - 1) * _page_size;
                foreach (var obj in list)
                {
                    cnt++;
                    var id = Utility.UrlEncode(Utility.Encrypt(obj.id.ToString()));
                    html += "<tr>" +
                                "<td style=\"text-align: center;\">" +
                                    "<input type=\"radio\" name=\"satId\" id=\"cb_" + id + "\" /></td>" +
                                "<td style=\"text-align: center;\">" + cnt + "</td>" +
                                "<td>" + obj.CardNo + "</td>" +
                                "<td>" + obj.Bound.Value + "</td>" +
                                "<td>" + obj.RegisterDate.Value.ToString("yyyy-MM-dd") + "</td>" +
                                "<td></td>" +
                            "</tr>";
                }
            }
            tbodyBody.InnerHtml = html;
            divPagging.InnerHtml = "";
            if (totalRecords > 0)
                ShowPaggings(pageIndex, totalPages, totalRecords, "./terminal_satellite.aspx", divPagging);
        }

        protected void btQuery_Click(object sender, EventArgs e)
        {
            ShowSatellites();
        }
    }
}