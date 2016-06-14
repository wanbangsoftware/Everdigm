using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;

using Wbs.Everdigm.Database;
using Wbs.Everdigm.BLL;
using Wbs.Utilities;
using Wbs.Protocol;

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
                    sec = (c[2] == "0F" || c[2] == "F0") ? "3000" : (c[1].IndexOf("reset") >= 0 ? "4000" : (c[1].IndexOf("sat") >= 0 ? "DD02" : "6007"));
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
                //commands.Add(new Command()
                //{
                //    Title = "Terminal: Reset to Satellite",
                //    Flag = "reset",
                //    Code = "4000",
                //    Param = "",
                //    Security = true,
                //    Content = ConfigurationManager.AppSettings["0x4000"]
                //});
                //commands.Add(new Command()
                //{
                //    Title = "Terminal: Reset to GSM",
                //    Flag = "reset_gsm",
                //    Code = "4000",
                //    Param = "",
                //    Security = true,
                //    Content = ConfigurationManager.AppSettings["0x4000_"]
                //});
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
        public static Command GetCommand(string flagOrCode, string param = "")
        {
            InitializeCommands();
            return commands.FirstOrDefault<Command>(f => (f.Flag.Equals(flagOrCode) || f.Code.Equals(flagOrCode)) && f.Param.IndexOf(param) >= 0);
        }
        /// <summary>
        /// 发送命令
        /// </summary>
        /// <param name="Sim"></param>
        /// <param name="code"></param>
        /// <param name="sms"></param>
        /// <param name="sender">发送者的ID，一般为当前登陆者的ID</param>
        /// <returns></returns>
        public static int SendCommand(TB_Terminal terminal, string code, bool sms, int sender, string extra = "")
        {
            Command cmd = GetCommand(code);
            string content = cmd.Content;
            if (cmd.Code.Equals("6007"))
            {
                // 保安命令
                // DX 的保安命令需要增加一个日期
                content += DateTime.Now.ToString("yyMMddHHmm") + cmd.Param;
                // DX 的保安命令长度不一样
                content = "1700" + content.Substring(4);
            }
            else if (cmd.Code.Equals("3000"))
            {
                // 装载机的锁车、解锁命令
                content = content.Substring(0, content.Length - 4) + cmd.Param + "00";
            }
            else if (cmd.Code.Equals("600B")) {
                uint time = uint.Parse(extra);
                byte[] tm = BitConverter.GetBytes(time);
                string t = tm[0].ToString("X2") + tm[1].ToString("X2") + tm[2].ToString("X2") + tm[3].ToString("X2");
                content = content.Substring(0, content.Length - 8) + t;
            }
            else if (cmd.Code.Equals("DD02")) {
                content = content.Substring(0, content.Length - 8) + cmd.Param + "000000";
            }
            else if (cmd.Code.Equals("4000"))
            {
                content = content.Substring(0, content.Length - 4) + cmd.Param + "00";
            }
            string sim = terminal.Sim;
            sim = (sim[0] == '8' && sim[1] == '9' && sim.Length < 11) ? (sim + "000") : sim;
            content = content.Replace(SIMNO, sim);
            // 终端类型更改，电子式的挖掘机和装载机一个类型，普通DX挖掘机一个类型
            byte ttype = terminal.Type.Value;
            ttype = (ttype == TerminalTypes.DXE || ttype == TerminalTypes.LD) ? TerminalTypes.LD : TerminalTypes.DX;
            var type = CustomConvert.IntToDigit(ttype, CustomConvert.HEX, 2);
            content = content.Substring(0, 6) + type + content.Substring(8);

            // 终端不是卫星方式连接且需要SMS方式发送时，发送SMS命令
            //if (terminal.OnlineStyle != (byte)LinkType.SATELLITE && sms)
            //{
            //    return SendSMSCommand(terminal, content, sender);
            //}
            //else
            //{
            using (var bll = new CommandBLL())
            {
                var command = bll.GetObject();
                command.DestinationNo = sim;
                // 终端是卫星方式连接则使用卫星方式发送命令
                command.Status = (terminal.OnlineStyle == (byte)LinkType.SATELLITE ?
                    (byte)CommandStatus.WaitingForSatellite :
                    // SMS发送时值为 re-sending
                    (byte)(sms ? CommandStatus.WaitingForSMS : CommandStatus.Waiting));
                command.Content = content;
                command.SendUser = (0 == sender ? (int?)null : sender);
                command.Terminal = terminal.id;
                return bll.Add(command).id;
            }
            //}
        }
        /// <summary>
        /// 发送命令并返回新增命令的id，外部捕获这个id并轮询这条命令的发送状态
        /// </summary>
        /// <param name="Sim">sim卡号码（11位数字，everdigm的sim卡号码后面带三个0，如89001435000）</param>
        /// <param name="Content">命令内容</param>
        /// <param name="sender">命令发送者的ID</param>
        /// <returns>命令记录的id，后续通过这个id查询命令状态</returns>
        private static int SendSMSCommand(TB_Terminal terminal, string Content, int sender)
        {
            string simno = terminal.Sim;
            // 判断Unitel的卡号，前面两位是89，且长度是8位数字
            simno = simno[0] == '8' && simno[1] == '9' ? simno.Substring(0, 8) : simno;
            string ret = SMSUtility.SendSMS(simno, Content);
            using (var bll = new CommandBLL())
            {
                // 查看发送成功与否的状态
                CommandStatus cs = ret.Equals("SUCCESS") ? CommandStatus.SentBySMS : CommandStatus.SentFail;

                // 新建一个命令发送类实体
                TB_Command ct = bll.GetObject();
                simno = (simno[0] == '8' && simno[1] == '9' && simno.Length < 11) ? (simno + "000") : simno;
                ct.DestinationNo = simno;
                ct.Status = (byte)cs;
                ct.Content = Content;
                ct.ActualSendTime = DateTime.Now;
                ct.Terminal = terminal.id;
                ct.SendUser = (0 == sender ? (int?)null : sender);

                var id = bll.Add(ct).id;
                // 保存终端的命令发送条数

                return id;
            }
        }
        /// <summary>
        /// 直接发送SMS命令
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns>返回发送是否成功</returns>
        public static bool SendSMSCommand(TB_Command cmd)
        {
            string simno = cmd.DestinationNo;
            // 判断Unitel的卡号，前面两位是89，且长度是8位数字
            simno = simno[0] == '8' && simno[1] == '9' ? simno.Substring(0, 8) : simno;
            string ret = SMSUtility.SendSMS(simno, cmd.Content);
            // 查看发送成功与否的状态
            CommandStatus cs = ret.Equals("SUCCESS") ? CommandStatus.SentBySMS : CommandStatus.SentFail;

            new CommandBLL().Update(f => f.id == cmd.id, act =>
            {
                act.Status = (byte)cs;
                act.ActualSendTime = DateTime.Now;
            });
            return cs == CommandStatus.SentBySMS;
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
                case CommandStatus.Waiting: ret = "Waiting in send queue..."; break;
                case CommandStatus.WaitingForSMS: ret = "Waiting in SMS queue..."; break;
                case CommandStatus.WaitingForSatellite: ret = "Waiting to handle by satellite"; break;
                case CommandStatus.SatelliteHandled: ret = "Has been handled by Satellite Server"; break;
                case CommandStatus.SentByTCP: ret = "Has been sent by TCP"; break;
                case CommandStatus.SentBySMS: ret = "Has been sent by SMS"; break;
                case CommandStatus.SentBySAT: ret = "Has been sent by Satellite Server"; break;
                case CommandStatus.SentToDest: ret = "Has been sent to destination"; break;
                case CommandStatus.SentToDestBySAT: ret = "Has been received by satellite model"; break;
                case CommandStatus.SentFail: ret = "Send fail: can not attach destination"; break;
                case CommandStatus.Returned: ret = "Data returned, "; break;
                case CommandStatus.Timedout: ret = "Timeout"; break;
                case CommandStatus.EposFail: ret = "EPOS response fail"; break;
                case CommandStatus.SecurityError: ret = "Cannot send this security command"; break;
                case CommandStatus.LinkLosed: ret = "TCP link lose"; break;
                case CommandStatus.TCPNetworkError: ret = "TCP network handle error"; break;
                case CommandStatus.EngNotStart: ret = "Eng. not start"; break;
                case CommandStatus.NoFunction: ret = "Terminal has no function to handle this command"; break;
                case CommandStatus.NotNeedReturn: ret = "Not need reply"; break;
            }
            return ret;
        }
    }
}
