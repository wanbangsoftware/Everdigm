using System;
using System.Linq;
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

        private void ShowFlow()
        {
            var month = int.Parse(selectedMonths.Value);
            spanMonthly.InnerText = string.Format("for {0}{1:00}", selectedYears.Value, (month == 0 ? " full year" : ("/" + month.ToString("00"))));

            var query = txtQueryNumber.Value.Trim();
            // 模糊查询时页码置为空
            if (!string.IsNullOrEmpty(query)) { hidPageIndex.Value = ""; }

            var totalRecords = 0;
            var pageIndex = "" == hidPageIndex.Value ? 1 : int.Parse(hidPageIndex.Value);
            var list = TerminalInstance.FindPageList<TB_Terminal>(pageIndex, PageSize, out totalRecords,
                f => f.Delete == false && (f.Sim.Contains(query) || f.TB_Satellite.CardNo.Contains(query)), "Number");
            var totalPages = totalRecords / PageSize + (totalRecords % PageSize > 0 ? 1 : 0);

            var monthly = int.Parse(string.Format("{0}{1:00}", selectedYears.Value, month));
            string html = "";
            if (totalRecords < 1)
            {
                html = "<tr><td colspan=\"13\">No records, Change condition and try again.</td></tr>";
            }
            else
            {
                var cnt = (pageIndex - 1) * PageSize;
                var n = (int?)null;
                foreach (var obj in list)
                {
                    cnt++;
                    // var id = Utility.UrlEncode(Utility.Encrypt(obj.id.ToString()));
                    // 查找绑定的设备信息
                    var equipment = obj.TB_Equipment.FirstOrDefault();
                    // 查找选定月的流量统计
                    var flow = obj.TB_TerminalFlow.Where<TB_TerminalFlow>(o => (month == 0 ?
                        (o.Monthly >= monthly && o.Monthly <= monthly + 100) :
                        o.Monthly == monthly));

                    var _flow = FlowInstance.GetObject();
                    _flow.GPRSDeliver = flow.Select(s => s.GPRSDeliver).Sum();
                    _flow.GPRSReceive = flow.Select(s => s.GPRSReceive).Sum();
                    _flow.SMSDeliver = flow.Select(s => s.SMSDeliver).Sum();
                    _flow.SMSReceive = flow.Select(s => s.SMSReceive).Sum();
                    // 查找选定月的铱星流量
                    var _sat = IridiumInstance.GetObject();
                    if (obj.Satellite != n)
                    {
                        var sat = obj.TB_Satellite.TB_IridiumFlow.Where(w => (month == 0 ?
                            (w.Monthly >= monthly && w.Monthly <= monthly + 100) : w.Monthly == monthly));
                        _sat.MOPayload = sat.Select(s => s.MOPayload).Sum();
                        _sat.MOTimes = sat.Select(s => s.MOTimes).Sum();
                        _sat.MTPayload = sat.Select(s => s.MTPayload).Sum();
                        _sat.MTTimes = sat.Select(s => s.MTTimes).Sum();
                    }

                    html += "<tr>" +
                        "<td class=\"in-tab-txt-b\">" + cnt + "</td>" +
                        "<td class=\"in-tab-txt-b\" style=\"text-align: left;\">" + (null == equipment ? "not bind" : EquipmentInstance.GetFullNumber(equipment)) + "</td>" +
                        "<td class=\"in-tab-txt-rb\" style=\"text-align: left;\">" + obj.Number + "</td>" +
                        "<td class=\"in-tab-txt-b\">" + CustomConvert.FormatSize(_flow.GPRSReceive.Value) + "</td>" +
                        "<td class=\"in-tab-txt-rb\">" + CustomConvert.FormatSize(_flow.GPRSDeliver.Value) + "</td>" +
                        "<td class=\"in-tab-txt-b\">" + _flow.SMSReceive.Value.ToString() + "</td>" +
                        "<td class=\"in-tab-txt-rb\">" + _flow.SMSDeliver.Value.ToString() + "</td>" +
                        "<td class=\"in-tab-txt-rb\" style=\"text-align: left;\">" + (n == obj.Satellite ? "not bind" : obj.TB_Satellite.CardNo) + "</td>" +
                        "<td class=\"in-tab-txt-b\">" + _sat.MOTimes.Value.ToString() + "</td>" +
                        "<td class=\"in-tab-txt-rb\">" + CustomConvert.FormatSize(_sat.MOPayload.Value) + "</td>" +
                        "<td class=\"in-tab-txt-b\">" + _sat.MTTimes.Value.ToString() + "</td>" +
                        "<td class=\"in-tab-txt-b\">" + CustomConvert.FormatSize(_sat.MTPayload.Value) + "</td>" +
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