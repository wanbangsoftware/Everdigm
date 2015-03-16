using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Wbs.Everdigm.BLL;
using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.Web.main
{
    public partial class equipment_terminal : BaseEquipmentPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (!HasSessionLose) 
            {
                if (!IsPostBack)
                { ShowEquipmentTypes(ddlType); }
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
                BindEquipment(ParseInt(hidTerminalId.Value), ParseInt(hidEquipmentId.Value));
            }
        }

        private void BindEquipment(int terminal, int equipment)
        {
            var ter = TerminalInstance.Find(f => f.id == terminal);
            if (null == ter)
            {
                ShowNotification("./equipment_terminal.aspx", "Error: Cannot find the terminal.", false);
                return;
            }
            else if (ter.HasBound == true)
            {
                ShowNotification("./equipment_terminal.aspx", "Error: The terminal \"" + ter.Number + "\" has been bound before this time.", false);
                return;
            }
            var equ = EquipmentInstance.Find(f => f.id == equipment);
            if (null == equ)
            {
                ShowNotification("./equipment_terminal.aspx", "Error: Cannot find the equipment.", false);
                return;
            }
            else if (equ.Terminal > 0)
            {
                ShowNotification("./equipment_terminal.aspx", "Error: The equipment \"" +
                    EquipmentInstance.GetFullNumber(equ) + "\" has bound an other terminal: \"" + equ.TB_Terminal.Number + "\".", false);
                return;
            }

            // 开始绑定流程
            var storage = CodeInstance.Find(f =>
                    f.TB_EquipmentStatusName.IsInventory == true && f.Code.Equals("N"));
            EquipmentInstance.Update(f => f.id == equ.id, act =>
            {
                act.Terminal = ter.id;
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
                ActionId = ActionInstance.Find(f => f.Name.Equals("Bond")).id,
                ObjectA = EquipmentInstance.GetFullNumber(equ) + " - " + ter.Number
            });

            ShowNotification("./equipment_terminal.aspx", "You have bound \"" + ter.Number + "\" on equipment \"" +
                EquipmentInstance.GetFullNumber(equ) + "\"");
        }
    }
}