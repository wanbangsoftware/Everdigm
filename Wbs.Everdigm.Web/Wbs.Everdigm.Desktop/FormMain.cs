using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Configuration;
using System.IO;

namespace Wbs.Everdigm.Desktop
{
    public partial class FormMain : Form
    {
        /// <summary>
        /// 标记是否真的关闭窗口
        /// </summary>
        private bool Close = false;
        /// <summary>
        /// 服务
        /// </summary>
        private SocketServer _server;
        /// <summary>
        /// 自定义委托类型
        /// </summary>
        private delegate void MyInvoker();
        public FormMain()
        {
            InitializeComponent();
            var width = Screen.PrimaryScreen.Bounds.Width;
            var height = Screen.PrimaryScreen.Bounds.Height;
            this.Width = (int)((width * 1.0) * 2 / 3);
            this.Height = (int)(height * 1.0 * 4 / 5);
        }
        private void OnServerMessage(object sender, string message)
        {
            ShowHistory(message, false);
        }
        /// <summary>
        /// 初始化并启动服务
        /// </summary>
        public void StartService()
        {
            var port = int.Parse(ConfigurationManager.AppSettings["SERVER_PORT"]);
            if (null == _server)
            {
                _server = new SocketServer(port);
                _server.OnMessage += new EventHandler<string>(OnServerMessage);
                _server.StartUDP = true;
                _server.Start();
                tsmiStartService.Enabled = !_server.Started;
                tsmiStopService.Enabled = _server.Started;
                tsslServerState.Text = "Service Start at: " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            }
        }
        /// <summary>
        /// 关闭服务
        /// </summary>
        public void StopService()
        {
            if (null != _server)
            {
                _server.Stop();
            }
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
        /// <summary>
        /// 获取当前时间
        /// </summary>
        private string Now { get { return DateTime.Now.ToString("[yyyy/MM/dd HH:mm:ss] "); } }
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
                    if (tsmiShowHistory.Checked)
                    {
                        rtbHistory.AppendText((showTime ? Now : "") + history + Environment.NewLine);
                        rtbHistory.SelectionStart = rtbHistory.Text.Length;
                        rtbHistory.ScrollToCaret();
                    }
                }
                catch { }
            });
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            StartService();
        }

        private void tsmiShowHistory_Click(object sender, EventArgs e)
        {
            tsmiShowHistory.Checked = !tsmiShowHistory.Checked;
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Close)
            {
                StopService();
            }
            else
            {
                this.WindowState = FormWindowState.Minimized;
                e.Cancel = true;
                notifyIcon.Visible = true;
            }
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                tsmiShowMainForm.Enabled = true;
                this.Hide();
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                tsmiShowMainForm.Enabled = false;
            }
        }

        private void tsmiShowMainForm_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            Close = true;
            this.Close();
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            if (tsmiShowMainForm.Enabled)
            {
                tsmiShowMainForm_Click(sender, e);
            }
        }
    }
}
