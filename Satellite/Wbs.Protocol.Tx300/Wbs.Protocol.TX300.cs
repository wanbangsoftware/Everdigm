using System;
using System.Runtime.InteropServices;
using Wbs.Protocol;
using Wbs.Protocol.Items;
using Wbs.Protocol.WbsDateTime;

namespace Wbs.Protocol.TX300
{
    /// <summary>
    /// TX300 协议中一些约定的常量。
    /// </summary>
    public class TX300Items
    {
        /// <summary>
        /// 当前能用的最新协议版本号码。
        /// </summary>
        public static byte protocol_version = 0x10;
        /// <summary>
        /// terminal_id 字段长度，固定值。
        /// </summary>
        public static int terminal_id_length = 6;
        /// <summary>
        /// TX300 协议数据包头长度。
        /// </summary>
        public static int header_length = 17;
        /// <summary>
        /// TX300 协议反馈包的长度。
        /// </summary>
        public static int response_length = 14;
        /// <summary>
        /// 保安命令（DX系列）中时间参数的长度。
        /// </summary>
        public static int security_time_stamp_length = 5;
        /// <summary>
        /// 服务器地址所占长度。
        /// </summary>
        public static int server_address_length = 4;
        /// <summary>
        /// SMS 服务器地址所占长度。
        /// </summary>
        public static int sms_server_length = 10;
        /// <summary>
        /// 字符串形式的 SMS 服务器地址长度。
        /// </summary>
        public static int sms_server_len = 12;
    }

    
    /// <summary>
    /// TX300 协议中 DX 状态报告的滤清器信息
    /// </summary>
    public class StatusFilters
    {
        /// <summary>
        /// 获取 DX 系列状态报告中的滤清器信息
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string GetStatusDX(byte b)
        {
            string bin = ProtocolItems.IntToDigit(b, 2, 8);
            string str = "";
            str += bin[0] == '1' ? "燃油滤清器：报警；" : "";
            str += bin[1] == '1' ? "空气滤清器：报警；" : "";
            str += bin[2] == '1' ? "发动机油滤清器：报警；" : "";
            str += bin[3] == '1' ? "回油滤清器：报警；" : "";
            str += bin[4] == '1' ? "先导滤清器：报警；" : "";
            str += bin[5] == '1' ? "发动机油：报警；" : "";
            str += bin[6] == '1' ? "液压油：报警；" : "";
            str += bin[7] == '1' ? "冷却水：报警；" : "";
            if ("" != str)
                str = str.Substring(0, str.Length - 1);
            return str;
        }
    }

    
    /// <summary>
    /// TX10G 的报警类型
    /// </summary>
    public enum TX10GAlarms : byte
    {
        /// <summary>
        /// 没有报警或报警解除。
        /// </summary>
        NoAlarm = 0x00,
        /// <summary>
        /// 充电电源断报警。
        /// </summary>
        ChargingOff = 0x20,
        /// <summary>
        /// 停车超时报警。
        /// </summary>
        StopTimeout = 0x40,
        /// <summary>
        /// 电池电量低报警。
        /// </summary>
        BatteryLow = 0x80
    }
    /// <summary>
    /// TX10G 的报警类型
    /// </summary>
    public class tx10g_alarms
    {
        /// <summary>
        /// 获取 TX10G 终端的报警信息
        /// </summary>
        /// <param name="b">报警内容。</param>
        /// <returns>返回报警描述信息。</returns>
        public static string GetAlarms(byte b)
        {
            string ret = "";
            if ((b & (byte)TX10GAlarms.BatteryLow) == (byte)TX10GAlarms.BatteryLow)
                ret += "电池电量低、";
            if ((b & (byte)TX10GAlarms.ChargingOff) == (byte)TX10GAlarms.ChargingOff)
                ret += "充电电源断、";
            if ((b & (byte)TX10GAlarms.StopTimeout) == (byte)TX10GAlarms.StopTimeout)
                ret += "停车超时、";
            if (ret != "")
                ret = ret.Substring(0, ret.Length - 1);
            return ret;
        }
    }
    

    /// <summary>
    /// TX300 协议中保安命令的类型。
    /// </summary>
    public class SecuritySignals
    {
        /// <summary>
        /// 启用/解除保安命令。
        /// </summary>
        public const byte ENABLE = 0x00;
        /// <summary>
        /// 禁用保安命令功能。
        /// </summary>
        public const byte DISABLE = 0x10;
        /// <summary>
        /// 代理商的保安命令。
        /// </summary>
        public const byte AGENT = 0x20;
        /// <summary>
        /// 本社（斗山）的保安命令。
        /// </summary>
        public const byte DOOSAN = 0x40;
        /// <summary>
        /// 获取字符串描述的保安命令类型。
        /// </summary>
        /// <param name="b">二进制表示的保安命令代码。</param>
        /// <returns>字符串描述的保安命令功能。</returns>
        public static string GetSecuritySignal(byte b)
        {
            string s = "N/A";
            int index = DePow(b / 0x10 * 2);
            int ubd = GetSecuritySignal().GetUpperBound(0);
            if (index > ubd)
                return s;
            else
                return GetSecuritySignal()[index];
        }
        /// <summary>
        /// 获取各种保安命令。
        /// </summary>
        /// <returns>返回各种保安命令码描述。</returns>
        public static string[] GetSecuritySignal()
        {
            string[] s = new string[] { "启用/解除", "禁用保安命令", "代理商命令", "本社命令" };
            return s;
        }
        /// <summary>
        /// 通过指定幂值获取底为 2 的指数。
        /// </summary>
        /// <param name="i">指定的幂值。</param>
        /// <returns>返回 2 的指数。</returns>
        private static int DePow(int i)
        {
            int ret = 0;
            while (Math.Truncate(Math.Pow(2, ret)) < i)
            {
                ret++;
            }
            return ret;
        }
    }

    /// <summary>
    /// EPOS 报警信息
    /// </summary>
    public class AlarmsStatus
    {
        /// <summary>
        /// 获取报警信息的状态。
        /// </summary>
        /// <param name="b">报警信息。</param>
        /// <returns>返回字符串形式的报警描述。</returns>
        public static string GetAlarmsStatus(byte b)
        {
            string s = "";
            switch (b)
            {
                case 0x00:
                case 0x30: s = "安全"; break;
                case 0x01:
                case 0x31: s = "危险"; break;
                default: s = "N/A"; break;
            }
            return s;
        }
        /// <summary>
        /// 获取终端的报警信息值。
        /// </summary>
        /// <param name="b">报警信息数据。</param>
        /// <returns>返回报警信息。</returns>
        public static string GetAlarmsValue(byte[] b)
        {
            string bin = ProtocolItems.IntToDigit(b[0], 2, 8) + ProtocolItems.IntToDigit(b[1], 2, 8);
            //int alarm = b;
            string ss = "";
            ss += bin[0] == '1' ? "挖掘机主电断、" : "";
            ss += bin[4] == '1' ? "GSM 盲区、" : "";
            ss += bin[5] == '1' ? "终端外壳打开、" : "";
            ss += bin[6] == '1' ? "GSM 天线短路、" : "";
            ss += bin[7] == '1' ? "GSM 天线接地、" : "";
            ss += bin[8] == '1' ? "GSM 天线断、" : "";
            ss += bin[9] == '1' ? "EPOS 通讯异常、" : "";
            ss += bin[10] == '1' ? "Sim 卡拔出、" : "";
            ss += bin[11] == '1' ? "越界、" : "";
            ss += bin[12] == '1' ? "卫星天线断、" : "";
            ss += bin[13] == '1' ? "超速、" : "";
            ss += bin[14] == '1' ? "GPS 天线断、" : "";
            ss += bin[15] == '1' ? "终端外接电源断、" : "";
            if (ss.Length > 0)
                ss = ss.Substring(0, ss.Length - 1);
            else
                ss = "无报警";
            return ss;
        }
    }
    /// <summary>
    /// EPOS 命令执行状态。
    /// </summary>
    public class EposStatus
    {
        /// <summary>
        /// 获取 EPOS 命令执行状态值。
        /// </summary>
        /// <param name="b">EPOS 命令的执行状态码。</param>
        /// <returns>返回 EPOS 执行状态值。</returns>
        public static string GetEposStatus(byte b)
        {
            string ss = "";
            switch (b)
            {
                case 0x00: ss = "EPOS 处理失败"; break;
                case 0x01: ss = "EPOS 处理成功"; break;
                default: ss = "N/A"; break;
            }
            return ss;
        }
    }
    
