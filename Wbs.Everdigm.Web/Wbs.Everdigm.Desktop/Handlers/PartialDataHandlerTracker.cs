using System;

using Wbs.Protocol.TX300;
using Wbs.Protocol.TX300.Analyse;
using Wbs.Sockets;
using Wbs.Everdigm.Database;
using Wbs.Everdigm.BLL;

namespace Wbs.Everdigm.Desktop
{
    /// <summary>
    /// 这里是处理TX10G数据的部分
    /// </summary>
    public partial class DataHandler
    {
        /// <summary>
        /// 处理TX10G的数据
        /// </summary>
        /// <param name="tx300"></param>
        /// <param name="data"></param>
        private void HandleTX10G(TX300 tx300, AsyncUserDataBuffer data)
        {
            var bll = new TrackerBLL();
            // 更新或新建Tracker信息
            UpdateTrackerInfo(tx300, data, bll);
            var sim = GetSimFromData(tx300);
            var tracker = bll.Find(f => f.SimCard.Equals(sim));

            switch (tx300.CommandID)
            {
                case 0x7020:
                    // 报警
                    Handle0x7020(tx300, tracker, bll);
                    break;
                case 0x7030:
                    // 位置信息
                    Handle0x7030(tx300, tracker);
                    break;
            }
        }
        /// <summary>
        /// 更新或新建Tracker的基本信息
        /// </summary>
        private void UpdateTrackerInfo(TX300 tx300, AsyncUserDataBuffer data, TrackerBLL bll)
        {
            var sim = GetSimFromData(tx300);
            var tracker = bll.Find(f => f.SimCard.Equals(sim));
            if (null == tracker)
            {
                // 新增一个tracker
                tracker = bll.GetObject();
                tracker.SimCard = sim;
                tracker.LastActionAt = data.ReceiveTime;
                tracker.Socket = data.SocketHandle;
                tracker.State = 1;
                bll.Add(tracker);
            }
            else
            {
                bll.Update(f => f.id == tracker.id, act =>
                {
                    act.LastActionAt = data.ReceiveTime;
                    act.Socket = data.SocketHandle;
                    if (tx300.CommandID == 0x7020 || tx300.CommandID == 0x7030 || tx300.CommandID == 0x7040)
                    {
                        act.CSQ = tx300.MsgContent[0];
                    }
                });
            }
        }
        /// <summary>
        /// 保存Tracker的历史定位记录
        /// </summary>
        /// <param name="tracker"></param>
        private void SaveTrackerPosition(string sim, string car, int tracker, TX10G_Position position, string type, DateTime time)
        {
            var bll = new TrackerPositionBLL();
            var pos = bll.GetObject();
            pos.Type = type;
            pos.CarNumber = car;
            pos.GPSTime = position.GPSTime;
            pos.Latitude = position.Latitude;
            pos.Longitude = position.Longitude;
            pos.ReceiveTime = time;
            pos.SimCard = sim;
            pos.Tracker = 0 > tracker ? (int?)null : tracker;
            bll.Add(pos);
        }
        /// <summary>
        /// 处理报警信息
        /// </summary>
        /// <param name="obj"></param>
        private void Handle0x7020(TX300 obj, TB_Tracker tracker,TrackerBLL bll)
        {
            _0x7020 x7020 = new _0x7020();
            x7020.Content = obj.MsgContent;
            x7020.Unpackage();
            
            var alarm = x7020.AlarmBIN;

            if (null != tracker)
            {
                bll.Update(f => f.id == tracker.id, act =>
                {
                    act.CSQ = x7020.CSQ;
                    if (alarm[0] == '1')
                    {
                        // 终端后备电池耗光报警
                        act.BatteryAlarm = tracker.LastActionAt;
                    }
                    else if (alarm[1] == '1')
                    {
                        // 停车超时报警
                        act.ParkingAlarm = tracker.LastActionAt;
                    }
                    else if (alarm[2] == '1')
                    {
                        // 充电接线断开报警
                        act.ChargingAlarm = tracker.LastActionAt;
                    }
                    else if (alarm[2] == '0')
                    {
                        // 充电接线链接报警
                        act.ChargingAlarm = null;
                    }
                });
            }
            string type = ((TX10GAlarms)x7020.Alarm).ToString().Replace("TX10GAlarms", "");
                //alarm[0] == '1' ? "Battery OFF" :
                //(alarm[1] == '1' ? "Parking Timeout" :
                //(alarm[2] == '1' ? "Charge OFF" : (alarm[2] == '0' ? "Charge ON" : "Unknown")));
            if (x7020.Position.Available)
            {
                SaveTrackerPosition(obj.TerminalID, (null == tracker ? "" : tracker.CarNumber),
                    (null == tracker ? -1 : tracker.id), x7020.Position, type, tracker.LastActionAt.Value);
            }
        }

        private void Handle0x7030(TX300 obj, TB_Tracker tracker)
        {
            _0x7030 x7030 = new _0x7030();
            x7030.Content = obj.MsgContent;
            x7030.Unpackage();
            
            foreach (var pos in x7030.Positions)
            {
                if (pos.Available)
                {
                    SaveTrackerPosition(obj.TerminalID, (null == tracker ? "" : tracker.CarNumber),
                      (null == tracker ? -1 : tracker.id), pos, "Tracking", tracker.LastActionAt.Value);
                }
            }
        }
        private void Handle0x7040(TX300 obj, TB_Tracker tracker)
        {
            //if (null != tracker) { UpdateTrackerCSQ(obj.MsgContent[0], tracker.id); }
        }
    }
}
