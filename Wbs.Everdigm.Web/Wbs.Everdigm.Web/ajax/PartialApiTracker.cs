using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using Wbs.Everdigm.BLL;
using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.Web.ajax
{
    /// <summary>
    /// Tracker相关的处理
    /// </summary>
    public partial class api
    {
        private string TrackerNumberPrefix = "";

        /// <summary>
        /// 添加或查询相同device id的tracker
        /// </summary>
        /// <param name="device"></param>
        /// <returns>返回具有相同device id的tracker或新建一个tracker</returns>
        private TB_Tracker addTracker(string device, TrackerBLL bll)
        {
            if (string.IsNullOrEmpty(device)) return null;

            var tracker = bll.Find(f => f.DeviceId.Equals(device) && f.Deleted == false);
            if (null == tracker)
            {
                if (string.IsNullOrEmpty(TrackerNumberPrefix))
                {
                    TrackerNumberPrefix = ConfigurationManager.AppSettings["TRACKER_NUMBER_PREFIX"];
                }
                // 生成一个新的tracker并与当前账户绑定
                tracker = bll.FindList<TB_Tracker>(f => f.SimCard.StartsWith(TrackerNumberPrefix) && f.Deleted == false, "SimCard", true).FirstOrDefault();
                string number;
                if (null == tracker)
                {
                    number = TrackerNumberPrefix + "0000";
                }
                else
                {
                    var old = int.Parse(tracker.SimCard) + 1;
                    number = old.ToString();
                }
                tracker = bll.GetObject();
                tracker.SimCard = number;
                tracker.DeviceId = device;
                tracker = bll.Add(tracker);
                // 保存tracker绑定历史记录
                SaveHistory(new TB_AccountHistory()
                {
                    Account = null,
                    ActionId = new ActionBLL().Find(f => f.Name.Equals("AddNewTracker")).id,
                    ObjectA = string.Format("tracker: {0}, device: {1}", tracker.SimCard, tracker.DeviceId)
                });
                return tracker;
            }
            return tracker;
        }
    }
}