    /// <summary>
    /// 0xEE00 中的错误类型。
    /// </summary>
    public class ErrorTypes
    {
        /// <summary>
        /// F/W 更新导致终端内没有处理此命令的功能。
        /// </summary>
        public static byte f_w_upgrade = 0x10;
        /// <summary>
        /// EPOS 没有正常返回数据。
        /// </summary>
        public static byte epos_not_resp = 0x20;
        /// <summary>
        /// EPOS 没有启动时发送的 EPOS 命令。
        /// </summary>
        public static byte epos_not_start = 0x30;
        /// <summary>
        /// 通过 0xEE00 命令中的错误码获取错误类型。
        /// </summary>
        /// <param name="b">错误码。</param>
        /// <returns>返回错误类型。</returns>
        public static string GetErrorTypes(byte b)
        {
            string ss = "";
            switch (b)
            {
                case 0x10: ss = "F/W 版本过旧"; break;
                case 0x20: ss = "EPOS 无返回"; break;
                case 0x30: ss = "车没启动的 EPOS 命令"; break;
                default: ss = "N/A"; break;
            }
            return ss;
        }
    }
    /// <summary>
    /// 远程控制终端的类型：0x3000。
    /// </summary>
    public class RemoteControls
    {
        /// <summary>
        /// 远程开机。
        /// </summary>
        public const byte remote_on = 0xF0;
        /// <summary>
        /// 远程关机。
        /// </summary>
        public const byte remote_off = 0x0F;
        /// <summary>
        /// 获取远程控制命令。
        /// </summary>
        /// <param name="b">远程控制代码。</param>
        /// <returns>返回远程控制命令。</returns>
        public static string GetRemoteControls(byte b)
        {
            string s = "N/A";
            if (b == remote_off)
                s = "远程控制：关机";
            else if (b == remote_on)
                s = "远程控制：开机";
            return s;
        }/*
        /// <summary>
        /// 获取远程控制命令代码。
        /// </summary>
        /// <param name="s">远程控制命令。</param>
        /// <returns>返回远程控制命令代码。</returns>
        public static byte GetRemoteControls(string s)
        {
            byte ret = 0x00;
            if (s.CompareTo(GetRemoteControls()[0]) == 0)
                ret = remote_on;
            else if (s.CompareTo(GetRemoteControls()[1]) == 0)
                ret = remote_off;
            return ret;
        }*/
        /// <summary>
        /// 获取各个远程控制命令。
        /// </summary>
        /// <returns>返回所有的远程控制命令。</returns>
        public static string[] GetRemoteControls()
        {
            string[] s = new string[] { "远程控制：开机", "远程控制：关机" };
            return s;
        }
    }
    /// <summary>
    /// 远程重启终端的类型。
    /// </summary>
    public class ResetTypes
    {
        /// <summary>
        /// 硬重启终端并以默认（UDP）方式连接服务器。
        /// </summary>
        public const byte hw_reset = 0x00;
        /// <summary>
        /// 重启终端到 UDP 通讯模式。
        /// </summary>
        public const byte reset_to_udp = 0x10;
        /// <summary>
        /// 重启终端到 SMS 通讯模式。
        /// </summary>
        public const byte reset_to_sms = 0x20;
        /// <summary>
        /// 重启终端到卫星通讯模式。
        /// </summary>
        public const byte reset_to_satellite = 0x30;
        /// <summary>
        /// 重启终端到 TCP 通讯模式。
        /// </summary>
        public const byte reset_to_tcp = 0x40;
        /// <summary>
        /// 获取远程重启方式。
        /// </summary>
        /// <param name="b">远程重启类型。</param>
        /// <returns>返回远程重启类型的描述。</returns>
        public static string GetResetTypes(byte b)
        {
            string ss = "N/A";
            int index = b / 0x10;
            if (index > 4)
                return ss;
            else
                return GetResetTypes()[index];
        }
        /// <summary>
        /// 获取各个远程重置命令。
        /// </summary>
        /// <returns>返回所有的远程重置类型。</returns>
        public static string[] GetResetTypes()
        {
            string[] s = new string[] { "按默认配置重置", "重置为 UDP 方式", "重置为 SMS 方式", "重置为卫星通讯方式", "重置为 TCP 方式" };
            return s;
        }
    }

    /// <summary>
    /// Sim 卡锁卡、解锁状态码。
    /// </summary>
    public class SimLockStatus
    {
        /// <summary>
        /// 询问锁卡状态。
        /// </summary>
        public const byte status_ask = 0x00;
        /// <summary>
        /// 锁定 Sim 卡。
        /// </summary>
        public const byte status_lock = 0x10;
        /// <summary>
        /// 解锁 Sim 卡。
        /// </summary>
        public const byte status_unlock = 0x20;
        /// <summary>
        /// 修改解锁密码。
        /// </summary>
        public const byte status_passwd = 0x30;
        /// <summary>
        /// Sim卡已锁定。
        /// </summary>
        public const byte sim_locked = 0x0F;
        /// <summary>
        /// Sim卡未锁定。
        /// </summary>
        public const byte sim_unlocked = 0xF0;
        /// <summary>
        /// 获取 Sim 卡的锁定、解锁状态。
        /// </summary>
        /// <param name="b">锁定、解锁状态。</param>
        /// <returns>返回锁定、解锁状态描述。</returns>
        public static string GetLockStatus(byte b)
        {
            string ss = "N/A";
            if (b == 0x0F)
                ss = "Sim卡已锁定";
            else if (b == 0xF0)
                ss = "Sim卡未锁定";
            else
            {
                int index = b / 0x10;
                if (index <= 3)
                    ss = GetLockStatus()[index];
            }
            return ss;
        }
        /// <summary>
        /// 获取 Sim 卡操作的各种状态。
        /// </summary>
        /// <returns>返回各种 Sim 卡操作的状态。</returns>
        public static string[] GetLockStatus()
        {
            string[] s = new string[] { "采集锁卡状态", "锁定 Sim 卡", "解锁 Sim 卡", "更改解锁密码" };
            return s;
        }
    }

    /// <summary>
    /// 获取仪表信息中的水温数据。
    /// </summary>
    public class WaterTempers
    {
        /// <summary>
        /// 转换 EPOS 仪表信息 EMD 中的水温数据。
        /// </summary>
        /// <param name="b">二进制代码。</param>
        /// <returns>返回转换过后的水温范围值。</returns>
        public static string GetWaterTemp(byte b)
        {
            string s = "";
            switch (b)
            {
                case 1: s = "41～60"; break;
                case 2: s = "61～71"; break;
                case 3: s = "72～78"; break;
                case 4: s = "79～85"; break;
                case 5: s = "86～94"; break;
                case 6: s = "95～98"; break;
                case 7: s = "99～101"; break;
                case 8: s = "102～104"; break;
                case 9: s = "105～107"; break;
                case 10: s = "> 108"; break;
                default: s = "< 40"; break;
            }
            return s;
        }
        /// <summary>
        /// 转换 EPOS 仪表信息 EMD 中的水温数据。
        /// </summary>
        /// <param name="value">水温数据。</param>
        /// <returns>返回转换过后的水温范围值。</returns>
        public static string GetWaterTempDX(ushort value)
        {
            string s = "";
            if (value == 0)
                value = 500;
            if (value <= 71)
                s = "108";
            else if (value < 88)
                s = "105";
            else if (value < 99)
                s = "102";
            else if (value < 107)
                s = "99";
            else if (value < 120)
                s = "95";
            else if (value < 163)
                s = "86";
            else if (value < 245)
                s = "79";
            else if (value < 314)
                s = "72";
            else if (value < 393)
                s = "61";
            else if (value < 462)
                s = "41";
            else
                s = "40";

            return s;
        }
    }

