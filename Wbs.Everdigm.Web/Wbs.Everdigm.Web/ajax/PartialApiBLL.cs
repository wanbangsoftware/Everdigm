using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wbs.Everdigm.BLL;
using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.Web.ajax
{
    /// <summary>
    /// BLL
    /// </summary>
    public partial class api
    {
        /// <summary>
        /// app版本查询
        /// </summary>
        private AppBLL AppInstance { get { return new AppBLL(); } }
        /// <summary>
        /// 历史记录
        /// </summary>
        protected HistoryBLL HistoryInstance { get { return new HistoryBLL(); } }
        /// <summary>
        /// 账户
        /// </summary>
        private AccountBLL AccountInstance { get { return new AccountBLL(); } }
        private TrackerBLL TrackerInstance { get { return new TrackerBLL(); } }
        private ActionBLL ActionInstance { get { return new ActionBLL(); } }
        /// <summary>
        /// 保存历史记录
        /// </summary>
        /// <param name="obj"></param>
        protected void SaveHistory(TB_AccountHistory obj)
        {
            obj.ActionTime = DateTime.Now;
            obj.Ip = Utility.GetClientIP(ctx);
            HistoryInstance.Add(obj);
        }
    }
}