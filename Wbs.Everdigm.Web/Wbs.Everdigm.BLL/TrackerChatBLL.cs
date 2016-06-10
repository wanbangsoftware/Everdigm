using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.BLL
{
    public class TrackerChatBLL : BaseService<TB_TrackerChat>
    {
        public TrackerChatBLL() : base(new BaseRepository<TB_TrackerChat>())
        {
        }

        public override TB_TrackerChat GetObject()
        {
            return new TB_TrackerChat()
            {
                id = 0,
                Content = "",
                CreateTime = DateTime.Now,
                Deliver = null,
                MqttTag = "",
                Receiver = null,
                Status = 0,
                Type = 0
            };
        }

        public override string ToString(TB_TrackerChat entity)
        {
            return "";
        }
    }
}
