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
        private System.Threading.Timer timer;
        private static int TIMER_INTEVAL = 10000;
        private bool bExited = false;
        private FormPrinter frmThis;
        private HttpClient httpClient = new HttpClient();
        private static string local = "http://localhost:33859";
        private static string everdigm = "http://tms.everdigm.com";
        private static string url = "/ajax/print.ashx?type=iridium&cmd=";
        private static string urlData = "&data=";
        private static string urlSuffix = "&date=";
        private static string cmdQuery = "query";
        private static string cmdPrint = "print";
        private bool bLocal = !ConfigurationManager.AppSettings["Host"].ToLower().Equals("everdigm");

        private TB_Satellite tempObj = new TB_Satellite()
        {
            CardNo = ConfigurationManager.AppSettings["TEST_LABEL_IMEI"],
            ManufactureDate = ConfigurationManager.AppSettings["TEST_LABEL_MFD"],
            Manufacturer = ConfigurationManager.AppSettings["TEST_LABEL_MF"],
            FWVersion = ConfigurationManager.AppSettings["TEST_LABEL_FW"],
            PcbNumber = ConfigurationManager.AppSettings["TEST_LABEL_PCB"],
            RatedVoltage= ConfigurationManager.AppSettings["TEST_LABEL_RV"]
        };

        public FormPrinter()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            log("perform to close");
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
            log("Start.");
            notifyIcon.ShowBalloonTip(3000, "Everdigm", "Iridium Label Printer", ToolTipIcon.Info);
            // 每次调用完timer之后需要暂停timer，以待程序执行完毕之后再次调用timer
            //timer = new System.Threading.Timer(new TimerCallback(Timer_Callback), 0, TIMER_INTEVAL, 0);
        }

        private void Timer_Callback(object obj)
        {
            timer.Change(Timeout.Infinite, Timeout.Infinite);
            Task.Factory.StartNew(() => QueryTask());
        }

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

        private string Url
        {
            get
            {
                return string.Format("{0}{1}", bLocal ? local : everdigm, url);
            }
        }

        private static string format(string format, params object[] args)
        {
            return string.Format(format, args);
        }

        private TimeSpan tsStart = TimeSpan.MinValue;
        private TimeSpan tsEnd = TimeSpan.MinValue;
        private void QueryTask()
        {
            log("================ process start");
            tsStart = new TimeSpan(DateTime.Now.Ticks);
            try
            {
                httpClient.GetAsync(format("{0}{1}{2}{3}", Url, cmdQuery, urlSuffix, Tick)).ContinueWith((requestTask) =>
                {
                    // Get HTTP response from completed task.
                    HttpResponseMessage response = requestTask.Result;
                    // Check that response was successful or throw exception
                    response.EnsureSuccessStatusCode();
                    String text = response.Content.ReadAsStringAsync().Result;

                    // 过滤掉返回的json={}的情况
                    if (text.Length > 2)
                    {
                        TB_Satellite obj = JsonConvert.ToObject<TB_Satellite>(text);
                        if (null != obj && obj.id > 0)
                        {
                            Task.Factory.StartNew(() => ConfirmPrintTask(obj, (byte)PrintStatus.Handling));
                        }
                        else
                        {
                            log("================ process end of error json parse.\r\n");
                            performExit();
                        }
                    }
                    else
                    {
                        log("================ process end of null query.\r\n");
                        performExit();
                    }
                });
            }
            catch (Exception e)
            {
                log(string.Format("Error: {0}", e.StackTrace));
                log("================ process end with error.\r\n");
                performExit();
            }
        }

        private void performExit()
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
                Close();
            }
        }

        /// <summary>
        /// 确定打印标签
        /// </summary>
        /// <param name="item"></param>
        private void ConfirmPrintTask(TB_Satellite item, byte status)
        {
            string json = format("{0}\"CardNo\":\"{1}\",\"LabelPrintStatus\":{2}{3}", "{", item.CardNo, status, "}");
            string urlText = format("{0}{1}{2}{3}{4}{5}", Url, cmdPrint, urlData, json, urlSuffix, Tick);
            try
            {
                httpClient.GetAsync(urlText).ContinueWith((request) =>
                {
                    HttpResponseMessage response = request.Result;
                    response.EnsureSuccessStatusCode();
                    String text = response.Content.ReadAsStringAsync().Result;
                    log(text);
                    if (text.Contains("\"State\":0"))
                    {
                        if (status == (byte)PrintStatus.Handling)
                        {
                            // 处理正常，进行打印标签流程
                            Task.Factory.StartNew(() => PrintLabel(item));
                        }
                        else
                        {
                            tsEnd = new TimeSpan(DateTime.Now.Ticks);
                            log(string.Format("Time used: {0}ms", (tsEnd - tsStart).Duration().TotalMilliseconds));
                            log("================ process end\r\n");
                            // 打印处理完毕，设置下一次轮训
                            performExit();
                        }
                    }
                    else
                    {
                        log(format("================ process end of handle \"{0}\" error: {1}\r\n", (PrintStatus)status, text));
                        // 处理错误时，设置进行下一次轮训
                        performExit();
                    }
                });
            }
            catch(Exception e)
            {
                log(format("================ process end of handle \"{0}\" error: {1}\r\n", (PrintStatus)status, e.Message));
                // 发生错误时开始执行下一次任务
                performExit();
            }
        }

        /// <summary>
        /// 打印标签
        /// </summary>
        /// <param name="item"></param>
        private void PrintLabel(TB_Satellite item)
        {
            button1.Enabled = false;
            try
            {
                try
                {
                    NameValueCollection nvc = ConfigurationManager.AppSettings;
                    TscLib.openport(nvc["PrinterName"]);
                    TscLib.clearbuffer();
                    //宽度mm,高度mm,速度,浓度,感应器,间距mm,偏移量mm
                    TscLib.setup(nvc["LabelWidth"], nvc["LabelHeight"], "2", "10", "0", "3", "0");
                    TscLib.windowsfont(int.Parse(nvc["IMEI_x"]), int.Parse(nvc["IMEI_y"]), 25, 180, 0, 0, "Arial", item.CardNo);
                    TscLib.windowsfont(int.Parse(nvc["PCB_x"]), int.Parse(nvc["PCB_y"]), 25, 180, 0, 0, "Arial", item.PcbNumber);
                    TscLib.windowsfont(int.Parse(nvc["FW_x"]), int.Parse(nvc["FW_y"]), 25, 180, 0, 0, "Arial", item.FWVersion);
                    TscLib.windowsfont(int.Parse(nvc["MFD_x"]), int.Parse(nvc["MFD_y"]), 25, 180, 0, 0, "Arial", item.ManufactureDate);
                    TscLib.windowsfont(int.Parse(nvc["RV_x"]), int.Parse(nvc["RV_y"]), 25, 180, 0, 0, "Arial", item.RatedVoltage);
                    TscLib.windowsfont(int.Parse(nvc["MF_x"]), int.Parse(nvc["MF_y"]), 25, 180, 0, 0, "Arial", item.Manufacturer);
                    // 条形码
                    TscLib.barcode(nvc["BAR_x"], nvc["BAR_x"], "128", "40", "0", "0", "4", "1", item.CardNo);
                    // 打印
                    TscLib.printlabel("1", "1");
                    Win32.TimeDelay(5000);
                    //Task.Factory.StartNew(() => ConfirmPrintTask(item, (byte)PrintStatus.Printed));
                }
                finally
                {
                    TscLib.closeport();
                }
            }
            catch(Exception e) { log(string.Format("Print label error: {0}, StackTrace: {1}", e.Message, e.StackTrace)); }
            button1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PrintLabel(tempObj);
        }
    }
}
