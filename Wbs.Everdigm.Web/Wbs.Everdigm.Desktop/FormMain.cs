﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Configuration;
using System.IO;
using System.Net;
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
        /// <summary>
        /// 服务
        /// </summary>
        private SocketServer _server;
        /// <summary>
        /// 铱星DirectIP MO接收服务
        /// </summary>
        private IridiumServer _iridium;
        /// <summary>
        /// 自定义委托类型
        /// </summary>
        private delegate void MyInvoker();
        /// <summary>
        /// 保存历史记录的计时器
        /// </summary>
        private System.Threading.Timer _timerSave;

        private string MAP_URL = ConfigurationManager.AppSettings["FETCH_MAP_URL"];

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
            if (null == _iridium)
            {
                _iridium = new IridiumServer();
                _iridium.OnMessage += new EventHandler<string>(OnServerMessage);
                _iridium.Start(int.Parse(tstbIridiumPort.Text));
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
            if (null != _iridium)
            {
                _iridium.Stop();
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
                    if (history.IndexOf("position: ") == 0) { 
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
        private uint _timerCounter = 0;
        private void FormMain_Load(object sender, EventArgs e)
        {
            tstbIridiumPort.Text = ConfigurationManager.AppSettings["IRIDIUM_PORT"];
            tstbIridiumServer.Text = ConfigurationManager.AppSettings["IRIDIUM_SERVER"];
            tscbIMEI.SelectedIndex = 0;
            StartService();
            _timerSave = new System.Threading.Timer(new TimerCallback(SaveHistory), null, 0, 10000);
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

                var ret = SaveFile("data\\" + DateTime.Now.ToString("yyyyMMdd") + "_history.txt", rtbHistory.Text);
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

        private void tsmiShowHistory_Click(object sender, EventArgs e)
        {
            tsmiShowHistory.Checked = !tsmiShowHistory.Checked;
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsClose)
            {
                StopService();
                Wbs.Utilities.Win32.TimeDelay(1000);
                SaveFile();
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
            IsClose = true;
            this.Close();
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
            if (e.KeyChar != 0x08 && e.KeyChar != '.' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void tstbIridiumPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 0x08 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void tsbtAnalyse_Click(object sender, EventArgs e)
        {

        }

        private void tsbtSend_Click(object sender, EventArgs e)
        {
            tsbtSend.Enabled = false;
            var client = new TcpClient();
            client.BeginConnect(IPAddress.Parse(tstbIridiumServer.Text), int.Parse(tstbIridiumPort.Text),
                new AsyncCallback(Connected_Callback), client);
        }

        private void Connected_Callback(IAsyncResult ar)
        {
            TcpClient client = (TcpClient)ar.AsyncState;
            try {
                if (client.Connected)
                {
                    client.EndConnect(ar);
                    ShowHistory("Client connected.", true);
                    ClientState cs = new ClientState();
                    cs.client = client;
                    cs.length = 0;
                    cs.received = null;
                    client.GetStream().BeginRead(cs.buffer, 0, ClientState.SIZE, new AsyncCallback(Read_Callback), cs);
                    // 组包并发送
                    // 数据内容
                    MTPayload mtp = new MTPayload();
                    mtp.Payload = CustomConvert.GetBytes(tstbData.Text);
                    mtp.Package();
                    // Header
                    MTHeader mth = new MTHeader();
                    mth.UniqueID = (uint)CustomConvert.DateTimeToJavascriptDate(DateTime.Now);
                    mth.IMEI = tscbIMEI.Text;
                    mth.DispositionFlags = 0;
                    mth.Package();
                    // 整包
                    Iridium iridium = new Iridium();
                    iridium.Content = new byte[mtp.Content.Length + mth.Content.Length];
                    Buffer.BlockCopy(mth.Content, 0, iridium.Content, 0, mth.Content.Length);
                    Buffer.BlockCopy(mtp.Content, 0, iridium.Content, mth.Content.Length, mtp.Content.Length);
                    iridium.Package();

                    //ShowHistory("MT: " + CustomConvert.GetHex(iridium.PackageContent), true);
                    client.GetStream().Write(iridium.PackageContent, 0, iridium.PackageContent.Length);
                    var buffer = new IridiumBuffer();
                    buffer.length = iridium.PackageContent.Length;
                    buffer.buffer = new byte[buffer.length];
                    Buffer.BlockCopy(iridium.PackageContent, 0, buffer.buffer, 0, buffer.length);
                    _iridium.AddQueue(buffer);
                }
                else
                {
                    ShowHistory("Can not establish connect", true);
                    client.Close();
                    client = null;
                }
            }
            catch(Exception e)
            {
                ShowHistory("Connect_Callback error: " + e.Message + Environment.NewLine + e.StackTrace, true);
            }
        }
        private void Read_Callback(IAsyncResult ar)
        {
            ClientState cs = (ClientState)ar.AsyncState;
            var len = cs.client.GetStream().EndRead(ar);
            if (len > 0)
            {
                if (null == cs.received)
                {
                    cs.received = new byte[len];
                    cs.length = len;
                }
                else
                {
                    cs.length += len;
                    cs.received = CustomConvert.expand(cs.received, cs.length);
                }
                Buffer.BlockCopy(cs.buffer, 0, cs.received, cs.length - len, len);
                cs.client.GetStream().BeginRead(cs.buffer, 0, ClientState.SIZE, new AsyncCallback(Read_Callback), cs);
                if (cs.length >= cs.received[2])
                {
                    var buffer = new IridiumBuffer();
                    buffer.socket = null;
                    buffer.length = cs.length;
                    buffer.buffer = new byte[cs.length];
                    Buffer.BlockCopy(cs.received, 0, buffer.buffer, 0, cs.length);
                    cs.length = 0;
                    cs.received = null;
                    _iridium.AddQueue(buffer);
                }
            }
            else
            {
                cs.Dispose();
                cs.client.GetStream().Close();
                cs.client.Close();
                cs.client = null;
                cs = null;
            }
        }

        private class ClientState : IDisposable
        {
            public TcpClient client;
            public const int SIZE = 4096;
            public byte[] buffer = new byte[SIZE];
            public int length = 0;
            public byte[] received = null;
            ~ClientState()
            { Dispose(); }
            public void Dispose()
            {
                length = 0;
                received = null;
            }
        }
    }

}