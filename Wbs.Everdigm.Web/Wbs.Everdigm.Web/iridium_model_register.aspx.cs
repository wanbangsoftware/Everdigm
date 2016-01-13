using System;
using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.Web
{
    public partial class iridium_model_register : BaseSatellitePage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            _cookie_name_ = "_satellite_register_list_page_";
            cookieName.Value = _cookie_name_;
            base.Page_Load(sender, e);
            if (!IsPostBack)
            {
                hidPageIndex.Value = null == Request.Cookies[_cookie_name_] ? "" : Request.Cookies[_cookie_name_].Value;
                ShowSatellites();
            }
        }
        /// <summary>
        /// 显示已经登记了的卫星号码列表
        /// </summary>
        private void ShowSatellites()
        {
            var number = txtQueryNumber.Value.Trim();
            number = string.IsNullOrEmpty(number) ? "" : number;
            var totalRecords = 0;
            var pageIndex = "" == hidPageIndex.Value ? 1 : int.Parse(hidPageIndex.Value);
            var list = SatelliteInstance.FindPageList<TB_Satellite>(pageIndex, PageSize, out totalRecords,
                f => f.CardNo.IndexOf(number) >= 0 && f.CardNo.Length > 10, "RegisterDate", true);
            var totalPages = totalRecords / PageSize + (totalRecords % PageSize > 0 ? 1 : 0);

            string html = "";
            if (totalRecords < 1)
            {
                html = "<tr><td colspan=\"7\">No records, you can change condition and try again, or " +
                    " <a>Add</a> some new one.</td></tr>";
            }
            else
            {
                var cnt = (pageIndex - 1) * PageSize;
                foreach (var obj in list)
                {
                    cnt++;
                    //var id = Utility.UrlEncode(Utility.Encrypt(obj.id.ToString()));
                    html += "<tr>" +
                        "<td style=\"text-align: center;\">" + cnt + "</td>" +
                        "<td><a style=\"cursor: pointer;\" data-toggle=\"modal\" data-target=\"#modalManufacturing\" data-whatever=\"" + obj.CardNo + "\" data-id=\"" + obj.id + "\">" + obj.CardNo + "</a></td>" +
                        "<td>" + obj.RegisterDate.Value.ToString("yyyy/MM/dd HH:mm:ss") + "</td>" +
                        "<td style=\"text-align: center;\">" + (obj.Bound.Value ? "Yes" : "No") + "</td>" +
                        "<td>" + (string.IsNullOrEmpty(obj.PcbNumber) ? "-" : obj.PcbNumber) + "</td>" +
                        "<td>" + (string.IsNullOrEmpty(obj.ManufactureDate) ? "-" : obj.ManufactureDate) + "</td>" +
                        "<td></td>" +
                        "</tr>";
                }
            }
            tbodyBody.InnerHtml = html;
            divPagging.InnerHtml = "";
            if (totalRecords > 0)
                ShowPaggings(pageIndex, totalPages, totalRecords, "iridium_model_register.aspx", divPagging);
        }

        protected void btQuery_Click(object sender, EventArgs e)
        {
            ShowSatellites();
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            // 保存新的铱星模块号码
            var number = txtQueryNumber.Value.Trim();
            var len = number.Length;
            if (len != 6 && len != 15)
            {
                ShowNotification("/iridium_model_register.aspx", "Your input is not a Iridium IMEI number.", false);
            }
            else
            {
                var pre = number.Substring(0, 3);
                if (pre != "306" && pre != "300")
                {
                    ShowNotification("/iridium_model_register.aspx", "Your input is not a Iridium IMEI number.", false);
                }
                else
                {
                    var obj = SatelliteInstance.Find(f => f.CardNo.Equals(number));
                    if (null != obj)
                    {
                        ShowNotification("/iridium_model_register.aspx", "There have a SAME satellite IMEI number exist.", false);
                    }
                    else
                    {
                        var n = SatelliteInstance.GetObject();
                        n.CardNo = number;
                        SatelliteInstance.Add(n);
                        ShowSatellites();
                    }
                }
            }
        }
    }
}