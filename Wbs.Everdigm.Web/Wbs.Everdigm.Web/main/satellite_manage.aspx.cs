using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.Web.main
{
    public partial class satellite_manage : BaseSatellitePage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            _cookie_name_ = "_satellite_list_page_";
            cookieName.Value = _cookie_name_;
            base.Page_Load(sender, e);
            if (!HasSessionLose)
            {
                if (!IsPostBack)
                {
                    hidPageIndex.Value = null == Request.Cookies[_cookie_name_] ? "" : Request.Cookies[_cookie_name_].Value;
                    ShowSatellites();

                    if (!string.IsNullOrEmpty(_key))
                    {
                        hidID.Value = _key;
                        // 显示编辑信息
                        ShowEdit();
                    }
                }
            }
            else
            { ShowNotification("../default.aspx", "Your session has expired, please login again.", false, true); }
        }

        private void ShowSatellites()
        {
            var totalRecords = 0;
            var pageIndex = "" == hidPageIndex.Value ? 1 : int.Parse(hidPageIndex.Value);
            var list = SatelliteInstance.FindPageList<TB_Satellite>(pageIndex, PageSize, out totalRecords, null, "CardNo");
            var totalPages = totalRecords / PageSize + (totalRecords % PageSize > 0 ? 1 : 0);
            pageIndex = 0 == pageIndex ? totalPages : pageIndex;
            if (pageIndex >= totalPages)
            {
                pageIndex = totalPages;
                list = SatelliteInstance.FindPageList<TB_Satellite>(pageIndex, PageSize, out totalRecords, null, "CardNo");
            }

            string html = "";
            if (totalRecords < 1)
            {
                html = "<tr><td colspan=\"6\">No records, you can change condition and try again, or " +
                    " <a>Add</a> some new one.</td></tr>";
            }
            else
            {
                var cnt = (pageIndex - 1) * PageSize;
                foreach (var obj in list)
                {
                    cnt++;
                    var id = Utility.UrlEncode(Utility.Encrypt(obj.id.ToString()));
                    html += "<tr>" +
                        // 系统默认角色无法删除
                        "<td style=\"width: 40px; text-align: center;\"><input type=\"checkbox\" id=\"cb_" + id + "\" /></td>" +
                        "<td style=\"width: 40px; text-align: center;\">" + cnt + "</td>" +
                        "<td><a href=\"./satellite_manage.aspx?key=" + id + "\" >" + obj.CardNo + "</a></td>" +
                        "<td>" + obj.RegisterDate.Value.ToString("yyyy-MM-dd") + "</td>" +
                        "<td>" + obj.Bound.ToString() + "</td>" +
                        "<td></td>" +
                        "</tr>";
                }
            }
            tbodyBody.InnerHtml = html;
            divPagging.InnerHtml = "";
            if (totalRecords > 0)
                ShowPaggings(pageIndex, totalPages, totalRecords, "./satellite_manage.aspx", divPagging);
        }

        private void ShowEdit()
        {
            var t = SatelliteInstance.Find(f => f.id == ParseInt(Utility.Decrypt(hidID.Value)));
            if (null != t)
            {
                txtNumber.Value = t.CardNo;
                btSave.Text = "Save";
            }
            else
            {
                ShowNotification("./satellite_manage.aspx", "Error: Cannot edit null object of <a>Satellite Item</a>.", false);
            }
        }

        private void NewSatellite()
        {
            var num = txtNumber.Value.Trim();
            var t = SatelliteInstance.Find(f => f.CardNo.Equals(num));
            if (null != t)
            {
                ShowNotification("./satellite_manage.aspx", "Error: A old one has the same card number <a>" + num + "</a>.", false);
                return;
            }
            else
            {
                var obj = SatelliteInstance.GetObject();
                obj.CardNo = num;
                SatelliteInstance.Add(obj);

                SaveHistory(new TB_AccountHistory()
                {
                    ActionId = ActionInstance.Find(f => f.Name.Equals("AddSat")).id,
                    ObjectA = SatelliteInstance.ToString(obj)
                });
                ShowNotification("./satellite_manage.aspx", "You add a new satellite object.");
            }
        }

        private void EditSatellite()
        {
            var num = txtNumber.Value.Trim();
            var t = SatelliteInstance.Find(f => f.id == ParseInt(Utility.Decrypt(hidID.Value)));
            if (null != t)
            {
                var old = SatelliteInstance.Find(f => f.CardNo.Equals(num));
                if (null != old && old.id != t.id)
                { ShowNotification("./satellite_manage.aspx", "Cannot edit the satellite object: The number has exist.", false); }
                else
                {
                    SatelliteInstance.Update(f => f.id == t.id, act => { act.CardNo = num; });
                    SaveHistory(new TB_AccountHistory()
                    {
                        ActionId = ActionInstance.Find(f => f.Name.Equals("EditSat")).id,
                        ObjectA = SatelliteInstance.ToString(t)
                    });
                    ShowNotification("./satellite_manage.aspx", "You saved the satellite information.");
                }
            }
            else
            { ShowNotification("./satellite_manage.aspx", "Cannot edit the satellite object: no record exist.", false); }
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            if (!HasSessionLose)
            {
                if ("" == hidID.Value)
                { NewSatellite(); }
                else
                { EditSatellite(); }
            }
        }

        protected void btQuery_Click(object sender, EventArgs e)
        {
            ShowSatellites();
        }
    }
}