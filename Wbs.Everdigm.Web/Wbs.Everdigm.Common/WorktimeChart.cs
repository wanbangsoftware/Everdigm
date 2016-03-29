using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wbs.Everdigm.Common
{
    /// <summary>
    /// 提供给web画chat图形的类
    /// </summary>
    public class WorktimeChart : IEquatable<WorktimeChart>
    {
        /// <summary>
        /// Javascript格式的日期
        /// </summary>
        public long x;
        /// <summary>
        /// 运转时间值，hh.mm格式
        /// </summary>
        public double y;
        /// <summary>
        /// 当日最初运转时间
        /// </summary>
        public uint min;

        public override bool Equals(object obj)
        {
            if (null == obj) return false;
            WorktimeChart wc = obj as WorktimeChart;
            if (null == wc) return false;
            return Equals(wc);
        }

        public bool Equals(WorktimeChart other)
        {
            if (null == other) return false;
            return other.x == this.x;
        }

        public override int GetHashCode()
        {
            return (int)this.x;
        }
    }
}
