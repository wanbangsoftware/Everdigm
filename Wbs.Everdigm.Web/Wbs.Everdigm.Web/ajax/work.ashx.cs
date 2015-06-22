using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Diagnostics;
using Wbs.Everdigm.Database;
using Wbs.Everdigm.BLL;
using Microsoft.Office.Interop.Excel;

namespace Wbs.Everdigm.Web.ajax
{
    /// <summary>
    /// work 的摘要说明
    /// </summary>
    public class work : BaseHttpHandler
    {
        private static string RET_FMT = "\"status\":{0},\"desc\":\"{1}\",\"data\":\"{2}\"";
        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);
            HandleRequest();
        }
        private string GetFormatedJson(int status, string desc, string data = null)
        {
            var ret = "{" + string.Format(RET_FMT, status, desc, null == data ? "" : data) + "}";
            return ret;
        }
        private WorkDetailBLL WorkDetailInstance { get { return new WorkDetailBLL(); } }
        private void HandleRequest()
        {
            var ret = "";
            if (null == User)
            {
                ret = GetFormatedJson(-1, "Your session has expired, Please try to login again.");
            }
            else
            {
                switch (cmd)
                {
                    case "detail":
                        // 生成工作项的文档
                        var id = int.Parse(Utility.Decrypt(data));
                        var detail = WorkDetailInstance.Find(f => f.id == id && f.Deleted == false);
                        if (null == detail)
                        {
                            ret = GetFormatedJson(-1, "Work is not exist.");
                        }
                        else
                        {
                            // 读取工作项，并保存到excel中
                            ret = HandleWorkDetail(detail);
                        }
                        break;
                }
            }
            ResponseJson(ret);
        }

        private string HandleWorkDetail(TB_WorkDetail obj)
        {
            var path = ctx.Server.MapPath("~/files/shop.order.TMS.installation.xlsx");
            var ret = "";
            var source = "";
            ApplicationClass app = null;
            Workbook book = null;
            Worksheet sheet = null;
            try
            {
                var n = (int?)null;
                app = new ApplicationClass();
                book = app.Workbooks.Open(path);
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
                var func = (EquipmentFunctional)obj.TB_Equipment.Functional;
                _string = func == EquipmentFunctional.Mechanical ? "EX300" : "EX100";
                sheet.Cells[row, column] = _string;
                sheet.Cells[row + 16, column] = _string;
                sheet.Cells[row + 32, column] = _string;

                // 另存为别的
                path = ctx.Server.MapPath("~/files/") + "xls/" + DateTime.Now.ToString("yyyyMMdd");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var equipment = obj.TB_Equipment.TB_EquipmentModel.Code + obj.TB_Equipment.Number;
                source = path + "/" + obj.TB_Work.RegisterTime.Value.ToString("HHmmss_") + equipment + ".xlsx";
                if (File.Exists(source))
                {
                    File.Delete(source);
                }
                book.SaveAs(source);

            }
            catch
            {
                ret = GetFormatedJson(-1, "Cannot change file.");
            }
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
                ret = ConvertToPdf(source, obj.TB_Work.RegisterTime.Value.ToString("yyyyMMdd"), ee);
            }
            return ret;
        }

        private string ConvertToPdf(string source, string dir, string name)
        {
            // 转换成pdf
            var path = "~/files/pdf/" + dir;
            var p = ctx.Server.MapPath(path);
            if (!Directory.Exists(p))
            {
                Directory.CreateDirectory(p);
            }
            var target = p + "\\" + name;
            var ok = OfficeConvert.ExcelConvertToPDF(source, target);
            if (ok)
            {
                return GetFormatedJson(0, "Success", (path + "/" + name).Replace("~", ".."));
            }
            return GetFormatedJson(-1, "Cannot convert file to PDF.");
        }
        /// <summary>
        /// 执行exe程序将pdf转换成
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="args"></param>
        private static void ExcutedCmd(string cmd, string args)
        {
            using (Process p = new Process())
            {

                ProcessStartInfo psi = new ProcessStartInfo(cmd, args);
                p.StartInfo = psi;
                p.Start();
                p.WaitForExit();
            }
        }
    }
}