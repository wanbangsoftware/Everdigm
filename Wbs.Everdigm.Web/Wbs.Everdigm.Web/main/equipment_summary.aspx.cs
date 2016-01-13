using System;
using System.Linq;

namespace Wbs.Everdigm.Web.main
{
    public partial class equipment_summary : BaseEquipmentPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (!HasSessionLose)
            { ShowCount(); }
        }

        private void ShowCount()
        {
            var list = EquipmentInstance.FindList(f => f.Deleted == false);

            // 统计野外
            var total = 0;
            // 统计销售的
            var count = list.Count(c => c.TB_EquipmentStatusName.IsItOutstorage == true);
            countSold.InnerText = count.ToString();
            total += count;
            // 统计租赁的
            count = list.Count(c => c.TB_EquipmentStatusName.IsItRental == true);
            countRental.InnerText = count.ToString();
            total += count;
            totalField.InnerText = total.ToString();
            // 统计野外现在正启动着的
            count = list.Count(c => (c.TB_EquipmentStatusName.IsItRental == true || 
                c.TB_EquipmentStatusName.IsItOutstorage == true) && 
                c.Voltage.IndexOf("G2") >= 0);
            countWorking.InnerText = count.ToString();
            // 统计野外未启动的
            countIdle.InnerText = (total - count).ToString();

            // 统计库存的
            total = 0;
            // 统计新产品
            count = list.Count(c => c.TB_EquipmentStatusName.IsItInventory == true && c.StoreTimes <= 1);
            countNew.InnerText = count.ToString();
            total += count;
            // 统计租赁回收的
            count = list.Count(c => c.TB_EquipmentStatusName.IsItInventory == true && c.StoreTimes > 1);
            countFleet.InnerText = count.ToString();
            total += count;
            totalInventory.InnerText = total.ToString();

            // 统计正在维修的
            total = list.Count(c => c.TB_EquipmentStatusName.IsItOverhaul == true);
            totalRepair.InnerText = total.ToString();
            countOverhaul.InnerText = total.ToString();
        }
    }
}