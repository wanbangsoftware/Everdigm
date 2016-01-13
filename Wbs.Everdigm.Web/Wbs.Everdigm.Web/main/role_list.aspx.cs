using System;
using System.Linq;

using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.Web.main
{
    public partial class role_list : BaseRolePage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            _cookie_name_ = "_role_list_page_";
            cookieName.Value = _cookie_name_;
            base.Page_Load(sender, e);
            if (!HasSessionLose)
            {
                if (!IsPostBack)
                {
                    hidPageIndex.Value = null == Request.Cookies[_cookie_name_] ? "" : Request.Cookies[_cookie_name_].Value;
                    ShowRoles();
                }
            }
        }

        private void ShowRoles()
        {
            var totalRecords = 0;
            var pageIndex = "" == hidPageIndex.Value ? 1 : int.Parse(hidPageIndex.Value);
            var list = RoleInstance.FindPageList<TB_Role>(pageIndex, PageSize, out totalRecords,
                p => p.Delete == false && p.Name.IndexOf(txtName.Value.Trim()) >= 0, "Name");
            var totalPages = totalRecords / PageSize + (totalRecords % PageSize > 0 ? 1 : 0);
            //list = list.OrderBy(o => o.IsAdministrator).ThenBy(t => t.IsDefault).ThenBy(t => t.AddTime);

            string html = "";
            if (totalRecords < 1)
            {
                html = "<tr><td colspan=\"8\">No records, you can change condition and try again, or " +
                    " <a href=\"./role_add.aspx\">Add</a> some new role.</td></tr>";
            }
            else
            {
                var cnt = (pageIndex - 1) * PageSize;
                foreach (var obj in list)
                {
                    cnt++;
                    var users = AccountInstance.FindList(f => f.Role == obj.id).Count();
                    var id = Utility.UrlEncode(Utility.Encrypt(obj.id.ToString()));
                    html += "<tr>" +
                        // 系统默认角色无法删除
                        "<td style=\"width: 40px; text-align: center;\">" + (obj.IsDefault.Value ? "" :
                            ("<input type=\"checkbox\" id=\"cb_" + id + "\" />")) + "</td>" +
                        "<td style=\"width: 40px; text-align: center;\">" + cnt + "</td>" +
                        "<td><a href=\"./role_add.aspx?key=" + id + "\" title=\"click to edit\">" + ("" == obj.Name ? "-" : obj.Name) + "</a></td>" +
                        "<td>" + (obj.IsDefault.Value ? "Yes" : "-") + "</td>" +
                        "<td>" + (obj.IsAdministrator.Value ? "Yes" : "-") + "</td>" +
                        "<td>" + (users > 0 ? ("<a href=\"./account_list.aspx?key=" +
                            Utility.UrlEncode(Utility.Encrypt("r," + obj.id.ToString())) + "\" >" + users.ToString() + "</a>") : users.ToString()) + "</td>" +
                        "<td><a href=\"./role_authority.aspx?key=" + id + "\">Edit</a></td>" +
                        "<td>" + obj.Description + "</td>" +
                        "</tr>";
                }
            }
            tbodyBody.InnerHtml = html;
            divPagging.InnerHtml = "";
            if (totalRecords > 0)
                ShowPaggings(pageIndex, totalPages, totalRecords, "./role_list.aspx", divPagging);
        }

        protected void btQuery_Click(object sender, EventArgs e)
        {
            ShowRoles();
        }

        protected void btDelete_Click(object sender, EventArgs e)
        {
            if (!HasSessionLose)
            {
                if ("" != hidID.Value)
                {
                    var ids = GetIdList(hidID.Value.Split(new char[] { ',' }));
                    var list = RoleInstance.FindList(f => ids.Contains(f.id) && f.Delete == false);
                    foreach (var role in list)
                    {
                        role.Delete = true;
                        Update(role);

                        SaveHistory(new TB_AccountHistory
                        {
                            ActionId = ActionInstance.Find(f => f.Name.Equals("DeleteRole")).id,
                            ObjectA = "[id=" + role.id + "] " + role.Name
                        });
                    }
                    // 更新默认角色
                    var dftRole = RoleInstance.Find(f => f.IsDefault == true && f.Delete == false);
                    foreach (var role in ids)
                    {
                        AccountInstance.ClearRoleInfo(role, null == dftRole ? 0 : dftRole.id);
                    }
                    ShowNotification("./role_list.aspx", "Success: You have delete " + ids.Count() + " role(s).");
                }
            }
        }
    }
}