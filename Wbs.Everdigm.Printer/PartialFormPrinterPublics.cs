using System;
using System.Threading;

namespace Wbs.Everdigm.Printer
{
    /// <summary>
    /// 基本方法的集合
    /// </summary>
    public partial class FormPrinter
    {
        private string Now
        {
            get { return DateTime.Now.ToString("[yyyy/MM/dd HH:mm:ss] "); }
        }

        private string Tick
        {
            get { return DateTime.Now.Ticks.ToString(); }
        }

        private void log(string text)
        {
            frmThis.Invoke(new Action(() =>
            {
                textBox.Text += string.Format("{0}{1}\r\n", Now, text);
                textBox.SelectionStart = textBox.Text.Length;
                textBox.ScrollToCaret();
            }));
        }

        private static string format(string format, params object[] args)
        {
            return string.Format(format, args);
        }
        /// <summary>
        /// 确定计时器继续进行还是退出UI
        /// </summary>
        private void PerformExitOrContinue()
        {
            if (!bExited)
            {
                timer.Change(TIMER_INTEVAL, 0);
            }
            else
            {
                if (null != timer)
                {
                    timer.Change(Timeout.Infinite, Timeout.Infinite);
                    timer.Dispose();
                    timer = null;
                }
                frmThis.Invoke(new Action(() => { Close(); }));
            }
        }
    }
}
