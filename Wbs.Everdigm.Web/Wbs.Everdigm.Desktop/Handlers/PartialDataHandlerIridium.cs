using System;
using System.Linq;
using Wbs.Everdigm.Database;
using Wbs.Utilities;
using Wbs.Everdigm.BLL;
using Wbs.Protocol;
using Wbs.Protocol.TX300;

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
            using (var bll = new CommandBLL())
            {
                bll.Update(f => f.TB_Terminal.TB_Satellite.CardNo.Equals(data.IMEI) && f.Status == (byte)CommandStatus.SatelliteHandled && f.ActualSendTime == null, action =>
                {
                    // 更新等待铱星处理的终端的命令为已发送状态
                    action.Status = (byte)CommandStatus.SentBySAT;
                    action.IridiumMTMSN = data.MTMSN;
                    action.ActualSendTime = DateTime.Now;
                });
            }
        }
        /// <summary>
        /// 处理1小时内已被卫星模块接收了的命令为正常返回状态
        /// </summary>
        private void HandleIridiumCommandResponseed(IridiumData data)
        {
            // 更新之前发送到网关或终端已读取的命令为发送成功  2015/11/27 12:38
            using (var bll = new CommandBLL())
            {
                bll.Update(f => f.TB_Terminal.TB_Satellite.CardNo.Equals(data.IMEI) && 
                    f.Command == "0x1000" && 
                    (f.Status == (byte)CommandStatus.SentToDestBySAT || f.Status == (byte)CommandStatus.SentBySAT) && 
                    f.ActualSendTime > DateTime.Now.AddMinutes(-60), act =>
                {
                    act.Status = (byte)CommandStatus.Returned;
                });
            }
        }
        /// <summary>
        /// 处理铱星终端接收命令的状态
        /// </summary>
        /// <param name="data"></param>
        private void HandleIridiumMTModelReceiveStatus(IridiumData data)
        {
            using (var bll = new CommandBLL())
            {
                bll.Update(f => f.TB_Terminal.TB_Satellite.CardNo.Equals(data.IMEI) && f.IridiumMTMSN == data.MTMSN && f.Status == (byte)CommandStatus.SentBySAT, act =>
                {
                    act.Status = (byte)CommandStatus.SentToDestBySAT;
                });
            }
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
                uint thisWorkTime = BitConverter.ToUInt32(data.Payload, 13);
                string locks = CustomConvert.GetHex(data.Payload[1]);
                string alarms = CustomConvert.IntToDigit(data.Payload[2], CustomConvert.BIN, 8) +
                    CustomConvert.IntToDigit(data.Payload[3], CustomConvert.BIN, 8);
                IridiumLocation location = new IridiumLocation();
                location.LatLng = new byte[IridiumLocation.SIZE];
                Buffer.BlockCopy(data.Payload, 4, location.LatLng, 0, IridiumLocation.SIZE);
                location.Unpackate();

                // 通过卫星的IMEI号码查找终端
                using (var tbll = new TerminalBLL())
                {
                    using (var ebll = new EquipmentBLL())
                    {
                        var terminal = tbll.Find(f => f.TB_Satellite.CardNo.Equals(data.IMEI));
                        TB_Equipment equipment = null;
                        if (null != terminal)
                        {
                            // 只有第一版的终端才需要计算补偿时间
                            var compensated = terminal.Version == 1;

                            tbll.Update(f => f.id == terminal.id, act =>
                            {
                                act.OnlineStyle = (byte)LinkType.SATELLITE;
                                // 同时更新终端的最后链接时间
                                act.OnlineTime = data.Time;
                            });
                            equipment = ebll.Find(f => f.Terminal == terminal.id);
                            if (null != equipment)
                            {
                                // 旧的运转时间
                                var oldRuntime = equipment.Runtime;
                                var interval = 0 == oldRuntime ? 0 : (thisWorkTime - oldRuntime);
                                // 粗略增加的小时数，启动时加2小时
                                var addMin = interval > 60 ? interval / 60 : 1 + (location.EngFlag.Equals("On") ? 1 : 0);
                                int addHour = (int)(interval > 60 ? interval / 60 : 1);// 显示历史记录
                                string oldCompensated = format("WorkHours {0}, UsedHours {1}, Efficiency {2}, AddHours {3}, Compensated {4}", equipment.WorkHours, equipment.UsedHours, equipment.HourWorkEfficiency, equipment.AddedHours, equipment.CompensatedHours);
                                string newCompensated = "";
                                // 更新设备的总运转时间
                                HandleEquipmentRuntime(equipment, thisWorkTime);
                                if (null != equipment)
                                {
                                    string calculated = "";
                                    ebll.Update(f => f.id == equipment.id, act =>
                                    {
                                        act.OnlineStyle = (byte)LinkType.SATELLITE;
                                        act.OnlineTime = data.Time;
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
                                        act.Voltage = location.EngFlag.Equals("On") ? "G2400" : "G0000";

                                        // 更新总运转时间
                                        act.Runtime = equipment.Runtime;
                                        act.AccumulativeRuntime = equipment.AccumulativeRuntime;
                                        // 更新 version 终端为1的版本的运转时间的补偿
                                        if (compensated && interval > 0)
                                        {
                                            // 实际工作小时数
                                            act.WorkHours += interval / 60.0;
                                            // 实际所用小时数
                                            act.UsedHours += addHour;
                                            // 重新计算每小时工作效率
                                            act.HourWorkEfficiency = act.WorkHours / act.UsedHours;
                                            // 补偿的小时数
                                            act.AddedHours += addMin / 60.0;
                                            // 实际补偿的小时数
                                            act.CompensatedHours = act.AddedHours * act.HourWorkEfficiency;

                                        }
                                        // 如果回来的运转时间比当前时间大则更新成为On状态  暂时  2015/09/02
                                        //if (worktime > act.Runtime)
                                        //{
                                        //    // act.Voltage = "G2400";
                                        //    act.Runtime = (int)worktime;
                                        //}
                                        //else
                                        //{
                                        //    if (worktime > 0)
                                        //    {
                                        //        // 运转时间不为零的话，更新运转时间
                                        //        act.Runtime = (int)worktime;
                                        //    }
                                        //}
                                        // 锁车状态 2015/08/14
                                        if (act.LockStatus != locks)
                                        {
                                            act.LockStatus = locks;
                                        }
                                        // 判断锁车的有效状态
                                        if (locks.Equals("40") || locks.Equals("0F") || locks.Equals("FF"))
                                        {
                                            // 锁车时还有开机状态
                                            if (location.EngFlag.Equals("On"))
                                            {
                                                // 锁车了，但未关机，还在工作中
                                                if (act.LockEffected == (byte)LockEffect.Locked)
                                                {
                                                    act.LockEffected = (byte)LockEffect.LockedAndStillWork;
                                                }
                                                else if (act.LockEffected == (byte)LockEffect.LockedAndEngineOff)
                                                {
                                                    // 锁车了且已关机了，此时再开机则需要报警(没锁住车)
                                                    act.LockEffected = (byte)LockEffect.LockedAndNoEffect;
                                                }
                                            }
                                            else
                                            {
                                                // 锁车了，且已关机了
                                                if (act.LockEffected == (byte)LockEffect.Locked)
                                                { act.LockEffected = (byte)LockEffect.LockedAndEngineOff; }
                                            }
                                        }
                                    });
                                    if (compensated && interval > 0)
                                    {
                                        equipment = ebll.Find(f => f.id == equipment.id);
                                        newCompensated = format("WorkHours {0}, UsedHours {1}, Efficiency {2}, AddHours {3}, Compensated {4}", equipment.WorkHours, equipment.UsedHours, equipment.HourWorkEfficiency, equipment.AddedHours, equipment.CompensatedHours);

                                        calculated = format("Compensate changed(interval: {0}){1} from {2}{3} to {4}", interval, Environment.NewLine, oldCompensated, Environment.NewLine, newCompensated);
                                    }
                                    if (!string.IsNullOrEmpty(calculated))
                                    {
                                        OnUnhandledMessage(this, new Sockets.UIEventArgs()
                                        {
                                            Message = format("{0}{1}{2}", Now, calculated, Environment.NewLine)
                                        });
                                    }
                                }
                            }
                            else
                            {
                                OnUnhandledMessage(this, new Sockets.UIEventArgs() { Message = format("{0} Iridium has not bind with any equipment.", Now, Environment.NewLine) });
                            }
                        }
                        else
                        {
                            OnUnhandledMessage(this, new Sockets.UIEventArgs()
                            {
                                Message = format("{0} Satellite has no terminal, data will save as terminal number: \"{1}\".{2}",
                                Now, data.IMEI.Substring(4), Environment.NewLine)
                            });
                        }
                        // 保存TX300历史记录
                        SaveTX300History(new TX300()
                        {
                            CommandID = 0x1000,
                            MsgContent = data.Payload,
                            ProtocolType = ProtocolTypes.SATELLITE,
                            // 默认装载机终端类型 2015/09/22 09:40
                            TerminalType = null == terminal ? TerminalTypes.LD : terminal.Type.Value,
                            // 没有终端时，用IMEI号码的末尾11位数字表示终端号码 2015/09/22 09:40
                            TerminalID = null == terminal ? data.IMEI.Substring(4) : terminal.Sim,
                            TotalLength = (ushort)data.Payload.Length
                        }, data.Time, ebll.GetFullNumber(equipment));

                        try
                        {
                            long? posId = null;
                            if (location.Available)
                            {
                                using (var posbll = new PositionBLL())
                                {
                                    var pos = posbll.GetObject();
                                    pos.Latitude = location.Latitude;
                                    pos.Longitude = location.Longitude;
                                    pos.NS = location.NSI;
                                    pos.EW = location.EWI;
                                    pos.StoreTimes = null == equipment ? 0 : equipment.StoreTimes;
                                    pos.GpsTime = data.Time;
                                    pos.Equipment = null == equipment ? (int?)null : equipment.id;
                                    // 没有终端时，用IMEI号码的末尾11位数字表示终端号码 2015/09/22 09:40
                                    pos.Terminal = null == terminal ? data.IMEI.Substring(4) : (terminal.Sim.Length < 11 ? (terminal.Sim + "000") : terminal.Sim);
                                    pos.Type = location.Report + "(Eng " + location.EngFlag + ")(SAT)";
                                    posbll.Add(pos);
                                    posId = pos.id;
                                }
                            }

                            // 处理报警信息
                            if (alarms != ALARM)
                            {
                                using (var armbll = new AlarmBLL())
                                {
                                    var arm = armbll.GetObject();
                                    arm.Code = alarms;
                                    arm.AlarmTime = data.Time;
                                    arm.Equipment = null == equipment ? (int?)null : equipment.id;
                                    arm.Position = posId;
                                    arm.StoreTimes = null == equipment ? 0 : equipment.StoreTimes;
                                    // 没有终端时，用IMEI号码的末尾11位数字表示终端号码 2015/09/22 09:40
                                    arm.Terminal = null == terminal ? data.IMEI.Substring(4) : (terminal.Sim.Length < 11 ? (terminal.Sim + "000") : terminal.Sim);
                                    armbll.Add(arm);
                                }
                            }

                        }
                        catch (Exception e)
                        {
                            OnUnhandledMessage(this, new Sockets.UIEventArgs()
                            {
                                Message = format("{0}{1}{2}{3}", Now, e.Message, Environment.NewLine, e.StackTrace)
                            });
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
            }
        }
        /// <summary>
        /// 处理铱星模块发送回来的MO信息
        /// </summary>
        /// <param name="data"></param>
        private void HandleIridiumModelMOPayload(IridiumData data)
        {
            // 如果是正式的协议则以正常的方式处理
            if (data.Length >= TX300Items.header_length)
            {
                // TX300通讯协议首字节必定大于等于17
                // 卫星通讯协议首字节必定等于01
                if (data.Payload[0] >= 17 &&
                    ProtocolTypes.IsTX300(data.Payload[2]) &&
                            TerminalTypes.IsTX300(data.Payload[3]))
                {
                    // 根据卫星号码查询终端的Sim卡号码并将其填入包头结构里
                    using (var bll = new TerminalBLL())
                    {
                        var terminal = bll.Find(f => f.TB_Satellite.CardNo.Equals(data.IMEI) && f.Delete == false);
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
                }
                else
                {
                    HandleIridiumNewProtocolPackage(data);
                }
                // 统计铱星的接收数据次数和数据长度
                HandleIridiumMOFlow(data);
            }
            data.Dispose();
        }
        /// <summary>
        /// 记录铱星的MO次数和长度
        /// </summary>
        /// <param name="data"></param>
        private void HandleIridiumMOFlow(IridiumData data)
        {
            using (var bll = new SatelliteBLL())
            {
                var iridium = bll.Find(f => f.CardNo.Equals(data.IMEI));
                if (null != iridium)
                {
                    var monthly = int.Parse(DateTime.Now.ToString("yyyyMM"));
                    using (var fbll = new IridiumFlowBLL())
                    {
                        var flow = fbll.Find(f => f.Iridium == iridium.id && f.Monthly == monthly);
                        if (null == flow)
                        {
                            flow = fbll.GetObject();
                            flow.Iridium = iridium.id;
                            flow.MOTimes = 1;
                            flow.MOPayload = data.Length;
                            fbll.Add(flow);
                        }
                        else
                        {
                            fbll.Update(f => f.id == flow.id, act =>
                            {
                                act.MOTimes += 1;
                                act.MOPayload += data.Length;
                            });
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 记录铱星的MT次数和长度
        /// </summary>
        private void HandleIridiumMTFlow(int iridium, int length)
        {
            var monthly = int.Parse(DateTime.Now.ToString("yyyyMM"));
            using (var bll = new IridiumFlowBLL())
            {
                var flow = bll.Find(f => f.Iridium == iridium && f.Monthly == monthly);
                if (null == flow)
                {
                    flow = bll.GetObject();
                    flow.Iridium = iridium;
                    flow.MTTimes = 1;
                    flow.MTPayload = length;
                    bll.Add(flow);
                }
                else
                {
                    bll.Update(f => f.id == flow.id, act =>
                    {
                        act.MTTimes += 1;
                        act.MTPayload += length;
                    });
                }
            }
        }
        /// <summary>
        /// 检测需要用铱星方式发送的命令
        /// </summary>
        public void CheckIridiumCommand()
        {
            if (null == _server) return;

            using (var bll = new CommandBLL())
            {
                var list = bll.FindList(f => f.ScheduleTime >= DateTime.Now.AddSeconds(-30) &&
                    f.Status == (byte)CommandStatus.WaitingForSatellite && f.ActualSendTime == (DateTime?)null).ToList();
                if (null != list && list.Count > 0)
                {
                    HandleIridiumCommand(list.First(), bll);
                }
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
            using (var bll = new IridiumMMSNBLL())
            {
                var obj = bll.Find(f => f.Year == (short)now.Year && f.Month == (byte)now.Month);
                if (null == obj)
                {
                    obj = bll.GetObject();
                    obj.Year = (short)now.Year;
                    obj.Month = (byte)now.Month;
                    obj.Number = 0;
                    bll.Add(obj);
                    num = IririumMTMSN.CalculateMTMSN(now, 0);
                }
                else
                {
                    num = IririumMTMSN.CalculateMTMSN(now, (ushort)obj.Number);
                    bll.Update(f => f.id == obj.id, act =>
                    {
                        act.Number = (short)(act.Number + 1);
                    });
                }
            }
            return num;
        }
        private void HandleIridiumCommand(TB_Command obj, CommandBLL bll)
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
                e.Data.Payload[2] = ProtocolTypes.SATELLITE;
                OnIridiumSend(this, e);
                // 更新命令发送状态
                bll.Update(f => f.id == obj.id, act =>
                {
                    act.Status = (byte)CommandStatus.SatelliteHandled;
                });
                if (obj.TB_Terminal.Satellite != (int?)null)
                {
                    HandleIridiumMTFlow(obj.TB_Terminal.Satellite.Value, obj.Content.Length / 2);
                }
            }
        }

        public EventHandler<IridiumDataEvent> OnIridiumSend;
    }
}
