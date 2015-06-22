using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wbs.Everdigm.BLL;

namespace Wbs.Everdigm.Web
{
    /// <summary>
    /// 基本的Tracker页面
    /// </summary>
    public class BaseTrackerPage : BaseBLLPage
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
        /// TX10G
        /// </summary>
        protected TrackerBLL TrackerInstance { get { return new TrackerBLL(); } }

        protected TrackerPositionBLL TrackerPositionInstance { get { return new TrackerPositionBLL(); } }

        protected WorkBLL WorkInstance { get { return new WorkBLL(); } }
        protected WorkDetailBLL WorkDetailInstance { get { return new WorkDetailBLL(); } }
        protected EquipmentBLL EquipmentInstance { get { return new EquipmentBLL(); } }
        protected TerminalBLL TerminalInstance { get { return new TerminalBLL(); } }
    }
}