    /// <summary>
    /// 获取 EPOS 故障描述。
    /// </summary>
    public class AlarmsDescription
    {
        /// <summary>
        /// 获取 TX 协议中终端报警的描述。
        /// </summary>
        /// <param name="code">TX 终端的报警代码。</param>
        /// <returns>返回 TX 终端的报警描述。</returns>
        public static string GetAlarmDescription(ushort code)
        {
            uint tmp = code;
            string s = "";
            if (tmp / 32 > 0)
            {
                s += "卡被拔、";
                tmp = tmp % 32;
            }
            if (tmp / 16 > 0)
            {
                s += "越界、";
                tmp = tmp % 16;
            }
            if (tmp / 8 > 0)
            {
                s += "卫星天线断、";
                tmp = tmp % 8;
            }
            if (tmp / 4 > 0)
            {
                s += "超速、";
                tmp = tmp % 4;
            }
            if (tmp / 2 > 0)
            {
                s += "GPS天线断、";
                tmp = tmp % 2;
            }
            if (tmp / 1 > 0)
            {
                s += "主电断、";
            }
            return s.Substring(0, s.Length - 1);
        }
        /// <summary>
        /// 获取 T100 协议中的终端报警描述。
        /// </summary>
        /// <param name="arm">终端报警内容。</param>
        /// <returns>终端报警描述。</returns>
        public static string GetAlarmDescription(string arm)
        {
            return arm.Replace("M", "主电断").Replace("G", "GPS天线断").Replace("A", "主电断、GPS天线断");
        }
        /// <summary>
        /// 获取 DH 型号 EPOS 故障描述。
        /// </summary>
        /// <param name="code">故障代码。</param>
        /// <returns>故障描述。</returns>
        public static string GetDescription(byte code)
        {
            string s = "";
            switch (code)
            {
                case 1:
                    s = "电磁比例阀短路"; break;
                case 2:
                    s = "升压电磁阀短路"; break;
                case 3:
                    s = "回转优先电磁阀短路"; break;
                case 11:
                    s = "电磁比例阀开路"; break;
                case 12:
                    s = "升压电磁阀开路"; break;
                case 13:
                    s = "回转优先电磁阀开路"; break;
                case 21:
                    s = "油门旋钮输出异常(H)"; break;
                case 22:
                    s = "油门旋钮输出异常(L)"; break;
                case 23:
                    s = "TPS输出异常(H)"; break;
                case 24:
                    s = "TPS输出异常(L)"; break;
                case 25:
                    s = "前泵压力传感器异常(H)"; break;
                case 26:
                    s = "前泵压力传感器异常(L)"; break;
                case 27:
                    s = "后泵压力传感器异常(H)"; break;
                case 28:
                    s = "后泵压力传感器异常(L)"; break;
                case 29:
                    s = "Speed传感器输出异常"; break;
                case 30:
                    s = "T/M压力输出异常"; break;
                case 31:
                    s = "燃油传感器接地"; break;
                case 32:
                    s = "燃油传感器开路"; break;
                case 33:
                    s = "发电机电压过高"; break;
                case 34:
                    s = "发电机电压过低"; break;
                case 41:
                    s = "空气滤清器堵塞"; break;
                case 42:
                    s = "发动机油压低"; break;
                case 43:
                    s = "冷却水过热"; break;
                case 44:
                    s = "液压油过热"; break;
                case 82:
                    s = "通信异常"; break;
                default:
                    s = "未知"; break;
            }
            return s;
        }
        /// <summary>
        /// 获取 DX 型号故障报告中的 FMI 信息描述。
        /// </summary>
        /// <param name="code">FMI 代码。</param>
        /// <returns>FMI 信息描述。</returns>
        public static string GetFMIDescription(byte code)
        {
            string str = "";
            switch (code)
            {
                case 0:
                    str = "Above normal Range(DATA VALID but ABOVE NORMAL OPERATIONAL RANGE)";
                    break;
                case 1:
                    str = "Below normal Range(DATA VALID but BELOW NORMAL OPERATIONAL RANGE)";
                    break;
                case 2:
                    str = "Incorrect signal(DATA ERRATIC, INTERMITTENT OR INCORRECT)";
                    break;
                case 3:
                    str = "Voltage above nomal(VOLTAGE ABOVE NORMAL, OR SHORTED TO HIGH SOURCE)";
                    break;
                case 4:
                    str = "Voltage below nomal(VOLTAGE BELOW NORMAL, OR SHORTED TO LOW SOURCE)";
                    break;
                case 5:
                    str = "Current below nomal(CURRENT BELOW NORMAL OR OPEN CIRCUIT)";
                    break;
                case 6:
                    str = "Current above nomal(CURRENT ABOVE NORMAL OR GROUNDED CIRCUIT)";
                    break;
                case 7:
                    str = "SIGNAL ERROR";
                    break;
                case 8:
                    str = "Abnomal signal(ABNORMAL FRQUENCY OR PULSE WHDTH OR PERIOD)";
                    break;
                case 9:
                    str = "ABNORMAL UPDATE(ABNORMAL UPDATE RATE)";
                    break;
                case 10:
                    str = "ABNORMAL RATE OF CHANGE(ABNORMAL RATE OF CHANGE)";
                    break;
                case 11:
                    str = "Failure mode not identifiable(ROOT CAUSE NOT KNOWN - Malfunction)";
                    break;
                case 12:
                    str = "FAILUER";
                    break;
                case 13:
                    str = "OUT OF CALIBRATION";
                    break;
                case 14:
                    str = "SPECIAL INSTRUCTIONS";
                    break;
                case 15:
                    str = "DATA VALID BUT BELOW NORMAL OPERATIONAL RANGE - LEAST SEVERSE LEVEL";
                    break;
                case 16:
                    str = "DATA VALID BUT BELOW NORMAL OPERATIONAL RANGE - MODERATELY SEVERE LEVEL";
                    break;
                case 17:
                    str = "DATA VALID BUT BELOW NORMAL OPERATIONAL RANGE - LEAST SEVERSE LEVEL";
                    break;
                case 18:
                    str = "DATA VALID BUT BELOW NORMAL OPERATIONAL RANGE - MODERATELY SEVERE LEVEL";
                    break;
                case 19:
                    str = "RECEIVED NETWORK DATA IN ERROR";
                    break;
                case 20:
                case 21:
                case 22:
                case 23:
                case 24:
                case 25:
                case 26:
                case 27:
                case 28:
                case 29:
                case 30:
                    str = "RESERVED FOR SAE ASSIGNMENT";
                    break;
                case 31:
                    str = "NOT AVAILABLE OR CONDITION EXISTS";
                    break;
                default:
                    str = "Unknow";
                    break;
            }
            return str;
        }
        /// <summary>
        /// 获取 DX 型号 EPOS 故障描述。
        /// </summary>
        /// <param name="code">故障代码。</param>
        /// <returns>故障描述。</returns>
        public static string GetDescriptionDX(byte code)
        {
            string s = "";
            switch (code)
            {
                case 1: s = "V210 PUMP P/V"; break;
                case 2: s = "V211 COOLING FAN P/V"; break;
                case 3: s = "V212 FLOW CONTROL P/V"; break;
                case 4: s = "V213 RELIEF PRESSURE UP S/V"; break;
                case 5: s = "V214 HIGH SPEED S/V"; break;
                case 6: s = "V215 SWING PRIORITY S/V"; break;
                case 7: s = "V216 REVERSE FAN SPEED S/V"; break;
                case 8: s = "V217 STARTER RELAY"; break;
                case 9: s = "V218 AFTER HEAT RELAY"; break;
                case 10: s = "V227 DIAL"; break;
                case 11: s = "V230 E/G CONTROL MOTOR SENSOR"; break;
                case 12: s = "V228 ACCEL. PEDAL SENSOR ERROR"; break;
                case 13: s = "V225 FUEL LEVEL SENSOR"; break;
                case 14: s = "V222 HYD. OIL TEMP. SENSOR"; break;
                case 15: s = "V223 WATER TEMPERATURE SENSOR"; break;
                case 16: s = "V224 ENGINE SPEED SENSOR"; break;
                case 17: s = "V220 FRONT PUMP PRESS. SENSOR"; break;
                case 18: s = "V221 REAL PUMP PRESS. SENSOR"; break;
                case 20: s = "V229 PARK BRAKE PRESS. SENSOR"; break;
                case 21: s = "V226 ALTERNATOR POTENTIAL"; break;
                case 23: s = "V203 COMMUNICATION ERROR J1939"; break;
                case 24: s = "V202 ECU COMMUNICATION ERROR"; break;
                case 25: s = "V201 CAUGE PANEL COMMUNICATION ERROR"; break;
                case 26: s = "V231 ACCEL PEDAL SWITCH ERROR"; break;
                case 30: s = "E011 COOLANT TEMPERATURE SENSOR"; break;
                case 31: s = "E012 FUEL TEMPERATURE SENSOR"; break;
                case 32: s = "E013 BOOST AIR TEMPERATURE SENSOR"; break;
                case 33: s = "E014 BOOST AIR PRESSURE SENSOR"; break;
                case 34: s = "E017 E/G OIL TEMPERATURE SENSOR"; break;
                case 35: s = "E018 E/G OIL PRESSURE SENSOR"; break;
                case 36: s = "E021 BATTERY VOLTAGE"; break;
                case 37: s = "E022 FUEL PRESSURE SENSOR"; break;
                case 38: s = "E032 FUEL PRESSURE MONITORING MPROP"; break;
                case 39: s = "E037 CAN-B LINE"; break;
                case 40: s = "E038 ENGINE OVERSPEED"; break;
                case 41: s = "E039 MAIN RELAY SCG"; break;
                case 42: s = "E041 REDUNDANT SHUTOFF PATH"; break;
                case 43: s = "E042 E/G SPEED SENSOR (CRANKSHAFT)"; break;
                case 44: s = "E043 E/G SPEED SENSOR (CAMSHAFT)"; break;
                case 45: s = "E044 E/G SPEED SENSOR"; break;
                case 46: s = "E045 EEPROM"; break;
                case 47: s = "E046 RECOVERY"; break;
                case 48: s = "E047 MONITORING OF PRV"; break;
                case 49: s = "E048 POWER SUPPLY"; break;
                case 50: s = "E049 MAIN RELAY SCB"; break;
                case 51: s = "E051 MAIN RELAY ECU"; break;
                case 52: s = "E091 CAN-A LINE"; break;
                case 53: s = "E058 SOLENOID POWERSTAGE 1"; break;
                case 54: s = "E059 SOLENOID POWERSTAGE 2"; break;
                case 55: s = "E061 SOLENOID POWERSTAGE 3"; break;
                case 56: s = "E062 SOLENOID POWERSTAGE 4"; break;
                case 57: s = "E063 SOLENOID POWERSTAGE 5"; break;
                case 58: s = "E064 SOLENOID POWERSTAGE 6"; break;
                case 59: s = "E083 PREHEAT LAMP"; break;
                case 60: s = "E068 PREHEAT RELAY"; break;
                case 61: s = "E065 FUEL HI PRESSURE PUMP"; break;
                case 62: s = "E097 MONITORING OF MISFIRE CYLINDER 1"; break;
                case 63: s = "E098 MONITORING OF MISFIRE CYLINDER 2"; break;
                case 64: s = "E099 MONITORING OF MISFIRE CYLINDER 3"; break;
                case 65: s = "E101 MONITORING OF MISFIRE CYLINDER 4"; break;
                case 66: s = "E102 MONITORING OF MISFIRE CYLINDER 5"; break;
                case 67: s = "E103 MONITORING OF MISFIRE CYLINDER 6"; break;
                case 68: s = "E104 MONITORING OF MISFIRE MULTIPLE CYLINDER"; break;
                case 69: s = "E105 MONITORING OF OVERRUN"; break;
                case 70: s = "E106 ENGINE SPEED REDUNDANT"; break;
                case 71: s = "E104 FAILURE OF RECEIVING ECU ERROR LOG"; break;
                default: s = "Unknow"; break;
            }
            return s;
        }
    }
    /// <summary>
    /// 发动机输出控制信号
    /// </summary>
    public class MonitorEngineControl
    {
        /// <summary>
        /// 获取 DH 型号的发动机输出控制信息
        /// </summary>
        /// <param name="bin">数据</param>
        /// <returns></returns>
        public static string GetEngControl(byte b)
        {
            string bin = ProtocolItems.IntToDigit(b, 2, 8);
            string tmp = "";
            tmp += bin[7] == '1' ? "标准、" : "";
            tmp += bin[6] == '1' ? "自动怠速、" : "";
            tmp += bin[5] == '1' ? "过热、" : "";
            tmp += bin[4] == '1' ? "工作模式、" : "";
            if ("" != tmp)
                tmp = tmp.Substring(0, tmp.Length - 1);
            return tmp;
        }
        /// <summary>
        /// 获取 DX 型号的发动机输出控制信息
        /// </summary>
        /// <param name="bin"></param>
        /// <returns></returns>
        public static string GetEngControlDX(byte b)
        {
            string bin = ProtocolItems.IntToDigit(b, 2, 8);
            string tmp = "";
            tmp += bin[7] == '1' ? "标准、" : "";
            tmp += bin[6] == '1' ? "自动怠速、" : "";
            tmp += bin[5] == '1' ? "工作模式、" : "";
            tmp += bin[4] == '1' ? "过热、" : "";
            if ("" != tmp)
                tmp = tmp.Substring(0, tmp.Length - 1);
            return tmp;
        }
    }
    /// <summary>
    /// 输入信号
    /// </summary>
    public class MonitorInput
    {
        /// <summary>
        /// 获取输入信号
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string GetInput(byte[] b)
        {
            int len = b.Length;
            string bin_1, bin_2, bin_3, bin_4;
            bin_1 = ProtocolItems.IntToDigit(b[0], 2, 8);
            bin_2 = ProtocolItems.IntToDigit(b[1], 2, 8);
            bin_3 = len == 2 ? "" : ProtocolItems.IntToDigit(b[2], 2, 8);
            bin_4 = len == 2 ? "" : ProtocolItems.IntToDigit(b[3], 2, 8);
            string tmp = "";
            tmp += len == 2 ? (bin_1[7] == '1' ? "发电机、" : "") : (bin_4[4] == '1' ? "发电机、" : "");
            tmp += len == 2 ? (bin_1[6] == '1' ? "升压、" : "") : (bin_1[7] == '1' ? "升压、" : "");
            tmp += len == 2 ? (bin_1[5] == '1' ? "行走选择、" : "") : (bin_1[5] == '1' ? "行走选择、" : "");
            tmp += len == 2 ? (bin_1[4] == '1' ? "高速行走、" : "") : (bin_1[4] == '1' ? "高速行走、" : "");
            tmp += len == 2 ? (bin_1[3] == '1' ? "自动行走、" : "") : (bin_1[3] == '1' ? "自动行走、" : "");
            tmp += len == 2 ? (bin_1[2] == '1' ? "作业灯、" : "") : (bin_1[2] == '1' ? "作业灯、" : "");
            tmp += len == 2 ? (bin_2[7] == '1' ? "行走压力、" : "") : (bin_3[7] == '1' ? "行走压力、" : "");
            tmp += len == 2 ? (bin_2[6] == '1' ? "作业压力、" : "") : (bin_3[6] == '1' ? "作业压力、" : "");
            tmp += len == 2 ? (bin_2[5] == '1' ? "发动机油压、" : "") : (bin_3[1] == '1' ? "发动机油压、" : "");
            tmp += len == 2 ? (bin_2[4] == '1' ? "空气滤清器、" : "") : (bin_3[5] == '1' ? "空气滤清器、" : "");
            tmp += len == 2 ? (bin_2[3] == '1' ? "回油滤清器、" : "") : (bin_3[3] == '1' ? "回油滤清器、" : "");
            tmp += len == 2 ? (bin_2[2] == '1' ? "先导滤清器、" : "") : (bin_3[4] == '1' ? "先导滤清器、" : "");
            tmp += len == 2 ? (bin_2[1] == '1' ? "过负荷选择、" : "") : (bin_3[2] == '1' ? "过负荷选择、" : "");
            if ("" != tmp)
                tmp = tmp.Substring(0, tmp.Length - 1);
            return tmp;
        }
    }
    /// <summary>
    /// 模式选择
    /// </summary>
    public class MonitorModule
    {
        /// <summary>
        /// 获取模式选择信息
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string GetModule(byte b, byte tt)
        {
            string bin = ProtocolItems.IntToDigit(b, 2, 8);
            string tmp = "";
            tmp += "Power：" + (bin[7] == '1' ? "Power、" : "标准、");
            tmp += "作业：" + (tt == TerminalTypes.DH ? (bin[6] == '1' ? "挖沟、" : "挖掘、") : (bin[5] == '1' ? "挖沟、" : "挖掘、"));
            tmp += tt == TerminalTypes.DH ? (bin[5] == '1' ? "自动怠速、" : "") : (bin[3] == '1' ? "自动怠速、" : "");
            if ("" != tmp)
                tmp = tmp.Substring(0, tmp.Length - 1);
            return tmp;
        }
    }
    /// <summary>
    /// 输出信息
    /// </summary>
    public class MonitorOutput
    {
        /// <summary>
        /// 获取输出信息
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string GetOutput(byte b)
        {
            string bin = ProtocolItems.IntToDigit(b, 2, 8);
            string tmp = "";
            tmp += bin[7] == '1' ? "升压、" : "";
            tmp += bin[6] == '1' ? "高速行走、" : "";
            tmp += bin[5] == '1' ? "回转优先、" : "";
            tmp += bin[4] == '1' ? "回转、" : "";
            tmp += bin[3] == '1' ? "启动接替、" : "";
            if ("" != tmp)
                tmp = tmp.Substring(0, tmp.Length - 1);
            return tmp;
        }
    }
    /// <summary>
    /// 先导灯输出信息。
    /// </summary>
    public class MonitorPilot
    {
        /// <summary>
        /// 获取先导灯输出信息
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string GetPilot(byte b,byte tt)
        {
            string bin = ProtocolItems.IntToDigit(b, 2, 8);
            string tmp = "";
            tmp += bin[7] == '1' ? "充电、" : "";
            tmp += bin[6] == '1' ? "发动机油压、" : "";
            tmp += tt == TerminalTypes.DH ? (bin[5] == '1' ? "发动机过热、" : "") : (bin[4] == '1' ? "发动机过热" : "");
            tmp += tt == TerminalTypes.DH ? (bin[3] == '1' ? "工作灯、" : "") : (bin[1] == '1' ? "工作灯" : "");
            tmp += tt == TerminalTypes.DH ? (bin[1] == '1' ? "过负荷报警、" : "") : (bin[2] == '1' ? "过负荷报警" : "");
            if ("" != tmp)
                tmp = tmp.Substring(0, tmp.Length - 1);
            return tmp;
        }
    }
    /// <summary>
    /// 液压泵压力计算
    /// </summary>
    public class MonitorPump
    {
        /// <summary>
        /// 获取液压泵压力大小
        /// </summary>
        /// <param name="pump"></param>
        /// <returns></returns>
        public static string GetPump(ushort pump, doosan type)
        {
            double p = pump * 1.0;
            if (type == doosan.DH)
            {
                if (pump > 1070)
                {
                    p = (p - 1000) / 8.16;
                }
            }
            else
            {
                p = (p * 5000 / 1024 - 1000) / 8.16;
            }
            return string.Format("{0:0}", p);
        }
    }
    /// <summary>
    /// 获取 EPOS 信息中仪表盘信息的油温数据。
    /// </summary>
    public class OilTempers
    {
        /// <summary>
        /// 转换 EPOS 数据中仪表 EMD 信息中的油温数据。
        /// </summary>
        /// <param name="b">二进制数据</param>
        /// <returns>返回转换过后的油温数据。</returns>
        public static string GetOilTemp(byte b)
        {
            string s = "";
            switch (b)
            {
                case 1: s = "< 30"; break;
                case 2: s = "30～50"; break;
                case 3: s = "50～75"; break;
                case 4: s = "75～85"; break;
                case 5: s = "85～95"; break;
                case 6: s = "> 96"; break;
                default: s = "0"; break;
            }
            return s;
        }
        /// <summary>
        /// 转换 EPOS 数据中 monitor 信息中的油温数据。
        /// </summary>
        /// <param name="value">油温数据。</param>
        /// <returns>油温描述。</returns>
        public static string GetOilTempDX(ushort value)
        {
            if (value == 0)
                value = 500;
            string s = "";
            if (value < 114)
                s = "100";
            else if (value < 120)
                s = "95";
            else if (value < 127)
                s = "93";
            else if (value < 143)
                s = "90";
            else if (value < 163)
                s = "86";
            else if (value < 235)
                s = "80";
            else if (value < 330)
                s = "70";
            else if (value < 392)
                s = "60";
            else if (value < 434)
                s = "50";
            else if (value < 465)
                s = "40";
            else
                s = "40";

            return s;
        }
        /// <summary>
        /// 转换 DX 燃油剩余量。
        /// </summary>
        /// <param name="value">EPOS 数据。</param>
        /// <returns>转换后的燃油剩余量：0~100。</returns>
        public static byte GetOilLeftDX(ushort value)
        {
            int tmp = (0 == value) ? 400 : value;
            byte ret = 0;
            if (117 > tmp)
                ret = 100;
            else if (142 > tmp && 117 <= tmp)
                ret = 90;
            else if (189 > tmp && 142 <= tmp)
                ret = 80;
            else if (226 > tmp && 189 <= tmp)
                ret = 70;
            else if (255 > tmp && 226 <= tmp)
                ret = 60;
            else if (278 > tmp && 255 <= tmp)
                ret = 50;
            else if (297 > tmp && 278 <= tmp)
                ret = 40;
            else if (313 > tmp && 297 <= tmp)
                ret = 30;
            else if (327 > tmp && 313 <= tmp)
                ret = 20;
            else if (339 > tmp && 327 <= tmp)
                ret = 10;
            else if (339 <= tmp)
                ret = 0;

            return ret;
        }
    }

