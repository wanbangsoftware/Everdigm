using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.UI.WebControls;
using Wbs.Everdigm.BLL;
using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.Web
{
    public class BaseEquipmentPage : BaseBLLPage
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
        /// 仓库信息业务处理实体
        /// </summary>
        protected WarehouseBLL WarehouseInstance { get { return new WarehouseBLL(); } }
        /// <summary>
        /// 设备类别业务处理实体
        /// </summary>
        protected EquipmentTypeBLL TypeInstance { get { return new EquipmentTypeBLL(); } }
        /// <summary>
        /// 设备型号业务处理实体
        /// </summary>
        protected EquipmentModelBLL ModelInstance { get { return new EquipmentModelBLL(); } }
        /// <summary>
        /// 设备状态业务处理实体
        /// </summary>
        protected EquipmentStatusBLL StatusInstance { get { return new EquipmentStatusBLL(); } }
        /// <summary>
        /// 设备状态码业务处理实体
        /// </summary>
        protected EquipmentStatusCodeBLL CodeInstance { get { return new EquipmentStatusCodeBLL(); } }
        /// <summary>
        /// 设备信息业务处理实体
        /// </summary>
        protected EquipmentBLL EquipmentInstance { get { return new EquipmentBLL(); } }
        /// <summary>
        /// 出入库历史记录
        /// </summary>
        protected StoreHistoryBLL StoreInstance { get { return new StoreHistoryBLL(); } }
        /// <summary>
        /// 终端业务处理实体
        /// </summary>
        protected TerminalBLL TerminalInstance { get { return new TerminalBLL(); } }

        /// <summary>
        /// 更新仓库信息
        /// </summary>
        /// <param name="obj"></param>
        protected void Update(TB_Warehouse obj)
        {
            WarehouseInstance.Update(f => f.id == obj.id, act =>
            {
                act.Code = obj.Code;
                act.Address = obj.Address;
                act.Name = obj.Name;
            });
        }
        /// <summary>
        /// 更新设备类别
        /// </summary>
        /// <param name="obj"></param>
        protected void Update(TB_EquipmentType obj)
        {
            TypeInstance.Update(f => f.id == obj.id, act => {
                act.Code = obj.Code;
                act.Name = obj.Name;
            });
        }
        /// <summary>
        /// 更新设备型号信息
        /// </summary>
        /// <param name="obj"></param>
        protected void Update(TB_EquipmentModel obj)
        {
            ModelInstance.Update(f => f.id == obj.id, act => {
                act.Code = obj.Code;
                act.Name = obj.Name;
                act.Type = obj.Type;
            });
        }
        /// <summary>
        /// 更新设备状态信息
        /// </summary>
        /// <param name="obj"></param>
        protected void Update(TB_EquipmentStatusName obj)
        {
            StatusInstance.Update(f => f.id == obj.id, act => {
                act.Code = obj.Code;
                act.Name = obj.Name;
                act.IsInventory = obj.IsInventory;
                act.IsOutstorage = obj.IsOutstorage;
                act.IsOverhaul = obj.IsOverhaul;
                act.IsWaiting = obj.IsWaiting;
            });
        }
        /// <summary>
        /// 更新设备状态码信息
        /// </summary>
        /// <param name="obj"></param>
        protected void Update(TB_EquipmentStatusCode obj)
        {
            CodeInstance.Update(f => f.id == obj.id, act => {
                act.Code = obj.Code;
                act.Name = obj.Name;
                act.Status = obj.Status;
            });
        }
        /// <summary>
        /// 在下拉列表框里显示设备类别列表
        /// </summary>
        /// <param name="ddl"></param>
        protected void ShowEquipmentTypes(DropDownList ddl)
        {
            var types = TypeInstance.FindList(null).OrderBy(o => o.Name);
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem() { Text = "Type:", Value = "", Selected = true });
            foreach (var type in types)
            {
                ddl.Items.Add(new ListItem() { Text = type.Name, Value = type.id.ToString() });
            }
        }
    }
}