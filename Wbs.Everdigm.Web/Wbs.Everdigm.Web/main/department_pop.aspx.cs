using System;

namespace Wbs.Everdigm.Web.main
{
    public partial class department_pop : BaseDepartmentPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            init();
        }

        private void init()
        {
            if (!string.IsNullOrEmpty(_key))
            {
                var title = "0" == _key ? "Select Parent:" : "Select User:";
                Title = title;
                tableTable.Visible = "0" != _key;
                ShowDepartments();
            }
        }

        private void ShowDepartments()
        {
            tvDepartments.Nodes.Clear();

            if ("0" == _key)
                tvDepartments.Attributes.Add("onclick", "return OnClientTreeNodeChecked(event);");

            ShowDepartmentsInTreeView(tvDepartments, 0, "0" != _key);
        }
    }
}