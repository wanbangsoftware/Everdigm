﻿using System.Collections.Generic;
using System.Linq;
using System;

using Wbs.Everdigm.Database;
using Wbs.Everdigm.Common;

namespace Wbs.Everdigm.Web.ajax
{
    public class TempTerminal
    {
        public int Id { get; set; }
        public string Sim { get; set; }
        public string Number { get; set; }
        public string Satellite { get; set; }
        public TempTerminal(TB_Terminal obj)
        {
            Id = obj.id;
            Sim = obj.Sim;
            Number = obj.Number;
            Satellite = (int?)null == obj.Satellite ? "" : obj.TB_Satellite.CardNo;
        }
    }
    /// <summary>
    /// 这里提供终端信息查询
    /// </summary>
    public partial class query
    {
        /// <summary>
        /// 处理终端的查询
        /// </summary>
        private void HandleTerminalQuery()
        {
            var ret = "[]";
            switch (cmd)
            {
                case "normal":
                    // 普通查询 2015/11/27 08:17
                    var normal = TerminalInstance.FindList(f => f.Delete == false && 
                    (f.Number.IndexOf(data) >= 0 || f.Sim.IndexOf(data) >= 0)).Take(10).ToList();
                    ret = JsonConverter.ToJson(normal);
                    break;
                case "bound":
                case "notbound":
                    // 查询是否绑定设备的终端列表
                    var bound = TerminalInstance.FindList(f =>
                        f.Delete == false && (f.HasBound == ("bound" == cmd ? true : false)) &&
                        (f.Number.IndexOf(data) >= 0 || f.Sim.IndexOf(data) >= 0)).Take(10).ToList();
                    ret = JsonConverter.ToJson(bound);
                    break;
                case "book":
                    var book = TerminalInstance.FindList(f => f.Delete == false && 
                        f.HasBound == false && f.Booked == false && 
                        (f.Number.IndexOf(data) >= 0 || f.Sim.IndexOf(data) >= 0)).Take(10).ToList();
                    var tmp = new List<TempTerminal>();
                    foreach (var t in book)
                    {
                        tmp.Add(new TempTerminal(t));
                    }
                    ret = JsonConverter.ToJson(tmp);
                    break;
                case "single":
                    // 查询单个终端
                    var single = TerminalInstance.FindList(f => f.Number.Equals(data) && f.Delete == false).ToList();
                    ret = JsonConverter.ToJson(single);
                    break;
                case "history":
                    // 查询终端的历史纪录
                    ret = QueryTerminalHistiry();
                    break;
            }
            ResponseJson(ret);
        }
        /// <summary>
        /// 查询按终端号码的历史纪录
        /// </summary>
        /// <returns></returns>
        private string QueryTerminalHistiry() {
            string ret;
            var time = GetParamenter("time");
            time = string.IsNullOrEmpty(time) ? "0" : time;
            long t = long.Parse(time);
            if (t <= 0)
            {
                ret = "{\"Time\":" + DateTime.Now.Ticks + ",\"Data\":[]}";
            }
            else {
                var dt = new DateTime(t);
                // 查询指定时间之后的通讯记录
                var his = DataInstance.FindList(f => f.terminal_id.Equals(data + "000") && f.receive_time >= dt);
                ret = "{\"Time\":" + time + ",\"Data\":" + JsonConverter.ToJson(his) + "}";
            }
            return ret;
        }
    }
}