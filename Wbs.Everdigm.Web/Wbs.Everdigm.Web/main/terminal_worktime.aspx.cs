using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Wbs.Everdigm.Database;
using Wbs.Everdigm.BLL;
using Wbs.Utilities;

namespace Wbs.Everdigm.Web.main
{
    public partial class terminal_worktime : BaseBLLPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (!IsPostBack)
            {
                tbodyBody.InnerHtml = "<tr><td colspan=\"13\">Input your equipment number to query.</td></tr>";
            }
        }

        protected void btQuery_Click(object sender, EventArgs e)
        {
            var cmds = new List<string>();
            cmds.Add("0x1000");
            cmds.Add("0x600B");
            var time1 = DateTime.Parse(start.Value.Trim() + " 00:00:00");
            var time2 = DateTime.Parse(end.Value.Trim() + " 23:59:59");
            var query = txtQuery.Value.Trim();
            var list = DataInstance.FindList<TB_HISTORIES>(f => f.mac_id.Contains(query) && f.receive_time >= time1 && f.receive_time <= time2 && cmds.Contains(f.command_id), "receive_time").ToList<TB_HISTORIES>();

            var html = "";
            uint totalTime = 0, totalWorktime = 0, addWorktime = 0;
            if (null == list || list.Count() < 1)
            {
                html = "<tr><td colspan=\"13\" class=\"in-tab-title-b\">No records, you can change condition and try again.</td></tr>";
            }
            else
            {
                uint lastworktime = 0;
                for (int i = 0, len = list.Count(); i < len; i++)
                {
                    var obj = list[i];
                    var t1 = i == 0 ? obj.receive_time.Value : list[i - 1].receive_time.Value;
                    var t2 = obj.receive_time.Value;
                    var tdiff = t2 - t1;
                    int seconds = (int)tdiff.TotalSeconds - 3600;
                    var data = CustomConvert.GetBytes(obj.message_content);
                    var worktime = BitConverter.ToUInt32(data, obj.command_id.Equals("0x1000") ? 13 : 0);
                    //if (lastworktime == 0) { lastworktime = worktime; }
                    var interval = i == 0 ? 0 : (worktime - lastworktime);
                    var bin = obj.command_id.Equals("0x600B") ? "00000000" : CustomConvert.IntToDigit(data[4], CustomConvert.BIN, 8);
                    var eng = obj.command_id.Equals("0x600B") ? EquipmentBLL.eng_off : ((data[1] == 0x40 || data[1] == 0x0F || data[1] == 0xFF) ? EquipmentBLL.eng_lock : (bin[6] == '1' ? EquipmentBLL.eng_on : EquipmentBLL.eng_off));
                    int add = 0;
                    if (seconds > 0 && interval > 0) {
                        if (seconds > 3600)
                        { add = (int)interval / 60; }
                        else
                        {
                            add = (seconds / 60) + (seconds % 60 > 30 ? 1 : 0);
                        }
                    }
                    int add1 = interval >= 60 ? 2 : 0;
                    addWorktime += (uint)add1;
                    totalTime += (uint)add;
                    totalWorktime += interval > 0 ? interval : 0;

                    html += "<tr>" +
                        "<td class=\"in-tab-title-b\">" + (i + 1) + "</td>" +
                        "<td class=\"in-tab-title-b textoverflow\" style=\"text-align: left; \">" + obj.receive_time.Value.ToString("yyyy/MM/dd HH:mm:ss") + "</td>" +
                        "<td class=\"in-tab-title-b textoverflow\" style=\"text-align: left; \">" + new DateTime(tdiff.Ticks).ToString("HH:mm:ss") + "</td>" +
                        "<td class=\"in-tab-title-b\" style=\"text-align: right; \">" + (seconds <= 0 ? "0" : (seconds > 3600 ? ("<font color=\"#FF0000\">" + seconds + "</font>") : seconds.ToString())) + "</td>" +
                        "<td class=\"in-tab-title-rb\" style=\"text-align: right;\">" + add + "</td>" +
                        "<td class=\"in-tab-title-b\" style=\"text-align: right;\">" + EquipmentBLL.GetRuntime((int?)worktime) + "</td>" +
                        "<td class=\"in-tab-title-b\" style=\"text-align: right;\">" + string.Format("{0:0,00}", worktime) + "</td>" +
                        "<td class=\"in-tab-title-b\" style=\"text-align: right;\">" + (interval > 60 ? ("<font color=\"#FF0000\">" + interval + "</font>") : interval.ToString()) + "</td>" +
                        "<td class=\"in-tab-title-rb\" style=\"text-align: right;\">" + add1 + "</td>" +
                        "<td class=\"in-tab-title-b\" style=\"text-align: left;\">" + obj.command_id + "</td>" +
                        "<td class=\"in-tab-title-rb\" style=\"text-align: left;\">" + obj.terminal_id + "</td>" +
                        "<td class=\"in-tab-title-b\" style=\"text-align: center;\">" + eng + "</td>" +
                        "<td class=\"in-tab-title-b\" style=\"text-align: left;\">" + obj.message_content + "</td>" +
                        "<td></td>" +
                        "</tr>";
                    lastworktime = worktime;
                }
            }
            tbodyBody.InnerHtml = html;
            spanTotalServerTime.InnerHtml = string.Format("({0:0.00})", (totalTime / 60.0));
            spanTotalWorkTime.InnerHtml = string.Format("({0:0.00})", (totalWorktime / 60.0));
        }
    }
}