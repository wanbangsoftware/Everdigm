using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.Web.main
{
    public partial class equipment_status_code : BaseEquipmentPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            _cookie_name_ = "_equipment_situation_code_list_page_";
            cookieName.Value = _cookie_name_;
            base.Page_Load(sender, e);
            if (!HasSessionLose)
            {
                if (!IsPostBack)
                {
                    hidPageIndex.Value = null == Request.Cookies[_cookie_name_] ? "" : Request.Cookies[_cookie_name_].Value;
                    ShowEquipmentSituationCodes();
                    ShowEquipmentSituations();
                    if (!string.IsNullOrEmpty(_key))
                    {
                        hidID.Value = _key;
                        // 显示编辑信息
                        ShowEdit();
                    }
                }
            }
        }
        /// <summary>
        /// 显示设备状态列表
        /// </summary>
        private void ShowEquipmentSituations()
        {
            ddlType.Items.Clear();
            ddlType.Items.Add(new ListItem() { Text = "Select Situation:", Value = "" });
            var statuses = StatusInstance.FindList(null).OrderBy(o => o.Name);
            foreach (var status in statuses)
            {
                ddlType.Items.Add(new ListItem() { Text = status.Name, Value = status.id.ToString() });
            }
        }

        private void ShowEdit()
        {
            var code = CodeInstance.Find(f => f.id == ParseInt(Utility.Decrypt(hidID.Value)));
            if (null != code)
            {
                txtCode.Value = code.Code;
                txtName.Value = code.Name;
                ddlType.SelectedValue = code.Status.ToString();
            }
            else
            {
                ShowNotification("./equipment_status_code.aspx", "Error: Cannot edit null object of <a>Equipment Situation Code</a>.", false);
            }
        }

        private void BuildCode(TB_EquipmentStatusCode obj)
        {
            obj.Code = txtCode.Value.Trim();
            obj.Name = txtName.Value.Trim();
            obj.Status = ParseInt(ddlType.SelectedValue);
            if (obj.Status <= 0)
                obj.Status = (int?)null;
        }

        private void NewCode()
        {
            var m = CodeInstance.GetObject();
            BuildCode(m);
            CodeInstance.Add(m);

            // 保存历史记录
            SaveHistory(new TB_AccountHistory
            {
                ActionId = ActionInstance.Find(f => f.Name.Equals("AddEquipmentStatusCode")).id,
                ObjectA = "[id=" + m.id + "] " + m.Name + ", " + m.Code
            });

            ShowNotification("./equipment_status_code.aspx", "You have added a new Equipment Situation Code: " + m.Name + "(" + m.Code + ").");
        }

        private void EditCode()
        {
            var code = CodeInstance.Find(f => f.id == ParseInt(Utility.Decrypt(hidID.Value)));
            if (null != code)
            {
                BuildCode(code);
                Update(code);

                // 保存历史记录
                SaveHistory(new TB_AccountHistory
                {
                    ActionId = ActionInstance.Find(f => f.Name.Equals("EditEquipmentStatusCode")).id,
                    ObjectA = "[id=" + code.id + "] " + code.Name + ", " + code.Code
                });

                ShowNotification("./equipment_status_code.aspx", "You changed the equipment situation code.");
            }
            else
            {
                ShowNotification("./equipment_status_code.aspx", "Error: paramenter error, cannot edit null object(equipment situation code).", false);
            }
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            if (!HasSessionLose)
            {
                if ("" == hidID.Value)
                { NewCode(); }
                else
                { EditCode(); }
            }
        }
        /// <summary>
        /// 显示所有设备状态码列表
        /// </summary>
        private void ShowEquipmentSituationCodes()
        {
            var totalRecords = 0;
            var pageIndex = "" == hidPageIndex.Value ? 1 : int.Parse(hidPageIndex.Value);
            var list = CodeInstance.FindPageList<TB_EquipmentStatusCode>(pageIndex, PageSize, out totalRecords, null, "Status,Name");
            var totalPages = totalRecords / PageSize + (totalRecords % PageSize > 0 ? 1 : 0);
            pageIndex = 0 == pageIndex ? totalPages : pageIndex;
            if (pageIndex >= totalPages)
            {
                pageIndex = totalPages;
                list = CodeInstance.FindPageList<TB_EquipmentStatusCode>(pageIndex, PageSize, out totalRecords, null, "Status,Name");
            }

            string html = "";
            if (totalRecords < 1)
            {
                html = "<tr><td colspan=\"6\">No records, you can change condition and try again, or " +
                    " <a>Add</a> some new one.</td></tr>";
            }
            else
            {
                var cnt = (pageIndex - 1) * PageSize;
                foreach (var obj in list)
                {
                    cnt++;
                    var id = Utility.UrlEncode(Utility.Encrypt(obj.id.ToString()));
                    html += "<tr>" +
                        "<td style=\"width: 40px; text-align: center;\"><input type=\"checkbox\" id=\"cb_" + id + "\" /></td>" +
                        "<td style=\"width: 40px; text-align: center;\">" + cnt + "</td>" +
                        "<td><a href=\"./equipment_status_code.aspx?key=" + id + "\" >" + ("" == obj.Name ? "-" : obj.Name) + "</a></td>" +
                        "<td>" + ((int?)null == obj.Status ? "-" : obj.TB_EquipmentStatusName.Name) + "</td>" +
                        "<td>" + obj.TB_EquipmentStatusName.Code + obj.Code + "</td>" +
                        "<td></td>" +
                        "</tr>";
                }
            }
            tbodyBody.InnerHtml = html;
            divPagging.InnerHtml = "";
            if (totalRecords > 0)
                ShowPaggings(pageIndex, totalPages, totalRecords, "./equipment_status_code.aspx", divPagging);
        }

        protected void btQuery_Click(object sender, EventArgs e)
        {
            ShowEquipmentSituationCodes();
        }
    }
}