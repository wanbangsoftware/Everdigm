﻿using System;
using System.Linq.Expressions;
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
        /// 按照查询条件显示设备列表
        /// </summary>
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
            var house = ParseInt(hidQueryWarehouse.Value);
            // 表达式
            Expression<Func<TB_Equipment, bool>> expression = PredicateExtensions.True<TB_Equipment>();
            expression = expression.And(a => a.TB_EquipmentStatusName.IsItInventory == true && a.Deleted == false && a.StoreTimes == 1 && a.Terminal != (int?)null);
            if (type > 0)
            {
                expression = expression.And(a => a.TB_EquipmentModel.Type == type);
            }
            if (model > 0)
            {
                expression = expression.And(a => a.Model == model);
            }
            if (house > 0)
            {
                expression = expression.And(a => a.Warehouse == house);
            }
            if (!string.IsNullOrEmpty(query))
            {
                pageIndex = 1;
                expression = expression.And(a => a.Number.Contains(query));
            }
            var list = EquipmentInstance.FindPageList<TB_Equipment>(pageIndex, PageSize, out totalRecords, expression, null);
            var totalPages = totalRecords / PageSize + (totalRecords % PageSize > 0 ? 1 : 0);
            hidTotalPages.Value = totalPages.ToString();

            string html = "";
            if (totalRecords < 1)
            {
                html = "<tr><td colspan=\"18\">No records, You can change the condition and try again.</td></tr>";
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
                        "<td class=\"in-tab-txt-rb textoverflow\">" + obj.TB_Warehouse.Name + "</td>" +
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
                ShowPaggings(pageIndex, totalPages, totalRecords, "./equipment_in_storage.aspx", divPagging);
        }
    }
}