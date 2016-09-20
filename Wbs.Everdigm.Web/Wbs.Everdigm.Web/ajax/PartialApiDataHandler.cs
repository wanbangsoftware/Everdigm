using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wbs.Everdigm.Common;
using Wbs.Everdigm.BLL;
using Wbs.Everdigm.Database;
using Wbs.Utilities;

namespace Wbs.Everdigm.Web.ajax
{
    /// <summary>
    /// 处理tracker汇报的数据
    /// </summary>
    public partial class api
    {
        /// <summary>
        /// 处理tracker汇报的数据
        /// </summary>
        /// <param name="obj"></param>
        private void HandleReportData(Api obj)
        {
            var tmp = ParseJson<Account>(obj.content);
            if (null != tmp && !string.IsNullOrEmpty(tmp.data))
            {
                string base64 = HttpUtility.UrlDecode(tmp.data);
                try
                {
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
                catch (Exception e)
                {
                    ResponseData(-1, string.Format("Can not handle your [report] request: {0}", e.Message));
                }
            }
            else
            {
                ResponseData(-1, "Cannot handler data report request with error content.");
            }
        }

        /// <summary>
        /// 处理消息拉取
        /// </summary>
        /// <param name="obj"></param>
        private void HandleFetchingMessage(Api obj)
        {
            var tmp = ParseJson<Account>(obj.content);
            if (null != tmp && !string.IsNullOrEmpty(tmp.name))
            {
                using (var bll = new TrackerChatBLL())
                {
                    var list = bll.FindList(f => f.TB_Tracker.SimCard.Equals(tmp.name) && f.Status != (byte)TrackerChatStatus.Delivered);
                    if (null != list && list.Count() > 0)
                    { }
                }
            }
        }
    }

    public class TrackerChat
    {
        public int id { get; set; }
        public long createTime { get; set; }
        public long publishTime { get; set; }
        public string deliver { get; set; }
        public byte type { get; set; }
        public string content { get; set; }

        public TrackerChat(TB_TrackerChat obj)
        {
            id = obj.id;
            createTime = CustomConvert.DateTimeToJavascriptDate(obj.CreateTime.Value);
            publishTime = CustomConvert.DateTimeToJavascriptDate(obj.SendTime.Value);
            deliver = obj.TB_Account.Name;
            type = obj.Type.Value;
            content = obj.Content;
        }
    }
}