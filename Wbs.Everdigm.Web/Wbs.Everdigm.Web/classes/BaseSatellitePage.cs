
using Wbs.Everdigm.BLL;

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