using System;
using Wbs.Everdigm.Database;
using Wbs.Everdigm.BLL;
using Wbs.Everdigm.Common;

namespace Wbs.Everdigm.Web.main
{
    public partial class equipment_change_warehouse : BaseEquipmentPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            _cookie_name_ = "_equipment_change_warehouse_list_page_";
            cookieName.Value = _cookie_name_;
            base.Page_Load(sender, e);
            if (!HasSessionLose)
            {
                if (!IsPostBack)
                {
                    if (!IsPostBack)
                    {
                        hidPageIndex.Value = null == Request.Cookies[_cookie_name_] ? "1" : Request.Cookies[_cookie_name_].Value;
                        ShowEquipments();
                    }
                }
            }
        }

        protected void btQuery_Click(object sender, EventArgs e)
        {
            if (!HasSessionLose) { ShowEquipments(); }
        }

        private void ShowEquipments()
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
                f => (model <= 0 ? f.Model >= 0 : f.Model == model) && f.Deleted == false &&
                    (house <= 0 ? f.Warehouse >= 0 : f.Warehouse == house) && f.Number.Contains(query) &&
                    f.TB_EquipmentStatusName.IsItInventory == true && f.StoreTimes > 0, null);
            var totalPages = totalRecords / PageSize + (totalRecords % PageSize > 0 ? 1 : 0);
            hidTotalPages.Value = totalPages.ToString();

            string html = "";
            if (totalRecords < 1)
            {
                html = "<tr><td colspan=\"16\">No records, You can change the condition and try again.</td></tr>";
            }
            else
            {
                var cnt = (pageIndex - 1) * PageSize;
                var n = (int?)null;
                foreach (var obj in list)
                {
                    cnt++;
                    // 入库记录
                    //var _in = StoreInstance.GetStoreInfo(obj.id, obj.StoreTimes.Value, true);
                    // 出库记录
                    //var _out = StoreInstance.GetStoreInfo(obj.id, obj.StoreTimes.Value, false);
                    var id = Utility.UrlEncode(Utility.Encrypt(obj.id.ToString()));
                    html += "<tr>" +
                        "<td class=\"in-tab-txt-b\">" + cnt + "</td>" +
                        "<td class=\"in-tab-txt-b\">" + (n == obj.Model ? "-" : obj.TB_EquipmentModel.TB_EquipmentType.Code) + "</td>" +
                        "<td class=\"in-tab-txt-b textoverflow\" style=\"text-align: left !important;\">" + (n == obj.Model ? "-" : ("<a href=\"./equipment_position.aspx?key=" + id + "\">" + EquipmentInstance.GetFullNumber(obj) + "</a>")) + "</td>" +
                        "<td class=\"in-tab-txt-b\" style=\"text-align: left !important;\">" + Utility.GetEquipmentFunctional(obj.Functional.Value) + "</td>" +
                        "<td class=\"in-tab-txt-b\" style=\"text-align: right !important;\">" + EquipmentBLL.GetRuntime(obj.Runtime + obj.InitializedRuntime, obj.CompensatedHours.Value) + "</td>" +
                        "<td class=\"in-tab-txt-b\">" + EquipmentInstance.GetEngStatus(obj) + "</td>" +
                        "<td class=\"in-tab-txt-b textoverflow\" title=\"" + obj.GpsAddress + "\">" + obj.GpsAddress + "</td>" +
                        "<td class=\"in-tab-txt-rb\" title=\"" + EquipmentInstance.GetStatusTitle(obj) + "\">" + EquipmentInstance.GetStatus(obj) + "</td>" +
                        "<td class=\"in-tab-txt-b\">" + EquipmentInstance.GetOutdoorDays(obj) + "</td>" +
                        "<td class=\"in-tab-txt-rb textoverflow\">" + EquipmentInstance.GetAverageWorktime(obj) + "</td>" +
                        //"<td class=\"in-tab-txt-b textoverflow\">" + (null == _in ? "-" : _in.Stocktime.Value.ToString("yyyy/MM/dd")) + "</td>" +
                        //"<td class=\"in-tab-txt-b\" title=\"" + StoreInstance.GetStatusTitle(_in) + "\">" + StoreInstance.GetStatus(_in) + "</td>" +
                        //"<td class=\"in-tab-txt-b textoverflow\">" + (null == _out ? "-" : _out.Stocktime.Value.ToString("yyyy/MM/dd")) + "</td>" +
                        //"<td class=\"in-tab-txt-b\" title=\"" + StoreInstance.GetStatusTitle(_out) + "\">" + StoreInstance.GetStatus(_out) + "</td>" +
                        "<td class=\"in-tab-txt-rb textoverflow\">" + ("<a href=\"#h\" id=\"a_" + id + "\">" + obj.TB_Warehouse.Name + "</a>") + "</td>" +
                        //"<td class=\"in-tab-txt-b\">" + ((byte?)null == obj.Signal ? "-" : obj.Signal.ToString()) + "</td>" +
                        "<td class=\"in-tab-txt-b\">" + Utility.GetOnlineStyle(obj.OnlineStyle, obj.OnlineTime, false) + "</td>" +
                        "<td class=\"in-tab-txt-b textoverflow\">" + ((DateTime?)null == obj.LastActionTime ? "" : obj.LastActionTime.Value.ToString("yyyy/MM/dd HH:mm")) + "</td>" +
                        "<td class=\"in-tab-txt-b textoverflow\" title=\"" + EquipmentInstance.GetTerinalTitleInfo(obj) + "\">" + (n == obj.Terminal ? "-" : obj.TB_Terminal.Number) + "</td>" +
                        "</tr>";
                }
            }
            tbodyBody.InnerHtml = html;
            divPagging.InnerHtml = "";
            if (totalRecords > 0)
                ShowPaggings(pageIndex, totalPages, totalRecords, "./equipment_change_warehouse.aspx", divPagging);
        }

        protected void btSaveChangeWarehouse_Click(object sender, EventArgs e)
        {
            if (!HasSessionLose)
            {
                var id = ParseInt(Utility.Decrypt(hidWarehouseEquipmentId.Value));
                var obj = EquipmentInstance.Find(f => f.id == id);
                var tmp = JsonConverter.ToObject<TB_Equipment>(hidWarehouseTo.Value);
                if (obj.TB_EquipmentStatusName.IsItInventory == false)
                {
                    ShowNotification("./equipment_change_warehouse.aspx", "The equipment is not in storage status.", false);
                }
                else if (obj.Warehouse == tmp.Warehouse)
                {
                    ShowNotification("./equipment_change_warehouse.aspx", "The equipment in same warehouse you selected.", false);
                }
                else
                {
                    //var transfer = CodeInstance.Find(f =>
                    //        f.TB_EquipmentStatusName.IsInventory == true && f.Code.Equals("T"));
                    EquipmentInstance.Update(f => f.id == obj.id && f.Deleted == false, act =>
                    {
                        act.Warehouse = tmp.Warehouse;
                        // 状态变为库存转移状态
                        //act.Status = transfer.id;
                    });

                    // 保存转库信息
                    var history = StoreInstance.GetObject();
                    history.Equipment = obj.id;
                    history.Status = obj.Status;//transfer.id;// 移库状态
                    history.Stocktime = DateTime.Now;
                    // 入库次数
                    history.StoreTimes = obj.StoreTimes;
                    history.Warehouse = tmp.Warehouse;// 保持目的仓库
                    StoreInstance.Add(history);

                    SaveHistory(new TB_AccountHistory()
                    {
                        ActionId = ActionInstance.Find(f => f.Name.Equals("Transfer")).id,
                        ObjectA = EquipmentInstance.GetFullNumber(obj) + ", \"" + obj.TB_Warehouse.Name + "\" to \"" +
                            WarehouseInstance.Find(f => f.id == tmp.Warehouse).Name + "\""
                    });

                    ShowEquipments();
                }
            }
        }
    }
}