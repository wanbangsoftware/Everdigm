using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Management;
using System.Security.Cryptography;

namespace Wbs.Protocol
{
    /// <summary>
    /// 提供协议分析以及其基本数据转换功能
    /// </summary>
    public class ProtocolItems
    {
        /// <summary>
        /// TX 通讯协议中涉及到的 IP 地址的长度
        /// </summary>
        public const byte ServerIpLength = 0x04;
        /// <summary>
        /// TX 通讯协议中涉及到的 SMS 地址长度
        /// </summary>
        public const byte SmsAddressLength = 20;
        /// <summary>
        /// Sim 卡号码长度
        /// </summary>
        public const int SimNoLength = 11;
        /// <summary>
        /// 终端号码长度
        /// </summary>
        public const int TerminalNoLength = 10;
        /// <summary>
        /// 模块号码长度
        /// </summary>
        public const int ModuleNoLength = 15;
        /// <summary>
        /// 进制转换的数据常量。
        /// </summary>
        private const string scale_char = "0123456789ABCDEF";
        /// <summary>
        /// 进制转换中表示 16 进制。
        /// </summary>
        public const byte HEX = 16;
        /// <summary>
        /// 进制转换中表示 10 进制。
        /// </summary>
        public const byte DEC = 10;
        /// <summary>
        /// 进制转换中表示 2 进制。
        /// </summary>
        public const byte BIN = 2;
        /// <summary>
        /// 进制转换中表示 8 进制。
        /// </summary>
        public const byte OCT = 8;
        /// <summary>
        /// 将 int 数据转换为对应的进制格式，高位在前低位在后顺序。
        /// </summary>
        /// <param name="mNumber">需要转换的 int 数据。</param>
        /// <param name="mScale">需要转换成的进制。</param>
        /// <param name="mLength">转换后的数据长度。</param>
        /// <returns>返回按照指定进制转换后字符串。</returns>
        public static string IntToDigit(int mNumber, byte mScale, int mLength)
        {
            int i, j;
            i = mNumber;
            string s = "";
            while ((i >= mScale) && (mScale > 1))
            {
                j = i % mScale;
                i = i / mScale;
                s = scale_char[j] + s;
            }
            s = scale_char[i] + s;
            //if (mLength > 0)
            //{
            //    for (int k = 1; k <= mLength - s.Length; k++)
            //        s = "0" + s;
            //}
            while (s.Length < mLength)
                s = "0" + s;
            return s;
        }
        /// <summary>
        /// 将 string 类型的数据转换为对应的 int 数据。
        /// </summary>
        /// <param name="mDigit">字符串类型的数据。</param>
        /// <param name="mScale">需要转换成的进制。</param>
        /// <returns>返回按照进制转换后的 int 数值。</returns>
        public static int DigitToInt(string mDigit, byte mScale)
        {
            int ret;
            ret = 0;
            mDigit = mDigit.ToUpper();
            for (int i = 0; i < mDigit.Length; i++)
            {
                ret += scale_char.IndexOf(mDigit[mDigit.Length - i - 1]) * (int)Math.Pow(mScale, i);
            }
            return ret;
        }
        /// <summary>
        /// 将 16 进制表示的字符串转换成对应的 byte 数组。
        /// </summary>
        /// <param name="hex">16 进制字符串。</param>
        /// <returns>返回 byte 数组。</returns>
        public static byte[] GetBytes(string hex)
        {
            // 不足位时左补零补齐
            if ((hex.Length % 2) != 0)
            {
                hex = "0" + hex;
            }
            byte[] b = new byte[hex.Length / 2];
            for (int i = 0; i < b.Length; i++)
            {
                b[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return b;
        }
        /// <summary>
        /// 将二进制流转换为等价的 16 进制字符串。
        /// </summary>
        /// <param name="b">二进制流。</param>
        /// <returns>等价的 16 进制字符串。</returns>
        public static string GetHex(byte[] b)
        {
            string s = "";
            if (b != null)
            {
                for (int i = 0; i < b.Length; i++)
                    s += GetHex(b[i]);
            }
            return s;
        }
        /// <summary>
        /// 将 byte 值转换为对应的 16 进制字符串。
        /// </summary>
        /// <param name="b">待转换的 byte 值。</param>
        /// <returns>返回与参数等值的 16 进制字符串。</returns>
        public static string GetHex(byte b)
        {
            return b.ToString("X2");
        }
        /// <summary>
        /// 将 ushort 值转换为等值的 16 进制字符串。
        /// </summary>
        /// <param name="value">待转换的 ushort 值。</param>
        /// <returns>返回与参数等值的 16 进制字符串。</returns>
        public static string GetHex(ushort value)
        {
            byte[] b = BitConverter.GetBytes(value);
            string s = "";
            int i = b.GetUpperBound(0);
            while (i >= 0)
            {
                s += GetHex(b[i]);
                i--;
            }
            return s;
        }
        /// <summary>
        /// 将 uing 值转换为等值的 16 进制字符串。
        /// </summary>
        /// <param name="value">待转换的 uing 值。</param>
        /// <returns>返回与参数等值的 16 进制字符串。</returns>
        public static string GetHex(uint value)
        {
            byte[] b = BitConverter.GetBytes(value);
            string s = "";
            int i = b.GetUpperBound(0);
            while (i >= 0)
            {
                s += GetHex(b[i]);
                i--;
            }
            return s;
        }
        /// <summary>
        /// 获取指定二进制数组的校验和。
        /// </summary>
        /// <param name="b">指定的二进制数组。</param>
        /// <returns>返回校验和。</returns>
        public static byte GetCheckSum(byte[] b)
        {
            byte sum = 0x00;
            for (int i = 0; i < b.Length; i++)
            {
                sum += b[i];
            }
            return sum;
        }
        /// <summary>
        /// 获取校验和。
        /// </summary>
        /// <param name="b">二进制数组。</param>
        /// <returns>返回 XOR 校验和。</returns>
        public static byte GetXor(byte[] b)
        {
            byte sum = 0x00;
            for (int i = 0; i < b.Length; i++)
            {
                sum ^= b[i];
            }
            return sum;
        }
        /// <summary>
        /// 扩展 byte 数组。
        /// </summary>
        /// <param name="b">原二进制数组。</param>
        /// <param name="new_len">新的数组长度。</param>
        /// <returns>返回新长度的数组。</returns>
        public static byte[] expand(byte[] src, int new_len)
        {
            byte[] b = new byte[new_len];
            if (new_len > src.Length)
                Buffer.BlockCopy(src, 0, b, 0, src.Length);
            else
                Buffer.BlockCopy(src, 0, b, 0, new_len);
            return b;
        }
        /// <summary>
        /// 反转字节数组。
        /// </summary>
        /// <param name="b">字节数组。</param>
        /// <returns>顺序颠倒的字节数组。</returns>
        public static byte[] reserve(byte[] b)
        {
            byte[] r = new byte[b.Length];
            for (int i = 0; i < b.Length; i++)
            {
                r[i] = b[b.Length - 1 - i];
            }
            return r;
        }
        /// <summary>
        /// 反转字符串。
        /// </summary>
        /// <param name="s">需要反转的字符串。</param>
        /// <returns>返回已经反转的字符串。</returns>
        public static string reserve(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
        
        /// <summary>
        /// 获取本机的 MAC 地址。
        /// </summary>
        /// <returns></returns>
        public static string getMacAddress()
        {
            string ret = "";
            ManagementObjectSearcher query = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection queryCollection = query.Get();
            foreach (ManagementObject mo in queryCollection)
            {
                if (mo["IPEnabled"].ToString() == "True")
                    ret = mo["MacAddress"].ToString();
            }
            return ret;
        }
        /// <summary>
        /// 获取IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetIPAddress()
        {
            try
            {
                //获取IP地址   
                string st = "";
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        //st=mo["IpAddress"].ToString();   
                        System.Array ar;
                        ar = (System.Array)(mo.Properties["IpAddress"].Value);
                        st = ar.GetValue(0).ToString();
                        break;
                    }
                }
                moc = null;
                mc = null;
                return st;
            }
            catch
            {
                return "unknow";
            }
            finally
            {
            }
        }
        /// <summary>
        /// 获取本机 CPU ID信息。
        /// </summary>
        /// <returns></returns>
        public static string getCpuId()
        {
            string cpuInfo = String.Empty;
            string temp = String.Empty;
            ManagementClass mc = new ManagementClass("Win32_Processor");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                //Debug.WriteLine("Processor Caption: " + mo.Properties["Caption"].Value.ToString());
                //Debug.WriteLine("Processor MaxClockSpeed: " + mo.Properties["MaxClockSpeed"].Value.ToString());
                if (cpuInfo == String.Empty)
                {
                    cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
                }
            }
            return cpuInfo;
        }
        /// <summary>
        /// 通过 MD5 加密方法加密字符串。
        /// </summary>
        /// <param name="encrypt_str">源码字符串。</param>
        /// <returns>返回经过 MD5 加密的密文字符串（16进制代码）。</returns>
        public static string MD5(string encrypt_str)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] b = Encoding.Default.GetBytes(encrypt_str);
            md5.ComputeHash(b);
            string res = "";
            foreach (byte bb in md5.Hash)
            {
                res += bb.ToString("X2");
            }
            return res;
        }
        /// <summary>
        /// 将 GPRMC 格式的 GPS 数据转换成度分格式
        /// </summary>
        /// <param name="gps"></param>
        /// <returns></returns>
        public static double GPRMC2DDMMMM(double gps)
        {
            // 3732.1024
            int dd = (int)(gps / 100.0);
            double mm = gps - (dd * 100);
            return dd + mm / 60.0;
        }
    }
}
