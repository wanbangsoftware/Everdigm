using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Wbs.Everdigm.Database;
using System.Data.Linq;

namespace Wbs.Everdigm.Web.service
{
    public partial class as_work_dispatch : BaseTrackerPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            _cookie_name_ = "_as_work_dispatch_list_page_";
            cookieName.Value = _cookie_name_;
            base.Page_Load(sender, e);
            if (!HasSessionLose)
            {
                if (!IsPostBack)
                {
                    hidPageIndex.Value = null == Request.Cookies[_cookie_name_] ? "1" : Request.Cookies[_cookie_name_].Value;
                    ShowQueryDate();
                    ShowWorks();
                }
            }
        }

        private void ShowQueryDate()
        {
            var now = DateTime.Now;
            var then = new DateTime(now.Year, now.Month, 1);
            // 查询开始时间
            start.Value = then.ToString("yyyy/MM/dd");
            // 查询结束时间
            end.Value = now.ToString("yyyy/MM/dd");
            // 新建/编辑开始时间
            //start1.Value = now.ToString("yyyy/MM/dd");
        }

        private void ShowWorks()
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

            var totalRecords = 0;
            var pageIndex = "" == hidPageIndex.Value ? 1 : int.Parse(hidPageIndex.Value);
            var list = WorkInstance.FindPageList<TB_Work>(pageIndex, PageSize, out totalRecords,
                f => f.ScheduleStart >= then && f.ScheduleStart <= now && f.Deleted == false, null);
            var totalPages = totalRecords / PageSize + (totalRecords % PageSize > 0 ? 1 : 0);

            string html = "";
            if (totalRecords < 1)
            {
                html = "<tr><td colspan=\"8\">No records.</td></tr>";
            }
            else
            {
                var cnt = (pageIndex - 1) * PageSize;
                foreach (var obj in list)
                {
                    cnt++;
                    var id = Utility.UrlEncode(Utility.Encrypt(obj.id.ToString()));
                    html += "<tr>" +
                        "<td style=\"text-align: center; height: 25px;\">" + cnt + "</td>" +
                        "<td>" + obj.RegisterTime.Value.ToString("yyyy/MM/dd") + "</td>" +
                        "<td>" + obj.ScheduleStart.Value.ToString("yyyy/MM/dd") + "</td>" +
                        "<td>" + obj.ScheduleEnd.Value.ToString("yyyy/MM/dd") + "</td>" +
                        "<td>" + obj.Director + "</td>" +
                        "<td style=\"text-align: center;\">" + obj.TB_WorkDetail.Count + "</td>" +
                        "<td class=\"textoverflow\" style=\"text-align: left;\" data-data=\"" + 
                            obj.Description + "\"><a data-content=\"" + obj.Description.Replace("\r\n", "<br />") + 
                            "\" title=\"Description:\" data-toggle=\"popover\" href=\"./as_work_details.aspx?key=" +
                            id + "\">" + obj.Title + "</a></td>" +
                        "<td></td>" +
                        "</tr>";
                }
            }
            tbodyBody.InnerHtml = html;
            divPagging.InnerHtml = "";
            if (totalRecords > 0)
                ShowPaggings(pageIndex, totalPages, totalRecords, "./as_work_dispatch.aspx", divPagging);
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

        private void New()
        {
            var obj = WorkInstance.GetObject();
            obj.ScheduleEnd = DateTime.Parse(end.Value);
            obj.ScheduleStart = DateTime.Parse(start.Value);
            obj.Description = description.Value;
            obj.Director = director.Value.Trim();
            obj.Title = title.Value.Trim();
            WorkInstance.Add(obj);

            SaveHistory(new TB_AccountHistory()
            {
                ActionId = ActionInstance.Find(f => f.Name.Equals("AddWork")).id,
                ObjectA = WorkInstance.ToString(obj)
            });

            ShowNotification("../service/as_work_dispatch.aspx", "You added a new work.");
        }

        private void Edit(int id)
        {
            var obj = WorkInstance.Find(f => f.id == id && f.Deleted == false);
            if (null == obj)
            {
                ShowNotification("../service/as_work_dispatch.aspx", "No work exist.", false);
            }
            else
            {
                WorkInstance.Update(f => f.id == id && f.Deleted == false, act =>
                {
                    act.Description = description.Value;
                    act.Director = director.Value.Trim();
                    if (!string.IsNullOrEmpty(end1.Value))
                        act.ScheduleEnd = DateTime.Parse(end1.Value.Trim());
                    if (!string.IsNullOrEmpty(start1.Value.Trim()))
                        act.ScheduleStart = DateTime.Parse(start1.Value);
                    act.Title = title.Value.Trim();
                });

                SaveHistory(new TB_AccountHistory()
                {
                    ActionId = ActionInstance.Find(f => f.Name.Equals("EditWork")).id,
                    ObjectA = WorkInstance.ToString(obj)
                });

                ShowNotification("../service/as_work_dispatch.aspx", "The work has changed.");
            }
        }

        protected void btQuery_Click(object sender, EventArgs e)
        {
            if (!HasSessionLose) ShowWorks();
        }
    }
}