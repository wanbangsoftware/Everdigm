/// <summary>
/// 终端连接状态
/// </summary>
public enum LinkType : byte
{
    /// <summary>
    /// 主电源断之后的OFF 0
    /// </summary>
    OFF = 0x00,
    /// <summary>
    /// TCP链接 16
    /// </summary>
    TCP = 0x10,
    /// <summary>
    /// UDP链接 32
    /// </summary>
    UDP = 0x20,
    /// <summary>
    /// SMS链接 48
    /// </summary>
    SMS = 0x30,
    /// <summary>
    /// 睡眠 64
    /// </summary>
    SLEEP = 0x40,
    /// <summary>
    /// 盲区 80
    /// </summary>
    BLIND = 0x50,
    /// <summary>
    /// 卫星通信 96
    /// </summary>
    SATELLITE = 0x60,
    /// <summary>
    /// 卫星通信暂停状态
    /// </summary>
    SATELLITE_STOP = 0x61,
    /// <summary>
    /// 其他
    /// </summary>
    OTHER = 0xFF,

    /// <summary>
    /// TCP链接状态的掩码
    /// </summary>
    LINK_TCP = 0x01,
    /// <summary>
    /// UDP链接状态的掩码
    /// </summary>
    LINK_UDP = 0x02,
    /// <summary>
    /// SMS链接状态的掩码
    /// </summary>
    LINK_SMS = 0x04,
    /// <summary>
    /// SAT链接状态的掩码
    /// </summary>
    LINK_SAT = 0x08
}