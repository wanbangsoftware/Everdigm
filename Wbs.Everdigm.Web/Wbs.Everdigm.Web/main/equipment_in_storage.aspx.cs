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
    public partial class equipment_in_storage : BaseEquipmentPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            _cookie_name_ = "_equipment_in_storage_list_page_";
            cookieName.Value = _cookie_name_;
            base.Page_Load(sender, e);
            if (!HasSessionLose)
            {
                if (!IsPostBack)
                {
                    hidPageIndex.Value = null == Request.Cookies[_cookie_name_] ? "1" : Request.Cookies[_cookie_name_].Value;
                    ShowEquipments();
                    ShowEquipmentFunctional();
                }
            }
        }

        private void ShowEquipmentFunctional() {
            string html = "";
            foreach (EquipmentFunctional f in Enum.GetValues(typeof(EquipmentFunctional)))
            {
                html += "<li role=\"presentation\"><a role=\"menuitem\" tabindex=\"" + (byte)f + "\" href=\"#\">" +
                    (f != EquipmentFunctional.Loader ? "Excavator: " : "") + Utility.GetEquipmentFunctional((byte)f) + "</a></li>";
            }
            menuFunctional.InnerHtml = html;
        }

        protected void btSaveNewInStorage_Click(object sender, EventArgs e)
        {
            // 保存新入库的信息
            if (!HasSessionLose)
            {
                var value = hidNewInstorage.Value;
                var equipment = JsonConverter.ToObject<TB_Equipment>(value);
                // 查找是否有相同型号的相同设备号码
                var exist = EquipmentInstance.Find(f => f.Model == equipment.Model && f.Number.Equals(equipment.Number));
                if (null == exist)
                {
                    var newEquipment = EquipmentInstance.GetObject();
                    newEquipment.Model = equipment.Model;
                    newEquipment.Status = StatusInstance.Find(f => f.IsItInventory == true).id;
                    newEquipment.Warehouse = equipment.Warehouse;
                    newEquipment.Number = equipment.Number;
                    newEquipment.StoreTimes = equipment.StoreTimes;
                    newEquipment.Functional = equipment.Functional;
                    newEquipment = EquipmentInstance.Add(newEquipment);

                    // 保存入库信息
                    var history = StoreInstance.GetObject();
                    history.Equipment = newEquipment.id;
                    history.Status = newEquipment.Status;
                    history.Stocktime = DateTime.Parse(inDate.Value);
                    // 默认第1次入库
                    history.StoreTimes = newEquipment.StoreTimes;
                    history.Warehouse = newEquipment.Warehouse;
                    StoreInstance.Add(history);

                    // 保存入库操作历史记录
                    SaveHistory(new TB_AccountHistory()
                    {
                        ActionId = ActionInstance.Find(f => f.Name.Equals("InStore")).id,
                        ObjectA = EquipmentInstance.ToString(newEquipment)
                    });

                    // 重新显示新的设备列表
                    ShowEquipments();
                }
                else
                {
                    ShowNotification("./equipment_in_storage.aspx", "There has a same number of \"" + equipment.Number + "\" exist.", false);
                }
            }
        }
        /// <summary>
        /// 查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btQuery_Click(object sender, EventArgs e)
        {
            if (!HasSessionLose)
            { ShowEquipments(); }
        }
        /// <summary>
        /// 查询设备状态码
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        //protected List<int> GetQueryStatus(string query)
        //{
        //    var list = new List<int>();
        //    IQueryable<TB_EquipmentStatusCode> status;
        //    // 查询库存
        //    if (query.Equals("I"))
        //    {
        //        // 查询库存状态码列表
        //        status = CodeInstance.FindList(f => f.TB_EquipmentStatusName.IsInventory == true);
        //    }
        //    else if (query.Equals("N")) {
        //        // 新品
        //        status = CodeInstance.FindList(f => f.Code.Equals("N"));
        //    }
        //    else
        //    {
        //        // 2手或租赁
        //        status = CodeInstance.FindList(f => f.Code.IndexOf("L") >= 0 || f.Code.IndexOf("S") >= 0);
        //    }

        //    foreach (var s in status)
        //    {
        //        list.Add(s.id);
        //    }
        //    return list;
        //}
        /// <summary>
        /// 按照查询条件显示设备列表
        /// </summary>
        private void ShowEquipments()
        {
            var totalRecords = 0;
            var pageIndex = "" == hidPageIndex.Value ? 1 : int.Parse(hidPageIndex.Value);
            pageIndex = (0 >= pageIndex ? 1 : pageIndex);
            var model = ParseInt(selectedModels.Value);
            var house = ParseInt(hidQueryWarehouse.Value);
            //var type = GetQueryStatus(hidQueryType.Value);
            var type = hidQueryType.Value;
            var list = EquipmentInstance.FindPageList<TB_Equipment>(pageIndex, PageSize, out totalRecords,
                f => (model <= 0 ? f.Model >= 0 : f.Model == model) &&
                    (house <= 0 ? f.Warehouse >= 0 : f.Warehouse == house) && //type.Contains(f.Status.Value) &&
                    f.TB_EquipmentStatusName.IsItInventory == true &&
                    // 新品查询入库次数为1的产品，移库时查询所有库存
                    ("N" == type ? f.StoreTimes == 1 : ("I" == type ? (f.StoreTimes > 0) : f.StoreTimes > 1)) &&
                    (f.Number.IndexOf(txtQueryNumber.Value.Trim()) >= 0), null);
            var totalPages = totalRecords / PageSize + (totalRecords % PageSize > 0 ? 1 : 0);
            hidTotalPages.Value = totalPages.ToString();
            pageIndex = 0 == pageIndex ? totalPages : pageIndex;
            if (totalRecords > 0 && pageIndex > totalPages)
            {
                pageIndex = totalPages;
                list = EquipmentInstance.FindPageList<TB_Equipment>(pageIndex, PageSize, out totalRecords,
                    f => (model <= 0 ? f.Model >= 0 : f.Model == model) &&
                        (house <= 0 ? f.Warehouse >= 0 : f.Warehouse == house) && //type.Contains(f.Status.Value) &&
                        f.TB_EquipmentStatusName.IsItInventory == true &&
                        // 新品查询入库次数为1的产品，移库时查询所有库存
                        ("N" == type ? f.StoreTimes == 1 : ("I" == type ? (f.StoreTimes > 0) : f.StoreTimes > 1)) &&
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
                // 是否为更改库存查询条件
                var _house = hidQueryType.Value.Equals("I");

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
                        "<td class=\"in-tab-txt-b textoverflow\" style=\"text-align: left !important;\">" + (n == obj.Model ? "-" : ("<a href=\"./equipment_position.aspx?key=" + id + "\">" + EquipmentInstance.GetFullNumber(obj) + "</a>")) + "</td>" +
                        "<td class=\"in-tab-txt-b\" style=\"text-align: left !important;\">" + Utility.GetEquipmentFunctional(obj.Functional.Value) + "</td>" +
                        "<td class=\"in-tab-txt-b\" style=\"text-align: right !important;\">" + EquipmentInstance.GetRuntime(obj.Runtime) + "</td>" +
                        "<td class=\"in-tab-txt-b\">" + EquipmentInstance.GetEngStatus(obj) + "</td>" +
                        "<td class=\"in-tab-txt-b textoverflow\" title=\"" + obj.GpsAddress + "\">" + obj.GpsAddress + "</td>" +
                        "<td class=\"in-tab-txt-rb\">" + EquipmentInstance.GetStatus(obj) + "</td>" +
                        "<td class=\"in-tab-txt-b textoverflow\">" + (null == _in ? "-" : _in.Stocktime.Value.ToString("yyyy/MM/dd")) + "</td>" +
                        "<td class=\"in-tab-txt-b\" title=\"" + StoreInstance.GetStatusTitle(_in) + "\">" + StoreInstance.GetStatus(_in) + "</td>" +
                        "<td class=\"in-tab-txt-b textoverflow\">" + (null == _out ? "-" : _out.Stocktime.Value.ToString("yyyy/MM/dd")) + "</td>" +
                        "<td class=\"in-tab-txt-b\" title=\"" + StoreInstance.GetStatusTitle(_out) + "\">" + StoreInstance.GetStatus(_out) + "</td>" +
                        "<td class=\"in-tab-txt-rb textoverflow\">" + (_house ? ("<a href=\"#h\" id=\"a_" + id + "\">" + obj.TB_Warehouse.Name + "</a>") : obj.TB_Warehouse.Name) + "</td>" +
                        "<td class=\"in-tab-txt-b\">" + ((byte?)null == obj.Signal ? "-" : obj.Signal.ToString()) + "</td>" +
                        "<td class=\"in-tab-txt-b\">" + Utility.GetOnlineStyle(obj.OnlineStyle) + "</td>" +
                        "<td class=\"in-tab-txt-b textoverflow\">" + ((DateTime?)null == obj.LastActionTime ? "" : obj.LastActionTime.Value.ToString("yyyy/MM/dd HH:mm")) + "</td>" +
                        "<td class=\"in-tab-txt-b textoverflow\" title=\"" + EquipmentInstance.GetTerinalTitleInfo(obj) + "\">" + (n == obj.Terminal ? "-" : obj.TB_Terminal.Number) + "</td>" +
                        //"<td class=\"in-tab-txt-b\">" + (n == obj.Terminal ? "-" : (n == obj.TB_Terminal.Satellite ? "-" : obj.TB_Terminal.TB_Satellite.CardNo)) + "</td>" +
                        //"<td class=\"in-tab-txt-b\">" + (n == obj.Terminal ? "-" : obj.TB_Terminal.Sim) + "</td>" +
                        "</tr>";
                }
            }
            tbodyBody.InnerHtml = html;
            divPagging.InnerHtml = "";
            if (totalRecords > 0)
                ShowPaggings(pageIndex, totalPages, totalRecords, "./equipment_in_storage.aspx", divPagging);
        }

        /// <summary>
        /// 保存2手设备回收状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btSave2Hand_Click(object sender, EventArgs e)
        {
            if (!HasSessionLose) {
                var obj = JsonConverter.ToObject<TB_Equipment>(hidOldInstorage.Value);
                var exist = EquipmentInstance.Find(f => f.id == obj.id);
                if (null != exist)
                {
                    // 保存之前的状态
                    var history = StoreInstance.GetObject();
                    history.Equipment = exist.id;
                    history.Status = exist.Status;

                    EquipmentInstance.Update(f => f.id == exist.id, act => {
                        // 保存仓库信息
                        act.Warehouse = obj.Warehouse;
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
                    exist = EquipmentInstance.Find(f => f.id == exist.id);
                    // 保存入库信息
                    history.Stocktime = DateTime.Now;
                    // 入库次数加1
                    history.StoreTimes = exist.StoreTimes + 1;
                    history.Warehouse = exist.Warehouse;
                    StoreInstance.Add(history);

                    // 保存入库操作历史记录
                    SaveHistory(new TB_AccountHistory()
                    {
                        ActionId = ActionInstance.Find(f => f.Name.Equals("InStoreOld")).id,
                        ObjectA = EquipmentInstance.ToString(exist)
                    });

                    ShowEquipments();
                }
                else {
                    ShowNotification("./equipment_in_storage.aspx", "Cannot change storage status: object is not exist.", false);
                }
            }
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
                    ShowNotification("./equipment_in_storage.aspx", "The equipment is not in storage status.", false);
                }
                else if (obj.Warehouse == tmp.Warehouse)
                {
                    ShowNotification("./equipment_in_storage.aspx", "The equipment in same warehouse you selected.", false);
                }
                else
                {
                    //var transfer = CodeInstance.Find(f =>
                    //        f.TB_EquipmentStatusName.IsInventory == true && f.Code.Equals("T"));
                    EquipmentInstance.Update(f => f.id == obj.id, act =>
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

        protected void btConfirmWarehouse_Click(object sender, EventArgs e)
        {
            if (!HasSessionLose)
            {
                var id = ParseInt(Utility.Decrypt(hidConfirmWarehouse.Value));
                var obj = EquipmentInstance.Find(f => f.id == id);
                var _in = StoreInstance.GetStoreInfo(obj.id, obj.StoreTimes.Value, true);
                // 新品入库
                //var n = CodeInstance.Find(f => f.TB_EquipmentStatusName.IsInventory == true && f.Code.Equals("N"));
                //EquipmentInstance.Update(f => f.id == id, act =>
                //{
                //    act.Status = (null == _in || _in.TB_EquipmentStatusCode.Code.Equals("T")) ? n.id : _in.TB_EquipmentStatusCode.id;
                //});

                // 保存操作历史
                SaveHistory(new TB_AccountHistory()
                {
                    ActionId = ActionInstance.Find(f => f.Name.Equals("TransferOK")).id,
                    ObjectA = EquipmentInstance.GetFullNumber(obj) + " - " + obj.TB_Warehouse.Name
                });

                ShowEquipments();
            }
        }
    }
}