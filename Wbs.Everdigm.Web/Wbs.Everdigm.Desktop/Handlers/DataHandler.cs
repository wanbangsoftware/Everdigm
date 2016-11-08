using System;

using Wbs.Sockets;
using Wbs.Utilities;
using Wbs.Everdigm.BLL;

namespace Wbs.Everdigm.Desktop
{
    /// <summary>
    /// 数据处理类
    /// </summary>
    public partial class DataHandler
    {
        /// <summary>
        /// 无法处理的数据
        /// </summary>
        public EventHandler<UIEventArgs> OnUnhandledMessage = null;
        /// <summary>
        /// 当前的Socket服务
        /// </summary>
        private SocketService _server = null;
        /// <summary>
        /// 设置Socket服务
        /// </summary>
        public SocketService Server { set { _server = value; } }
        
        public DataHandler()
        {
            LastOlderLinkHandledTime = DateTime.Now;
        }
        private string IP { get; set; }
        private int Port { get; set; }
        /// <summary>
        /// 格式化字符串
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string format(string format, params object[] args)
        {
            return string.Format(format, args) ;
        }
        /// <summary>
        /// 处理收到的数据
        /// </summary>
        /// <param name="data"></param>
        public void HandleData(AsyncUserDataBuffer data)
        {
            try
            {
                //if (data.PackageType == AsyncDataPackageType.UDP)
                {
                    IP = data.IP;
                    Port = data.Port;
                }
                bool handled = false;
                switch (data.DataType)
                {
                    case AsyncUserDataType.ClientConnected: break;
                    case AsyncUserDataType.ClientDisconnected:
                        // 处理客户端断开连接的情况
                        HandleClientDisconnect(data.SocketHandle);
                        break;
                    case AsyncUserDataType.ReceivedData:
                        var len = data.Buffer.Length;
                        // 如果收到的数据长度小于TX300的包头则不用处理数据了
                        if (len >= Protocol.TX300.TX300Items.header_length && Protocol.ProtocolTypes.IsTX300(data.Buffer[2]) && Protocol.TerminalTypes.IsTX300(data.Buffer[3]))
                        {
                            handled = true;
                            if (data.Buffer[0] != len)
                            {
                                HandleException("Data length(package length: " + data.Buffer[0] + ", buffer length: " + len + ") error.", CustomConvert.GetHex(data.Buffer));
                            }
                            else
                            {
                                HandleReceivedData(data);
                            }
                        }
                        else
                        {
                            // 增加未能处理的数据保存   2015/09/02 15:09
                            HandleException("Data is not TX protocol.", CustomConvert.GetHex(data.Buffer));
                        }
                        break;
                }
                if (data.PackageType == AsyncDataPackageType.SAT)
                {
                    if (!handled)
                    { 

                    }
                    data.Dispose();
                    data = null;
                }
            }
            catch (Exception e)
            {
                ShowUnhandledMessage(Now + e.Message + Environment.NewLine + 
                    e.StackTrace + Environment.NewLine + CustomConvert.GetHex(data.Buffer));
                HandleException(e.Message + Environment.NewLine + e.StackTrace, CustomConvert.GetHex(data.Buffer));
            }
            IP = "";
            Port = 0;
        }
        /// <summary>
        /// 向主界面提交未处理的消息
        /// </summary>
        /// <param name="message"></param>
        private void ShowUnhandledMessage(string message)
        {
            OnUnhandledMessage?.Invoke(this, new UIEventArgs() { Message = message });
        }
        /// <summary>
        /// 将Sim号码组成TX协议中的字节串
        /// </summary>
        /// <param name="sim"></param>
        /// <returns></returns>
        private byte[] SimToByte(string sim)
        {
            if (sim[0] == '8' && sim[1] == '9' && sim.Length == 8) { sim = format("0{0}000", sim); }
            else if (sim[0] == '9' && sim.Length == 8) { sim = format("0{0}000", sim); }
            else if (sim.Length == 11) { sim = format("0{0}", sim); }

            return CustomConvert.GetBytes(sim);
        }
        /// <summary>
        /// 获取当前系统时间的字符串
        /// </summary>
        private string Now { get { return DateTime.Now.ToString("[yyyy/MM/dd HH:mm:ss.fff] "); } }
        /// <summary>
        /// 将异常保存在数据库中备查
        /// </summary>
        /// <param name="trace"></param>
        /// <param name="data"></param>
        private void HandleException(string trace, string data)
        {
            var bll = new ErrorBLL();
            var obj = bll.GetObject();
            obj.ErrorData = data;
            obj.ErrorMessage = trace;
            bll.Add(obj);
        }
        /// <summary>
        /// 处理接受且还未处理地址信息的定位记录
        /// </summary>
        public void HandleGpsAddress()
        {
            try
            {
                var bll = new PositionBLL();
                var pos = bll.Find(f => f.Updated < 2);
                if (null != pos)
                {
                    ShowUnhandledMessage("position: " + pos.id);
                }
                // 清理获取GPS地址信息失败的记录
                bll.Update(f => f.Updated == 1 && f.ReceiveTime < DateTime.Now.AddMinutes(-10), act =>
                {
                    act.Updated = 0;
                });
            }
            catch (Exception e)
            {
                ShowUnhandledMessage(format("{0}GPS address handler error: {1}{2}{3}", Now, e.Message, Environment.NewLine, e.StackTrace));
            }
        }
    }
}