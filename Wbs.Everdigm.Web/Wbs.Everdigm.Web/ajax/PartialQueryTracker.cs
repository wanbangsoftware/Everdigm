using System;
using Wbs.Everdigm.Database;
using Wbs.Everdigm.Common;

namespace Wbs.Everdigm.Web.ajax
{
    /// <summary>
    /// 查询Tracker的信息
    /// </summary>
    public partial class query
    {
        private void HandleTrackerQuery()
        {
            var ret = "[]";
            switch (cmd)
            { 
                case "position":
                    ret = GetTrackerHistory();
                    break;
            }
            ResponseJson(ret);
        }

        private string GetTrackerHistory()
        {
            var ret = "[]";
            try
            {
                var id = ParseInt(Utility.Decrypt(data));
                var start = DateTime.Parse(GetParamenter("start") + " 00:00:00");
                var end = DateTime.Parse(GetParamenter("end") + " 23:59:59");

                var list = TrackerPositionInstance.FindList<TB_Tracker_Position>(
                    f => f.Tracker == id && f.GPSTime >= start && f.GPSTime <= end, "GPSTime");
                ret = JsonConverter.ToJson(list);
            }
            catch { }
            return ret;
        }
    }
}