using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.Web.main
{
    public partial class warehouse : BaseEquipmentPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            _cookie_name_ = "_warehouse_list_page_";
            cookieName.Value = _cookie_name_;
            base.Page_Load(sender, e);
            if (!HasSessionLose)
            {
                if (!IsPostBack)
                {
                    hidPageIndex.Value = null == Request.Cookies[_cookie_name_] ? "" : Request.Cookies[_cookie_name_].Value;
                    ShowWarehouses();

                    if (!string.IsNullOrEmpty(_key))
                    {
                        hidID.Value = _key;
                        // 显示编辑信息
                        showEdit();
                    }
                }
            }
        }

        private void showEdit()
        {
            var w = WarehouseInstance.Find(f => f.id == ParseInt(Utility.Decrypt(hidID.Value)));
            if (null != w)
            {
                txtAbbreviation.Value = w.Code;
                txtAddress.Value = w.Address;
                txtName.Value = w.Name;
            }
            else
            {
                ShowNotification("./warehouse.aspx", "Error: paramenter error, cannot edit the warehouse information.", false);
            }
        }
        private void BuildWarehouse(TB_Warehouse obj)
        {
            obj.Name = txtName.Value.Trim();
            obj.Code = txtAbbreviation.Value.Trim();
            obj.Address = txtAddress.Value.Trim();
        }
        /// <summary>
        /// 新建仓库信息
        /// </summary>
        private void NewWarehouse()
        {
            var w = WarehouseInstance.GetObject();
            BuildWarehouse(w);
            WarehouseInstance.Add(w);

            // 保存历史记录
            SaveHistory(new TB_AccountHistory
            {
                ActionId = ActionInstance.Find(f => f.Name.Equals("AddWarehouse")).id,
                ObjectA = "[id=" + w.id + "] " + w.Name + ", " + w.Code
            });

            ShowNotification("./warehouse.aspx", "You have added a new warehouse.");
        }

        private void EditWarehouse()
        {
            var w = WarehouseInstance.Find(f => f.id == ParseInt(Utility.Decrypt(hidID.Value)));
            if (null != w)
            {
                BuildWarehouse(w);
                Update(w);

                // 保存历史记录
                SaveHistory(new TB_AccountHistory
                {
                    ActionId = ActionInstance.Find(f => f.Name.Equals("EditWarehouse")).id,
                    ObjectA = "[id=" + w.id + "] " + w.Name + ", " + w.Code
                });

                ShowNotification("./warehouse.aspx", "You changed the warehouse information.");
            }
            else
            {
                ShowNotification("./warehouse.aspx", "Error: paramenter error, cannot edit the role information.", false);
            }
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            if (!HasSessionLose)
            {
                if ("" == hidID.Value)
                { NewWarehouse(); }
                else
                { EditWarehouse(); }
            }
        }
        /// <summary>
        /// 显示仓库列表
        /// </summary>
        private void ShowWarehouses()
        {
            var totalRecords = 0;
            var pageIndex = "" == hidPageIndex.Value ? 1 : int.Parse(hidPageIndex.Value);
            var list = WarehouseInstance.FindPageList<TB_Warehouse>(pageIndex, PageSize, out totalRecords,
                f => f.Delete == false, "Name");
            var totalPages = totalRecords / PageSize + (totalRecords % PageSize > 0 ? 1 : 0);
            pageIndex = 0 == pageIndex ? totalPages : pageIndex;
            if (pageIndex >= totalPages)
            {
                pageIndex = totalPages;
                list = WarehouseInstance.FindPageList<TB_Warehouse>(pageIndex, PageSize, out totalRecords,
                    f => f.Delete == false, "Name");
            }

            string html = "";
            if (totalRecords < 1)
            {
                html = "<tr><td colspan=\"5\">No records, you can change condition and try again, or " +
                    " <a href=\"./warehouse.aspx\">Add</a> some new one.</td></tr>";
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
                        "<td><a href=\"./warehouse.aspx?key=" + id + "\" >" + ("" == obj.Name ? "-" : obj.Name) + "</a></td>" +
                        "<td>" + obj.Code + "</td>" +
                        "<td>" + obj.Address + "</td>" +
                        "</tr>";
                }
            }
            tbodyBody.InnerHtml = html;
            divPagging.InnerHtml = "";
            if (totalRecords > 0)
                ShowPaggings(pageIndex, totalPages, totalRecords, "./warehouse.aspx", divPagging);
        }

        protected void btQuery_Click(object sender, EventArgs e)
        {
            ShowWarehouses();
        }
    }
}