using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.Web.service
{
    public partial class as_trackers : BaseTrackerPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            _cookie_name_ = "_as_trackers_list_page_";
            cookieName.Value = _cookie_name_;
            base.Page_Load(sender, e);
            if (!HasSessionLose)
            {
                if (!IsPostBack)
                {
                    hidPageIndex.Value = null == Request.Cookies[_cookie_name_] ? "1" : Request.Cookies[_cookie_name_].Value;
                    ShowTrackers();
                }
            }
        }

        private void ShowTrackers()
        {
            var query = txtQueryNumber.Value.Trim();
            var totalRecords = 0;
            var pageIndex = "" == hidPageIndex.Value ? 1 : int.Parse(hidPageIndex.Value);
            pageIndex = (0 >= pageIndex ? 1 : pageIndex);
            var list = TrackerInstance.FindPageList<TB_Tracker>(pageIndex, PageSize, out totalRecords,
                f => (f.SimCard.IndexOf(query) >= 0 || f.CarNumber.IndexOf(query) >= 0) && f.Deleted == false, "SimCard");
            var totalPages = totalRecords / PageSize + (totalRecords % PageSize > 0 ? 1 : 0);
            hidTotalPages.Value = totalPages.ToString();

            string html = "";
            if (totalRecords < 1)
            {
                html = "<tr><td colspan=\"10\">No records, Change condition to try again.</td></tr>";
            }
            else
            {
                var cnt = (pageIndex - 1) * PageSize;
                var d = (DateTime?)null;
                var fmt = "yyyy/MM/dd HH:mm:ss";
                foreach (var obj in list)
                {
                    cnt++;
                    var id = Utility.UrlEncode(Utility.Encrypt(obj.id.ToString()));

                    html += "<tr>" +
                        "<td class=\"in-tab-txt-b\">" + cnt + "</td>" +
                        "<td class=\"in-tab-txt-b\" style=\"text-align: left;\"><a href=\"./as_tracker.aspx?key=" + id + "\">" + obj.SimCard + "</a></td>" +
                        "<td class=\"in-tab-txt-b\">" + (d == obj.LastActionAt ? "-" : obj.LastActionAt.Value.ToString(fmt)) + "</td>" +
                        "<td class=\"in-tab-txt-b\">" + (d == obj.ChargingAlarm ? "-" : obj.ChargingAlarm.Value.ToString(fmt)) + "</td>" +
                        "<td class=\"in-tab-txt-b\">" + (d == obj.BatteryAlarm ? "-" : obj.BatteryAlarm.Value.ToString(fmt)) + "</td>" +
                        "<td class=\"in-tab-txt-b\">" + (d == obj.ParkingAlarm ? "-" : obj.ParkingAlarm.Value.ToString(fmt)) + "</td>" +
                        "<td class=\"in-tab-txt-b\">" + obj.CarNumber + "</td>" +
                        "<td class=\"in-tab-txt-b\">" + obj.Director + "</td>" +
                        "<td class=\"in-tab-txt-b\" style=\"text-align: left;\">" + obj.Address + "</td>" +
                        "<td class=\"in-tab-txt-b\"></td>" +
                        "</tr>";
                }
            }
            tbodyBody.InnerHtml = html;
            divPagging.InnerHtml = "";
            if (totalRecords > 0)
                ShowPaggings(pageIndex, totalPages, totalRecords, "./as_trackers.aspx", divPagging);
        }

        protected void btQuery_Click(object sender, EventArgs e)
        {
            if (!HasSessionLose)
            { ShowTrackers(); }
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            var value = hiddenId.Value;
            if (string.IsNullOrEmpty(value)) { New(); }
            else
            {
                var id = int.Parse(Utility.Decrypt(Utility.UrlDecode(hiddenId.Value)));
                Edit(id);
            }
        }

        private void New() {
            var old = TrackerInstance.Find(f => f.SimCard.Equals(number.Value.Trim())&&f.Deleted==false);
            if (null == old)
            {
                var newOne = TrackerInstance.GetObject();
                newOne.SimCard = number.Value.Trim();
                newOne.CarNumber = vehicle.Value.Trim();
                newOne.Director = director.Value.Trim();
                TrackerInstance.Add(newOne);

                // 保存操作历史记录
                SaveHistory(new TB_AccountHistory()
                {
                    ActionId = ActionInstance.Find(f => f.Name.Equals("AddTracker")).id,
                    ObjectA = TrackerInstance.ToString(newOne)
                });
                ShowNotification("/service/as_trackers.aspx", "New tracker added.");
            }
            else
            {
                ShowNotification("/service/as_trackers.aspx", "Same number of " + number.Value.Trim() + " exists.", false);
            }
        }

        private void Edit(int id) {
            var old = TrackerInstance.Find(f => f.id == id && f.Deleted == false);
            if (null == old)
            {
                ShowNotification("/service/as_trackers.aspx", "Cannot find the tracker.", false);
            }
            else
            {
                TrackerInstance.Update(f => f.id == id && f.Deleted == false, act =>
                {
                    act.CarNumber = vehicle.Value.Trim();
                    act.Director = director.Value.Trim();
                });
                // 保存操作历史记录
                SaveHistory(new TB_AccountHistory()
                {
                    ActionId = ActionInstance.Find(f => f.Name.Equals("EditTracker")).id,
                    ObjectA = TrackerInstance.ToString(old)
                });
                ShowNotification("/service/as_trackers.aspx", "Tracker information has been saved.");
            }
        }
    }
}