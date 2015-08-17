using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wbs.Everdigm.Web.main
{
    public partial class equipment_position : BaseEquipmentPage
    {
        public static string Lat = "0.0";
        public static string Lng = "0.0";
        public static string Dat = "";
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (!HasSessionLose)
            {
                if (!IsPostBack)
                {
                    if (string.IsNullOrEmpty(_key))
                    {
                        ShowNotification("./equipment_inquiry.aspx", "Can not find object with null paramenter.", false);
                    }
                    else
                    {
                        ShowStaticPosition();
                    }
                }
            }
        }

        private void ShowStaticPosition()
        {
            if (!string.IsNullOrEmpty(_key))
            {
                var id = ParseInt(Utility.Decrypt(_key));
                var obj = EquipmentInstance.Find(f => f.id == id && f.Deleted == false);
                if (null != obj)
                {
                    Lat = obj.Latitude.ToString();
                    Lng = obj.Longitude.ToString();
                    Dat = (DateTime?)null == obj.LastActionTime ? "-" : obj.LastActionTime.Value.ToString("yyyy/MM/dd HH:mm:ss");
                }
            }
        }
    }
}