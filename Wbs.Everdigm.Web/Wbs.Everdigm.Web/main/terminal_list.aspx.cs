using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI.WebControls;

using Wbs.Everdigm.BLL;
using Wbs.Everdigm.Database;
using Wbs.Everdigm.Common;
using System.Configuration;
using Wbs.Utilities;
using Wbs.Protocol;

namespace Wbs.Everdigm.Web.main
{
    public partial class terminal_list : BaseTerminalPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            _cookie_name_ = "_terminal_list_page_";
            cookieName.Value = _cookie_name_;
            base.Page_Load(sender, e);
            if (!HasSessionLose)
            {
                if (!IsPostBack)
                {
                    hidPageIndex.Value = null == Request.Cookies[_cookie_name_] ? "" : Request.Cookies[_cookie_name_].Value;
                    InitializeEquipmentTypes();
                    var role = new RoleBLL().Find(f => f.id == Account.Role);
                    bt_Delete.Visible = role.IsAdministrator.Value;
                    ShowQuery();
                }
            }
        }
        private void InitializeEquipmentTypes()
        {
            var t = new EquipmentTypeBLL();
            var tlist = t.FindList(f => f.Delete == false).OrderBy(o => o.Name);
            //ddlEquipmentType.Items.Clear();
            //ddlEquipmentType.Items.Add(new ListItem() { Text = "Equipment Type:", Value = "" });
            string html = "";
            foreach (var v in tlist)
            {
                html += "<li role=\"presentation\"><a role=\"menuitem\" tabindex=\"" + v.id.ToString() + "\" href=\"#\">" + v.Name + "</a></li>";
            }
            menuEquipmentType.InnerHtml = html;
            var m = new EquipmentModelBLL();
            var mlist = m.FindList(f => f.Delete == false).OrderBy(o => o.Type).ThenBy(b => b.Name).ToList();
            hidJson.Value = JsonConverter.ToJson(mlist);
        }
        /// <summary>
        /// 查询结果变颜色
        /// </summary>
        /// <param name="obj">内容</param>
        /// <param name="type">0=终端号码，1=手机号码，2=卫星号码</param>
        /// <returns></returns>
        private string CheckQueryString(string obj)
        {
            var replace = txtNumber.Value;
            return string.IsNullOrEmpty(replace) ? obj : obj.Replace(replace, ("<span style=\"color: #FF0000;\">" + replace + "</span>"));
        }

        private string GetEquipment(TB_Terminal terminal, TB_Equipment equipment)
        {
            if (null == equipment)
                return "<a href=\"./equipment_terminal.aspx?key=" + Utility.UrlEncode(Utility.Encrypt(terminal.id.ToString())) + "\">bind</a>";

            if (string.IsNullOrEmpty(terminal.Sim))
                return "no sim card";

            return "<a href=\"#unbind_" + terminal.id + "\">" + EquipmentInstance.GetFullNumber(equipment) + "</a>";
        }
        /// <summary>
        /// 查询未绑定车辆的终端
        /// </summary>
        private void ShowQuery()
        {
            var number = txtNumber.Value.Trim();
            // 模糊查询时页码置为空
            if (!string.IsNullOrEmpty(number)) { hidPageIndex.Value = ""; }

            var pageIndex = "" == hidPageIndex.Value ? 1 : int.Parse(hidPageIndex.Value);

            var selEquipment = int.Parse(selectedEquipment.Value);
            // -1:ignore,0:not bind,1:bound
            if (selEquipment != 1)
            {
                ShowTerminalsNotBind(pageIndex);
            }
            else
            {
                ShowTerminalsBinded(pageIndex);
            }
        }
        private string GotSelectedType(int type)
        {
            switch (type)
            {
                case -1: return "Ignore";
                case 0: return "Not bind";
                default: return "Bound";
            }
        }
        private void ShowTerminalsNotBind(int pageIndex)
        {
            var totalRecords = 0;

            // 表达式
            Expression<Func<TB_Terminal, bool>> expression = PredicateExtensions.True<TB_Terminal>();

            expression = expression.And(a => a.Delete == false);
            // 是否绑定卫星 -1:ignore,0:not,1:bound
            var sat = int.Parse(selectedSatellite.Value);
            spanSatellite.InnerHtml = "Satellite:" + GotSelectedType(sat);
            if (sat >= 0)
            {
                if (sat == 0)
                {
                    expression = expression.And(a => a.Satellite == null);
                }
                else
                {
                    expression = expression.And(a => a.Satellite != null);
                }
            }
            // 是否绑定设备-1:ignore,0:not,1:bound
            var equ = int.Parse(selectedEquipment.Value);
            spanEquipment.InnerHtml = "Equipment:" + GotSelectedType(equ);
            if (equ >= 0)
            {
                if (equ == 0)
                {
                    expression = expression.And(a => a.HasBound == false);
                }
            }
            // 模糊查询
            var query = txtNumber.Value.Trim();
            if (!string.IsNullOrEmpty(query))
            {
                expression = expression.And(a => a.Number.Contains(query) || a.TB_Satellite.CardNo.Contains(query));
            }

            var list = TerminalInstance.FindPageList<TB_Terminal>(pageIndex, PageSize, out totalRecords, expression, "Number");
            var totalPages = totalRecords / PageSize + (totalRecords % PageSize > 0 ? 1 : 0);

            string html = "";
            if (totalRecords < 1)
            {
                html = "<tr><td colspan=\"11\">No records, You can change condition and try again or " +
                    " <a href=\"./terminal_register.aspx\">ADD</a> new one.</td></tr>";
            }
            else
            {
                var cnt = (pageIndex - 1) * PageSize;
                //var n = (int?)null;
                foreach (var obj in list)
                {
                    cnt++;
                    var id = Utility.UrlEncode(Utility.Encrypt(obj.id.ToString()));
                    //-1:ignore,0:not,1:bound
                    var equipment = EquipmentInstance.Find(f => f.TB_Terminal.id == obj.id && f.Deleted == false);
                    //if(equ==0&&equi)
                    html += "<tr>" +
                        //"<td class=\"in-tab-txt-rb\"><input type=\"checkbox\" id=\"cb_" + id + "\" /></td>" +
                        "<td class=\"in-tab-txt-rb\">" + cnt + "</td>" +
                        "<td class=\"in-tab-txt-rb\" style=\"text-align: left !important;\"><a href=\"./terminal_register.aspx?key=" + id + "\" >" + CheckQueryString(obj.Number) + "</a></td>" +
                        "<td class=\"in-tab-txt-rb\" style=\"text-align: left !important; cursor: pointer;\" title=\"Click to show advanced options\">" + CheckQueryString(TerminalInstance.GetSatellite(obj, true)) + "</td>" +
                        "<td class=\"in-tab-txt-rb\">" + obj.Firmware + "</td>" +
                        "<td class=\"in-tab-txt-rb\">" + obj.Revision.ToString() + "</td>" +
                        "<td style=\"text-align: left !important;\" class=\"in-tab-txt-rb\">" + TerminalTypes.GetTerminalType(obj.Type.Value) + "</td>" +
                        "<td class=\"in-tab-txt-rb\" style=\"text-align: left !important;\">" + obj.ProductionDate.Value.ToString("yyyy/MM/dd") + "</td>" +
                        "<td class=\"in-tab-txt-rb\">" + (obj.HasBound == true ? "yes" : "no") + "</td>" +
                        "<td class=\"in-tab-txt-rb\" style=\"text-align: left !important;\">" + CheckQueryString(GetEquipment(obj, equipment)) + "</td>" +
                        "<td class=\"in-tab-txt-b\">" + Utility.GetOnlineStyle(obj.OnlineStyle, obj.OnlineTime, false) + "</td>" +
                        //"<td class=\"in-tab-txt-rb\" style=\"text-align: left !important;\">" + obj.Sim + "</td>" +
                        "<td class=\"in-tab-txt-b\"></td>" +
                        "</tr>";
                }
            }
            ShowFooter(totalRecords, pageIndex, totalPages, html);
        }

        private void ShowTerminalsBinded(int pageIndex)
        {
            var totalRecords = 0;
            // 表达式
            Expression<Func<TB_Equipment, bool>> expression = PredicateExtensions.True<TB_Equipment>();
            // 必须是绑定了终端的设备
            expression = expression.And(f => f.Deleted == false && f.Terminal != (int?)null);

            // 设备type
            //var type = ddlEquipmentType.SelectedValue;
            //if (!string.IsNullOrEmpty(type))
            //{
            //    expression = expression.And(a => a.TB_EquipmentModel.Type == ParseInt(type));
            //}

            // 设备model
            //var model = selModel.Value;
            //if (!string.IsNullOrEmpty(model)) { expression = expression.And(a => a.Model == ParseInt(model)); }

            // 是否绑定卫星
            // 是否绑定卫星 -1:ignore,0:not,1:bound
            var sat = int.Parse(selectedSatellite.Value);
            spanSatellite.InnerHtml = "Satellite:" + GotSelectedType(sat);
            if (sat >= 0)
            {
                if (sat == 0)
                {
                    expression = expression.And(a => a.TB_Terminal.Satellite == null);
                }
                else
                {
                    expression = expression.And(a => a.TB_Terminal.Satellite != null);
                }
            }

            // 号码查询
            var query = txtNumber.Value.Trim();
            if (!string.IsNullOrEmpty(query))
            {
                expression = expression.And(a => a.Number.Contains(query) || a.TB_Terminal.Number.Contains(query) || a.TB_Terminal.TB_Satellite.CardNo.Contains(query));
            }

            var list = EquipmentInstance.FindPageList<TB_Equipment>(pageIndex, PageSize, out totalRecords, expression, "Number");
            var totalPages = totalRecords / PageSize + (totalRecords % PageSize > 0 ? 1 : 0);

            string html = "";
            if (totalRecords < 1)
            {
                html = "<tr><td colspan=\"13\">No records, You can change condition and try again or " +
                    " <a href=\"./terminal_register.aspx\">ADD</a> new one.</td></tr>";
            }
            else
            {
                var cnt = (pageIndex - 1) * PageSize;
                foreach (var obj in list)
                {
                    cnt++;
                    var id = Utility.UrlEncode(Utility.Encrypt(obj.id.ToString()));
                    html += "<tr>" +
                        //"<td style=\"text-align: center;\" class=\"in-tab-txt-rb\"><input type=\"checkbox\" id=\"cb_" + id + "\" /></td>" +
                        "<td style=\"text-align: center;\" class=\"in-tab-txt-rb\">" + cnt + "</td>" +
                        "<td class=\"in-tab-txt-rb\" style=\"text-align: left !important;\"><a href=\"./terminal_register.aspx?key=" + id + "\" >" + CheckQueryString(obj.TB_Terminal.Number) + "</a></td>" +
                        "<td class=\"in-tab-txt-rb\" style=\"text-align: left !important; cursor: pointer;\" title=\"Click to show advanced options\">" + CheckQueryString(TerminalInstance.GetSatellite(obj.TB_Terminal, true)) + "</td>" +
                        "<td class=\"in-tab-txt-rb\">" + obj.TB_Terminal.Firmware + "</td>" +
                        "<td class=\"in-tab-txt-rb\">" + obj.TB_Terminal.Revision.ToString() + "</td>" +
                        "<td class=\"in-tab-txt-rb\" style=\"text-align: left !important;\">" + TerminalTypes.GetTerminalType(obj.TB_Terminal.Type.Value) + "</td>" +
                        "<td class=\"in-tab-txt-rb\" style=\"text-align: left !important;\">" + obj.TB_Terminal.ProductionDate.Value.ToString("yyyy/MM/dd") + "</td>" +
                        "<td class=\"in-tab-txt-rb\">yes</td>" +
                        "<td class=\"in-tab-txt-rb\" style=\"text-align: left !important;\">" + CheckQueryString(GetEquipment(obj.TB_Terminal, obj)) + "</td>" +
                        "<td class=\"in-tab-txt-b\">" + Utility.GetOnlineStyle(obj.OnlineStyle, obj.OnlineTime, false) + "</td>" +
                        //"<td class=\"in-tab-txt-rb\">" + obj.TB_Terminal.Sim + "</td>" +
                        "<td class=\"in-tab-txt-b\"></td>" +
                        "</tr>";
                }
            }
            ShowFooter(totalRecords, pageIndex, totalPages, html);
        }

        private void ShowFooter(int totalRecords, int pageIndex, int totalPages, string html)
        {
            tbodyBodies.InnerHtml = html;
            divPagging.InnerHtml = "";
            if (totalRecords > 0)
                ShowPaggings(pageIndex, totalPages, totalRecords, "./terminal_list.aspx", divPagging);
        }

        protected void btQuery_Click(object sender, EventArgs e)
        {
            if (!HasSessionLose)
            { ShowQuery(); }
        }

        protected void btDelete_Click(object sender, EventArgs e)
        {
            if (!HasSessionLose)
            {
                if ("" != hidID.Value)
                {
                    var ids = GetIdList(hidID.Value.Split(new char[] { ',' }));
                    var list = TerminalInstance.FindList(f => ids.Contains(f.id) && f.Delete == false);
                    foreach (var terminal in list)
                    {
                        terminal.Delete = true;
                        Update(terminal);

                        SaveHistory(new TB_AccountHistory
                        {
                            ActionId = ActionInstance.Find(f => f.Name.Equals("DeleteTerminal")).id,
                            ObjectA = TerminalInstance.ToString(terminal)
                        });
                    }
                    ShowNotification("./terminal_list.aspx", "Success: You have delete " + ids.Count() + " terminal(s).");
                }
            }
        }
        /// <summary>
        /// 解绑终端和卫星模块
        /// </summary>
        private void UnboundSatellite()
        {
            var id = int.Parse(hidBoundSatellite.Value.Trim());
            var t = TerminalInstance.Find(f => f.id == id);
            if (null == t) { ShowNotification("./terminal_list.aspx", "Unbind fail: Terminal not exists.", false); }
            else
            {
                if ((int?)null == t.Satellite) { ShowNotification("./terminal_list.aspx", "Unbind fail: No Satellite bound on it.", false); }
                else
                {
                    string satno = t.TB_Satellite.CardNo;
                    TerminalInstance.Update(f => f.id == t.id, act =>
                    {
                        act.Satellite = (int?)null;
                        // 更新终端的链接为OFF
                        if (act.OnlineStyle == (byte)LinkType.SATELLITE)
                        {
                            act.OnlineStyle = (byte?)null;
                        }
                        // 更新卫星功能为false
                        act.SatelliteStatus = false;
                    });
                    // 更新设备的链接为OFF
                    EquipmentInstance.Update(f => f.Terminal == t.id, act =>
                    {
                        if (act.OnlineStyle == (byte)LinkType.SATELLITE)
                        {
                            act.OnlineStyle = (byte?)null;
                        }
                        act.SatelliteStatus = false;
                    });
                    SatelliteInstance.Update(f => f.id == t.Satellite, act => { act.Bound = false; });
                    // 发送解绑卫星模块的命令
                    SendDD02Command(false, t);
                    SaveHistory(new TB_AccountHistory()
                    {
                        ActionId = ActionInstance.Find(f => f.Name.Equals("UnbindSat")).id,
                        ObjectA = "Ter: " + t.Number + " unbind Sat: " + satno
                    });
                    ShowNotification("./terminal_list.aspx", "Ter: " + t.Number + " unbind Sat: " + satno + " OK!");
                }
            }
        }
        private void SendDD02Command(bool bound, TB_Terminal terminal)
        {
            // 查看是否允许服务器自动修改卫星绑定关系
            var ctrl = int.Parse(ConfigurationManager.AppSettings["SatelliteControl"]);
            if (ctrl <= 0) return;

            var str = GetDD02Command(bound, terminal.Sim);
            var CommandInstance = new Wbs.Everdigm.BLL.CommandBLL();
            var obj = CommandInstance.GetObject();
            obj.Content = str;
            var sim = terminal.Sim;
            sim = (sim[0] == '8' && sim[1] == '9' && sim.Length < 11) ? (sim + "000") : sim;
            obj.DestinationNo = sim;
            obj.Terminal = terminal.id;
            obj = CommandInstance.Add(obj);
            //CommandUtility.SendSMSCommand(obj);
        }
        private string GetDD02Command(bool bound, string sim)
        {
            sim = (sim[0] == '8' && sim[1] == '9' && sim.Length < 11) ? (sim + "000") : sim;
            var str = ConfigurationManager.AppSettings["0xDD02"];
            str = str.Replace("13953598693", sim);
            var data = Utility.GetBytes(str);
            data[data.Length - 4] = (byte)(bound ? 1 : 0);
            if (bound)
            {
                var server = ConfigurationManager.AppSettings["SATELLITE_SERVER"];
                var b = BitConverter.GetBytes(int.Parse(server));
                b = CustomConvert.reserve(b);
                System.Buffer.BlockCopy(b, 1, data, data.Length - 3, 3);
            }
            return Utility.GetHex(data);
        }

        protected void btBoundSatellite_Click(object sender, EventArgs e)
        {
            var value = hidBoundSatellite.Value.Trim();
            if (string.IsNullOrEmpty(value)) return;
            // 为终端绑定卫星模块
            var index = value.IndexOf(',');
            if (index < 0)
            {
                // 没有,分割的是解绑卫星模块
                UnboundSatellite();
                return;
            }
            var tid = value.Substring(0, index);
            var gid = value.Substring(index + 1);
            gid = Utility.Decrypt(gid);
            var t = TerminalInstance.Find(f => f.id == int.Parse(tid));
            if (null == t)
            {
                ShowNotification("./terminal_list.aspx", "Bound fail: Terminal not exists.", false);
            }
            else
            {
                if (t.Satellite != (int?)null)
                {
                    ShowNotification("./terminal_list.aspx", "Terminal \"" + t.Number + "\" has bound Satellite: " + t.TB_Satellite.CardNo, false);
                }
                else
                {
                    var g = SatelliteInstance.Find(f => f.id == int.Parse(gid));
                    if (null == g) { ShowNotification("./terminal_list.aspx", "No Satellite info exists.", false); }
                    else
                    {
                        if (g.Bound == true)
                        {
                            var gt = TerminalInstance.Find(f => f.TB_Satellite.id == g.id);
                            ShowNotification("./terminal_list.aspx", "Satellite \"" + g.CardNo + "\" has bound on Terminal: " + gt.Number, false);
                        }
                        else
                        {
                            TerminalInstance.Update(f => f.id == t.id, act =>
                            {
                                act.Satellite = g.id;
                            });
                            t = TerminalInstance.Find(f => f.id == t.id);
                            SatelliteInstance.Update(f => f.id == g.id, act => { act.Bound = true; });
                            // 发送绑定卫星模块的命令
                            SendDD02Command(true, t);
                            // 保存绑定卫星模块的历史记录
                            SaveHistory(new TB_AccountHistory()
                            {
                                ActionId = ActionInstance.Find(f => f.Name.Equals("BindSat")).id,
                                ObjectA = TerminalInstance.ToString(t)
                            });
                            //ShowTerminals();
                            ShowNotification("./terminal_list.aspx", "Terminal \"" + t.Number + "\" bound Satellite \"" + g.CardNo + "\" OK!");
                        }
                    }
                }
            }
        }

        protected void btUnbindEquipment_Click(object sender, EventArgs e)
        {
            var value = hidBoundSatellite.Value.Trim();
            if (string.IsNullOrEmpty(value)) return;
            var id = int.Parse(value);
            var terminal = TerminalInstance.Find(f => f.id == id);
            if (null == terminal) return;

            var equipment = EquipmentInstance.Find(f => f.Terminal == id && f.Deleted == false);
            // 更新设备的终端为空并清空设备的相应值
            EquipmentInstance.Update(f => f.Terminal == id && f.Deleted == false, act =>
            {
                act.Terminal = (int?)null;
                act.GpsAddress = "";
                act.LastAction = "";
                act.LastActionBy = "";
                act.LastActionTime = (DateTime?)null;
                act.Latitude = 0.0;
                act.Longitude = 0.0;
                act.OnlineStyle = (byte?)null;
                act.OnlineTime = (DateTime?)null;
                act.Runtime = 0;
                act.Socket = 0;
                act.Port = 0;
                act.IP = "";
                act.LockStatus = "00";
                act.Rpm = 0;
                act.Signal = 0;
                act.Voltage = "G0000";
            });
            // 更新终端的绑定状态为false
            TerminalInstance.Update(f => f.id == id, act =>
            {
                act.HasBound = false;
            });
            // 保存解绑终端历史
            SaveHistory(new TB_AccountHistory()
            {
                ActionId = ActionInstance.Find(f => f.Name.Equals("Unbind")).id,
                ObjectA = "unbind terminal " + terminal.Number + " and equipment " + EquipmentInstance.GetFullNumber(equipment)
            });

            ShowNotification("./terminal_list.aspx", "You have unbind the terminal and equipment.");
        }

        protected void bt_Test_Click(object sender, EventArgs e)
        {
            if (!HasSessionLose)
            {
                if ("" != hidID.Value)
                {
                    var id = int.Parse(Utility.Decrypt(hidID.Value));
                    var terminal = TerminalInstance.Find(f => f.id == id && f.Delete == false);
                    if (null != terminal)
                    {
                        if (terminal.HasBound.Value == false)
                        {
                            ShowNotification("./terminal_list.aspx", "No equipment bind on this terminal.", false);
                        }
                        else
                        {
                            var test = StatusInstance.Find(f => f.IsItTesting == true);
                            if (null == test)
                            {
                                ShowNotification("Situation code is not exist.", "", false);
                            }
                            else
                            {
                                var equip = terminal.TB_Equipment.FirstOrDefault();
                                if (null != equip)
                                {
                                    EquipmentInstance.Update(f => f.id == equip.id, act =>
                                    {
                                        act.Status = StatusInstance.Find(f => f.IsItTesting == true).id;
                                    });
                                    SaveHistory(new TB_AccountHistory
                                    {
                                        ActionId = ActionInstance.Find(f => f.Name.Equals("EditTerminal")).id,
                                        ObjectA = EquipmentInstance.GetFullNumber(equip) + ", " + terminal.Number + ", set to test mode"
                                    });
                                    ShowNotification("./terminal_list.aspx", EquipmentInstance.GetFullNumber(equip) + ", " + terminal.Number + ", set to test mode");
                                }
                                else
                                {
                                    ShowNotification("./terminal_list.aspx", "No equipment bind on this terminal.", false);
                                }
                            }
                        }
                    }
                    else ShowNotification("./terminal_list.aspx", "Terminal is not exist.", false);
                }
            }
        }

        protected void btnSatelliteStopping_Click(object sender, EventArgs e)
        {
            var value = hidBoundSatellite.Value.Trim();
            if (string.IsNullOrEmpty(value))
            {
                ShowNotification("./terminal_list.aspx", "Cannot find object with null parameter.", false);
            }
            else
            {
                var id = int.Parse(value);
                var terminal = TerminalInstance.Find(f => f.id == id);
                if (null == terminal)
                {
                    ShowNotification("./terminal_list.aspx", "Terminal is not exist.", false);
                }
                else
                {
                    // 更新终端的连接为卫星停止状态
                    TerminalInstance.Update(f => f.id == terminal.id, act => { act.OnlineStyle = (byte)LinkType.SATELLITE_STOP; });
                    // 更新设备的连接为卫星停止状态
                    EquipmentInstance.Update(f => f.Terminal == terminal.id, act => { act.OnlineStyle = (byte)LinkType.SATELLITE_STOP; });

                    ShowNotification("./terminal_list.aspx", "You have stopped satellite " + terminal.TB_Satellite.CardNo, true);
                }
            }
        }
    }
}