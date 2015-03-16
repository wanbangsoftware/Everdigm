using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Wbs.Everdigm.BLL;
using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.Web
{
    /// <summary>
    /// 终端信息处理页面基类
    /// </summary>
    public class BaseTerminalPage : BaseBLLPage
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
        /// 终端信息业务处理实例
        /// </summary>
        protected TerminalBLL TerminalInstance { get { return new TerminalBLL(); } }
        /// <summary>
        /// 更新终端的信息
        /// </summary>
        /// <param name="obj"></param>
        protected void Update(TB_Terminal obj)
        {
            TerminalInstance.Update(f => f.id == obj.id, act =>
            {
                act.Delete = obj.Delete;
                act.Firmware = obj.Firmware;
                act.HasBound = obj.HasBound;
                act.Number = obj.Number;
                act.Revision = obj.Revision;
                act.Satellite = obj.Satellite;
                act.Sim = obj.Sim;
                act.Type = obj.Type;
            });
        }
    }
}