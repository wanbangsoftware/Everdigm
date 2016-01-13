
using Wbs.Everdigm.BLL;

namespace Wbs.Everdigm.Web.ajax
{
    /// <summary>
    /// 这里提供query的所有BLL
    /// </summary>
    public partial class query
    {
        /// <summary>
        /// EPOS报警信息处理逻辑
        /// </summary>
        private EposAlarmBLL EposInstance { get { return new EposAlarmBLL(); } }
        /// <summary>
        /// 报警历史记录业务逻辑
        /// </summary>
        private AlarmBLL AlarmInstance { get { return new AlarmBLL(); } }
        /// <summary>
        /// 定位历史记录业务逻辑
        /// </summary>
        private PositionBLL PositionInstance { get { return new PositionBLL(); } }
        /// <summary>
        /// 数据历史记录业务逻辑
        /// </summary>
        private DataBLL DataInstance { get { return new DataBLL(); } }
        /// <summary>
        /// 设备出入库记录业务处理实体
        /// </summary>
        private StoreHistoryBLL StorageInstance { get { return new StoreHistoryBLL(); } }
        /// <summary>
        /// 终端业务逻辑
        /// </summary>
        private TerminalBLL TerminalInstance { get { return new TerminalBLL(); } }
        /// <summary>
        /// 设备相关业务处理逻辑
        /// </summary>

        private EquipmentBLL EquipmentInstance { get { return new EquipmentBLL(); } }
        /// <summary>
        /// 客户信心业务处理实体
        /// </summary>
        private CustomerBLL CustomerInstance { get { return new CustomerBLL(); } }

        /// <summary>
        /// Tracker历史记录BLL
        /// </summary>
        private TrackerPositionBLL TrackerPositionInstance { get { return new TrackerPositionBLL(); } }
    }
}