using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Wbs.Everdigm.Web.ajax
{
    /// <summary>
    /// 提供Android手机app访问服务器的接口
    /// </summary>
    public partial class api : BaseHttpHandler
    {
        /// <summary>
        /// 提供手机端访问服务器的api
        /// </summary>
        /// <param name="context"></param>
        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);
            HandleRequest();
        }

        /// <summary>
        /// 返回给app端json对象
        /// </summary>
        /// <param name="state">服务器处理结果，0=正常，其他=错误</param>
        /// <param name="data">需要返回的数据内容</param>
        /// <param name="json">data字段是否为json对象</param>
        private void ResponseData(int state, string data, bool json = false)
        {
            ResponseJson(string.Format("{0}\"State\":{1},\"Data\":{2}{3}{4}{5}", "{", state, (json ? "" : "\""), data, (json ? "" : "\""), "}"));
        }
        private string FormatJsonObject(string data)
        {
            return string.Format("{0}{1}{2}", "{", data, "}");
        }
        /// <summary>
        /// 处理app的请求
        /// </summary>
        private void HandleRequest()
        {
            switch (cmd)
            {
                case "GetParameter":
                    // app端获取mqtt服务地址
                    ResponseData(0, ConfigurationManager.AppSettings["MQTT_SERVICE_ADDRESS"]);
                    break;
                case "CheckUpdate":
                    // 检测应用更新升级
                    HandleCheckUpdate();
                    break;
                case "BindAccount":
                    // 绑定账号
                    HandleAccountBinder();
                    break;
            }
        }
    }
}