using System;

namespace Wbs.Everdigm.Web.classes
{
    /// <summary>
    /// 设备详情页面的基类
    /// </summary>
    public class BaseEquipmentInfo : BaseEquipmentPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (!HasSessionLose)
            {
                if (!IsPostBack)
                {
                    //hidKey.Value = HttpUtility.UrlEncode(_key);
                }
            }
        }
    }
}