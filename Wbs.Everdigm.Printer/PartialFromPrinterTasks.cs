using System;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;

namespace Wbs.Everdigm.Printer
{
    public partial class FormPrinter
    {
        private Timer timer;
        private HttpClient httpClient = new HttpClient();
        private static int PRINT_DELAY = 5000;
        /// <summary>
        /// 每5s调用一次
        /// </summary>
        private static int TIMER_INTEVAL = 10000;
        private TimeSpan timespanStart = TimeSpan.MinValue;
        private TimeSpan timespanEnd = TimeSpan.MinValue;

        private void Timer_Callback(object obj)
        {
            timer.Change(Timeout.Infinite, Timeout.Infinite);
            Task.Factory.StartNew(() => QueryTask());
        }
        /// <summary>
        /// 一个计时周期成功结束
        /// </summary>
        private void ConfirmTaskHandleComplete()
        {
            timespanEnd = new TimeSpan(DateTime.Now.Ticks);
            //log(string.Format("Time used: {0}ms", (timespanEnd - timespanStart).Duration().TotalMilliseconds));
            //log("================ process end\r\n");
            // 打印处理完毕，设置下一次轮训
            PerformExitOrContinue();
        }
        /// <summary>
        /// 获取远程http内容
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private async Task<string> FetchingHttpRequest(string url)
        {
            string text = "";
            try
            {
                //log(url);
                HttpResponseMessage response = await httpClient.GetAsync(url);
                text = await response.Content.ReadAsStringAsync();
                //log(text);
            }
            catch { }
            return text;
        }
        /// <summary>
        /// 查询远程服务器上的标签打印需求
        /// </summary>
        private void QueryTask()
        {
            handleTerminal = !handleTerminal;
            //log("================ process start");
            timespanStart = new TimeSpan(DateTime.Now.Ticks);
            try
            {
                string url = ParseUrl(PrintType, CmdQuery);
                FetchingHttpRequest(url).ContinueWith((task) =>
                {
                    string text = task.Result;
                    // 过滤掉返回的json={}的情况
                    if (text.Length > 2)
                    {
                        log(text);
                        if (handleTerminal)
                        {
                            // 处理普通终端标签打印
                            HandlerTerminalLabel(text);
                        }
                        else
                        {
                            // 处理卫星终端标签打印
                            HandleIridiumLable(text);
                        }
                    }
                    else
                    {
                        //log("================ process end of null query.\r\n");
                        PerformExitOrContinue();
                    }
                });
            }
            catch (Exception e)
            {
                log(string.Format("Handle query error: {0}", e.StackTrace));
                //log("================ process end with error.\r\n");
                PerformExitOrContinue();
            }
        }
    }
}