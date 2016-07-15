using System.Web;
using System.Diagnostics;
using Wbs.Everdigm.BLL;
using Wbs.Everdigm.Common;

namespace Wbs.Everdigm.Web.ajax
{
    /// <summary>
    /// work 的摘要说明
    /// </summary>
    public class work : BaseHttpHandler
    {
        private static string RET_FMT = "\"status\":{0},\"desc\":\"{1}\",\"data\":\"{2}\"";
        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);
            HandleRequest();
        }
        private string GetFormatedJson(int status, string desc, string data = null)
        {
            var ret = "{" + string.Format(RET_FMT, status, desc, null == data ? "" : data) + "}";
            return ret;
        }
        private WorkDetailBLL WorkDetailInstance { get { return new WorkDetailBLL(); } }
        private ExcelHandlerBLL ExcelHandlerInstance { get { return new ExcelHandlerBLL(); } }
        private void HandleRequest()
        {
            var ret = "";
            try
            {
                if (null == User)
                {
                    ret = GetFormatedJson(-1, "Your session has expired, Please try to login again.");
                }
                else
                {
                    switch (cmd)
                    {
                        case "detail":
                            // 生成工作项的文档
                            var id = int.Parse(Utility.Decrypt(data));
                            var detail = WorkDetailInstance.Find(f => f.id == id && f.Deleted == false);
                            if (null == detail)
                            {
                                ret = GetFormatedJson(-1, "Work is not exist.");
                            }
                            else
                            {
                                // 读取工作项，并保存到excel中
                                ret = SaveWorkHandlerRequest(id);//HandleWorkDetail(detail);
                            }
                            break;
                        case "excel":
                            ret = HandleWorkHandlerStatus();
                            break;
                    }
                }
            }
            finally
            {
                WorkDetailInstance.Close();
                ExcelHandlerInstance.Close();
            }
            ResponseJson(ret);
        }

        /// <summary>
        /// 保存请求生成打印文档的记录
        /// </summary>
        /// <param name="workId"></param>
        /// <returns></returns>
        private string SaveWorkHandlerRequest(int workId)
        {
            // 查找相同的工作是否已经有生成的文件
            var obj = ExcelHandlerInstance.Find(f => f.Type == (byte)ExcelExportType.TMSWork && f.Work == workId && f.Deleted == false);
            if (null == obj)
            {
                obj = ExcelHandlerInstance.GetObject();
                obj.Work = workId;
                // 生成TMS工作指派导出任务
                obj.Type = (byte)ExcelExportType.TMSWork;
                ExcelHandlerInstance.Add(obj);
            }
            else
            {
                if (obj.Status != 0)
                {
                    ExcelHandlerInstance.Update(f => f.id == obj.id, act =>
                    {
                        act.Handled = false;
                        act.Status = 0;
                        act.Data = "";
                    });
                }
            }
            return GetFormatedJson(0, "SUCESS", obj.id.ToString());
        }

        /// <summary>
        /// 查询处理结果的请求
        /// </summary>
        /// <returns></returns>
        private string HandleWorkHandlerStatus()
        {
            var id = ParseInt(data);
            var obj = ExcelHandlerInstance.Find(f => f.id == id && f.Deleted == false);
            if (null != obj)
            {
                if (obj.Handled == true)
                {
                    // 更新已删除状态
                    //ExcelHandlerInstance.Update(f => f.id == obj.id, act => { act.Deleted = true; });
                    if (obj.Status == 0)
                    {
                        return GetFormatedJson(1, "SUCCESS", obj.Target);
                    }
                    else
                    {
                        return GetFormatedJson(-1, "FAILED", obj.Data);
                    }
                }
                else
                {
                    return GetFormatedJson(0, "");
                }
            }
            else
            {
                return GetFormatedJson(-1, "Can not find work handler object, please try again.");
            }
        }

        
        /// <summary>
        /// 执行exe程序将pdf转换成
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="args"></param>
        private static void ExcutedCmd(string cmd, string args)
        {
            using (Process p = new Process())
            {

                ProcessStartInfo psi = new ProcessStartInfo(cmd, args);
                p.StartInfo = psi;
                p.Start();
                p.WaitForExit();
            }
        }
    }
}