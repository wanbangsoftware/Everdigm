using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using Wbs.Everdigm.BLL;
using Wbs.Everdigm.Database;
using Wbs.Everdigm.Common;

namespace Wbs.Everdigm.Web.ajax
{
    /// <summary>
    /// 处理账户相关
    /// </summary>
    public partial class api
    {
        private void HandleAccountBinder(Api obj)
        {
            var acnt = ParseJson<Account>(obj.content);
            if (null == acnt) { ResponseData(-1, "Can not bind your account with error object."); }
            else if (string.IsNullOrEmpty(acnt.device)) { ResponseData(-1, "Can not bind your account with error parameter."); }
            {
                var name = acnt.name;
                if (name.Length >= 30)
                    name = name.Substring(0, 30);
                
                var pwd = acnt.md5.ToLower();
                try
                {
                    using (var bll = new AccountBLL())
                    {
                        var account = bll.Find(f => f.Code.Equals(name) && f.Delete == false);
                        if (null == account)
                        {
                            ResponseData(-1, "Your account is not exist");
                        }
                        else
                        {
                            if (!pwd.Equals(account.Password.ToLower()))
                            {
                                ResponseData(-1, "Your password is not correct.");
                            }
                            else if (account.Locked == true)
                            {
                                ResponseData(-1, "Your account was locked.");
                            }
                            else if ((int?)null != account.Tracker)
                            {
                                if (account.TB_Tracker.DeviceId.Equals(acnt.device))
                                {
                                    string uuid = Guid.NewGuid().ToString();
                                    // 每次绑定账户都生成一个新的session id
                                    bll.Update(u => u.id == account.id, act =>
                                    {
                                        act.DeviceLoginId = uuid;
                                        act.TB_Tracker.LastActionAt = DateTime.Now;
                                    });
                                    // 返回当前已经登录过的用户信息
                                    ResponseData(0, JsonConverter.ToJson(new Account()
                                    {
                                        name = acnt.name,
                                        data = account.TB_Tracker.SimCard,
                                        // 新的session id
                                        session = uuid
                                    }), true);
                                }
                                else
                                {
                                    ResponseData(-1, "Your account was bind with another device.");
                                }
                            }
                            else
                            {
                                // 创建一个新的tracker绑定关系
                                BindAccountWithTracker(account, acnt.device, bll);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    ResponseData(-1, string.Format("Can not hander your \\\"bind account\\\" request: {0}", e.Message));
                }
            }
        }

        private void BindAccountWithTracker(TB_Account account, string device, AccountBLL abll)
        {
            if (string.IsNullOrEmpty(device))
            {
                ResponseData(-1, "Cannot bind your device with error parameter.");
                return;
            }

            using (var bll = new TrackerBLL())
            {
                // 查找相同deviceid的tracker
                var tracker = bll.Find(f => f.Deleted == false && f.DeviceId.Equals(device));
                if (null != tracker)
                {
                    string uuid = Guid.NewGuid().ToString();
                    // 查找这个设备是否已经绑定到别人账户上
                    var cnt = tracker.TB_Account.Count;
                    if (cnt > 0)
                    {
                        Account exist = new Account()
                        {
                            name = account.Code,
                            data = account.TB_Tracker.SimCard,
                            // 每次绑定账户都生成新的session id
                            session = uuid
                        };
                        var user = tracker.TB_Account.FirstOrDefault(f => f.id == account.id);
                        if (null != user)
                        {
                            abll.Update(u => u.id == user.id, act => { act.DeviceLoginId = uuid; });
                            // 返回当前已经登录过的用户信息
                            ResponseData(0, JsonConverter.ToJson(exist), true);
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
                        abll.Update(f => f.id == account.id, act =>
                        {
                            act.Tracker = tracker.id;
                            act.DeviceLoginId = uuid;
                        });
                        // 保存tracker绑定历史记录
                        SaveHistory(new TB_AccountHistory()
                        {
                            Account = account.id,
                            ActionId = new ActionBLL().Find(f => f.Name.Equals("BindTracker")).id,
                            ObjectA = string.Format("tracker: {0}, device: {1}, account: {2}", tracker.SimCard, tracker.DeviceId, account.Code)
                        });
                        ResponseData(0, JsonConverter.ToJson(new Account()
                        {
                            name = account.Code,
                            data = tracker.SimCard,
                            // 每次绑定账户都生成新的session id
                            session = uuid
                        }), true);
                    }
                    // 更新在线时间
                    bll.Update(f => f.id == tracker.id, act => { act.LastActionAt = DateTime.Now; });
                }
                else
                {
                    tracker = addTracker(device, bll);
                    string uuid = Guid.NewGuid().ToString();
                    abll.Update(u => u.id == account.id, act => { act.DeviceLoginId = uuid; });
                    ResponseData(0, JsonConverter.ToJson(new Account()
                    {
                        name = account.Code,
                        data = tracker.SimCard,
                        // 每次绑定账户都生成新的session id
                        session = uuid
                    }), true);
                }
            }
        }
    }
}