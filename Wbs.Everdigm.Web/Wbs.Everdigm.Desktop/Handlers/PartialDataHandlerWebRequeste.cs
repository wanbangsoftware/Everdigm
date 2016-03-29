using System;
using System.IO;
using Wbs.Everdigm.Database;
using Microsoft.Office.Interop.Excel;
using System.Configuration;

namespace Wbs.Everdigm.Desktop
{
    public partial class DataHandler
    {
        /// <summary>
        /// excel文件所在的目录
        /// </summary>
        private static string EXCEL_PATH = ConfigurationManager.AppSettings["EXCEL_PATH"];
        private static string EXCEL_TMS = ConfigurationManager.AppSettings["EXCEL_TMS"];
        /// <summary>
        /// web所在的根目录
        /// </summary>
        private static string WEB_PATH = ConfigurationManager.AppSettings["WEB_PATH"];
        /// <summary>
        /// 处理web传过来的excel处理请求
        /// </summary>
        /// <returns>返回是否有处理过数据</returns>
        public bool HandleWebRequestExcel2pdf()
        {
            try
            {
                // 这里只查询excel导出到pdf的记录
                var excel = ExcelHandlerInstance.Find(f => f.Handled == false && f.Equipment == (int?)null && f.Deleted == false);
                if (null != excel)
                {
                    HandleWorkDetail(excel.id, excel.TB_WorkDetail);
                    return true;
                }
            }
            catch (Exception e)
            {
                ShowUnhandledMessage(format("{0}Excel to pdf handler error: {1}{2}{3}", Now, e.Message, Environment.NewLine, e.StackTrace));
            }
            return false;
        }
        private void HandleWorkDetail(int detail,TB_WorkDetail obj)
        {
            //var ret = "";
            var source = "";
            Application app = null;
            Workbook book = null;
            Worksheet sheet = null;
            try
            {
                var n = (int?)null;
                app = new Application();
                book = app.Workbooks.Open(EXCEL_PATH + EXCEL_TMS);
                sheet = (Worksheet)book.ActiveSheet;
                // 更改Shop order No.
                var _int = obj.id;
                int column = 5, row = 2;
                sheet.Cells[row, column] = _int;
                sheet.Cells[row + 16, column] = _int;
                sheet.Cells[row + 32, column] = _int;
                // 更改Shop order date
                row += 1;
                sheet.Cells[row, column] = DateTime.Today;
                sheet.Cells[row + 16, column] = DateTime.Today;
                sheet.Cells[row + 32, column] = DateTime.Today;
                // 更改Equipment model
                row += 1;
                var _string = obj.TB_Equipment.TB_EquipmentModel.Code;
                sheet.Cells[row, column] = _string;
                sheet.Cells[row + 16, column] = _string;
                sheet.Cells[row + 32, column] = _string;
                // 更改Equipment serial No.
                row += 1;
                _string = obj.TB_Equipment.Number;
                sheet.Cells[row, column] = _string;
                sheet.Cells[row + 16, column] = _string;
                sheet.Cells[row + 32, column] = _string;
                // 更改Equipment location
                row += 1;
                _string = n == obj.TB_Equipment.Warehouse ? "" : obj.TB_Equipment.TB_Warehouse.Name;
                sheet.Cells[row, column] = _string;
                sheet.Cells[row + 16, column] = _string;
                sheet.Cells[row + 32, column] = _string;
                // 更改TMS model
                row += 2;
                // 10=机械式，其他=电子式
                var func = obj.TB_Equipment.Functional;
                _string = func == 10 ? "EX300" : "EX100";
                sheet.Cells[row, column] = _string;
                sheet.Cells[row + 16, column] = _string;
                sheet.Cells[row + 32, column] = _string;

                // 另存为别的
                var path = Path.Combine(WEB_PATH, "files\\xls\\", DateTime.Now.ToString("yyyyMMdd"));
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var equipment = obj.TB_Equipment.TB_EquipmentModel.Code + obj.TB_Equipment.Number;
                source = path + "\\" + obj.TB_Work.RegisterTime.Value.ToString("HHmmss_") + equipment + ".xlsx";
                if (File.Exists(source))
                {
                    File.Delete(source);
                }
                book.SaveAs(source);

            }
            //catch
            //{
            //    ret = GetFormatedJson(-1, "Cannot change file.");
            //}
            finally
            {
                if (null != book)
                {
                    book.Close();
                    book = null;
                }
                if (null != app)
                {
                    app.Quit();
                    app = null;
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();

                // 转换成pdf
                var ee = obj.TB_Work.RegisterTime.Value.ToString("yyyyMMddHHmmssfff") + ".pdf";
                ConvertToPdf(detail, source, obj.TB_Work.RegisterTime.Value.ToString("yyyyMMdd"), ee);
            }
            //return ret;
        }

        private void ConvertToPdf(int detail, string source, string dir, string name)
        {
            // 转换成pdf
            var path = "files\\pdf\\" + dir;
            var p = Path.Combine(WEB_PATH, path);
            if (!Directory.Exists(p))
            {
                Directory.CreateDirectory(p);
            }
            var target = p + "\\" + name;
            var ok = OfficeConvert.ExcelConvertToPDF(source, target);
            if (ok)
            {
                var resp = ("~/" + path + "/" + name).Replace("~", "..").Replace("\\", "/");
                ExcelHandlerInstance.Update(f => f.id == detail, act =>
                {
                    act.Handled = true;
                    act.Source = source;
                    act.Target = resp;
                });
                //return GetFormatedJson(0, "Success", resp);
            }
            //return GetFormatedJson(-1, "Cannot convert file to PDF.");
        }
    }
}
