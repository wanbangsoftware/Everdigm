using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wbs.Everdigm.BLL;
using Wbs.Everdigm.Database;
using Wbs.Everdigm.Common;

namespace Wbs.Everdigm.Web.ajax
{
    /// <summary>
    /// 终端标签打印部分
    /// </summary>
    public partial class print
    {
        private TerminalBLL TerminalInstance { get { return new TerminalBLL(); } }

        /// <summary>
        /// 将json字符串转换成Terminal对象
        /// </summary>
        /// <returns></returns>
        private TB_Terminal parseTerminalObject()
        {
            if (!string.IsNullOrEmpty(data))
            {
                try
                {
                    var obj = JsonConverter.ToObject<TB_Terminal>(data);
                    if (null != obj)
                    {
                        if (!string.IsNullOrEmpty(obj.Number))
                        {
                            var exists = TerminalInstance.Find(f => f.Number.Equals(obj.Number));
                            if (null != exists)
                                return exists;
                            else
                                ResponseError(string.Format("Can not find object with paramenter \"{0}\".", obj.Number));
                        }
                        else
                            ResponseError("Can not find object with null paramenter.");
                    }
                    else
                        ResponseError("Can not parse object.");
                }
                catch (Exception e)
                {
                    ResponseError("Parse object error: " + e.Message);
                }
            }
            else
                ResponseError("Can not perform this operation with null paramenter.");
            return null;
        }
        /// <summary>
        /// 查询正则等待打印标签的终端
        /// </summary>
        private void HandleTerminalLabelPrintQuery()
        {
            var label = TerminalInstance.Find(f => f.LabelPrintSchedule >= DateTime.Now.AddMinutes(-5) &&
            f.LabelPrintStatus == (byte)Common.PrintStatus.Waiting);
            if (null != label)
            {
                ResponseJson(JsonConverter.ToJson(label));
            }
            else
            {
                ResponseJson("{}");
            }
        }
        /// <summary>
        /// 更改终端标签的打印进程
        /// </summary>
        private void HandleTerminalLablePrintProgress()
        {
            var obj = parseTerminalObject();
            if (null != obj)
            {
                obj = JsonConverter.ToObject<TB_Terminal>(data);
                TerminalInstance.Update(f => f.Number.Equals(obj.Number), act =>
                {
                    act.LabelPrintStatus = obj.LabelPrintStatus;
                });
                ResponseData(0, "");
            }
        }
        /// <summary>
        /// 保存终端标签打印状态
        /// </summary>
        private void HandleTerminalLabelPrintSave() {
            var obj = parseTerminalObject();
            if (null != obj)
            {
                obj = JsonConverter.ToObject<TB_Terminal>(data);
                TerminalInstance.Update(f => f.Number.Equals(obj.Number), act =>
                {
                    //act.PcbNumber = obj.PcbNumber;
                    //act.FWVersion = obj.FWVersion;
                    //act.ManufactureDate = obj.ManufactureDate;
                    //act.Manufacturer = obj.Manufacturer;
                    //act.RatedVoltage = obj.RatedVoltage;
                    // 设置成未打印状态
                    act.LabelPrintStatus = (byte)Common.PrintStatus.Nothing;
                });
                ResponseData(0, "");
            }
        }
        /// <summary>
        /// 请求打印一个新标签
        /// </summary>
        private void HandleTerminalLabelPrintRequest() {
            // 请求打印一个新的标签
            var obj = parseTerminalObject();
            if (null != obj)
            {
                TerminalInstance.Update(f => f.Number.Equals(obj.Number), act =>
                {
                    act.LabelPrintSchedule = DateTime.Now;
                    act.LabelPrintStatus = (byte)Common.PrintStatus.Waiting;
                    act.LabelPrinted += 1;
                });
                ResponseData(0, "");
            }
        }
        /// <summary>
        /// 查询标签打印过程的状态
        /// </summary>
        private void HandleTerminalLabelPrintStatus()
        {
            // 查询打印进度
            var obj = parseTerminalObject();
            if (null != obj)
            {
                ResponseData(obj.LabelPrintStatus.Value, "");
            }
        }
    }
}