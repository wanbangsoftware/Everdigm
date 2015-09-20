using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Net;
using System.IO;
using Wbs.Everdigm.BLL;
using Wbs.Everdigm.Common;
using Wbs.Everdigm.Database;
using Wbs.Protocol.TX300;
using Wbs.Utilities;
using System.Text.RegularExpressions;

namespace Wbs.Everdigm.Web
{
    /// <summary>
    /// Unitel发送过来的SMS需要用到的页面
    /// </summary>
    public partial class sms_test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // http://mongolia.wanbangsoftware.com/sms_test.aspx?&sms=JgAgFA%2B7EQ0AAAAAAAAAAQE4OTk3Njg4MDA1MjI3Mzk4MDI0NGk%3D%0A&date=2014-10-27%2017%3A59%3A20.956&number=89001483
            string sources = Request.QueryString["number"];
            string text = Request.QueryString["sms"];
            string date = Request.QueryString["date"];
            
            if (null != sources && null != text)
            {
                // 来源如果全为数字的话，保存以供查询
                if (Regex.Match(sources, "^\\d+$").Success)
                {
                    save(sources, text, date);
                }
                else
                {
                    Response.Write("Can not match \"sender\" as Numeric<br /><br />");
                }
            }
        }

        private void save(string sender, string text, string date)
        {
            DateTime dt = DateTime.Parse(date);//DateTime.ParseExact(date, "yyyyMMddHHmmss", null, System.Globalization.DateTimeStyles.None);
            byte[] b = Convert.FromBase64String(text);
            string content = Utility.GetHex(b);
            //try
            {
                TX300 x300 = new TX300(content);
                x300.package_to_msg();
                x300.TerminalID = sender + "000";
                //save(x300);
                if (x300.CommandID == 0xBB0F)
                {
                    handleBB0F(sender, x300.TerminalType);
                }

                var SMSInstance = new SmsBLL();
                var sms = SMSInstance.GetObject();
                sms.SendTime = dt;
                sms.Sender = sender;
                sms.Data = text;
                SMSInstance.Add(sms);

            }
        }

        private void HandleEquipmentState(string sender, ushort Command)
        {
            var EquipmentInstance = new EquipmentBLL();
            EquipmentInstance.Update(f => f.TB_Terminal.Sim.Equals(sender) && f.Deleted == false, act =>
            {
                act.Socket = 0;
                act.OnlineTime = DateTime.Now;
                act.IP = "";
                act.Port = 0;
                act.LastAction = "0x" + CustomConvert.IntToDigit(Command, CustomConvert.HEX, 4);
                act.LastActionBy = "SMS";
                act.LastActionTime = DateTime.Now;
                if (act.OnlineStyle == (byte)LinkType.OFF && Command == 0x2000)
                { }
                else
                {
                    act.OnlineStyle = (byte)LinkType.SMS;
                }
            });
            var TerminalInstance = new TerminalBLL();
            TerminalInstance.Update(f => f.Sim.Equals(sender), act =>
            {
                act.Socket = 0;
                act.OnlineTime = DateTime.Now;
                if (act.OnlineStyle == (byte)LinkType.OFF && Command == 0x2000)
                {
                    // 收到报警但此时已经是OFF状态时，不更新在线状态
                }
                else
                {
                    act.OnlineStyle = (byte)LinkType.SMS;
                }
            });
        }

        private void handleBB0F(string sender, byte terminalType)
        {
            // 170020140FBB10FFFF0890014950000101089001495000
            string cmd = "170020" + Utility.GetHex(terminalType) + "0FBB10FFFF0" + sender + "00001010" + sender + "000";

            var TerminalInstance = new TerminalBLL();
            var terminal = TerminalInstance.Find(f => f.Sim.Equals(sender));

            var CommandInstance = new CommandBLL();
            var obj = CommandInstance.GetObject();
            //var simno = (simno[0] == '8' && simno[1] == '9' && simno.Length < 11) ? (simno + "000") : simno;
            obj.DestinationNo = sender + "000";
            obj.Status = (byte)CommandStatus.Waiting;
            obj.Content = cmd;
            //obj.ActualSendTime = DateTime.Now;
            obj.Terminal = null == terminal ? (int?)null : terminal.id;
            obj.SendUser = (int?)null;
            obj = CommandInstance.Add(obj);

            //CommandUtility.SendSMSCommand(obj);

            // 保存SMS发送流量
            //saveTerminalFlow(null == terminal ? -1 : terminal.id, sender);
        }
        /// <summary>
        /// 保存命令发送流量
        /// </summary>
        /// <param name="ter"></param>
        /// <param name="sim"></param>
        private void saveTerminalFlow(int ter, string sim)
        {
            var n = (int?)null;
            int monthly = int.Parse(DateTime.Now.ToString("yyyyMM"));
            var flow = new TerminalFlowBLL();
            var f = flow.Find(find => find.Sim.Equals(sim) && find.Monthly == monthly);
            if (null == f)
            {
                f = flow.GetObject();
                f.Monthly = monthly;
                f.Sim = sim;
                f.Terminal = ter < 0 ? n : ter;
                f.SMSDeliver = 1;
                flow.Add(f);
            }
            else
            {
                flow.Update(u => u.id == f.id, act =>
                {
                    if (n == act.Terminal)
                    {
                        act.Terminal = ter < 0 ? n : ter;
                    }
                    act.SMSDeliver += 1;
                });
            }
        }

        protected void Encode_Click(object sender, EventArgs e)
        {
            var str = hex.Value.Trim();
            if (null == str || "" == str) { return; }

            var data = CustomConvert.GetBytes(str);
            base64.InnerText = Convert.ToBase64String(data);
            base64Encode.InnerText = HttpUtility.UrlEncode(base64.InnerText);
        }

        protected void Decode_Click(object sender, EventArgs e)
        {
            var str = hex.Value.Trim();
            if (null == str || "" == str) return;
            
            var data = Convert.FromBase64String(str);
            var h = CustomConvert.GetHex(data);
            hexdata.InnerText = h;
            base64.InnerText = str;
        }
    }
}