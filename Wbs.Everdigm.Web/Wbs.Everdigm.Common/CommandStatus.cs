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
    /// 正在队列中等待发送0
    /// </summary>
    Waiting = 0,
    /// <summary>
    /// 正在等待重新发送1
    /// </summary>
    ReSending,
    /// <summary>
    /// 已由TCP方式发送2
    /// </summary>
    SentByTCP,
    /// <summary>
    /// 已由SMS方式发送3
    /// </summary>
    SentBySMS,
    /// <summary>
    /// 已由卫星方式发送4
    /// </summary>
    SentBySAT,
    /// <summary>
    /// 已发送到终端5
    /// </summary>
    SentToDest,
    /// <summary>
    /// 发送失败6，一般发生在SMS下发信息时SMSC找不到终端的情况
    /// </summary>
    SentFail,
    /// <summary>
    /// 命令已返回7
    /// </summary>
    Returned,
    /// <summary>
    /// 命令超时未返回8
    /// </summary>
    Timedout,
    /// <summary>
    /// EPOS返回错误9
    /// </summary>
    EposFail,
    /// <summary>
    /// 已有保安状态下禁止发送的保安命令，如在本社保安命令下再发送代理商保安命令
    /// </summary>
    SecurityError
}