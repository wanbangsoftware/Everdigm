using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;
using Wbs.Everdigm.BLL;
using Wbs.Everdigm.Database;
using Wbs.Everdigm.Common;
using System.Configuration;
using Microsoft.Office.Interop.Excel;
using Wbs.Protocol;

namespace Wbs.Everdigm.Desktop
{
    /// <summary>
    /// 处理导出信息到excel
    /// </summary>
    public partial class DataHandler
    {
        private static string EXCEL_EQUIPMENTS = ConfigurationManager.AppSettings["EXCEL_EQUIPMENTS"];
        private static string EXCEL_TERMINALS = ConfigurationManager.AppSettings["EXCEL_TERMINALS"];

        /// <summary>
        /// 处理是否有需要导出设备列表或终端列表到excel的请求
        /// </summary>
        public void HandleWebRequestEquipmentsOrTerminalsToExcel()
        {
            try
            {
                List<byte> condition = new List<byte>();
                condition.Add((byte)ExcelExportType.Equipments);
                condition.Add((byte)ExcelExportType.Terminals);

                using (var bll = new ExcelHandlerBLL())
                {
                    var excel = bll.Find(f => f.Handled == false && f.Deleted == false && condition.Contains(f.Type.Value));
                    if (null != excel)
                    {
                        // 有待处理的
                        if (excel.Type == (byte)ExcelExportType.Equipments)
                            ExportEquipmentsToExcel(bll, excel);
                        else
                            ExportTerminalsToExcel(bll, excel);
                    }
                }
            }
            catch (Exception e)
            {
                ShowUnhandledMessage(format("{0}Equipment/Terminal to Excel handler error: {1}{2}{3}", Now, e.Message, Environment.NewLine, e.StackTrace));
            }
        }
        /// <summary>
        /// 导出设备列表到excel
        /// </summary>
        /// <param name="bll"></param>
        /// <param name="excel"></param>
        private void ExportEquipmentsToExcel(ExcelHandlerBLL bll, TB_ExcelHandler excel)
        {
            // data为保存出错时的异常数据
            string source = "", data = "";
            Application app = null;
            Workbook book = null;
            Worksheet sheet = null;
            try
            {
                app = new Application();
                book = app.Workbooks.Open(EXCEL_PATH + EXCEL_EQUIPMENTS);
                sheet = (Worksheet)book.ActiveSheet;
                app.Visible = false;
                app.AlertBeforeOverwriting = false;
                app.DisplayAlerts = false;

                using (var ebll = new EquipmentBLL())
                {
                    int line = 3;
                    int cnt = 0;
                    var n = (int?)null;
                    var list = ebll.FindList(f => f.Deleted == false);
                    foreach (var obj in list)
                    {
                        var x = line + cnt;
                        sheet.Cells[x, 1] = (cnt + 1);
                        sheet.Cells[x, 2] = n == obj.Model ? "-" : obj.TB_EquipmentModel.TB_EquipmentType.Code;
                        sheet.Cells[x, 3] = n == obj.Model ? "-" : ebll.GetFullNumber(obj);
                        sheet.Cells[x, 4] = EverdigmUtils.GetEquipmentFunctional(obj.Functional.Value);
                        sheet.Cells[x, 5] = EquipmentBLL.GetRuntime(obj.Runtime + obj.InitializedRuntime, obj.CompensatedHours.Value);
                        sheet.Cells[x, 6] = ebll.GetEngineState(obj);
                        sheet.Cells[x, 7] = string.IsNullOrEmpty(obj.GpsAddress) ? "-" : obj.GpsAddress;
                        // 状态
                        sheet.Cells[x, 8] = obj.TB_EquipmentStatusName.Code;
                        // customer
                        sheet.Cells[x, 9] = n == obj.Customer ? "-" : obj.TB_Customer.Code;
                        sheet.Cells[x, 10] = n == obj.Customer ? "-" : obj.TB_Customer.Name;
                        // 终端
                        var link = ebll.GetOnlineStyle(obj, false);
                        link = link.Substring(link.IndexOf('>') + 1);
                        link = link.Substring(0, link.IndexOf('<'));
                        sheet.Cells[x, 11] = string.IsNullOrEmpty(link) ? "-" : link;
                        string alarm = ebll.GetAlarmStatus(obj.Alarm);
                        alarm = alarm.Substring(alarm.IndexOf("title=\"") + 7);
                        alarm = alarm.Substring(0, alarm.IndexOf('"'));
                        sheet.Cells[x, 12] = alarm.Contains("No") ? "-" : alarm;
                        sheet.Cells[x, 13] = null == obj.LastActionTime ? "-" : obj.LastActionTime.Value.ToString("yyyy/MM/dd HH:mm");
                        sheet.Cells[x, 14] = n == obj.Terminal ? "-" : obj.TB_Terminal.Number;
                        bool sat = n != obj.Terminal && n != obj.TB_Terminal.Satellite;
                        sheet.Cells[x, 15] = sat ? obj.TB_Terminal.TB_Satellite.CardNo : "-";
                        sheet.Cells[x, 16] = n == obj.Warehouse ? "-" : obj.TB_Warehouse.Name;
                        cnt++;
                    }
                }
                // 另存为别的
                var date = excel.CreateDate.Value.ToString("yyyyMMdd");
                var path = Path.Combine(WEB_PATH, "files\\xls\\", date);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                source = path + "\\Equipments2Excel_" + excel.CreateDate.Value.ToString("yyyyMMdd") + ".xlsx";
                if (File.Exists(source))
                {
                    File.Delete(source);
                }
                book.SaveAs(source);
            }
            catch (Exception e)
            {
                data = e.StackTrace;
                ShowUnhandledMessage(format("{0}Equipment to Excel handler error: {1}{2}{3}", Now, e.Message, Environment.NewLine, e.StackTrace));
            }
            finally
            {
                // 关闭book
                if (null != book)
                {
                    book.Close();
                    book = null;
                }
                // 关闭application
                if (null != app)
                {
                    app.Quit();
                    app = null;
                }
                // 释放内存
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            var target = "../" + source.Replace(WEB_PATH, "").Replace("\\", "/");
            bll.Update(f => f.id == excel.id, act =>
            {
                act.Handled = true;
                act.Target = target;
                act.Status = (byte)(string.IsNullOrEmpty(data) ? 0 : 1);
                act.Data = data;
            });
        }
        /// <summary>
        /// 导出终端列表到excel
        /// </summary>
        /// <param name="bll"></param>
        /// <param name="excel"></param>
        private void ExportTerminalsToExcel(ExcelHandlerBLL bll, TB_ExcelHandler excel)
        {
            string source = "", data = "";
            Application app = null;
            Workbook book = null;
            Worksheet sheet = null;
            try
            {
                app = new Application();
                book = app.Workbooks.Open(EXCEL_PATH + EXCEL_TERMINALS);
                sheet = (Worksheet)book.ActiveSheet;
                app.Visible = false;
                app.AlertBeforeOverwriting = false;
                app.DisplayAlerts = false;

                using (var tbll = new TerminalBLL())
                {
                    using (var ebll = new EquipmentBLL())
                    {
                        int line = 2;
                        int cnt = 0;
                        var n = (int?)null;
                        var list = tbll.FindList(f => f.Delete == false);
                        foreach (var obj in list)
                        {
                            var x = line + cnt;
                            sheet.Cells[x, 1] = (cnt + 1);
                            sheet.Cells[x, 2] = obj.Number;
                            sheet.Cells[x, 3] = n == obj.Satellite ? "-" : obj.TB_Satellite.CardNo;
                            sheet.Cells[x, 4] = string.IsNullOrEmpty(obj.Firmware) ? "-" : obj.Firmware;
                            sheet.Cells[x, 5] = obj.Revision;
                            sheet.Cells[x, 6] = TerminalTypes.GetTerminalType(obj.Type.Value);
                            sheet.Cells[x, 7] = obj.ProductionDate.Value.ToString("yyyy/MM/dd");
                            var e = ebll.Find(d => d.Terminal == obj.id && d.Deleted == false);
                            sheet.Cells[x, 8] = null == e ? "-" : ebll.GetFullNumber(e);

                            var link = EverdigmUtils.GetOnlineStyle(obj.OnlineStyle, obj.OnlineTime, false);
                            link = link.Substring(link.IndexOf('>') + 1);
                            link = link.Substring(0, link.IndexOf('<'));
                            sheet.Cells[x, 9] = string.IsNullOrEmpty(link) ? "-" : link;
                            sheet.Cells[x, 10] = null == obj.OnlineTime ? "-" : obj.OnlineTime.Value.ToString("yyyy/MM/dd HH:mm");
                            cnt++;
                        }
                    }
                }

                // 另存为别的
                var date = excel.CreateDate.Value.ToString("yyyyMMdd");
                var path = Path.Combine(WEB_PATH, "files\\xls\\", date);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                source = path + "\\Terminals2Excel_" + excel.CreateDate.Value.ToString("yyyyMMdd") + ".xlsx";
                if (File.Exists(source))
                {
                    File.Delete(source);
                }
                book.SaveAs(source);
            }
            catch (Exception e)
            {
                data = e.StackTrace;
                ShowUnhandledMessage(format("{0}Terminal to Excel handler error: {1}{2}{3}", Now, e.Message, Environment.NewLine, e.StackTrace));
            }
            finally
            {
                // 关闭book
                if (null != book)
                {
                    book.Close();
                    book = null;
                }
                // 关闭application
                if (null != app)
                {
                    app.Quit();
                    app = null;
                }
                // 释放内存
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            var target = "../" + source.Replace(WEB_PATH, "").Replace("\\", "/");
            bll.Update(f => f.id == excel.id, act =>
            {
                act.Handled = true;
                act.Status = (byte)(string.IsNullOrEmpty(data) ? 0 : 1);
                act.Target = target;
                act.Data = data;
            });
        }
    }
}
