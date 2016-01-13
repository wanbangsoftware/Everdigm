using System;
using System.Linq;

using Wbs.Everdigm.BLL;
using Wbs.Everdigm.Database;
using System.Web.UI.WebControls;

namespace Wbs.Everdigm.Web
{
    /// <summary>
    /// 部门机构管理业务基类
    /// </summary>
    public class BaseDepartmentPage : BaseBLLPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (HasSessionLose)
            {
                ShowNotification("../default.aspx", "Your session has expired, please login again.", false, true);
            }
        }

        protected DepartmentBLL DepartmentInstance { get { return new DepartmentBLL(); } }
        /// <summary>
        /// 更新部门信息
        /// </summary>
        /// <param name="obj"></param>
        protected void Update(TB_Department obj)
        {
            if (obj.Parent < 0) 
                obj.Parent = 0;
            DepartmentInstance.Update(f => f.id == obj.id, action => {
                action.Address = obj.Address;
                action.Code = obj.Code;
                action.Delete = obj.Delete;
                action.Fax = obj.Fax;
                action.IsDefault = obj.IsDefault;
                action.Name = obj.Name;
                action.Parent = obj.Parent;
                action.Phone = obj.Phone;
                action.Room = obj.Room;
            });
        }
        /// <summary>
        /// 将部门列表显示在树形目录下
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="parent">上级部门</param>
        /// <param name="showUser">标记是否在部门下显示用户列表</param>
        protected void ShowDepartmentsInTreeView(TreeView tree, int parent, bool showUser)
        {
            var list = DepartmentInstance.FindList(f => f.Parent == parent && f.Delete == false).OrderBy(o => o.Name);
            foreach (var dept in list)
            {
                TreeNode node = new TreeNode();
                node.Text = dept.Name;
                node.ToolTip = dept.Phone;
                if (showUser)
                    node.SelectAction = TreeNodeSelectAction.Expand;
                else
                    node.Target = "right_frame";
                node.NavigateUrl = "#" + dept.id;
                ShowSubdepartmentInTreeView(node, dept.id, showUser);
                if (showUser)
                    ShowDepartmentUsersInTreeView(node, dept.id);
                tree.Nodes.Add(node);
            }
        }
        /// <summary>
        /// 显示下级部门列表
        /// </summary>
        /// <param name="node"></param>
        /// <param name="parent"></param>
        /// <param name="showUser"></param>
        protected void ShowSubdepartmentInTreeView(TreeNode node, int parent, bool showUser)
        {
            var list = DepartmentInstance.FindList(f => f.Parent == parent && f.Delete == false).OrderBy(o => o.Name);
            foreach (var dept in list)
            {
                TreeNode sub = new TreeNode();
                sub.Text = dept.Name;
                if (showUser) sub.SelectAction = TreeNodeSelectAction.Expand;
                else node.Target = "right_frame";
                sub.NavigateUrl = "#" + dept.id;

                ShowSubdepartmentInTreeView(sub, dept.id, showUser);
                if (showUser)
                    ShowDepartmentUsersInTreeView(sub, dept.id);
                node.ChildNodes.Add(sub);
            }
        }
        /// <summary>
        /// 显示指定部门的所有用户列表
        /// </summary>
        /// <param name="node"></param>
        /// <param name="parent"></param>
        protected void ShowDepartmentUsersInTreeView(TreeNode node, int parent)
        {
            var users = AccountInstance.FindList(f => f.Department == parent).OrderBy(o=>o.Name);
            foreach (var user in users)
            {
                node.ChildNodes.Add(new TreeNode
                {
                    Text = user.Name,
                    ShowCheckBox = true,
                    SelectAction = TreeNodeSelectAction.Expand,
                    NavigateUrl = "#" + user.id
                });
            }
        }
    }
}