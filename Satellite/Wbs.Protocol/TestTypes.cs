using System;
using System.Collections.Generic;
using System.Text;

namespace Wbs.Protocol
{
    /// <summary>
    /// 生产测试类型。
    /// </summary>
    public class TestTypes
    {
        /// <summary>
        /// 生产提前准备组 Sim 卡测试。
        /// </summary>
        public const string TEST_CARD = "CARD";
        /// <summary>
        /// 生产半成品终端 9F 测试。
        /// </summary>
        public const string TEST_9F = "9F";
        /// <summary>
        /// 生产提前准备组 PCB 版测试。
        /// </summary>
        public const string TEST_PCB = "PCB";
        /// <summary>
        /// 生产提前准备组模块 GPS 功能测试。
        /// </summary>
        public const string TEST_GPS = "GPS";
        /// <summary>
        /// 生产半成品/成品终端通讯测试。
        /// </summary>
        public const string TEST_TERMINAL = "TERMINAL";
    }
}
