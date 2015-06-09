using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wbs.Everdigm.Web.main
{
    public partial class equipment_setting : BaseEquipmentPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (!HasSessionLose)
            {
                if (!IsPostBack)
                { ShowOldInformations(); }
            }
        }
        /// <summary>
        /// 显示待修改的设备的信息
        /// </summary>
        private void ShowOldInformations()
        { 
            var id = ParseInt(Utility.Decrypt(_key));
            var equipment = EquipmentInstance.Find(f => f.id == id);
            if (null != equipment)
            {
                number.Value = equipment.Number;
                old.Value = equipment.Number;
            }
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
            var equipment = EquipmentInstance.Find(f => f.id == id);
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
                var num = number.Value.Trim();
                if (!string.IsNullOrEmpty(num))
                {
                    equipment.Number = num;
                    needSave = true;
                }
                if (needSave)
                {
                    EquipmentInstance.Update(f => f.id == equipment.id, act =>
                    {
                        if (act.Model != equipment.Model)
                            act.Model = equipment.Model;
                        if (act.Warehouse != equipment.Warehouse)
                            act.Warehouse = equipment.Warehouse;
                        if (!act.Number.Equals(equipment.Number))
                            act.Number = equipment.Number;
                    });
                    ShowNotification("./equipment_setting.aspx?key=" + Utility.UrlEncode(_key), "You have saved the equipment info.", true);
                }
            }
            else
            {
                ShowNotification("./equipment_setting.aspx?key=" + Utility.UrlEncode(_key), "Not found the equipment", false);
            }
        }
    }
}