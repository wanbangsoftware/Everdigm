using System;
using Wbs.Everdigm.Common;

namespace Wbs.Everdigm.Web.main
{
    public partial class equipment_work : BaseEquipmentPage
    {
        public string MacId = "";
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (!HasSessionLose)
            {
                initializeSessionKey();
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
                    MacId = EquipmentInstance.GetFullNumber(obj);
                    hiddenLastDate.Value = (DateTime?)null == obj.LastActionTime ? DateTime.Now.ToString("yyyy/MM/dd") :
                        obj.LastActionTime.Value.ToString("yyyy/MM/dd");
                }
                divWorkTime.Visible = (null != obj && obj.Functional == (byte)EquipmentFunctional.Mechanical);
            }
        }
    }
}