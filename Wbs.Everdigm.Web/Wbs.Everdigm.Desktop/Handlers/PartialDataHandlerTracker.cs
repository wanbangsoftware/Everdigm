using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wbs.Protocol.TX300;
using Wbs.Protocol.TX300.Analyse;
using Wbs.Sockets;
using Wbs.Everdigm.Database;

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
            var sim = GetSimFromData(tx300);
            var tracker = TrackerInstance.Find(f => f.SimCard.Equals(sim));
            if (null == tracker)
            { 
                // 新增一个tracker
                tracker = TrackerInstance.GetObject();
                tracker.SimCard = sim;
                tracker.LastActionAt = DateTime.Now;
                tracker.Socket = data.SocketHandle;
                tracker.State = 1;
                tracker = TrackerInstance.Add(tracker);
            }
            //HandleTrackerCSQ(tx300.MsgContent[0], sim);
            switch (tx300.CommandID)
            {
                case 0x7000:
                    break;
                case 0x7010:
                    // 通讯参数设置，不需要进行处理
                    break;
                case 0x7020:
                    // 报警
                    Handle0x7020(tx300, tracker);
                    break;
                case 0x7030:
                    // 位置信息
                    Handle0x7030(tx300, tracker);
                    break;
                case 0x7040:
                    // 心跳
                    if (null != tracker) { UpdateTrackerCSQ(tx300.MsgContent[0], tracker.id); }
                    break;
            }
        }
        /// <summary>
        /// 保存Tracker的历史定位记录
        /// </summary>
        /// <param name="tracker"></param>
        private void SaveTrackerPosition(string sim,string car, int tracker, TX10G_Position position, string type)
        {
            var pos = TrackerPosition.GetObject();
            pos.Type = type;
            pos.CarNumber = car;
            pos.GPSTime = position.GPSTime;
            pos.Latitude = position.Latitude;
            pos.Longitude = position.Longitude;
            pos.ReceiveTime = DateTime.Now;
            pos.SimCard = sim;
            pos.Tracker = 0 > tracker ? (int?)null : tracker;
            TrackerPosition.Add(pos);
        }
        /// <summary>
        /// 更新Tracker的信号强度
        /// </summary>
        /// <param name="csq"></param>
        /// <param name="tracker"></param>
        private void UpdateTrackerCSQ(byte csq, int tracker)
        {
            TrackerInstance.Update(f => f.id == tracker, act => { act.CSQ = csq; });
        }
        /// <summary>
        /// 处理报警信息
        /// </summary>
        /// <param name="obj"></param>
        private void Handle0x7020(TX300 obj, TB_Tracker tracker)
        {
            _0x7020 x7020 = new _0x7020();
            x7020.Content = obj.MsgContent;
            x7020.Unpackage();

            UpdateTrackerCSQ(x7020.CSQ, tracker.id);

            var alarm = x7020.AlarmBIN;

            if (null != tracker)
            {
                TrackerInstance.Update(f => f.id == tracker.id, act =>
                {
                    act.CSQ = x7020.CSQ;
                    if (alarm[0] == '1')
                    {
                        // 终端后备电池耗光报警
                        act.BatteryAlarm = DateTime.Now;
                    }
                    else if (alarm[1] == '1')
                    {
                        // 停车超时报警
                        act.ParkingAlarm = DateTime.Now;
                    }
                    else if (alarm[2] == '1')
                    {
                        // 充电接线断开报警
                        act.ChargingAlarm = DateTime.Now;
                    }
                    else if (alarm[2] == '0')
                    {
                        // 充电接线链接报警
                        act.ChargingAlarm = (DateTime?)null;
                    }
                });
            }
            string type = alarm[0] == '1' ? "Battery OFF" :
                (alarm[1] == '1' ? "Parking Timeout" :
                (alarm[2] == '1' ? "Charge OFF" : (alarm[2] == '0' ? "Charge ON" : "Unknown")));
            if (x7020.Position.Available)
            {
                SaveTrackerPosition(obj.TerminalID, (null == tracker ? "" : tracker.CarNumber),
                    (null == tracker ? -1 : tracker.id), x7020.Position, type);
            }
        }

        private void Handle0x7030(TX300 obj, TB_Tracker tracker)
        {
            _0x7030 x7030 = new _0x7030();
            x7030.Content = obj.MsgContent;
            x7030.Unpackage();

            if (null != tracker)
            {
                UpdateTrackerCSQ(x7030.CSQ, tracker.id);
            }

            foreach (var pos in x7030.Positions)
            {
                if (pos.Available)
                {
                    SaveTrackerPosition(obj.TerminalID, (null == tracker ? "" : tracker.CarNumber),
                      (null == tracker ? -1 : tracker.id), pos, "Tracking");
                }
            }
        }
        private void Handle0x7040(TX300 obj, TB_Tracker tracker)
        {
            if (null != tracker)
            {
                UpdateTrackerCSQ(obj.MsgContent[0], tracker.id);
            }
        }
    }
}
