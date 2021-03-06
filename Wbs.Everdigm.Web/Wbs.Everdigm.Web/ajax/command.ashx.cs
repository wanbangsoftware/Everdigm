﻿using System;
using System.Linq;
using System.Web;

using Wbs.Everdigm.Common;
using Wbs.Everdigm.Database;
using Wbs.Everdigm.BLL;
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
                    ret = string.Format("Worktime: {0}", EquipmentBLL.GetRuntime((int?)BitConverter.ToUInt32(buffer, start), 0.0, true));
                    break;
                case "0x600B":
                    ret = string.Format("Worktime: {0}", EquipmentBLL.GetRuntime((int?)BitConverter.ToUInt32(buffer, 0), 0.0, true));
                    break;
                case "0x6007": ret = string.Format("Security: {0}", _0x6007.GetSecurity(buffer[1])); break;
                case "0x3000": ret = string.Format("Security: {0}", _0x3000.GetFlag(buffer[0])); break;
                case "0xDD02": ret = string.Format("Satellite: {0}", _0xDD02.GetStatus(buffer[0])); break;
            }
            return ret;
        }
        /// <summary>
        /// 处理查询命令发送状态的请求
        /// </summary>
        /// <returns></returns>
        private string HandleQueryCommandStatusRequest()
        {
            string ret = "";
            using (var bll = new CommandBLL())
            {
                var cmd = bll.Find(f => f.id == ParseInt(data));
                if (null == cmd)
                {
                    ret = ResponseMessage(-1, "No such command record exists.");
                }
                else
                {
                    byte status = cmd.Status.Value;
                    CommandStatus state = (CommandStatus)status;
                    if (state == CommandStatus.Returned)
                    {
                        using (var dbll = new DataBLL())
                        {
                            var list = dbll.FindList<TB_HISTORIES>(f =>
                                f.command_id.Equals(cmd.Command) && f.terminal_id.Equals(cmd.DestinationNo) &&
                                f.receive_time > cmd.ActualSendTime, "receive_time", true);
                            var data = list.FirstOrDefault();
                            var desc = CommandUtility.GetCommandStatus(state);
                            if (null != data)
                            {
                                desc += GetCommandData(data);
                            }
                            ret = ResponseMessage(status, desc);
                        }
                    }
                    else
                    {
                        ret = ResponseMessage(status, CommandUtility.GetCommandStatus(state));
                        if (cmd.Command == "0x4000" && (state == CommandStatus.SentBySMS || state == CommandStatus.SentByTCP))
                        {
                            // 将重置终端连接的命令状态改成不需要回复的状态
                            bll.Update(f => f.id == cmd.id, act =>
                            {
                                act.Status = (byte)CommandStatus.NotNeedReturn;
                            });
                        }
                    }
                }
            }
            return ret;
        }
        /// <summary>
        /// 处理终端命令的请求
        /// </summary>
        /// <returns></returns>
        private string HandleTerminalCommandRequest()
        {
            string ret = "{}";
            try
            {
                using (var bll = new TerminalBLL())
                {
                    var t = bll.Find(f => f.Number.Equals(data));
                    if (null == t)
                    {
                        ret = ResponseMessage(-1, "No terminal like \\\"" + data + "\\\" exists");
                    }
                    else
                    {
                        ret = HandleTerminalCommandRequest(t);
                    }
                }
            }
            catch (Exception e)
            {
                ret = ResponseMessage(-1, "Handle Terminal command error:" + e.Message);
            }
            return ret;
        }
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
                return ResponseMessage(-1, "Your session was expired, please try to login again.");
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
            LinkType link = (LinkType)obj.OnlineStyle;
            string ret = "";
            // 判断是否是转到卫星链接命令
            if (cmd == "reset_sat")
            {
                // GSM连接下才能发送转卫星连接的命令
                if (link >= LinkType.TCP && link <= LinkType.SMS)
                {
                    // 卫星功能禁用时不能发送这个命令
                    if (obj.SatelliteStatus == false)
                    {
                        ret = ResponseMessage(-1, "Command has blocked(Satellite function was disabled)");
                    }
                    else
                    {
                        var mac = obj.TB_Equipment.FirstOrDefault();
                        // 主电报警时不能发送转到卫星链接的命令
                        if (null != mac && (mac.Alarm[0] == '1' || mac.Alarm[15] == '1'))
                        {
                            ret = ResponseMessage(-1, "Can not turn to satellite mode when terminal use battery.");
                        }
                    }
                }
                else
                {
                    // 非GSM在线状态不能发送强制转卫星连接的命令
                    ret = ResponseMessage(-1, "Can not send this command with link: " + link + ".");
                }
            }
            if (cmd == "satenable" || cmd == "satdisable")
            {
                bool enabled = cmd.Equals("satenable");
                // 设置卫星功能
                if (link >= LinkType.TCP && link <= LinkType.SMS)
                {
                    if (enabled && obj.SatelliteStatus == true)
                    {
                        ret = ResponseMessage(-1, "It is not nessesary to re-Enable satellite function.");
                    }
                    else if (!enabled && obj.SatelliteStatus == false)
                    {
                        ret = ResponseMessage(-1, "It is not nessesary to re-Disable satellite function.");
                    }
                }
                else
                {
                    // 非GSM在线状态不能发送禁用或启用卫星命令
                    ret = ResponseMessage(-1, "Can not send this command with link: " + link + ".");
                }
            }
            if(string.IsNullOrEmpty(ret))
            {
                if ((byte?)null == obj.OnlineStyle)
                {
                    ret = ResponseMessage(-1, "Can not send any commands with this situation(Link: Unknow).");
                }
                else
                {
                    switch (link)
                    {
                        case LinkType.OFF: ret = ResponseMessage(-1, "Terminal is power off."); break;
                        case LinkType.TCP:// 通过TCP方式发送命令
                            // 增加强制SMS方式发送选择
                            ret = SendCommand(obj, forceType == ForceType.SMS ? true : false);
                            break;
                        case LinkType.SATELLITE:// 通过卫星方式发送命令
                            ret = SendCommand(obj, false);
                            break;
                        case LinkType.UDP:
                        case LinkType.SMS:
                            // 通过SMS方式发送命令
                            ret = SendCommand(obj, true);
                            break;
                        case LinkType.SLEEP:
                            // 睡眠模式下如果发送转Satellite命令的话，控制
                            if (cmd == "reset_sat")
                            {
                                ret = ResponseMessage(-1, "Command has blocked(Main power lose).");
                            }
                            else
                            {
                                // 通过SMS方式发送命令
                                ret = SendCommand(obj, true);
                            }
                            break;
                        default:
                            ret = ResponseMessage(-1, "Terminal has no communication with server long time.");
                            break;
                    }
                }
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
                        ret = ResponseMessage(-1, "Cannot send this security command from Disabled status.");
                    }
                    break;
                case "20":
                    // 代理商保安命令不能发送禁用命令
                    if (tobe.Equals("10"))
                    {
                        ret = ResponseMessage(-1, "Cannot disable security function directly from Partial Lock status.");
                    }
                    else if (tobe.Equals("40"))
                    {
                        ret = ResponseMessage(-1, "Cannot change Partial Lock to Full Lock directly.");
                    }
                    break;
                case "40":
                    if (tobe.Equals("10"))
                    {
                        ret = ResponseMessage(-1, "Cannot disable security function directly from Full Lock status.");
                    }
                    else if (tobe.Equals("20"))
                    {
                        ret = ResponseMessage(-1, "Cannot lower the Security level directly from Full Lock status.");
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
                using (var bll = new EquipmentBLL())
                {
                    var obj = bll.Find(f => f.id == id && f.Deleted == false);
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
                using (var bll = new EquipmentBLL())
                {
                    var obj = bll.Find(f => f.id == id && f.Deleted == false);
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
                using (var ebll = new EquipmentBLL())
                {
                    var obj = ebll.Find(f => f.id == id && f.Deleted == false);
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
                            using (var bll = new CommandBLL())
                            {
                                var list = bll.FindList<TB_Command>(f => f.DestinationNo.Equals(sim) &&
                                    f.ScheduleTime >= start && f.ScheduleTime <= end && f.Command != "0xBB0F", "ScheduleTime", true);
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
                                    else if (security && command.Code.Equals("4000"))
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
                                        if (record.Command == "0x4000")
                                            param = record.Content.Substring(record.Content.Length - 4, 2);
                                        if (record.Command == "0xDD02")
                                            param = record.Content.Substring(record.Content.Length - 8, 2);
                                        Command _cmd = CommandUtility.GetCommand(record.Command.Replace("0x", ""), param);
                                        var func = (EquipmentFunctional)obj.Functional;
                                        var called = (func == EquipmentFunctional.Mechanical || func == EquipmentFunctional.Electric) ? "Equipment" : "Loader";
                                        record.Command = (null == _cmd ? "" : _cmd.Title.Replace("Equipment", called).Replace("Loader", called));
                                        // 加入命令发送者  2015/09/18 10:36
                                        record.Content = (int?)null == record.SendUser ? "Server" : record.TB_Account.Name;
                                    }
                                }
                                ret = JsonConverter.ToJson(list);
                            }
                        }
                        else { ret = ResponseMessage(-1, "No terminal bond with this equipment."); }
                    }
                    else { ret = ResponseMessage(-1, "Equipment is not exist."); }
                }
            }
            catch(Exception e)
            { ret = ResponseMessage(-1, "Handle History request error:" + e.Message); }
            return ret;
        }
    }
}