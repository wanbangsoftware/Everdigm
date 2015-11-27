using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.SessionState;
using Wbs.Everdigm.Database;

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
        /// 客户端要求的命令发送方式
        /// </summary>
        private string force = "";
        /// <summary>
        /// 命令的发送方式
        /// </summary>
        protected ForceType forceType = ForceType.AutoDetect;
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
            force = GetParamenter("by");
            if (!string.IsNullOrEmpty(force) && force.Equals("sms"))
            {
                // 坚持客户端的命令发送方式要求，默认为自动检测方式
                forceType = ForceType.SMS;
            }
            data = GetParamenter("data");
        }

        //public void SaveHistory(string str)
        //{
        //    string filename = DateTime.Now.ToString("yyyyMMdd");
        //    var path = ctx.Server.MapPath("~/") + "exceptions/";
        //    if (!Directory.Exists(path))
        //        Directory.CreateDirectory(path);
        //    FileStream fs = new FileStream((path + "" + filename + ".txt"), FileMode.Append);
        //    //创建FileSteam类,参数为路径\打开文件方式\对文件进行什么样的操作
        //    StreamWriter ww = new StreamWriter(fs, System.Text.Encoding.Default);
        //    ww.WriteLine("\r\n**************" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "**************");
        //    ww.Write(str);
        //    ww.WriteLine("\r\n**************End**************");
        //    ww.Close();
        //    fs.Close();
        //}

        /// <summary>
        /// 当前登陆者的信息
        /// </summary>
        protected TB_Account User { get { return ctx.Session[Utility.SessionName] as TB_Account; } }
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
    /// <summary>
    /// 命令发送方式
    /// </summary>
    public enum ForceType
    {
        /// <summary>
        /// 自动检测方式发送命令
        /// </summary>
        AutoDetect,
        /// <summary>
        /// 强制SMS发送
        /// </summary>
        SMS
    }
}