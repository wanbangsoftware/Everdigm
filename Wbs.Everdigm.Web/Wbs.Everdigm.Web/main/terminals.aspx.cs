using System;
using Wbs.Everdigm.Database;
using Wbs.Protocol;
using Wbs.Everdigm.Common;

namespace Wbs.Everdigm.Web.main
{
    public partial class terminals : BaseTerminalPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            _cookie_name_ = "_terminals_bind_page_";
            cookieName.Value = _cookie_name_;
            base.Page_Load(sender, e);
            if (!HasSessionLose)
            {
                if (!IsPostBack)
                {
                    hidPageIndex.Value = null == Request.Cookies[_cookie_name_] ? "" : Request.Cookies[_cookie_name_].Value;
                    ShowTerminals();
                }
            }
        }
        private byte TerminalType
        {
            get
            {
                var k = byte.Parse(_key);
                var ef = (EquipmentFunctional)k;
                byte ret = TerminalTypes.DX;
                switch (ef)
                {
                    case EquipmentFunctional.Electric: ret = TerminalTypes.DXE; break;
                    case EquipmentFunctional.Loader: ret = TerminalTypes.LD; break;
                    case EquipmentFunctional.Mechanical: ret = TerminalTypes.DX; break;
                }
                return ret;
            }
        }
        private void ShowTerminals()
        {
            var totalRecords = 0;
            var pageIndex = "" == hidPageIndex.Value ? 1 : int.Parse(hidPageIndex.Value);
            var list = TerminalInstance.FindPageList<TB_Terminal>(pageIndex, PageSize, out totalRecords,
                f => f.Delete == false && f.HasBound == false && f.Type == TerminalType, "Number");
            var totalPages = totalRecords / PageSize + (totalRecords % PageSize > 0 ? 1 : 0);

            string html = "";
            if (totalRecords < 1)
            {
                html = "<tr><td colspan=\"6\">No records</tr>";
            }
            else
            {
                var cnt = (pageIndex - 1) * PageSize;
                var n = (int?)null;
                foreach (var obj in list)
                {
                    cnt++;
                    var id = Utility.UrlEncode(Utility.Encrypt(obj.id.ToString()));
                    html += "<tr>" +
                        "<td style=\"text-align: center;\"><input type=\"radio\" name=\"satId\" id=\"cb_" + id + "\" /></td>" +
                        "<td style=\"text-align: center;\">" + cnt + "</td>" +
                        "<td><a>" + obj.Number + "</a></td>" +
                        "<td>" + obj.Sim + "</td>" +
                        "<td>" + (n == obj.Satellite ? "-" : obj.TB_Satellite.CardNo) + "</td>" +
                        "<td>" + TerminalTypes.GetTerminalType(obj.Type.Value) + "</td>" +
                        "<td></td>" +
                        "</tr>";
                }
            }
            tbodyBody.InnerHtml = html;
            divPagging.InnerHtml = "";
            if (totalRecords > 0)
                ShowPaggings(pageIndex, totalPages, totalRecords, "./terminals.aspx", divPagging);
        }
    }
}