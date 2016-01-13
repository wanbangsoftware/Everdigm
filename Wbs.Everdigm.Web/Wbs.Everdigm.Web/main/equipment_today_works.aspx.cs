using System;

using Wbs.Everdigm.BLL;

namespace Wbs.Everdigm.Web.main
{
    public partial class equipment_today_works : BaseBLLPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (!HasSessionLose)
            {
                ShowChart();
            }
            else
                ResponseError("Your session has expired, please login again.");
        }
        /// <summary>
        /// 返回错误信息
        /// </summary>
        /// <param name="msg"></param>
        private void ResponseError(string msg)
        {
            ErrorChart chart = new ErrorChart(msg);
            var width = ParseInt(GetParamenter("width"));
            chart.Width = width;
            chart.Chart(Response.OutputStream);
        }
        /// <summary>
        /// 显示指定日期的工作时间
        /// </summary>
        private void ShowChart()
        {
            try
            {
                if (string.IsNullOrEmpty(_key)) ResponseError("Invalid parameter: key.");
                else
                {
                    var id = ParseInt(Utility.Decrypt(_key));
                    var obj = new EquipmentBLL().Find(f => f.id == id);
                    if (null == obj) { ResponseError("No equipment object exists."); }
                    else
                    {
                        var date = GetParamenter("date");
                        var chart = new TodayWorktimeChart();
                        var width = ParseInt(GetParamenter("width"));
                        chart.Width = width;
                        chart.Date = date;
                        chart.Equipment = obj.TB_EquipmentModel.Code + obj.Number;
                        chart.Chart(Response.OutputStream);
                    }
                }
            }
            catch (Exception e)
            {
                ResponseError("Error: " + e.Message);
            }
        }
    }
}