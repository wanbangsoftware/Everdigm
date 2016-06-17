using System.Collections.Generic;
using System.Linq;
using System;
using Wbs.Everdigm.BLL;
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
            try
            {
                switch (cmd)
                {
                    case "normal":
                        // 普通查询 2015/11/27 08:17
                        ret = QueryNormal();
                        break;
                    case "bound":
                    case "notbound":
                        // 查询是否绑定设备的终端列表
                        ret = QueryBound();
                        break;
                    case "book":
                        ret = QueryBook();
                        break;
                    case "single":
                        // 查询单个终端
                        ret = QuerySingle();
                        break;
                    case "history":
                        // 查询终端的历史纪录
                        ret = QueryTerminalHistiry();
                        break;
                    case "sim":
                        ret = QueryAutoDefinedSimNumber();
                        break;
                }
            }
            finally { CloseTerminalBlls(); }
            ResponseJson(ret);
        }
        private TerminalBLL TerminalInstance { get { return new TerminalBLL(); } }
        private void CloseTerminalBlls() {
            TerminalInstance.Close();
        }
        /// <summary>
        /// 查询单个终端
        /// </summary>
        /// <returns></returns>
        private string QuerySingle()
        {
            var single = TerminalInstance.FindList(f => f.Number.Equals(data) && f.Delete == false).ToList();
            return JsonConverter.ToJson(single);
        }
        /// <summary>
        /// 查询是否预定安装
        /// </summary>
        /// <returns></returns>
        private string QueryBook()
        {
            var exp = PredicateExtensions.True<TB_Terminal>();
            if (!string.IsNullOrEmpty(data))
            {
                exp = exp.And(a => a.Number.Contains(data) || a.Sim.Contains(data));
            }
            exp = exp.And(a => a.Delete == false && a.HasBound == false && a.Booked == false);
            var book = TerminalInstance.FindList(exp).Take(10).ToList();
            var tmp = new List<TempTerminal>();
            foreach (var t in book)
            {
                tmp.Add(new TempTerminal(t));
            }
            return JsonConverter.ToJson(tmp);
        }
        /// <summary>
        /// 查询绑定状态
        /// </summary>
        /// <returns></returns>
        private string QueryBound()
        {
            var exp = PredicateExtensions.True<TB_Terminal>();
            exp = exp.And(a => a.HasBound == ("bound" == cmd ? true : false));
            if (!string.IsNullOrEmpty(data))
            {
                exp = exp.And(a => a.Number.Contains(data) || a.Sim.Contains(data));
            }
            exp = exp.And(a => a.Delete == false);
            var bound = TerminalInstance.FindList(exp).Take(10).ToList();
            return JsonConverter.ToJson(bound);
        }
        /// <summary>
        /// 普通查询终端
        /// </summary>
        /// <returns></returns>
        private string QueryNormal()
        {
            var exp = PredicateExtensions.True<TB_Terminal>();
            if (!string.IsNullOrEmpty(data))
            {
                exp = exp.And(a => a.Number.Contains(data) || a.Sim.Contains(data));
            }
            exp = exp.And(a => a.Delete == false);
            var normal = TerminalInstance.FindList(exp).Take(10).ToList();
            return JsonConverter.ToJson(normal);
        }
        /// <summary>
        /// 查找自定义的sim卡号码
        /// </summary>
        /// <returns></returns>
        private string QueryAutoDefinedSimNumber()
        {
            var ret = "{\"Sim\":\"89000000\"}";
            var tmp = TerminalInstance.FindList<TB_Terminal>(f => f.Sim.Contains("89000"), "Sim", true).FirstOrDefault();
            if (null != tmp)
            {
                var s = int.Parse(tmp.Sim);
                ret = string.Format("{0}\"Sim\":\"{1}\"{2}", "{", s + 1, "}");
            }
            return ret;
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