using System;

using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.Web.main
{
    public partial class customer_new : BaseCustomerPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (!HasSessionLose)
            {
                if (!string.IsNullOrEmpty(_key))
                {
                    hidID.Value = _key;
                    if (!IsPostBack)
                    {
                        ShowEdit();
                    }
                }
            }
        }

        private void ShowEdit()
        {
            var obj = CustomerInstance.Find(f => f.id == ParseInt(Utility.Decrypt(_key)));
            if (null == obj)
            {
                ShowNotification("./customers.aspx", "Error: paramenter error, cannot edit the customer.", false);
            }
            else
            {
                txtAddress.Value = obj.Address;
                txtCode.Value = obj.Code;
                txtIdCard.Value = obj.IdCard;
                txtName.Value = obj.Name;
                txtPhone.Value = obj.Phone;
            }
        }

        private void BuilldCustomer(TB_Customer obj)
        {
            obj.Address = txtAddress.Value.Trim();
            obj.Code = txtCode.Value.Trim();
            obj.IdCard = txtIdCard.Value.Trim();
            obj.Name = txtName.Value.Trim();
            obj.Phone = txtPhone.Value.Trim();
        }

        private void NewCustomer()
        {
            var obj = CustomerInstance.Find(f => f.Code.Equals(txtCode.Value.Trim()));
            if (null != obj)
            { ShowNotification("./customer_new.aspx", "Error: same customer number exists.", false); }
            else
            {
                obj = CustomerInstance.GetObject();
                BuilldCustomer(obj);
                CustomerInstance.Add(obj);

                // 保存历史记录
                SaveHistory(new TB_AccountHistory()
                {
                    ActionId = ActionInstance.Find(f => f.Name.Equals("AddCustomer")).id,
                    ObjectA = CustomerInstance.ToString(obj)
                });

                ShowNotification("./customer_new.aspx", "You added a new customer into system.");
            }
        }

        private void EditCustomer()
        {
            var id = ParseInt(Utility.Decrypt(_key));
            var obj = CustomerInstance.Find(f => f.id == id);
            if (null == obj)
            {
                ShowNotification("./customers.aspx", "Error: could not find the customer.", false);
            }
            else
            {
                BuilldCustomer(obj);
                var chk = CustomerInstance.Find(f => f.Code.Equals(obj.Code) && f.id != id);
                if (null != chk)
                {
                    ShowNotification("./customer_new.aspx", "Error: there have a same customer code exists.", false);
                }
                else
                {
                    Update(obj);

                    // 保存历史记录
                    SaveHistory(new TB_AccountHistory()
                    {
                        ActionId = ActionInstance.Find(f => f.Name.Equals("EditCustomer")).id,
                        ObjectA = CustomerInstance.ToString(obj)
                    });

                    ShowNotification("./customers.aspx", "You have changed the customer: " + CustomerInstance.ToString(obj) + ".");
                }
            }
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            if (!HasSessionLose)
            {
                if ("" == hidID.Value)
                {
                    NewCustomer();
                }
                else
                {
                    EditCustomer();
                }
            }
        }
    }
}