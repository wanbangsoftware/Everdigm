using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Linq.Expressions;
using Wbs.Everdigm.Common;
using Wbs.Everdigm.BLL;
using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.Web.ajax
{
    /// <summary>
    /// 处理update相关请求
    /// </summary>
    public partial class api
    {
        private static string MQTT_SERVER = ConfigurationManager.AppSettings["MQTT_SERVICE_ADDRESS"];
        /// <summary>
        /// 处理app端检查更新的请求
        /// </summary>
        private void HandleCheckUpdate(Api apiObj)
        {
            try
            {
                var obj = JsonConverter.ToObject<TB_Application>(apiObj.content);
                if (null != obj)
                {
                    using (var bll = new AppBLL())
                    {
                        Expression<Func<TB_Application, bool>> exp = PredicateExtensions.True<TB_Application>();
                        exp = exp.And(a => a.Useable == true);
                        exp.And(a => a.VersionCode > obj.VersionCode || a.VersionName.CompareTo(obj.VersionName) > 0 ||
                            a.InternalVersion > obj.InternalVersion);
                        var app = bll.Find(exp);
                        if (null == app)
                        {
                            ResponseData(-2, "No new version exist.");
                        }
                        else
                        {
                            ResponseData(0, JsonConverter.ToJson(app), true);
                        }
                    }
                }
                else { ResponseData(-1, "Can not hander your [update] request with error object."); }
            }
            catch (Exception e)
            {
                ResponseData(-1, string.Format("Can not handle your [update] request: {0}", e.Message));
            }
        }

        private void HandleGetParameter(Api obj)
        {
            try
            {
                var acnt = ParseJson<Account>(obj.content);
                if (null != acnt)
                {
                    Account resp = new Account();
                    resp.md5 = MQTT_SERVER;

                    var device = acnt.device;
                    using (var bll = new TrackerBLL())
                    {
                        var tracker = bll.Find(f => f.DeviceId.Equals(device) && f.Deleted == false);
                        if (null == tracker)
                        {
                            tracker = addTracker(acnt.device, bll);
                            resp.data = tracker.SimCard;
                            // 新登记的tracker没有session
                            resp.session = "";
                        }
                        else
                        {
                            // 查找绑定用户的登录session
                            var user = tracker.TB_Account.FirstOrDefault();
                            if (null == user)
                            {
                                // 没有绑定用户时session为空
                                resp.session = "";
                                resp.device = "";
                            }
                            else
                            {
                                resp.name = user.Code;
                                // 已绑定过的用户返回session
                                resp.session = user.DeviceLoginId;
                                // 返回用户所在区域
                                resp.device = user.Belong;
                            }
                            resp.data = tracker.SimCard;
                            bll.Update(f => f.id == tracker.id, act => { act.LastActionAt = DateTime.Now; });
                        }

                        // get parameter的时候只返回设备与sim卡之间的绑定关系，没有账户的信息
                        ResponseData(0, JsonConverter.ToJson(resp), true);
                    }
                }
                else { ResponseData(-1, "Can not handle your get_parameter request with error data."); }
            }
            catch (Exception e) { ResponseData(-1, string.Format("Can not handle your get_parameter request: {0}", e.Message)); }
        }
    }
}