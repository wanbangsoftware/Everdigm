using System;

using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.BLL
{
    /// <summary>
    /// TX10G的BLL
    /// </summary>
    public class TrackerBLL : BaseService<TB_Tracker>
    {
        public TrackerBLL()
            : base(new BaseRepository<TB_Tracker>())
        { }

        public override TB_Tracker GetObject()
        {
            return new TB_Tracker()
            {
                Address = "",
                BatteryAlarm = null,
                CarNumber = "",
                ChargingAlarm = null,
                Deleted = false,
                Director = "",
                Period = 10,
                DeviceId = "",
                CSQ = 0,
                id = 0,
                LastActionAt = null,
                Latitude = 0.0,
                Longitude = 0.0,
                ParkingAlarm = null,
                SimCard = "",
                Socket = 0,
                State = 0
            };
        }

        public override string ToString(TB_Tracker entity)
        {
            return string.Format("Number: {0}, Vehicle:{1}", entity.SimCard, 
                string.IsNullOrEmpty(entity.CarNumber) ? "-" : entity.CarNumber);
        }
    }
}
