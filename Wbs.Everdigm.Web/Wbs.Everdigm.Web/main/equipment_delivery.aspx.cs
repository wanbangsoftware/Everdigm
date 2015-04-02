using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wbs.Everdigm.Web.main
{
    public partial class equipment_delivery : BaseEquipmentPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (!HasSessionLose)
            {
                if (!IsPostBack)
                {
                    ShowEquipmentTypes(ddlType);
                    ShowOutStorageSituations();
                }
            }
        }

        private void ShowOutStorageSituations()
        {
            //ddlSituation.Items.Clear();
            //ddlSituation.Items.Add(new ListItem() { Text = "Deliver as:", Value = "", Selected = true });
            //var list = CodeInstance.FindList(f => f.TB_EquipmentStatusName.IsOutstorage == true).OrderBy(o => o.Name);
            //foreach (var obj in list)
            //{
            //    ddlSituation.Items.Add(new ListItem() { Text = obj.Name, Value = obj.id.ToString() });
            //}
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            if (!HasSessionLose)
            {
                // 设备出库
                if (!string.IsNullOrEmpty(hidCustomerId.Value.Trim()) &&
                    !string.IsNullOrEmpty(hidEquipmentId.Value.Trim()))
                {
                    var obj = EquipmentInstance.Find(f => f.id == ParseInt(hidEquipmentId.Value.Trim()));
                    if (obj.TB_EquipmentStatusName.IsItInventory == false)
                    {
                        // 如果不在库存状态则提示失败
                        ShowNotification("./equipment_delivery.aspx", "Error: equipment \"" +
                            obj.TB_EquipmentModel.Code + obj.Number + "\" is not in inventory.", false);
                    }
                    else
                    {
                        EquipmentInstance.Update(f => f.id == obj.id, act =>
                        {
                            act.Status = ParseInt(ddlSituation.SelectedValue);
                            // 出库后库存信息置为null
                            act.Warehouse = (int?)null;
                            act.Customer = ParseInt(hidCustomerId.Value);
                        });

                        // 保存出库历史记录
                        var history = StoreInstance.GetObject();
                        history.Equipment = obj.id;
                        history.Status = ParseInt(ddlSituation.SelectedValue);
                        history.Stocktime = DateTime.Now;
                        // 设备的出入库次数，入库时增1，出库时不变
                        history.StoreTimes = obj.StoreTimes;
                        history.Warehouse = (int?)null;
                        StoreInstance.Add(history);

                        // 保存操作历史记录
                        SaveHistory(new Database.TB_AccountHistory()
                        {
                            ActionId = ActionInstance.Find(f => f.Name.Equals("Deliver")).id,
                            ObjectA = ""
                        });

                        ShowNotification("./equipment_deliver.aspx", "\"" + EquipmentInstance.GetFullNumber(obj) + "\" has delivered.");
                    }
                }
            }
        }
    }
}