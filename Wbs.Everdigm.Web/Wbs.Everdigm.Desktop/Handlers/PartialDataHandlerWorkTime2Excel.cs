using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Wbs.Everdigm.Database;
using Wbs.Everdigm.Common;
using Wbs.Utilities;
using System.Configuration;
using Microsoft.Office.Interop.Excel;
using Wbs.Everdigm.BLL;

namespace Wbs.Everdigm.Desktop
{
    /// <summary>
    /// 处理work time导出到excel的部分
    /// </summary>
    public partial class DataHandler
    {
        private static string EXCEL_WORKTIME = ConfigurationManager.AppSettings["EXCEL_WORKTIME"];
        /// <summary>
        /// 处理web传过来的导出工作时间的请求
        /// </summary>
        public void HandleWebRequestWorkTime2Excel()
        {
            try
            {
                var bll = new ExcelHandlerBLL();
                var excel = bll.Find(f => f.Handled == false && f.Work == (int?)null && f.Deleted == false);
                if (null != excel)
                {
                    if (string.IsNullOrEmpty(excel.Data) || excel.Data.Equals("[]"))
                    {
                        bll.Update(f => f.id == excel.id, act => { act.Handled = true; });
                    }
                    else
                    {
                        ExportWorkTimeToExcel(excel);
                    }
                }
            }
            catch (Exception e)
            {
                ShowUnhandledMessage(format("{0}Work time to Excel handler error: {1}{2}{3}", Now, e.Message, Environment.NewLine, e.StackTrace));
            }
        }
        /// <summary>
        /// 第一页开始行、结束行、页大小、页行数
        /// </summary>
        private static int lineStart = 16, pageSize = 78, pageRows = 40, pageCount = 39;

        private void ExportWorkTimeToExcel(TB_ExcelHandler excel)
        {
            var source = "";
            Application app = null;
            Workbook book = null;
            Worksheet sheet = null;
            try
            {
                app = new Application();
                book = app.Workbooks.Open(EXCEL_PATH + EXCEL_WORKTIME);
                sheet = (Worksheet)book.ActiveSheet;
                var equipment = excel.TB_Equipment.TB_EquipmentModel.Code + excel.TB_Equipment.Number;
                sheet.Name = equipment;
                // 所属公司
                string customer = "";
                if (excel.TB_Equipment.Customer == (int?)null)
                {
                    customer = "invalid";
                }
                else
                {
                    customer = excel.TB_Equipment.TB_Customer.Name;
                }
                sheet.Cells[3, 3] = customer;
                // 设备型号
                sheet.Cells[4, 3] = excel.TB_Equipment.TB_EquipmentModel.Name;
                // 设备号码
                sheet.Cells[5, 3] = excel.TB_Equipment.Number;
                // 设备出库日期
                string outdoor = "";
                if ((DateTime?)null == excel.TB_Equipment.OutdoorTime)
                {
                    outdoor = "invalid";
                }
                else
                {
                    outdoor = excel.TB_Equipment.OutdoorTime.Value.ToString("yyyy/MM/dd");
                }
                sheet.Cells[6, 3] = outdoor;
                // 打印日期
                sheet.Cells[6, 9] = excel.CreateDate.Value.ToString("yyyy/MM/dd");
                // 出库类型
                sheet.Cells[7, 3] = excel.TB_Equipment.TB_EquipmentStatusName.Code;
                sheet.Cells[8, 3] = excel.TB_Equipment.GpsAddress;
                // 查询开始日期
                sheet.Cells[10, 3] = excel.StartDate;
                sheet.Cells[11, 3] = excel.EndDate;

                // 组织数据
                List<WorktimeChart> works = JsonConverter.ToObject<List<WorktimeChart>>(excel.Data);
                int row = 0, cell = 1, page = 0, len = works.Count, count = 0;
                double total = 0.0,pcount=0.0;
                for (int i = 0; i < len; i++)
                {
                    count++;
                    page = i / (pageSize);
                    var baseRow = page * pageRows;
                    cell = (i / pageCount) % 2 == 0 ? 1 : 6;
                    row = baseRow + i % pageCount;
                    row += lineStart;
                    sheet.Cells[row, cell] = count;
                    sheet.Cells[row, cell + 1] = works[i].date;
                    sheet.Cells[row, cell + 2] = works[i].y;
                    total += works[i].y;
                    pcount += works[i].y;
                    // 如果运转时间等于0时，将上一条运转时间复制过来
                    if (works[i].min == 0 && i > 0)
                    {
                        works[i].min = works[i - 1].min;
                    }
                    sheet.Cells[row, cell + 3] = works[i].min / 60.0;
                    if (count % pageCount == 0)
                    {
                        sheet.Cells[row + 1, cell + 1] = "subtotal";
                        sheet.Cells[row + 1, cell + 2] = pcount;
                        Range range = sheet.Range[sheet.Cells[row + 1, cell], sheet.Cells[row + 1, cell + 3]];
                        range.Cells.Borders.Item[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
                        range.Cells.Borders.Item[XlBordersIndex.xlEdgeTop].Color = XlRgbColor.rgbGray;
                        range.Cells.Borders.Item[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
                        pcount = 0.0;
                    }
                }
                // 总运转时间
                sheet.Cells[12, 3] = total;

                // 最后一页中的统计
                sheet.Cells[row + 1, cell + 1] = "subtotal";
                sheet.Cells[row + 1, cell + 2] = pcount;
                Range last = sheet.Range[sheet.Cells[row + 1, cell], sheet.Cells[row + 1, cell + 3]];
                last.Cells.Borders.Item[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
                last.Cells.Borders.Item[XlBordersIndex.xlEdgeTop].Color = XlRgbColor.rgbGray;
                last.Cells.Borders.Item[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;

                // 另存为别的
                var date = excel.CreateDate.Value.ToString("yyyyMMdd");
                var path = Path.Combine(WEB_PATH, "files\\xls\\", date);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                source = path + "\\Equipment operation report_" + equipment + excel.CreateDate.Value.ToString("_HHmmss") + ".xlsx";
                if (File.Exists(source))
                {
                    File.Delete(source);
                }
                book.SaveAs(source);
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
            new ExcelHandlerBLL().Update(f => f.id == excel.id, act =>
            {
                act.Handled = true;
                act.Target = target;
            });
        }
    }
}
