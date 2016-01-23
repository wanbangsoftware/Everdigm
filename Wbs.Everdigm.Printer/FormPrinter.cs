using System;
using System.Collections.Specialized;
using System.Windows.Forms;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Wbs.Label.Printer;
using Wbs.Everdigm.Common;
using Wbs.Everdigm.Database;
using Wbs.Utilities;
using System.Configuration;

namespace Wbs.Everdigm.Printer
{
    public partial class FormPrinter : Form
    {
        /// <summary>
        /// 标记是否退出程序
        /// </summary>
        private bool bExited = false;
        /// <summary>
        /// 是否处理终端标签
        /// </summary>
        private bool handleTerminal = false;

        private FormPrinter frmThis;

        public FormPrinter()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //log("perform to close");
            if (bExited)
            {
                Close();
            }
            else
            {
                bExited = true;
                exitToolStripMenuItem.Enabled = false;
                notifyIcon.ShowBalloonTip(3000, "Everdigm", "Perform to close, please wait...", ToolTipIcon.Warning);
            }
        }

        private void FormPrinter_Load(object sender, EventArgs e)
        {
            frmThis = this;
            //log("Start.");
            notifyIcon.ShowBalloonTip(3000, "Everdigm", "Iridium Label Printer", ToolTipIcon.Info);
            // 每次调用完timer之后需要暂停timer，以待程序执行完毕之后再次调用timer
            timer = new System.Threading.Timer(new TimerCallback(Timer_Callback), 0, TIMER_INTEVAL, 0);
        }
        /// <summary>
        /// 禁用或启用button
        /// </summary>
        /// <param name="disable"></param>
        private void DisableButtons(bool disable)
        {
            buttonIridiumLabel.Enabled = !disable;
            buttonTerminalLabel.Enabled = !disable;
        }
        private void buttonIridiumLabel_Click(object sender, EventArgs e)
        {
            PrintIridiumLabel(tempObj);
        }

        private void buttonTerminalLabel_Click(object sender, EventArgs e)
        {
            PrintTerminalLabel(tempTerminal);
        }
    }
}
