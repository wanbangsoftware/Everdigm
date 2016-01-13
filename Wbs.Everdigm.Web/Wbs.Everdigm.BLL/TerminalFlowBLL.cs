using System;

using Wbs.Everdigm.Database;
using Wbs.Utilities;

namespace Wbs.Everdigm.BLL
{
    public class TerminalFlowBLL : BaseService<TB_TerminalFlow>
    {
        public TerminalFlowBLL()
            : base(new BaseRepository<TB_TerminalFlow>())
        { }
        public override TB_TerminalFlow GetObject()
        {
            return new TB_TerminalFlow()
            {
                id = -1,
                GPRSDeliver = 0,
                GPRSReceive = 0,
                Monthly = int.Parse(DateTime.Now.ToString("yyyyMM")),
                Terminal = (int?)null,
                SMSDeliver = 0,
                SMSReceive = 0,
                Sim = ""
            };
        }

        public override string ToString(TB_TerminalFlow entity)
        {
            return string.Format("{0}: {1}, GPRS Received: {2}, GPRS Deliver: {3}, SMS Received: {4}, SMS Deliver: {5}",
                entity.TB_Terminal.Sim, entity.Monthly, CustomConvert.FormatSize(entity.GPRSReceive.Value),
                CustomConvert.FormatSize(entity.GPRSDeliver.Value), entity.SMSReceive, entity.SMSDeliver);
        }
    }
}
