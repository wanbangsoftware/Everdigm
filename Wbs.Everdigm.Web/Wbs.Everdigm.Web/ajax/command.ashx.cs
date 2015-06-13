using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

using Wbs.Everdigm.Common;
using Wbs.Everdigm.Database;
using Wbs.Protocol.TX300;
using Wbs.Protocol.TX300.Analyse;

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
                case "equipment": 
                    // 发送命令并返回命令发送记录的id以供后续查询状态
                    ret = HandleEquipmentCommandRequest();
                    break;
                case "terminal":
                    // 直接通过终端的信息发送命令
                    ret = HandleTerminalCommandRequest();
                    break;
                case "history": 
                    // 查询命令历史记录
                    ret = HandleCommandHistoryRequest(false);
                    break;
                case "security":
                    ret = HandleSecurityCommandRequest();
                    break;
                case "sechistory":
                    // 查询保安命令的历史记录
                    ret = HandleCommandHistoryRequest(true);
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
        private string GetCommandData(TB_HISTORIES data)
        {
            var ret = "";
            var buffer = Wbs.Utilities.CustomConvert.GetBytes(data.message_content);
            var start = 0;
            switch (data.command_id)
            {
                case "0xDD00":
                    ret = string.Format("Signal: {0}({1})", buffer[8], Utility.ASU2DBM(buffer[8]));
                    break;
                case "0x1000":
                    ret = "Please going to page <code>Map</code> to check out the data."; 
                    break;
                case "0x6000":
                    ret = string.Format("RPM: {0}, Fuel: {1}", BitConverter.ToUInt16(buffer, 5),
                        OilTempers.GetOilLeftDX((ushort)(BitConverter.ToUInt16(buffer, 29) * 500 / 1024)).ToString());
                    break;
                case "0x6004":
                    start = data.terminal_type == Wbs.Protocol.TerminalTypes.DX ? 5 : 1;
                    ret = string.Format("Worktime: {0}", EquipmentInstance.GetRuntime((int?)BitConverter.ToUInt32(buffer, start)));
                    break;
                case "0x600B":
                    ret = string.Format("Worktime: {0}", EquipmentInstance.GetRuntime((int?)BitConverter.ToUInt32(buffer, 0)));
                    break;
                case "0x6007": ret = string.Format("Security: {0}", _0x6007.GetSecurity(buffer[1])); break;
                case "0x3000": ret = string.Format("Security: {0}", _0x3000.GetFlag(buffer[0])); break;
                case "0xDD02": ret = string.Format("Satellite: {0}",_0xDD02.GetStatus(buffer[0])); break;
            }
            return ret;
        }
        /// <summary>
        /// 处理查询命令发送状态的请求
        /// </summary>
        /// <returns></returns>
        private string HandleQueryCommandStatusRequest() {
            string ret = "";
            var cmd = CommandInstance.Find(f => f.id == ParseInt(data));
            if (null == cmd) { ret = ResponseMessage(-1, "No such command record exists."); }
            else {
                byte status = cmd.Status.Value;
                CommandStatus state = (CommandStatus)status;
                if (state == CommandStatus.Returned) {
                    var list = DataInstance.FindList<TB_HISTORIES>(f =>
                        f.command_id.Equals(cmd.Command) && f.terminal_id.Equals(cmd.DestinationNo) &&
                        f.receive_time > cmd.ActualSendTime, "receive_time", true);
                    var data = list.FirstOrDefault<TB_HISTORIES>();
                    var desc = CommandUtility.GetCommandStatus(state);
                    if (null != data)
                    {
                        desc += GetCommandData(data);
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
        /// <summary>
        /// 当前登陆者的信息
        /// </summary>
        private TB_Account User { get { return ctx.Session[Utility.SessionName] as TB_Account; } }
        /// <summary>
        /// 发送命令
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="sms"></param>
        /// <returns></returns>
        private string SendCommand(TB_Terminal obj, bool sms)
        {
            if (null == User)
            {
                return ResponseMessage(-1, "Your session has expired, Please try to login again.");
            }
            else
            {
                var extra = GetParamenter("param");
                var id = CommandUtility.SendCommand(obj, cmd, sms, User.id, extra);
                return ResponseMessage(0, id.ToString());
            }
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
        /// 判断当前保安状态是否可以发送指定的保安命令
        /// </summary>
        /// <param name="current"></param>
        /// <param name="tobe"></param>
        /// <returns></returns>
        private string HandleSecurityStatus(string current, string tobe)
        {
            var ret = "";
            switch (current)
            {
                case "00": break;// 初始状态可以发送任何保安命令
                case "10":
                    // 保安命令处于已禁用时只能发送00，其余的都不能发送
                    if (!tobe.Equals("00") && !tobe.Equals("10"))
                    {
                        ret = ResponseMessage(-1, "Cannot send this security command in Disabled status.");
                    }
                    break;
                case "20":
                    // 代理商保安命令不能发送禁用命令
                    if (tobe.Equals("10"))
                    {
                        ret = ResponseMessage(-1, "Cannot disable security function in Custom Lock status.");
                    }
                    break;
                case "40":
                    if (tobe.Equals("10"))
                    {
                        ret = ResponseMessage(-1, "Cannot disable security function in Full Lock status.");
                    }
                    else if (tobe.Equals("20"))
                    {
                        ret = ResponseMessage(-1, "Cannot lower the security level in Full Lock status.");
                    }
                    break;
            }
            return ret;
        }
        /// <summary>
        /// 处理发送保安命令请求
        /// </summary>
        /// <returns></returns>
        private string HandleSecurityCommandRequest()
        {
            var ret = "[]";
            try
            {
                var id = ParseInt(Utility.Decrypt(data));
                var obj = EquipmentInstance.Find(f => f.id == id && f.Deleted == false);
                if (null != obj)
                {
                    if ((int?)null != obj.Terminal)
                    {
                        ret = "";
                        // 查看是否发送的保安命令
                        var command = CommandUtility.GetCommand(cmd);
                        if (command.Security && command.Code.Equals("6007"))
                        {
                            ret = HandleSecurityStatus(obj.LockStatus, command.Param);
                        }
                        if (string.IsNullOrEmpty(ret))
                        {
                            // 查看当前设备的链接状态然后确定命令的发送方式
                            ret = HandleTerminalCommandRequest(obj.TB_Terminal);
                        }
                    }
                    else { ret = ResponseMessage(-1, "No terminal bond with this equipment."); }
                }
                else { ret = ResponseMessage(-1, "Equipment is not exist."); }
            }
            catch (Exception e) { ret = ResponseMessage(-1, "Handle Security command error:" + e.Message); }
            return ret;
        }
        /// <summary>
        /// 处理发送到设备的命令请求
        /// </summary>
        /// <returns></returns>
        private string HandleEquipmentCommandRequest()
        {
            var ret = "[]";
            try
            {
                var id = ParseInt(Utility.Decrypt(data));
                var obj = EquipmentInstance.Find(f => f.id == id && f.Deleted == false);
                if (null != obj)
                {
                    if ((int?)null != obj.Terminal)
                    {
                        // 查看当前设备的链接状态然后确定命令的发送方式
                        ret = HandleTerminalCommandRequest(obj.TB_Terminal);
                    }
                    else { ret = ResponseMessage(-1, "No terminal bond with this equipment."); }
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
        /// <param name="security">是否查询保安命令</param>
        /// <returns></returns>
        private string HandleCommandHistoryRequest(bool security)
        {
            var ret = "[]";
            try
            {
                var id = ParseInt(Utility.Decrypt(data));
                var obj = EquipmentInstance.Find(f => f.id == id && f.Deleted == false);
                if (null != obj)
                {
                    // 终端不为空时才查询
                    if ((int?)null != obj.Terminal)
                    {
                        var start = DateTime.Parse(GetParamenter("start") + " 00:00:00");
                        var end = DateTime.Parse(GetParamenter("end") + " 23:59:59");
                        var sim = obj.TB_Terminal.Sim;
                        if (sim[0] == '8' && sim[1] == '9' && sim.Length < 11)
                        {
                            sim += "000";
                        }
                        // 查询的命令
                        //string _command = "";
                        Command command = null;
                        if (!string.IsNullOrEmpty(cmd))
                        {
                            command = CommandUtility.GetCommand(cmd);
                            //_command = command.Code;
                        }
                        var list = CommandInstance.FindList<TB_Command>(f => f.DestinationNo.Equals(sim) &&
                            f.ScheduleTime >= start && f.ScheduleTime <= end, "ScheduleTime", true);
                        if (security)
                        {
                            list = list.Where(w => w.Command == "0x6007" || w.Command == "0x4000" || w.Command == "0x3000" || w.Command == "0xDD02");
                        }
                        else
                        {
                            list = list.Where(w => w.Command != "0x6007" && w.Command != "0x4000" && w.Command != "0x3000" && w.Command != "0xDD02");
                        }
                        //&&
                        //    (string.IsNullOrEmpty(_command) ? f.u_sms_command.IndexOf("0x") >= 0 : f.u_sms_command.IndexOf(_command) >= 0),
                        //    "u_sms_schedule_time", true);
                        if (null != command)
                        {
                            list = list.Where(w => w.Command.IndexOf(command.Code) >= 0);
                            if (security && command.Code.Equals("6007"))
                            {
                                list = list.Where(w => w.Content.Substring(w.Content.Length - 2) == command.Param);
                            }
                            else if (security && command.Code.Equals("3000"))
                            {
                                list = list.Where(w => w.Content.Substring(w.Content.Length - 4, 2) == command.Param);
                            }
                        }
                        if (list.Count() > 0)
                        {
                            // 将command_id替换
                            //List<Command> commands = CommandUtility.GetCommand();
                            foreach (var record in list)
                            {
                                string param = "";
                                if (record.Command == "0x6007")
                                    param = record.Content.Substring(record.Content.Length - 2);
                                if (record.Command == "0x3000")
                                    param = record.Content.Substring(record.Content.Length - 4, 2);
                                Command _cmd = CommandUtility.GetCommand(record.Command.Replace("0x", ""), param);
                                record.Command = (null == _cmd ? "" : _cmd.Title);
                            }
                        }
                        ret = JsonConverter.ToJson(list);
                    }
                    else { ret = ResponseMessage(-1, "No terminal bond with this equipment."); }
                }
                else { ret = ResponseMessage(-1, "Equipment is not exist."); }
            }
            catch(Exception e)
            { ret = ResponseMessage(-1, "Handle History request error:" + e.Message); }
            return ret;
        }
    }
}