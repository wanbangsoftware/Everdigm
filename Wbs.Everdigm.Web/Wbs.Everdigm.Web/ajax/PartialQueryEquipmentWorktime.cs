using System;
using System.Collections.Generic;
using System.Linq;
using Wbs.Everdigm.Common;
using Wbs.Utilities;
using Wbs.Protocol;

namespace Wbs.Everdigm.Web.ajax
{
    /// <summary>
    /// 查询运转时间的部分
    /// </summary>
    public partial class query
    {
        /// <summary>
        /// 查询指定日期范围内的运转时间并补偿相应的数量
        /// </summary>
        /// <param name="averagable"></param>
        /// <returns></returns>
        private string HandleQueryEquipmentWorktime(bool averagable = true)
        {
            var ret = "{}";
            var id = ParseInt(Utility.Decrypt(data));
            var obj = EquipmentInstance.Find(f => f.id == id && f.Deleted == false);
            if (null != obj)
            {
                var date = DateTime.Parse(GetParamenter("date") + " 00:00:00");
                // 如果不是求平均值则将日期往前推一天
                //if (!averagable) { date = date.AddDays(-1); }
                var date1 = DateTime.Parse(GetParamenter("date1") + " 23:59:59");
                List<WorktimeChart> avg = new List<WorktimeChart>();
                List<WorktimeChart> work = new List<WorktimeChart>();
                DateTime dt = date;
                // 循环每天一个节点
                while (dt.Ticks < date1.Ticks)
                {
                    avg.Add(new WorktimeChart() { date = dt.ToString("yyyy/MM/dd"), x = Utility.DateTimeToJavascriptDate(dt.Date), y = 0, min = 0 });
                    work.Add(new WorktimeChart() { date = dt.ToString("yyyy/MM/dd"), x = Utility.DateTimeToJavascriptDate(dt.Date), y = 0, min = 0 });
                    dt = dt.AddDays(1);
                }
                var macid = EquipmentInstance.GetFullNumber(obj);
                var cmds = new string[] { "0x1000", "0x600B" };
                var runtimes = DataInstance.FindList(f => f.mac_id.Equals(macid) && cmds.Contains(f.command_id) &&
                    f.receive_time >= date && f.receive_time <= date1).OrderBy(o => o.receive_time);
                var list = new List<Worktime>();
                if (null != runtimes)
                {
                    long today = 0;
                    foreach (var r in runtimes)
                    {
                        bool gps = r.command_id.Equals("0x1000");
                        var t = Utility.DateTimeToJavascriptDate(r.receive_time.Value.Date);
                        // 日期不同则重置日期和运转时间
                        if (today != t) { today = t; }

                        byte[] temp = null;
                        int index = 0;
                        if (gps)
                        {
                            if (r.protocol_type == ProtocolTypes.SATELLITE)
                            {
                                temp = CustomConvert.GetBytes(r.message_content);
                                index = 13;
                            }
                        }
                        else
                        {
                            temp = CustomConvert.GetBytes(r.message_content);
                            index = 0;
                        }

                        // 增加一个节点
                        Worktime wt = new Worktime();
                        wt.date = r.receive_time.Value.ToString("yyyy/MM/dd HH:mm:ss");
                        wt.worktime = null == temp ? 0 : BitConverter.ToUInt32(temp, index);
                        var bin = !gps ? "00000000" : CustomConvert.IntToDigit(temp[4], CustomConvert.BIN, 8);
                        // 如果list已经有数据则计算与上一条数据之间的差值
                        var cnt = list.Count;
                        if (cnt > 0)
                        {
                            if (wt.worktime < list[cnt - 1].worktime)
                            {
                                // 当前运转时间小于前一条时，小计为0
                                wt.interval = 0;
                            }
                            else
                            {
                                // 差值
                                wt.interval = !gps ? 0 : (wt.worktime - list[cnt - 1].worktime);
                                // 补偿的分钟
                                if (wt.interval > 0)
                                {
                                    uint ad = 0;
                                    if (wt.interval > 60)
                                    {
                                        ad = wt.interval / 60;
                                    }
                                    else
                                    {
                                        ad = 1;
                                    }
                                    if (bin[6] == '1')
                                    {
                                        ad += 1;
                                    }
                                    wt.added = ad;
                                }
                                else { wt.interval = 0; }
                                // 所用的小时
                                wt.hours = wt.interval > 0 ? (wt.interval > 60 ? wt.interval / 60 : 1) : 0;
                            }
                        }
                        else
                        {
                            // 第一条数据小计为0
                            wt.interval = 0;
                        }
                        list.Add(wt);

                        // 更新本日最后的运转时间
                        var wk = work.FirstOrDefault(f => f.x == today);
                        if (null != wk)
                        {
                            //if (wk.min == 0) { wk.min = run; }
                            wk.min = wt.worktime;
                        }
                    }// end of foreach

                    // 工作时间总计
                    var totalWorkMins = list.Sum(s => s.interval);
                    // 补偿的分钟
                    var totalAddMins = list.Sum(s => s.added);
                    // 所用的小时数
                    var totalUsedHours = list.Sum(s => s.hours);
                    // 工作效率
                    var compensate = totalWorkMins / 60.0 / totalUsedHours;
                    // 最终增加的小时数
                    var finalAdded = totalAddMins / 60.0 * compensate;

                    // 计算每日运转时间
                    foreach (var f in work)
                    {
                        f.y = Math.Round(list.Where(w => w.date.Contains(f.date)).Sum(s => s.interval) / 60.0, 2);
                        f.add = finalAdded;
                    }
                    // 有工作时间的天数里平均加入补偿的小时数
                    var per = finalAdded / work.Count(c => c.y > 0);
                    //per = Math.Round(per, 2);
                    // 加入补偿
                    foreach (var w in work)
                    {
                        w.y += w.y > 0 ? per : 0;
                        if (averagable) { w.y = Math.Round(w.y, 2); }
                    }

                    // 计算平均值
                    var avgg = Math.Round(work.Sum(s => s.y) * 1.0 / work.Count, 2);
                    foreach (var a in avg)
                    { a.y = avgg; }

                }
                if (averagable)
                    ret = string.Format("{0}\"Average\":{1},\"Worktime\":{2}{3}", "{", JsonConverter.ToJson(avg), JsonConverter.ToJson(work), "}");
                else
                    ret = JsonConverter.ToJson(work);
            }
            return ret;
        }

        private class Worktime
        {
            /// <summary>
            /// 日期
            /// </summary>
            public string date;
            public uint worktime = 0;
            public uint interval = 0;
            public uint added = 0;
            public uint hours = 0;
        }
    }
}