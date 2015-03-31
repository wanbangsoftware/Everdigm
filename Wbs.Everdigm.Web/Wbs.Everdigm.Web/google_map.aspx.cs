using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Wbs.Everdigm.BLL;

namespace Wbs.Everdigm.Web
{
    /// <summary>
    /// 调用google的逆向地址解析服务来获取地址并保存
    /// </summary>
    public partial class google_map : BasePage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (!IsPostBack)
            {
                ShowGpsInfo();
            }
        }
        /// <summary>
        /// 设备信息
        /// </summary>
        private EquipmentBLL EquipmentInstance { get { return new EquipmentBLL(); } }
        /// <summary>
        /// 位置信息业务处理
        /// </summary>
        private PositionBLL PositionInstance { get { return new PositionBLL(); } }
        /// <summary>
        /// 获取未处理的GPS信息
        /// </summary>
        private void ShowGpsInfo()
        {
            var id = ParseInt(_key);
            if (id < 1)
                return;

            var pos = PositionInstance.Find(f => f.id == id && f.Updated == 0);
            if (null != pos)
            {
                hidPosId.Value = id.ToString();
                hidLatLng.Value = pos.Latitude.ToString() + "," + pos.Longitude.ToString();
                hidAddress.Value = "";
                PositionInstance.Update(f => f.id == pos.id, act => { act.Updated = 1; });
            }
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            // 保存历史
            var id = int.Parse(hidPosId.Value);
            if (id > 0)
            {
                if (!string.IsNullOrEmpty(hidAddress.Value))
                {
                    // 地址不为空时更新成功
                    var pos = PositionInstance.Find(f => f.id == id);
                    PositionInstance.Update(f => f.id == id && f.Updated == 1, act =>
                    {
                        act.Address = hidAddress.Value;
                        act.Updated = 2;
                    });
                    // 更新设备的最新定位信息
                    if (pos.Equipment != (int?)null)
                    {
                        var EquipmentInstance = new EquipmentBLL();
                        EquipmentInstance.Update(f => f.id == pos.Equipment, act =>
                        {
                            act.GpsAddress = hidAddress.Value;
                            act.GpsUpdated = true;
                        });
                    }
                }
                else
                {
                    // 地址不为空时更新失败，其他时候会将其更新成待更新状态
                    PositionInstance.Update(f => f.id == id && f.Updated == 1, act =>
                    {
                        act.Updated = 3;
                    });
                }
                hidPosId.Value = "0";
            }
        }
    }
}