using System;
using System.Linq;
using System.Linq.Expressions;
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
            //if (!HasSessionLose)
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
            if (!string.IsNullOrEmpty(query)) { hidPageIndex.Value = ""; }

            var totalRecords = 0;
            var pageIndex = "" == hidPageIndex.Value ? 1 : int.Parse(hidPageIndex.Value);
            pageIndex = (0 >= pageIndex ? 1 : pageIndex);
            Expression<Func<TB_Tracker, bool>> expression = PredicateExtensions.True<TB_Tracker>();
            if (!string.IsNullOrEmpty(query))
            {
                expression = expression.And(a => a.CarNumber.Contains(query));
            }
            expression = expression.And(a => a.Deleted == false);
            var list = TrackerInstance.FindPageList<TB_Tracker>(pageIndex, PageSize, out totalRecords, expression, "SimCard");
            var totalPages = totalRecords / PageSize + (totalRecords % PageSize > 0 ? 1 : 0);
            hidTotalPages.Value = totalPages.ToString();

            string html = "";
            if (totalRecords < 1)
            {
                html = "<tr><td colspan=\"12\">No records, Change condition to try again.</td></tr>";
            }
            else
            {
                var cnt = (pageIndex - 1) * PageSize;
                var d = (DateTime?)null;
                var fmt = "yyyy/MM/dd HH:mm:ss";
                var td = "<td class=\"in-tab-txt-b\" style=\"text-align: left;\">";
                foreach (var obj in list)
                {
                    cnt++;
                    var id = Utility.UrlEncode(Utility.Encrypt(obj.id.ToString()));
                    bool notchatable = null == obj.LastActionAt || obj.LastActionAt <= DateTime.Now.AddDays(-10);
                    var chat = notchatable ? "gray" : "attention";
                    var chattitle = notchatable ? "" : "notify this guy";
                    var chatLink = notchatable ? "#" : "#chat_" + id;
                    var setting = notchatable ? "warning" : "success";
                    var settingTitle = "Change the report period" + (notchatable ? "(maybe cannot take effect)" : "");
                    var user = obj.TB_Account.Count > 0 ? obj.TB_Account.FirstOrDefault(f => f.Tracker == obj.id) : null;
                    html += "<tr>" +
                        "<td class=\"in-tab-txt-b\">" + cnt + "</td>" +
                        td + "<a href=\"./as_tracker.aspx?key=" + id + "\">" + obj.SimCard + "</a></td>" +
                        td + "<a href=\"#sett_" + id + "\"><span class=\"text-custom-" + setting + "\" title=\"" + settingTitle + "\"><span class=\"fa fa-cog\" style=\"font-size:130%;\" aria-hidden=\"true\"></span></span></a></td>" +
                        td + "<a href=\"" + chatLink + "\"><span class=\"text-custom-" + chat + "\" title=\"" + chattitle + "\"><span class=\"fa fa-comments\" style=\"font-size: 130%;\" aria-hidden=\"true\"></span></span></a></td>" +
                        td + (d == obj.LastActionAt ? "-" : obj.LastActionAt.Value.ToString(fmt)) + "</td>" +
                        td + (d == obj.ChargingAlarm ? "-" : obj.ChargingAlarm.Value.ToString(fmt)) + "</td>" +
                        td + (d == obj.BatteryAlarm ? "-" : obj.BatteryAlarm.Value.ToString(fmt)) + "</td>" +
                        td + (d == obj.ParkingAlarm ? "-" : obj.ParkingAlarm.Value.ToString(fmt)) + "</td>" +
                        td + obj.CarNumber + "</td>" +
                        "<td class=\"in-tab-txt-b textoverflow\" style=\"text-align: left;\">" + (null == user ? "-" : user.Name) + "</td>" +
                        td + obj.Address + "</td>" +
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