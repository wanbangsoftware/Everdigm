using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 终端发送命令的状态信息
/// </summary>
public enum CommandStatus
{
    /// <summary>
    /// 正在队列中等待发送
    /// </summary>
    Waiting = 0,
    /// <summary>
    /// 正在等待重新发送
    /// </summary>
    ReSending = 1,
    /// <summary>
    /// 已由TCP方式发送
    /// </summary>
    SentByTCP = 2,
    /// <summary>
    /// 已由SMS方式发送
    /// </summary>
    SentBySMS = 3,
    /// <summary>
    /// 已发送到终端
    /// </summary>
    SentToDest = 4,
    /// <summary>
    /// 发送失败，一般发生在SMS下发信息时SMSC找不到终端的情况
    /// </summary>
    SentFail = 5,
    /// <summary>
    /// 命令已返回
    /// </summary>
    Responsed = 6,
    /// <summary>
    /// 命令超时未返回
    /// </summary>
    Timedout = 7,
    /// <summary>
    /// EPOS返回错误
    /// </summary>
    EposFail = 8,
    /// <summary>
    /// 已有保安状态下禁止发送的保安命令，如在本社保安命令下再发送代理商保安命令
    /// </summary>
    SecurityError = 9
}