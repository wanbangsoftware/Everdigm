using System;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;
using Wbs.Everdigm.Database;
using Wbs.Everdigm.Common;
using Wbs.Label.Printer;
using Wbs.Utilities;

namespace Wbs.Everdigm.Printer
{
    /// <summary>
    /// 普通终端标签打印处理
    /// </summary>
    public partial class FormPrinter
    {
        private void HandlerTerminalLabel(string json)
        {
            TB_Terminal obj = JsonConvert.ToObject<TB_Terminal>(json);
            if (null != obj && obj.id > 0) { Task.Factory.StartNew(() => ConfirmTerminalLabel(obj, (byte)PrintStatus.Handling)); }
            else { PerformExitOrContinue(); }
        }
        /// <summary>
        /// 确定打印终端标签
        /// </summary>
        /// <param name="item"></param>
        /// <param name="state"></param>
        private void ConfirmTerminalLabel(TB_Terminal item,byte state)
        {
            string json = format("{0}\"Number\":\"{1}\",\"LabelPrintStatus\":{2}{3}", "{", item.Number, state, "}");
            string url = ParseUrl(PrintType, CmdPrint, json);
            FetchingHttpRequest(url).ContinueWith((task) => {
                string text = task.Result;
                // 保存打印状态成功
                if (!string.IsNullOrEmpty(text) && text.Contains("\"State\":0"))
                {
                    if (state == (byte)PrintStatus.Handling)
                    {
                        // 如果保存的是正在打印状态则启动打印
                        Task.Factory.StartNew(() => PrintTerminalLabel(item));
                    }
                    else
                    {
                        ConfirmTaskHandleComplete();
                    }
                }
                else
                {
                    //log(format("================ process end of handle \"{0}\" error: {1}\r\n", (PrintStatus)state, text));
                    // 处理错误时，设置进行下一次轮训
                    PerformExitOrContinue();
                }
            });
        }

        /// <summary>
        /// 打印终端终端标签
        /// </summary>
        private void PrintTerminalLabel(TB_Terminal item)
        {
            if (frmThis.InvokeRequired)
            {
                frmThis.Invoke(new Action(() =>
                {
                    PrintTerminalLabelDirectly(item);
                }));
            }
            else
            {
                PrintTerminalLabelDirectly(item);
            }
        }
        /// <summary>
        /// 打印终端标签
        /// </summary>
        /// <param name="item"></param>
        private void PrintTerminalLabelDirectly(TB_Terminal item)
        {
            DisableButtons(true);
            try
            {
                try
                {
                    NameValueCollection nvc = ConfigurationManager.AppSettings;
                    TscLib.openport(nvc["PrinterName"]);
                    TscLib.clearbuffer();
                    //宽度mm,高度mm,速度,浓度,感应器,间距mm,偏移量mm
                    TscLib.setup(nvc["LabelWidth"], nvc["LabelHeight"], "2", "10", "0", "3", "0");
                    TscLib.windowsfont(int.Parse(nvc["TerminalProductNo_x"]), int.Parse(nvc["TerminalProductNo_y"]), 25, 180, 0, 0, "Arial", nvc["TerminalProductNo"]);
                    TscLib.windowsfont(int.Parse(nvc["TerminalModel_x"]), int.Parse(nvc["TerminalModel_y"]), 25, 180, 0, 0, "Arial", nvc["TerminalModel"]);
                    TscLib.windowsfont(int.Parse(nvc["TerminalNumber_x"]), int.Parse(nvc["TerminalNumber_y"]), 25, 180, 0, 0, "Arial", item.Number);
                    TscLib.windowsfont(int.Parse(nvc["TerminalSimCard_x"]), int.Parse(nvc["TerminalSimCard_y"]), 25, 180, 0, 0, "Arial", item.Sim);
                    TscLib.windowsfont(int.Parse(nvc["TerminalMFD_x"]), int.Parse(nvc["TerminalMFD_y"]), 25, 180, 0, 0, "Arial", item.ProductionDate.Value.ToString("yyyy/MM/dd"));
                    TscLib.windowsfont(int.Parse(nvc["TerminalRV_x"]), int.Parse(nvc["TerminalRV_y"]), 25, 180, 0, 0, "Arial", nvc["TerminalRV"]);
                    TscLib.windowsfont(int.Parse(nvc["TerminalMF_x"]), int.Parse(nvc["TerminalMF_y"]), 25, 180, 0, 0, "Arial", nvc["TerminalMF"]);
                    // 条形码
                    TscLib.barcode(nvc["TerminalBAR_x"], nvc["TerminalBAR_x"], "128", "40", "0", "0", "4", "1", item.Number);
                    // 打印
                    TscLib.printlabel("1", "1");
                    Win32.TimeDelay(TIMER_INTEVAL);

                    // 打印完毕通知服务器保存已打印的状态
                    Task.Factory.StartNew(() => ConfirmTerminalLabel(item, (byte)PrintStatus.Printed));
                }
                finally
                {
                    TscLib.closeport();
                }
            }
            catch (Exception e)
            {
                log(string.Format("Print terminal label error: {0}, StackTrace: {1}", e.Message, e.StackTrace));
                PerformExitOrContinue();
            }
            DisableButtons(false);
        }
    }
}