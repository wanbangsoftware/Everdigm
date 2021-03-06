﻿using System;
using System.Linq;
using Wbs.Everdigm.Database;
using Wbs.Protocol;
using System.Configuration;

namespace Wbs.Everdigm.Web.main
{
    public partial class terminal_register : BaseTerminalPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (!HasSessionLose)
            {
                if (!IsPostBack)
                {
                    if (!string.IsNullOrEmpty(_key))
                    {
                        hidID.Value = _key;
                        ShowEdit();
                    }
                    else {
                        // 显示新注册一个终端号码
                        NewTerminalNo();
                    }
                }
            }
        }
        private void NewTerminalNo()
        {
            var no = DateTime.Now.ToString("yyyyMMdd");
            // 查找当前日期中比1号更大的终端号码并倒叙排列
            var ter = TerminalInstance.FindList<TB_Terminal>(f => f.Number.StartsWith(no) && f.Delete == false, "Number", true).FirstOrDefault();
            if (null != ter)
            {
                no = (ParseInt(ter.Number) + 1).ToString();
            }
            else {
                no += "01";
            }
            txtNumber.Value = no;
            NewSimNo();
        }

        private void NewSimNo()
        {
            var auto = int.Parse(ConfigurationManager.AppSettings["AutoSimCardNumber"]);
            if (auto > 0)
            {
                var tmp = TerminalInstance.FindList<TB_Terminal>(f => f.Sim.Contains("89000"), "Sim", true).FirstOrDefault();
                if (null != tmp)
                {
                    var s = int.Parse(tmp.Sim);
                    txtSimcard.Value = (s + 1).ToString();
                }
                else
                {
                    txtSimcard.Value = "89000001";
                }
            }
        }
        private void ShowEdit()
        {
            var t = TerminalInstance.Find(f => f.id == ParseInt(Utility.Decrypt(hidID.Value)));
            if (null != t)
            {
                txtNumber.Value = t.Number;
                //txtSatellite.Value = t.Satellite;
                txtSimcard.Value = t.Sim;
                if (t.Type == TerminalTypes.DX)
                    _dx_normal.Checked = true;
                else if (t.Type == TerminalTypes.DXE)
                    _dx.Checked = true;
                else
                    _ld.Checked = true;
            }
            else { ShowNotification("./terminal_list.aspx", "Error: Cannot edit null object of Terminal.", false); }
        }

        private void BuildObject(TB_Terminal obj)
        {
            obj.Number = txtNumber.Value.Trim();
            //obj.Satellite = txtSatellite.Value.Trim();
            obj.Sim = txtSimcard.Value.Trim();
            obj.Type = byte.Parse(_dx_normal.Checked ? _dx_normal.Value : (_dx.Checked ? _dx.Value : _ld.Value));
        }

        private void NewTerminal()
        {
            TB_Terminal t = null;
            // 如果没有输入Sim卡号码则查询是否具有相同终端号码的记录
            if (string.IsNullOrEmpty(txtSimcard.Value.Trim()))
            {
                t = TerminalInstance.Find(f => f.Number.Equals(txtNumber.Value.Trim()) && f.Delete == false);
            }
            else
            {
                // 如果有Sim卡号码输入则查询终端或Sim卡号码是否有相同记录存在
                t = TerminalInstance.Find(f => (f.Number.Equals(txtNumber.Value.Trim()) || f.Sim.Equals(txtSimcard.Value.Trim())) && f.Delete == false);
            }

            if (null != t)
            {
                ShowNotification("./terminal_register.aspx", "Terminal exist: " + TerminalInstance.ToString(t), false);
            }
            else
            {
                t = TerminalInstance.GetObject();
                BuildObject(t);
                TerminalInstance.Add(t);

                SaveHistory(new TB_AccountHistory
                {
                    ActionId = ActionInstance.Find(f => f.Name.Equals("AddTerminal")).id,
                    ObjectA = TerminalInstance.ToString(t)
                });

                ShowNotification("./terminal_list.aspx", "You added a new terminal: " + TerminalInstance.ToString(t));
            }
        }

        private void EditTerminal()
        {
            var id = ParseInt(Utility.Decrypt(hidID.Value));
            var t = TerminalInstance.Find(f => (f.Number.Equals(txtNumber.Value.Trim()) || f.Sim.Equals(txtSimcard.Value.Trim())) && 
                f.id != id && f.Delete == false);
            if (null == t)
            {
                t = TerminalInstance.Find(f => f.id == ParseInt(Utility.Decrypt(hidID.Value)));
                if (null != t)
                {
                    BuildObject(t);
                    Update(t);

                    SaveHistory(new TB_AccountHistory
                    {
                        ActionId = ActionInstance.Find(f => f.Name.Equals("EditTerminal")).id,
                        ObjectA = TerminalInstance.ToString(t)
                    });
                    ShowNotification("./terminal_list.aspx", "You changed terminal: " + TerminalInstance.ToString(t));
                }
                else { ShowNotification("./terminal_list.aspx", "Error: Cannot edit null object of Terminal.", false); }
            }
            else
            {
                ShowNotification("./terminal_register.aspx?key=" + Utility.UrlEncode(hidID.Value),
                    "Error: New terminal number or sim card number has exists: " + TerminalInstance.ToString(t), false);
            }
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            if (!HasSessionLose)
            {
                if ("" == hidID.Value) { NewTerminal(); }
                else { EditTerminal(); }
            }
        }
    }
}