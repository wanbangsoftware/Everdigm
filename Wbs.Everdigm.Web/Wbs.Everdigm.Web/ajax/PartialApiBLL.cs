using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wbs.Everdigm.BLL;

namespace Wbs.Everdigm.Web.ajax
{
    /// <summary>
    /// BLL
    /// </summary>
    public partial class api
    {
        /// <summary>
        /// app版本查询
        /// </summary>
        private AppBLL AppInstance { get { return new AppBLL(); } }
    }
}