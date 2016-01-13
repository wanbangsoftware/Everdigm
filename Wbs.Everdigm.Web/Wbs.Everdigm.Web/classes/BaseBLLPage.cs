using System;

using Wbs.Everdigm.BLL;
using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.Web
{
    /// <summary>
    /// 所有业务处理页面的基类
    /// </summary>
    public class BaseBLLPage : BasePage
    {
        /// <summary>
        /// 当前页面定义的cookie
        /// </summary>
        protected string _cookie_name_ = "";
        /// <summary>
        /// 当前登陆者的session信息
        /// </summary>
        protected TB_Account Account;
        /// <summary>
        /// 标记session是否丢失
        /// </summary>
        protected bool HasSessionLose = false;

        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            Account = Session[Utility.SessionName] as TB_Account;
            if (null == Account)
            {
                HasSessionLose = true;
            }
        }

        /// <summary>
        /// 用户信息业务处理
        /// </summary>
        protected AccountBLL AccountInstance { get { return new AccountBLL(); } }
        /// <summary>
        /// 历史记录业务处理类
        /// </summary>
        protected HistoryBLL HistoryInstance { get { return new HistoryBLL(); } }
        /// <summary>
        /// 历史记录类型
        /// </summary>
        protected ActionBLL ActionInstance { get { return new ActionBLL(); } }
        /// <summary>
        /// 保存历史记录
        /// </summary>
        /// <param name="obj"></param>
        protected void SaveHistory(TB_AccountHistory obj)
        {
            obj.ActionTime = DateTime.Now;
            if (obj.Account <= 0 || (int?)null == obj.Account)
                obj.Account = Account.id;
            obj.Ip = Utility.GetClientIP(this.Context);
            HistoryInstance.Add(obj);
        }
    }
}