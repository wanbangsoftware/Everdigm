using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            using (var bll = new DataBLL())
            {
                Expression<Func<TB_HISTORIES, bool>> expression = PredicateExtensions.True<TB_HISTORIES>();
                if (!string.IsNullOrEmpty(query))
                {
                    expression = expression.And(a => a.mac_id.Contains(query));
                }
                expression = expression.And(a => cmds.Contains(a.command_id));
                expression = expression.And(a => a.receive_time >= time1 && a.receive_time <= time2);

                var list = bll.FindList<TB_HISTORIES>(expression , "receive_time").ToList<TB_HISTORIES>();

                var html = "";
                int TotalAddedMinutes = 0, TotalWorkedMinutes = 0, TotalUsedHour = 0;
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
                        var data = CustomConvert.GetBytes(obj.message_content);
                        var worktime = BitConverter.ToUInt32(data, obj.command_id.Equals("0x1000") ? 13 : 0);
                        int interval = (int)(i == 0 ? 0 : (worktime - lastworktime));
                        TotalWorkedMinutes += obj.command_id.Equals("0x600B") ? 0 : (interval > 0 ? interval : 0);
                        var bin = obj.command_id.Equals("0x600B") ? "00000000" : CustomConvert.IntToDigit(data[4], CustomConvert.BIN, 8);
                        string eng, engflag;
                        if (obj.command_id.Equals("0x600B"))
                        {
                            eng = EquipmentBLL.eng_off;
                            engflag = "0";
                        }
                        else if (data[1] == 0x40 || data[1] == 0x0F || data[1] == 0xFF)
                        {
                            eng = EquipmentBLL.eng_lock;
                            engflag = "0";
                        }
                        else
                        {
                            eng = (bin[6] == '1' ? EquipmentBLL.eng_on : EquipmentBLL.eng_off);
                            engflag = (bin[6] == '1' ? "1" : "0");
                        }
                        int added = (interval > 0 ? (obj.command_id.Equals("0x600B") ? 0 : (interval > 60 ? interval / 60 : 1)) : 0) + (engflag.Equals("1") ? 1 : 0);
                        TotalAddedMinutes += added;
                        TotalUsedHour += interval > 0 ? (obj.command_id.Equals("0x600B") ? 0 : (interval > 60 ? interval / 60 : 1)) : 0;

                        html += "<tr>" +
                            "<td class=\"in-tab-title-b\">" + (i + 1) + "</td>" +
                            "<td class=\"in-tab-title-rb textoverflow\" style=\"text-align: left; \">" + obj.receive_time.Value.ToString("yyyy/MM/dd HH:mm:ss") + "</td>" +
                            "<td class=\"in-tab-title-b\" style=\"text-align: right;\">" + EquipmentBLL.GetRuntime((int?)worktime) + "</td>" +
                            "<td class=\"in-tab-title-b\" style=\"text-align: right;\">" + string.Format("{0:0,00}", worktime) + "</td>" +
                            "<td class=\"in-tab-title-b\" style=\"text-align: right;\">" + (interval > 60 ? ("<font color=\"#FF0000\">" + interval + "</font>") : interval.ToString()) + "</td>" +
                            "<td class=\"in-tab-title-rb\" style=\"text-align: right;\">" + added + "</td>" +
                            "<td class=\"in-tab-title-b\" style=\"text-align: left;\">" + obj.command_id + "</td>" +
                            "<td class=\"in-tab-title-b\" style=\"text-align: left;\">" + obj.terminal_id + "</td>" +
                            "<td class=\"in-tab-title-rb textoverflow\" style=\"text-align: left;\">" + obj.mac_id + "</td>" +
                            "<td class=\"in-tab-title-b\" style=\"text-align: center; width: 30px;\">" + eng + "</td>" +
                            "<td class=\"in-tab-title-b\" style=\"text-align: center; width: 30px;\">" + engflag + "</td>" +
                            "<td class=\"in-tab-title-b\" style=\"text-align: left;\">" + obj.message_content + "</td>" +
                            "<td class=\"in-tab-title-b\"></td>" +
                            "</tr>";
                        lastworktime = worktime;

                        if (i == len - 1)
                        {
                            var compensate = (TotalWorkedMinutes / 60.0) / TotalUsedHour;
                            var finalAdded = TotalAddedMinutes / 60.0 * compensate;
                            var finalWork = worktime / 60.0 + finalAdded;
                            string summary = "<tr>" +
                                        "<td class=\"in-tab-title-rb textoverflow\">" + obj.mac_id + "</td>" +
                                        "<td class=\"in-tab-title-rb\" style=\"text-align: right;\">" + EquipmentBLL.GetRuntime((int?)worktime) + "</td>" +
                                        "<td class=\"in-tab-title-b\" style=\"text-align: right;\">" + EquipmentBLL.GetRuntime(TotalWorkedMinutes) + "</td>" +
                                        "<td class=\"in-tab-title-b\" style=\"text-align: right;\">" + TotalUsedHour + "</td>" +
                                        "<td class=\"in-tab-title-rb\" style=\"text-align: right;\">" + compensate + "</td>" +
                                        "<td class=\"in-tab-title-b\" style=\"text-align: right;\">" + TotalAddedMinutes + "/" + EquipmentBLL.GetRuntime((int?)TotalAddedMinutes) + "</td>" +
                                        "<td class=\"in-tab-title-b\" style=\"text-align: right;\">" + finalAdded + "</td>" +
                                        "<td class=\"in-tab-title-rb\" style=\"text-align: right;\">" + finalWork + "</td>" +
                                        "<td class=\"in-tab-title-b\"></td>" +
                                    "</tr>";
                            tbodySummary.InnerHtml = summary;
                        }
                    }
                }
                tbodyBody.InnerHtml = html;
            }
        }
    }
}