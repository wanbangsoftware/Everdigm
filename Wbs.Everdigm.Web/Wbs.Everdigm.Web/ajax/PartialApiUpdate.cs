using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wbs.Everdigm.Common;

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
        private void HandleCheckUpdate()
        {
            try
            {
                var app = AppInstance.Find(f => f.Useable == true);
                if (null == app) app = AppInstance.GetObject();
                ResponseData(0, JsonConverter.ToJson(app), true);
            }
            catch (Exception e)
            {
                ResponseData(-1, string.Format("Can not handle your [update] request: {0}", e.Message));
            }
        }
    }
}