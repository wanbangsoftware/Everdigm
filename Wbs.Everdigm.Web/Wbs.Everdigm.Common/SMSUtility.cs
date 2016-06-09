using System;

using System.IO;
using System.Web;
using System.Net;
using System.Configuration;

// 通用方法集合
namespace Wbs.Everdigm.Common
{
    /// <summary>
    /// 提供Everdigm系统中发送短信的功能
    /// </summary>
    public class SMSUtility
    {
        /// <summary>
        /// 终端GSM汇报回来的SMS消息
        /// </summary>
        public static byte SMS_TERMINAL = 0x00;
        /// <summary>
        /// 手机设备tracker汇报回来的数据，全部都存在sms表里
        /// </summary>
        public static byte SMS_TRACKER = 0x01;
        /// <summary>
        /// SMS内容字符串
        /// </summary>
        private static string CONTENT = ConfigurationManager.AppSettings["SMSContent"];
        /// <summary>
        /// SMS发送目的号码字符串
        /// </summary>
        private static string MOBILE = ConfigurationManager.AppSettings["SMSMobile"];
        /// <summary>
        /// 发送SMS信息的URL
        /// </summary>
        private static string URL = ConfigurationManager.AppSettings["SendSMS64"].Replace('#', '&'); 

        /// <summary>
        /// 发送SMS到Unitel服务器
        /// </summary>
        /// <param name="sim">目的号码。如89001483</param>
        /// <param name="command">需要发送的SMS内容。16进制字符串，参见web.config里的appSetting字段设置</param>
        /// <returns></returns>
        public static string SendSMS(string sim, string command)
        {
            string ret = "";
            string content = "";
            byte[] b = ProtocolItems.GetBytes(command);
            content = Convert.ToBase64String(b);
            content = HttpUtility.UrlEncode(content);

            // 组合当前发送号码和发送内容到url中
            string url = URL.Replace(CONTENT, content).Replace(MOBILE, sim);

            try
            {
                // 正式调用URL发送SMS信息，测试期可以注释掉，运营期时再恢复
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                StreamReader reader = new StreamReader(resp.GetResponseStream(), System.Text.Encoding.GetEncoding("utf-8"));
                ret = reader.ReadToEnd();
                reader.Close();
            }
            catch (Exception e)
            {
                ret = "exception: " + e.ToString();
            }
            return ret;
        }
    }
}
