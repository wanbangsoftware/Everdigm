using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wbs.Everdigm.Common;
using Wbs.Everdigm.BLL;
using Wbs.Everdigm.Database;
using Wbs.Utilities;

namespace Wbs.Everdigm.Desktop
{
    /// <summary>
    /// 处理Tracker推送消息
    /// </summary>
    public partial class DataHandler
    {
        /// <summary>
        /// 检测是否有需要发送的Tracker推送消息
        /// </summary>
        public void CheckTrackerChat()
        {
            using (var bll = new TrackerChatBLL())
            {
                var list = bll.FindList<TB_TrackerChat>(f => f.ScheduleTime >= DateTime.Now.AddMinutes(-5) && 
                f.Status == (byte)TrackerChatStatus.Waiting, "ScheduleTime");
                if (null != list && list.Count() > 0)
                {
                    var chat = list.FirstOrDefault();
                    HandleTrackerChat(chat, bll);
                }
                // 重置一下超时的记录等待再次发送
                ResetTimeoutTrackerChats(bll);
                // 重置超时的记录
                ResetDeliveringToTimeout(bll);
            }
        }
        /// <summary>
        /// 处理推送
        /// </summary>
        /// <param name="chat"></param>
        /// <param name="bll"></param>
        private void HandleTrackerChat(TB_TrackerChat chat, TrackerChatBLL bll)
        {
            bll.Update(u => u.id == chat.id, act => 
            {
                act.Status = (byte)TrackerChatStatus.Sending;
                act.SendTime = DateTime.Now;
                // 生成长度为5的随机字符串
                act.MqttTag = CustomConvert.RandomString(5);
            });
            OnTrackerChating?.Invoke(this, new TrackerChatEvent() { Target = chat.TB_Tracker.SimCard, Content = chat.MqttTag });
        }
        /// <summary>
        /// 重置超过1小时还未被读取的记录
        /// </summary>
        /// <param name="bll"></param>
        private void ResetTimeoutTrackerChats(TrackerChatBLL bll)
        {
            bll.Update(u => u.Status == (byte)TrackerChatStatus.Timeout && u.ScheduleTime < DateTime.Now.AddMinutes(-60), act => 
            {
                act.ScheduleTime = DateTime.Now;
                act.Status = (byte)TrackerChatStatus.Waiting;
            });
        }
        /// <summary>
        /// 重置发送超过指定时间的记录为超时
        /// </summary>
        /// <param name="bll"></param>
        private void ResetDeliveringToTimeout(TrackerChatBLL bll)
        {
            // 未读取的消息都可以设置为超时，并等待再次推送
            bll.Update(u => u.Status < (byte)TrackerChatStatus.Delivered && u.SendTime < DateTime.Now.AddMinutes(-30), act =>
            {
                // 超时
                act.Status = (byte)TrackerChatStatus.Timeout;
                // 超时次数+1
                act.ResetedTimes += 1;
            });
        }

        public EventHandler<TrackerChatEvent> OnTrackerChating;
    }
}
