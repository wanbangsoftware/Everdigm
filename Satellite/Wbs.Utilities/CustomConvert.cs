using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wbs.Utilities
{
    /// <summary>
    /// 一些数据转换方法
    /// </summary>
    public static class CustomConvert
    {
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
        /// <param name="mScale">字符串的原始进制类型。</param>
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
        /// 将指定字节数组中指定长度的内容转换成 16 进制字符串
        /// </summary>
        /// <param name="b"></param>
        /// <param name="start"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string GetHex(byte[] b, int start, int len)
        {
            int L = b.Length;
            if (start < 0 || start > L) return "";
            if (start + len > L) return "";
            string s = "";
            for (int i = start; i < (start + len); i++)
                s += GetHex(b[i]);
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
        /// 获取校验和
        /// </summary>
        /// <param name="b"></param>
        /// <param name="start"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static byte GetXor(byte[] b, int start, int len)
        {
            var L = b.Length;
            if (start < 0 || start > L) return 0x00;
            if (len < 0) return 0x00;
            if ((start + len) > L) return 0x00;

            byte xor = 0x00;
            for (int i = start; i < (start + len); i++)
                xor ^= b[i];

            return xor;
        }
        /// <summary>
        /// 扩展 byte 数组。
        /// </summary>
        /// <param name="b">原二进制数组。</param>
        /// <param name="new_len">新的数组长度。</param>
        /// <returns>返回新长度的数组。</returns>
        public static byte[] expand(byte[] src, int new_len)
        {
            if (null == src) return new byte[new_len];

            var oLen = src.Length;
            byte[] b = new byte[new_len];
            if (new_len > oLen)
                Buffer.BlockCopy(src, 0, b, 0, oLen);
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
        /// 将byte序列转换成字符串，非可显示字符用.代替
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string BytesToASCII(byte[] data)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0, len = data.Length; i < len; i++)
            {
                var b = data[i];
                if ((b >= 0x20 && b < 0x7F))
                    sb.Append(ASCIIEncoding.ASCII.GetString(data, i, 1));
                else
                    sb.Append('.');
            }
            return sb.ToString();
        }
    }
}
