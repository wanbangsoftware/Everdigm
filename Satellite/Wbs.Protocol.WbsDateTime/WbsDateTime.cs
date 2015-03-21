using System;
using System.Collections.Generic;
using System.Text;

namespace Wbs.Protocol.WbsDateTime
{
    /// <summary>
    /// 按照通讯规约将时间打包成 4 个字节或者将 4 个字节的数据解析成有效的时间格式。
    /// </summary>
    public class WbsDateTime
    {
        private DateTime datetime;
        private byte[] b_datetime = new byte[4];
        private string[] bin_str;
        private string str_bin = "";

        private int year, month, day, hour, minute, second;
        private const short year_len = 6;
        private const short month_len = 4;
        private const short day_len = 5;
        private const short hour_len = day_len;
        private const short minute_len = year_len;
        private const short second_len = year_len;

        private bool bin_to_time = false;

        /// <summary>
        /// 默认当前系统时间为参数进行字节转换
        /// </summary>
        public WbsDateTime()
        {
            ini_bin_str();
            datetime = DateTime.Now;
            bin_to_time = false;
            ConvertDateTime();
        }

        /// <summary>
        /// 传入时间作为参数进行字节转换
        /// </summary>
        /// <param name="time">需要转换成字节的时间</param>
        public WbsDateTime(DateTime time)
        {
            ini_bin_str();
            datetime = time;
            bin_to_time = false;
            ConvertDateTime();
        }

        /// <summary>
        /// 传入字符串格式的时间进行字节转换
        /// </summary>
        /// <param name="time">需要转换成字节的时间，需要有效的时间的字符串格式，请参见 MSDN 中的 DateTime 类。</param>
        public WbsDateTime(string time)
        {
            bool is_error = false;
            try
            {
                datetime = DateTime.Parse(time);
            }
            catch
            {
                is_error = true;
                throw new System.Exception("传入的字符串不是有效的时间格式。");
            }
            if (!is_error)
            {
                ini_bin_str();
                bin_to_time = false;
                ConvertDateTime();
            }
        }

        /// <summary>
        /// 传入字节作为参数获取时间
        /// </summary>
        /// <param name="time"></param>
        public WbsDateTime(byte[] time)
        {
            ini_bin_str();
            bin_to_time = true;
            if (time.Length != 4)
            {
                throw new System.Exception("时间的格式（4 bytes）不正确。");
            }
            else if (time == null)
            {
                throw new System.Exception("输入的参数不能为空。");
            }
            else
            {
                Buffer.BlockCopy(time, 0, b_datetime, 0, b_datetime.Length);
                ConvertDateTime();
            }
        }

        /// <summary>
        /// 初始化 0 ～ 15 的二进制表示方式
        /// </summary>
        private void ini_bin_str()
        {
            bin_str = new string[16];
            for (int i = 0; i < 16; i++)
            {
                bin_str[i] = FillBinString(Convert.ToString(i, 2), 4);
            }
        }

        /// <summary>
        /// 在时间格式和字节格式中转换。
        /// </summary>
        private void ConvertDateTime()
        {
            if (!bin_to_time)
            {
                DateTimeToBin();
            }
            else
            {
                BinToDateTime();
            }
        }

        /// <summary>
        /// byte 数组转换成时间表示方式。
        /// </summary>
        private void BinToDateTime()
        {
            str_bin = "";
            //将字节转换为 16 进制字符串
            string hex_ = "";
            for (int i = 0; i < b_datetime.Length; i++)
            {
                hex_ = hex_ + b_datetime[i].ToString("X2");
            }
            //将 16 进制字符串转成二进制字符串
            for (int i = 0; i < hex_.Length; i++)
            {
                str_bin = str_bin + bin_str[Convert.ToInt32(hex_.Substring(i, 1), 16)];
            }

            int iIndex = 0;
            //获取年份的数字表示方式
            year = Convert.ToUInt16(str_bin.Substring(iIndex, year_len), 2);
            year += 2000;
            iIndex += year_len;

            //获取月份的数字表示方式
            month = Convert.ToUInt16(str_bin.Substring(iIndex, month_len), 2);
            iIndex += month_len;

            //获取日期的数字标示方式
            day = Convert.ToUInt16(str_bin.Substring(iIndex, day_len), 2);
            iIndex += day_len;

            //获取小时的数字标示方式
            hour = Convert.ToUInt16(str_bin.Substring(iIndex, hour_len), 2);
            iIndex += hour_len;

            //获取分钟的数字表示方式
            minute = Convert.ToUInt16(str_bin.Substring(iIndex, minute_len), 2);
            iIndex += minute_len;

            //获取秒的数字表示方式
            second = Convert.ToUInt16(str_bin.Substring(iIndex, second_len), 2);
            iIndex += second_len;

            string d = year + "-" + month + "-" + day + " " + hour + ":" + minute + ":" + second;

            try
            {
                datetime = DateTime.Parse(d);
            }
            catch
            {
                //throw new System.Exception("输入的 byte 字符串不是有效的时间格式。");
                datetime = new DateTime(2000, 1, 1, 0, 0, 0);
            }
        }

        /// <summary>
        /// 填充满各个时间格式的二进制字符串。
        /// </summary>
        /// <param name="value">二进制字符串。</param>
        /// <param name="len">将 value 扩展该字符串的最大长度。</param>
        /// <returns>新的已经在字符串前面填充 0 的新字符串。</returns>
        private string FillBinString(string value, int len)
        {
            string str = "";
            if (value.Length > len)
            {
                str = value.Substring(0, len);
            }
            else
            {
                str = value;
                for (int i = 0; i < len - value.Length; i++)
                {
                    str = "0" + str;
                }
            }
            return str;
        }

        /// <summary>
        /// 时间格式转换成 byte 数组。
        /// </summary>
        private void DateTimeToBin()
        {
            //从时间中分离出年月日时分秒数据
            year = datetime.Year - 2000;
            month = datetime.Month;
            day = datetime.Day;
            hour = datetime.Hour;
            minute = datetime.Minute;
            second = datetime.Second;
            string bin = "";
            str_bin = "";
            //年份的二进制表示方式
            bin = FillBinString(Convert.ToString(year, 2), year_len);
            str_bin += bin;

            //月份的二进制表示方式
            bin = FillBinString(Convert.ToString(month, 2), month_len);
            str_bin += bin;

            //日期的二进制表示方式
            bin = FillBinString(Convert.ToString(day, 2), day_len);
            str_bin += bin;

            //小时的二进制表示方式
            bin = FillBinString(Convert.ToString(hour, 2), hour_len);
            str_bin += bin;

            //分钟的二进制表示方式
            bin = FillBinString(Convert.ToString(minute, 2), minute_len);
            str_bin += bin;

            //秒钟的二进制表示方式
            bin = FillBinString(Convert.ToString(second, 2), second_len);
            str_bin += bin;

            //将二进制字符串保存到字节数组中
            for (int i = 0; i < b_datetime.Length; i++)
            {
                b_datetime[i] = Convert.ToByte(str_bin.Substring(i * 8, 8), 2);
            }
        }

        /// <summary>
        /// 获取字节顺序的时间
        /// </summary>
        public byte[] DateTimeToByte
        {
            get { return b_datetime; }
        }

        /// <summary>
        /// 通过字节获取时间
        /// </summary>
        public DateTime ByteToDateTime
        {
            get { return datetime; }
        }
    }
}
