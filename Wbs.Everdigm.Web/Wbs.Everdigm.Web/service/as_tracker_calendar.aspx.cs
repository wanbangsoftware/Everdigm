using System;

namespace Wbs.Everdigm.Web.service
{
    public partial class as_tracker_calendar : BaseTrackerPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (!HasSessionLose)
            {
                hidKey.Value = Utility.UrlEncode(_key);
                ShowTrackerInformation();
            }
        }

        private void ShowTrackerInformation() {
            int id= ParseInt(Utility.Decrypt(hidKey.Value));
            var tracker = TrackerInstance.Find(f => f.id == id && f.Deleted == false);
            if (null != tracker)
            {
                spanTrackerNumber.InnerText = tracker.SimCard;
            }
        }
    }
}