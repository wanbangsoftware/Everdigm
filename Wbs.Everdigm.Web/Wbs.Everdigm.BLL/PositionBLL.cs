using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.BLL
{
    /// <summary>
    /// 位置信息业务处理逻辑
    /// </summary>
    public class PositionBLL : BaseService<TB_Data_Position>
    {
        public PositionBLL()
            : base(new BaseRepository<TB_Data_Position>())
        { }
        /// <summary>
        /// 生成一个新的位置信息实体
        /// </summary>
        /// <returns></returns>
        public override TB_Data_Position GetObject()
        {
            return new TB_Data_Position()
            {
                id = 0,
                Address = "",
                Altitude = 0.0,
                Ber = 0,
                Csq = 0,
                Direction = 0.0,
                Equipment = (int?)null,
                EW = 'E',
                GpsTime = DateTime.Now,
                Latitude = 0.0,
                Longitude = 0.0,
                NS = 'N',
                ReceiveTime = DateTime.Now,
                SectorCode = "0000",
                Speed = 0.0,
                StationCode = "0000",
                StoreTimes = 0,
                Type = "Unknown",
                Terminal = "",
                Updated = 0
            };
        }
        /// <summary>
        /// 地址信息转成字符串
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string ToString(TB_Data_Position entity)
        {
            return string.Format("{0}", entity.Address);
        }
    }
}
