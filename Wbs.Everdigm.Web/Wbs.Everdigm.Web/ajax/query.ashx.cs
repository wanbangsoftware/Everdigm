using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Wbs.Everdigm.BLL;
using Wbs.Everdigm.Database;
using Wbs.Protocol.TX300;
using Wbs.Protocol.TX300.Analyse;

namespace Wbs.Everdigm.Web.ajax
{
    /// <summary>
    /// 查询
    /// </summary>
    public partial class query : BaseHttpHandler
    {
        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);
            HandleRequest();
        }
        /// <summary>
        /// 处理客户端过来的请求
        /// </summary>
        private void HandleRequest()
        {
            switch (type)
            { 
                case "terminal":
                    // 查询终端
                    HandleTerminalQuery();
                    break;
                case "equipment":
                    // 查询设备
                    HandleEquipmentQuery();
                    break;
                case "customer":
                    // 查询客户信息
                    HandleCustomerQuery();
                    break;
            }
        }
    }
}