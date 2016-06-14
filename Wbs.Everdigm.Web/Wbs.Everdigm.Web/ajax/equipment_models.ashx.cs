using System.Linq;
using System.Web;

using Wbs.Everdigm.BLL;
using Wbs.Everdigm.Database;
using Wbs.Everdigm.Common;

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
        private void HandleEquipmentTypesRequest()
        {
            // 设备类型
            var types = new EquipmentTypeBLL().FindList(f => f.Delete == false).OrderBy(o => o.Name).ToList();
            // 设备型号
            var models = new EquipmentModelBLL().FindList(f => f.Delete == false).OrderBy(o => o.Type).ThenBy(t => t.Name).ToList();
            // 仓库
            var houses = new WarehouseBLL().FindList(f => f.Delete == false).OrderBy(o => o.Name).ToList();
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