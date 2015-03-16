using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Wbs.Everdigm.BLL;

namespace Wbs.Everdigm.Web.main
{
    /// <summary>
    /// 设备详情页面的Master
    /// </summary>
    public partial class EquipmentInfo : BaseBllMaster
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (!HasSessionLose)
            {
                if (!IsPostBack)
                {
                    hidKey.Value = Utility.UrlEncode(_key);
                    ShowEquipmentInfo();
                }
            }
        }
        private void ShowEquipmentInfo()
        {
            var n = (int?)null;
            if (!string.IsNullOrEmpty(_key))
            {
                var id = ParseInt(Utility.Decrypt(_key));
                var EquipmentInstance = new EquipmentBLL();
                var obj = EquipmentInstance.Find(f => f.id == id);
                if (null != obj)
                {
                    equipment_id.InnerHtml = obj.TB_EquipmentModel.Code + obj.Number;
                    objectInfo.Rows[1].Cells[1].InnerHtml = obj.TB_EquipmentModel.TB_EquipmentType.Name;
                    objectInfo.Rows[1].Cells[3].InnerHtml = obj.TB_EquipmentModel.Code;

                    objectInfo.Rows[2].Cells[1].InnerHtml = obj.Number;
                    objectInfo.Rows[2].Cells[3].InnerHtml =
                        obj.TB_EquipmentStatusCode.TB_EquipmentStatusName.Code + obj.TB_EquipmentStatusCode.Code;
                    objectInfo.Rows[2].Cells[3].Attributes["title"] =
                        obj.TB_EquipmentStatusCode.Name + "(" + obj.TB_EquipmentStatusCode.TB_EquipmentStatusName.Name + ")";

                    objectInfo.Rows[3].Cells[1].InnerHtml = EquipmentInstance.GetRuntime(obj.Runtime);
                    objectInfo.Rows[3].Cells[3].InnerText = EquipmentInstance.GetEngStatus(obj.Voltage);

                    objectInfo.Rows[4].Cells[1].InnerText = "";

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

                    objectInfo.Rows[8].Cells[1].InnerText = obj.TB_Warehouse.Name;

                    objectInfo.Rows[10].Cells[1].InnerText = (n == obj.Terminal ? "-" : obj.TB_Terminal.Number);
                    objectInfo.Rows[10].Cells[3].InnerHtml =
                        "<div class=\"links " + EquipmentInstance.GetOnlineStyle(obj.OnlineStyle) + "\"></div>";

                    objectInfo.Rows[11].Cells[1].InnerText = (n == obj.Terminal ? "-" : obj.TB_Terminal.Sim);
                    objectInfo.Rows[11].Cells[3].InnerText =
                        (n == obj.Terminal ? "-" :
                        (string.IsNullOrEmpty(obj.TB_Terminal.Satellite) ? "" : obj.TB_Terminal.Satellite));

                    objectInfo.Rows[12].Cells[1].InnerText =
                        (byte?)null == obj.Signal ? "-" : obj.Signal.Value.ToString();
                    objectInfo.Rows[12].Cells[3].InnerText =
                        (DateTime?)null == obj.LastActionTime ? "-" : obj.LastActionTime.Value.ToString("yyyy/MM/dd");
                }
            }
        }
    }
}