using System;
using Wbs.Everdigm.Database;
using Wbs.Everdigm.Common;

namespace Wbs.Everdigm.Web.main
{
    public partial class equipment_new_product : BaseEquipmentPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (!HasSessionLose)
            {
                if (!IsPostBack)
                {
                    ShowEquipmentFunctional();
                }
            }
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

        protected void btSave_Click(object sender, EventArgs e)
        {
            if (!HasSessionLose)
            {
                SaveNewEquipment();
            }
        }

        private void SaveNewEquipment()
        {
            var value = hidNewInstorage.Value;
            var equipment = JsonConverter.ToObject<TB_Equipment>(value);
            // 查找是否有相同型号的相同设备号码
            var exist = EquipmentInstance.Find(f => f.Model == equipment.Model && f.Number.Equals(equipment.Number) && f.Deleted == false);
            if (null == exist)
            {
                var model = ModelInstance.Find(f => f.id == equipment.Model);
                var newOne = EquipmentInstance.GetObject();
                newOne.Model = equipment.Model;
                if (model.TB_EquipmentType.IsVehicle == true)
                {
                    // 新增的设备是普通车辆时，直接划为车辆，不参与出库/入库流程
                    newOne.Status = StatusInstance.Find(f => f.IsItVehicle == true).id;
                }
                else
                {
                    newOne.Status = StatusInstance.Find(f => f.IsItInventory == true).id;
                }
                newOne.Warehouse = equipment.Warehouse;
                newOne.Number = equipment.Number;
                newOne.StoreTimes = equipment.StoreTimes;
                newOne.Functional = equipment.Functional;
                newOne = EquipmentInstance.Add(newOne);

                // 保存入库信息
                var history = StoreInstance.GetObject();
                history.Equipment = newOne.id;
                history.Status = newOne.Status;
                history.Stocktime = DateTime.Parse(inDate.Value);
                // 默认第1次入库
                history.StoreTimes = newOne.StoreTimes;
                history.Warehouse = newOne.Warehouse;
                StoreInstance.Add(history);

                // 保存入库操作历史记录
                SaveHistory(new TB_AccountHistory()
                {
                    ActionId = ActionInstance.Find(f => f.Name.Equals("InStore")).id,
                    ObjectA = EquipmentInstance.ToString(newOne)
                });

                ShowNotification("./equipment_new_product.aspx",
                    "New equipment \"" + EquipmentInstance.GetFullNumber(exist) + "\" has been saved.");
            }
            else
            {
                ShowNotification("./equipment_new_product.aspx", 
                    "There has a same number of \"" + EquipmentInstance.GetFullNumber(exist) + "\" exist.", false);
            }
        }
    }
}