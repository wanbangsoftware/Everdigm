using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Wbs.Utilities
{
    /// <summary>
    /// ListView 排序比较接口实现类
    /// </summary>
    public class ListViewItemComparer : IComparer
    {
        private int col;
        /// <summary>
        /// 初始化类并指定默认第一列为比较列
        /// </summary>
        public ListViewItemComparer()
        {
            col = 0;
        }
        /// <summary>
        /// 初始化类并指定需要比较的列，从0开始计数
        /// </summary>
        /// <param name="column"></param>
        public ListViewItemComparer(int column)
        {
            col = column;
        }
        /// <summary>
        /// 比较两个指定对象
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(object x, object y)
        {
            string strA = ((ListViewItem)x).SubItems[col].Text, strB = ((ListViewItem)y).SubItems[col].Text;
            strA = col == 0 ? UInt64.Parse(strA).ToString("000000000000000") : strA;
            strB = col == 0 ? UInt64.Parse(strB).ToString("000000000000000") : strB;
            return String.Compare(strA, strB);
        }
    }
}
