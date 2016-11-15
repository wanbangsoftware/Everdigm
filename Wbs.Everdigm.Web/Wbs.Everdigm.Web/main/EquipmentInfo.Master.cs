using System;
using Wbs.Everdigm.Common;
using Wbs.Everdigm.BLL;

namespace Wbs.Everdigm.Web.main
{
    /// <summary>
    /// 设备详情页面的Master
    /// </summary>
    public partial class EquipmentInfo : BaseBllMaster
    {
        /// <summary>
        /// 当前打开的Equipment
        /// </summary>
        private static string SessionKey = "_opened_equipment_";
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (!HasSessionLose)
            {
                if (!IsPostBack)
                {
                    if (string.IsNullOrEmpty(_key)) { _key = (string)Session[SessionKey]; }

                    if (string.IsNullOrEmpty(_key))
                    {
                        ShowNotification("./equipment_inquiry.aspx", "Can not find object with null paramenter.", false);
                    }
                    else
                    {
                        hidKey.Value = Utility.UrlEncode(_key);
                        ShowNavigator();
                        ShowEquipmentInfo();
                    }
                }
            }
        }
        private static string MENU_ITEM = "<li role=\"presentation\"  class=\"#class#\"><a href =\"#url#\" class=\"dropdown-toggle\" aria-haspopup=\"true\" aria-expanded=\"false\">#menu##caret#</a></li>";
        private const string URL_CMD = "equipment_command.aspx";
        private const string URL_SEC = "equipment_security.aspx";
        private const string URL_ARM = "equipment_alarm.aspx";
        private const string URL_POS = "equipment_position.aspx";
        private const string URL_WORK = "equipment_work.aspx";
        private const string URL_AS = "equipment_as.aspx";
        private const string URL_STOC = "equipment_storage.aspx";
        private const string URL_SETT = "equipment_setting.aspx";
        /// <summary>
        /// 显示导航栏
        /// </summary>
        private void ShowNavigator()
        {
            string url = Request.Url.AbsolutePath;
            int index = url.LastIndexOf('/');
            url = url.Substring(index + 1);

            // command
            bool b = url.Contains(URL_CMD);
            string html = MENU_ITEM.Replace("#class#", b ? "active" : "").Replace("#url#", URL_CMD).Replace("#menu#", "Command").Replace("#caret#", b ? "<span class=\"caret\"></span>" : "");
            // security
            b = url.Contains(URL_SEC);
            html += MENU_ITEM.Replace("#class#", b ? "active" : "").Replace("#url#", URL_SEC).Replace("#menu#", "Security").Replace("#caret#", b ? "<span class=\"caret\"></span>" : "");
            // alarm
            b = url.Contains(URL_ARM);
            html += MENU_ITEM.Replace("#class#", b ? "active" : "").Replace("#url#", URL_ARM).Replace("#menu#", "Alarm").Replace("#caret#", b ? "<span class=\"caret\"></span>" : "");
            // position
            b = url.Contains(URL_POS);
            html += MENU_ITEM.Replace("#class#", b ? "active" : "").Replace("#url#", URL_POS).Replace("#menu#", "Position").Replace("#caret#", b ? "<span class=\"caret\"></span>" : "");
            // work
            b = url.Contains(URL_WORK);
            html += MENU_ITEM.Replace("#class#", b ? "active" : "").Replace("#url#", URL_WORK).Replace("#menu#", "Work").Replace("#caret#", b ? "<span class=\"caret\"></span>" : "");
            // as
            b = url.Contains(URL_AS);
            html += MENU_ITEM.Replace("#class#", b ? "active" : "").Replace("#url#", URL_AS).Replace("#menu#", "A/S History").Replace("#caret#", b ? "<span class=\"caret\"></span>" : "");
            // storage
            b = url.Contains(URL_STOC);
            html += MENU_ITEM.Replace("#class#", b ? "active" : "").Replace("#url#", URL_STOC).Replace("#menu#", "Storage History").Replace("#caret#", b ? "<span class=\"caret\"></span>" : "");
            // setting
            b = url.Contains(URL_SETT);
            html += MENU_ITEM.Replace("#class#", b ? "active" : "").Replace("#url#", URL_SETT).Replace("#menu#", "Setting").Replace("#caret#", b ? "<span class=\"caret\"></span>" : "");

            html += MENU_ITEM.Replace("#class#", "").Replace("#url#", "./equipment_inquiry.aspx").Replace("#menu#", "&times;").Replace("#caret#", "");

            functionBar.InnerHtml = html;
        }
        /// <summary>
        /// 显示设备的基本信息
        /// </summary>
        private void ShowEquipmentInfo()
        {
            var n = (int?)null;
            if (!string.IsNullOrEmpty(_key))
            {
                var id = ParseInt(Utility.Decrypt(_key));
                var EquipmentInstance = new EquipmentBLL();
                var obj = EquipmentInstance.Find(f => f.id == id && f.Deleted == false);
                if (null != obj)
                {
                    functional.InnerText = ((EquipmentFunctional)obj.Functional != EquipmentFunctional.Loader ? "Excavator: " : "") +
                        Utility.GetEquipmentFunctional(obj.Functional.Value);

                    equipment_id.InnerHtml = obj.TB_EquipmentModel.Code + obj.Number;
                    objectInfo.Rows[1].Cells[1].InnerHtml = obj.TB_EquipmentModel.TB_EquipmentType.Name;
                    objectInfo.Rows[1].Cells[3].InnerHtml = obj.TB_EquipmentModel.Code;

                    objectInfo.Rows[2].Cells[1].InnerHtml = obj.Number;
                    objectInfo.Rows[2].Cells[3].InnerHtml = obj.TB_EquipmentStatusName.Code;
                    objectInfo.Rows[2].Cells[3].Attributes["title"] = obj.TB_EquipmentStatusName.Name;

                    objectInfo.Rows[3].Cells[1].InnerHtml = EquipmentBLL.GetRuntime(obj.Runtime + obj.InitializedRuntime, obj.CompensatedHours.Value);
                    objectInfo.Rows[3].Cells[3].InnerHtml = EquipmentInstance.GetEngStatus(obj);

                    objectInfo.Rows[4].Cells[1].InnerText = obj.GpsAddress;

                    var StoreInstance = new StoreHistoryBLL();
                    // 入库记录
                    var _in = StoreInstance.GetStoreInfo(obj.id, obj.StoreTimes.Value, true);
                    // 出库记录
                    var _out = StoreInstance.GetStoreInfo(obj.id, obj.StoreTimes.Value, false);
                    objectInfo.Rows[6].Cells[1].InnerText = (null == _in ? "-" : _in.Stocktime.Value.ToString("yyyy/MM/dd"));
                    objectInfo.Rows[6].Cells[3].InnerText = StoreInstance.GetStatus(_in);
                    objectInfo.Rows[6].Cells[3].Attributes["title"] = StoreInstance.GetStatusTitle(_in);

                    objectInfo.Rows[7].Cells[1].InnerText = (null == _out ? "-" : _out.Stocktime.Value.ToString("yyyy/MM/dd"));
                    objectInfo.Rows[7].Cells[3].InnerText = StoreInstance.GetStatus(_out);
                    objectInfo.Rows[7].Cells[3].Attributes["title"] = StoreInstance.GetStatusTitle(_out);

                    objectInfo.Rows[8].Cells[1].InnerText = (int?)null == obj.Warehouse ? "-" : obj.TB_Warehouse.Name;

                    objectInfo.Rows[10].Cells[1].InnerText = (n == obj.Terminal ? "-" : obj.TB_Terminal.Number);
                    objectInfo.Rows[10].Cells[3].InnerHtml = Utility.GetOnlineStyle(obj.OnlineStyle, obj.OnlineTime, false);

                    objectInfo.Rows[11].Cells[1].InnerText = (n == obj.Terminal ? "-" : obj.TB_Terminal.Sim);
                    objectInfo.Rows[11].Cells[3].InnerText =
                        (n == obj.Terminal ? "-" :
                        ((int?)null == obj.TB_Terminal.Satellite ? "" : obj.TB_Terminal.TB_Satellite.CardNo));

                    //objectInfo.Rows[12].Cells[1].InnerText =
                    //    (byte?)null == obj.Signal ? "-" : obj.Signal.Value.ToString();
                    objectInfo.Rows[12].Cells[1].InnerText =
                        (DateTime?)null == obj.LastActionTime ? "-" : obj.LastActionTime.Value.ToString("yyyy/MM/dd HH:mm");
                }
            }
        }
    }
}