using System;
using System.Collections.Generic;
using System.Linq;

using Wbs.Everdigm.BLL;
using Wbs.Everdigm.Database;
using System.Web.UI.WebControls;

namespace Wbs.Everdigm.Web
{
    /// <summary>
    /// 权限相关基类
    /// </summary>
    public class BasePermissionPage : BaseBLLPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (HasSessionLose)
            {
                ShowNotification("../default.aspx", "Your session has expired, please login again.", false, true);
            }
        }
        /// <summary>
        /// 业务处理逻辑
        /// </summary>
        protected PermissionBLL PermissionInstance { get { return new PermissionBLL(); } }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="strId"></param>
        /// <param name="strParent"></param>
        protected void Edit(int newParent, TB_Permission obj)
        {
            // 修改
            var per = PermissionInstance.Find(p => p.id == obj.id);
            if (null == per)
            {
                // 无法进行更新
                ShowNotification("./permission_list.aspx", "Cannot edit permission: object not exist.", false);
                return;
            }
            else
            {
                // 查看是否更改了父级菜单
                if (newParent != per.Parent)
                {
                    // 更改了父级菜单
                    // 查找兄弟菜单并且显示顺序在本菜单之后的
                    var brothers = PermissionInstance.FindList(b => b.Parent == per.Parent && b.DisplayOrder > per.DisplayOrder);
                    foreach (var bro in brothers)
                    {
                        // 这些兄弟节点的显示顺序都减一
                        PermissionInstance.Update(u => u.id == bro.id, a =>
                        {
                            a.DisplayOrder = a.DisplayOrder - 1;
                        });
                    }
                    // 查找新兄弟菜单项
                    brothers = PermissionInstance.FindList(b => b.Parent == newParent);
                    // 菜单在新兄弟菜单项中显示在末尾
                    obj.Parent = newParent;
                    obj.DisplayOrder = brothers.Count() + 1;
                }
                Update(obj);

                // 记录历史
                SaveHistory(new TB_AccountHistory()
                {
                    ActionId = ActionInstance.Find(f => f.Name.Equals("EditPermission")).id,
                    ObjectA = "[id=" + per.id + "] " + (obj.Name.Equals(per.Name) ? obj.Name : ("[" + per.Name + "] to [" + obj.Name + "]"))
                });
                ShowNotification("./permission_list.aspx", "Success: You have changed the permission [" + per.Name + "].");
            }
        }

        protected void Update(TB_Permission per)
        {
            PermissionInstance.Update(p => p.id == per.id, update =>
            {
                update.Description = per.Description;
                update.DisplayOrder = per.DisplayOrder;
                update.Image = per.Image;
                update.IsDefault = per.IsDefault;
                update.Name = per.Name;
                update.Parent = per.Parent;
                update.Url = per.Url;
                update.Delete = per.Delete;
            });
        }
        /// <summary>
        /// 在树形结构中显示目录
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="parent"></param>
        /// <param name="all">大于 0 时显示全部，小于 0 时显示部分，等于 0 时显示全部且显示复选框</param>
        /// <param name="rights"></param>
        protected void ShowPermissionsInTreeView(TreeView tree, int parent, int all, List<int> rights)
        {
            if (all == 0) tree.ShowCheckBoxes = TreeNodeTypes.All;
            var menus = PermissionInstance.FindList(m => m.Parent == parent && m.Delete == false &&
                (all >= 0 ? m.id > 0 : rights.Contains(m.id))).OrderBy(o => o.DisplayOrder);
            foreach (var m in menus)
            {
                TreeNode node = new TreeNode();
                node.Text = m.Name;
                if (string.IsNullOrEmpty(m.Url))
                    node.SelectAction = TreeNodeSelectAction.Expand;
                node.ToolTip = m.Description;
                node.Target = "right_frame";
                node.ImageUrl = m.Image;
                node.Checked = all == 0 && rights.Contains(m.id);
                node.NavigateUrl = all >= 0 ? ("#" + m.id) : (m.Url);
                ShowSubPermissions(node, m.id, all, rights);
                tree.Nodes.Add(node);
            }
        }
        /// <summary>
        /// 显示子树
        /// </summary>
        /// <param name="node"></param>
        /// <param name="parent"></param>
        /// <param name="all"></param>
        /// <param name="rights"></param>
        private void ShowSubPermissions(TreeNode node, int parent, int all, List<int> rights)
        {
            var menus = PermissionInstance.FindList(m => m.Parent == parent && m.Delete == false &&
                (all >= 0 ? m.id > 0 : rights.Contains(m.id))).OrderBy(o => o.DisplayOrder);
            foreach (var m in menus)
            {
                TreeNode sub = new TreeNode();
                sub.Text = m.Name;
                if (string.IsNullOrEmpty(m.Url))
                    sub.SelectAction = TreeNodeSelectAction.Expand;
                sub.ToolTip = m.Description;
                sub.Target = "right_frame";
                sub.ImageUrl = m.Image;
                sub.Checked = all == 0 && rights.Contains(m.id);
                // 大于等于0时需要的是选择其ID和name，小于0时直接点击打开链接
                sub.NavigateUrl = all >= 0 ? ("#" + m.id) : (m.Url);
                ShowSubPermissions(sub, m.id, all, rights);
                node.ChildNodes.Add(sub);
            }
        }
    }
}