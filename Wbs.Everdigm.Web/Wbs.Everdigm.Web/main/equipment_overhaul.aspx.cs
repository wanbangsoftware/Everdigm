using System;

using Wbs.Everdigm.Database;
using Wbs.Everdigm.BLL;

namespace Wbs.Everdigm.Web.main
{
    public partial class equipment_overhaul : BaseEquipmentPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            _cookie_name_ = "_equipment_overhaul_list_page_";
            cookieName.Value = _cookie_name_;
            base.Page_Load(sender, e);
            if (!HasSessionLose)
            {
                if (!IsPostBack)
                {
                    hidPageIndex.Value = null == Request.Cookies[_cookie_name_] ? "1" : Request.Cookies[_cookie_name_].Value;
                    ShowOverhaulEquipments();
                }
            }
        }
        /// <summary>
        /// 显示处于修理状态的设备列表
        /// </summary>
        private void ShowOverhaulEquipments()
        {
            var query = txtQueryNumber.Value.Trim();
            // 模糊查询时页码置为空
            if (!string.IsNullOrEmpty(query)) { hidPageIndex.Value = ""; }

            var totalRecords = 0;
            var pageIndex = "" == hidPageIndex.Value ? 1 : int.Parse(hidPageIndex.Value);
            pageIndex = (0 >= pageIndex ? 1 : pageIndex);
            var model = ParseInt(selectedModels.Value);
            var house = ParseInt(hidQueryWarehouse.Value);
            var list = EquipmentInstance.FindPageList<TB_Equipment>(pageIndex, PageSize, out totalRecords,
                f => f.TB_EquipmentStatusName.IsItOverhaul == true && (model <= 0 ? f.Model >= 0 : f.Model == model) &&
                    (house <= 0 ? (f.Warehouse >= 0 || f.Warehouse == (int?)null) : f.Warehouse == house) &&
                    (f.Number.Contains(query) && f.Deleted == false && f.Terminal != (int?)null), null);
            var totalPages = totalRecords / PageSize + (totalRecords % PageSize > 0 ? 1 : 0);
            hidTotalPages.Value = totalPages.ToString();

            string html = "";
            if (totalRecords < 1)
            {
                html = "<tr><td colspan=\"15\">No records, You can change the condition and try again.</td></tr>";
            }
            else
            {
                var cnt = (pageIndex - 1) * PageSize;
                var n = (int?)null;
                foreach (var obj in list)
                {
                    cnt++;
                    // 入库记录
                    var _in = StoreInstance.GetStoreInfo(obj.id, obj.StoreTimes.Value, true);
                    // 出库记录
                    var _out = StoreInstance.GetStoreInfo(obj.id, obj.StoreTimes.Value, false);

                    var id = Utility.UrlEncode(Utility.Encrypt(obj.id.ToString()));

                    html += "<tr>" +
                        "<td class=\"in-tab-txt-b\">" + cnt + "</td>" +
                        "<td class=\"in-tab-txt-b\">" + (n == obj.Model ? "-" : obj.TB_EquipmentModel.TB_EquipmentType.Code) + "</td>" +
                        "<td class=\"in-tab-txt-b textoverflow\" style=\"text-align: left !important;\">" +
                            (n == obj.Model ? "-" : ("<a id=\"a_" + obj.id + "\" style=\"cursor: pointer;\" data-toggle=\"modal\" data-target=\"#modalCheckout\">" + EquipmentInstance.GetFullNumber(obj) + "</a>")) + "</td>" +
                        "<td class=\"in-tab-txt-b\" style=\"text-align: right !important;\">" + EquipmentBLL.GetRuntime(obj.Runtime + obj.InitializedRuntime, obj.CompensatedHours.Value) + "</td>" +
                        "<td class=\"in-tab-txt-b\">" + EquipmentInstance.GetEngStatus(obj) + "</td>" +
                        "<td class=\"in-tab-txt-b textoverflow\" title=\"" + obj.GpsAddress + "\">" + obj.GpsAddress + "</td>" +
                        "<td class=\"in-tab-txt-rb\" title=\"" + EquipmentInstance.GetStatusTitle(obj) + "\">" + EquipmentInstance.GetStatus(obj) + "</td>" +
                        //"<td class=\"in-tab-txt-b\">" + ((byte?)null == obj.Signal ? "-" : obj.Signal.ToString()) + "</td>" +
                        "<td class=\"in-tab-txt-b\">" + Utility.GetOnlineStyle(obj.OnlineStyle, false) + "</td>" +
                        "<td class=\"in-tab-txt-b textoverflow\">" + ((DateTime?)null == obj.LastActionTime ? "" : obj.LastActionTime.Value.ToString("yyyy/MM/dd HH:mm")) + "</td>" +
                        "<td class=\"in-tab-txt-rb textoverflow\" title=\"" + EquipmentInstance.GetTerinalTitleInfo(obj) + "\">" + (n == obj.Terminal ? "-" : obj.TB_Terminal.Number) + "</td>" +
                        "<td class=\"in-tab-txt-b textoverflow\">" + (null == _in ? "-" : _in.Stocktime.Value.ToString("yyyy/MM/dd")) + "</td>" +
                        "<td class=\"in-tab-txt-b\" title=\"" + StoreInstance.GetStatusTitle(_in) + "\">" + StoreInstance.GetStatus(_in) + "</td>" +
                        //"<td class=\"in-tab-txt-b textoverflow\">" + (null == _out ? "-" : _out.Stocktime.Value.ToString("yyyy/MM/dd")) + "</td>" +
                        //"<td class=\"in-tab-txt-b\" title=\"" + StoreInstance.GetStatusTitle(_out) + "\">" + StoreInstance.GetStatus(_out) + "</td>" +
                        "<td class=\"in-tab-txt-b textoverflow\">" + (n == obj.Warehouse ? "-" : obj.TB_Warehouse.Name) + "</td>" +
                        "</tr>";
                }
            }
            tbodyBody.InnerHtml = html;
            divPagging.InnerHtml = "";
            if (totalRecords > 0)
                ShowPaggings(pageIndex, totalPages, totalRecords, "./equipment_overhaul.aspx", divPagging);
        }

        protected void btQuery_Click(object sender, EventArgs e)
        {
            if (!HasSessionLose)
            { ShowOverhaulEquipments(); }
        }

        protected void btRepairComplete_Click(object sender, EventArgs e)
        {
            if (!HasSessionLose)
            {
                var id = int.Parse(hidRepairId.Value);
                var equipment = EquipmentInstance.Find(f => f.id == id && f.Deleted == false);
                if (null == equipment)
                {
                    ShowNotification("./equipment_overhaul.aspx", "Cannot find the equipment: object is not exist.", false);
                }
                else
                {
                    EquipmentInstance.Update(f => f.id == equipment.id && f.Deleted == false, act =>
                    {
                        act.Status = StatusInstance.Find(f => f.IsItInventory == true).id;
                    });
                    //RepairOK
                    equipment = EquipmentInstance.Find(f => f.id == equipment.id && f.Deleted == false);

                    var history = StoreInstance.GetObject();
                    history.Equipment = equipment.id;
                    // 先保存维修状态
                    history.Status = equipment.Status;
                    // 保存维修完成信息
                    history.Stocktime = DateTime.Now;
                    // 入库次数不变
                    history.StoreTimes = equipment.StoreTimes;
                    history.Warehouse = equipment.Warehouse;
                    StoreInstance.Add(history);

                    // 保存维修完毕操作历史记录
                    SaveHistory(new TB_AccountHistory()
                    {
                        ActionId = ActionInstance.Find(f => f.Name.Equals("RepairOK")).id,
                        ObjectA = EquipmentInstance.ToString(equipment)
                    });

                    ShowNotification("./equipment_overhaul.aspx", "Equipment \""+
                        EquipmentInstance.GetFullNumber(equipment) + "\" inspection & repair complete, and re-store in warehouse.");
                }
            }
        }
    }
}