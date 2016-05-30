using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wbs.Everdigm.Database;
using Wbs.Everdigm.BLL;

namespace Wbs.Everdigm.Web.ajax
{
    public partial class query
    {
        private ExcelHandlerBLL DailyInstance = new ExcelHandlerBLL();
        /// <summary>
        /// 返回json值
        /// </summary>
        /// <param name="status"></param>
        /// <param name="desc"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private string DailyWorkReturn(int status, string desc, string data="")
        {
            return string.Format("{0}\"status\":{1},\"desc\":\"{2}\",\"data\":\"{3}\"{4}", "{", status, desc, data, "}");
        }
        /// <summary>
        /// 生成请求
        /// </summary>
        /// <returns></returns>
        private string HandleQueryEquipmentWorkTime2Excel()
        {
            var id = ParseInt(Utility.Decrypt(data));
            var obj = EquipmentInstance.Find(f => f.id == id && f.Deleted == false);
            var json = HandleQueryEquipmentWorktime(false);
            var date1 = GetParamenter("date");
            var date2 = GetParamenter("date1");
            var daily = DailyInstance.Find(f => f.Equipment == id && f.Work == (int?)null && f.Deleted == false && f.StartDate == date1 && f.EndDate == date2);
            if (null == daily)
            {
                daily = DailyInstance.GetObject();
                daily.Data = json;
                daily.Equipment = id;
                // json为空时，不需要处理了
                daily.Handled = string.IsNullOrEmpty(json) || json.Equals("[]");
                daily.StartDate = GetParamenter("date");
                daily.EndDate = GetParamenter("date1");
                daily = DailyInstance.Add(daily);
            }
            else
            {
                DailyInstance.Update(f => f.id == daily.id, act =>
                {
                    act.Data = json;
                    // json为空时，不需要处理了
                    daily.Handled = string.IsNullOrEmpty(json) || json.Equals("[]");
                });
            }

            return DailyWorkReturn(0, "SUCCESS", daily.id.ToString());
        }
        /// <summary>
        /// 查询Daily Work Time 2 Excel操作进程
        /// </summary>
        /// <returns></returns>
        private string HandleQueryWorkTime2ExcelStatus()
        {
            var id = ParseInt(data);
            var obj = DailyInstance.Find(f => f.id == id && f.Deleted == false);
            if (null == obj)
            {
                return DailyWorkReturn(-1, "Can not find your operation");
            }
            else
            {
                if (obj.Handled == true)
                {
                    if (string.IsNullOrEmpty(obj.Target) || obj.Data.Equals("[]"))
                    {
                        // 下载目标不存在时，提示用户没有记录，不需要等待下载了
                        return DailyWorkReturn(-1, "No any more records exists.");
                    }
                    else {
                        // 更新已删除状态
                        //ExcelHandlerInstance.Update(f => f.id == obj.id, act => { act.Deleted = true; });
                        return DailyWorkReturn(1, "SUCCESS", obj.Target);
                    }
                }
                else
                {
                    return DailyWorkReturn(0, "");
                }
            }
        }
    }
}