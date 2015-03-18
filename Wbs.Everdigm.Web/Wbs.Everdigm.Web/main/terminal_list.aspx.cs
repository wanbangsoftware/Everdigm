using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Wbs.Everdigm.Database;

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

        private void ShowTerminals()
        {
            var totalRecords = 0;
            var pageIndex = "" == hidPageIndex.Value ? 1 : int.Parse(hidPageIndex.Value);
            var list = TerminalInstance.FindPageList<TB_Terminal>(pageIndex, PageSize, out totalRecords,
                f => f.Delete == false && f.Number.Contains(txtNumber.Value) &&
                    f.Sim.Contains(txtSimcard.Value), "Number");
            var totalPages = totalRecords / PageSize + (totalRecords % PageSize > 0 ? 1 : 0);
            pageIndex = 0 == pageIndex ? totalPages : pageIndex;
            if (pageIndex > totalPages)
            {
                pageIndex = totalPages;
                list = TerminalInstance.FindPageList<TB_Terminal>(pageIndex, PageSize, out totalRecords, 
                    f => f.Delete == false && f.Number.Contains(txtNumber.Value) && 
                        f.Sim.Contains(txtSimcard.Value), "Number");
            }

            string html = "";
            if (totalRecords < 1)
            {
                html = "<tr><td colspan=\"11\">No records, You can change the condition and try again or " +
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
                    html += "<tr>" +
                        "<td style=\"text-align: center;\"><input type=\"checkbox\" id=\"cb_" + id + "\" /></td>" +
                        "<td style=\"text-align: center;\">" + cnt + "</td>" +
                        "<td><a href=\"./terminal_register.aspx?key=" + id + "\" >" + CheckQueryString(obj.Number, 0) + "</a></td>" +
                        "<td>" + CheckQueryString(obj.Sim, 1) + "</td>" +
                        "<td>" + TerminalInstance.GetSatellite(obj, true) + "</td>" +
                        "<td>" + obj.Firmware + "</td>" +
                        "<td style=\"text-align: center;\">" + obj.Revision.ToString() + "</td>" +
                        "<td style=\"text-align: center;\">" + obj.Type + "</td>" +
                        "<td>" + obj.ProductionDate.Value.ToString("yyyy/MM/dd") + "</td>" +
                        "<td style=\"text-align: center;\">" + (obj.HasBound == true ? "yes" : "-") + "</td>" +
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
            if (null == t) { ShowNotification("./terminal_list.aspx", "Unbound fail: Terminal not exists.", false); }
            else
            {
                if ((int?)null == t.Satellite) { ShowNotification("./terminal_list.aspx", "Unbound fail: No Satellite bound on it.", false); }
                else
                {
                    string satno = t.TB_Satellite.CardNo;
                    TerminalInstance.Update(f => f.id == t.id, act => { act.Satellite = (int?)null; });
                    SatelliteInstance.Update(f => f.id == t.Satellite, act => { act.Bound = false; });
                    SaveHistory(new TB_AccountHistory()
                    {
                        ActionId = ActionInstance.Find(f => f.Name.Equals("UnboundSat")).id,
                        ObjectA = "Ter: " + t.Number + " unbound Sat: " + satno
                    });
                    ShowNotification("./terminal_list.aspx", "Ter: " + t.Number + " unbound Sat: " + satno + " OK!");
                }
            }
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
                            TerminalInstance.Update(f => f.id == t.id, act => {
                                act.Satellite = g.id;
                            });
                            SatelliteInstance.Update(f => f.id == g.id, act => { act.Bound = true; });
                            // 保存绑定卫星模块的历史记录
                            SaveHistory(new TB_AccountHistory()
                            {
                                ActionId = ActionInstance.Find(f => f.Name.Equals("BoundSat")).id,
                                ObjectA = TerminalInstance.ToString(t)
                            });
                            //ShowTerminals();
                            ShowNotification("./terminal_list.aspx", "Terminal \"" + t.Number + "\" bound Satellite \"" + g.CardNo + "\" OK!");
                        }
                    }
                }
            }
        }
    }
}