    /// <summary>
    /// TX300 协议中 0xCC00 命令里连接状态字段。
    /// </summary>
    public class ConnectStatus
    {
        /// <summary>
        /// 获取字符串描述的重启类型。
        /// </summary>
        /// <param name="b">二进制流表示的重启类型。</param>
        /// <returns>字符串表示的重启类型。</returns>
        public static string GetConnectStatus(byte b)
        {
            string s = "";
            switch (b)
            {
                case 0x00:
                case 0x10:
                case 0x20:
                case 0x40:
                    s = "服务器命令重启"; break;
                case 0x01: s = "发送数据时错误"; break;
                case 0x02: s = "本状态码已删除不用"; break;
                case 0x03: s = "模块无回复"; break;
                case 0x04: s = "SMS 收信过程错误"; break;
                case 0x05: s = "CPU 与模块 7 分钟没有通讯"; break;
                case 0x06: s = "CPU 超过 12 分钟没有检查网络"; break;
                case 0x07: s = "SMS 收信在 5 分钟内没处理完"; break;
                case 0x08: s = "初始化时间超过 2 分钟以上"; break;
                case 0x09: s = "模块初始化时，超过按命令阶段回复"; break;
                case 0x0A: s = "超过定期报告时间但没有上传报告"; break;
                case 0x0B: s = "终端低电压报警时"; break;
                case 0x0C: s = "发送 0xCC00 时没有 Sim 卡号码"; break;
                case 0x0D: s = "5 小时内发送命令一次都没成功"; break;
                case 0x0E: s = "5 分钟内 CSQ > limit 但没有网络"; break;
                case 0x0F: s = "Sim 卡保安命令锁车"; break;
                case 0x11: s = "试图连接 GPRS 时连接错误"; break;
                case 0x12: s = "工作中模块电源关闭"; break;
                case 0x13: s = "GPS 拆除报警后 GPS 依然可以收信"; break;
                case 0x14: s = "备份过程中超过备份数量"; break;
                case 0x15: s = "超过报告传送队列个数"; break;
                case 0x16: s = "超过 EPOS 传送队列个数"; break;
                case 0x17: s = "发送报告时发生超过数据数量错误"; break;
                case 0x18: s = "超过 EPOS 发送待机时间（20秒）"; break;
                case 0x19: s = "超过备份进行待机时间（5分）"; break;
                case 0x1A: s = "模块收信超时重启（10秒）"; break;
                case 0x1B: s = "EPOS 收信超时重启（10秒）"; break;
                case 0x1C: s = "模块收信过程中处理错误重启（1分）"; break;
                case 0x1D: s = "EPOS 收信过程中处理错误重启（1分）"; break;
                case 0x1E: s = "网络检查过程中错误（1分）"; break;
                default: s = "unknow"; break;
            }
            return s;
        }
    }

