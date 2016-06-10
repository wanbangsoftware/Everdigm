using System;

using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.BLL
{
    /// <summary>
    /// TX10G历史记录的BLL
    /// </summary>
    public class TrackerPositionBLL : BaseService<TB_Tracker_Position>
    {
        public TrackerPositionBLL()
            : base(new BaseRepository<TB_Tracker_Position>())
        { }

        public override TB_Tracker_Position GetObject()
        {
            return new TB_Tracker_Position()
            {
                Address = "",
                CarNumber = "",
                GPSTime = DateTime.Now,
                id = 0,
                Provider = "",
                Latitude = 0.0,
                SimCard = "",
                Longitude = 0.0,
                ReceiveTime = DateTime.Now,
                Tracker = (int?)null,
                Type = ""
            };
        }

        public override string ToString(TB_Tracker_Position entity)
        {
            return "";
        }
    }
}
