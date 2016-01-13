using System;

using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.Web.main
{
    public partial class department_add : BaseDepartmentPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (!HasSessionLose)
            {
                if (!IsPostBack)
                    checkCheckBox();
                // key 不为空时表示编辑role信息
                if (!string.IsNullOrEmpty(_key))
                {
                    hidID.Value = _key;
                    if (!IsPostBack)
                    {
                        showEdit();
                    }
                }
            }
        }
        /// <summary>
        /// 检测默认部门按钮是否可用
        /// </summary>
        private void checkCheckBox()
        {
            var dft = DepartmentInstance.Find(f => f.IsDefault == true && f.Delete == false);
            cbIsDefault.Enabled = null == dft;
        }
        /// <summary>
        /// 显示编辑选项
        /// </summary>
        private void showEdit()
        {
            var dept = DepartmentInstance.Find(f => f.id == ParseInt(Utility.Decrypt(hidID.Value)));
            if (null != dept)
            {
                txtAbbreviation.Value = dept.Code;
                txtAddress.Value = dept.Address;
                var parent = DepartmentInstance.Find(f => f.id == dept.Parent && f.Delete == false);
                txtDepartment.Value = null == parent ? "" : parent.Name;
                hidDepartment.Value = null == parent ? "" : parent.id.ToString();
                txtFax.Value = dept.Fax;
                txtName.Value = dept.Name;
                txtPhone.Value = dept.Phone;
                txtRoom.Value = dept.Room;
                cbIsDefault.Checked = dept.IsDefault.Value;
                if (dept.IsDefault == true)
                {
                    cbIsDefault.Enabled = true;
                }
            }
            else {
                ShowNotification("./department_list.aspx", "Error: paramenter error, cannot edit dept. information.", false);
            }
        }
        private void BuildInfo(TB_Department dept)
        {
            dept.Address = txtAddress.Value.Trim();
            dept.Code = txtAbbreviation.Value.Trim();
            dept.Fax = txtFax.Value.Trim();
            dept.IsDefault = cbIsDefault.Checked;
            dept.Name = txtName.Value.Trim();
            dept.Parent = ParseInt(hidDepartment.Value);
            if (dept.Parent < 0) dept.Parent = 0;
            dept.Phone = txtPhone.Value.Trim();
            dept.Room = txtRoom.Value.Trim();
        }
        private void NewDepartment()
        {
            var dept = DepartmentInstance.GetObject();
            BuildInfo(dept);
            DepartmentInstance.Add(dept);

            // 保存操作历史
            SaveHistory(new TB_AccountHistory
            {
                ActionId = ActionInstance.Find(f => f.Name.Equals("AddDept")).id,
                ObjectA = "[id=" + dept.id + "] " + dept.Name
            });

            ShowNotification("./department_list.aspx", "Success: You added a new department.", true);
        }
        private void EditDept()
        {
            var id = int.Parse(Utility.Decrypt(hidID.Value));
            var dept = DepartmentInstance.Find(f => f.id == id && f.Delete == false);
            BuildInfo(dept);
            Update(dept);

            SaveHistory(new TB_AccountHistory
            {
                ActionId = ActionInstance.Find(f => f.Name.Equals("EditDept")).id,
                ObjectA = "[id=" + dept.id + "] " + dept.Name
            });

            ShowNotification("./department_list.aspx", "Success: You changed the dept. info.", true);
        }
        protected void btSave_Click(object sender, EventArgs e)
        {
            if (!HasSessionLose)
            {
                if ("" == hidID.Value)
                {
                    NewDepartment();
                }
                else
                {
                    EditDept();
                }
            }
        }
    }
}