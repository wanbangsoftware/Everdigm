using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Wbs.Everdigm.BLL;

namespace Wbs.Everdigm.Desktop
{
    /// <summary>
    /// DataHandler的BLL集合
    /// </summary>
    public partial class DataHandler
    {
        /// <summary>
        /// 异常处理逻辑
        /// </summary>
        private ErrorBLL ErrorInstance { get { return new ErrorBLL(); } }
        /// <summary>
        /// 设备的BLL
        /// </summary>
        private EquipmentBLL EquipmentInstance { get { return new EquipmentBLL(); } }
        /// <summary>
        /// 终端的BLL
        /// </summary>
        private TerminalBLL TerminalInstance { get { return new TerminalBLL(); } }
        /// <summary>
        /// TX数据历史记录的BLL
        /// </summary>
        private DataBLL DataInstance { get { return new DataBLL(); } }
        /// <summary>
        /// 命令的BLL
        /// </summary>
        private CommandBLL CommandInstance { get { return new CommandBLL(); } }
        /// <summary>
        /// 位置信息记录的BLL
        /// </summary>
        private PositionBLL PositionInstance { get { return new PositionBLL(); } }
        /// <summary>
        /// 报警信息记录BLL
        /// </summary>
        private AlarmBLL AlarmInstance { get { return new AlarmBLL(); } }
        /// <summary>
        /// TX10G的BLL
        /// </summary>
        private TrackerBLL TrackerInstance { get { return new TrackerBLL(); } }
        /// <summary>
        /// TX10G的历史记录BLL
        /// </summary>
        private TrackerPositionBLL TrackerPosition { get { return new TrackerPositionBLL(); } }
        /// <summary>
        /// SMS消息处理BLL
        /// </summary>
        private SmsBLL SmsInstance { get { return new SmsBLL(); } }
        /// <summary>
        /// 提供MTMSN计算的BLL
        /// </summary>
        private IridiumMMSNBLL MtmsnInstance { get { return new IridiumMMSNBLL(); } }
        /// <summary>
        /// 铱星数据流量统计BLL
        /// </summary>
        private IridiumFlowBLL FlowInstance { get { return new IridiumFlowBLL(); } }
        /// <summary>
        /// 卫星模块BLL
        /// </summary>
        private SatelliteBLL SatelliteInstance { get { return new SatelliteBLL(); } }
        /// <summary>
        /// Excel转成PDF
        /// </summary>
        private ExcelHandlerBLL ExcelHandlerInstance { get { return new ExcelHandlerBLL(); } }
    }
}