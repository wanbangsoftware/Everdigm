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
        private static string resp = "\"status\":{0},\"desc\":\"{1}\",\"date\":\"{2}\"";
        /// <summary>
        /// 返回处理状态以及处理结果
        /// </summary>
        /// <param name="status"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private string ResponseMessage(int status, string msg)
        {
            return "{" + string.Format(resp, status, msg, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")) + "}";
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
                case "terminal":
                    // 直接通过终端的信息发送命令
                    ret = HandleTerminalCommandRequest();
                    break;
                case "history": 
                    // 查询命令历史记录
                    ret = HandleCommandHistoryRequest();
                    break;
                case "query":
                    // 查询命令的发送状态
                    ret = HandleQueryCommandStatusRequest();
                    break;
                default:
                    ret = ResponseMessage(-1, "Unknown command type.");
                    break;
            }
            ResponseJson(ret);
        }
        /// <summary>
        /// 处理查询命令发送状态的请求
        /// </summary>
        /// <returns></returns>
        private string HandleQueryCommandStatusRequest() {
            string ret = "";
            var cmd = CommandInstance.Find(f => f.u_sms_id == ParseInt(data));
            if (null == cmd) { ret = ResponseMessage(-1, "No such command record exists."); }
            else {
                byte status = cmd.u_sms_status.Value;
                CommandStatus state = (CommandStatus)status;
                if (state == CommandStatus.Returned) {
                    var list = DataInstance.FindList<TB_HISTORIES>(f =>
                        f.command_id.Equals(cmd.u_sms_command) &&
                        f.receive_time > cmd.u_sms_schedule_time, "receive_time", true);
                    var data = list.FirstOrDefault<TB_HISTORIES>();
                    var desc = CommandUtility.GetCommandStatus(state);
                    if (null != data)
                    {
                        desc += ", you can click <code data-data=\"" + data.message_content + "\">here</code> to Analyse it";
                    }
                    ret = ResponseMessage(status, desc);
                }
                else
                {
                    ret = ResponseMessage(status, CommandUtility.GetCommandStatus(state));
                }
            }
            return ret;
        }
        /// <summary>
        /// 处理终端命令的请求
        /// </summary>
        /// <returns></returns>
        private string HandleTerminalCommandRequest() {
            string ret = "{}";
            try {
                var t = TerminalInstance.Find(f => f.Number.Equals(data));
                if (null == t) { ret = ResponseMessage(-1, "No terminal like \"" + data + "\" exists"); }
                else { ret = HandleTerminalCommandRequest(t); }
            }
            catch (Exception e) {
                ret = ResponseMessage(-1, "Handle Terminal command error:" + e.Message);
            }
            return ret;
        }
        private string SendCommand(TB_Terminal obj, bool sms) {
            var id = CommandUtility.SendCommand(obj.Sim, cmd, sms);
            return ResponseMessage(0, id.ToString());
        }
        /// <summary>
        /// 处理终端的命令发送情况
        /// </summary>
        /// <param name="obj">终端对象</param>
        /// <param name="link">当前终端的链接情况，没有绑定设备时默认采用SMS方式发送命令</param>
        /// <returns></returns>
        private string HandleTerminalCommandRequest(TB_Terminal obj)
        {
            string ret = "";
            LinkType link = (LinkType)obj.OnlineStyle;
            switch (link)
            {
                case LinkType.OFF: ret = ResponseMessage(-1, "Terminal is power off."); break;
                case LinkType.TCP:// 通过TCP方式发送命令
                case LinkType.SATELLITE:// 通过卫星方式发送命令
                    ret = SendCommand(obj, false);
                    break;
                case LinkType.UDP:
                case LinkType.SMS:
                case LinkType.SLEEP:
                    // 通过SMS方式发送命令
                    ret = SendCommand(obj, true);
                    break;
                default:
                    ret = ResponseMessage(-1, "Terminal has no communication with server long time.");
                    break;
            }
            return ret;
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
                        ret = HandleTerminalCommandRequest(obj.TB_Terminal);
                    }
                    else { ret = ResponseMessage(-1, "No terminal bond with equipment."); }
                }
                else { ret = ResponseMessage(-1, "Equipment is not exist."); }
            }
            catch (Exception e)
            { ret = ResponseMessage(-1, "Handle Equipment command error:" + e.Message); }
            return ret;
        }
        /// <summary>
        /// 查询命令历史记录
        /// </summary>
        /// <returns></returns>
        private string HandleCommandHistoryRequest()
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