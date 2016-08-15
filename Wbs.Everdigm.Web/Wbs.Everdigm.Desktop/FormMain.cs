using System;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using System.Configuration;
using System.Net.Sockets;
using Wbs.Utilities;

namespace Wbs.Everdigm.Desktop
{
    public partial class FormMain : Form
    {
        /// <summary>
        /// 标记是否真的关闭窗口
        /// </summary>
        private bool IsClose = false;
        
        public FormMain()
        {
            InitializeComponent();
            ThreadPool.RegisterWaitForSingleObject(Program.ProgramStarted, OnProgramStarted, null, -1, false);
            var width = Screen.PrimaryScreen.Bounds.Width;
            var height = Screen.PrimaryScreen.Bounds.Height;
            Width = (int)((width * 1.0) * 2 / 3);
            Height = (int)(height * 1.0 * 4 / 5);
        }
        /// <summary>
        /// 当收到第二个进程的通知时，显示窗体
        /// </summary>
        /// <param name="state"></param>
        /// <param name="timeout"></param>
        private void OnProgramStarted(object state, bool timeout)
        {
            notifyIcon.ShowBalloonTip(5000, "Hey! I'm here.", "Double click me to show main window.", ToolTipIcon.Info);
            //notifyIcon_DoubleClick(this, EventArgs.Empty);
        }
        /// <summary>
        /// 初始化并启动服务
        /// </summary>
        public void StartService()
        {
            try
            {
                // 启动socket网络服务
                StartSocketServer();
                // 启动铱星服务
                StartIridiumServer();
                // 启动mqtt broker
                StartMqttBroker();
                // 启动mqtt服务器端的订阅
                //StartMqttClient();
            }
            catch (SocketException e)
            {
                if (e.SocketErrorCode == SocketError.AddressAlreadyInUse)
                {
                    MessageBox.Show("This program has another instance still running now.\nIf that one is not created by you, please contact the administrator.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    tsmiExit_Click(this, EventArgs.Empty);
                }
            }
        }
        /// <summary>
        /// 关闭服务
        /// </summary>
        public void StopService()
        {
            StopMqttClient();
            // 停止mqtt broker
            StopMqttBroker();
            // 停止铱星 server
            StopIridiumServer();
            // 停止 socket 服务
            StopSocketServer();
        }
        /// <summary>
        /// 跟随窗口大小改变历史记录框
        /// </summary>
        private void ResizeHistoryBox()
        {
            rtbHistory.Top = tsMain.Top + tsMain.Height + ssMain.Height + 1;
            rtbHistory.Height = ClientRectangle.Height - rtbHistory.Top - ssMain.Height;
            rtbHistory.Left = 0;
            rtbHistory.Width = ClientRectangle.Width;
        }
        private void FormMain_Load(object sender, EventArgs e)
        {
            tstbIridiumPort.Text = ConfigurationManager.AppSettings["IRIDIUM_PORT"];
            tstbIridiumServer.Text = ConfigurationManager.AppSettings["IRIDIUM_SERVER"];
            tscbIMEI.SelectedIndex = 0;
            StartService();
            _timerSave = new System.Threading.Timer(new TimerCallback(SaveHistory), null, 0, SAVE_TIMER_PERIOD);
        }
        
        private void tsmiShowHistory_Click(object sender, EventArgs e)
        {
            tsmiShowHistory.Checked = !tsmiShowHistory.Checked;
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsClose)
            {
                StopService();
                Win32.TimeDelay(2000);
                // 停止timer
                if (null != _timerSave)
                {
                    _timerSave.Change(Timeout.Infinite, Timeout.Infinite);
                }
                SaveFile();
                Win32.TimeDelay(5000);
            }
            else
            {
                WindowState = FormWindowState.Minimized;
                e.Cancel = true;
                notifyIcon.Visible = true;
            }
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                tsmiShowMainForm.Enabled = true;
                Hide();
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                tsmiShowMainForm.Enabled = false;
            }
        }

        private void tsmiShowMainForm_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            IsClose = true;
            Close();
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            if (tsmiShowMainForm.Enabled)
            {
                tsmiShowMainForm_Click(sender, e);
            }
        }

        private void tsmiStopFetchingAddress_Click(object sender, EventArgs e)
        {
            tsmiStopFetchingAddress.Checked = !tsmiStopFetchingAddress.Checked;
        }

        private void tstbIridiumServer_KeyPress(object sender, KeyPressEventArgs e)
        {
            // IP地址输入
            if (e.KeyChar != 0x08 && e.KeyChar != '.' && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void tstbIridiumPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 0x08 && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void tsbtAnalyse_Click(object sender, EventArgs e)
        {
            IridiumManualAnalysePackage();
        }

        private void tsbtSend_Click(object sender, EventArgs e)
        {
            IridiumManualSend();
        }
        private void tsmiFlushMtQueue_Click(object sender, EventArgs e)
        {
            tsmiFlushMtQueue.Checked = !tsmiFlushMtQueue.Checked;
            FlushMTQueue = tsmiFlushMtQueue.Checked;
        }

        private void tsmiShowIridiumPackage_Click(object sender, EventArgs e)
        {
            tsmiShowIridiumPackage.Checked = !tsmiShowIridiumPackage.Checked;
            if (null != _iridium)
            {
                _iridium.ShowPackageInformation = tsmiShowIridiumPackage.Checked;
            }
        }
    }

}
