using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.IO;
using System.Text;
using System.Diagnostics;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace Wbs.Everdigm.Web
{
    /// <summary>
    /// 一些常量定义
    /// </summary>
    public class Utility
    {
        /// <summary>
        /// 全局加密解密类
        /// </summary>
        public static RijndaelManaged _RijndaelManaged = null;
        /// <summary>
        /// 登陆用户的session名
        /// </summary>
        public static string SessionName = "_session_login_name_";
        /// <summary>
        /// 客户登陆的session名
        /// </summary>
        public static string SessionNameCustomer = "_session_login_name_customer_";
        /// <summary>
        /// 判断16进制字符串的正则
        /// </summary>
        private static string REGEX_HEX = "^[0-9A-Fa-f]+$";
        /// <summary>
        /// 16进制字符串
        /// </summary>
        //private static string HEXS = "0123456789ABCDEF";
        /// <summary>
        /// 获取一个 byte 数据的 16 进制值。
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string GetHex(byte b)
        {
            return b.ToString("X2");
        }
        /// <summary>
        /// 获取一个 byte 数组的 16 进制值。
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string GetHex(byte[] b)
        {
            string s = "";
            if (null != b)
            {
                for (int i = 0; i < b.Length; i++)
                {
                    s += GetHex(b[i]);
                }
            }
            return s;
        }
        /// <summary>
        /// 判断字符串是否全为16进制字符
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static bool IsHexString(string hexString)
        {
            return Regex.Match(hexString, REGEX_HEX).Success;
        }
        /// <summary>
        /// 将 16 进制字符串（最多 2 位）转换为相应的 ASCII 值。
        /// </summary>
        /// <param name="hexString">16 进制字符串。</param>
        /// <returns>返回字符串对应的 ASCII 值。</returns>
        public static byte GetByte(string hexString)
        {
            if (null == hexString || hexString.Length <= 0)
                throw new ArgumentNullException("Cannot convert null hex string");
            int len = hexString.Length;
            if (2 < len) {
                hexString = "0" + hexString;
            }
            return Convert.ToByte(hexString, 16);
        }
        /// <summary>
        /// 通过 16 进制字符串获取相应的 byte 数组
        /// </summary>
        /// <param name="hexString">16 进制字符串</param>
        /// <returns>byte 数组</returns>
        public static byte[] GetBytes(string hexString)
        {
            if (null == hexString || hexString.Length <= 0)
                throw new ArgumentNullException("Cannot convert null hex string");
            if (hexString.Length % 2 != 0)
                throw new Exception("Cannot convert the string to hex, the length is ODD.");
            int len = hexString.Length;
            byte[] ret = new byte[len / 2];
            for (int i = ret.GetLowerBound(0); i <= ret.GetUpperBound(0); i++)
            {
                ret[i] = GetByte(hexString.Substring(i * 2, 2));
            }
            return ret;
        }
        /// <summary>
        /// 获取一个指定范围内的随机数
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int GetRandom(int max)
        {
            Random rd = new Random();
            return rd.Next(max);
        }
        /// <summary>
        /// 获取客户端的IP地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetClientIP(HttpContext context)
        {
            string ip;
            try
            {
                if (context.Request.ServerVariables["HTTP_VIA"] != null) // 服务器， using proxy
                {
                    //得到真实的客户端地址
                    ip = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString(); 
                    // Return real client IP.
                }
                else//如果没有使用代理服务器或者得不到客户端的ip not using proxy or can't get the Client IP
                {

                    //得到服务端的地址
                    ip = context.Request.ServerVariables["REMOTE_ADDR"].ToString(); 
                    //While it can't get the Client IP, it will return proxy IP.
                }
            }
            catch
            {
                ip = "";
            }
            return ip;
        }
        /// <summary>
        /// 获取客户端浏览器类型和版本号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetClientBrowser(HttpRequest request)
        {
            return request.Browser.Browser + ", " + request.Browser.Version;
        }
        /// <summary>
        /// 获取一个字符串的 MD5 值
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5(string str)
        {
            byte[] b = Encoding.Default.GetBytes(str);
            return MD5(b);
        }
        /// <summary>
        /// 获取一个二进制数组的MD5值
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string MD5(byte[] b)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(b);
            return GetHex(md5.Hash);
        }
        /// <summary>
        /// 编码含有特殊字符的url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string UrlEncode(string url)
        {
            if (url.IndexOf("%") >= 0) return url;
            return HttpUtility.UrlEncode(url);
        }
        /// <summary>
        /// 解码含有特殊字符的字符串url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string UrlDecode(string url)
        {
            if (url.IndexOf("%") >= 0)
                return HttpUtility.UrlDecode(url);
            return url;
        }
        /// <summary>
        /// 将明文字符串加密成16进制字符串
        /// </summary>
        /// <param name="plainText">明文</param>
        /// <returns>返回加密过后的密文Base64字符串</returns>
        public static string Encrypt(string plainText)
        {
            return Convert.ToBase64String(EncryptAES(plainText));
        }
        /// <summary>
        /// 将密文的Base64字符串解密成明文
        /// </summary>
        /// <param name="key">已加密过后的密文字符串(Base64)</param>
        /// <returns>返回明文字符串</returns>
        public static string Decrypt(string key)
        {
            return DecryptAES(Convert.FromBase64String(UrlDecode(key)));
        }
        /// <summary>
        /// 加密明文字符串
        /// </summary>
        /// <param name="plainText">明文字符串</param>
        /// <returns>返回加密过后的二进制数组</returns>
        private static byte[] EncryptAES(string plainText)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("Cannot encrypt null text.");

            // Declare the stream used to encrypt to an in memory
            // array of bytes.
            MemoryStream msEncrypt = null;

            // Declare the RijndaelManaged object
            // used to encrypt the data.
            RijndaelManaged aesAlg = null;

            try
            {
                // Create a RijndaelManaged object
                // with the specified key and IV.
                aesAlg = new RijndaelManaged();
                aesAlg.Key = _RijndaelManaged.Key;
                aesAlg.IV = _RijndaelManaged.IV;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                msEncrypt = new MemoryStream();
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {

                        //Write all data to the stream.
                        swEncrypt.Write(plainText);
                    }
                }
            }
            finally
            {
                // Clear the RijndaelManaged object.
                if (aesAlg != null)
                    aesAlg.Clear();
            }

            // Return the encrypted bytes from the memory stream.
            return msEncrypt.ToArray();
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        private static string DecryptAES(byte[] cipherText)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("Cannot decrypt null data.");

            // Declare the RijndaelManaged object
            // used to decrypt the data.
            RijndaelManaged aesAlg = null;

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            try
            {
                // Create a RijndaelManaged object
                // with the specified key and IV.
                aesAlg = new RijndaelManaged();
                aesAlg.Key = _RijndaelManaged.Key;
                aesAlg.IV = _RijndaelManaged.IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
            finally
            {
                // Clear the RijndaelManaged object.
                if (aesAlg != null)
                    aesAlg.Clear();
            }

            return plaintext;
        }

        private static string EventSource = ConfigurationManager.AppSettings["ServiceEventName"];
        private static string EventName = ConfigurationManager.AppSettings["EventName"];
        private static int EventID = 65533;
        private static int CanLogEvent = int.Parse(ConfigurationManager.AppSettings["EventLogStatus"]);
        /// <summary>
        /// 写入系统日志(EventLogStatus不为0时才会写入系统日志中)
        /// </summary>
        /// <param name="EventString">日志内容</param>
        public static void WriteEventLog(string EventString)
        {
            if (!EventLog.SourceExists(EventSource))
                EventLog.CreateEventSource(EventSource, EventName);

            if (CanLogEvent > 0)
            {
                EventLog.WriteEntry(EventSource, EventString, EventLogEntryType.Information, EventID);
            }
        }
        private static System.Text.RegularExpressions.Regex devices = new System.Text.RegularExpressions.Regex(@"android|avantgo|blackberry|blazer|compal|elaine|" +
                    "fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\\/|plucker|pocket|" +
                    "psp|symbian|treo|up\\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino",
                    System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Multiline);
        //Create a list of all mobile types
        private static readonly string[] mobiles = new[] { "midp", "j2me", "avant", "docomo", "novarra", "palmos", "palmsource", "240x320", 
                    "opwv", "chtml", "pda", "windows ce", "mmp/", "blackberry", "mib/", "symbian", "wireless", "nokia", 
                    "hand", "mobi", "phone", "cdm", "up.b", "audio", "SIE-", "SEC-", "samsung", "HTC", "mot-", "mitsu", 
                    "sagem", "sony", "alcatel", "lg", "eric", "vx", "NEC", "philips", "mmm", "xx", "panasonic", "sharp", 
                    "wap", "sch", "rover", "pocket", "benq", "java", "pt", "pg", "vox", "amoi", "bird", "compal", "kg", 
                    "voda", "sany", "kdd", "dbt", "sendo", "sgh", "gradi", "jb", "dddi", "moto", "iphone" };
        /// <summary>
        /// 判断当前是否为移动端设备
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static bool IsMobile(System.Web.HttpContext context)
        {
            //FIRST TRY BUILT IN ASP.NT CHECK
            if (context.Request.Browser.IsMobileDevice)
            {
                return true;
            }
            //THEN TRY CHECKING FOR THE HTTP_X_WAP_PROFILE HEADER
            if (context.Request.ServerVariables["HTTP_X_WAP_PROFILE"] != null)
            {
                return true;
            }
            //THEN TRY CHECKING THAT HTTP_ACCEPT EXISTS AND CONTAINS WAP
            if (context.Request.ServerVariables["HTTP_ACCEPT"] != null &&
                context.Request.ServerVariables["HTTP_ACCEPT"].ToLower().Contains("wap"))
            {
                return true;
            }

            //AND FINALLY CHECK THE HTTP_USER_AGENT 
            //HEADER VARIABLE FOR ANY ONE OF THE FOLLOWING
            if (context.Request.ServerVariables["HTTP_USER_AGENT"] != null)
            {
                //根据HTTP_USER_AGENT，用正则表达式来判断是否是手机在访问
                string agent = context.Request.ServerVariables["HTTP_USER_AGENT"];
                
                System.Text.RegularExpressions.Regex v = new System.Text.RegularExpressions.Regex(@"1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s" +
                    "|a wa|abac|ac(er|oo|s\\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\\-m|r |s )|avan|" +
                    "be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\\-(n|u)|c55\\/|capi|ccwa|cdm\\-|cell|chtm|cldc|cmd\\-|co(mp|nd)|craw|" +
                    "da(it|ll|ng)|dbte|dc\\-s|devi|dica|dmob|do(c|p)o|ds(12|\\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|" +
                    "fly(\\-|_)|g1 u|g560|gene|gf\\-5|g\\-mo|go(\\.w|od)|gr(ad|un)|haie|hcit|hd\\-(m|p|t)|hei\\-|hi(pt|ta)|hp( i|ip)|hs\\-c|" +
                    "ht(c(\\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\\-(20|go|ma)|i230|iac( |\\-|\\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|" +
                    "jbro|jemu|jigs|kddi|keji|kgt( |\\/)|klon|kpt |kwc\\-|kyo(c|k)|le(no|xi)|lg( g|\\/(k|l|u)|50|54|e\\-|e\\/|\\-[a-w])|libw|" +
                    "lynx|m1\\-w|m3ga|m50\\/|ma(te|ui|xo)|mc(01|21|ca)|m\\-cr|me(di|rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\\-| |o|v)|zz)|" +
                    "mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|" +
                    "op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\\-2|po(ck|rt|se)|prox|psio|pt\\-g|qa\\-a|" +
                    "qc(07|12|21|32|60|\\-[2-7]|i\\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\\-|oo|p\\-)|sdk\\/|" +
                    "se(c(\\-|0|1)|47|mc|nd|ri)|sgh\\-|shar|sie(\\-|m)|sk\\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\\-|v\\-|v )|sy(01|mb)|" +
                    "t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\\-|tdg\\-|tel(i|m)|tim\\-|t\\-mo|to(pl|sh)|ts(70|m\\-|m3|m5)|tx\\-9|up(\\.b|g1|si)|utst|" +
                    "v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\\-| )|webc|whit|wi(g |nc|nw)|" +
                    "wmlb|wonu|x700|xda(\\-|2|g)|yas\\-|your|zeto|zte\\-",
                    System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Multiline);
                if ((devices.IsMatch(agent) || v.IsMatch(agent.Substring(0, 4))))
                {
                    return true;
                }

                //Loop through each item in the list created above 
                //and check if the header contains that text
                foreach (string s in mobiles)
                {
                    if (context.Request.ServerVariables["HTTP_USER_AGENT"].ToLower().Contains(s.ToLower()))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        /// <summary>
        /// 获取链接状态
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetOnlineStyle(byte? type)
        {
            var ret = "";
            if ((byte?)null == type)
                ret = "unknown";

            switch (type)
            {
                case 0x00:// OFF
                    ret = "<span class=\"label label-default\">battery off</span>";
                    break;
                case 0x10:// TCP
                    ret = "<span class=\"label label-info\">tcp</span>";
                    break;
                case 0x20:// UDP
                    ret = "<span class=\"label label-success\">udp</span>";
                    break;
                case 0x30:// SMS
                    ret = "<span class=\"label label-warning\">sms</span>";
                    break;
                case 0x40:// SLEEP
                    ret = "<span class=\"label label-warning\">sleep</span>";
                    break;
                case 0x50:// BLIND
                    ret = "<span class=\"label label-danger\">blind</span>";
                    break;
                case 0x60:// SATELLITE
                    ret = "<span class=\"label label-primary\">satellite</span>";
                    break;
                case 0xFF:// TROUBLE
                    ret = "<span class=\"label label-danger\">trouble</span>";
                    break;
            }
            return ret;
        }
    }
}