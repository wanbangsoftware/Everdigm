using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wbs.Everdigm.Common;
using Wbs.Everdigm.Database;
using Wbs.Utilities;

namespace Wbs.Everdigm.Desktop
{
    /// <summary>
    /// 提供处理铱星数据的集合
    /// </summary>
    public partial class DataHandler
    {
        /// <summary>
        /// 处理掉铱星发过来的数据
        /// </summary>
        /// <param name="data"></param>
        public void HandleIridiumData(IridiumData data)
        {
            switch (data.Type)
            {
                case IridiumDataType.MTServerSendStatus:
                    HandleIridiumMTServerSendStatus(data);
                    break;
                case IridiumDataType.MTModelReceiveStatus:
                    HandleIridiumMTModelReceiveStatus(data);
                    break;
                case IridiumDataType.MOPayload:
                    HandleIridiumModelMOPayload(data);
                    break;
            }
        }
        /// <summary>
        /// 处理铱星命令发送状态
        /// </summary>
        /// <param name="data"></param>
        private void HandleIridiumMTServerSendStatus(IridiumData data)
        {
            CommandInstance.Update(f => f.TB_Terminal.TB_Satellite.CardNo.Equals(data.IMEI) &&
                f.Status == (byte)CommandStatus.SatelliteHandled && f.ActualSendTime == (DateTime?)null, action =>
                {
                    // 更新等待铱星处理的终端的命令为已发送状态
                    action.Status = (byte)CommandStatus.SentBySAT;
                    action.IridiumMTMSN = data.MTMSN;
                    action.ActualSendTime = DateTime.Now;
                });
        }
        /// <summary>
        /// 处理1小时内已被卫星模块接收了的命令为正常返回状态
        /// </summary>
        private void HandleIridiumCommandResponseed(IridiumData data)
        {
            CommandInstance.Update(f => f.TB_Terminal.TB_Satellite.CardNo.Equals(data.IMEI) && f.Command == "0x1000" &&
            f.Status == (byte)CommandStatus.SentToDestBySAT && f.ActualSendTime > DateTime.Now.AddMinutes(-60), act =>
            {
                act.Status = (byte)CommandStatus.Returned;
            });
        }
        /// <summary>
        /// 处理铱星终端接收命令的状态
        /// </summary>
        /// <param name="data"></param>
        private void HandleIridiumMTModelReceiveStatus(IridiumData data)
        {
            CommandInstance.Update(f => f.TB_Terminal.TB_Satellite.CardNo.Equals(data.IMEI) &&
                f.IridiumMTMSN == data.MTMSN && f.Status == (byte)CommandStatus.SentBySAT, act =>
            {
                act.Status = (byte)CommandStatus.SentToDestBySAT;
            });
        }
        /// <summary>
        /// 空报警
        /// </summary>
        private static string ALARM = "0000000000000000";
        /// <summary>
        /// 处理新版的卫星通讯协议
        /// </summary>
        /// <param name="data"></param>
        private void HandleIridiumNewProtocolPackage(IridiumData data)
        {
            if (data.Payload[0] == 0x01)
            { 
                // 新版的卫星通讯协议
                uint worktime = BitConverter.ToUInt32(data.Payload, 13);
                string locks = CustomConvert.GetHex(data.Payload[1]);
                string alarms = CustomConvert.IntToDigit(data.Payload[2], CustomConvert.BIN, 8) +
                    CustomConvert.IntToDigit(data.Payload[3], CustomConvert.BIN, 8);
                IridiumLocation location = new IridiumLocation();
                location.LatLng = new byte[IridiumLocation.SIZE];
                Buffer.BlockCopy(data.Payload, 4, location.LatLng, 0, IridiumLocation.SIZE);
                location.Unpackate();
                if (location.Available)
                {
                    var terminal = TerminalInstance.Find(f => f.TB_Satellite.CardNo.Equals(data.IMEI));
                    TB_Equipment equipment = null;
                    if (null != terminal)
                    {
                        TerminalInstance.Update(f => f.id == terminal.id, act =>
                        {
                            act.OnlineStyle = (byte)LinkType.SATELLITE;
                            // 同时更新终端的最后链接时间
                            act.OnlineTime = DateTime.Now;
                        });
                        equipment = EquipmentInstance.Find(f => f.Terminal == terminal.id);
                        if (null != equipment)
                        {
                            EquipmentInstance.Update(f => f.id == equipment.id, act =>
                            {
                                act.OnlineStyle = (byte)LinkType.SATELLITE;
                                act.OnlineTime = DateTime.Now;
                                // 更新设备的报警状态 2015/09/10 14:04
                                act.Alarm = alarms;

                                act.LastAction = "0x1000";
                                act.LastActionBy = "SAT";
                                act.LastActionTime = data.Time;
                                // 更新定位信息 2015/09/09 23:29
                                if (location.Available)
                                {
                                    act.Latitude = location.Latitude;
                                    act.Longitude = location.Longitude;
                                }
                                // 更新启动与否状态 2015/08/31
                                act.Voltage = location.EngFlag == "On" ? "G2400" : "G0000";
                                // 如果回来的运转时间比当前时间大则更新成为On状态  暂时  2015/09/02
                                if (worktime > act.Runtime)
                                {
                                    // act.Voltage = "G2400";
                                    act.Runtime = (int)worktime;
                                }
                                else
                                {
                                    if (worktime > 0)
                                    {
                                        // 运转时间不为零的话，更新运转时间
                                        act.Runtime = (int)worktime;
                                    }
                                }
                                // 锁车状态 2015/08/14
                                if (act.LockStatus != locks) { act.LockStatus = locks; }
                            });
                        }
                    }
                    // 保存TX300历史记录
                    SaveTX300History(new Protocol.TX300.TX300()
                    {
                        CommandID = 0x1000,
                        MsgContent = data.Payload,
                        ProtocolType = Wbs.Protocol.ProtocolTypes.SATELLITE,
                        TerminalType = terminal.Type.Value,
                        TerminalID = null == terminal ? "" : terminal.Sim,
                        TotalLength = (ushort)data.Payload.Length
                    }, data.Time, EquipmentInstance.GetFullNumber(equipment));

                    var pos = PositionInstance.GetObject();
                    pos.Latitude = location.Latitude;
                    pos.Longitude = location.Longitude;
                    pos.NS = location.NSI;
                    pos.EW = location.EWI;
                    pos.StoreTimes = null == equipment ? 0 : equipment.StoreTimes;
                    pos.GpsTime = data.Time;
                    pos.Equipment = null == equipment ? (int?)null : equipment.id;
                    pos.Terminal = null == terminal ? "" : (terminal.Sim.Length < 11 ? (terminal.Sim + "000") : terminal.Sim);
                    pos.Type = location.Report + "(Eng " + location.EngFlag + ")(SAT)";
                    try
                    {
                        PositionInstance.Add(pos);
                        // 处理报警信息
                        if (alarms != ALARM)
                        {
                            var arm = AlarmInstance.GetObject();
                            arm.Code = alarms;
                            arm.Equipment = null == equipment ? (int?)null : equipment.id;
                            arm.Position = pos.id;
                            arm.StoreTimes = null == equipment ? 0 : equipment.StoreTimes;
                            arm.Terminal = null == terminal ? "" : (terminal.Sim.Length < 11 ? (terminal.Sim + "000") : terminal.Sim);
                            AlarmInstance.Add(arm);
                        }
                    }
                    catch (Exception e)
                    {
                        OnUnhandledMessage(this, new Sockets.UIEventArgs()
                        {
                            Message = e.Message + Environment.NewLine + e.StackTrace + Environment.NewLine + PositionInstance.ToString(pos)
                        });
                    }
                }
                // 更新卫星方式的命令状态(只处理命令回复的1000，其他的命令在普通命令过程中处理)
                if (location.ReportType == 1 && data.Payload.Length <= 17)
                {
                    HandleIridiumCommandResponseed(data);
                }
                location.Dispose();
                location = null;
            }
        }
        /// <summary>
        /// 处理铱星模块发送回来的MO信息
        /// </summary>
        /// <param name="data"></param>
        private void HandleIridiumModelMOPayload(IridiumData data)
        {
            // 如果是正式的协议则以正常的方式处理
            if (data.Length >= Wbs.Protocol.TX300.TX300Items.header_length)
            {
                // TX300通讯协议首字节必定大于等于17
                // 卫星通讯协议首字节必定等于01
                if (data.Payload[0] >= 17 && 
                    Wbs.Protocol.ProtocolTypes.IsTX300(data.Payload[2]) &&
                            Wbs.Protocol.TerminalTypes.IsTX300(data.Payload[3]))
                {
                    // 根据卫星号码查询终端的Sim卡号码并将其填入包头结构里
                    var terminal = TerminalInstance.Find(f => f.TB_Satellite.CardNo.Equals(data.IMEI) && f.Delete == false);
                    if (null != terminal)
                    {
                        var sim = terminal.Sim;
                        sim += sim.Length < 11 ? "000" : "";
                        sim = "0" + sim;
                        var s = CustomConvert.GetBytes(sim);
                        Buffer.BlockCopy(s, 0, data.Payload, 9, s.Length);
                        s = null;
                        // 更新命令的MTMSN状态为返回状态
                        HandleData(new Sockets.AsyncUserDataBuffer()
                        {
                            Buffer = data.Payload,
                            DataType = Sockets.AsyncUserDataType.ReceivedData,
                            IP = "",
                            PackageType = Sockets.AsyncDataPackageType.SAT,
                            Port = 0,
                            ReceiveTime = DateTime.Now,
                            SocketHandle = 0
                        });
                    }
                    else
                    {
                        ShowUnhandledMessage("Unbind satellite report data: " + CustomConvert.GetHex(data.Payload));
                    }
                }
                else { HandleIridiumNewProtocolPackage(data); }
                // 统计铱星的接收数据次数和数据长度
                HandleIridiumMOFlow(data);
            }
            data.Dispose();
        }
        /// <summary>
        /// 记录铱星的MO次数和长度
        /// </summary>
        /// <param name="data"></param>
        private void HandleIridiumMOFlow(IridiumData data) {
            var iridium = SatelliteInstance.Find(f => f.CardNo.Equals(data.IMEI));
            if (null != iridium)
            {
                var monthly = int.Parse(DateTime.Now.ToString("yyyyMM"));
                var flow = FlowInstance.Find(f => f.Iridium == iridium.id && f.Monthly == monthly);
                if (null == flow)
                {
                    flow = FlowInstance.GetObject();
                    flow.Iridium = iridium.id;
                    flow.MOTimes = 1;
                    flow.MOPayload = data.Length;
                    FlowInstance.Add(flow);
                }
                else
                {
                    FlowInstance.Update(f => f.id == flow.id, act =>
                    {
                        act.MOTimes += 1;
                        act.MOPayload += data.Length;
                    });
                }
            }
        }
        /// <summary>
        /// 记录铱星的MT次数和长度
        /// </summary>
        private void HandleIridiumMTFlow(int iridium, int length)
        {
            var monthly = int.Parse(DateTime.Now.ToString("yyyyMM"));
            var flow = FlowInstance.Find(f => f.Iridium == iridium && f.Monthly == monthly);
            if (null == flow)
            {
                flow = FlowInstance.GetObject();
                flow.Iridium = iridium;
                flow.MTTimes = 1;
                flow.MTPayload = length;
                FlowInstance.Add(flow);
            }
            else
            {
                FlowInstance.Update(f => f.id == flow.id, act =>
                {
                    act.MTTimes += 1;
                    act.MTPayload += length;
                });
            }
        }
        /// <summary>
        /// 检测需要用铱星方式发送的命令
        /// </summary>
        public void CheckIridiumCommand()
        {
            if (null == _server) return;

            var list = CommandInstance.FindList(f => f.ScheduleTime >= DateTime.Now.AddSeconds(-30) &&
                f.Status == (byte)CommandStatus.WaitingForSatellite && f.ActualSendTime == (DateTime?)null).ToList();
            if (null != list && list.Count > 0)
            {
                HandleIridiumCommand(list.First());
            }
        }
        /// <summary>
        /// 从数据库中获取当月发送的MTMSN号码
        /// </summary>
        /// <returns></returns>
        private ushort GetIridiumMTMSN()
        {
            ushort num = 0;
            var now = DateTime.Now;
            var obj = MtmsnInstance.Find(f => f.Year == (short)now.Year && f.Month == (byte)now.Month);
            if (null == obj)
            {
                obj = MtmsnInstance.GetObject();
                obj.Year = (short)now.Year;
                obj.Month = (byte)now.Month;
                obj.Number = 0;
                MtmsnInstance.Add(obj);
                num = IririumMTMSN.CalculateMTMSN(now, 0);
            }
            else
            {
                num = IririumMTMSN.CalculateMTMSN(now, (ushort)obj.Number);
                MtmsnInstance.Update(f => f.id == obj.id, act =>
                {
                    act.Number = (short)(act.Number + 1);
                });
            }
            return num;
        }
        private void HandleIridiumCommand(TB_Command obj)
        {
            if (null != OnIridiumSend)
            {
                IridiumDataEvent e = new IridiumDataEvent();
                e.Data = new IridiumData()
                {
                    Payload = CustomConvert.GetBytes(obj.Content),
                    IMEI = obj.TB_Terminal.TB_Satellite.CardNo,
                    MTMSN = GetIridiumMTMSN()
                };
                e.Data.Payload[2] = Protocol.ProtocolTypes.SATELLITE;
                OnIridiumSend(this, e);
                // 更新命令发送状态
                CommandInstance.Update(f => f.id == obj.id, act =>
                {
                    act.Status = (byte)CommandStatus.SatelliteHandled;
                });
                if (obj.TB_Terminal.Satellite != (int?)null)
                {
                    HandleIridiumMTFlow(obj.TB_Terminal.Satellite.Value, (int)(obj.Content.Length / 2));
                }
            }
        }

        public EventHandler<IridiumDataEvent> OnIridiumSend;
    }
}
