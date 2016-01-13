using System;
using Wbs.Everdigm.BLL;

namespace Wbs.Everdigm.Web.mobile
{
    public partial class my_devices : BaseMobilePage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (SessionLosed)
            {
                Response.Redirect("default.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    ShowMineInfo();
                    ShowMyDevices();
                }
            }
        }
        private void ShowMineInfo()
        {
            if (null != me)
            {
                account.InnerText = "Welcome " + me.Name;
            }
        }
        /// <summary>
        /// 显示我作为购买者名下的所有设备列表
        /// </summary>
        private void ShowMyDevices()
        {
            var items = EquipmentInstance.FindList(f => f.Customer == me.id && f.Deleted == false);
            var html = "";
            foreach (var item in items)
            {
                var eng = EquipmentInstance.GetEngStatus(item);
                html += "<dl class=\"invest-type\" id=\"" + item.id.ToString() + "\">" +
                    "            <dt>" +
                    "                <span class=\"iconleft\">" +
                    "                    <img class=\"img-rounded ex\" src=\"" + item.TB_EquipmentModel.TB_EquipmentType.Image + "\">" +
                    "                </span>" + EquipmentInstance.GetFullNumber(item) +
                    "                <em class=\"status\">" + item.TB_EquipmentModel.TB_EquipmentType.Name + "</em>" +
                    "            </dt>" +
                    "            <dd>" +
                    "                <span class=\"text-" + (eng.Equals("ON") ? "success" : "danger") + "\"><span class=\"signal cell-engine\"></span> Engine " + eng + "</span>" +
                    "                <em class=\"status\"><span class=\"glyphicon glyphicon-time\"></span> " + EquipmentBLL.GetRuntime(item.Runtime + item.InitializedRuntime) + "</em>" +
                    "            </dd>" +
                    "            <dd>" +
                    "                <span class=\"text-info\"><span class=\"signal cell-signal-" + Utility.ASU2Signal(item.Signal.Value) + "\"></span> Signal: " + Utility.ASU2DBM(item.Signal.Value) + "dBm(ASU: " + item.Signal + ")</span>" +
                    "                <div class=\"total-num\">" + Utility.GetOnlineStyle(item.OnlineStyle, false) + "</div>" +
                    "            </dd>" +
                    "            <dd class=\"desc\"><span class=\"glyphicon glyphicon-globe\"></span> " + item.GpsAddress + "</dd>" +
                    "</dl>";
            }
            equipmentItems.InnerHtml = html;
        }
    }
}