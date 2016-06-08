using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;
using Wbs.Everdigm.Common;
using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.Web.ajax
{
    /// <summary>
    /// 处理update相关请求
    /// </summary>
    public partial class api
    {
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
                    Expression<Func<TB_Application, bool>> exp = PredicateExtensions.True<TB_Application>();
                    exp = exp.And(a => a.Useable == true);
                    exp.And(a => a.VersionCode > obj.VersionCode || a.VersionName.CompareTo(obj.VersionName) > 0 || 
                        a.InternalVersion > obj.InternalVersion);
                    var app = AppInstance.Find(exp);
                    if (null == app)
                    {
                        ResponseData(-2, "No new version exist.");
                    }
                    else
                    {
                        ResponseData(0, JsonConverter.ToJson(app), true);
                    }
                }
                else { ResponseData(-1, "Can not hander your [update] request with error object."); }
            }
            catch (Exception e)
            {
                ResponseData(-1, string.Format("Can not handle your [update] request: {0}", e.Message));
            }
        }
    }
}