﻿using System;

using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.Web.main
{
    public partial class equipment_type : BaseEquipmentPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            _cookie_name_ = "_equipment_type_list_page_";
            cookieName.Value = _cookie_name_;
            base.Page_Load(sender, e);
            if (!HasSessionLose)
            {
                if (!IsPostBack)
                {
                    hidPageIndex.Value = null == Request.Cookies[_cookie_name_] ? "" : Request.Cookies[_cookie_name_].Value;
                    ShowEquipmentTypes();

                    if (!string.IsNullOrEmpty(_key))
                    {
                        hidID.Value = _key;
                        // 显示编辑信息
                        ShowEdit();
                    }
                }
            }
        }

        private void ShowEdit()
        {
            var t = TypeInstance.Find(f => f.id == ParseInt(Utility.Decrypt(hidID.Value)));
            if (null != t)
            {
                txtCode.Value = t.Code;
                txtName.Value = t.Name;
                imgImage.Src = t.Image;
                hidImage.Value = t.Image;
                cbNormalVehicle.Checked = t.IsVehicle == true;
            }
            else
            {
                ShowNotification("./equipment_type.aspx", "Error: Cannot edit null object of <a>Equipment Type</a>.", false);
            }
        }

        private void BuildType(TB_EquipmentType obj)
        {
            obj.Code = txtCode.Value.Trim();
            obj.Name = txtName.Value.Trim();
            obj.Image = hidImage.Value;
            obj.IsVehicle = cbNormalVehicle.Checked;
        }

        private void NewType()
        {
            var t = TypeInstance.GetObject();
            BuildType(t);
            var check = TypeInstance.Find(f => f.Name.Equals(t.Name));
            if (null != check)
            {
                ShowNotification("./equipment_type.aspx", "Cannot add new Equipment type, The name of \"" + t.Name + "(" + t.Code + ")\" is exist.", false);
                return;
            }
            else
            {
                TypeInstance.Add(t);

                // 保存历史记录
                SaveHistory(new TB_AccountHistory
                {
                    ActionId = ActionInstance.Find(f => f.Name.Equals("AddEquipmentType")).id,
                    ObjectA = "[id=" + t.id + "] " + t.Name + ", " + t.Code
                });

                ShowNotification("./equipment_type.aspx", "You have added a new Equipment type: " + t.Name + "(" + t.Code + ").");
            }
        }

        private void EditType()
        {
            var t = TypeInstance.Find(f => f.id == ParseInt(Utility.Decrypt(hidID.Value)));
            if (null != t)
            {
                BuildType(t);
                Update(t);

                // 保存历史记录
                SaveHistory(new TB_AccountHistory()
                {
                    ActionId = ActionInstance.Find(f => f.Name.Equals("EditEquipmentType")).id,
                    ObjectA = "[id=" + t.id + "] " + t.Name + ", " + t.Code
                });

                ShowNotification("./equipment_type.aspx", "You changed the equipment type.");
            }
            else
            {
                ShowNotification("./equipment_type.aspx", "Error: paramenter error, cannot edit null object(equipment type).", false);
            }
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            if (!HasSessionLose)
            {
                if ("" == hidID.Value)
                { NewType(); }
                else
                { EditType(); }
            }
        }
        /// <summary>
        /// 显示设备类别列表
        /// </summary>
        private void ShowEquipmentTypes()
        {
            var totalRecords = 0;
            var pageIndex = "" == hidPageIndex.Value ? 1 : int.Parse(hidPageIndex.Value);
            var list = TypeInstance.FindPageList<TB_EquipmentType>(pageIndex, PageSize, out totalRecords,
                f => f.Delete == false, "Name");
            var totalPages = totalRecords / PageSize + (totalRecords % PageSize > 0 ? 1 : 0);

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
                        "<td><a href=\"./equipment_type.aspx?key=" + id + "\" >" + ("" == obj.Name ? "-" : obj.Name) + "</a></td>" +
                        "<td>" + obj.Code + "</td>" +
                        "<td>" + (obj.IsVehicle.Value ? "Yes" : "-") + "</td>" +
                        "<td><img alt=\"clipart\" style=\"width: 41px;\" class=\"img-rounded\" src=\"" + obj.Image + "\"/></td>" +
                        "<td></td>" +
                        "</tr>";
                }
            }
            tbodyBody.InnerHtml = html;
            divPagging.InnerHtml = "";
            if (totalRecords > 0)
                ShowPaggings(pageIndex, totalPages, totalRecords, "./equipment_type.aspx", divPagging);
        }

        protected void btQuery_Click(object sender, EventArgs e)
        {
            ShowEquipmentTypes();
        }
    }
}