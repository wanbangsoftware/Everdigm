namespace Wbs.Everdigm.Common
{
    public enum PrintStatus : byte
    {
        /// <summary>
        /// 没有任何状态
        /// </summary>
        Nothing = 0,
        /// <summary>
        /// 等待标签打印
        /// </summary>
        Waiting = 1,
        /// <summary>
        /// 正在打印
        /// </summary>
        Handling = 2,
        /// <summary>
        /// 已打印完毕
        /// </summary>
        Printed = 3
    }
}
