using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace Wbs.Everdigm.Web.ajax
{
    /// <summary>
    /// Ajax 处理基类
    /// </summary>
    public class BaseHttpHandler : IHttpHandler, IRequiresSessionState
    {
        /// <summary>
        /// 当前 HttpContext 实例
        /// </summary>
        protected HttpContext ctx = null;
        /// <summary>
        /// 客户端要求的查询类型
        /// </summary>
        protected string type = "";
        /// <summary>
        /// 客户端要求的命令字
        /// </summary>
        protected string cmd = "";
        /// <summary>
        /// 客户端要求的命令字的详细内容
        /// </summary>
        protected string data = "";
        /// <summary>
        /// 处理客户端过来的请求
        /// </summary>
        /// <param name="context"></param>
        public virtual void ProcessRequest(HttpContext context)
        {
            ctx = context;
            context.Response.ContentType = "application/json;charset=utf-8";
            // 是否需要跨域访问？
            //context.Response.AddHeader("Access-Control-Allow-Origin", "*");

            HandlerParamenters();
        }
        /// <summary>
        /// 处理参数列表
        /// </summary>
        protected virtual void HandlerParamenters()
        {
            type = GetParamenter("type");
            cmd = GetParamenter("cmd");
            data = GetParamenter("data");
        }

        /// <summary>
        /// 获取客户端亲求中的 param 值
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        protected string GetParamenter(string param)
        {
            return ctx.Request.Params[param];
        }
        /// <summary>
        /// 将字符串转换成对应的int值
        /// </summary>
        /// <param name="value">需要转换成int的字符串</param>
        /// <returns>转换失败时返回-1</returns>
        protected int ParseInt(string value)
        {
            var i = 0;
            if (!int.TryParse(value, out i))
                i = -1;
            return i;
        }
        /// <summary>
        /// 返回JSON结果
        /// </summary>
        /// <param name="context"></param>
        /// <param name="responseString"></param>
        protected void ResponseJson(string responseString)
        {
            // 返回结果
            ctx.Response.Clear();
            ctx.Response.Write(responseString);
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}