    /// <summary>
    /// TX300 通讯协议包头结构。
    /// </summary>
    public class TX300
    {
        /// <summary>
        /// 整包总长度。
        /// </summary>
        protected ushort total_length;
        /// <summary>
        /// 通讯所用的协议。
        /// </summary>
        private byte protocol_type;
        /// <summary>
        /// 终端类型。
        /// </summary>
        private byte terminal_type;
        /// <summary>
        /// 命令字。每一个数据包中唯一标识。
        /// </summary>
        private ushort command_id;
        /// <summary>
        /// 本包数据所用的通讯协议版本。所有的升级版本向下兼容。
        /// </summary>
        private byte protocol_version;
        /// <summary>
        /// 终端发送消息的流水号。
        /// </summary>
        private ushort sequence_id;
        /// <summary>
        /// 终端号码。
        /// </summary>
        private byte[] terminal_id = new byte[TX300Items.terminal_id_length];
        /// <summary>
        /// 帧 ID。
        /// </summary>
        private byte package_id;
        /// <summary>
        /// 帧总数
        /// </summary>
        private byte total_package;
        /// <summary>
        /// 按照 TX300 协议组包后的二进制流。
        /// </summary>
        protected byte[] content;
        /// <summary>
        /// 去掉 TX300 协议包头之后的消息内容。
        /// </summary>
        private byte[] messages;
        protected int iIndex = 0;
        /// <summary>
        /// 创建一个新的按照 TX300 最新通讯协议配置的数据包。
        /// </summary>
        public TX300()
        {
            total_length = 0;
            protocol_version = TX300Items.protocol_version;
            package_id = 1;
            total_package = 1;
            sequence_id = 0xFFFF;
        }
        /// <summary>
        /// 通过一个 16 进制的字符串表示的二进制流来创建一个数据包。
        /// </summary>
        /// <param name="data_hex_string">由 16 进制字符串表示的二进制流。</param>
        public TX300(string data_hex_string)
        {
            content = ProtocolItems.GetBytes(data_hex_string);
        }
        /// <summary>
        /// 通过一个二进制序列来创建一个数据包。
        /// </summary>
        /// <param name="b"></param>
        public TX300(byte[] b)
        {
            content = new byte[b.Length];
            Buffer.BlockCopy(b, 0, content, 0, b.Length);
        }
        /// <summary>
        /// 获取数据包的总长度。
        /// </summary>
        public ushort TotalLength
        {
            get { return total_length; }
        }
        /// <summary>
        /// 获取或设置数据包的通讯协议类型。
        /// </summary>
        public byte ProtocolType
        {
            get { return protocol_type; }
            set { protocol_type = value; }
        }
        /// <summary>
        /// 获取或设置数据包的终端类型。
        /// </summary>
        public byte TerminalType
        {
            get { return terminal_type; }
            set { terminal_type = value; }
        }
        /// <summary>
        /// 获取或设置命令字。
        /// </summary>
        public ushort CommandID
        {
            get { return command_id; }
            set { command_id = value; }
        }
        /// <summary>
        /// 获取或设置通讯协议版本号。
        /// </summary>
        public byte ProtocolVersion
        {
            get { return protocol_version; }
            set { protocol_version = value; }
        }
        /// <summary>
        /// 获取或设置流水号。
        /// </summary>
        public ushort SequenceID
        {
            get { return sequence_id; }
            set { sequence_id = value; }
        }
        /// <summary>
        /// 获取或设置终端 ID。
        /// </summary>
        public string TerminalID
        {
            get 
            {
                string t = ProtocolItems.GetHex(terminal_id);
                // 去掉最前面的 0 后就是终端的 Sim 卡号码。
                return t.Substring(1); 
            }
            set
            {
                byte[] b = ProtocolItems.GetBytes(value);
                Buffer.BlockCopy(b, 0, terminal_id, 0, b.Length);
                b = null;
            }
        }
        /// <summary>
        /// 获取或设置数据包的帧索引。
        /// </summary>
        public byte PackageID
        {
            get { return package_id; }
            set { package_id = value; }
        }
        /// <summary>
        /// 获取或设置总帧数。
        /// </summary>
        public byte TotalPackage
        {
            get { return total_package; }
            set { total_package = value; }
        }
        /// <summary>
        /// 获取或设置数据包的二进制流。
        /// </summary>
        public byte[] Content
        {
            get { return content; }
            set { Buffer.BlockCopy(value, 0, content, 0, value.Length); }
        }
        /// <summary>
        /// 将 16 进制的字符串导入二进制流中作拆包用。
        /// </summary>
        public string SetContent
        {
            set { content = ProtocolItems.GetBytes(value); }
        }
        /// <summary>
        /// 获取或设置数据包内容（去除 TX300 协议数据包头部分的数据）。
        /// </summary>
        public byte[] MsgContent
        {
            get { return messages; }
            set { Buffer.BlockCopy(value, 0, messages, 0, value.Length); }
        }

