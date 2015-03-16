using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 终端连接状态
/// </summary>
public enum LinkType
{
    /// <summary>
    /// 主电源断之后的OFF
    /// </summary>
    OFF = 0x00,
    /// <summary>
    /// TCP链接
    /// </summary>
    TCP = 0x10,
    /// <summary>
    /// UDP链接
    /// </summary>
    UDP = 0x20,
    /// <summary>
    /// SMS链接
    /// </summary>
    SMS = 0x30,
    /// <summary>
    /// 睡眠
    /// </summary>
    SLEEP = 0x40,
    /// <summary>
    /// 盲区
    /// </summary>
    BLIND = 0x50,
    /// <summary>
    /// 卫星通信
    /// </summary>
    SATELLITE = 0x60,
    /// <summary>
    /// 其他
    /// </summary>
    OTHER = 0xFF
}