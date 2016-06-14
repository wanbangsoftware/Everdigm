using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wbs.Everdigm.Common;
using Wbs.Everdigm.BLL;
using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.Web.ajax
{
    /// <summary>
    /// 处理tracker汇报的数据
    /// </summary>
    public partial class api
    {
        private void HandleReportData(Api obj)
        {
            var tmp = ParseJson<Account>(obj.content);
            if (null != tmp && !string.IsNullOrEmpty(tmp.data))
            {
                string base64 = HttpUtility.UrlDecode(tmp.data);
                using (var bll = new SmsBLL())
                {
                    TB_SMS sms = bll.GetObject();
                    sms.Data = base64;
                    sms.Sender = tmp.name;
                    sms.Type = SMSUtility.SMS_TRACKER;
                    bll.Add(sms);
                    ResponseData(0, "Data saved");
                }
            }
            else
            {
                ResponseData(-1, "Cannot handler data report request with error content.");
            }
        }
    }
}