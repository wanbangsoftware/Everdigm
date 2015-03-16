using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

using Wbs.Everdigm.Common;
using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.Web.ajax
{
    /// <summary>
    /// command 的摘要说明
    /// </summary>
    public partial class command : BaseHttpHandler
    {

        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);
            HandleRequest();
        }
        private static string resp = "{\"status\":%d,\"desc\":\"%s\"}";
        /// <summary>
        /// 返回处理状态以及处理结果
        /// </summary>
        /// <param name="status"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private string ResponseMessage(int status, string msg)
        {
            return string.Format(resp, status, msg);
        }
        /// <summary>
        /// 处理命令发送相关的请求
        /// </summary>
        private void HandleRequest()
        {
            var ret = "";
            switch (type)
            {
                case "command": 
                    // 发送命令并返回命令发送记录的id以供后续查询状态
                    ret = HandleCommandRequest();
                    break;
                case "query": 
                    // 查询命令发送状态
                    ret = HandleQueryRequest();
                    break;
                default:
                    ret = ResponseMessage(-1, "Unknown command type.");
                    break;
            }
            ResponseJson(ret);
        }
        /// <summary>
        /// 处理发送命令的请求
        /// </summary>
        /// <returns></returns>
        private string HandleCommandRequest()
        {
            var ret = "[]";
            try
            {
                var id = ParseInt(Utility.Decrypt(data));
                var obj = EquipmentInstance.Find(f => f.id == id);
                if (null != obj)
                {
                    if ((int?)null != obj.Terminal)
                    {
                        // 查看当前设备的链接状态然后确定命令的发送方式
                    }
                    else { ret = ResponseMessage(-1, "No terminal bond with equipment."); }
                }
                else { ret = ResponseMessage(-1, "Equipment is not exist."); }
            }
            catch (Exception e)
            { ret = ResponseMessage(-1, "Handle command error:" + e.Message); }
            return ret;
        }
        /// <summary>
        /// 查询命令历史记录
        /// </summary>
        /// <returns></returns>
        private string HandleQueryRequest()
        {
            var ret = "[]";
            try
            {
                var id = ParseInt(Utility.Decrypt(data));
                var obj = EquipmentInstance.Find(f => f.id == id);
                if (null != obj)
                {
                    // 终端不为空时才查询
                    if ((int?)null != obj.Terminal)
                    {
                        var start = DateTime.Parse(GetParamenter("start") + " 00:00:00");
                        var end = DateTime.Parse(GetParamenter("end") + " 23:59:59");
                        var sim = obj.TB_Terminal.Sim;
                        var list = CommandInstance.FindList<CT_00000>(f => f.u_sms_mobile_no.Equals(sim) &&
                            f.u_sms_schedule_time >= start && f.u_sms_schedule_time <= end &&
                            (string.IsNullOrEmpty(cmd) ? f.u_sms_command.IndexOf("0x") >= 0 : f.u_sms_command.Equals("")), "u_sms_schedule_time", true);
                        ret = JsonConverter.ToJson(list);
                    }
                    else { ret = ResponseMessage(-1, "No terminal bond with equipment."); }
                }
                else { ret = ResponseMessage(-1, "Equipment is not exist."); }
            }
            catch(Exception e)
            { ret = ResponseMessage(-1, "Handle query error:" + e.Message); }
            return ret;
        }
    }
}