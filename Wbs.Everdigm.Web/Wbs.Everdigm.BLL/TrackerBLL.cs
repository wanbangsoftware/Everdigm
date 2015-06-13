using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                BatteryAlarm = (DateTime?)null,
                CarNumber = "",
                ChargingAlarm = (DateTime?)null,
                Deleted = false,
                Director = "",
                CSQ = 0,
                id = 0,
                LastActionAt = (DateTime?)null,
                Latitude = 0.0,
                Longitude = 0.0,
                ParkingAlarm = (DateTime?)null,
                SimCard = "",
                Socket = 0,
                State = 0
            };
        }

        public override string ToString(TB_Tracker entity)
        {
            return string.Format("ID: {0}, Sim: {1}, Car NO.:{2}", entity.id, entity.SimCard, entity.CarNumber);
        }
    }
}
