using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.Web.main
{
    public partial class equipment_status : BaseEquipmentPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            _cookie_name_ = "_equipment_status_list_page_";
            cookieName.Value = _cookie_name_;
            base.Page_Load(sender, e);
            if (!HasSessionLose)
            {
                if (!IsPostBack)
                {
                    checkCheckBoxes();
                    hidPageIndex.Value = null == Request.Cookies[_cookie_name_] ? "" : Request.Cookies[_cookie_name_].Value;
                    ShowEquipmentStatus();

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
        /// 检测选择框是否能用
        /// </summary>
        private void checkCheckBoxes()
        {
            var _in = StatusInstance.Find(f => f.IsInventory == true);
            cbIsInventory.Enabled = null == _in;

            var wait = StatusInstance.Find(f => f.IsWaiting == true);
            cbIsWaiting.Enabled = null == wait;

            var _out = StatusInstance.Find(f => f.IsOutstorage == true);
            cbIsOutstorage.Enabled = null == _out;

            var over = StatusInstance.Find(f => f.IsOverhaul == true);
            cbIsOverhaul.Enabled = null == over;
        }

        private void ShowEdit()
        {
            var s = StatusInstance.Find(f => f.id == ParseInt(Utility.Decrypt(hidID.Value)));
            if (null != s)
            {
                txtCode.Value = s.Code;
                txtName.Value = s.Name;
                cbIsInventory.Checked = s.IsInventory.Value;
                if (s.IsInventory == true)
                    cbIsInventory.Enabled = true;

                cbIsOutstorage.Checked = s.IsOutstorage.Value;
                if (s.IsOutstorage == true)
                    cbIsOutstorage.Enabled = true;

                cbIsOverhaul.Checked = s.IsOverhaul.Value;
                if (s.IsOverhaul == true)
                    cbIsOverhaul.Enabled = true;

                cbIsWaiting.Checked = s.IsWaiting.Value;
                if (s.IsWaiting == true)
                    cbIsWaiting.Enabled = true;
            }
            else
            {
                ShowNotification("./equipment_status.aspx", "Error: Cannot edit null object of <a>Equipment Status</a>.", false);
            }
        }

        private void BuildStatus(TB_EquipmentStatusName obj)
        {
            obj.Code = txtCode.Value.Trim();
            obj.Name = txtName.Value.Trim();
            obj.IsInventory = cbIsInventory.Checked;
            obj.IsOutstorage = cbIsOutstorage.Checked;
            obj.IsOverhaul = cbIsOverhaul.Checked;
            obj.IsWaiting = cbIsWaiting.Checked;
        }

        private void NewStatus()
        {
            var t = StatusInstance.GetObject();
            BuildStatus(t);
            StatusInstance.Add(t);

            // 保存历史记录
            SaveHistory(new TB_AccountHistory
            {
                ActionId = ActionInstance.Find(f => f.Name.Equals("AddEquipmentStatus")).id,
                ObjectA = "[id=" + t.id + "] " + t.Name + ", " + t.Code
            });

            ShowNotification("./equipment_status.aspx", "You have added a new Equipment type: " + t.Name + "(" + t.Code + ").");
        }

        private void EditStatus()
        {
            var t = StatusInstance.Find(f => f.id == ParseInt(Utility.Decrypt(hidID.Value)));
            if (null != t)
            {
                BuildStatus(t);
                Update(t);

                // 保存历史记录
                SaveHistory(new TB_AccountHistory()
                {
                    ActionId = ActionInstance.Find(f => f.Name.Equals("EditEquipmentStatus")).id,
                    ObjectA = "[id=" + t.id + "] " + t.Name + ", " + t.Code
                });

                ShowNotification("./equipment_status.aspx", "You changed the equipment status.");
            }
            else
            {
                ShowNotification("./equipment_status.aspx", "Error: paramenter error, cannot edit null object(equipment status).", false);
            }
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            if (!HasSessionLose)
            {
                if ("" == hidID.Value)
                { NewStatus(); }
                else
                { EditStatus(); }
            }
        }
        /// <summary>
        /// 显示设备状态列表
        /// </summary>
        private void ShowEquipmentStatus()
        {
            var totalRecords = 0;
            var pageIndex = "" == hidPageIndex.Value ? 1 : int.Parse(hidPageIndex.Value);
            var list = StatusInstance.FindPageList<TB_EquipmentStatusName>(pageIndex, PageSize, out totalRecords, null, "Name");
            var totalPages = totalRecords / PageSize + (totalRecords % PageSize > 0 ? 1 : 0);
            pageIndex = 0 == pageIndex ? totalPages : pageIndex;
            if (pageIndex >= totalPages)
            {
                pageIndex = totalPages;
                list = StatusInstance.FindPageList<TB_EquipmentStatusName>(pageIndex, PageSize, out totalRecords, null, "Name");
            }

            string html = "";
            if (totalRecords < 1)
            {
                html = "<tr><td colspan=\"9\">No records, you can change condition and try again, or " +
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
                        // 系统默认角色无法删除
                        "<td style=\"width: 40px; text-align: center;\"><input type=\"checkbox\" id=\"cb_" + id + "\" /></td>" +
                        "<td style=\"width: 40px; text-align: center;\">" + cnt + "</td>" +
                        "<td><a href=\"./equipment_status.aspx?key=" + id + "\" >" + ("" == obj.Name ? "-" : obj.Name) + "</a></td>" +
                        "<td>" + obj.Code + "</td>" +
                        "<td>" + (obj.IsInventory == true ? "Yes" : "-") + "</td>" +
                        "<td>" + (obj.IsOutstorage == true ? "Yes" : "-") + "</td>" +
                        "<td>" + (obj.IsOverhaul == true ? "Yes" : "-") + "</td>" +
                        "<td>" + (obj.IsWaiting == true ? "Yes" : "-") + "</td>" +
                        "<td></td>" +
                        "</tr>";
                }
            }
            tbodyBody.InnerHtml = html;
            divPagging.InnerHtml = "";
            if (totalRecords > 0)
                ShowPaggings(pageIndex, totalPages, totalRecords, "./equipment_status.aspx", divPagging);
        }

        protected void btQuery_Click(object sender, EventArgs e)
        {
            ShowEquipmentStatus();
        }
    }
}