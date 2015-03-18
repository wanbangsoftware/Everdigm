using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Wbs.Everdigm.BLL;
using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.Web
{
    public class BaseSatellitePage : BaseBLLPage
    {
        /// <summary>
        /// 卫星信息业务处理逻辑实体
        /// </summary>
        protected SatelliteBLL SatelliteInstance { get { return new SatelliteBLL(); } }
    }
}