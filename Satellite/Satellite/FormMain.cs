using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Timers;
using Wbs.Protocol;
using Wbs.Utilities;

namespace Satellite
{
    public partial class FormMain : Form
    {
        /// <summary>
        /// 保存历史记录的时间间隔：10分钟保存一次
        /// </summary>
        private static int SAVE_HISTORY_INTERVAL = 10;
        /// <summary>
        /// 标记窗体是否关闭
        /// </summary>
        private bool hasClosed = false;
        /// <summary>
        /// 数据处理实体
        /// </summary>
        private DataHandler _handler = null;
        /// <summary>
        /// 定时保存历史记录的线程
        /// </summary>
        private Thread _thread = null;
        /// <summary>
        /// 串口打开时间
        /// </summary>
        private TimeSpan _portOpenTime;

        public FormMain()
        {
            InitializeComponent();
            var width = Screen.PrimaryScreen.Bounds.Width;
            var height = Screen.PrimaryScreen.Bounds.Height;
            this.Width = (int)((width * 1.0) * 2 / 3);
            this.Height = (int)(height * 1.0 * 4 / 5);
        }
        /// <summary>
        /// 初始化串口列表
        /// </summary>
        private void EnumBaudRate()
        {
            tscbPort.Items.Clear();
            var ports = System.IO.Ports.SerialPort.GetPortNames();
            foreach (var port in ports)
            {
                tscbPort.Items.Add(port);
            }
            if (tscbPort.Items.Count > 0)
                tscbPort.SelectedIndex = 0;
            tscbBaudrate.SelectedIndex = 9;
        }
        /// <summary>
        /// 初始化波特率和串口列表
        /// </summary>
        private void InitializeSerialPortParamenters()
        {
            EnumBaudRate();
        }
        /// <summary>
        /// 窗体初始化时执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Load(object sender, EventArgs e)
        { 
            if (null == _handler)
            {
                InitializeSerialPortParamenters();
                //ResizeHistoryBox();
                _handler = new DataHandler();
                _handler.OnDataHandled += new EventHandler<HandledData>(OnDataHandled);
                _handler.OnDataSend += new EventHandler<DataPackage>(OnDataSend);
                _handler.StartService();

                StartTimer();
            }
        }
        /// <summary>
        /// 数据处理完毕事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="list"></param>
        private void OnDataHandled(object sender, HandledData e)
        {
            // 显示分析的详细内容
            if (tsmiAnalyseData.Checked)
            {
                // 读出本地的卡号
                if (e.Data.Command.Equals("$ZJXX"))
                {
                    this.BeginInvoke((MyInvoker)delegate
                    {
                        tstbOrigin.Text = e.Data.OriginAddress;
                        tsslNumber.Text = "Original No.: " + e.Data.OriginAddress;
                        ZJXX z = e.Data as ZJXX;
                        tsslCapacity.Text = "Capacity: " + string.Format("[{0}:{1}][{2}:{3}][{4}:{5}][{6}:{7}][{8}:{9}][{10}:{11}]",
                            1, z.PW1, 2, z.PW2, 3, z.PW3, 4, z.PW4, 5, z.PW5, 6, z.PW6);
                    });
                }
                foreach (var str in e.Message)
                {
                    ShowHistory(str, false);
                }
            }
        }
        /// <summary>
        /// 需要发送数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void OnDataSend(object sender, DataPackage data)
        {
            if (null == data)
            {
                ShowHistory("不能发送为空(null)的数据。");
                return;
            }
            if (data.Type != DataType.Send) {
                ShowHistory("数据类别不是为了发送而设，请设为DataType.Send。");
                return;
            }
            if (spPort.IsOpen)
            {
                spPort.Write(data.Data, 0, data.Data.Length);
                ShowHistory("Send: " + CustomConvert.GetHex(data.Data));
            }
            else
            { ShowHistory("发送失败：没有可用的已打开的串口设备。"); }
            // 回收该条数据
            _handler.RecycleDataPackage(data);
        }
        /// <summary>
        /// 勾选是否显示历史记录的菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiDisplayHistory_Click(object sender, EventArgs e)
        {
            tsmiDisplayHistory.Checked = !tsmiDisplayHistory.Checked;
        }
        /// <summary>
        /// 打开串口开始通讯
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtOpen_Click(object sender, EventArgs e)
        {
            if (spPort.IsOpen)
            { spPort.Close(); }
            if (tsbtOpen.Text.Equals("Open"))
            {
                if (string.IsNullOrEmpty(tscbPort.Text))
                {
                    ShowHistory("系统中没有可用的串口设备。");
                    return;
                }
                // 打开串口
                try
                {
                    spPort.PortName = tscbPort.Text;
                    spPort.BaudRate = int.Parse(tscbBaudrate.Text);
                    spPort.Open();
                }
                catch (Exception open)
                {
                    ShowHistory("无法打开串口 \"" + spPort.PortName + "\"：" + open.Message);
                }
            }
            else
            {
                // 关闭串口
            }
            tsbtOpen.Text = spPort.IsOpen ? "Close" : "Open";

            
            if (spPort.IsOpen)
            {
                StartTimer();
            }
        }
        /// <summary>
        /// 启动计时器
        /// </summary>
        private void StartTimer()
        {
            _portOpenTime = new TimeSpan(DateTime.Now.Ticks);
            if (null != _thread)
            {
                _thread.Abort();
                _thread.Join();
                _thread = null;
            }
            _thread = new Thread(new ThreadStart(OnTimeEventThread));
            _thread.Start();
        }
        /// <summary>
        /// 
        /// </summary>
        private void OnTimeEventThread()
        {
            while (true)
            {
                Thread.Sleep(250);
                if (hasClosed) break;

                OnTimedEvent(null, null);
            }
        }
        private static bool dataSaved = false, gpsSaved = false;
        /// <summary>
        /// 显示计时器
        /// </summary>
        /// <param name="timer"></param>
        private void ShowTimer(string timer)
        {
            this.BeginInvoke((MyInvoker)delegate
            {
                tsslTimer.Text = timer;
            });
        }
        /// <summary>
        /// 计时器的执行方法
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            var now = new TimeSpan(DateTime.Now.Ticks);
            var interval = (now - _portOpenTime).Duration();

