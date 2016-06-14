using System;
using System.Web;

namespace Wbs.Everdigm.Web.ajax
{
    /// <summary>
    /// print 的摘要说明
    /// </summary>
    public partial class print : BaseHttpHandler
    {
        /// <summary>
        /// 终端标签标记
        /// </summary>
        private static string LBTER = "terminal";
        /// <summary>
        /// 铱星标签标记
        /// </summary>
        private static string LBSAT = "iridium";

        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);
            if (string.IsNullOrEmpty(type) || string.IsNullOrEmpty(cmd))
            {
                ResponseError("Unknow message type.");
            }
            else
            {
                switch (cmd)
                {
                    case "query":
                        // printer查询是否有需要打印的标签
                        if (type.Equals(LBSAT))
                        {
                            HandleIridiumLabelPrintQuery();
                        }
                        else if (type.Equals(LBTER))
                        {
                            HandleTerminalLabelPrintQuery();
                        }
                        break;
                    case "print":
                        // printer 更新打印状态
                        if (type.Equals(LBSAT))
                        {
                            HandleIridiumLablePrintProgress();
                        }
                        else if (type.Equals(LBTER))
                        {
                            HandleTerminalLablePrintProgress();
                        }
                        break;
                    case "save":
                        // web保存打印的信息
                        if (type.Equals(LBSAT))
                        {
                            HandleIridiumLabelPrintSave();
                        }
                        else if (type.Equals(LBTER))
                        {
                            HandleTerminalLabelPrintSave();
                        }
                        break;
                    case "request":
                        // web请求打印一个新的标签
                        if (type.Equals(LBSAT))
                        {
                            HandleIridiumLabelPrintRequest();
                        }
                        else if (type.Equals(LBTER))
                        {
                            HandleTerminalLabelPrintRequest();
                        }
                        break;
                    case "status":
                        // web查询打印状态
                        if (type.Equals(LBSAT))
                        {
                            HandleIridiumLabelPrintStatus();
                        }
                        else if (type.Equals(LBTER))
                        {
                            HandleTerminalLabelPrintStatus();
                        }
                        break;
                    default:
                        // 未知的传递消息
                        ResponseError("Unknow message request.");
                        break;
                }
            }
        }
        /// <summary>
        /// 返回错误信息
        /// </summary>
        /// <param name="text"></param>
        private void ResponseError(string text)
        {
            ResponseData(-1, text);
        }
        /// <summary>
        /// 返回状态信息
        /// </summary>
        /// <param name="status"></param>
        /// <param name="text"></param>
        /// <param name="json"></param>
        private void ResponseData(int status, string text, bool json = false)
        {
            SatelliteInstance.Close();
            TerminalInstance.Close();
            ResponseJson(string.Format("{0}\"State\":{1},\"Type\":\"{2}\",\"Data\":{3}{4}{5}{6}",
                "{", status, type, (json ? "" : "\""), text, (json ? "" : "\""), "}"));
        }
    }
}