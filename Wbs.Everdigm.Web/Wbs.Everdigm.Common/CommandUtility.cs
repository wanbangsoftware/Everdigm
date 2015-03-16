using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Wbs.Everdigm.Database;
using Wbs.Everdigm.BLL;

namespace Wbs.Everdigm.Common
{
    /// <summary>
    /// 提供发送命令方法集合
    /// </summary>
    public class CommandUtility
    {
        /// <summary>
        /// 发送命令并返回新增命令的id，外部捕获这个id并轮询这条命令的发送状态
        /// </summary>
        /// <param name="Sim">sim卡号码（11位数字，everdigm的sim卡号码后面带三个0，如89001435000）</param>
        /// <param name="Content">命令内容</param>
        /// <returns>命令记录的id，后续通过这个id查询命令状态</returns>
        public static int SendCommand(string Sim, string Content)
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
    }
}
