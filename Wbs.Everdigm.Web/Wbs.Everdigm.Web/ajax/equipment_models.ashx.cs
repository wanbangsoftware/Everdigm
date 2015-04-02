using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Wbs.Everdigm.BLL;
using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.Web.ajax
{
    /// <summary>
    /// 设备类型
    /// </summary>
    public class equipment_models : BaseHttpHandler
    {

        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);
            HandleEquipmentTypesRequest();
        }
        /// <summary>
        /// 设备型号业务
        /// </summary>
        private EquipmentModelBLL ModelInstance { get { return new EquipmentModelBLL(); } }
        /// <summary>
        /// 设备类别业务
        /// </summary>
        private EquipmentTypeBLL TypeInstance { get { return new EquipmentTypeBLL(); } }
        /// <summary>
        /// 仓库列表
        /// </summary>
        private WarehouseBLL WarehouseInstance { get { return new WarehouseBLL(); } }

        private void HandleEquipmentTypesRequest()
        {
            // 设备类型
            var types = TypeInstance.FindList(f => f.Delete == false).OrderBy(o => o.Name).ToList();
            // 设备型号
            var models = ModelInstance.FindList(f => f.Delete == false).OrderBy(o => o.Type).ThenBy(t => t.Name).ToList();
            // 仓库
            var houses = WarehouseInstance.FindList(f => f.Delete == false).OrderBy(o => o.Name).ToList();
            // 设备状态类别
            var statuses = new EquipmentStatusBLL().FindList(null).OrderBy(o => o.Name).ToList();
            // 设备状态码
            //var codes = new EquipmentStatusCodeBLL().FindList(null).OrderBy(o => o.Status).ThenBy(t => t.Name).ToList();

            var ret = "{\"types\":" + JsonConverter.ToJson(types) + ",\"models\":" +
                JsonConverter.ToJson(models) + ",\"warehouses\":" + JsonConverter.ToJson(houses) +
                ",\"statuses\":" + JsonConverter.ToJson(statuses) + "}";
            ResponseJson(ret);
        }
    }
}