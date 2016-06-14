using System;
using System.Web;
using Wbs.Everdigm.BLL;
using Wbs.Everdigm.Database;

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
        /// <summary>
        /// 保存历史记录
        /// </summary>
        /// <param name="obj"></param>
        private void SaveHistory(TB_AccountHistory obj)
        {
            obj.ActionTime = DateTime.Now;
            obj.Ip = Utility.GetClientIP(ctx);
            new HistoryBLL().Add(obj);
        }
        /// <summary>
        /// 处理app的请求
        /// </summary>
        private void HandleRequest()
        {
            var apiObject = ParseJson<Api>(requestedContent);
            if (null == apiObject)
            {
                ResponseData(-1, "Can not hander your request with error object.");
            }
            else
            {
                switch (apiObject.cmd)
                {
                    case "GetParameter":
                        // app端获取mqtt服务地址
                        HandleGetParameter(apiObject);
                        break;
                    case "CheckUpdate":
                        HandleCheckUpdate(apiObject);
                        break;
                    case "BindAccount":
                        HandleAccountBinder(apiObject);
                        break;
                    case "Report":
                        // 汇报数据
                        HandleReportData(apiObject);
                        break;
                    default:
                        ResponseData(-1, string.Format("Can not handle your request command: {0}", apiObject.cmd));
                        break;
                }
            }
        }
    }
}