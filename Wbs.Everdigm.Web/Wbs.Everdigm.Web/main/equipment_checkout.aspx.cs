using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.Web.main
{
    public partial class equipment_checkout : BaseEquipmentPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            _cookie_name_ = "_equipment_checkout_list_page_";
            cookieName.Value = _cookie_name_;
            base.Page_Load(sender, e);
            if (!HasSessionLose)
            {
                if (!IsPostBack)
                {
                    ShowCheckoutTypes();
                    hidPageIndex.Value = null == Request.Cookies[_cookie_name_] ? "1" : Request.Cookies[_cookie_name_].Value;
                    ShowInventoryEquipments();
                }
            }
        }

        private void ShowCheckoutTypes()
        {
            var state = StatusInstance.FindList(f => f.IsItRental == true || f.IsItOutstorage == true);
            ddlOuttype.Items.Add(new ListItem() { Value = "", Text = "Out type:" });
            foreach (var stat in state)
            {
                ddlOuttype.Items.Add(new ListItem() { Value = stat.id.ToString(), Text = stat.Name });
            }
        }

        protected void btQuery_Click(object sender, EventArgs e)
        {
            if (!HasSessionLose)
            { ShowInventoryEquipments(); }
        }
        /// <summary>
        /// 将库存的设备列表显示出来
        /// </summary>
        private void ShowInventoryEquipments()
        {
            var totalRecords = 0;
            var pageIndex = "" == hidPageIndex.Value ? 1 : int.Parse(hidPageIndex.Value);
            pageIndex = (0 >= pageIndex ? 1 : pageIndex);
            var model = ParseInt(selectedModels.Value);
            var house = ParseInt(hidQueryWarehouse.Value);
            var list = EquipmentInstance.FindPageList<TB_Equipment>(pageIndex, PageSize, out totalRecords,
                f => f.TB_EquipmentStatusName.IsItInventory == true && f.Deleted == false && (model <= 0 ? f.Model >= 0 : f.Model == model) &&
                    (house <= 0 ? (f.Warehouse >= 0 || f.Warehouse == (int?)null) : f.Warehouse == house) &&
                    (f.Number.IndexOf(txtQueryNumber.Value.Trim()) >= 0), null);
            var totalPages = totalRecords / PageSize + (totalRecords % PageSize > 0 ? 1 : 0);
            hidTotalPages.Value = totalPages.ToString();
            pageIndex = 0 == pageIndex ? totalPages : pageIndex;
            if (totalRecords > 0 && pageIndex > totalPages)
            {
                pageIndex = totalPages;
                list = EquipmentInstance.FindPageList<TB_Equipment>(pageIndex, PageSize, out totalRecords,
                    f => f.TB_EquipmentStatusName.IsItInventory == true && f.Deleted == false && (model <= 0 ? f.Model >= 0 : f.Model == model) &&
                        (house <= 0 ? (f.Warehouse >= 0 || f.Warehouse == (int?)null) : f.Warehouse == house) &&
                        (f.Number.IndexOf(txtQueryNumber.Value.Trim()) >= 0), null);
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
                    var _in = StoreInstance.GetStoreInfo(obj.id, obj.StoreTimes.Value, true);
                    // 出库记录
                    var _out = StoreInstance.GetStoreInfo(obj.id, obj.StoreTimes.Value, false);

                    var id = Utility.UrlEncode(Utility.Encrypt(obj.id.ToString()));

                    html += "<tr>" +
                        "<td class=\"in-tab-txt-b\">" + cnt + "</td>" +
                        "<td class=\"in-tab-txt-b\">" + (n == obj.Model ? "-" : obj.TB_EquipmentModel.TB_EquipmentType.Code) + "</td>" +
                        "<td class=\"in-tab-txt-b textoverflow\" style=\"text-align: left !important;\">" +
                            (n == obj.Model ? "-" : ("<a id=\"a_" + obj.id + "\" style=\"cursor: pointer;\" data-toggle=\"modal\" data-target=\"#modalCheckout\">" + EquipmentInstance.GetFullNumber(obj) + "</a>")) + "</td>" +
                        "<td class=\"in-tab-txt-b\" style=\"text-align: left !important;\">" + Utility.GetEquipmentFunctional(obj.Functional.Value) + "</td>" +
                        "<td class=\"in-tab-txt-b\" style=\"text-align: right !important;\">" + EquipmentInstance.GetRuntime(obj.Runtime) + "</td>" +
                        "<td class=\"in-tab-txt-b\">" + EquipmentInstance.GetEngStatus(obj) + "</td>" +
                        "<td class=\"in-tab-txt-b textoverflow\" title=\"" + obj.GpsAddress + "\">" + obj.GpsAddress + "</td>" +
                        "<td class=\"in-tab-txt-rb\" title=\"" + EquipmentInstance.GetStatusTitle(obj) + "\">" + EquipmentInstance.GetStatus(obj) + "</td>" +
                        "<td class=\"in-tab-txt-b\">" + ((byte?)null == obj.Signal ? "-" : obj.Signal.ToString()) + "</td>" +
                        "<td class=\"in-tab-txt-b\">" + Utility.GetOnlineStyle(obj.OnlineStyle) + "</td>" +
                        "<td class=\"in-tab-txt-b textoverflow\">" + ((DateTime?)null == obj.LastActionTime ? "" : obj.LastActionTime.Value.ToString("yyyy/MM/dd HH:mm")) + "</td>" +
                        "<td class=\"in-tab-txt-rb textoverflow\" title=\"" + EquipmentInstance.GetTerinalTitleInfo(obj) + "\">" + (n == obj.Terminal ? "-" : obj.TB_Terminal.Number) + "</td>" +
                        "<td class=\"in-tab-txt-b textoverflow\">" + (null == _in ? "-" : _in.Stocktime.Value.ToString("yyyy/MM/dd")) + "</td>" +
                        "<td class=\"in-tab-txt-b\" title=\"" + StoreInstance.GetStatusTitle(_in) + "\">" + StoreInstance.GetStatus(_in) + "</td>" +
                        "<td class=\"in-tab-txt-b textoverflow\">" + (null == _out ? "-" : _out.Stocktime.Value.ToString("yyyy/MM/dd")) + "</td>" +
                        "<td class=\"in-tab-txt-b\" title=\"" + StoreInstance.GetStatusTitle(_out) + "\">" + StoreInstance.GetStatus(_out) + "</td>" +
                        "<td class=\"in-tab-txt-b textoverflow\">" + (n == obj.Warehouse ? "-" : obj.TB_Warehouse.Name) + "</td>" +
                        "</tr>";
                }
            }
            tbodyBody.InnerHtml = html;
            divPagging.InnerHtml = "";
            if (totalRecords > 0)
                ShowPaggings(pageIndex, totalPages, totalRecords, "./equipment_checkout.aspx", divPagging);
        }

        protected void btCheckoutStorage_Click(object sender, EventArgs e)
        {
            var id = int.Parse(hidCheckEquipmentId.Value);
            var equipment = EquipmentInstance.Find(f => f.id == id && f.Deleted == false);
            if (null == equipment)
            {
                ShowNotification("./equipment_checkout.aspx", "Cannot find the equipment.", false);
                //return;
            }
            else
            {
                EquipmentInstance.Update(f => f.id == equipment.id, act =>
                {
                    act.Status = int.Parse(ddlOuttype.SelectedValue);
                    act.Customer = int.Parse(hidCheckCustomerId.Value);
                    // 出库后库存信息置为null
                    act.Warehouse = (int?)null;
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
                    ActionId = ActionInstance.Find(f => f.Name.Equals("Deliver")).id,
                    ObjectA = EquipmentInstance.GetFullNumber(equipment) +
                        " check out(" + ddlOuttype.SelectedItem.Text + ") to: " + equipment.TB_Customer.Name
                });

                ShowNotification("./equipment_checkout.aspx", "\"" + EquipmentInstance.GetFullNumber(equipment) + "\" has delivered.");
            }
        }
    }
}