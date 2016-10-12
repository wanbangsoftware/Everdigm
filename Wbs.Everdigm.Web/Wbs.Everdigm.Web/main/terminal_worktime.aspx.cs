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
            liRefreshAll.Visible = null != Account && Account.Code.ToLower().Equals("leekwok");
        }

        protected void btQuery_Click(object sender, EventArgs e)
        {
            tbodySummary.InnerHtml = "";
            var time1 = DateTime.Parse(start.Value.Trim() + " 00:00:00");
            var time2 = DateTime.Parse(end.Value.Trim() + " 23:59:59");
            var query = txtQuery.Value.Trim();
            bool chk = checkDirectlySave.Checked;
            if (chk)
            { Query(query); }
            else
            {
                Query(new Work() { Id = (chk ? 1 : 0), MacId = query, Time1 = time1, Time2 = time2 });
            }
        }
        private class Work
        {
            /// <summary>
            /// 设备的id，如果为0则说明是模糊查询
            /// </summary>
            public int Id;
            /// <summary>
            /// 设备的号码
            /// </summary>
            public string MacId;
            /// <summary>
            /// 查询开始时间
            /// </summary>
            public DateTime Time1;
            /// <summary>
            /// 查询结束时间
            /// </summary>
            public DateTime Time2;
        }
        protected void buttonRefreshAll_Click(object sender, EventArgs e)
        {
            Query("");
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="query"></param>
        private void Query(string query)
        {
            tbodySummary.InnerHtml = "";
            List<Work> macs = new List<Work>();
            using (var bll = new EquipmentBLL())
            {
                Expression<Func<TB_Equipment, bool>> expression = PredicateExtensions.True<TB_Equipment>();
                expression = expression.And(a => a.Deleted == false && a.TB_Terminal.Version == 1);
                if (!string.IsNullOrEmpty(query))
                {
                    string number = query;
                    string mode = "";
                    int dash = query.LastIndexOf('-');
                    if (dash > 0)
                    {
                        number = query.Substring(dash + 1);
                        mode = query.Replace(number, "");
                        expression = expression.And(a => a.Number.Equals(number) && a.TB_EquipmentModel.Code.Equals(mode));
                    }
                    else
                    {
                        expression = expression.And(a => a.Number.Equals(number));
                    }
                }
                var list = bll.FindList(expression);
                if (null != list && list.Count() > 0)
                {
                    foreach (var obj in list)
                    {
                        macs.Add(new Work()
                        {
                            Id = obj.id,
                            MacId = bll.GetFullNumber(obj),
                            Time1 = obj.RegisterTime.Value,
                            Time2 = DateTime.Now
                        });
                    }
                }
            }
            // 循环统计每一个设备的计算时间
            foreach (var mac in macs)
            {
                Query(mac);
            }
        }
        /// <summary>
        /// 查询指定条件
        /// </summary>
        /// <param name="query"></param>
        /// <param name="time1"></param>
        /// <param name="time2"></param>
        private void Query(Work query) {
            var cmds = new List<string>();
            cmds.Add("0x1000");
            cmds.Add("0x600B");
            using (var bll = new DataBLL())
            {
                Expression<Func<TB_HISTORIES, bool>> expression = PredicateExtensions.True<TB_HISTORIES>();
                if (!string.IsNullOrEmpty(query.MacId))
                {
                    expression = expression.And(a => a.mac_id.Contains(query.MacId));
                }
                expression = expression.And(a => cmds.Contains(a.command_id));
                expression = expression.And(a => a.receive_time >= query.Time1 && a.receive_time <= query.Time2);

                var list = bll.FindList<TB_HISTORIES>(expression, "receive_time").ToList<TB_HISTORIES>();

                var html = "";
                int TotalAddedMinutes = 0, TotalWorkedMinutes = 0, TotalUsedHour = 0;
                if (null == list || list.Count() < 1)
                {
                    html = "<tr><td colspan=\"13\" class=\"in-tab-title-b\">No records, you can change condition and try again.</td></tr>";
                }
                else
                {
                    uint lastworktime = 0;
                    string macid = "";
                    byte maccount = 0;
                    for (int i = 0, len = list.Count(); i < len; i++)
                    {
                        var obj = list[i];
                        if (!macid.Equals(obj.mac_id))
                        {
                            macid = obj.mac_id;
                            maccount++;
                        }
                        var data = CustomConvert.GetBytes(obj.message_content);
                        var worktime = BitConverter.ToUInt32(data, obj.command_id.Equals("0x1000") ? 13 : 0);
                        int interval = (int)(i == 0 ? 0 : (worktime - lastworktime));
                        // 两条数据之间接收时间之差
                        long minInterval = (long)((i == 0) ? 0 : (obj.receive_time.Value - list[i - 1].receive_time.Value).TotalSeconds);
                        if (interval > minInterval)
                        {
                            // 如果这条数据与上一条数据的工作时间之差大于了这两条数据的接收时间之差则将其算做0
                            interval = 0;
                        }

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
                            tbodySummary.InnerHtml += summary;
                            if (query.Id > 0)
                            {
                                // 只查询到一个设备时才更新统计数据，否则不需要更新
                                if (maccount <= 1)
                                {
                                    new EquipmentBLL().Update(f => f.id == query.Id, act =>
                                    {
                                    // 实际工作小时数
                                    act.WorkHours = TotalWorkedMinutes / 60.0;
                                    // 粗略计算工作小时数
                                    act.UsedHours = TotalUsedHour;
                                    // 工作效率
                                    act.HourWorkEfficiency = compensate;
                                    // 补偿的小时数
                                    act.AddedHours = TotalAddedMinutes / 60.0;
                                    // 实际补偿的小时数
                                    act.CompensatedHours = finalAdded;

                                    });
                                }
                            }
                        }
                    }
                }
                if (checkAnalyseDailyWork.Checked)
                {
                    tbodyBody.InnerHtml = html;
                }
            }
        }
    }
}