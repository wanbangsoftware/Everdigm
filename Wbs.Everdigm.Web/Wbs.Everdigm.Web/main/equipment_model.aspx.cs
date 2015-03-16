using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.Web.main
{
    public partial class equipment_model : BaseEquipmentPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            _cookie_name_ = "_equipment_model_list_page_";
            cookieName.Value = _cookie_name_;
            base.Page_Load(sender, e);
            if (!HasSessionLose)
            {
                if (!IsPostBack)
                {
                    hidPageIndex.Value = null == Request.Cookies[_cookie_name_] ? "" : Request.Cookies[_cookie_name_].Value;
                    ShowEquipmentModels();
                    ShowTypes();
                    if (!string.IsNullOrEmpty(_key))
                    {
                        hidID.Value = _key;
                        // 显示编辑信息
                        ShowEdit();
                    }
                }
            }
        }

        private void ShowTypes()
        {
            ddlType.Items.Clear();
            ddlType.Items.Add(new ListItem { Text = "Equipment Type:", Value = "" });
            var types = TypeInstance.FindList(null).OrderBy(o=>o.Name);
            if (types.Count() > 0)
            {
                types = types.OrderBy(o => o.Name);
                foreach (var type in types)
                {
                    ddlType.Items.Add(new ListItem
                    {
                        Text = type.Name,
                        Value = type.id.ToString()
                    });
                }
            }
        }

        private void ShowEdit()
        {
            var m = ModelInstance.Find(f => f.id == ParseInt(Utility.Decrypt(hidID.Value)));
            if (null != m)
            {
                txtCode.Value = m.Code;
                txtName.Value = m.Name;
                ddlType.SelectedValue = m.Type.ToString();
            }
            else
            {
                ShowNotification("./equipment_model.aspx", "Error: Cannot edit null object of <a>Equipment Model</a>.", false);
            }
        }

        private void BuildModel(TB_EquipmentModel obj)
        {
            obj.Code = txtCode.Value.Trim();
            obj.Name = txtName.Value.Trim();
            obj.Type = ParseInt(ddlType.SelectedValue);
            if (obj.Type <= 0)
                obj.Type = (int?)null;
        }

        private void NewModel()
        {
            var m = ModelInstance.GetObject();
            BuildModel(m);
            ModelInstance.Add(m);

            // 保存历史记录
            SaveHistory(new TB_AccountHistory
            {
                ActionId = ActionInstance.Find(f => f.Name.Equals("AddEquipmentModel")).id,
                ObjectA = "[id=" + m.id + "] " + m.Name + ", " + m.Code
            });

            ShowNotification("./equipment_model.aspx", "You have added a new Equipment model: " + m.Name + "(" + m.Code + ").");
        }

        private void EditModel()
        {
            var m = ModelInstance.Find(f => f.id == ParseInt(Utility.Decrypt(hidID.Value)));
            if (null != m)
            {
                BuildModel(m);
                Update(m);

                // 保存历史记录
                SaveHistory(new TB_AccountHistory
                {
                    ActionId = ActionInstance.Find(f => f.Name.Equals("EditEquipmentModel")).id,
                    ObjectA = "[id=" + m.id + "] " + m.Name + ", " + m.Code
                });

                ShowNotification("./equipment_model.aspx", "You changed the equipment model.");
            }
            else
            {
                ShowNotification("./equipment_model.aspx", "Error: paramenter error, cannot edit null object(equipment model).", false);
            }
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            if (!HasSessionLose)
            {
                if ("" == hidID.Value)
                { NewModel(); }
                else
                { EditModel(); }
            }
        }

        private void ShowEquipmentModels()
        {
            var totalRecords = 0;
            var pageIndex = "" == hidPageIndex.Value ? 1 : int.Parse(hidPageIndex.Value);
            var list = ModelInstance.FindPageList<TB_EquipmentType>(pageIndex, PageSize, out totalRecords,
                f => f.Delete == false, "Type,Name");
            var totalPages = totalRecords / PageSize + (totalRecords % PageSize > 0 ? 1 : 0);
            pageIndex = 0 == pageIndex ? totalPages : pageIndex;
            if (pageIndex >= totalPages)
            {
                pageIndex = totalPages;
                list = ModelInstance.FindPageList<TB_EquipmentType>(pageIndex, PageSize, out totalRecords,
                    f => f.Delete == false, "Type,Name");
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
                        "<td>" + ((int?)null == obj.Type ? "-" : obj.TB_EquipmentType.Name) + "</td>" +
                        "<td><a href=\"./equipment_model.aspx?key=" + id + "\" >" + ("" == obj.Name ? "-" : obj.Name) + "</a></td>" +
                        "<td>" + obj.Code + "</td>" +
                        "<td></td>" +
                        "</tr>";
                }
            }
            tbodyBody.InnerHtml = html;
            divPagging.InnerHtml = "";
            if (totalRecords > 0)
                ShowPaggings(pageIndex, totalPages, totalRecords, "./equipment_model.aspx", divPagging);
        }

        protected void btQuery_Click(object sender, EventArgs e)
        {
            ShowEquipmentModels();
        }
    }
}