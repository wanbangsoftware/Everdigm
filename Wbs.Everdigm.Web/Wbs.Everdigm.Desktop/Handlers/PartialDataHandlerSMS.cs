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
        public void CheckSMSData()
        {
            var sms = SmsInstance.Find(f => f.Handled == false);
            if (null != sms)
            {
                using (var data = new AsyncUserDataBuffer())
                {
                    data.Buffer = Convert.FromBase64String(sms.Data);
                    data.DataType = AsyncUserDataType.ReceivedData;
                    data.IP = "";
                    data.PackageType = AsyncDataPackageType.SMS;
                    data.Port = 0;
                    data.ReceiveTime = sms.SendTime.Value;
                    data.SocketHandle = 0;
                    ShowUnhandledMessage(string.Format("{0}SMS data from {1}: {2}", Now, sms.Sender, CustomConvert.GetHex(data.Buffer)));
                    // 如果数据中的sim号码是000000000000则将sender直接放入其中   2015/09/02 15:18
                    if (data.Buffer[9] == 0 && data.Buffer[10] == 0)
                    {
                        byte[] t = SimToByte(sms.Sender);
                        Buffer.BlockCopy(t, 0, data.Buffer, 9, 6);
                        t = null;
                    }
                    HandleData(data);
                }
                UpdateSMSDataHandled(sms.id);
            }
        }
        private void UpdateSMSDataHandled(long id)
        {
            SmsInstance.Update(f => f.id == id, act => { act.Handled = true; });
        }
    }
}
