using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wbs.Everdigm.BLL;
using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.Web.ajax
{
    public partial class print
    {
        private SatelliteBLL SatelliteInstance { get { return new SatelliteBLL(); } }

        /// <summary>
        /// 查找当前正等待打印的铱星标签
        /// </summary>
        private void HandleIridiumLabelPrintQuery()
        {
            var label = SatelliteInstance.Find(f => f.LabelPrintSchedule >= DateTime.Now.AddMinutes(-5) &&
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
        /// 获取铱星的Object
        /// </summary>
        /// <returns></returns>
        private TB_Satellite parseSatelliteObject()
        {
            if (!string.IsNullOrEmpty(data))
            {
                try
                {
                    var obj = JsonConverter.ToObject<TB_Satellite>(data);
                    if (null != obj)
                    {
                        if (!string.IsNullOrEmpty(obj.CardNo))
                        {
                            var exists = SatelliteInstance.Find(f => f.CardNo.Equals(obj.CardNo));
                            if (null != exists)
                                return exists;
                            else
                                ResponseError(string.Format("Can not find object with paramenter \"{0}\".", obj.CardNo));
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
        /// 保存铱星的标签信息
        /// </summary>
        private void HandleIridiumLabelPrintSave()
        {
            var obj = parseSatelliteObject();
            if (null != obj)
            {
                obj = JsonConverter.ToObject<TB_Satellite>(data);
                SatelliteInstance.Update(f => f.CardNo.Equals(obj.CardNo), act =>
                {
                    act.PcbNumber = obj.PcbNumber;
                    act.FWVersion = obj.FWVersion;
                    act.ManufactureDate = obj.ManufactureDate;
                    act.Manufacturer = obj.Manufacturer;
                    act.RatedVoltage = obj.RatedVoltage;
                    // 设置成未打印状态
                    act.LabelPrintStatus = (byte)Common.PrintStatus.Nothing;
                });
                ResponseData(0, "");
            }
        }

        /// <summary>
        /// 请求打印一个铱星标签
        /// </summary>
        private void HandleIridiumLabelPrintRequest()
        {
            // 请求打印一个新的标签
            var obj = parseSatelliteObject();
            if (null != obj)
            {
                SatelliteInstance.Update(f => f.CardNo.Equals(obj.CardNo), act =>
                {
                    act.LabelPrintSchedule = DateTime.Now;
                    act.LabelPrintStatus = (byte)Common.PrintStatus.Waiting;
                    act.LabelPrinted += 1;
                });
                ResponseData(0, "");
            }
        }

        /// <summary>
        /// 查询铱星标签打印过程状态
        /// </summary>
        private void HandleIridiumLabelPrintStatus()
        {
            // 查询打印进度
            var obj = parseSatelliteObject();
            if (null != obj)
            {
                ResponseData(obj.LabelPrintStatus.Value, "");
            }
        }

        /// <summary>
        /// 更新铱星标签打印进程
        /// </summary>
        private void HandleIridiumLablePrintProgress()
        {
            var obj = parseSatelliteObject();
            if (null != obj)
            {
                obj = JsonConverter.ToObject<TB_Satellite>(data);
                SatelliteInstance.Update(f => f.CardNo.Equals(obj.CardNo), act =>
                {
                    act.LabelPrintStatus = obj.LabelPrintStatus;
                });
                ResponseData(0, "");
            }
        }
    }
}