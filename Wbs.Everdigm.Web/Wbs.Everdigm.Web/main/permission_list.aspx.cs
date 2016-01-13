using System;
using System.Collections.Generic;
using System.Linq;
using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.Web.main
{
    public partial class permission_list : BasePermissionPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            _cookie_name_ = "_permission_list_page_";
            cookieName.Value = _cookie_name_;
            base.Page_Load(sender, e);
            if (!HasSessionLose)
            {
                if (!IsPostBack)
                {
                    hidPageIndex.Value = null == Request.Cookies[_cookie_name_] ? "" : Request.Cookies[_cookie_name_].Value;
                    ShowPermissions();
                }
            }
        }

        private void ShowPermissions()
        {
            List<int> menus;
            if ("" == hidParent.Value)
                menus = PermissionInstance.GetAllMenus();
            else
                menus = PermissionInstance.GetSubmenus(ParseInt(hidParent.Value));
            
            var totalRecords = 0;
            var pageIndex = "" == hidPageIndex.Value ? 1 : int.Parse(hidPageIndex.Value);
            var list = PermissionInstance.FindPageList<TB_Permission>(pageIndex, PageSize, out totalRecords,
                p => p.Delete == false && (p.Name.IndexOf(txtName.Value.Trim()) >= 0) && menus.Contains(p.id), "Parent,DisplayOrder");
            var totalPages = totalRecords / PageSize + (totalRecords % PageSize > 0 ? 1 : 0);

            list.OrderBy(o => o.id).ThenBy(t => t.Name);
            string html = "";
            if (totalRecords < 1)
            {
                html = "<tr><td colspan=\"8\">No records, You can change the condition and try again or " +
                    " <a href=\"./permission_add.aspx\">ADD</a> new one.</td></tr>";
            }
            else
            {
                var cnt = (pageIndex - 1) * PageSize;
                foreach (var obj in list)
                {
                    cnt++;
                    var upper = 0 == obj.Parent ? null : PermissionInstance.Find(f => f.id == obj.Parent);
                    var id = Utility.UrlEncode(Utility.Encrypt(obj.id.ToString()));
                    html += "<tr>" +
                            "<td style=\"width: 40px; text-align: center;\"><input type=\"checkbox\" id=\"cb_" +
                                id + "\" /></td>" +
                            "<td style=\"width: 40px; text-align: center;\">" + cnt + "</td>" +
                            "<td style=\"width: 150px;\"><a href=\"./permission_add.aspx?key=" +
                                id + "\" title=\"Edit\">" + obj.Name + "</a></td>" +
                            "<td style=\"width: 40px;\">" + ("" == obj.Image ? "-" : ("<img alt=\"\" src=\"" + obj.Image + "\" />")) + "</td>" +
                            "<td style=\"width: 60px;\">" + (obj.IsDefault.Value ? ("<img alt=\"\" src=\"../images/check_hover.png\" />") : "-") + "</td>" +
                            "<td><a href=\"#p" + (0 == obj.Parent ? "" : obj.Parent.ToString()) + "\" title=\"查询本类页面列表\">" +
                                (null == upper ? "" : upper.Name) + "</a></td>" +
                            "<td>" + obj.Url + "</td>" +
                            "<td>" + obj.Description + "</td>" +
                            "</tr>";
                }
            }
            tbodyBody.InnerHtml = html;
            divPagging.InnerHtml = "";
            if (totalRecords > 0)
                ShowPaggings(pageIndex, totalPages, totalRecords, "./permission_list.aspx", divPagging);
        }

        protected void btQuery_Click(object sender, EventArgs e)
        {
            ShowPermissions();
        }

        protected void bt_Delete_Click(object sender, EventArgs e)
        {
            if ("" != hidID.Value) {
                var ids = GetIdList(hidID.Value.Split(new char[] { ',' }));
                var list = PermissionInstance.FindList(f => ids.Contains(f.id));
                foreach (var tmp in list)
                {
                    tmp.Delete = true;
                    Update(tmp);

                    var his = new TB_AccountHistory();
                    his.Account = Account.id;
                    his.ActionId = ActionInstance.Find(f => f.Name.Equals("DeletePermission")).id;
                    his.Ip = Utility.GetClientIP(this.Context);
                    his.ObjectA = "[id=" + tmp.id + "] " + tmp.Name;
                    SaveHistory(his);
                }
                ShowNotification("./permission_list.aspx", "Success: You have delete " + ids.Count() + " permission(s).");
            }
        }

        protected void bt_ToUpper_Click(object sender, EventArgs e)
        {
            ChangeDisplayOrder(true);
        }

        protected void bt_ToDown_Click(object sender, EventArgs e)
        {
            ChangeDisplayOrder(false);
        }
        /// <summary>
        /// 更改显示顺序
        /// </summary>
        /// <param name="ord">原始项目</param>
        /// <param name="brother">附近的兄弟项目</param>
        /// <param name="toupper">true=向上调整，false=向下调整</param>
        private void ChangeDisplayOrder(TB_Permission ord, TB_Permission brother, bool toupper)
        {
            if (null != brother)
            {
                brother.DisplayOrder = toupper ? (brother.DisplayOrder + 1) : (brother.DisplayOrder - 1);
                Update(brother);
                ord.DisplayOrder = toupper ? (ord.DisplayOrder - 1) : (ord.DisplayOrder + 1);
                Update(ord);
            }
        }
        /// <summary>
        /// 更改显示顺序
        /// </summary>
        /// <param name="toupper">true=往上调，false=往下调</param>
        private void ChangeDisplayOrder(bool toupper)
        {
            var id = int.Parse(Utility.Decrypt(hidID.Value));
            var obj = PermissionInstance.Find(f => f.id == id);
            var brothers = PermissionInstance.FindList(f => f.Parent == obj.Parent).OrderBy(o => o.DisplayOrder);
            if (obj.DisplayOrder == 0)
            {
                // 原始顺序在第一位时，只有向下调
                if (!toupper)
                {
                    var t = brothers.FirstOrDefault(f => f.DisplayOrder == obj.DisplayOrder + 1);
                    ChangeDisplayOrder(obj, t, toupper);
                }
            }
            else if (obj.DisplayOrder == brothers.Count() - 1)
            {
                // 原始顺序在最后一位时，只有向上调
                if (toupper)
                {
                    var t = brothers.FirstOrDefault(f => f.DisplayOrder == obj.DisplayOrder - 1);
                    ChangeDisplayOrder(obj, t, toupper);
                }
            }
            else
            {
                var t = brothers.FirstOrDefault(f => f.DisplayOrder == (toupper ? (obj.DisplayOrder - 1) : (obj.DisplayOrder + 1)));
                ChangeDisplayOrder(obj, t, toupper);
            }

            var his = new TB_AccountHistory();
            his.Account = Account.id;
            his.ActionId = ActionInstance.Find(f => f.Name.Equals("EditPermission")).id;
            his.Ip = Utility.GetClientIP(this.Context);
            his.ObjectA = "[id=" + obj.id + "] " + obj.Name + ", change display order to " + (toupper ? "lower" : "upper");
            SaveHistory(his);

            ShowNotification("./permission_list.aspx", "Success: You have changed the display order of " + obj.Name + ".");
        }
    }
}