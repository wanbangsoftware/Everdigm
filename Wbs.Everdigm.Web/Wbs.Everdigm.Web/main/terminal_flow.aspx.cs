using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Wbs.Everdigm.Database;
using Wbs.Utilities;

namespace Wbs.Everdigm.Web.main
{
    public partial class terminal_flow : BaseTerminalPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            _cookie_name_ = "_terminal_flow_list_page_";
            cookieName.Value = _cookie_name_;
            base.Page_Load(sender, e);
            if (!HasSessionLose)
            {
                if (!IsPostBack)
                {
                    hidPageIndex.Value = null == Request.Cookies[_cookie_name_] ? "" : Request.Cookies[_cookie_name_].Value;
                    initYears();
                    ShowFlow();
                }
            }
        }

        private void initYears()
        {
            int year = DateTime.Now.Year;
            selectedYears.Value = year.ToString();
            string html = "";
            for (int i = year - 1; i <= year; i++)
            {
                html += "<li role=\"presentation\"><a role=\"menuitem\" tabindex=\"" + i.ToString() + "\" href=\"#\">" + i.ToString() + "</a></li>";
            }
            menuYears.InnerHtml = html;
            initMonth();
        }

        private void initMonth()
        {
            int month = DateTime.Now.Month;
            selectedMonths.Value = month.ToString("00");
            string html = "";
            for (int i = 0; i <= 12; i++)
            {
                string value = (i == 0 ? "Full year" : i.ToString("00th"));
                html += "<li role=\"presentation\"><a role=\"menuitem\" tabindex=\"" +
                    i.ToString("00") + "\" href=\"#\">" + value + "</a></li>";
            }
            menuMonths.InnerHtml = html;
        }

        protected void btQuery_Click(object sender, EventArgs e)
        {
            if (!HasSessionLose)
            {
                ShowFlow();
            }
        }

        private void ShowFlow() {
            var query = txtQueryNumber.Value.Trim();
            var totalRecords = 0;
            var pageIndex = "" == hidPageIndex.Value ? 1 : int.Parse(hidPageIndex.Value);
            var list = TerminalInstance.FindPageList<TB_Terminal>(pageIndex, PageSize, out totalRecords,
                f => f.Delete == false && (f.Sim.Contains(query) || f.TB_Satellite.CardNo.Contains(query)), "Number");
            var totalPages = totalRecords / PageSize + (totalRecords % PageSize > 0 ? 1 : 0);
            pageIndex = 0 == pageIndex ? totalPages : pageIndex;
            if (pageIndex > totalPages)
            {
                pageIndex = totalPages;
                list = TerminalInstance.FindPageList<TB_Terminal>(pageIndex, PageSize, out totalRecords,
                    f => f.Delete == false && (f.Sim.Contains(query) || f.TB_Satellite.CardNo.Contains(query)), "Number");
            }
            var month = int.Parse(selectedMonths.Value);
            spanMonthly.InnerText = string.Format("for {0}/{1:00}", selectedYears.Value, month);
            var monthly = int.Parse(string.Format("{0}{1:00}", selectedYears.Value, month));
            string html = "";
            if (totalRecords < 1)
            {
                html = "<tr><td colspan=\"14\">No records, Change condition and try again.</td></tr>";
            }
            else
            {
                var cnt = (pageIndex - 1) * PageSize;
                var n = (int?)null;
                foreach (var obj in list)
                {
                    cnt++;
                    var id = Utility.UrlEncode(Utility.Encrypt(obj.id.ToString()));
                    // 查找绑定的设备信息
                    var equipment = obj.TB_Equipment.FirstOrDefault();
                    // 查找选定月的流量统计
                    var flow = obj.TB_TerminalFlow.FirstOrDefault(o => o.Monthly == monthly);
                    // 查找选定月的铱星流量
                    var sat = obj.Satellite == n ? null : (obj.TB_Satellite.TB_IridiumFlow.FirstOrDefault(s => s.Monthly == monthly));

                    html += "<tr>" +
                        "<td class=\"in-tab-txt-b\">" + cnt + "</td>" +
                        "<td class=\"in-tab-txt-b\" style=\"text-align: left;\">" + (null == equipment ? "not bind" : EquipmentInstance.GetFullNumber(equipment)) + "</td>" +
                        "<td class=\"in-tab-txt-b\" style=\"text-align: left;\">" + obj.Number + "</td>" +
                        "<td class=\"in-tab-txt-rb\" style=\"text-align: left;\">" + obj.Sim + "</td>" +
                        "<td class=\"in-tab-txt-b\">" + (null == flow ? "0" : CustomConvert.FormatSize(flow.GPRSReceive.Value)) + "</td>" +
                        "<td class=\"in-tab-txt-rb\">" + (null == flow ? "0" : CustomConvert.FormatSize(flow.GPRSDeliver.Value)) + "</td>" +
                        "<td class=\"in-tab-txt-b\">" + (null == flow ? "0" : flow.SMSReceive.Value.ToString()) + "</td>" +
                        "<td class=\"in-tab-txt-rb\">" + (null == flow ? "0" : flow.SMSDeliver.Value.ToString()) + "</td>" +
                        "<td class=\"in-tab-txt-b\" style=\"text-align: left;\">" + (n == obj.Satellite ? "not bind" : obj.TB_Satellite.CardNo) + "</td>" +
                        "<td class=\"in-tab-txt-b\">" + (null == sat ? "0" : sat.MOTimes.Value.ToString()) + "</td>" +
                        "<td class=\"in-tab-txt-rb\">" + (null == sat ? "0" : CustomConvert.FormatSize(sat.MOPayload.Value)) + "</td>" +
                        "<td class=\"in-tab-txt-b\">" + (null == sat ? "0" : sat.MTTimes.Value.ToString()) + "</td>" +
                        "<td class=\"in-tab-txt-b\">" + (null == sat ? "0" : CustomConvert.FormatSize(sat.MTPayload.Value)) + "</td>" +
                        "<td class=\"in-tab-txt-b\"></td>" +
                        "</tr>";
                }
            }
            tbodyBody.InnerHtml = html;
            divPagging.InnerHtml = "";
            if (totalRecords > 0)
                ShowPaggings(pageIndex, totalPages, totalRecords, "./terminal_flow.aspx", divPagging);
        }
    }
}