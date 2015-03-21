using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

using Wbs.Everdigm.Database;
using Wbs.Everdigm.BLL;

namespace Wbs.Everdigm.Common
{
    /// <summary>
    /// 提供发送命令方法集合
    /// </summary>
    public class CommandUtility
    {
        private static string CUSTOM_CMD = ConfigurationManager.AppSettings["Custom_Commands"];
        private static string SECURITY_CMD = ConfigurationManager.AppSettings["Security_Commands"];
        private static string SIMNO = "13953598693";
        private static List<Command> commands = null;
        /// <summary>
        /// 初始化命令列表
        /// </summary>
        private static void InitializeCommands()
        {
            if (null == commands)
            {
                commands = new List<Command>();
                // 初始化命令列表
                // 初始化普通命令列表
                var cmds = CUSTOM_CMD.Split(new char[] { ',' });
                foreach (var cmd in cmds)
                {
                    var c = cmd.Split(new char[] { '|' });
                    commands.Add(new Command()
                    {
                        Title = c[0],
                        Flag = c[1],
                        Code = c[2],
                        Param = "",
                        Security = false,
                        Content = ConfigurationManager.AppSettings["0x" + c[2]]
                    });
                }
                // 初始化保安命令列表
                cmds = SECURITY_CMD.Split(new char[] { ',' });
                var sec = "6007";
                foreach (var cmd in cmds)
                {
                    var c = cmd.Split(new char[] { '|' });
                    commands.Add(new Command()
                    {
                        Title = c[0],
                        Flag = c[1],
                        Code = sec,
                        Security = true,
                        Param = c[2],
                        Content = ConfigurationManager.AppSettings["0x" + sec]
                    });
                }
                // 初始化终端重置命令Terminal: Reset|reset|4000
                commands.Add(new Command()
                {
                    Title = "Terminal: Reset",
                    Flag = "reset",
                    Code = "4000",
                    Param = "",
                    Security = true,
                    Content = ConfigurationManager.AppSettings["0x4000"]
                });
            }
        }
        /// <summary>
        /// 获取服务器上可发的命令列表
        /// </summary>
        /// <returns></returns>
        public static List<Command> GetCommand(bool security) {
            InitializeCommands();
            return commands.FindAll(f => f.Security == security);
        }
        /// <summary>
        /// 获取服务器上可以发送的所有命令列表
        /// </summary>
        /// <returns></returns>
        public static List<Command> GetCommand() {
            InitializeCommands();
            return commands;
        }
        /// <summary>
        /// 根据指定的code获取command详细消息
        /// </summary>
        /// <param name="flagOrCode">通过命令的区别码或命令的代码查询</param>
        /// <returns></returns>
        public static Command GetCommand(string flagOrCode)
        {
            InitializeCommands();
            return commands.FirstOrDefault<Command>(f => f.Flag.Equals(flagOrCode) || f.Code.Equals(flagOrCode));
        }
        /// <summary>
        /// 发送命令
        /// </summary>
        /// <param name="Sim"></param>
        /// <param name="code"></param>
        /// <param name="sms"></param>
        /// <returns></returns>
        public static int SendCommand(string Sim, string code, bool sms) {
            Command cmd = GetCommand(code);
            string content = cmd.Content;
            if (cmd.Code.Equals("6007")) { 
                // 保安命令
                // DX 的保安命令需要增加一个日期
                content += DateTime.Now.ToString("yyMMddHHmm") + cmd.Param;
                // DX 的保安命令长度不一样
                content = "1700" + content.Substring(4);
            }
            string sim = (Sim[0] == '8' && Sim[1] == '9' && Sim.Length < 11) ? (Sim + "000") : Sim;
            content = content.Replace(SIMNO, sim);
            if (sms) {
                return SendSMSCommand(sim, content);
            }
            else {
                var CommandInstance = new CommandBLL();
                var command = CommandInstance.GetObject();
                command.u_sms_mobile_no = sim;
                command.u_sms_status = (byte)CommandStatus.Waiting;
                command.u_sms_content = content;
                return CommandInstance.Add(command).u_sms_id;
            }
        }
        /// <summary>
        /// 发送命令并返回新增命令的id，外部捕获这个id并轮询这条命令的发送状态
        /// </summary>
        /// <param name="Sim">sim卡号码（11位数字，everdigm的sim卡号码后面带三个0，如89001435000）</param>
        /// <param name="Content">命令内容</param>
        /// <returns>命令记录的id，后续通过这个id查询命令状态</returns>
        private static int SendSMSCommand(string Sim, string Content)
        {
            // 判断Unitel的卡号，前面两位是89，且长度是8位数字
            string simno = Sim[0] == '8' && Sim[1] == '9' ? Sim.Substring(0, 8) : Sim;
            string ret = SMSUtility.SendSMS(simno, Content);
            var CommandInstance = new CommandBLL();
            // 查看发送成功与否的状态
            CommandStatus cs = ret.Equals("SUCCESS") ? CommandStatus.SentBySMS : CommandStatus.SentFail;

            // 新建一个命令发送类实体
            CT_00000 ct = CommandInstance.GetObject();
            ct.u_sms_mobile_no = simno;
            ct.u_sms_status = (byte)cs;
            ct.u_sms_content = Content;

            return CommandInstance.Add(ct).u_sms_id;
        }
        /// <summary>
        /// 获取命令的发送状态描述
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static string GetCommandStatus(CommandStatus status) {
            string ret = "";
            switch (status)
            {
                case CommandStatus.Waiting: ret = "Waiting in queue for send"; break;
                case CommandStatus.ReSending: ret = "Waiting for re-send again"; break;
                case CommandStatus.SentByTCP: ret = "Has been sent by TCP"; break;
                case CommandStatus.SentBySMS: ret = "Has been sent by SMS"; break;
                case CommandStatus.SentBySAT: ret = "Has been sent by Satellite"; break;
                case CommandStatus.SentToDest: ret = "Has been sent to destination"; break;
                case CommandStatus.SentFail: ret = "Send fail: can not attach destination"; break;
                case CommandStatus.Returned: ret = "Data has returned"; break;
                case CommandStatus.Timedout: ret = "Timeout"; break;
                case CommandStatus.EposFail: ret = "EPOS response fail."; break;
                case CommandStatus.SecurityError: ret = "Cannot send this security command"; break;
            }
            return ret;
        }
    }
}