        /// <summary>
        /// 清空数据包中各字段的值以便重新设定和打包。
        /// </summary>
        public virtual void Clear()
        {
            content = null;
            messages = null;
            terminal_id.Initialize();
            command_id = 0;
            sequence_id = 0;
        }
        /// <summary>
        /// 将各个字段的值按照 TX300 最新通讯协议打包。
        /// </summary>
        public virtual void msg_to_package()
        {
            // 跳过 total_length 字段，等到组包最后再处理。
            iIndex = Marshal.SizeOf(total_length);
            // 组包 protocol_type 字段
            iIndex += Marshal.SizeOf(protocol_type);
            content = new byte[iIndex];
            content[content.Length - Marshal.SizeOf(protocol_type)] = protocol_type;
            // 组包 terminal_type 字段
            iIndex += Marshal.SizeOf(terminal_type);
            content = ProtocolItems.expand(content, iIndex);
            content[content.Length - Marshal.SizeOf(terminal_type)] = terminal_type;
            // 组包 command_id 字段
            byte[] b = BitConverter.GetBytes(command_id);
            iIndex += b.Length;
            content = ProtocolItems.expand(content, iIndex);
            Buffer.BlockCopy(b, 0, content, content.Length - b.Length, b.Length);
            // 组包 protocol_version 字段
            iIndex += Marshal.SizeOf(protocol_version);
            content = ProtocolItems.expand(content, iIndex);
            content[content.Length - Marshal.SizeOf(protocol_version)] = protocol_version;
            // 组包 sequence_id 字段
            b = BitConverter.GetBytes(sequence_id);
            iIndex += b.Length;
            content = ProtocolItems.expand(content, iIndex);
            Buffer.BlockCopy(b, 0, content, content.Length - b.Length, b.Length);
            // 组包 terminal_id 字段
            iIndex += terminal_id.Length;
            content = ProtocolItems.expand(content, iIndex);
            Buffer.BlockCopy(terminal_id, 0, content, content.Length - terminal_id.Length, terminal_id.Length);
            // 组包 package_id 字段
            iIndex += Marshal.SizeOf(package_id);
            content = ProtocolItems.expand(content, iIndex);
            content[content.Length - Marshal.SizeOf(package_id)] = package_id;
            // 组包 total_package 字段
            iIndex += Marshal.SizeOf(total_package);
            content = ProtocolItems.expand(content, iIndex);
            content[content.Length - Marshal.SizeOf(total_package)] = total_package;
            // 重新组包 total_length
            total_length = (ushort)iIndex;
            b = BitConverter.GetBytes(total_length);
            Buffer.BlockCopy(b, 0, content, 0, b.Length);
        }
        /// <summary>
        /// 将各个字段按照最新的 TX300 通讯协议解析出来。
        /// </summary>
        /// <returns></returns>
        public virtual bool package_to_msg()
        {
            iIndex = 0;
            // 解包 total_length 字段
            total_length = BitConverter.ToUInt16(content, iIndex);
            iIndex += Marshal.SizeOf(total_length);
            // 解包 protocol_type 字段
            protocol_type = content[iIndex];
            iIndex += Marshal.SizeOf(protocol_type);
            // 解包 terminal_type 字段
            terminal_type = content[iIndex];
            iIndex += Marshal.SizeOf(terminal_type);
            // 解包 command_id 字段
            command_id = BitConverter.ToUInt16(content, iIndex);
            iIndex += Marshal.SizeOf(command_id);
            // 解包 protocol_version 字段
            protocol_version = content[iIndex];
            iIndex += Marshal.SizeOf(protocol_version);
            // 解包 sequence_id 字段
            sequence_id = BitConverter.ToUInt16(content, iIndex);
            iIndex += Marshal.SizeOf(sequence_id);
            // 解包 terminal_id 字段
            Buffer.BlockCopy(content, iIndex, terminal_id, 0, terminal_id.Length);
            iIndex += terminal_id.Length;
            // 解包 package_id 字段
            package_id = content[iIndex];
            iIndex += Marshal.SizeOf(package_id);
            // 解包 total_package 字段
            total_package = content[iIndex];
            iIndex += Marshal.SizeOf(total_package);
            // 解包 messages 字段
            if (total_length > iIndex)
            {
                messages = new byte[total_length - iIndex];
                Buffer.BlockCopy(content, iIndex, messages, 0, messages.Length);
            }
            return iIndex == total_length;
        }
    }
    /// <summary>
    /// 0x1000 定位信息相关命令。
    /// </summary>
    public class TX300_1000 : TX300
    {
        public TX300_1000()
        {
            CommandID = 0x1000;
        }
        /// <summary>
        /// 需求定位信息条数。
        /// </summary>
        private byte request_no;
        /// <summary>
        /// 设置需求的定位信息条数。
        /// </summary>
        public byte RequestNo
        {
            set { request_no = value; }
            //get { return request_no; }
        }
        /// <summary>
        /// 将 0x1000 命令中的各个字段按照 TX300 协议打包。
        /// </summary>
        public override void msg_to_package()
        {
            base.msg_to_package();
            // 组包 request_no
            iIndex += Marshal.SizeOf(request_no);
            content = ProtocolItems.expand(content, iIndex);
            content[iIndex - 1] = request_no;
            // 组包 total_length
            total_length = (ushort)iIndex;
            byte[] b = BitConverter.GetBytes(total_length);
            Buffer.BlockCopy(b, 0, content, 0, b.Length);
        }
    }
    /// <summary>
    /// 0x3000 远程开关机命令。
    /// </summary>
    public class TX300_3000 : TX300
    {
        public TX300_3000()
        {
            CommandID = 0x3000;
        }
        /// <summary>
        /// 远程控制状态。
        /// </summary>
        private byte control_status;
        /// <summary>
        /// 控制动作时间：在此时间之后开始控制。
        /// </summary>
        private byte action_time;
        /// <summary>
        /// 设置远程控制状态类型。
        /// </summary>
        public byte ControlStatus
        {
            set { control_status = value; }
        }
        /// <summary>
        /// 设置远程控制动作时间。
        /// </summary>
        public byte ActionTime
        {
            set { action_time = value; }
        }
        /// <summary>
        /// 将 0x3000 命令中的各个字段按照 TX300 协议打包。
        /// </summary>
        public override void msg_to_package()
        {
            base.msg_to_package();
            // 组包 control_status
            iIndex += Marshal.SizeOf(control_status);
            content = ProtocolItems.expand(content, iIndex);
            content[iIndex - Marshal.SizeOf(control_status)] = control_status;
            // 组包 action_time
            iIndex += Marshal.SizeOf(action_time);
            content = ProtocolItems.expand(content, iIndex);
            content[iIndex - Marshal.SizeOf(action_time)] = action_time;
            // 组包 total_length
            total_length = (ushort)iIndex;
            byte[] b = BitConverter.GetBytes(total_length);
            Buffer.BlockCopy(b, 0, content, 0, b.Length);
        }
    }
    /// <summary>
    /// 0x4000 远程重置相关命令。
    /// </summary>
    public class TX300_4000 : TX300
    {
        public TX300_4000()
        {
            CommandID = 0x4000;
        }
        /// <summary>
        /// 重置状态。
        /// </summary>
        private byte reset_status;
        /// <summary>
        /// 重置动作时间：在此时间后重置。
        /// </summary>
        private byte action_time;
        /// <summary>
        /// 设置重置状态码。
        /// </summary>
        public byte ResetStatus
        {
            set { reset_status = value; }
        }
        /// <summary>
        /// 设置重置动作时间。
        /// </summary>
        public byte ActionTime
        {
            set { action_time = value; }
        }
        /// <summary>
        /// 将 0x4000 命令中的各个字段按照 TX300 协议打包。
        /// </summary>
        public override void msg_to_package()
        {
            base.msg_to_package();
            // 组包 reset_status
            iIndex += Marshal.SizeOf(reset_status);
            content = ProtocolItems.expand(content, iIndex);
            content[iIndex - 1] = reset_status;
            // 组包 action_time
            iIndex += Marshal.SizeOf(action_time);
            content = ProtocolItems.expand(content, iIndex);
            content[iIndex - 1] = action_time;
            // 组包 total_length
            total_length = (ushort)iIndex;
            byte[] b = BitConverter.GetBytes(total_length);
            Buffer.BlockCopy(b, 0, content, 0, b.Length);
        }
    }
    /// <summary>
    /// 0x6002 EPOS 运转信息图形数据相关命令。
    /// </summary>
    public class TX300_6002 : TX300
    {
        public TX300_6002()
        {
            CommandID = 0x6002;
        }
        /// <summary>
        /// 需求的图形分析数据的时间，单位分钟。
        /// </summary>
        private byte request_times = 1;
        /// <summary>
        /// 设置需求的分析数据的时间，单位分钟。
        /// </summary>
        public byte RequestTimes
        {
            set { request_times = value; }
        }
        /// <summary>
        /// 将 0x6002 命令中的各个字段按照 TX300 协议打包。
        /// </summary>
        public override void msg_to_package()
        {
            base.msg_to_package();
            // 打包 request_times
            iIndex += Marshal.SizeOf(request_times);
            content = ProtocolItems.expand(content, iIndex);
            content[iIndex - 1] = request_times;
            // 打包 total_length
            total_length = (ushort)iIndex;
            byte[] b = BitConverter.GetBytes(total_length);
            Buffer.BlockCopy(b, 0, content, 0, b.Length);
        }
    }
    /// <summary>
    /// 0x6007 保安命令相关。
    /// </summary>
    public class TX300_6007 : TX300
    {
        public TX300_6007()
        {
            CommandID = 0x6007;
        }
        /// <summary>
        /// 保安命令码。
        /// </summary>
        private byte security_code;
        /// <summary>
        /// DX 系列保安命令的时间戳：YYMMDDHHMM。
        /// </summary>
        private byte[] time_stamp = new byte[TX300Items.security_time_stamp_length];
        /// <summary>
        /// 设置保安命令码。
        /// </summary>
        public byte SecurityCode
        {
            set { security_code = value; }
        }
        /// <summary>
        /// 将 0x6007 命令中的各个字段按照 TX300 协议打包。
        /// </summary>
        public override void msg_to_package()
        {
            base.msg_to_package();
            // 判断 DX 或 DH 型号的终端通讯类容
            // DX 系列保安命令有时间戳。
            if (TerminalType == TerminalTypes.DX)
            {
                string stamp = DateTime.Now.ToString("yyMMddHHmm");
                Buffer.BlockCopy(ProtocolItems.GetBytes(stamp), 0, time_stamp, 0, TX300Items.security_time_stamp_length);
                iIndex += TX300Items.security_time_stamp_length;
                content = ProtocolItems.expand(content, iIndex);
                Buffer.BlockCopy(time_stamp, 0, content, iIndex - TX300Items.security_time_stamp_length, TX300Items.security_time_stamp_length);
            }
            // 组包 security_code
            iIndex += Marshal.SizeOf(security_code);
            content = ProtocolItems.expand(content, iIndex);
            content[iIndex - 1] = security_code;
            // 组包 total_length
            total_length = (ushort)iIndex;
            byte[] b = BitConverter.GetBytes(total_length);
            Buffer.BlockCopy(b, 0, content, 0, b.Length);
        }
    }
    /// <summary>
    /// 0x600B 设置 DH80G/DX60/Loader 运转时间相关命令。
    /// </summary>
    public class TX300_600B : TX300
    {
        public TX300_600B()
        {
            CommandID = 0x600B;
        }
        /// <summary>
        /// 运转时间：小时。
        /// </summary>
        private ushort run_hours;
        /// <summary>
        /// 运转时间：分钟。
        /// </summary>
        private byte run_minutes;
        /// <summary>
        /// 设置 DH80G/DX60/Loader 运转时间：小时。
        /// </summary>
        public ushort Hours
        {
            set { run_hours = value; }
        }
        /// <summary>
        /// 设置 DH80G/DX60/Loader 运转时间：分钟。
        /// </summary>
        public byte Minutes
        {
            set { run_minutes = value; }
        }
        /// <summary>
        /// 将 0x600B 命令各字段按照 TX300 协议打包。
        /// </summary>
        public override void msg_to_package()
        {
            base.msg_to_package();
            // 组包 run_hours
            byte[] b = BitConverter.GetBytes(run_hours);
            iIndex += b.Length;
            content = ProtocolItems.expand(content, iIndex);
            Buffer.BlockCopy(b, 0, content, iIndex - b.Length, b.Length);
            // 组包 run_minutes
            iIndex += Marshal.SizeOf(run_minutes);
            content = ProtocolItems.expand(content, iIndex);
            content[iIndex - Marshal.SizeOf(run_minutes)] = run_minutes;
            // 组包 total_length
            total_length = (ushort)iIndex;
            b = BitConverter.GetBytes(total_length);
            Buffer.BlockCopy(b, 0, content, 0, b.Length);
        }
    }
    /// <summary>
    /// TX10G 的参数设置命令。
    /// </summary>
    public class TX10G_7010 : TX300
    {
        public TX10G_7010()
        {
            CommandID = 0x7010;
        }
        /// <summary>
        /// 命令状态：0x00，采集；0xFF，设置。
        /// </summary>
        private byte msg_status;
        /// <summary>
        /// 定期报告时间间隔。
        /// </summary>
        private ushort period;
        /// <summary>
        /// 停车超时时间。
        /// </summary>
        private byte stoped;
        /// <summary>
        /// 心跳时间间隔。
        /// </summary>
        private byte heartbeat;
        /// <summary>
        /// 最低信号强度（盲区）。
        /// </summary>
        private byte csq_low;
        /// <summary>
        /// SMS 通讯信号强度设置。
        /// </summary>
        private byte csq_sms;
        /// <summary>
        /// 后备电池电压。
        /// </summary>
        private byte battery;
        /// <summary>
        /// 服务器 IP 地址。
        /// </summary>
        private byte[] server_address = new byte[TX300Items.server_address_length];
        /// <summary>
        /// 服务器端口。
        /// </summary>
        private ushort server_port;
        /// <summary>
        /// SMS 服务器地址。
        /// </summary>
        private byte[] sms_address;
        /// <summary>
        /// 获取或设置命令状态。
        /// </summary>
        public byte MsgStatus
        {
            get { return msg_status; }
            set { msg_status = value; }
        }
        /// <summary>
        /// 获取或设置定期报告时间间隔。
        /// </summary>
        public ushort PeriodInteval
        {
            get { return period; }
            set { period = value; }
        }
        /// <summary>
        /// 获取或设置停车超时时间。
        /// </summary>
        public byte StopTimeout
        {
            get { return stoped; }
            set { stoped = value; }
        }
        /// <summary>
        /// 获取或设置心跳时间间隔。
        /// </summary>
        public byte Heartbeat
        {
            get { return heartbeat; }
            set { heartbeat = value; }
        }
        /// <summary>
        /// 获取或设置最低信号强度。
        /// </summary>
        public byte LowCSQ
        {
            get { return csq_low; }
            set { csq_low = value; }
        }
        /// <summary>
        /// 获取或设置 SMS 通讯信号强度。
        /// </summary>
        public byte SMSCSQ
        {
            get { return csq_sms; }
            set { csq_sms = value; }
        }
        /// <summary>
        /// 获取或设置后备电池最低电压设定。
        /// </summary>
        public byte Battery
        {
            get { return battery; }
            set { battery = value; }
        }
        /// <summary>
        /// 获取或设置 GPRS 服务器 IP 地址。
        /// </summary>
        public string ServerIP
        {
            get
            {
                string ip = "";
                for (int i = 0; i < server_address.Length; i++)
                {
                    ip += server_address[i].ToString() + ".";
                }
                // 返回去掉最后的.的IP地址。
                return ip.Substring(0, ip.Length - 1); ;
            }
            set
            {
                string[] s = value.Split(new char[] { '.' });
                server_address = new byte[TX300Items.server_address_length];
                for (int i = 0; i < s.Length; i++)
                {
                    server_address[i] = byte.Parse(s[i]);
                }
            }
        }
        /// <summary>
        /// 获取或设置 GPRS 服务器端口号码。
        /// </summary>
        public ushort ServerPort
        {
            get { return server_port; }
            set { server_port = value; }
        }
        /// <summary>
        /// 获取或设置 SMS 服务地址。
        /// </summary>
        public string SmsAddress
        {
            get { return ProtocolItems.GetHex(sms_address); }
            set
            {
                if (value.Length % 2 != 0)
                    value = "0" + value;
                byte[] b = ProtocolItems.GetBytes(value);
                sms_address = new byte[b.Length + 1];
                Buffer.BlockCopy(b, 0, sms_address, 1, b.Length);
                sms_address[0] = (byte)b.Length;
            }
        }
        /// <summary>
        /// 将 0x7010 命令的各个字段按照协议打包。
        /// </summary>
        public override void msg_to_package()
        {
            base.msg_to_package();
            byte[] b = null;
            // 打包 msg_status
            iIndex += Marshal.SizeOf(msg_status);
            content = ProtocolItems.expand(content, iIndex);
            content[iIndex - 1] = msg_status;
            if (msg_status == 0xFF)
            {
                // 组包 period
                b = BitConverter.GetBytes(period);
                iIndex += b.Length;
                content = ProtocolItems.expand(content, iIndex);
                Buffer.BlockCopy(b, 0, content, iIndex - b.Length, b.Length);
                // 组包 stoped
                iIndex += Marshal.SizeOf(stoped);
                content = ProtocolItems.expand(content, iIndex);
                content[iIndex - 1] = stoped;
                // 组包 heartbeat
                iIndex += Marshal.SizeOf(heartbeat);
                content = ProtocolItems.expand(content, iIndex);
                content[iIndex - 1] = heartbeat;
                // 组包 csq_low
                iIndex += Marshal.SizeOf(csq_low);
                content = ProtocolItems.expand(content, iIndex);
                content[iIndex - 1] = csq_low;
                // 组包 csq_sms
                iIndex += Marshal.SizeOf(csq_sms);
                content = ProtocolItems.expand(content, iIndex);
                content[iIndex - 1] = csq_sms;
                // 组包 battery
                iIndex += Marshal.SizeOf(battery);
                content = ProtocolItems.expand(content, iIndex);
                content[iIndex - 1] = battery;
                // 组包 server_address
                iIndex += server_address.Length;
                content = ProtocolItems.expand(content, iIndex);
                Buffer.BlockCopy(server_address, 0, content, iIndex - server_address.Length, server_address.Length);
                // 组包 server_port
                b = BitConverter.GetBytes(server_port);
                iIndex += b.Length;
                content = ProtocolItems.expand(content, iIndex);
                Buffer.BlockCopy(b, 0, content, iIndex - b.Length, b.Length);
                // 组包 sms_address
                iIndex += sms_address.Length;
                content = ProtocolItems.expand(content, iIndex);
                Buffer.BlockCopy(sms_address, 0, content, iIndex - sms_address.Length, sms_address.Length);
            }
            // 重新打包 total_length
            total_length = (ushort)iIndex;
            b = BitConverter.GetBytes(total_length);
            Buffer.BlockCopy(b, 0, content, 0, b.Length);
        }
    }
    /// <summary>
    /// 0xBB00 Sim卡锁卡、解锁操作相关命令。
    /// </summary>
    public class TX300_BB00 : TX300
    {
        public TX300_BB00()
        {
            CommandID = 0xBB00;
        }
        /// <summary>
        /// 锁卡状态。
        /// </summary>
        private byte lock_status;
        /// <summary>
        /// 解锁密码。
        /// </summary>
        private ushort password;
        /// <summary>
        /// 设置锁卡状态。
        /// </summary>
        public byte LockStatus
        {
            set { lock_status = value; }
        }
        /// <summary>
        /// 设置解锁密码，4位数字，超过4位时取最左边的4位数字。
        /// </summary>
        public string PassWrod
        {
            set 
            {
                string pw = value;
                if (pw.Length > 4)
                    pw = pw.Substring(0, 4);
                try
                {
                    password = ushort.Parse(pw);
                }
                catch
                {
                    throw new Exception("您的密码不是有效的数字格式。");
                }
            }
        }
        /// <summary>
        /// 将 0xBB00 命令中的各个字段按照 TX300 协议打包。
        /// </summary>
        public override void msg_to_package()
        {
            base.msg_to_package();
            // 组包 lock_status
            iIndex += Marshal.SizeOf(lock_status);
            content = ProtocolItems.expand(content, iIndex);
            content[iIndex - 1] = lock_status;
            // 组包 password
            if ((lock_status == SimLockStatus.status_ask) || (lock_status == SimLockStatus.status_unlock))
                password = 0;
            byte[] b = BitConverter.GetBytes(password);
            iIndex += b.Length;
            content = ProtocolItems.expand(content, iIndex);
            Buffer.BlockCopy(b, 0, content, iIndex - b.Length, b.Length);
            // 组包 total_length
            total_length = (ushort)iIndex;
            b = BitConverter.GetBytes(total_length);
            Buffer.BlockCopy(b, 0, content, 0, b.Length);
        }
    }
    /// <summary>
    /// 0xDD00 全局设置相关命令。
    /// </summary>
    public class TX300_DD00 : TX300
    {
        public TX300_DD00()
        {
            CommandID = 0xDD00;
        }
        /// <summary>
        /// 命令状态。0x00时为发送命令询问这些设置；0xFF时为发送命令更改这些设置。
        /// </summary>
        private byte msg_status;
        /// <summary>
        /// 终端F/W版本号码，只在终端回复服务器的命令中有本子段。
        /// </summary>
        //private string fw_version;
        /// <summary>
        /// 当前信号强度，只在终端回复服务器的命令中有本子段。
        /// </summary>
        //private byte csq;
        /// <summary>
        /// 定期报告时间间隔，单位分钟。
        /// </summary>
        private ushort inteval_period;
        /// <summary>
        /// SMS-GPRS转换时间间隔（好像不用了）。
        /// </summary>
        private ushort inteval_sms_gprs;
        /// <summary>
        /// 最低先好强度限制。
        /// </summary>
        private byte limit_csq;
        /// <summary>
        /// 最低后备电池电压限制。
        /// </summary>
        private byte limit_battery;
        /// <summary>
        /// 最高时速限制。
        /// </summary>
        private byte limit_speed;
        /// <summary>
        /// 越界半径。
        /// </summary>
        private byte limit_radius;
        /// <summary>
        /// 越界中心点经度。
        /// </summary>
        private uint limit_longitude;
        /// <summary>
        /// 越界中心点纬度。
        /// </summary>
        private uint limit_latitude;
        /// <summary>
        /// 定期报告设定，可以设定GPRS、SMS、Satellite模式下是否允许定期报告。
        /// </summary>
        private byte limit_period;
        /// <summary>
        /// 服务器开放的端口。
        /// </summary>
        private ushort server_port;
        /// <summary>
        /// 服务器地址。
        /// </summary>
        private byte[] server_address = new byte[TX300Items.server_address_length];
        /// <summary>
        /// SMS服务器地址。
        /// </summary>
        private byte[] sms_address = new byte[TX300Items.sms_server_length];
        /// <summary>
        /// 终端系统时间。
        /// </summary>
        private WbsDateTime.WbsDateTime wdt;
        /// <summary>
        /// 获取或设置信息状态。
        /// </summary>
        public byte MsgStatus
        {
            get { return msg_status; }
            set { msg_status = value; }
        }
        /// <summary>
        /// 获取终端FW版本，只在终端回复服务器的信息包中有。
        /// </summary>
        /*public string FW_Version
        {
            get { return fw_version; }
            //set { fw_version = value; }
        }
        /// <summary>
        /// 获取当前信号强度。
        /// </summary>
        public byte CSQ
        {
            get { return csq; }
        }*/
        /// <summary>
        /// 获取或设置定期报告时间间隔。
        /// </summary>
        public ushort IntevalPeriod
        {
            get { return inteval_period; }
            set { inteval_period = value; }
        }
        /// <summary>
        /// 获取或设置SMS-GPRS转换的时间间隔。
        /// </summary>
        public ushort IntevalSmsGprs
        {
            get { return inteval_sms_gprs; }
            set { inteval_sms_gprs = value; }
        }
        /// <summary>
        /// 获取或设置最低信号强度限制。
        /// </summary>
        public byte LimitCSQ
        {
            get { return limit_csq; }
            set { limit_csq = value; }
        }
        /// <summary>
        /// 获取或设置最低电压限制。
        /// </summary>
        public byte LimitBattery
        {
            get { return limit_battery; }
            set { limit_battery = value; }
        }
        /// <summary>
        /// 获取或设置最高时速限制。
        /// </summary>
        public byte LimitSpeed
        {
            get { return limit_speed; }
            set { limit_speed = value; }
        }
        /// <summary>
        /// 获取或设置越界半径。
        /// </summary>
        public byte LimitRadius
        {
            get { return limit_radius; }
            set { limit_radius = value; }
        }
        /// <summary>
        /// 获取或设置越界中心点经度。
        /// </summary>
        public uint LimitLongitude
        {
            get { return limit_longitude; }
            set { limit_longitude = value; }
        }
        /// <summary>
        /// 获取或设置越界中心点纬度。
        /// </summary>
        public uint LimitLatitude
        {
            get { return limit_latitude; }
            set { limit_latitude = value; }
        }
        /// <summary>
        /// 获取或设置定期报告设定。
        /// </summary>
        public byte LimitPeriod
        {
            get { return limit_period; }
            set { limit_period = value; }
        }
        /// <summary>
        /// 获取或设置服务器端口号吗。
        /// </summary>
        public ushort ServerPort
        {
            get { return server_port; }
            set { server_port = value; }
        }
        /// <summary>
        /// 获取或设置服务器IP地址。
        /// </summary>
        public string ServerIP
        {
            get
            {
                string ip = "";
                for (int i = 0; i < server_address.Length; i++)
                {
                    ip += server_address[i].ToString() + ".";
                }
                // 返回去掉最后的.的IP地址。
                return ip.Substring(0, ip.Length - 1); ;
            }
            set
            {
                string[] s = value.Split(new char[] { '.' });
                server_address = new byte[TX300Items.server_address_length];
                for (int i = 0; i < s.Length; i++)
                {
                    server_address[i] = byte.Parse(s[i]);
                }
            }
        }
        /// <summary>
        /// 获取或设置SMS服务器地址。
        /// </summary>
        public string SmsAddress
        {
            get { return ProtocolItems.GetHex(sms_address); }
            set 
            {
                if (value.Length % 2 != 0)
                    value = "0" + value;
                byte[] b = ProtocolItems.GetBytes(value);
                sms_address = new byte[b.Length + 1];
                Buffer.BlockCopy(b, 0, sms_address, 1, b.Length);
                sms_address[0] = (byte)b.Length;
            }
        }
        /// <summary>
        /// 获取或设置终端系统时间，只在终端回复的数据中有。
        /// </summary>
        public DateTime TerminalTime
        {
            get { return wdt.ByteToDateTime; }
            set { wdt = new WbsDateTime.WbsDateTime(value); }
        }
        /// <summary>
        /// 将服务器下发的命令打包。
        /// </summary>
        public override void msg_to_package()
        {
            base.msg_to_package();
            byte[] b = null;
            // 组包 msg_status
            iIndex += Marshal.SizeOf(msg_status);
            content = ProtocolItems.expand(content, iIndex);
            content[iIndex - Marshal.SizeOf(msg_status)] = msg_status;
            // 如果命令的类型是更改终端设置则需要组包其余的字段值。
            if (msg_status == 0xFF)
            { 
                // 组包 inteval_period
                b = BitConverter.GetBytes(inteval_period);
                iIndex += b.Length;
                content = ProtocolItems.expand(content, iIndex);
                Buffer.BlockCopy(b, 0, content, iIndex - b.Length, b.Length);
                // 组包 inteval_sms_gprs
                b = BitConverter.GetBytes(inteval_sms_gprs);
                iIndex += b.Length;
                content = ProtocolItems.expand(content, iIndex);
                Buffer.BlockCopy(b, 0, content, iIndex - b.Length, b.Length);
                // 组包 limit_csq
                iIndex += Marshal.SizeOf(limit_csq);
                content = ProtocolItems.expand(content, iIndex);
                content[iIndex - 1] = limit_csq;
                // 组包 limit_battery
                iIndex += Marshal.SizeOf(limit_battery);
                content = ProtocolItems.expand(content, iIndex);
                content[iIndex - 1] = limit_battery;
                // 组包 limit_speed
                iIndex += Marshal.SizeOf(limit_speed);
                content = ProtocolItems.expand(content, iIndex);
                content[iIndex - 1] = limit_speed;
                // 组包 limit_radius
                b = BitConverter.GetBytes(limit_radius);
                iIndex += b.Length;
                content = ProtocolItems.expand(content, iIndex);
                Buffer.BlockCopy(b, 0, content, iIndex - b.Length, b.Length);
                // 组包 limit_longitude
                b = BitConverter.GetBytes(limit_longitude);
                iIndex += b.Length;
                content = ProtocolItems.expand(content, iIndex);
                Buffer.BlockCopy(b, 0, content, iIndex - b.Length, b.Length);
                // 组包 limit_latitude
                b = BitConverter.GetBytes(limit_latitude);
                iIndex += b.Length;
                content = ProtocolItems.expand(content, iIndex);
                Buffer.BlockCopy(b, 0, content, iIndex - b.Length, b.Length);
                // 组包 limit_period
                iIndex += Marshal.SizeOf(limit_period);
                content = ProtocolItems.expand(content, iIndex);
                content[iIndex - 1] = limit_period;
                // 组包 server_port
                b = BitConverter.GetBytes(server_port);
                iIndex += b.Length;
                content = ProtocolItems.expand(content, iIndex);
                Buffer.BlockCopy(b, 0, content, iIndex - b.Length, b.Length);
                // 组包 server_address
                iIndex += server_address.Length;
                content = ProtocolItems.expand(content, iIndex);
                Buffer.BlockCopy(server_address, 0, content, iIndex - server_address.Length, server_address.Length);
                // 组包 sms_address
                iIndex += sms_address.Length;
                content = ProtocolItems.expand(content, iIndex);
                Buffer.BlockCopy(sms_address, 0, content, iIndex - sms_address.Length, sms_address.Length);
            }
            // 重新打包 total_length
            total_length = (ushort)iIndex;
            b = BitConverter.GetBytes(total_length);
            Buffer.BlockCopy(b, 0, content, 0, b.Length);
        }
        /// <summary>
        /// 将终端发送回来的数据解析成个字段。
        /// </summary>
        /// <returns></returns>
        public override bool package_to_msg()
        {
            return base.package_to_msg();
            // 解包 
        }
    }
    /// <summary>
    /// 0xDD01 全局设置相关命令
    /// </summary>
    public class TX300_DD01 : TX300
    {
        /// <summary>
        /// M days
        /// </summary>
        byte days;
        /// <summary>
        /// Manual unlock password
        /// </summary>
        uint pwd = 1234;
        public TX300_DD01()
        {
            CommandID = 0xDD01;
        }
        /// <summary>
        /// 设置或获取 M 天数。
        /// </summary>
        public byte MDays
        {
            get { return days; }
            set { days = value; }
        }
        /// <summary>
        /// 设置或获取解锁密码，如果设定的密码大于 99999999 则密码将会被直接置为 0。
        /// </summary>
        public uint Password
        {
            get { return pwd; }
            set 
            {
                if (value > 99999999)
                    pwd = 0;
                else
                    pwd = value;
            }
        }
        /// <summary>
        /// 将 0xDD01 的各个字段按照 TX300 协议打包
        /// </summary>
        public override void msg_to_package()
        {
            base.msg_to_package();
            // 组包 lock_status
            iIndex += Marshal.SizeOf(days);
            content = ProtocolItems.expand(content, iIndex);
            content[iIndex - 1] = days;
            // 组包 password
            byte[] b = BitConverter.GetBytes(pwd);
            iIndex += b.Length;
            content = ProtocolItems.expand(content, iIndex);
            Buffer.BlockCopy(b, 0, content, iIndex - b.Length, b.Length);
            // 组包 total_length
            total_length = (ushort)iIndex;
            b = BitConverter.GetBytes(total_length);
            Buffer.BlockCopy(b, 0, content, 0, b.Length);
        }
        /// <summary>
        /// 将 0xDD01 的各个字段从数据中解析出来
        /// </summary>
        /// <returns></returns>
        public override bool package_to_msg()
        {
            base.package_to_msg();
            // 解包 days
            days = content[iIndex];
            iIndex += Marshal.SizeOf(days);
            // 解包 pwd
            pwd = BitConverter.ToUInt32(content, iIndex);
            iIndex += Marshal.SizeOf(pwd);
            // 解包 checksum
            iIndex += 1;
            return iIndex == total_length;
        }
    }
}

