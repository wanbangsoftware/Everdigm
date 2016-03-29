using System;

using Wbs.Protocol.TX300.Analyse;
using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.BLL
{
    /// <summary>
    /// 报警信息业务处理逻辑
    /// </summary>
    public class AlarmBLL : BaseService<TB_Data_Alarm>
    {
        public AlarmBLL()
            : base(new BaseRepository<TB_Data_Alarm>())
        { }

        /// <summary>
        /// 生成一个新的报警信息实体
        /// </summary>
        /// <returns></returns>
        public override TB_Data_Alarm GetObject()
        {
            return new TB_Data_Alarm()
            {
                id = 0,
                Code = "0000000000000000",
                Terminal = "",
                // 增加了报警时间  2015/09/18 08:30
                AlarmTime = DateTime.Now,
                Equipment = (int?)null,
                Position = (int?)null,
                StoreTimes = 0
            };
        }
        /// <summary>
        /// 报警信息字符串显示
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string ToString(TB_Data_Alarm entity)
        {
            return _0x2000.GetAlarm(entity.Code, true);
        }
    }
}