            ShowTimer(string.Format("{0:00}:{1:00}:{2:00}",
                interval.Hours, interval.Minutes, interval.Seconds));

            if ((interval.Minutes > 0) &&
                (interval.Minutes % SAVE_HISTORY_INTERVAL == 0))
            {
                SaveHistory();
            }
            else
            {
                if (dataSaved) 
                    dataSaved = false;
                if (gpsSaved) 
                    gpsSaved = false;
            }
        }
        /// <summary>
        /// 保存历史记录到文件
        /// </summary>
        private void SaveHistory()
        {
            SaveDataHistory();
            if (tsmiShowGPSData.Checked)
            {
                SaveGpsHistory();
            }
        }
        /// <summary>
        /// 保存数据记录
        /// </summary>
        private void SaveDataHistory()
        {
            if (!dataSaved)
            {
                dataSaved = true;
                rtbHistory.BeginInvoke((MyInvoker)delegate
                {
                    var ret = SaveFile("data\\" + DateTime.Now.ToString("yyyyMMdd") + "_data.txt", rtbHistory.Text);
                    if (string.IsNullOrEmpty(ret)) { rtbHistory.Clear(); } else { ShowHistory(ret); }

                });
            }
        }
        /// <summary>
        /// 保存定位记录
        /// </summary>
        private void SaveGpsHistory()
        {
            if (!gpsSaved)
            {
                gpsSaved = true;
                rtbGPS.BeginInvoke((MyInvoker)delegate
                {
                    var ret = SaveFile("gps\\" + DateTime.Now.ToString("yyyyMMdd") + "_gps.txt", rtbGPS.Text);
                    rtbHistory.Clear();
                });
            }
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
                catch (Exception e){
                    ret = "保存文件失败：" + e.Message;
                }
                finally
                {
                    fs.Close();
                }
            }
            return ret;
            //filePath;
        }
        /// <summary>
        /// 自定义委托类型
        /// </summary>
        private delegate void MyInvoker();
        /// <summary>
        /// 获取当前系统时间的文字表达方式
        /// </summary>
        /// <returns></returns>
        private string Now()
        {
            return DateTime.Now.ToString("[yyyy/MM/dd HH:mm:ss] ");
        }
        /// <summary>
        /// 获取当前系统时间的文字表达方式
        /// </summary>
        /// <returns></returns>
        private string Time()
        {
            return DateTime.Now.ToString("[HH:mm:ss] ");
        }
        /// <summary>
        /// 显示历史信息
        /// </summary>
        /// <param name="text">信息内容</param>
        /// <param name="time">是否包含时间内容</param>
        /// <param name="newLine">是否加换行</param>
        private void ShowHistory(string text, bool time = true, bool newLine = true)
        {
            rtbHistory.BeginInvoke((MyInvoker)delegate
            {
                try
                {
                    if (tsmiDisplayHistory.Checked)
                    {
                        rtbHistory.AppendText((time ? Now() : "") + text + (newLine ? Environment.NewLine : ""));
                        rtbHistory.SelectionStart = rtbHistory.Text.Length;
                        rtbHistory.ScrollToCaret();
                    }
                }
                catch { }
            });
        }
        /// <summary>
        /// 显示GPS信息
        /// </summary>
        /// <param name="text"></param>
        private void ShowHistoryGPS(string text)
        {
            rtbGPS.BeginInvoke((MyInvoker)delegate {
                try
                {
                    rtbGPS.AppendText(Time() + text + Environment.NewLine);
                    rtbGPS.SelectionStart = rtbGPS.Text.Length;
                    rtbGPS.ScrollToCaret();
                }
                catch
                { }
            });
        }
        /// <summary>
        /// 跟随窗口大小改变历史记录框
        /// </summary>
        private void ResizeHistoryBox()
        {
            rtbHistory.Top = tsMain.Top + tsMain.Height + tsSend.Height + 1;
            rtbHistory.Height = ClientRectangle.Height - rtbHistory.Top - ssMain.Height;
            rtbHistory.Left = 0;
            rtbHistory.Width = ClientRectangle.Width;
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            //ResizeHistoryBox();
        }

        private void spPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            if (sender.GetType() != typeof(System.IO.Ports.SerialPort))
            {
                return;
            }

            if (hasClosed) return;

            byte[] bReceived = null;
            SerialPort comp = (System.IO.Ports.SerialPort)sender;

            try
            {
                comp.ReceivedBytesThreshold = comp.ReadBufferSize;
                while (true)
                {
                    int len = comp.BytesToRead;
                    if (len > 0)
                    {
                        byte[] b = new byte[len];
                        comp.Read(b, 0, len);

                        if (bReceived == null)
                        {
                            bReceived = new byte[len];
                        }
                        else
                        {
                            bReceived = CustomConvert.expand(bReceived, bReceived.Length + len);
                        }
                        Buffer.BlockCopy(b, 0, bReceived, bReceived.Length - len, len);

                        Win32.TimeDelay(50);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch (Exception readError)
            {
                ShowHistory("Cannot read data from \"" + comp.PortName + "\": " + readError.Message);
            }
            finally
            {
                comp.ReceivedBytesThreshold = 1;
            }
            if (bReceived == null)
                return;

            HandleReceivedData(bReceived);
        }
        /// <summary>
        /// 处理接受到的数据，按照$符号进行分割处理
        /// </summary>
        /// <param name="data"></param>
        private void HandleReceivedData(byte[] data)
        {
            byte[] tmp = null;
            for (int i = 0, len = data.Length; i < len; i++)
            {
                if (data[i] == 0x24)// 从$字符开始
                {
                    if (i + 6 >= len) return;
                    // 第6位是,且第5位大于等于A的话，是GPS数据
                    if (data[i + 6] == 0x2C && data[i + 5] >= 0x41)
                    {
                        // GPS 数据
                        tmp = new byte[1];
                        tmp[0] = data[i];
                    }
                    else
                    {
                        byte[] d = new byte[2];
                        Buffer.BlockCopy(data, i + 5, d, 0, 2);
                        d = CustomConvert.reserve(d);
                        var dLen = BitConverter.ToUInt16(d, 0);
                        tmp = new byte[dLen];
                        Buffer.BlockCopy(data, i, tmp, 0, dLen);
                        i += dLen;
                        HandleData(tmp);
                    }
                }
                else if (data[i] == 0x0D)// 回车换行是结束
                {
                    if (i + 1 >= len)
                    {
                        if (null != tmp)
                        {
                            HandleData(tmp);
                            tmp = null;
                        }
                        //return; 
                    }
                    else
                    {
                        if (data[i + 1] == 0x0A)
                        {
                            if (null != tmp)
                            {
                                HandleData(tmp);
                                tmp = null;
                            }
                            i++;
                        }
                    }
                }
                else
                {
                    if (i + 1 >= len)
                        return;
                    tmp = CustomConvert.expand(tmp, tmp.Length + 1);
                    tmp[tmp.Length - 1] = data[i];
                }
            }
        }
        /// <summary>
        /// 处理分割出来的单条数据
        /// </summary>
        /// <param name="data"></param>
        private void HandleData(byte[] data)
        {
            string str = CustomConvert.BytesToASCII(data);
            if (str.IndexOf("$") == 0 && str.IndexOf(",") == 6)
            {
                ShowHistoryGPS(str);
                return;
            }
            ShowHistory(str);
            if (tsmiDisplayHex.Checked)
                ShowHistory("Hex: " + CustomConvert.GetHex(data));

            //return;
            // 加入等待处理的数据队列
            var tmp = _handler.GetBlankDataPackage();
            tmp.Type = DataType.Received;
            tmp.Time = DateTime.Now;
            tmp.Data = data;
            _handler.AddMessage(tmp);
        }

        private void tsmiDisplayHex_Click(object sender, EventArgs e)
        {
            tsmiDisplayHex.Checked = !tsmiDisplayHex.Checked;
        }

        private void tsmiSendAsHexData_Click(object sender, EventArgs e)
        {
            tsmiSendAsHexData.Checked = !tsmiSendAsHexData.Checked;
        }
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtSend_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tstbOrigin.Text) &&
                !string.IsNullOrEmpty(tstbTarget.Text) &&
                !string.IsNullOrEmpty(tstbData.Text))
            {
                var str = tstbData.Text.Trim();
                TXSQ txsq = new TXSQ();
                txsq.OriginAddress = tstbOrigin.Text;
                txsq.Type.TransferType = 1;
                txsq.TargetAddress = tstbTarget.Text;
                txsq.Reply = 0;
                txsq.Message = tsmiSendAsHexData.Checked ? CustomConvert.GetBytes(str) : ASCIIEncoding.ASCII.GetBytes(str);
                txsq.Package();

                //ShowHistory(CustomConvert.GetHex(txsq.Content));
                DataPackage data = _handler.GetBlankDataPackage();
                data.Data = txsq.Content;
                data.Time = DateTime.Now;
                data.Type = DataType.Send;
                _handler.AddMessage(data);
            }
            else
            {
                if (string.IsNullOrEmpty(tstbOrigin.Text))
                    tstbOrigin.Focus();
                else if (string.IsNullOrEmpty(tstbTarget.Text))
                    tstbTarget.Focus();
                else
                    tstbData.Focus();
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            hasClosed = true;
            if (spPort.IsOpen)
                spPort.Close();

            if (null != _thread && _thread.IsAlive)
            {
                _thread.Abort();
                _thread.Join();
                _thread = null;
            }

            _handler.StopService();

            SaveHistory();
        }
        /// <summary>
        /// 是否分析收发的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiAnalyseData_Click(object sender, EventArgs e)
        {
            tsmiAnalyseData.Checked = !tsmiAnalyseData.Checked;
        }

        private void tsbtTest_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tstbData.Text)) return;
            try
            {
                var tmp = CustomConvert.GetBytes(tstbData.Text);
                HandleReceivedData(tmp);
                return;
                var data = _handler.GetBlankDataPackage();
                data.Time = DateTime.Now;
                data.Type = DataType.Received;
                data.Data = CustomConvert.GetBytes(tstbData.Text);
                _handler.AddMessage(data);
            }
            catch (Exception convertError)
            {
                ShowHistory("数据转换错误：" + convertError.Message);
            }
        }

        private void tsmiShowGPSData_Click(object sender, EventArgs e)
        {
            tsmiShowGPSData.Checked = !tsmiShowGPSData.Checked;
        }

        private void tsbtICTEST_Click(object sender, EventArgs e)
        {
            // 发送IC检测命令
            var data = _handler.GetBlankDataPackage();
            data.Time = DateTime.Now;
            data.Type = DataType.Send;
            ICJC icjc = new ICJC();
            icjc.Package();
            data.Data = icjc.Content;
            ShowHistory(CustomConvert.GetHex(data.Data));
            _handler.AddMessage(data);
        }

        private void tsbtSystemTest_Click(object sender, EventArgs e)
        {
            // 发送系统检测命令
            XTZJ xtzj = new XTZJ();
            xtzj.OriginAddress = tstbOrigin.Text;
            xtzj.Frequency = 0;
            xtzj.Package();
            var data = _handler.GetBlankDataPackage();
            data.Time = DateTime.Now;
            data.Type = DataType.Send;
            data.Data = xtzj.Content;
            ShowHistory(CustomConvert.GetHex(data.Data));
            _handler.AddMessage(data);
        }
    }
}
