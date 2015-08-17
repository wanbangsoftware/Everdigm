using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.Web.main
{
    public partial class equipment_rental_fleet : BaseEquipmentPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            _cookie_name_ = "_equipment_rental_fleet_list_page_";
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
            var totalRecords = 0;
            var pageIndex = "" == hidPageIndex.Value ? 1 : int.Parse(hidPageIndex.Value);
            pageIndex = (0 >= pageIndex ? 1 : pageIndex);
            var model = ParseInt(selectedModels.Value);
            var house = ParseInt(hidQueryWarehouse.Value);
            var number = txtQueryNumber.Value.Trim();
            var list = EquipmentInstance.FindPageList<TB_Equipment>(pageIndex, PageSize, out totalRecords,
                f => (model <= 0 ? f.Model >= 0 : f.Model == model) &&
                    //(house <= 0 ? f.Warehouse >= 0 : f.Warehouse == house) &&
                    (f.Number.IndexOf(txtQueryNumber.Value.Trim()) >= 0) &&
                    (f.TB_EquipmentStatusName.IsItOutstorage == true || f.TB_EquipmentStatusName.IsItRental == true)
                     && f.Deleted == false, null);
            var totalPages = totalRecords / PageSize + (totalRecords % PageSize > 0 ? 1 : 0);
            hidTotalPages.Value = totalPages.ToString();
            pageIndex = 0 == pageIndex ? totalPages : pageIndex;
            if (totalRecords > 0 && pageIndex > totalPages)
            {
                pageIndex = totalPages;
                list = EquipmentInstance.FindPageList<TB_Equipment>(pageIndex, PageSize, out totalRecords,
                    f => (model <= 0 ? f.Model >= 0 : f.Model == model) &&
                    //(house <= 0 ? f.Warehouse >= 0 : f.Warehouse == house) &&
                    (f.Number.IndexOf(txtQueryNumber.Value.Trim()) >= 0) &&
                    (f.TB_EquipmentStatusName.IsItOutstorage == true || f.TB_EquipmentStatusName.IsItRental == true)
                     && f.Deleted == false, null);
            }

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
                        "<td class=\"in-tab-txt-b textoverflow\" style=\"text-align: left !important;\">" + (n == obj.Model ? "-" : ("<a href=\"#=" + id + "\">" + EquipmentInstance.GetFullNumber(obj) + "</a>")) + "</td>" +
                        "<td class=\"in-tab-txt-b\" style=\"text-align: left !important;\">" + Utility.GetEquipmentFunctional(obj.Functional.Value) + "</td>" +
                        "<td class=\"in-tab-txt-b\" style=\"text-align: right !important;\">" + EquipmentInstance.GetRuntime(obj.Runtime + obj.InitializedRuntime) + "</td>" +
                        "<td class=\"in-tab-txt-b\">" + EquipmentInstance.GetEngStatus(obj) + "</td>" +
                        "<td class=\"in-tab-txt-b textoverflow\" title=\"" + obj.GpsAddress + "\">" + obj.GpsAddress + "</td>" +
                        "<td class=\"in-tab-txt-rb\" title=\"" + EquipmentInstance.GetStatusTitle(obj) + "\">" + EquipmentInstance.GetStatus(obj) + "</td>" +
                        "<td class=\"in-tab-txt-b\">" + EquipmentInstance.GetOutdoorDays(obj) + "</td>" +
                        "<td class=\"in-tab-txt-rb textoverflow\">" + EquipmentInstance.GetAverageWorktime(obj) + "</td>" +
                        //"<td class=\"in-tab-txt-b textoverflow\">" + (null == _in ? "-" : _in.Stocktime.Value.ToString("yyyy/MM/dd")) + "</td>" +
                        //"<td class=\"in-tab-txt-b\" title=\"" + StoreInstance.GetStatusTitle(_in) + "\">" + StoreInstance.GetStatus(_in) + "</td>" +
                        //"<td class=\"in-tab-txt-b textoverflow\">" + (null == _out ? "-" : _out.Stocktime.Value.ToString("yyyy/MM/dd")) + "</td>" +
                        //"<td class=\"in-tab-txt-b\" title=\"" + StoreInstance.GetStatusTitle(_out) + "\">" + StoreInstance.GetStatus(_out) + "</td>" +
                        "<td class=\"in-tab-txt-rb textoverflow\">" + (n == obj.Warehouse ? "-" : obj.TB_Warehouse.Name) + "</td>" +
                        //"<td class=\"in-tab-txt-b\">" + ((byte?)null == obj.Signal ? "-" : obj.Signal.ToString()) + "</td>" +
                        "<td class=\"in-tab-txt-b\">" + Utility.GetOnlineStyle(obj.OnlineStyle) + "</td>" +
                        "<td class=\"in-tab-txt-b textoverflow\">" + ((DateTime?)null == obj.LastActionTime ? "" : obj.LastActionTime.Value.ToString("yyyy/MM/dd HH:mm")) + "</td>" +
                        "<td class=\"in-tab-txt-b textoverflow\" title=\"" + EquipmentInstance.GetTerinalTitleInfo(obj) + "\">" + (n == obj.Terminal ? "-" : obj.TB_Terminal.Number) + "</td>" +
                        "</tr>";
                }
            }
            tbodyBody.InnerHtml = html;
            divPagging.InnerHtml = "";
            if (totalRecords > 0)
                ShowPaggings(pageIndex, totalPages, totalRecords, "./equipment_rental_fleet.aspx", divPagging);
        }

        protected void btSaveOldInStorage_Click(object sender, EventArgs e)
        {
            if (!HasSessionLose)
            {
                var id = int.Parse(Utility.Decrypt(Utility.UrlDecode(hidOldInstorage.Value)));
                var exist = EquipmentInstance.Find(f => f.id == id && f.Deleted == false);
                if (null != exist)
                {
                    // 保存之前的状态
                    var history = StoreInstance.GetObject();
                    history.Equipment = exist.id;
                    history.Status = exist.Status;

                    EquipmentInstance.Update(f => f.id == exist.id && f.Deleted == false, act =>
                    {
                        // 保存仓库信息
                        act.Warehouse = int.Parse(hiddenOldWarehouse.Value);
                        // 清除出厂日期和出厂时运转时间
                        act.OutdoorWorktime = 0;
                        act.OutdoorTime = (DateTime?)null;
                        // 客户信息清除
                        act.Customer = (int?)null;
                        act.StoreTimes = exist.StoreTimes + 1;
                        // 需要维修
                        if (cbRepair.Checked)
                        {
                            act.Status = StatusInstance.Find(f => f.IsItOverhaul == true).id;
                        }
                        else
                        { act.Status = StatusInstance.Find(f => f.IsItInventory == true).id; }
                    });
                    // 重新查询设备信息
                    exist = EquipmentInstance.Find(f => f.id == exist.id && f.Deleted == false);
                    // 保存入库信息
                    history.Stocktime = DateTime.Now;
                    // 入库次数加1
                    history.StoreTimes = exist.StoreTimes;
                    history.Warehouse = exist.Warehouse;
                    StoreInstance.Add(history);

                    // 保存入库操作历史记录
                    SaveHistory(new TB_AccountHistory()
                    {
                        ActionId = ActionInstance.Find(f => f.Name.Equals("InStoreOld")).id,
                        ObjectA = EquipmentInstance.ToString(exist)
                    });

                    ShowNotification("./equipment_rental_fleet.aspx", "Equipment has been store in warehouse.");
                    //ShowEquipments();
                }
                else
                {
                    ShowNotification("./equipment_rental_fleet.aspx", "Cannot change storage status: object is not exist.", false);
                }
            }
        }
    }
}