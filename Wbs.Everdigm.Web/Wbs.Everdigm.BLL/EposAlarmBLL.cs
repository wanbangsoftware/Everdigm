using System;

using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.BLL
{
    /// <summary>
    /// EPOS故障信息业务处理逻辑
    /// </summary>
    public class EposAlarmBLL : BaseService<TB_Data_EposFault>
    {
        /// <summary>
        /// 实例化一个EPOS故障信息处理业务逻辑实体
        /// </summary>
        public EposAlarmBLL()
            : base(new BaseRepository<TB_Data_EposFault>())
        { }
        /// <summary>
        /// 生成一个新的EPOS故障信息实体
        /// </summary>
        /// <returns></returns>
        public override TB_Data_EposFault GetObject()
        {
            return new TB_Data_EposFault()
            {
                id = 0,
                CodeHex = "00",
                CodeName = "",
                Count = 0,
                Equipment = (int?)null,
                FMIHex = "00",
                FMIName = "",
                Terminal = "",
                ReceiveTime = DateTime.Now,
                StoreTimes = 0
            };
        }
        /// <summary>
        /// 将故障信息显示为字符串
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string ToString(TB_Data_EposFault entity)
        {
            return string.Format("Code: {0}, Description: {1}, FMI: {2}, Description: {3}",
                entity.CodeHex, entity.CodeName, entity.FMIHex, entity.FMIName);
        }
    }
}
