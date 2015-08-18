using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Wbs.Utilities;
using Wbs.Protocol;
using Wbs.Everdigm.BLL;
using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.Web
{
    /// <summary>
    /// 画出指定设备在指定日期中的工作情况
    /// </summary>
    public class TodayWorktimeChart
    {
        /// <summary>
        /// 柱形高度
        /// </summary>
        private int _barHeight = 7;
        /// <summary>
        /// 启动时间计算时间间隔，每5分钟为一个刻度
        /// </summary>
        private int _interval = 5;
        /// <summary>
        /// 默认图片高度，35px
        /// </summary>
        private int _height = 35;
        /// <summary>
        /// 设置图片的高度
        /// </summary>
        public int Height
        {
            get { return _height; }
            set
            {
                if (value > 0)
                    _height = value;
            }
        }
        /// <summary>
        /// 默认图片的宽度，240px
        /// </summary>
        private int _width = 240;
        /// <summary>
        /// 设置图片的宽度
        /// </summary>
        public int Width
        {
            get { return _width; }
            set
            {
                if (value > 0)
                    _width = value;
            }
        }
        /// <summary>
        /// 设备的ID
        /// </summary>
        private string _equipment = "";
        /// <summary>
        /// 设置设备号码
        /// </summary>
        public string Equipment { set { _equipment = value; } }
        /// <summary>
        /// 默认画出当日的运转情况
        /// </summary>
        private string _date = DateTime.Now.ToString("yyyy/MM/dd");
        private DateTime _start, _end;
        /// <summary>
        /// 设置日期
        /// </summary>
        public string Date
        {
            set
            {
                _date = value;
                _start = DateTime.Parse(_date + " 00:00:00");
                _end = DateTime.Parse(_date + " 23:59:59");
            }
        }

        /// <summary>
        /// 画图
        /// </summary>
        /// <param name="target"></param>
        public void Chart(Stream target)
        {
            chart(target);
        }
        /// <summary>
        /// 查询历史记录并开始画图
        /// </summary>
        /// <param name="target"></param>
        private void chart(Stream target)
        {
            byte[,] times = new byte[24, 60 / _interval];
            Int64 total_seconds = 0;
            // 初始化数组
            for (int i = 0; i < times.GetLength(0); i++)
                for (int j = 0; j < times.GetLength(1); j++)
                    times[i, j] = 10;
            // 获取数据库中今日启动情况
            var data = new DataBLL();
            var list = data.FindList(h => h.receive_time >= _start && h.receive_time <= _end && h.mac_id.Equals(_equipment) &&
                (h.command_id.Equals("0x5000") || h.command_id.Equals("0x1000"))).OrderBy(o => o.receive_time);

            // 当日开机时间默认为凌晨零点。
            TimeSpan lastOpen = new TimeSpan(_start.Ticks), lastClose;
            var lastIsOpen = true;
            var terType = 0;
            foreach (var obj in list)
            {
                // 非卫星方式回来的1000命令不处理
                if (obj.command_id.Equals("0x1000"))
                {
                    if (obj.protocol_type != ProtocolTypes.SATELLITE)
                        continue;
                    if (obj.message_content.Substring(0, 2) != "01")
                        continue;
                }
                if (terType == 0)
                {
                    var t = new TerminalBLL().Find(find => obj.terminal_id.IndexOf(find.Sim) >= 0);
                    if (null != t)
                    {
                        terType = (byte)t.Type;
                    }
                }
                string str = "";
                DateTime dt;
                int hh, mm;

                //while (sdr.Read())
                //{
                if (obj.protocol_type == ProtocolTypes.SATELLITE)
                {
                    byte[] msgc = CustomConvert.GetBytes(obj.message_content);
                    string bin = CustomConvert.IntToDigit(msgc[4], CustomConvert.BIN, 8);
                    msgc = null;
                    str = bin[6] == '1' ? "F000" : "0000";
                }
                else
                {
                    str = terType == TerminalTypes.DX ? obj.message_content.Substring(0, 4) :
                        (obj.message_content.Substring(8, 2) == "00" ? "F000" : "0000");
                }
                dt = obj.receive_time.Value;
                hh = Convert.ToInt32(dt.ToString("HH"));
                mm = Convert.ToInt32(dt.ToString("mm")) / _interval;
                // 开机
                if (0 < str.CompareTo("0000"))
                {
                    times[hh, mm] = 1;
                    lastOpen = new TimeSpan(dt.Ticks);
                    lastIsOpen = true;
                }
                else
                {
                    times[hh, mm] = 0;
                    lastClose = new TimeSpan(dt.Ticks);
                    if (lastIsOpen)
                    {
                        lastIsOpen = false;
                        total_seconds += (long)(lastClose - lastOpen).Duration().TotalSeconds;
                    }
                }
                //}
            }
            // 格式化显示今日的总开机时间
            total_seconds = total_seconds / 60;
            string total_runtime = (total_seconds / 60) + " h " + (total_seconds % 60)+" m";
            // 添加表格
            bool is_open = false;
            int hn, mn;
            hn = Convert.ToInt32(DateTime.Now.ToString("HH"));
            mn = Convert.ToInt32(DateTime.Now.ToString("mm")) / _interval;
            Bitmap b = new Bitmap(_width, _height);
            Graphics g = Graphics.FromImage(b);
            // 填充白色背景
            g.Clear(Color.White);
            Pen p = new Pen(Color.FromArgb(0x00, 0x99, 0xCC));
            Font f = new System.Drawing.Font("Arial", 9);
            Brush br = new SolidBrush(Color.FromArgb(0x00, 0x99, 0xCC));
            int font_height = (int)g.MeasureString(_date, f).Height;
            // 画日期提示
            g.DrawString(_date + ": " + total_runtime, f, br, 0, 0);
            // 画方框
            g.DrawRectangle(p, 0, font_height, b.Width + 1, _barHeight);
            // 每一个区域块的宽度
            int per_width = b.Width / (times.GetLength(0) * times.GetLength(1));
            int ct = 0;
            bool kedu = false;
            for (int i = 0; i < times.GetLength(0); i++)
            {
                kedu = false;
                for (int j = 0; j < times.GetLength(1); j++)
                {
                    if (times[i, j] == 1)
                        is_open = true;
                    if (times[i, j] == 0)
                        is_open = false;
                    if (i == hn)
                    {
                        if (j >= mn)
                            is_open = false;
                    }

                    Rectangle r = new Rectangle(ct * per_width, font_height, per_width, _barHeight);
                    if (is_open)
                    {
                        // 填充为实心区域
                        g.FillRectangle(br, r);
                        // 画底部实线
                        g.DrawLine(p, r.Left, r.Top + _barHeight, r.Left + r.Width, r.Top + _barHeight);
                    }
                    else
                    {
                        // 画顶部实线
                        g.DrawLine(p, r.Left, r.Top, r.Left + r.Width, r.Top);
                        // 画底部实线
                        g.DrawLine(p, r.Left, r.Top + _barHeight, r.Left + r.Width, r.Top + _barHeight);
                    }
                    //if (i % 4 == 0)
                    {
                        if (!kedu)
                        {
                            kedu = true;
                            // 画刻度
                            g.DrawLine(p, r.Left, r.Top + _barHeight, r.Left, b.Height);
                            // 显示刻度数字
                            g.DrawString(i.ToString("00"), new Font("tahoma", 8), br, r.Left + 1, r.Top + _barHeight + 1);
                        }
                    }
                    // 画最右边的竖线
                    if (i == times.GetUpperBound(0) && j == times.GetUpperBound(1))
                    {
                        g.DrawLine(p, r.Left + r.Width, r.Top, r.Left + r.Width, b.Height);
                    }
                    ct++;
                }
            }

            b.Save(target, ImageFormat.Png);
            p.Dispose();
            g.Dispose();
            b.Dispose();
        }
    }
}