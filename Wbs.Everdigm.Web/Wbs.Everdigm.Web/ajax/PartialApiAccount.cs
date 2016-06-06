using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wbs.Everdigm.BLL;
using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.Web.ajax
{
    /// <summary>
    /// 处理账户相关
    /// </summary>
    public partial class api
    {
        private void HandleAccountBinder()
        {
            var name = GetParamenter("account");
            if (name.Length >= 30)
                name = name.Substring(0, 30);

            var device = GetParamenter("device");
            var pwd = GetParamenter("password").ToLower();
            try
            {
                var account = AccountInstance.Find(name, pwd);
                if (null == account)
                {
                    ResponseData(-1, "Your account is not exist");
                }
                else
                {
                    if (account.Locked == true)
                    {
                        ResponseData(-1, "Your account was locked.");
                    }
                    else if ((int?)null != account.Tracker)
                    {
                        if (account.TB_Tracker.DeviceId.Equals(device))
                        {
                            ResponseData(-1, "Your account is already bind with this device.");
                        }
                        else
                        {
                            ResponseData(-1, "Your account was bind with another device.");
                        }
                    }
                    else
                    {
                        // 创建一个新的tracker绑定关系
                        BindAccountWithTracker(account);
                    }
                }
            }
            catch (Exception e)
            {
                ResponseData(-1, string.Format("Can not hander your \\\"bind account\\\" request: {0}", e.Message));
            }
        }

        private void BindAccountWithTracker(TB_Account account)
        {
            var deviceId = GetParamenter("device");
            // 查找相同deviceid的tracker
            var tracker = TrackerInstance.Find(f => f.Deleted == false && f.DeviceId.Equals(deviceId));
            if (null != tracker)
            {
                // 查找这个设备是否已经绑定到别人账户上
                var cnt = tracker.TB_Account.Count;
                if (cnt > 0)
                {
                    var user = tracker.TB_Account.FirstOrDefault(f => f.id == account.id);
                    if (null != user)
                    {
                        ResponseData(-1, "This device is already bound with your tms account.");
                    }
                    else
                    {
                        ResponseData(-1, "Your device is already bound with another tms account.");
                    }
                }
                else
                {
                    // 该设备还未绑定任何人员
                    // 更改account的tracker信息
                    AccountInstance.Update(f => f.id == account.id, act =>
                    {
                        act.Tracker = tracker.id;
                    });
                    // 保存tracker绑定历史记录
                    SaveHistory(new TB_AccountHistory()
                    {
                        Account = account.id,
                        ActionId = ActionInstance.Find(f => f.Name.Equals("BindTracker")).id,
                        ObjectA = string.Format("tracker: {0}, device: {1}, account: {2}", tracker.SimCard, tracker.DeviceId, account.Code)
                    });
                    ResponseData(0, tracker.SimCard);
                }
            }
            else {
                // 生成一个新的tracker并与当前账户绑定

            }
        }
    }
}