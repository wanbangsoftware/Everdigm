﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.BLL
{
    /// <summary>
    /// 设备仓储业务处理
    /// </summary>
    public class EquipmentBLL : BaseService<TB_Equipment>
    {
        public EquipmentBLL()
            : base(new BaseRepository<TB_Equipment>())
        { }
        /// <summary>
        /// 生成一个新的默认实例
        /// </summary>
        /// <returns></returns>
        public override TB_Equipment GetObject()
        {
            return new TB_Equipment()
            {
                Customer = (int?)null,
                GpsAddress = "",
                GpsUpdated = true,
                id = 0,
                IP = "",
                LastAction = "",
                LastActionBy = "",
                LastActionTime = (DateTime?)null,
                Latitude = 0.0,
                StoreTimes = 0,
                LockStatus = "00",
                Longitude = 0.0,
                Model = (int?)null,
                Number = "",
                OnlineStyle = 0,
                OnlineTime = (DateTime?)null,
                Port = 0,
                RegisterTime = DateTime.Now,
                Runtime = 0,
                ServerName = "",
                Signal = 0,
                Socket = 0,
                Status = (int?)null,
                Terminal = (int?)null,
                Voltage = "G0000",
                Warehouse = (int?)null
            };
        }
        /// <summary>
        /// 将实体生成字符串(如：设备号，终端号，Sim号，Sat号)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string ToString(TB_Equipment entity)
        {
            // 判断终端是否为空
            var n = (int?)null;
            var t = n == entity.Terminal;
            return string.Format("{0}, Sim: {1}, Satellite: {2}", (entity.TB_EquipmentModel.Code + entity.Number),
                (t ? "" : entity.TB_Terminal.Sim), 
                (t ? "" : (n == entity.TB_Terminal.Satellite ? "" : entity.TB_Terminal.TB_Satellite.CardNo)));
        }
        /// <summary>
        /// 生成设备的完整号码
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string GetFullNumber(TB_Equipment entity)
        {
            if (null == entity) return "";
            return entity.TB_EquipmentModel.Code + entity.Number;
        }
        /// <summary>
        /// 获取设备的状态信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string GetStatus(TB_Equipment entity)
        {
            return entity.TB_EquipmentStatusCode.TB_EquipmentStatusName.Code + entity.TB_EquipmentStatusCode.Code;
        }
        /// <summary>
        /// 获取设备的状态信息描述
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string GetStatusTitle(TB_Equipment entity)
        {
            return entity.TB_EquipmentStatusCode.Name + "(" + entity.TB_EquipmentStatusCode.TB_EquipmentStatusName.Name + ")";
        }

        /// <summary>
        /// 获取终端信息的title
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string GetTerinalTitleInfo(TB_Equipment obj)
        {
            var n = (int?)null;
            return n == obj.Terminal ? "" :
                (("GSM Card: " + obj.TB_Terminal.Sim) +
                (n == obj.TB_Terminal.Satellite ? "" :
                ("&#10;SAT Card: " + obj.TB_Terminal.TB_Satellite.CardNo)));
        }

        /// <summary>
        /// 获取链接状态
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetOnlineStyle(byte? type)
        {
            var ret = "";
            if ((byte?)null == type)
                ret = "unknown";

            switch (type)
            {
                case 0x00:// OFF
                    ret = "off";
                    break;
                case 0x10:// TCP
                    ret = "tcp";
                    break;
                case 0x20:// UDP
                    ret = "udp";
                    break;
                case 0x30:// SMS
                    ret = "sms";
                    break;
                case 0x40:
                    ret = "sleep";
                    break;
                case 0x50:
                    ret = "blind";
                    break;
                case 0xFF:
                    ret = "trouble";
                    break;
            }
            return ret;
        }
        /// <summary>
        /// 获取发动机的启动状态
        /// </summary>
        /// <param name="voltage"></param>
        /// <returns></returns>
        public string GetEngStatus(string voltage)
        {
            if (null == voltage) return "OFF";
            if (voltage.IndexOf("G2") >= 0) return "ON";
            return "OFF";
        }
        /// <summary>
        /// 获取运转时间
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public string GetRuntime(int? time)
        {
            if ((int?)null == time) return "0";
            return (time / 60) + "h" + (time % 60) + "m";
        }

    }
}