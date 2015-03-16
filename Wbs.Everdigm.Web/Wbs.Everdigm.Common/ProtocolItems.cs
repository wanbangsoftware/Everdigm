using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wbs.Everdigm.Common
{
    /// <summary>
    /// 终端数据协议中一些用到的方法
    /// </summary>
    public class ProtocolItems
    {
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
        /// 扩展byte[]数组。
        /// </summary>
        /// <param name="res">源数组。</param>
        /// <param name="new_len">新的数组长度。</param>
        /// <returns>返回新的包含原有数据的数组。</returns>
        public static byte[] expand(byte[] res, int new_len)
        {
            byte[] b = new byte[new_len];
            if (res.Length > new_len)
                Buffer.BlockCopy(res, 0, b, 0, new_len);
            else
                Buffer.BlockCopy(res, 0, b, 0, res.Length);
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
            for (int i = 0; i < b.Length; i++)
                s += b[i].ToString("X2");
            return s;
        }
        /// <summary>
        /// 将 16 进制表示的字符串转换成 byte 数组。
        /// </summary>
        /// <param name="hex">16 进制字符串。</param>
        /// <returns>返回 byte 数组。</returns>
        public static byte[] GetBytes(string hex)
        {
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
    }
}
