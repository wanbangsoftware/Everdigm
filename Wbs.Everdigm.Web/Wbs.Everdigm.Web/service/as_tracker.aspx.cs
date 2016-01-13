using System;

namespace Wbs.Everdigm.Web.service
{
    public partial class as_tracker : BaseTrackerPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (!HasSessionLose)
            {
                hidKey.Value = Utility.UrlEncode(_key);
                ShowTrackerInfo();
            }
        }

        private void ShowTrackerInfo()
        {
            var id = ParseInt(Utility.Decrypt(hidKey.Value));
            var tracker = TrackerInstance.Find(f => f.id == id && f.Deleted == false);
            if (null != tracker)
            {
                aTrackerId.InnerText = tracker.SimCard;
                aTrackerVehicle.InnerText = (string.IsNullOrEmpty(tracker.CarNumber) ? "none" : tracker.CarNumber);
            }
        }
    }
}