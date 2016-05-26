using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Configuration;

namespace Wbs.Everdigm.Desktop
{
    /// <summary>
    /// 历史记录保存处理部分
    /// </summary>
    public partial class FormMain
    {
        /// <summary>
        /// 自定义委托类型
        /// </summary>
        private delegate void MyInvoker();
        /// <summary>
        /// 保存历史记录的计时器
        /// </summary>
        private System.Threading.Timer _timerSave;

        private uint _timerCounter = 0;

        /// <summary>
        /// 获取当前时间
        /// </summary>
        private string Now { get { return DateTime.Now.ToString("[yyyy/MM/dd HH:mm:ss.fff] "); } }

        private string MAP_URL = ConfigurationManager.AppSettings["FETCH_MAP_URL"];

        /// <summary>
        /// 显示历史记录
        /// </summary>
        /// <param name="history"></param>
        private void ShowHistory(string history, bool showTime)
        {
            rtbHistory.BeginInvoke((MyInvoker)delegate
            {
                try
                {
                    if (history.IndexOf("position: ") == 0)
                    {
                        // 捕获位置信息
                        if (!tsmiStopFetchingAddress.Checked)
                        {
                            browser.Navigate(MAP_URL + history.Replace("position: ", "") + "&time=" + DateTime.Now.Ticks);
                        }
                    }
                    else
                    {
                        if (tsmiShowHistory.Checked)
                        {
                            rtbHistory.AppendText((showTime ? Now : "") + history + Environment.NewLine);
                            rtbHistory.SelectionStart = rtbHistory.Text.Length;
                            rtbHistory.ScrollToCaret();
                        }
                    }
                }
                catch { }
            });
        }

        /// <summary>
        /// 保存历史记录
        /// </summary>
        /// <param name="sender"></param>
        private void SaveHistory(object sender)
        {
            _timerCounter++;
            // 一小时保存一次
            if (_timerCounter % 360 == 0)
            {
                _timerCounter = 0;
                SaveFile();
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        private void SaveFile()
        {
            rtbHistory.BeginInvoke((MyInvoker)delegate
            {
                if (rtbHistory.Text.Length < 1) return;

                var ret = SaveFile("data\\" + DateTime.Now.ToString("yyyyMM") + "\\" + DateTime.Now.ToString("yyyyMMdd") + "_history.txt", rtbHistory.Text);
                if (string.IsNullOrEmpty(ret))
                {
                    rtbHistory.Clear();
                }
                else
                {
                    ShowHistory(ret, true);
                }
            });
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        private string SaveFile(string path, string text)
        {
            var ret = "";
            if (string.IsNullOrEmpty(text)) return ret;

            var filePath = Application.StartupPath + "\\" + path;
            var p = filePath.Substring(0, filePath.LastIndexOf("\\"));
            if (!Directory.Exists(p))
            {
                Directory.CreateDirectory(p);
            }
            using (FileStream fs = File.Open(filePath, FileMode.Append, FileAccess.Write))
            {
                try
                {
                    var bytes = Encoding.UTF8.GetBytes(text);
                    fs.Write(bytes, 0, bytes.Length);
                }
                catch (Exception e)
                {
                    ret = "保存文件失败：" + e.Message;
                }
                finally
                {
                    fs.Close();
                }
            }
            return ret;
        }

    }
}
