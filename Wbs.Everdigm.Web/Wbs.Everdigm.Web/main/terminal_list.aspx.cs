using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Wbs.Everdigm.Database;
using System.Configuration;
using Wbs.Utilities;
using Wbs.Everdigm.Common;
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
                    ShowTerminals();
                }
            }
        }
        /// <summary>
        /// 查询结果变颜色
        /// </summary>
        /// <param name="obj">内容</param>
        /// <param name="type">0=终端号码，1=手机号码，2=卫星号码</param>
        /// <returns></returns>
        private string CheckQueryString(string obj, int type)
        {
            var replace="";
            switch (type)
            {
                case 0:
                    // 终端号码查询
                    replace = txtNumber.Value;
                    break;
                case 1:
                    // 手机号码查询
                    replace = txtSimcard.Value;
                    break;
                case 2:
                    // 卫星号码查询
                    //replace = txtSatellite.Value;
                    break;
            }

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
        private void ShowTerminals()
        {
            var totalRecords = 0;
            var pageIndex = "" == hidPageIndex.Value ? 1 : int.Parse(hidPageIndex.Value);
            var list = TerminalInstance.FindPageList<TB_Terminal>(pageIndex, PageSize, out totalRecords,
                f => f.Delete == false && f.Number.Contains(txtNumber.Value) &&
                    f.Sim.Contains(txtSimcard.Value), "Number");
            var totalPages = totalRecords / PageSize + (totalRecords % PageSize > 0 ? 1 : 0);

            string html = "";
            if (totalRecords < 1)
            {
                html = "<tr><td colspan=\"13\">No records, You can change the condition and try again or " +
                    " <a href=\"./terminal_register.aspx\">ADD</a> new one.</td></tr>";
            }
            else
            {
                var cnt = (pageIndex - 1) * PageSize;
                var n = (int?)null;
                foreach (var obj in list)
                {
                    cnt++;
                    var id = Utility.UrlEncode(Utility.Encrypt(obj.id.ToString()));
                    var equipment = EquipmentInstance.Find(f => f.TB_Terminal.id == obj.id && f.Deleted == false);
                    html += "<tr>" +
                        "<td style=\"text-align: center;\"><input type=\"checkbox\" id=\"cb_" + id + "\" /></td>" +
                        "<td style=\"text-align: center;\">" + cnt + "</td>" +
                        "<td><a href=\"./terminal_register.aspx?key=" + id + "\" >" + CheckQueryString(obj.Number, 0) + "</a></td>" +
                        "<td>" + CheckQueryString(obj.Sim, 1) + "</td>" +
                        "<td>" + TerminalInstance.GetSatellite(obj, true) + "</td>" +
                        "<td>" + obj.Firmware + "</td>" +
                        "<td style=\"text-align: center;\">" + obj.Revision.ToString() + "</td>" +
                        "<td style=\"text-align: center;\">" + TerminalTypes.GetTerminalType(obj.Type.Value) + "</td>" +
                        "<td>" + obj.ProductionDate.Value.ToString("yyyy/MM/dd") + "</td>" +
                        "<td style=\"text-align: center;\">" + (obj.HasBound == true ? "yes" : "no") + "</td>" +
                        "<td>" + GetEquipment(obj, equipment) + "</td>" +
                        "<td>" + Utility.GetOnlineStyle(obj.OnlineStyle, false) + "</td>" +
                        "<td></td>" +
                        "</tr>";
                }
            }
            tbodyBody.InnerHtml = html;
            divPagging.InnerHtml = "";
            if (totalRecords > 0)
                ShowPaggings(pageIndex, totalPages, totalRecords, "./terminal_list.aspx", divPagging);
        }

        protected void btQuery_Click(object sender, EventArgs e)
        {
            if (!HasSessionLose)
            { ShowTerminals(); }
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
        private void SendDD02Command(bool bound,TB_Terminal terminal)
        {
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
            if (index < 0) {
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
            TerminalInstance.Update(f => f.id == id, act => {
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
    }
}