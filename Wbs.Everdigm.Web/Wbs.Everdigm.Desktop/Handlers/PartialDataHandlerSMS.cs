using System;

using Wbs.Sockets;
using Wbs.Utilities;
using Wbs.Everdigm.BLL;

namespace Wbs.Everdigm.Desktop
{
    public partial class DataHandler
    {
        private DateTime _lastCheckSMSData = DateTime.Now;
        public bool CanCheckSMSData()
        {
            return _lastCheckSMSData.Subtract(DateTime.Now).Duration().TotalSeconds >= 5;
        }
        /// <summary>
        /// 每5秒获取一次未处理的SMS数据
        /// </summary>
        public void CheckSMSData()
        {
            if (_lastCheckSMSData.Subtract(DateTime.Now).Duration().TotalSeconds < 5) 
                return;
            _lastCheckSMSData = DateTime.Now;

            using (var bll = new SmsBLL())
            {
                var sms = bll.Find(f => f.Handled == false);
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
                        ShowUnhandledMessage(format("{0}SMS data from {1}: {2}", Now, sms.Sender, CustomConvert.GetHex(data.Buffer)));
                        // 如果数据中的sim号码是000000000000则将sender直接放入其中   2015/09/02 15:18
                        if (data.Buffer[9] == 0 && data.Buffer[10] == 0)
                        {
                            byte[] t = SimToByte(sms.Sender);
                            Buffer.BlockCopy(t, 0, data.Buffer, 9, 6);
                            t = null;
                        }
                        HandleData(data);
                    }
                    bll.Update(f => f.id == sms.id, act => { act.Handled = true; });
                }
            }
        }
    }
}
