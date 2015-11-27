using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 终端发送命令的状态信息
/// </summary>
public enum CommandStatus : byte
{
    /// <summary>
    /// 正在队列中等待发送
    /// </summary>
    Waiting = 0,
    /// <summary>
    /// 正在等待重新发送
    /// </summary>
    WaitingForSMS,
    /// <summary>
    /// 正在等待卫星方式发送
    /// </summary>
    WaitingForSatellite,
    /// <summary>
    /// 卫星发送已处理
    /// </summary>
    SatelliteHandled,
    /// <summary>
    /// 已由TCP方式发送
    /// </summary>
    SentByTCP,
    /// <summary>
    /// 已由SMS方式发送
    /// </summary>
    SentBySMS,
    /// <summary>
    /// 已由卫星方式发送(发送到铱星网关)6
    /// </summary>
    SentBySAT,
    /// <summary>
    /// 已发送到终端
    /// </summary>
    SentToDest,
    /// <summary>
    /// 已通过卫星发送到终端
    /// </summary>
    SentToDestBySAT,
    /// <summary>
    /// 发送失败9（一般发生在SMS下发信息时SMSC找不到终端的情况）
    /// </summary>
    SentFail,
    /// <summary>
    /// 命令已返回10
    /// </summary>
    Returned,
    /// <summary>
    /// 命令超时未返回11
    /// </summary>
    Timedout,
    /// <summary>
    /// EPOS返回错误12
    /// </summary>
    EposFail,
    /// <summary>
    /// 已有保安状态下禁止发送的保安命令，如在本社保安命令下再发送代理商保安命令
    /// </summary>
    SecurityError,
    /// <summary>
    /// 发送失败：终端的TCP链接已丢失
    /// </summary>
    LinkLosed,
    /// <summary>
    /// TCP发送数据时网络处理错误
    /// </summary>
    TCPNetworkError,
    /// <summary>
    /// 终端没有处理命令的方法
    /// </summary>
    NoFunction,
    /// <summary>
    /// Eng未启动
    /// </summary>
    EngNotStart,
    /// <summary>
    /// 命令不需要回复数据
    /// </summary>
    NotNeedReturn
}