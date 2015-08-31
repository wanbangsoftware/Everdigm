using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.Web.main
{
    public partial class equipment_rental : BaseEquipmentPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            _cookie_name_ = "_equipment_rentail_list_page_";
            cookieName.Value = _cookie_name_;
            base.Page_Load(sender, e);
            if (!HasSessionLose)
            {
                if (!IsPostBack)
                {
                    hidPageIndex.Value = null == Request.Cookies[_cookie_name_] ? "1" : Request.Cookies[_cookie_name_].Value;
                    ShowEquipments();
                }
            }
        }

        private void ShowEquipments() 
        {
            var totalRecords = 0;
            var pageIndex = "" == hidPageIndex.Value ? 1 : int.Parse(hidPageIndex.Value);
            pageIndex = (0 >= pageIndex ? 1 : pageIndex);
            var model = ParseInt(selectedModels.Value);
            var house = ParseInt(hidQueryWarehouse.Value);
            // 只查询库存或租赁出去了的设备列表
            var list = EquipmentInstance.FindPageList<TB_Equipment>(pageIndex, PageSize, out totalRecords,
                f => (f.TB_EquipmentStatusName.IsItInventory == true || f.TB_EquipmentStatusName.IsItRental == true) && 
                    (model <= 0 ? f.Model >= 0 : f.Model == model) && f.Deleted == false &&
                    (house <= 0 ? (f.Warehouse >= 0 || f.Warehouse == (int?)null) : f.Warehouse == house) &&
                    (f.Number.IndexOf(txtQueryNumber.Value.Trim()) >= 0), null);
            var totalPages = totalRecords / PageSize + (totalRecords % PageSize > 0 ? 1 : 0);
            hidTotalPages.Value = totalPages.ToString();
            pageIndex = 0 == pageIndex ? totalPages : pageIndex;
            if (totalRecords > 0 && pageIndex > totalPages)
            {
                pageIndex = totalPages;
                list = EquipmentInstance.FindPageList<TB_Equipment>(pageIndex, PageSize, out totalRecords,
                    f => (f.TB_EquipmentStatusName.IsItInventory == true || f.TB_EquipmentStatusName.IsItRental == true) &&
                     (model <= 0 ? f.Model >= 0 : f.Model == model) && f.Deleted == false &&
                        (house <= 0 ? (f.Warehouse >= 0 || f.Warehouse == (int?)null) : f.Warehouse == house) &&
                        (f.Number.IndexOf(txtQueryNumber.Value.Trim()) >= 0), null);
            }

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
                    //var _in = StoreInstance.GetStoreInfo(obj.id, obj.StoreTimes.Value, true);
                    // 出库记录
                    //var _out = StoreInstance.GetStoreInfo(obj.id, obj.StoreTimes.Value, false);

                    var id = Utility.UrlEncode(Utility.Encrypt(obj.id.ToString()));

                    html += "<tr>" +
                        "<td class=\"in-tab-txt-b\">" + cnt + "</td>" +
                        "<td class=\"in-tab-txt-b\">" + (n == obj.Model ? "-" : obj.TB_EquipmentModel.TB_EquipmentType.Code) + "</td>" +
                        "<td class=\"in-tab-txt-b textoverflow\" style=\"text-align: left !important;\">" + (n == obj.Model ? "-" : ("<a href=\"#" + id + "\">" + EquipmentInstance.GetFullNumber(obj) + "</a>")) + "</td>" +
                        "<td class=\"in-tab-txt-b\" style=\"text-align: left !important;\">" + Utility.GetEquipmentFunctional(obj.Functional.Value) + "</td>" +
                        "<td class=\"in-tab-txt-b\" style=\"text-align: right !important;\">" + EquipmentInstance.GetRuntime(obj.Runtime + obj.InitializedRuntime) + "</td>" +
                        "<td class=\"in-tab-txt-b\">" + EquipmentInstance.GetEngStatus(obj) + "</td>" +
                        "<td class=\"in-tab-txt-b textoverflow\" style=\"text-align: left !important;\" title=\"" + obj.GpsAddress + "\">" + obj.GpsAddress + "</td>" +
                        "<td class=\"in-tab-txt-rb\" title=\"" + EquipmentInstance.GetStatusTitle(obj) + "\">" + EquipmentInstance.GetStatus(obj) + "</td>" +
                        "<td class=\"in-tab-txt-b textoverflow\">" + ((DateTime?)null == obj.OutdoorTime ? "0000/00/00" : obj.OutdoorTime.Value.ToString("yyyy/MM/dd")) + "</td>" +
                        "<td class=\"in-tab-txt-b\" style=\"width: 20px;\">to</td>"+
                        "<td class=\"in-tab-txt-b textoverflow\">" + ((DateTime?)null == obj.ReclaimTime ? "0000/00/00" : obj.ReclaimTime.Value.ToString("yyyy/MM/dd")) + "</td>" +
                        "<td class=\"in-tab-txt-b\">" + EquipmentInstance.GetOutdoorDays(obj) + "</td>" +
                        "<td class=\"in-tab-txt-rb\">" + EquipmentInstance.GetAverageWorktime(obj) + "</td>" +
                        "<td class=\"in-tab-txt-b\">" + (n == obj.Customer ? "-" : obj.TB_Customer.Code) + "</td>" +
                        "<td class=\"in-tab-txt-rb textoverflow\" style=\"text-align: left !important;\" title=\"" + (n == obj.Customer ? "-" : obj.TB_Customer.Name) + "\">" + (n == obj.Customer ? "-" : obj.TB_Customer.Name) + "</td>" +
                        //"<td class=\"in-tab-txt-b\">" + ((byte?)null == obj.Signal ? "-" : obj.Signal.ToString()) + "</td>" +
                        //"<td class=\"in-tab-txt-b\">" + Utility.GetOnlineStyle(obj.OnlineStyle) + "</td>" +
                        //"<td class=\"in-tab-txt-b textoverflow\">" + ((DateTime?)null == obj.LastActionTime ? "" : obj.LastActionTime.Value.ToString("yyyy/MM/dd HH:mm")) + "</td>" +
                        //"<td class=\"in-tab-txt-rb textoverflow\" title=\"" + EquipmentInstance.GetTerinalTitleInfo(obj) + "\">" + (n == obj.Terminal ? "-" : obj.TB_Terminal.Number) + "</td>" +
                        //"<td class=\"in-tab-txt-b textoverflow\">" + (null == _in ? "-" : _in.Stocktime.Value.ToString("yyyy/MM/dd")) + "</td>" +
                        //"<td class=\"in-tab-txt-b\" title=\"" + StoreInstance.GetStatusTitle(_in) + "\">" + StoreInstance.GetStatus(_in) + "</td>" +
                        //"<td class=\"in-tab-txt-b textoverflow\">" + (null == _out ? "-" : _out.Stocktime.Value.ToString("yyyy/MM/dd")) + "</td>" +
                        //"<td class=\"in-tab-txt-b\" title=\"" + StoreInstance.GetStatusTitle(_out) + "\">" + StoreInstance.GetStatus(_out) + "</td>" +
                        "<td class=\"in-tab-txt-b textoverflow\">" + (n == obj.Warehouse ? "-" : obj.TB_Warehouse.Name) + "</td>" +
                        //"<td class=\"in-tab-txt-b\">" + (n == obj.Terminal ? "-" : (n == obj.TB_Terminal.Satellite ? "-" : obj.TB_Terminal.TB_Satellite.CardNo)) + "</td>" +
                        //"<td class=\"in-tab-txt-rb\">" + (n == obj.Terminal ? "-" : obj.TB_Terminal.Sim) + "</td>" +
                        "</tr>";
                }
            }
            tbodyBody.InnerHtml = html;
            divPagging.InnerHtml = "";
            if (totalRecords > 0)
                ShowPaggings(pageIndex, totalPages, totalRecords, "./equipment_rentail.aspx", divPagging);
        }

        protected void btQuery_Click(object sender, EventArgs e)
        {
            if (!HasSessionLose)
            { ShowEquipments(); }
        }

        protected void confirmRental_Click(object sender, EventArgs e)
        {
            // 租赁出库
            if (!HasSessionLose)
            {
                var id = ParseInt(Utility.Decrypt(hiddenRentalId.Value));
                var equipment = EquipmentInstance.Find(f => f.id == id && f.Deleted == false);
                if (null == equipment)
                {
                    ShowNotification("./equipment_rental.aspx", "Cannot find the equipment.", false);
                }
                else
                {
                    EquipmentInstance.Update(f => f.id == equipment.id, act =>
                    {
                        act.Status = StatusInstance.Find(f => f.IsItRental == true).id;
                        act.Customer = int.Parse(hiddenCustomer.Value);
                        act.OutdoorTime = DateTime.Today;
                        act.OutdoorWorktime = equipment.Runtime;
                        act.Warehouse = (int?)null;
                        act.ReclaimTime = DateTime.Parse(deadLine.Value + " 00:00:00");
                    });

                    equipment = EquipmentInstance.Find(f => f.id == equipment.id);
                    // 保存出库历史记录
                    var history = StoreInstance.GetObject();
                    history.Equipment = equipment.id;
                    history.Status = equipment.Status;
                    history.Stocktime = DateTime.Now;
                    // 设备的出入库次数，入库时增1，出库时不变
                    history.StoreTimes = equipment.StoreTimes;
                    history.Warehouse = (int?)null;
                    StoreInstance.Add(history);

                    // 保存操作历史记录
                    SaveHistory(new TB_AccountHistory()
                    {
                        ActionId = ActionInstance.Find(f => f.Name.Equals("Rental")).id,
                        ObjectA = EquipmentInstance.GetFullNumber(equipment) +
                            " rent to: " + equipment.TB_Customer.Name + ", " + 
                            equipment.OutdoorTime.Value.ToString("yyyy/MM/dd") + 
                            " to " + equipment.ReclaimTime.Value.ToString("yyyy/MM/dd")
                    });

                    ShowNotification("./equipment_rental.aspx", "\"" +
                        EquipmentInstance.GetFullNumber(equipment) + "\" has rent to " + equipment.TB_Customer.Name + ".");
                }
            }
        }

        protected void btRentalEdit_Click(object sender, EventArgs e)
        {
            if (!HasSessionLose)
            {
                var id = ParseInt(Utility.Decrypt(hiddenEditId.Value));
                var equipment = EquipmentInstance.Find(f => f.id == id && f.Deleted == false);
                if (null == equipment)
                { ShowNotification("./equipment_rental.aspx", "Cannot find the equipment.", false); }
                else
                {
                    // 延期
                    if (optionExtend.Checked == true)
                    {
                        EquipmentInstance.Update(f => f.id == equipment.id, act =>
                        {
                            act.ReclaimTime = DateTime.Parse(deadLineExtend.Value);
                        });
                        equipment = EquipmentInstance.Find(f => f.id == id && f.Deleted == false);
                        // 保存操作历史记录
                        SaveHistory(new TB_AccountHistory()
                        {
                            ActionId = ActionInstance.Find(f => f.Name.Equals("ExtendRental")).id,
                            ObjectA = EquipmentInstance.ToString(equipment) + ", Extend rental to: " + 
                            equipment.ReclaimTime.Value.ToString("yyyy/MM/dd")
                        });

                        ShowNotification("./equipment_rental.aspx", "You have extended the rental date.");
                    }
                    else if (optionReclaim.Checked == true)
                    { 
                        // 保存之前的状态
                        var history = StoreInstance.GetObject();
                        history.Equipment = equipment.id;
                        history.Status = equipment.Status;

                        // 更新
                        EquipmentInstance.Update(f => f.id == equipment.id, act =>
                        {
                            // 保存仓库信息
                            act.Warehouse = int.Parse(hiddenWarehouse.Value);
                            // 清除出厂日期和出厂时运转时间
                            act.OutdoorWorktime = 0;
                            act.OutdoorTime = (DateTime?)null;
                            // 清除到期时间
                            act.ReclaimTime = (DateTime?)null;
                            // 客户信息清除
                            act.Customer = (int?)null;
                            act.StoreTimes = equipment.StoreTimes + 1;
                            // 需要维修
                            if (cbRepair.Checked)
                            {
                                act.Status = StatusInstance.Find(f => f.IsItOverhaul == true).id;
                            }
                            else
                            { act.Status = StatusInstance.Find(f => f.IsItInventory == true).id; }
                        });
                        // 重新查询
                        equipment = EquipmentInstance.Find(f => f.id == id && f.Deleted == false);
                        // 保存入库信息
                        history.Stocktime = DateTime.Now;
                        // 入库次数加1
                        history.StoreTimes = equipment.StoreTimes;
                        history.Warehouse = equipment.Warehouse;
                        StoreInstance.Add(history);

                        // 保存入库操作历史记录
                        SaveHistory(new TB_AccountHistory()
                        {
                            ActionId = ActionInstance.Find(f => f.Name.Equals("InStoreOld")).id,
                            ObjectA = EquipmentInstance.GetFullNumber(equipment) + " recovered"
                        });

                        ShowNotification("./equipment_rental.aspx", "Equipment has been recovered.");
                    }
                }
            }
        }
    }
}