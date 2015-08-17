using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wbs.Everdigm.Web.main
{
    public partial class equipment_work : BaseEquipmentPage
    {
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
                        GetLastReceiveDate();
                    }
                }
            }
        }

        private void GetLastReceiveDate()
        {
            if (!string.IsNullOrEmpty(_key))
            {
                var id = ParseInt(Utility.Decrypt(_key));
                var obj = EquipmentInstance.Find(f => f.id == id && f.Deleted == false);
                if (null != obj)
                {
                    hiddenLastDate.Value = (DateTime?)null == obj.LastActionTime ? DateTime.Now.ToString("yyyy/MM/dd") :
                        obj.LastActionTime.Value.ToString("yyyy/MM/dd");
                }
            }
        }
    }
}