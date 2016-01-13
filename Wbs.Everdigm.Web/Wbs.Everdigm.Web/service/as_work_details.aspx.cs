using System;
using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.Web.service
{
    public partial class as_work_details : BaseTrackerPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (!HasSessionLose)
            {
                hidKey.Value = Utility.UrlEncode(_key);
                ShowWorkInfo();
            }
        }

        private void ShowWorkInfo()
        {
            var id = ParseInt(Utility.Decrypt(hidKey.Value));
            var work = WorkInstance.Find(f => f.id == id && f.Deleted == false);
            if (null != work)
            {
                var ymd="yyyy/MM/dd";
                workinfo.Rows[1].Cells[1].InnerText = work.Title;
                workinfo.Rows[2].Cells[1].InnerText = work.RegisterTime.Value.ToString(ymd);
                workinfo.Rows[3].Cells[1].InnerText = work.ScheduleStart.Value.ToString(ymd) + " - " + work.ScheduleEnd.Value.ToString(ymd);
                workinfo.Rows[4].Cells[1].InnerText = work.Director;
                workinfo.Rows[5].Cells[1].InnerText = work.TB_WorkDetail.Count.ToString();
                workinfo.Rows[6].Cells[1].InnerHtml = work.Description.Replace("\r\n", "<br />");
            }
            ShowWorkDetails(work);
        }

        private void ShowWorkDetails(TB_Work work)
        {
            var html = "";
            if (null == work || work.TB_WorkDetail.Count < 1) {
                html = "<tr><td colspan=\"8\">No equipment(s) exist.</td></tr>";
            }
            else
            {
                var cnt = 0;
                var n=(int?)null;
                foreach (var obj in work.TB_WorkDetail)
                {
                    cnt++;
                    html += "<tr>" +
                        "<td style=\"text-align: center;\">" + cnt + "</td>" +
                        "<td>" + (n == obj.Equipment ? "-" : EquipmentInstance.GetFullNumber(obj.TB_Equipment)) + "</td>" +
                        "<td class=\"textoverflow\">" + WorkDetailInstance.GetWorkType(obj.Type.Value) + "</td>" +
                        "<td>" + (n == obj.BookedTerminal ? "-" : obj.TB_Terminal.Number) + "</td>" +
                        "<td>" + (n == obj.BookedTerminal ? "-" : obj.TB_Terminal.Sim) + "</td>" +
                        "<td>" + (n == obj.BookedTerminal ? "-" : (n == obj.TB_Terminal.Satellite ? "-" : obj.TB_Terminal.TB_Satellite.CardNo)) + "</td>" +
                        "<td style=\"text-align: left;\">" + obj.Details + "</td>" +
                        "<td></td>" +
                        "</tr>";
                }
            }
            tbodyBody.InnerHtml = html;
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            if (!HasSessionLose)
            {
                SaveWorkDetail();
            }
        }

        private void SaveWorkDetail() {
            var id = ParseInt(Utility.Decrypt(hidKey.Value));
            var work = WorkInstance.Find(f => f.id == id && f.Deleted == false);
            if (null != work)
            {
                var obj = WorkDetailInstance.GetObject();
                if (!string.IsNullOrEmpty(hiddenTerminal.Value))
                    obj.BookedTerminal = int.Parse(hiddenTerminal.Value);
                obj.Equipment = int.Parse(hiddenEquipment.Value);
                obj.Type = byte.Parse(hiddenType.Value);
                obj.Details = detail.Value;
                obj.Work = work.id;
                WorkDetailInstance.Add(obj);

                // 更新终端的预定状态
                if ((int?)null != obj.BookedTerminal) {
                    TerminalInstance.Update(f => f.id == obj.BookedTerminal && f.Delete == false, act =>
                    {
                        act.Booked = true;
                    });
                }
                // 保存操作历史记录
                SaveHistory(new TB_AccountHistory()
                {
                    ActionId = ActionInstance.Find(f => f.Name.Equals("EditWorkDetail")).id,
                    ObjectA = WorkDetailInstance.ToString(obj)
                });

                ShowNotification("../service/as_work_details.aspx?key=" + hidKey.Value, "Add new work.");
            }
            else
            {
                ShowNotification("../service/as_work_dispatch.aspx", "This work is not exist.", false);
            }
        }
    }
}