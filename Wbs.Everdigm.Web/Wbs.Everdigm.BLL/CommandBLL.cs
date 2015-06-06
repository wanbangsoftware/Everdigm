using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.BLL
{
    /// <summary>
    /// 命令数据处理业务
    /// </summary>
    public class CommandBLL : BaseService<TB_Command>
    {
        public CommandBLL()
            : base(new BaseRepository<TB_Command>())
        { }
        /// <summary>
        /// 生成一个新的命令记录实例
        /// </summary>
        /// <returns></returns>
        public override TB_Command GetObject()
        {
            return new TB_Command()
            {
                ActualSendTime = (DateTime?)null,
                Command = "",
                Content = "",
                IridiumMTMSN = 0,
                DataType = 2,
                Terminal = (int?)null,
                DestinationNo = "",
                ScheduleTime = DateTime.Now,
                SendUser = (int?)null,
                Status = 0
            };
        }
        public override string ToString(TB_Command entity)
        {
            return "";
        }
        /// <summary>
        /// 通过Id查找命令记录
        /// </summary>
        /// <param name="commandId"></param>
        /// <returns></returns>
        public TB_Command Find(int commandId)
        {
            return CurrentRepository.Find(c => c.id == commandId);
        }
    }
}
