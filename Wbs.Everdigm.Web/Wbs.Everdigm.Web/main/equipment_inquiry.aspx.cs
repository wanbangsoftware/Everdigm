using System;
using System.Linq.Expressions;
using Wbs.Everdigm.Database;
using Wbs.Everdigm.BLL;

namespace Wbs.Everdigm.Web.main
{
    public partial class equipment_inquiry : BaseEquipmentPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            _cookie_name_ = "_equipment_inquiry_list_page_";
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

        protected void btQuery_Click(object sender, EventArgs e)
        {
            if (!HasSessionLose)
            { ShowEquipments(); }
        }

        private void ShowEquipments()
        {
            var query = txtQueryNumber.Value.Trim();
            // 模糊查询时页码置为空
            if (!string.IsNullOrEmpty(query)) { hidPageIndex.Value = ""; }

            var totalRecords = 0;
            var pageIndex = "" == hidPageIndex.Value ? 1 : int.Parse(hidPageIndex.Value);
            pageIndex = (0 >= pageIndex ? 1 : pageIndex);
            var type = ParseInt(selectedTypes.Value);
            var model = ParseInt(selectedModels.Value);
            var house = 0;//ParseInt(hidQueryWarehouse.Value);
            var customer = ParseInt(hiddenCustomer.Value);

            // 表达式
            Expression<Func<TB_Equipment, bool>> expression = PredicateExtensions.True<TB_Equipment>();
            // 设备类型
            if (type > 0) { expression = expression.And(a => a.TB_EquipmentModel.Type == type); }
            // 设备型号
            if (model > 0) { expression = expression.And(a => a.Model == model); }
            // 仓库
            if (house > 0) { expression = expression.And(a => a.Warehouse == house); }
            // 查询号码
            if (!string.IsNullOrEmpty(query)) { expression = expression.And(a => a.Number.Contains(query) || a.TB_Customer.Code.Contains(query)); }
            // 客户号码
            switch (customer)
            {
                case 3:
                    // None
                    expression = expression.And(a => a.Customer == (int?)null);
                    break;
                case 2:
                    // Bound
                    expression = expression.And(a => a.Customer != (int?)null);
                    //if (!string.IsNullOrEmpty(query))
                    //{
                    //    expression = expression.And(a => a.TB_Customer.Code.Contains(query));
                    //}
                    break;
                default:
                    // Default
                    break;
            }
            // 终端绑定状态
            var terminal = ParseInt(hiddenTerminal.Value.Trim());
            switch (terminal)
            {
                case 2:
                    // 绑定终端了
                    expression = expression.And(a => a.Terminal != (int?)null);
                    break;
                case 3:
                    // 未绑定终端
                    expression = expression.And(a => a.Terminal == (int?)null);
                    break;
                default:
                    // 忽略的话就是不查询终端的绑定状态
                    break;
            }
            expression = expression.And(a => a.Deleted == false);

            var list = EquipmentInstance.FindPageList<TB_Equipment>(pageIndex, PageSize, out totalRecords, expression, null);
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
                    bool sat = n != obj.Terminal && n != obj.TB_Terminal.Satellite;

                    // 锁车状态和报警信息
                    string arms = EquipmentInstance.GetLockEffectedStatus(obj);
                    if (string.IsNullOrEmpty(arms))
                    {
                        arms = EquipmentInstance.GetAlarmStatus(obj.Alarm);
                    }

                    html += "<tr>" +
                        "<td class=\"in-tab-txt-b\">" + cnt + "</td>" +
                        "<td class=\"in-tab-txt-b\">" + (n == obj.Model ? "-" : obj.TB_EquipmentModel.TB_EquipmentType.Code) + "</td>" +
                        "<td class=\"in-tab-txt-b textoverflow\" style=\"text-align: left !important;\">" + (n == obj.Model ? "-" : ("<a href=\"./equipment_position.aspx?key=" + id + "\">" + EquipmentInstance.GetFullNumber(obj) + "</a>")) + "</td>" +
                        "<td class=\"in-tab-txt-b\" style=\"text-align: left !important;\">" + Utility.GetEquipmentFunctional(obj.Functional.Value) + "</td>" +
                        "<td class=\"in-tab-txt-b\" style=\"text-align: right !important;\">" + EquipmentBLL.GetRuntime(obj.Runtime + obj.InitializedRuntime, obj.CompensatedHours.Value) + "</td>" +
                        "<td class=\"in-tab-txt-b\" style=\"padding: 2px !important; vertical-align: middle !important;\">" + EquipmentInstance.GetEngStatus(obj) + "</td>" +
                        "<td class=\"in-tab-txt-b textoverflow\" title=\"" + obj.GpsAddress + "\" style=\"text-align: left !important;\">" + obj.GpsAddress + "</td>" +
                        "<td class=\"in-tab-txt-rb\" title=\"" + EquipmentInstance.GetStatusTitle(obj) + "\">" + EquipmentInstance.GetStatus(obj) + "</td>" +
                        //"<td class=\"in-tab-txt-b\">" + EquipmentInstance.GetOutdoorDays(obj) + "</td>" +
                        //"<td class=\"in-tab-txt-rb textoverflow\">" + EquipmentInstance.GetAverageWorktime(obj) + "</td>" +
                        "<td class=\"in-tab-txt-b\">" + (n == obj.Customer ? "-" : obj.TB_Customer.Code) + "</td>" +
                        "<td class=\"in-tab-txt-rb textoverflow\" style=\"text-align: left !important;\" title=\"" + (n == obj.Customer ? "-" : obj.TB_Customer.Name) + "\">" + (n == obj.Customer ? "-" : obj.TB_Customer.Name) + "</td>" +
                        //"<td class=\"in-tab-txt-b\">" + ((byte?)null == obj.Signal ? "-" : obj.Signal.ToString()) + "</td>" +
                        "<td class=\"in-tab-txt-b\">" + EquipmentInstance.GetOnlineStyle(obj) + "</td>" +
                        "<td class=\"in-tab-txt-b\">" + arms + "</td>" +
                        "<td class=\"in-tab-txt-b textoverflow\">" + ((DateTime?)null == obj.LastActionTime ? "" : obj.LastActionTime.Value.ToString("yyyy/MM/dd HH:mm")) + "</td>" +
                        "<td class=\"in-tab-txt-b textoverflow\" title=\"" + EquipmentInstance.GetTerinalTitleInfo(obj) + "\">" + (n == obj.Terminal ? "-" : obj.TB_Terminal.Number) + "</td>" +
                        "<td class=\"in-tab-txt-rb\" title=\"" + EquipmentInstance.GetSatelliteTitleInfo(obj) + "\"><span class=\"glyphicon glyphicon-" + (sat ? "ok" : "remove") + " text-" + (sat ? "success" : "danger") + "\" aria-hidden=\"true\"></span></td>" +
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
                ShowPaggings(pageIndex, totalPages, totalRecords, "./equipment_inquiry.aspx", divPagging);
        }
    }
}