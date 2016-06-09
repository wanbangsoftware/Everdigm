using System;

using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.BLL
{
    public class SmsBLL : BaseService<TB_SMS>
    {
        public SmsBLL()
            : base(new BaseRepository<TB_SMS>())
        { }

        public override TB_SMS GetObject()
        {
            return new TB_SMS()
            {
                id = 0,
                Data = "",
                ReceiveTime = DateTime.Now,
                Sender = "",
                Type = 0,
                Handled = false,
                SendTime = DateTime.Now
            };
        }

        public override string ToString(TB_SMS entity)
        {
            return string.Format("ID: {0}, Time: {2}, Sender: {2}, Data: {3}", 
                entity.id, entity.SendTime.Value.ToString("YYYY/MM/dd HH:mm:ss"), entity.Sender, entity.Data);
        }
    }
}
