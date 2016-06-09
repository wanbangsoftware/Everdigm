using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
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
        private TB_Tracker addTracker(string device)
        {
            if (string.IsNullOrEmpty(device)) return null;

            var tracker = TrackerInstance.Find(f => f.DeviceId.Equals(device) && f.Deleted == false);
            if (null == tracker)
            {
                if (string.IsNullOrEmpty(TrackerNumberPrefix))
                {
                    TrackerNumberPrefix = ConfigurationManager.AppSettings["TRACKER_NUMBER_PREFIX"];
                }
                // 生成一个新的tracker并与当前账户绑定
                tracker = TrackerInstance.Find(f => f.SimCard.StartsWith(TrackerNumberPrefix) && f.Deleted == false);
                string number;
                if (null == tracker) { number = TrackerNumberPrefix + "0000"; }
                else
                {
                    var old = int.Parse(tracker.SimCard) + 1;
                    number = old.ToString();
                }
                tracker = TrackerInstance.GetObject();
                tracker.SimCard = number;
                tracker.DeviceId = device;
                tracker = TrackerInstance.Add(tracker);
                // 保存tracker绑定历史记录
                SaveHistory(new TB_AccountHistory()
                {
                    Account = null,
                    ActionId = ActionInstance.Find(f => f.Name.Equals("AddNewTracker")).id,
                    ObjectA = string.Format("tracker: {0}, device: {1}", tracker.SimCard, tracker.DeviceId)
                });
                return tracker;
            }
            return tracker;
        }
    }
}