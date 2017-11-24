using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wbs.Everdigm.Web.ajax
{
    /// <summary>
    /// test 的摘要说明
    /// </summary>
    public class test : BaseHttpHandler
    {

        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);
            HandleRequest();
        }

        private void HandleRequest()
        {
            switch (type)
            {
                case "list":
                    ResponseJson(Properties.Resources.testQueryJson);
                    break;
                default:
                    ResponseJson(Properties.Resources.testSingleJson);
                    break;
            }
        }
    }
}