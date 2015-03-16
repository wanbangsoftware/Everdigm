using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wbs.Everdigm.Web.ajax
{
    /// <summary>
    /// 这里提供终端信息查询
    /// </summary>
    public partial class query
    {
        /// <summary>
        /// 处理终端的查询
        /// </summary>
        private void HandleTerminalQuery()
        {
            var ret = "[]";
            switch (cmd)
            {
                case "bound":
                case "notbound":
                    // 查询是否绑定设备的终端列表
                    var bound = TerminalInstance.FindList(f =>
                        f.Delete == false && (f.HasBound == ("bound" == cmd ? true : false)) &&
                        (f.Number.IndexOf(data) >= 0 || f.Sim.IndexOf(data) >= 0)).Take(10).ToList();
                    ret = JsonConverter.ToJson(bound);
                    break;
                case "single":
                    // 查询单个终端
                    var single = TerminalInstance.FindList(f => f.Number.Equals(data) && f.Delete == false).ToList();
                    ret = JsonConverter.ToJson(single);
                    break;
            }
            ResponseJson(ret);
        }
    }
}