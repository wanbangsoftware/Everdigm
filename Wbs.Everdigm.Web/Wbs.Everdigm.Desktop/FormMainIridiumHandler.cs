using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Wbs.Utilities;
using Wbs.Sockets;

namespace Wbs.Everdigm.Desktop
{
    /// <summary>
    /// 铱星处理部分
    /// </summary>
    public partial class FormMain
    {
        /// <summary>
        /// 铱星DirectIP MO接收服务
        /// </summary>
        private IridiumServer _iridium;

        /// <summary>
        /// 要发送的铱星模块号码
        /// </summary>
        private string SendIMEI { get; set; }
        /// <summary>
        /// 要发送到铱星模块的内容
        /// </summary>
        private byte[] SendContent = null;
        /// <summary>
        /// 要发送的MTMSN号码
        /// </summary>
        private ushort SendMTMSN { get; set; }
        /// <summary>
        /// 启动铱星服务
        /// </summary>
        private void StartIridiumServer()
        {
            if (null == _iridium)
            {
                _iridium = new IridiumServer();
                _iridium.ShowPackageInformation = tsmiShowIridiumPackage.Checked;
                _iridium.OnMessage += new EventHandler<UIEventArgs>(OnServerMessage);
                _iridium.OnIridiumReceive += new EventHandler<IridiumDataEvent>(OnIridiumReceive);
                _iridium.Start(int.Parse(tstbIridiumPort.Text));
            }
        }
        /// <summary>
        /// 停止铱星服务
        /// </summary>
        private void StopIridiumServer()
        {
            if (null != _iridium)
            {
                _iridium.Stop();
            }
        }
        private void OnServerMessage(object sender, UIEventArgs e)
        {
            ShowHistory(e.Message, false);
        }
        /// <summary>
        /// 接收到铱星模块发送的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnIridiumReceive(object sender, IridiumDataEvent e)
        {
            // 加入铱星数据待处理列表
            _server.EnqueueIridiumData(e);
        }
        /// <summary>
        /// 手动分析指定的铱星数据包
        /// </summary>
        private void IridiumManualAnalysePackage()
        {
            //uint shor = IririumMTMSN.CalculateMTMSN(DateTime.Now, mtmsn++);
            //byte[] b = BitConverter.GetBytes(shor);
            //ShowHistory("short: " + shor + ", byte: " + CustomConvert.GetHex(b) + ", to: " + CustomConvert.GetHex(CustomConvert.reserve(b)), false);
            //if (shor == 0)
            //{ shor = 1; }
            var temp = CustomConvert.GetBytes(tstbData.Text.Trim());
            var len = temp.Length;
            var buffer = new IridiumBuffer();
            buffer.socket = null;
            buffer.length = len;
            buffer.buffer = new byte[len];
            Buffer.BlockCopy(temp, 0, buffer.buffer, 0, len);
            _iridium.AddQueue(buffer);
            temp = null;
        }
        /// <summary>
        /// 手动发送一条铱星数据
        /// </summary>
        private void IridiumManualSend()
        {
            //if (_iridium.ShowPackageInformation)
            {
                ShowHistory(Environment.NewLine + Now + "MT operation begin, try to connect Iridium server...", false);
            }
            tsbtSend.Enabled = false;
            SendIMEI = tscbIMEI.Text.Trim();
            if (string.IsNullOrEmpty(tstbData.Text.Trim()))
            {
                SendContent = null;
            }
            else
            {
                SendContent = CustomConvert.GetBytes(tstbData.Text.Trim());
            }
            SendMTMSN = mtmsn++;
            performSend();
            Task task = new Task(() =>
            {
                Thread.Sleep(5000);
                BeginInvoke((MyInvoker)delegate
                {
                    tsbtSend.Enabled = true;
                });
            });
            task.Start();
        }
        /// <summary>
        /// 铱星发送命令事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnIridiumSend(object sender, IridiumDataEvent e)
        {
            SendIMEI = e.Data.IMEI;
            SendContent = new byte[e.Data.Length];//CustomConvert.GetHex(e.Data.Payload);
            Buffer.BlockCopy(e.Data.Payload, 0, SendContent, 0, e.Data.Length);
            // 服务器生成的MTMSN
            SendMTMSN = e.Data.MTMSN;
            // 加入新生成的MTMSN到监控列表里
            _iridium.AddMTMSN(e.Data.MTMSN);

            e = null;
            BeginInvoke((MyInvoker)delegate
            {
                performSend();
                ShowHistory(Environment.NewLine + Now + "Iridium Command: " + CustomConvert.GetHex(SendContent), false);
            });
        }
        /// <summary>
        /// 尝试发送铱星命令
        /// </summary>
        private void performSend()
        {
            var client = new TcpClient();
            client.BeginConnect(IPAddress.Parse(tstbIridiumServer.Text), int.Parse(tstbIridiumPort.Text),
                new AsyncCallback(Connected_Callback), client);
        }
        private ushort mtmsn = 1;
        private bool FlushMTQueue = false;
        private void Connected_Callback(IAsyncResult ar)
        {
            try
            {
                TcpClient client = (TcpClient)ar.AsyncState;
                if (client.Connected)
                {
                    client.EndConnect(ar);
                    //if (_iridium.ShowPackageInformation)
                    {
                        ShowHistory("Iridium server has connected.", true);
                    }
                    ClientState cs = new ClientState();
                    cs.IsConnected = true;
                    cs.client = client;
                    cs.length = 0;
                    cs.received = null;
                    client.GetStream().BeginRead(cs.buffer, 0, ClientState.SIZE, new AsyncCallback(Read_Callback), cs);
                    // 组包并发送
                    // 数据内容
                    MTPayload mtp = new MTPayload();
                    mtp.Payload = SendContent;
                    mtp.Package();

                    // Header
                    MTHeader mth = new MTHeader();
                    mth.UniqueID = SendMTMSN;
                    mth.IMEI = SendIMEI;
                    mth.DispositionFlags = (ushort)(FlushMTQueue ? 1 : 32);
                    mth.Package();
                    // 整包
                    Iridium iridium = new Iridium();
                    iridium.Content = new byte[mth.Content.Length + (FlushMTQueue ? 0 : mtp.Content.Length)];
                    Buffer.BlockCopy(mth.Content, 0, iridium.Content, 0, mth.Content.Length);
                    if (!FlushMTQueue)
                    {
                        // 如果不是删除队列标记则添加payload数据
                        Buffer.BlockCopy(mtp.Content, 0, iridium.Content, mth.Content.Length, mtp.Content.Length);
                    }
                    iridium.Package();

                    client.GetStream().Write(iridium.PackageContent, 0, iridium.PackageContent.Length);
                    var buffer = new IridiumBuffer();
                    buffer.length = iridium.PackageContent.Length;
                    buffer.buffer = new byte[buffer.length];
                    Buffer.BlockCopy(iridium.PackageContent, 0, buffer.buffer, 0, buffer.length);
                    _iridium.AddQueue(buffer);
                }
                else
                {
                    ShowHistory("Can not establish connect to Iridium server.", true);
                    client.Close();
                    client = null;
                }
            }
            catch (Exception e)
            {
                ShowHistory("Connect_Callback error: " + e.Message + Environment.NewLine + e.StackTrace, true);
            }
        }
        private void Read_Callback(IAsyncResult ar)
        {
            try
            {
                ClientState cs = (ClientState)ar.AsyncState;
                if (null == cs) return;
                if (null == cs.client) return;
                if (!cs.client.Connected)
                {
                    cs.Dispose();
                    try
                    {
                        cs.client.Close();
                        //cs.client.GetStream().Close();
                    }
                    catch { }
                    cs.client = null;
                    return;
                }
                if (cs.IsConnected == false) return;

                var len = cs.client.GetStream().EndRead(ar);
                if (len > 0)
                {
                    if (null == cs.received)
                    {
                        cs.received = new byte[len];
                        cs.length = len;
                    }
                    else
                    {
                        cs.length += len;
                        cs.received = CustomConvert.expand(cs.received, cs.length);
                    }
                    Buffer.BlockCopy(cs.buffer, 0, cs.received, cs.length - len, len);
                    cs.client.GetStream().BeginRead(cs.buffer, 0, ClientState.SIZE, new AsyncCallback(Read_Callback), cs);
                    if (cs.length >= cs.received[2])
                    {
                        var buffer = new IridiumBuffer();
                        buffer.socket = null;
                        buffer.length = cs.length;
                        buffer.buffer = new byte[cs.length];
                        Buffer.BlockCopy(cs.received, 0, buffer.buffer, 0, cs.length);
                        cs.length = 0;
                        cs.received = null;
                        _iridium.AddQueue(buffer);
                        try
                        {
                            cs.Dispose();
                            cs.client.Close();
                            //if (_iridium.ShowPackageInformation)
                            ShowHistory("MT operation complete, client disconnected.", true);
                        }
                        catch (Exception e)
                        {
                            ShowHistory("Handle Iridium package error: " + e.Message + Environment.NewLine + e.StackTrace, true);
                        }
                        finally
                        {
                            cs.client = null;
                            cs = null;
                        }
                    }
                }
                else
                {
                    try
                    {
                        cs.Dispose();
                        cs.client.GetStream().Close();
                        cs.client.Close();
                    }
                    catch (Exception ee)
                    {
                        ShowHistory("Handle Iridium package error(len<=0): " + ee.Message + Environment.NewLine + ee.StackTrace, true);
                    }
                    finally
                    {
                        cs = null;
                    }
                }
            }
            catch (Exception error)
            { ShowHistory("Read_Callback error: " + error.Message + Environment.NewLine + error.StackTrace, true); }
        }

        private class ClientState : IDisposable
        {
            public TcpClient client;
            public const int SIZE = 4096;
            public byte[] buffer = new byte[SIZE];
            public int length = 0;
            public byte[] received = null;
            /// <summary>
            /// 增加客户端是否还正常连接的标记，Connect_Callback时标记为true
            /// Read_Callback的时候判断MT流程结束时为false
            /// </summary>
            public bool IsConnected = false;
            ~ClientState()
            { Dispose(); }
            public void Dispose()
            {
                length = 0;
                IsConnected = false;
                received = null;
            }
        }

    }
}
