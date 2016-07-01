using System;

using Wbs.Everdigm.BLL;
using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.Web.main
{
    public partial class equipment_terminal : BaseEquipmentPage
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
                    hidTerminalId.Value = _key;
                    ShowEquipmentTypes(ddlType);
                    ShowTerminalInfo();
                    ShowNotbindEquipments();
                }
            }
        }
        /// <summary>
        /// 显示已选择的终端的信息
        /// </summary>
        private void ShowTerminalInfo()
        {
            var id = int.Parse(Utility.Decrypt(hidTerminalId.Value));
            var terminal = TerminalInstance.Find(f => f.id == id);
            if (null == terminal)
            {
                ShowNotification("./terminal_list.aspx", "Error: terminal not exist.", false);
            }
            else
            {
                hiddenType.Value = terminal.Type.Value.ToString();
                terType.InnerText = Wbs.Protocol.TerminalTypes.GetTerminalType(terminal.Type.Value);
                terminalinfo.Rows[1].Cells[1].InnerText = terminal.Number;
                terminalinfo.Rows[1].Cells[3].InnerText = terminal.Sim;
                terminalinfo.Rows[1].Cells[5].InnerText = (int?)null == terminal.Satellite ? "-" : terminal.TB_Satellite.CardNo;

                terminalinfo.Rows[2].Cells[1].InnerText = terminal.Firmware;
                terminalinfo.Rows[2].Cells[3].InnerText = (DateTime?)null == terminal.OnlineTime ? "-" : terminal.OnlineTime.Value.ToString("yyyy/MM/dd HH:mm:ss");
                terminalinfo.Rows[2].Cells[5].InnerHtml = Utility.GetOnlineStyle(terminal.OnlineStyle, false);
            }
        }
        /// <summary>
        /// 绑定终端和设备
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btSave_Click(object sender, EventArgs e)
        {
            if (!HasSessionLose)
            {
                BindEquipment(int.Parse(Utility.Decrypt(hidTerminalId.Value)), ParseInt(hidEquipmentId.Value));
            }
        }

        private void BindEquipment(int terminal, int equipment)
        {
            var ter = TerminalInstance.Find(f => f.id == terminal);
            if (null == ter)
            {
                ShowNotification("./terminal_list.aspx", "Error: Cannot find the terminal.", false);
                return;
            }
            else if (ter.HasBound == true)
            {
                ShowNotification("./terminal_list.aspx", "Error: The terminal \"" + ter.Number + "\" has been bound before this time.", false);
                return;
            }
            var equ = EquipmentInstance.Find(f => f.id == equipment && f.Deleted == false);
            if (null == equ)
            {
                ShowNotification("./terminal_list.aspx", "Error: Cannot find the equipment.", false);
                return;
            }
            else if (equ.Terminal > 0)
            {
                ShowNotification("./terminal_list.aspx", "Error: The equipment \"" +
                    EquipmentInstance.GetFullNumber(equ) + "\" has bound an other terminal: \"" + equ.TB_Terminal.Number + "\".", false);
                return;
            }

            // 开始绑定流程
            var storage = StatusInstance.Find(f => f.IsItInventory == true);
            EquipmentInstance.Update(f => f.id == equ.id && f.Deleted == false, act =>
            {
                act.Terminal = ter.id;
                // 更新设备的相应信息为终端的信息
                act.OnlineStyle = ter.OnlineStyle;
                act.OnlineTime = ter.OnlineTime;
                act.Socket = ter.Socket;
                // 新品等待入库的，绑定终端之后直接确定为库存状态
                act.Status = storage.id;
            });
            TerminalInstance.Update(f => f.id == ter.id, act => { act.HasBound = true; });

            // 保存入库信息
            var history = StoreInstance.GetObject();
            history.Equipment = equ.id;
            history.Status = storage.id;
            history.Stocktime = DateTime.Now;
            // 绑定终端时，入库次数不变
            history.StoreTimes = equ.StoreTimes;
            history.Warehouse = equ.Warehouse;
            StoreInstance.Add(history);

            // 保存操作历史记录
            SaveHistory(new TB_AccountHistory()
            {
                ActionId = ActionInstance.Find(f => f.Name.Equals("Bind")).id,
                ObjectA = "bind equipment " + EquipmentInstance.GetFullNumber(equ) + " and terminal " + ter.Number
            });

            ShowNotification("./terminal_list.aspx", "You have bound \"" + ter.Number + "\" on equipment \"" +
                EquipmentInstance.GetFullNumber(equ) + "\"");
        }
        /// <summary>
        /// 通过终端的类型查找相应的设备
        /// </summary>
        /// <returns></returns>
        private byte GetEquipmentTypeByTerminalType()
        {
            byte b = byte.Parse(hiddenType.Value);
            byte ret = (byte)EquipmentFunctional.Mechanical;
            switch (b)
            {
                case Wbs.Protocol.TerminalTypes.DX: ret = (byte)EquipmentFunctional.Mechanical; break;
                case Wbs.Protocol.TerminalTypes.DXE: ret = (byte)EquipmentFunctional.Electric; break;
                case Wbs.Protocol.TerminalTypes.LD: ret = (byte)EquipmentFunctional.Loader; break;
            }
            return ret;
        }
        private void ShowNotbindEquipments()
        {
            var totalRecords = 0;
            var pageIndex = "" == hidPageIndex.Value ? 1 : int.Parse(hidPageIndex.Value);
            var list = EquipmentInstance.FindPageList<TB_Equipment>(pageIndex, PageSize, out totalRecords,
                f => f.Terminal == (int?)null && f.Number.IndexOf(txtEquipment.Value.Trim()) >= 0 &&
                    f.Functional == GetEquipmentTypeByTerminalType() && f.Deleted == false, null);
            var totalPages = totalRecords / PageSize + (totalRecords % PageSize > 0 ? 1 : 0);

            string html = "";
            if (totalRecords < 1)
            {
                html = "<tr><td colspan=\"9\">No equipment has unbind terminal.</td></tr>";
            }
            else
            {
                var cnt = (pageIndex - 1) * PageSize;
                var n = (int?)null;
                foreach (var obj in list)
                {
                    cnt++;
                    html += "<tr style=\"cursor: pointer;\">" +
                             "<td style=\"text-align: center;\">" +
                             "    <input type=\"radio\" name=\"bind\" id=\"radio_" + obj.id + "\" />" +
                             "</td>" +
                             "<td style=\"text-align: center;\">" + cnt + "</td>" +
                             "<td>" + (n == obj.Model ? "-" : obj.TB_EquipmentModel.TB_EquipmentType.Code) + "</td>" +
                             "<td>" + EquipmentInstance.GetFullNumber(obj) + "</td>" +
                             "<td style=\"text-align: right;\">" + EquipmentBLL.GetRuntime(obj.Runtime + obj.InitializedRuntime, obj.CompensatedHours.Value) + "</td>" +
                             "<td style=\"text-align: center;\" title=\"" + EquipmentInstance.GetStatusTitle(obj) + "\">" + EquipmentInstance.GetStatus(obj) + "</td>" +
                             "<td>" + (n == obj.Terminal ? "not bind" : obj.TB_Terminal.Number) + "</td>" +
                             "<td>" + (n == obj.Warehouse ? "-" : obj.TB_Warehouse.Name) + "</td>" +
                             "<td>" + obj.GpsAddress + "</td>" +
                             "<td></td>" +
                             "</tr>";
                }
            }
            tbodyBody.InnerHtml = html;
            divPagging.InnerHtml = "";
            if (totalRecords > 0)
                ShowPaggings(pageIndex, totalPages, totalRecords, "./equipment_terminal.aspx", divPagging);
        }
        protected void btQuery_Click(object sender, EventArgs e)
        {
            ShowNotbindEquipments();
        }
    }
}