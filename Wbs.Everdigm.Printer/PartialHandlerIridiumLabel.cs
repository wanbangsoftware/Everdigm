using System;
using System.Threading.Tasks;
using Wbs.Everdigm.Common;
using Wbs.Everdigm.Database;
using Wbs.Label.Printer;
using Wbs.Utilities;
using System.Configuration;
using System.Collections.Specialized;

namespace Wbs.Everdigm.Printer
{
    /// <summary>
    /// 卫星模块标签打印处理
    /// </summary>
    public partial class FormPrinter
    {
        /// <summary>
        /// 处理卫星模块标签的打印请求
        /// </summary>
        /// <param name="json"></param>
        private void HandleIridiumLable(string json)
        {
            TB_Satellite obj = JsonConvert.ToObject<TB_Satellite>(json);
            if (null != obj && obj.id > 0)
            {
                Task.Factory.StartNew(() => ConfirmIridiumLabel(obj, (byte)PrintStatus.Handling));
            }
            else
            {
                log("================ process end of error json parse.\r\n");
                PerformExitOrContinue();
            }
        }
        /// <summary>
        /// 确定处理铱星的终端标签
        /// </summary>
        /// <param name="item"></param>
        /// <param name="state"></param>
        private void ConfirmIridiumLabel(TB_Satellite item, byte state)
        {
            string json = format("{0}\"CardNo\":\"{1}\",\"LabelPrintStatus\":{2}{3}", "{", item.CardNo, state, "}");
            string url = ParseUrl(PrintType, CmdPrint, json);
            FetchingHttpRequest(url).ContinueWith((task) =>
            {
                string text = task.Result;
                // 保存打印状态成功
                if (!string.IsNullOrEmpty(text) && text.Contains("\"State\":0"))
                {
                    if (state == (byte)PrintStatus.Handling)
                    {
                        // 如果保存的是正在打印状态则启动打印
                        Task.Factory.StartNew(() => PrintIridiumLabel(item));
                    }
                    else
                    {
                        ConfirmTaskHandleComplete();
                    }
                }
                else
                {
                    log(format("================ process end of handle \"{0}\" error: {1}\r\n", (PrintStatus)state, text));
                    // 处理错误时，设置进行下一次轮训
                    PerformExitOrContinue();
                }
            });
        }
        /// <summary>
        /// 打印铱星终端标签
        /// </summary>
        private void PrintIridiumLabel(TB_Satellite item)
        {
            if (frmThis.InvokeRequired)
            {
                frmThis.Invoke(new Action(() =>
                {
                    PrintIridiumLabelDirectly(item);
                }));
            }
            else
            {
                PrintIridiumLabelDirectly(item);
            }
        }

        private void PrintIridiumLabelDirectly(TB_Satellite item)
        {
            DisableButtons(true);
            try
            {
                try
                {
                    //NameValueCollection nvc = ConfigurationManager.AppSettings;
                    //TscLib.openport(nvc["PrinterName"]);
                    //TscLib.clearbuffer();
                    ////宽度mm,高度mm,速度,浓度,感应器,间距mm,偏移量mm
                    //TscLib.setup(nvc["LabelWidth"], nvc["LabelHeight"], "2", "10", "0", "3", "0");
                    //TscLib.windowsfont(int.Parse(nvc["IMEI_x"]), int.Parse(nvc["IMEI_y"]), 25, 180, 0, 0, "Arial", item.CardNo);
                    //TscLib.windowsfont(int.Parse(nvc["PCB_x"]), int.Parse(nvc["PCB_y"]), 25, 180, 0, 0, "Arial", item.PcbNumber);
                    //TscLib.windowsfont(int.Parse(nvc["FW_x"]), int.Parse(nvc["FW_y"]), 25, 180, 0, 0, "Arial", item.FWVersion);
                    //TscLib.windowsfont(int.Parse(nvc["MFD_x"]), int.Parse(nvc["MFD_y"]), 25, 180, 0, 0, "Arial", item.ManufactureDate);
                    //TscLib.windowsfont(int.Parse(nvc["RV_x"]), int.Parse(nvc["RV_y"]), 25, 180, 0, 0, "Arial", item.RatedVoltage);
                    //TscLib.windowsfont(int.Parse(nvc["MF_x"]), int.Parse(nvc["MF_y"]), 25, 180, 0, 0, "Arial", item.Manufacturer);
                    //// 条形码
                    //TscLib.barcode(nvc["BAR_x"], nvc["BAR_x"], "128", "40", "0", "0", "4", "1", item.CardNo);
                    //// 打印
                    //TscLib.printlabel("1", "1");
                    Win32.TimeDelay(TIMER_INTEVAL);

                    // 打印完毕通知服务器保存已打印的状态
                    Task.Factory.StartNew(() => ConfirmIridiumLabel(item, (byte)PrintStatus.Printed));
                }
                finally
                {
                    //TscLib.closeport();
                }
            }
            catch (Exception e)
            {
                log(string.Format("Print label error: {0}, StackTrace: {1}", e.Message, e.StackTrace));
                PerformExitOrContinue();
            }
            DisableButtons(false);
        }
    }
}