using System;

namespace Wbs.Everdigm.Web.mobile
{
    public partial class device : BaseMobilePage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (SessionLosed)
            {
                Response.Redirect("default.aspx");
            }
            else
            { ShowEquipmentInformations(); }
        }
        private void ShowEquipmentInformations()
        {
            var obj = EquipmentInstance.Find(f => f.id == int.Parse(_key) && f.Deleted == false);
            if (null == obj)
            {
                ShowNotification("/mobile/devices.aspx", "No such equipment exist: paramenter error.", false);
            }
            else
            {
                equipmentId.InnerText = EquipmentInstance.GetFullNumber(obj);
                spanLat.InnerText = obj.Latitude.ToString();
                spanLon.InnerText = obj.Longitude.ToString();
            }
        }
    }
}