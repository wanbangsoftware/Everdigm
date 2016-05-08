using System.Configuration;

namespace Wbs.Everdigm.Printer
{
    public partial class FormPrinter
    {
        private static string Host = ConfigurationManager.AppSettings["Host"];
        private static string HttpLocal = "http://localhost:33859";
        private static string HttpWbs = "http://tms.wanbangsoftware.com";
        private static string HttpEverdigm = "http://tms.everdigm.com";
        private static string HttpBaseUrl = "/ajax/print.ashx?type=";
        /// <summary>
        /// 普通终端标签
        /// </summary>
        private static string TypeTerminal = "terminal";
        /// <summary>
        /// 卫星终端标签
        /// </summary>
        private static string TypeIridium = "iridium";
        private static string ParamData = "&data=";
        private static string ParamCmd = "&cmd=";
        private static string ParamDate = "&date=";
        private static string CmdQuery = "query";
        private static string CmdPrint = "print";

        private string GetHttpHeader()
        {
            string ret = "";
            switch (Host.ToLower())
            {
                case "everdigm": ret = HttpEverdigm; break;
                case "wbs": ret = HttpWbs; break;
                default: ret = HttpLocal; break;
            }
            return ret;
        }
        /// <summary>
        /// 基本的url组合，如http://xxx/ajax/print.ashx?type=
        /// </summary>
        private string BaseUrl { get { return format("{0}{1}", GetHttpHeader(), HttpBaseUrl); } }
        /// <summary>
        /// 要打印的标签类型
        /// </summary>
        private string PrintType { get { return handleTerminal ? TypeTerminal : TypeIridium; } }
        /// <summary>
        /// 组合url
        /// </summary>
        /// <param name="type"></param>
        /// <param name="cmd"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private string ParseUrl(string type, string cmd, string data = "")
        {
            return format("{0}{1}{2}{3}{4}{5}{6}{7}", BaseUrl, type, ParamCmd, cmd, ParamData, data, ParamDate, Tick);
        }
    }
}