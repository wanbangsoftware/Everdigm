using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.Web.main
{
    public partial class equipment_setting : BaseEquipmentPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (!HasSessionLose)
            {
                initializeSessionKey(); 
                if (!IsPostBack)
                {
                    ShowOldInformations();
                }
            }
        }
        /// <summary>
        /// 显示待修改的设备的信息
        /// </summary>
        private void ShowOldInformations()
        {
            var id = ParseInt(Utility.Decrypt(_key));
            var equipment = EquipmentInstance.Find(f => f.id == id && f.Deleted == false);
            if (null != equipment)
            {
                number.Value = equipment.Number;
                old.Value = equipment.Number;
                oldFunc.Value = equipment.Functional.Value.ToString();
                if (equipment.Terminal != (int?)null)
                {
                    oldTerminal.Value = Utility.UrlEncode(Utility.Encrypt(equipment.Terminal.Value.ToString()));
                    terminalInfo.Cells[1].InnerHtml = equipment.TB_Terminal.Number + " <a href=\"#unbind\">Unbind?</a>";
                    terminalInfo.Cells[3].InnerText = equipment.TB_Terminal.Sim;
                }
                else
                {
                    oldTerminal.Value = "";
                    terminalInfo.Cells[1].InnerHtml = "<a href=\"#bind\">Click here to bind</a>";
                    terminalInfo.Cells[3].InnerText = "-";
                }
            }
            // 显示设备的类型
            ShowEquipmentFunctional();
        }
        private void ShowEquipmentFunctional()
        {
            string html = "";
            foreach (EquipmentFunctional f in Enum.GetValues(typeof(EquipmentFunctional)))
            {
                html += "<li role=\"presentation\"><a role=\"menuitem\" tabindex=\"" + (byte)f + "\" href=\"#\">" +
                    (f != EquipmentFunctional.Loader ? "Excavator: " : "") + Utility.GetEquipmentFunctional((byte)f) + "</a></li>";
            }
            menuFunctional.InnerHtml = html;
        }
        protected void btSaveInfo_Click(object sender, EventArgs e)
        {
            if (!HasSessionLose)
            {
                SaveChanges();
            }
        }
        /// <summary>
        /// 保存设备的更改信息
        /// </summary>
        private void SaveChanges()
        {
            var id = ParseInt(Utility.Decrypt(_key));
            var equipment = EquipmentInstance.Find(f => f.id == id && f.Deleted == false);
            bool needSave = false;
            if (null != equipment)
            {
                var tmp = int.Parse(hidWarehouse.Value);
                if (tmp > 0)
                {
                    equipment.Warehouse = tmp;
                    needSave = true;
                }
                tmp = int.Parse(selectedModel.Value);
                if (tmp > 0)
                {
                    equipment.Model = tmp;
                    needSave = true;
                }
                tmp = int.Parse(hidFunctional.Value);
                if (tmp != 0 && tmp != int.Parse(oldFunc.Value))
                {
                    equipment.Functional = (byte)tmp;
                    needSave = true;
                }
                var num = number.Value.Trim();
                if (!string.IsNullOrEmpty(num))
                {
                    equipment.Number = num;
                    needSave = true;
                }
                //tmp = int.Parse(Utility.Decrypt(Utility.UrlDecode(oldTerminal.Value)));
                var n = string.IsNullOrEmpty(newTerminal.Value) ? 0 : int.Parse(Utility.Decrypt(Utility.UrlDecode(newTerminal.Value)));
                TB_Terminal newOne = null;
                if (n > 0) {
                    // 更新旧终端为未绑定状态
                    if ((int?)null != equipment.Terminal)
                    {
                        TerminalInstance.Update(f => f.id == equipment.Terminal, act =>
                        {
                            act.HasBound = false;
                        });
                    }
                    newOne = TerminalInstance.Find(f => f.id == n);
                    equipment.Terminal = n;
                    // 更新新终端的绑定状态
                    TerminalInstance.Update(f => f.id == n, act => { act.HasBound = true; });
                    needSave = true;
                }
                if (needSave)
                {
                    EquipmentInstance.Update(f => f.id == equipment.id && f.Deleted == false, act =>
                    {
                        if (act.Model != equipment.Model)
                            act.Model = equipment.Model;
                        if (act.Warehouse != equipment.Warehouse)
                            act.Warehouse = equipment.Warehouse;
                        if (!act.Number.Equals(equipment.Number))
                            act.Number = equipment.Number;
                        if (act.Functional != equipment.Functional)
                            act.Functional = equipment.Functional;
                        if (act.Terminal != equipment.Terminal)
                        {
                            act.Terminal = equipment.Terminal;
                            act.Socket = newOne.Socket;
                            act.OnlineTime = newOne.OnlineTime;
                            act.OnlineStyle = newOne.OnlineStyle;
                        }
                    });
                    // 保存更改设备信息的历史
                    SaveHistory(new Database.TB_AccountHistory()
                    {
                        ActionId = ActionInstance.Find(f => f.Name.Equals("EditEquipmentInfo")).id,
                        ObjectA = EquipmentInstance.GetFullNumber(equipment)
                    });
                    ShowNotification("./equipment_setting.aspx?key=" + Utility.UrlEncode(_key), "You have saved the equipment info.", true);
                }
            }
            else
            {
                ShowNotification("./equipment_setting.aspx?key=" + Utility.UrlEncode(_key), "Not found the equipment", false);
            }
        }

        protected void btUnbind_Click(object sender, EventArgs e)
        {
            if (!HasSessionLose)
            {
                var id = ParseInt(Utility.Decrypt(_key));
                var equipment = EquipmentInstance.Find(f => f.id == id && f.Deleted == false);
                if (null == equipment) {
                    ShowNotification("./equipment_setting.aspx?key=" + Utility.UrlEncode(_key), "Not found the equipment", false);
                }
                else
                {
                    var ter = equipment.TB_Terminal.Number;
                    TerminalInstance.Update(f => f.id == equipment.Terminal, act => { act.HasBound = false; });
                    EquipmentInstance.Update(f => f.id == equipment.id, act =>
                    {
                        act.Terminal = (int?)null;
                        act.OnlineStyle = (byte?)null;
                        act.OnlineTime = (DateTime?)null;
                        act.Socket = 0;
                        act.IP = "";
                        act.LastAction = "";
                        act.LastActionBy = "";
                        act.LastActionTime = (DateTime?)null;
                        act.Latitude = 0.0;
                        act.Longitude = 0.0;
                        act.GpsAddress = "";
                        act.Rpm = 0;
                        act.ServerName = "";
                        act.Voltage = "G0000";
                    });
                    // 保存解绑终端历史
                    SaveHistory(new TB_AccountHistory()
                    {
                        ActionId = ActionInstance.Find(f => f.Name.Equals("Unbind")).id,
                        ObjectA = "unbind terminal " + ter + " and equipment " + EquipmentInstance.GetFullNumber(equipment)
                    });
                    ShowNotification("./equipment_setting.aspx?key=" + Utility.UrlEncode(_key), "You have unbind terminal & equipment.");
                }
            }
        }

        protected void btDelete_Click(object sender, EventArgs e)
        {
            // 删除设备
            var id = ParseInt(Utility.Decrypt(_key));
            var equipment = EquipmentInstance.Find(f => f.id == id);
            if (null == equipment)
            {
                ShowNotification("./equipment_setting.aspx?key=" + Utility.UrlEncode(_key), "Not found the equipment", false);
            }
            else
            {
                string number = EquipmentInstance.GetFullNumber(equipment);
                // 解绑终端
                TerminalInstance.Update(f => f.id == equipment.Terminal, act =>
                {
                    act.HasBound = false;
                });
                // 更新Deleted=true
                EquipmentInstance.Update(f => f.id == equipment.id, act => {
                    act.Deleted = true;
                    act.GpsAddress = "";
                    act.IP = "";
                    act.LastAction = "";
                    act.LastActionBy = "";
                    act.LastActionTime = (DateTime?)null;
                    act.Latitude = 0.0;
                    act.Longitude = 0.0;
                    act.OnlineStyle = (byte?)null;
                    act.OnlineTime = (DateTime?)null;
                    act.Port = 0;
                    act.Rpm = 0;
                    act.Runtime = 0;
                    act.ServerName = "";
                    act.Signal = 0;
                    act.Socket = 0;
                    act.Terminal = (int?)null;
                    act.Voltage = "G0000";
                });
                // 保存删除设备历史
                SaveHistory(new TB_AccountHistory()
                {
                    ActionId = ActionInstance.Find(f => f.Name.Equals("Unbind")).id,
                    ObjectA = "delete equipment " + number
                });
                ShowNotification("./equipment_inquiry.aspx", "You have deleted a equipment: " + number);
            }
        }
    }
}