using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.Web.main
{
    public partial class department_list : BaseDepartmentPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            _cookie_name_ = "_department_list_page_";
            cookieName.Value = _cookie_name_;
            base.Page_Load(sender, e);
            if (!HasSessionLose)
            {
                if (!IsPostBack)
                {
                    hidPageIndex.Value = null == Request.Cookies[_cookie_name_] ? "" : Request.Cookies[_cookie_name_].Value;
                    ShowDepartments();
                }
            }
        }

        private void ShowDepartments()
        {
            List<int> depts;
            if ("" == hidDepartment.Value)
                depts = DepartmentInstance.GetAllDepartments();
            else
                depts = DepartmentInstance.GetSubdepartments(ParseInt(hidDepartment.Value));

            var totalRecords = 0;
            var pageIndex = "" == hidPageIndex.Value ? 1 : int.Parse(hidPageIndex.Value);
            var list = DepartmentInstance.FindPageList<TB_Department>(pageIndex, PageSize, out totalRecords,
                f => f.Delete == false && (f.Name.IndexOf(txtName.Value.Trim()) >= 0) && depts.Contains(f.id), "Parent,Name");
            var totalPages = totalRecords / PageSize + (totalRecords % PageSize > 0 ? 1 : 0);
            pageIndex = 0 == pageIndex ? totalPages : pageIndex;
            if (pageIndex > totalPages)
            {
                pageIndex = totalPages;
                list = DepartmentInstance.FindPageList<TB_Department>(pageIndex, PageSize, out totalRecords,
                    f => f.Delete == false && (f.Name.IndexOf(txtName.Value.Trim()) >= 0) && depts.Contains(f.id), "Parent,Name");
            }

            string html = "";
            if (totalRecords < 1)
            {
                html = "<tr><td colspan=\"10\">No records, You can change the condition and try again or " +
                    " <a href=\"./department_add.aspx\">ADD</a> new one.</td></tr>";
            }
            else
            {
                var cnt = (pageIndex - 1) * PageSize;
                foreach (var obj in list)
                {
                    cnt++;
                    var users = AccountInstance.FindList(f => f.Department == obj.id).Count();
                    var upper = 0 == obj.Parent ? null : DepartmentInstance.Find(f => f.id == obj.Parent);
                    var id = Utility.UrlEncode(Utility.Encrypt(obj.id.ToString()));
                    html += "<tr>" +
                        "<td style=\"width: 40px; text-align: center;\"><input type=\"checkbox\" id=\"cb_" + id + "\" /></td>" +
                        "<td style=\"width: 40px; text-align: center;\">" + cnt + "</td>" +
                        "<td><a href=\"./department_add.aspx?key=" + id + "\" >" + obj.Name + "</a></td>" +
                        "<td>" + obj.Phone + "</td>" +
                        "<td>" + obj.Fax + "</td>" +
                        "<td>" + (obj.IsDefault == true ? "Yes" : "-") + "</td>" +
                        "<td><a href=\"#d" + (null == upper ? "" : upper.id.ToString()) + "\" >" +
                            (null == upper ? "" : upper.Name) + "</a></td>" +
                        "<td>" + (0 == users ? "0" : ("<a href=\"./account_list.aspx?key=" +
                            Utility.UrlEncode(Utility.Encrypt("d," + obj.id.ToString())) + "\" >" +
                            users + "</a>")) + "</td>" +
                        "<td>" + obj.Room + "</td>" +
                        "<td>" + obj.Address + "</td>" +
                        "</tr>";
                }
            }
            tbodyBody.InnerHtml = html;
            divPagging.InnerHtml = "";
            if (totalRecords > 0)
                ShowPaggings(pageIndex, totalPages, totalRecords, "./department_list.aspx", divPagging);
        }

        protected void btQuery_Click(object sender, EventArgs e)
        {
            if (!HasSessionLose)
            {
                ShowDepartments();
            }
        }

        protected void bt_Delete_Click(object sender, EventArgs e)
        {
            if (!HasSessionLose)
            {
                if ("" != hidID.Value)
                {
                    var subIds = new List<int>();
                    var ids = GetIdList(hidID.Value.Split(new char[] { ',' }));
                    subIds.AddRange(ids);
                    var list = DepartmentInstance.FindList(f => ids.Contains(f.id));
                    foreach (var dept in list)
                    {
                        // 先删除所有下属部门
                        var subs = DepartmentInstance.GetSubdepartments(dept.id);
                        subIds.AddRange(subs);
                        var subList = DepartmentInstance.FindList(f => subs.Contains(f.id) && f.Delete == false);
                        foreach (var sub in subList)
                        {
                            //subIds.Add(sub.id);
                            sub.Delete = true;
                            Update(sub);
                        }

                        // 更新删除状态
                        dept.Delete = true;
                        Update(dept);
                        // 保存删除历史记录
                        SaveHistory(new TB_AccountHistory
                        {
                            ActionId = ActionInstance.Find(f => f.Name.Equals("DeleteDept")).id,
                            ObjectA = "[id=" + dept.id + "] " + dept.Name
                        });
                    }
                    // 删除动作完成之后才处理用户的默认部门信息
                    TB_Department dftDept = DepartmentInstance.GetDefaultDepartment();
                    // 清理所有下级部门的用户列表
                    foreach (var id in subIds)
                        AccountInstance.ClearDeptInfo(id, null == dftDept ? 0 : dftDept.id);

                    ShowNotification("./department_list.aspx", "Success: You have delete " + ids.Count() + " department(s).");
                }
            }
        }
    }
}