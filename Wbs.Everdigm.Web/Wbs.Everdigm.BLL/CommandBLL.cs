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
    public class CommandBLL : BaseService<CT_00000>
    {
        public CommandBLL()
            : base(new BaseRepository<CT_00000>())
        { }
        /// <summary>
        /// 生成一个新的命令记录实例
        /// </summary>
        /// <returns></returns>
        public override CT_00000 GetObject()
        {
            return new CT_00000()
            {
                u_sms_actual_send_time = null,
                u_sms_command = "",
                u_sms_confirm_send_time = null,
                u_sms_content = "",
                u_sms_data_type = 2,
                u_sms_mobile_no = "",
                u_sms_id = 0,
                u_sms_mobile_type = 0,
                u_sms_retry_times = 0,
                u_sms_schedule_time = DateTime.Now,
                u_sms_send_status = "",
                u_sms_send_status_time = null,
                u_sms_status = 0
            };
        }
        public override string ToString(CT_00000 entity)
        {
            return "";
        }
        /// <summary>
        /// 通过Id查找命令记录
        /// </summary>
        /// <param name="commandId"></param>
        /// <returns></returns>
        public CT_00000 Find(int commandId)
        {
            return CurrentRepository.Find(c => c.u_sms_id == commandId);
        }
    }
}
