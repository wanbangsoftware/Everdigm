using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wbs.Sockets;
using Wbs.Utilities;

namespace Wbs.Everdigm.Desktop
{
    public partial class DataHandler
    {
        private void CheckSMSData()
        {
            var sms = SmsInstance.Find(f => f.Handled == false);
            if (null != sms)
            {
                using (var data = new AsyncUserDataBuffer())
                {
                    data.Buffer = CustomConvert.GetBytes(sms.Data);
                    data.DataType = AsyncUserDataType.ReceivedData;
                    data.IP = "";
                    data.PackageType = AsyncDataPackageType.SMS;
                    data.Port = 0;
                    data.ReceiveTime = sms.SendTime.Value;
                    data.SocketHandle = 0;
                    HandleData(data);
                }
            }
        }
    }
}
