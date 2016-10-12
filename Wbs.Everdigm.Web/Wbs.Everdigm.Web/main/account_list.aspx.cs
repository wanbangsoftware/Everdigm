using System;
using System.Collections.Generic;
using System.Linq;

using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.Web.main
{
    public partial class account_list : BaseAccountPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            _cookie_name_ = "_account_list_page_";
            cookieName.Value = _cookie_name_;
            base.Page_Load(sender, e);
            if (!HasSessionLose)
            {
                if (!IsPostBack)
                {
                    hidPageIndex.Value = null == Request.Cookies[_cookie_name_] ? "" : Request.Cookies[_cookie_name_].Value;
                    initQueryCondition();
                    ShowAccountList();
                }
            }
        }

        private void initQueryCondition()
        {
            if (!string.IsNullOrEmpty(_key))
            {
                var k = Utility.Decrypt(_key).Split(new char[] { ',' });
                switch (k[0])
                { 
                    case "d":
                        // 部门查询
                        hidDepartment.Value = k[1];
                        var dept = DepartmentInstance.Find(f => f.id == ParseInt(k[1]));
                        txtDepartment.Value = dept.Name;
                        break;
                    case "r":
                        hidRole.Value = k[1];
                        var role = RoleInstance.Find(f => f.id == ParseInt(k[1]));
                        txtRole.Value = role.Name;
                        break;
                }
            }
        }

        private void ShowAccountList()
        {
            // 部门id列表
            var depts = "" != hidDepartment.Value ?
                (DepartmentInstance.GetSubdepartments(ParseInt(hidDepartment.Value))) :
                (DepartmentInstance.GetAllDepartments());
            // 角色列表
            List<int> roles;
            if ("" != hidRole.Value)
            {
                roles = new List<int>();
                roles.Add(ParseInt(hidRole.Value));
            }
            else
            {
                roles = RoleInstance.GetAllRole();
            }
            var totalRecords = 0;
            var pageIndex = "" == hidPageIndex.Value ? 1 : int.Parse(hidPageIndex.Value);
            var list = AccountInstance.FindPageList<TB_Account>(pageIndex, PageSize, out totalRecords,
                f => (f.Name.IndexOf(txtName.Value.Trim()) >= 0) &&
                    (roles.Contains(f.Role.Value) || f.Role == (int?)null) &&
                    (depts.Contains(f.Department.Value) || f.Department == (int?)null) && f.Delete == false, "Name");
            var totalPages = totalRecords / PageSize + (totalRecords % PageSize > 0 ? 1 : 0);

            string html = "";
            if (totalRecords < 1)
            {
                html = "<tr><td colspan=\"12\">No records, You can change the condition and try again or " +
                    " <a href=\"./account_add.aspx\">ADD</a> new one.</td></tr>";
            }
            else
            {
                var cnt = (pageIndex - 1) * PageSize;
                foreach (var obj in list)
                {
                    cnt++;
                    var id = Utility.UrlEncode(Utility.Encrypt(obj.id.ToString()));
                    html += "<tr " + (obj.Locked == true ? "style=\"color: red;\"" : "") + ">" +
                        "<td style=\"width: 40px; text-align: center;\"><input type=\"checkbox\" id=\"cb_" + id + "\" /></td>" +
                        "<td style=\"width: 40px; text-align: center;\">" + cnt + "</td>" +
                        "<td style=\"width: 60px;\"><a href=\"./account_add.aspx?key=" + id + "\" >" + obj.Name + "</a></td>" +
                        "<td style=\"width: 60px;\">" + obj.Code + "</td>" +
                        "<td style=\"width: 80px;\">" + obj.RegisterTime.Value.ToString("yyyy-MM-dd") + "</td>" +
                        "<td style=\"width: 50px;\">" + (obj.Locked == true ? "Locked" : "Normal") + "</td>" +
                        "<td>" + ("<a href=\"#d" + (null == obj.Department ? "" : obj.Department.ToString()) + "\" >" +
                            (null == obj.Department ? "-" : obj.TB_Department.Name) + "</a>") + "</td>" +
                        "<td>" + ("<a href=\"#r" + (null == obj.Role ? "" : obj.Role.ToString()) + "\" >" +
                            (null == obj.Role ? "-" : obj.TB_Role.Name) + "</a>") + "</td>" +
                        "<td>" + obj.LoginTimes.ToString() + "</td>" +
                        "<td>" + (null == obj.LastLoginTime ? "never" : obj.LastLoginTime.Value.ToString("yyyy-MM-dd HH:mm:ss")) + "</td>" +
                        "<td>" + obj.LastLoginIp + "</td>" +
                        "<td></td>" +
                        "</tr>";
                }
            }
            tbodyBody.InnerHtml = html;
            divPagging.InnerHtml = "";
            if (totalRecords > 0)
                ShowPaggings(pageIndex, totalPages, totalRecords, "./account_list.aspx", divPagging);
        }

        protected void btQuery_Click(object sender, EventArgs e)
        {
            if (!HasSessionLose)
                ShowAccountList();
        }

        protected void bt_Unlock_Click(object sender, EventArgs e)
        {
            if (!HasSessionLose)
            {
                if ("" != hidID.Value)
                {
                    var ids = GetIdList(hidID.Value.Split(new char[] { ',' }));
                    var list = AccountInstance.FindList(f => ids.Contains(f.id) && f.Delete == false);
                    foreach (var account in list)
                    {
                        account.Locked = !account.Locked;
                        Update(account);

                        SaveHistory(new TB_AccountHistory
                        {
                            ActionId = ActionInstance.Find(f => f.Name.Equals("Lock/UnlockAccount")).id,
                            ObjectA = "[id=" + account.id + "] " + account.Name
                        });
                    }
                    ShowNotification("./account_list.aspx", "Success: You have Lock/UnlockAccount " + ids.Count() + " account(s).");
                }
            }
        }

        protected void btDelete_Click(object sender, EventArgs e)
        {
            if (!HasSessionLose)
            {
                if ("" != hidID.Value)
                {
                    var ids = GetIdList(hidID.Value.Split(new char[] { ',' }));
                    var list = AccountInstance.FindList(f => ids.Contains(f.id) && f.Delete == false);
                    foreach (var account in list)
                    {
                        account.Delete = true;
                        Update(account);

                        SaveHistory(new TB_AccountHistory
                        {
                            ActionId = ActionInstance.Find(f => f.Name.Equals("DeleteAccount")).id,
                            ObjectA = "[id=" + account.id + "] " + account.Name
                        });
                    }
                    ShowNotification("./account_list.aspx", "Success: You have delete " + ids.Count() + " account(s).");
                }
            }
        }
    }
}