using System;
using System.Text;

using Wbs.Protocol.Items;
using Wbs.Protocol.TX300;
using Wbs.Protocol.WbsDateTime;

namespace Wbs.Protocol.TX300
{
    /// <summary>
    /// 各个字段的数据类型。
    /// </summary>
    public enum TX300DataTyeps
    {
        /// <summary>
        /// 表示本字段内容为 byte 型。
        /// </summary>
        BYTE = 1,
        /// <summary>
        /// 表示本字段内容为十进制数字型。
        /// </summary>
        UBYTE,
        /// <summary>
        /// 表示本字段为 0xEE00 中错误类别型。
        /// </summary>
        ERROR_TYPE,
        /// <summary>
        /// 表示本字段内容为 ushort 型。
        /// </summary>
        USHORT,
        /// <summary>
        /// 表示本字段内容为 uint 型。
        /// </summary>
        UINT,
        /// <summary>
        /// 表示本字段内容为 int 型，带正负。
        /// </summary>
        INT,
        /// <summary>
        /// 表示本字段内容为 string 型，需要转换成 string 输出。
        /// </summary>
        STRING,
        /// <summary>
        /// 表示本字段内容为 datetime 型，需要转换成 TX300 协议中的时间格式再显示。
        /// </summary>
        DATETIME,
        /// <summary>
        /// 表示本字段内容为 IP 形，需要将其转换为 IP 的字符串形式再显示。
        /// </summary>
        IP,
        /// <summary>
        /// 表示本字段内容为电压值，需要除以 10 得到正确值。
        /// </summary>
        VOLTAGE,
        /// <summary>
        /// 表示本字段内容命令状态。
        /// </summary>
        STATUS,
        /// <summary>
        /// 表示本字段内容为经纬度表示方式。
        /// </summary>
        POSITION,
        /// <summary>
        /// 表示本字段内容需要转换成十进制之后再除以 100。
        /// </summary>
        DIV100,
        /// <summary>
        /// 表示本字段内容为保安命令。
        /// </summary>
        SECURITY_SINGAL,
        /// <summary>
        /// 表示本字段内容为远程控制状态。
        /// </summary>
        CONTROL,
        /// <summary>
        /// 表示本字段内容为连接原因。
        /// </summary>
        CONNECT_STATUS,
        /// <summary>
        /// 表示本字段内容为远程重置状态。
        /// </summary>
        RESET,
        /// <summary>
        /// 表示本字段内容为报警状态。
        /// </summary>
        ALARM,
        /// <summary>
        /// 表示本字段内容为 EPOS 相关的故障代码。
        /// </summary>
        EPOS_TRUBLE,
        /// <summary>
        /// 表示本字段为 EPOS 的报警代码。
        /// </summary>
        EPOS_ALARM,
        /// <summary>
        /// 表示本字段为 EPOS 总运转时间。
        /// </summary>
        EPOS_RUNTIME,
        /// <summary>
        /// 表示本字段为 EPOS 仪表盘信息中的 PUMP 信息，需要进行转换。
        /// </summary>
        EPOS_EMD_PUMP,
        /// <summary>
        /// 表示本字段为 EPOS 仪表盘信息中的水温信息，需要进行转换。
        /// </summary>
        EPOS_EMD_WATER_TEMP,
        /// <summary>
        /// 表示本字段为 EPOS 仪表盘信息中的油温信息，需要进行转换。
        /// </summary>
        EPOS_EMD_OIL_TEMP,
        /// <summary>
        /// 表示本字段为 EPOS 命令中终端发送给 EPOS 的状态。
        /// </summary>
        EPOS_STATUS,
        /// <summary>
        /// 表示本字段为 0xDD00 中的服务器 SMS 地址。
        /// </summary>
        SMS_SERVER_ADDRESS,
        /// <summary>
        /// 表示本字段为 Sim 卡锁卡命令码。
        /// </summary>
        SIM_LOCK_STATUS,
        /// <summary>
        /// 表示本字段为 EE00 命令中的参数内容字段。
        /// </summary>
        EE00_PARAM,
        /// <summary>
        /// 表示 Loader 运转时间中的时间。
        /// </summary>
        LD_RUN_DATETIME,
        /// <summary>
        /// 表示本字段为反馈包中的命令字。
        /// </summary>
        RESP_COMMAND,
        /// <summary>
        /// 表示本字段为反馈包的状态码。
        /// </summary>
        RESP_STATUS,
        /// <summary>
        /// 表示本字段为故障报告中的故障数量。
        /// </summary>
        EPOS_ETF_ERR_COUNT,
        /// <summary>
        /// 表示本字段为故障报告中的故障代码。
        /// </summary>
        EPOS_ERR_CODE,
        /// <summary>
        /// 表示本字段为故障报告中故障的FMI码。
        /// </summary>
        EPOS_ERR_FMI,
        /// <summary>
        /// 表示本子段为 Monitor 数据中的输入信号
        /// </summary>
        EPOS_MONITOR_INPUT,
        /// <summary>
        /// 表示本子段为 Monitor 数据中的工作模式信息
        /// </summary>
        EPOS_MONITOR_MODULE,
        /// <summary>
        /// 表示本子段为 Monitor 数据中的输出信号
        /// </summary>
        EPOS_MONITOR_OUTPUT,
        /// <summary>
        /// 表示本子段为 Monitor 数据中的先导灯输出信号
        /// </summary>
        EPOS_MONITOR_PILOT,
        /// <summary>
        /// 表示本子段为 Monitor 数据中的发动机输出控制信号
        /// </summary>
        EPOS_MONITOR_ENG_CONTROL,
        /// <summary>
        /// 表示本子段为 Monitor 数据中的比例阀电流
        /// </summary>
        EPOS_MONITOR_EEPRC,
        /// <summary>
        /// 表示本子段为 Monitor 数据中的 TPS 电压
        /// </summary>
        EPOS_MONITOR_TPS,
        /// <summary>
        /// 表示本子段为 Monitor 数据中的油门旋钮电压
        /// </summary>
        EPOS_MONITOR_DIALVOL,
        /// <summary>
        /// 表示本子段为 Monitor 数据中的变速箱
        /// </summary>
        EPOS_MONITOR_TMPPRE,
        /// <summary>
        /// 表示本字段为 Monitor 数据中的大臂油缸
        /// </summary>
        EPOS_MONITOR_BOOMPRE,
        /// <summary>
        /// 表示本字段为 Monitor 数据中的蓄电池电压
        /// </summary>
        EPOS_MONITOR_BATTERY_VOLTAGE,
        /// <summary>
        /// 表示本字段为 Monitor 数据中的燃油剩余量
        /// </summary>
        EPOS_MONITOR_OIL_LEFT,
        /// <summary>
        /// 表示本字段为 Status report 中的滤清器警报信息
        /// </summary>
        EPOS_STATUS_FILTER,
        /// <summary>
        /// 盲区手动解锁密码
        /// </summary>
        BLIND_UNLOCK_PASSWORD,
        /// <summary>
        /// 表示本字段为 TX10G 终端的报警信息
        /// </summary>
        TX10G_ALARMS,
        /// <summary>
        /// 表示本字段为 TX10G 终端的定位信息
        /// </summary>
        TX10G_GPS,
        /// <summary>
        /// 表示本字段为 TX10G 终端定位信息中的定位信息数量
        /// </summary>
        TX10G_GPS_COUNT
    }

    /// <summary>
    /// 定位信息类别
    /// </summary>
    public enum PositionType
    {
        /// <summary>
        /// 报警信息中的GPS信息
        /// </summary>
        Alarm = 0,
        /// <summary>
        /// 命令回复中的GPS信息
        /// </summary>
        Command,
        /// <summary>
        /// 发动机开关机中的GPS信息
        /// </summary>
        EngControl,
        /// <summary>
        /// 定期报告中的GPS信息
        /// </summary>
        Period,
        /// <summary>
        /// 终端关机信息中的GPS信息
        /// </summary>
        Shutdown,
        /// <summary>
        /// 未知，暂时未定义的
        /// </summary>
        Unknown
    }

    /// <summary>
    /// 本字段所属命令的传输方向。
    /// </summary>
    public enum TX300CommandDirect
    { 
        /// <summary>
        /// S-T：表示本字段所属的命令是由服务器发往终端的。
        /// </summary>
        SERVER,
        /// <summary>
        /// T-S：表示本字段所属的命令是由终端发往服务器的。
        /// </summary>
        CLIENT
    }

    public enum doosan
    { 
        /// <summary>
        /// 表示本命令是 DH 类型终端发送的信息。
        /// </summary>
        DH,
        /// <summary>
        /// 表示本命令是 DX 类型终端发送的信息。
        /// </summary>
        DX,
        /// <summary>
        /// 表示本命令是 Loader 类型终端发送的信息。
        /// </summary>
        Loader,
        /// <summary>
        /// 表示本命令是 TX10G 类型终端发送的信息。
        /// </summary>
        TX10G,
        /// <summary>
        /// 表示本命令是各型号终端通用的信息。
        /// </summary>
        ALL
    }

    public enum TX300CommandTypes
    { 
        /// <summary>
        /// 表示本命令是 TX 类型终端适用，包括 TX100 和 TX300。
        /// </summary>
        TX,
        /// <summary>
        /// 表示本命令是 TX10G 类型终端适用。
        /// </summary>
        TX10G,
        /// <summary>
        /// 表示本命令是所有终端都适用。
        /// </summary>
        ALL
    }

    /// <summary>
    /// 各字段基本属性。
    /// </summary>
    public struct TX300CommandFiled
    {
        /// <summary>
        /// 字段所属命令码。
        /// </summary>
        public ushort command_id;
        /// <summary>
        /// 字段的描述。
        /// </summary>
        public string filed_name;
        /// <summary>
        /// 字段内容长度。
        /// </summary>
        public byte filed_len;
        /// <summary>
        /// 本字段的数据类型。
        /// </summary>
        public TX300DataTyeps filed_type;
        /// <summary>
        /// 本命令的传输方向。
        /// </summary>
        public TX300CommandDirect direction;
        /// <summary>
        /// 本命令的类型（DH或DX）。
        /// </summary>
        public doosan filed_style;
        /// <summary>
        /// 本命令的备注。
        /// </summary>
        public string memo;
        /// <summary>
        /// 本字段在分析界面中是否要特别醒目的显示。
        /// </summary>
        public bool special;
        /// <summary>
        /// 构造一个新的字段属性描述结构体。
        /// </summary>
        /// <param name="cmd">所属命令。</param>
        /// <param name="name">字段的描述。</param>
        /// <param name="len">字段的长度。</param>
        public TX300CommandFiled(ushort cmd, string name, byte len, TX300DataTyeps type, TX300CommandDirect dir, doosan style, string mem, bool speci)
        {
            command_id = cmd;
            filed_name = name;
            filed_len = len;
            filed_type = type;
            direction = dir;
            filed_style = style;
            memo = mem;
            special = speci;
        }
    }
    /// <summary>
    /// TX300 命令大全。
    /// </summary>
    public struct TX300Command
    {
        /// <summary>
        /// 命令名称。
        /// </summary>
        public string command_name;
        /// <summary>
        /// 命令代码。
        /// </summary>
        public ushort command_id;
        /// <summary>
        /// 命令类别。
        /// </summary>
        public TX300CommandTypes command_type;
        /// <summary>
        /// 构造一个新的 TX300 命令结构体。
        /// </summary>
        /// <param name="name">命令名称。</param>
        /// <param name="id">命令码。</param>
        public TX300Command(string name, ushort id, TX300CommandTypes ctype)
        {
            command_name = name;
            command_id = id;
            command_type = ctype;
        }
    }

    /// <summary>
    /// TX300 协议各命令各字段定义
    /// </summary>
    public class TX300Fileds
    {
        /// <summary>
        /// 要发送给终端的命令。
        /// </summary>
        public static TX300Command[] commands = new TX300Command[] 
        {
            new TX300Command("定位信息(0x1000)", 0x1000, TX300CommandTypes.TX),
            new TX300Command("新版定位信息(0x1001)", 0x1001, TX300CommandTypes.TX),
            new TX300Command("Loader 远程控制(0x3000)", 0x3000, TX300CommandTypes.TX),
            new TX300Command("远程重置终端连接(0x4000)", 0x4000, TX300CommandTypes.TX),
            new TX300Command("开关机信息(0x5000)", 0x5000, TX300CommandTypes.TX),
            new TX300Command("仪表盘信息(0x6000)", 0x6000, TX300CommandTypes.TX),
            new TX300Command("故障标记(0x6001)", 0x6001, TX300CommandTypes.TX),
            new TX300Command("图形分析数据(0x6002)", 0x6002, TX300CommandTypes.TX),
            new TX300Command("故障历史(0x6003)", 0x6003, TX300CommandTypes.TX),
            new TX300Command("启动时间(0x6004)", 0x6004, TX300CommandTypes.TX),
            new TX300Command("滤油信息(0x6005)", 0x6005, TX300CommandTypes.TX),
            new TX300Command("状态报告(0x6006)", 0x6006, TX300CommandTypes.TX),
            new TX300Command("保安命令(0x6007)", 0x6007, TX300CommandTypes.TX),
            new TX300Command("DX 机器信息(0x6008)", 0x6008, TX300CommandTypes.TX),
            new TX300Command("DX 工作时间(0x6009)", 0x6009, TX300CommandTypes.TX),
            new TX300Command("DX 默认信息(0x600A)", 0x600A, TX300CommandTypes.TX),
            new TX300Command("Loader设置运转时间(0x600B)", 0x600B, TX300CommandTypes.TX),
            new TX300Command("Loader每日运转时间(0x600C)", 0x600C, TX300CommandTypes.TX),
            new TX300Command("Sim 卡操作(0xBB00)", 0xBB00, TX300CommandTypes.TX),
            new TX300Command("全局设置(0xDD00)", 0xDD00, TX300CommandTypes.TX),
            new TX300Command("盲区设置(0xDD01)", 0xDD01, TX300CommandTypes.TX),
            new TX300Command("通讯参数设置(0x7010)", 0x7010, TX300CommandTypes.TX10G),
            new TX300Command("", 0x0000, TX300CommandTypes.ALL),
            new TX300Command("报警信息(0x2000)", 0x2000, TX300CommandTypes.TX),
            new TX300Command("TCP 心跳信息(0xAA00)", 0xAA00, TX300CommandTypes.TX),
            new TX300Command("终端连接信息(0xCC00)", 0xCC00, TX300CommandTypes.TX),
            new TX300Command("终端处理错误信息(0xEE00)", 0xEE00, TX300CommandTypes.TX),
            new TX300Command("终端电池耗尽关机信息(0xFF00)", 0xFF00, TX300CommandTypes.TX),
            new TX300Command("终端获取 Sim 卡号码(0xBB0F)", 0xBB0F, TX300CommandTypes.TX),
            new TX300Command("连接服务器(0x7000)", 0x7000, TX300CommandTypes.TX10G),
            new TX300Command("报警信息(0x7020)", 0x7020, TX300CommandTypes.TX10G),
            new TX300Command("定位信息(0x7030)", 0x7030, TX300CommandTypes.TX10G),
            new TX300Command("心跳信息(0x7040)", 0x7040, TX300CommandTypes.TX10G)
        };

        public static TX300CommandFiled[] fileds = new TX300CommandFiled[] 
        { 
            // 0xFFFF: TX 协议中接收方的反馈包结构
            new TX300CommandFiled(0xFFFF, "反馈包总长度", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.SERVER, doosan.ALL, "", false),
            new TX300CommandFiled(0xFFFF, "反馈包的命令字", 2, TX300DataTyeps.RESP_COMMAND, TX300CommandDirect.SERVER, doosan.ALL, "与上行的命令字相同", false),
            new TX300CommandFiled(0xFFFF, "反馈包的流水号", 2, TX300DataTyeps.UBYTE, TX300CommandDirect.SERVER, doosan.ALL, "与上行的流水号相同", false),
            new TX300CommandFiled(0xFFFF, "反馈包的状态码", 1, TX300DataTyeps.RESP_STATUS, TX300CommandDirect.SERVER, doosan.ALL, "", false),
            new TX300CommandFiled(0xFFFF, "反馈包的帧索引", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.SERVER, doosan.ALL, "与上行的帧索引相同", false),
            // 0xAA00: T-S
            new TX300CommandFiled(0xAA00, "信号强度", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.ALL, "", false),
            // 0xBB00: T-S
            new TX300CommandFiled(0xBB00, "终端F/W版本", 7, TX300DataTyeps.STRING, TX300CommandDirect.CLIENT, doosan.ALL, "", false),
            new TX300CommandFiled(0xBB00, "卡的锁定状态", 1, TX300DataTyeps.STRING, TX300CommandDirect.CLIENT, doosan.ALL, "", false),
            new TX300CommandFiled(0xBB00, "当前解锁密码", 2, TX300DataTyeps.USHORT, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            // 0xBB00: S-T
            new TX300CommandFiled(0xBB00, "命令操作类型", 1, TX300DataTyeps.SIM_LOCK_STATUS, TX300CommandDirect.SERVER, doosan.ALL, "", false),
            new TX300CommandFiled(0xBB00, "密码", 2, TX300DataTyeps.USHORT, TX300CommandDirect.SERVER, doosan.ALL, "", false),
            // 0xBB0F: S-T
            new TX300CommandFiled(0xBB0F, "Sim卡号码", 6, TX300DataTyeps.BYTE, TX300CommandDirect.SERVER, doosan.ALL, "", false),
            new TX300CommandFiled(0xBB0F, "解锁密码", 4, TX300DataTyeps.BLIND_UNLOCK_PASSWORD, TX300CommandDirect.SERVER, doosan.ALL, "", false),
            // 0xBB0F: T-S
            new TX300CommandFiled(0xBB0F, "ICCID号码", 20, TX300DataTyeps.STRING, TX300CommandDirect.CLIENT, doosan.ALL, "", false),
            // 0xCC00: T-S
            new TX300CommandFiled(0xCC00, "终端F/W版本", 7, TX300DataTyeps.STRING, TX300CommandDirect.CLIENT, doosan.ALL, "", false),
            new TX300CommandFiled(0xCC00, "移动基站号码", 4, TX300DataTyeps.STRING, TX300CommandDirect.CLIENT, doosan.ALL, "", false),
            new TX300CommandFiled(0xCC00, "连接状态", 1, TX300DataTyeps.CONNECT_STATUS, TX300CommandDirect.CLIENT, doosan.ALL, "00|0B|10|20|40为正常，其余非正常", true),
            new TX300CommandFiled(0xCC00, "总运转时间", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.ALL, "", false),
            new TX300CommandFiled(0xCC00, "终端系统时间", 4, TX300DataTyeps.DATETIME, TX300CommandDirect.CLIENT, doosan.ALL, "", false),
            // 0xCC00: S-T
            new TX300CommandFiled(0xCC00, "心跳时间间隔", 2, TX300DataTyeps.USHORT, TX300CommandDirect.SERVER, doosan.ALL, "单位：分", false),
            new TX300CommandFiled(0xCC00, "服务器系统时间", 4, TX300DataTyeps.DATETIME, TX300CommandDirect.SERVER, doosan.ALL, "", false),
            // 0xDD00: S-T
            new TX300CommandFiled(0xDD00, "命令类别", 1, TX300DataTyeps.STATUS, TX300CommandDirect.SERVER, doosan.ALL, "", false),
            new TX300CommandFiled(0xDD00, "定期报告时间间隔", 2, TX300DataTyeps.USHORT, TX300CommandDirect.SERVER, doosan.ALL, "单位：分", false),
            new TX300CommandFiled(0xDD00, "SMS-GPRS时间间隔", 2, TX300DataTyeps.USHORT, TX300CommandDirect.SERVER, doosan.ALL, "单位：分", false),
            new TX300CommandFiled(0xDD00, "信号强度最低限制", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.SERVER, doosan.ALL, "", false),
            new TX300CommandFiled(0xDD00, "后备电池低电压限制", 1, TX300DataTyeps.VOLTAGE, TX300CommandDirect.SERVER, doosan.ALL, "单位：V" , false),
            new TX300CommandFiled(0xDD00, "最大时速设定", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.SERVER, doosan.ALL, "单位：km/h" , false),
            new TX300CommandFiled(0xDD00, "越界半径", 2, TX300DataTyeps.USHORT, TX300CommandDirect.SERVER, doosan.ALL, "单位：米" , false),
            new TX300CommandFiled(0xDD00, "越界中心点经度", 4, TX300DataTyeps.POSITION, TX300CommandDirect.SERVER, doosan.ALL, "" , false),
            new TX300CommandFiled(0xDD00, "越界中心点纬度", 4, TX300DataTyeps.POSITION, TX300CommandDirect.SERVER, doosan.ALL, "" , false),
            new TX300CommandFiled(0xDD00, "定期报告设置", 1, TX300DataTyeps.BYTE, TX300CommandDirect.SERVER, doosan.ALL, "" , false),
            new TX300CommandFiled(0xDD00, "服务器端口", 2, TX300DataTyeps.USHORT, TX300CommandDirect.SERVER, doosan.ALL, "" , false),
            new TX300CommandFiled(0xDD00, "服务器地址", 4, TX300DataTyeps.IP, TX300CommandDirect.SERVER, doosan.ALL, "" , false),
            new TX300CommandFiled(0xDD00, "短信息服务器地址", 1, TX300DataTyeps.SMS_SERVER_ADDRESS, TX300CommandDirect.SERVER, doosan.ALL, "" , false),
            // 0xDD01: S-T
            new TX300CommandFiled(0xDD01, "设定盲区天数", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.SERVER, doosan.ALL, "进盲区后锁车", true),
            new TX300CommandFiled(0xDD01, "手动解锁密码", 4, TX300DataTyeps.BLIND_UNLOCK_PASSWORD, TX300CommandDirect.SERVER, doosan.ALL, "", true),
            // 0xDD01: T-S
            new TX300CommandFiled(0xDD01, "设定盲区天数", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.ALL, "进盲区后锁车", true),
            new TX300CommandFiled(0xDD01, "手动解锁密码", 4, TX300DataTyeps.BLIND_UNLOCK_PASSWORD, TX300CommandDirect.CLIENT, doosan.ALL, "", true),
            // 0xDD00: T-S
            new TX300CommandFiled(0xDD00, "命令类别", 1, TX300DataTyeps.STATUS, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0xDD00, "终端F/W版本", 7, TX300DataTyeps.STRING, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0xDD00, "当前信号强度", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.ALL, "" , true),
            new TX300CommandFiled(0xDD00, "定期报告时间间隔", 2, TX300DataTyeps.USHORT, TX300CommandDirect.CLIENT, doosan.ALL, "单位：分" , false),
            new TX300CommandFiled(0xDD00, "SMS-GPRS时间间隔", 2, TX300DataTyeps.USHORT, TX300CommandDirect.CLIENT, doosan.ALL, "单位：分" , false),
            new TX300CommandFiled(0xDD00, "信号强度最低限制", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.ALL, "" , true),
            new TX300CommandFiled(0xDD00, "后备电池低电压限制", 1, TX300DataTyeps.VOLTAGE, TX300CommandDirect.CLIENT, doosan.ALL, "单位：V" , true),
            new TX300CommandFiled(0xDD00, "最大时速设定", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.ALL, "单位：km/h" , false),
            new TX300CommandFiled(0xDD00, "越界半径", 2, TX300DataTyeps.USHORT, TX300CommandDirect.CLIENT, doosan.ALL, "单位：米" , false),
            new TX300CommandFiled(0xDD00, "越界中心点经度", 4, TX300DataTyeps.POSITION, TX300CommandDirect.CLIENT, doosan.ALL, "$GPRMC格式" , false),
            new TX300CommandFiled(0xDD00, "越界中心点纬度", 4, TX300DataTyeps.POSITION, TX300CommandDirect.CLIENT, doosan.ALL, "$GPRMC格式" , false),
            new TX300CommandFiled(0xDD00, "定期报告设置", 1, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0xDD00, "服务器端口", 2, TX300DataTyeps.USHORT, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0xDD00, "服务器地址", 4, TX300DataTyeps.IP, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0xDD00, "短信息服务器地址", 1, TX300DataTyeps.SMS_SERVER_ADDRESS, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0xDD00, "终端系统时间", 4, TX300DataTyeps.DATETIME, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            // 0xEE00
            new TX300CommandFiled(0xEE00, "终端F/W版本", 7, TX300DataTyeps.STRING, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0xEE00, "错误类型", 1, TX300DataTyeps.ERROR_TYPE, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0xEE00, "出错的命令码", 2, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0xEE00, "服务器命令参数长度", 1, TX300DataTyeps.EE00_PARAM, TX300CommandDirect.CLIENT, doosan.ALL, "字节数" , false),
            new TX300CommandFiled(0xEE00, "服务器命令参数内容", 1, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            // 0xFF00
            new TX300CommandFiled(0xFF00, "后备电池电压", 1, TX300DataTyeps.VOLTAGE, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0xFF00, "定位时间", 4, TX300DataTyeps.DATETIME, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0xFF00, "经度", 4, TX300DataTyeps.POSITION, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0xFF00, "东西指示", 1, TX300DataTyeps.STRING, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0xFF00, "纬度", 4, TX300DataTyeps.POSITION, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0xFF00, "南北指示", 1, TX300DataTyeps.STRING, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0xFF00, "当前速度", 2, TX300DataTyeps.DIV100, TX300CommandDirect.CLIENT, doosan.ALL, "单位：km/h" , false),
            new TX300CommandFiled(0xFF00, "行进方向", 2, TX300DataTyeps.DIV100, TX300CommandDirect.CLIENT, doosan.ALL, "单位：度(正北为0°)" , false),
            new TX300CommandFiled(0xFF00, "海拔高度", 4, TX300DataTyeps.DIV100, TX300CommandDirect.CLIENT, doosan.ALL, "单位：米" , false),
            // 0x1000: S-T
            new TX300CommandFiled(0x1000, "需求数量", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.SERVER, doosan.ALL, "条" , false),
            // 0x1000: T-S
            new TX300CommandFiled(0x1000, "信息类别", 1, TX300DataTyeps.STATUS, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x1000, "条数索引", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.ALL, "====================" , false),
            new TX300CommandFiled(0x1000, "定位时间", 4, TX300DataTyeps.DATETIME, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x1000, "经度", 4, TX300DataTyeps.POSITION, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x1000, "东西指示", 1, TX300DataTyeps.STRING, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x1000, "纬度", 4, TX300DataTyeps.POSITION, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x1000, "南北指示", 1, TX300DataTyeps.STRING, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x1000, "当前速度", 2, TX300DataTyeps.DIV100, TX300CommandDirect.CLIENT, doosan.ALL, "单位：km/h" , false),
            new TX300CommandFiled(0x1000, "行进方向", 2, TX300DataTyeps.DIV100, TX300CommandDirect.CLIENT, doosan.ALL, "单位：度(正北为0°)" , false),
            new TX300CommandFiled(0x1000, "海拔高度", 4, TX300DataTyeps.DIV100, TX300CommandDirect.CLIENT, doosan.ALL, "单位：米" , false),
            // 0x1001: T-S
            new TX300CommandFiled(0x1001, "信息类别", 1, TX300DataTyeps.STATUS, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x1001, "定位时间", 4, TX300DataTyeps.DATETIME, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x1001, "信号强度", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x1001, "误码率", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x1001, "纬度", 4, TX300DataTyeps.POSITION, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x1001, "经度", 4, TX300DataTyeps.POSITION, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x1001, "信号强度", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x1001, "误码率", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x1001, "纬度", 4, TX300DataTyeps.POSITION, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x1001, "经度", 4, TX300DataTyeps.POSITION, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x1001, "终端F/W版本", 7, TX300DataTyeps.STRING, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x1001, "版本区别码", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x1001, "基站号码", 2, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x1001, "小区号码", 2, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x1001, "总运转时间", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x1001, "燃油剩余量", 2, TX300DataTyeps.USHORT, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x1001, "剩余数据长度", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            //new fileds(0x1000, "海拔高度", 4, type.DIV100, direct.CLIENT, doosan.ALL, "单位：米" , false),
            //new fileds(0x1000, "海拔高度", 4, type.DIV100, direct.CLIENT, doosan.ALL, "单位：米" , false),
            //new fileds(0x1000, "海拔高度", 4, type.DIV100, direct.CLIENT, doosan.ALL, "单位：米" , false),
            // 0x2000: T-S
            new TX300CommandFiled(0x2000, "报警信息", 2, TX300DataTyeps.ALARM, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x2000, "定位时间", 4, TX300DataTyeps.DATETIME, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x2000, "经度", 4, TX300DataTyeps.POSITION, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x2000, "东西指示", 1, TX300DataTyeps.STRING, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x2000, "纬度", 4, TX300DataTyeps.POSITION, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x2000, "南北指示", 1, TX300DataTyeps.STRING, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x2000, "当前速度", 2, TX300DataTyeps.DIV100, TX300CommandDirect.CLIENT, doosan.ALL, "单位：km/h" , false),
            new TX300CommandFiled(0x2000, "当前行进方向", 2, TX300DataTyeps.DIV100, TX300CommandDirect.CLIENT, doosan.ALL, "单位：度(正北为0°)" , false),
            new TX300CommandFiled(0x2000, "海拔高度", 4, TX300DataTyeps.DIV100, TX300CommandDirect.CLIENT, doosan.ALL, "单位：米" , false),
            
            // 0x3000: S-T
            new TX300CommandFiled(0x3000, "远程控制状态", 1, TX300DataTyeps.CONTROL, TX300CommandDirect.SERVER, doosan.ALL, "" , false),
            new TX300CommandFiled(0x3000, "控制动作时间", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.SERVER, doosan.ALL, "在此时间后控制，单位：分" , false),
            // 0x3000: T-S
            new TX300CommandFiled(0x3000, "远程控制状态", 1, TX300DataTyeps.CONTROL, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x3000, "定位时间", 4, TX300DataTyeps.DATETIME, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x3000, "经度", 4, TX300DataTyeps.POSITION, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x3000, "东西指示", 1, TX300DataTyeps.STRING, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x3000, "纬度", 4, TX300DataTyeps.POSITION, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x3000, "南北指示", 1, TX300DataTyeps.STRING, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x3000, "当前速度", 2, TX300DataTyeps.DIV100, TX300CommandDirect.CLIENT, doosan.ALL, "km/h" , false),
            new TX300CommandFiled(0x3000, "当前行进方向", 2, TX300DataTyeps.DIV100, TX300CommandDirect.CLIENT, doosan.ALL, "单位：度(正北为0°)" , false),
            new TX300CommandFiled(0x3000, "海拔高度", 4, TX300DataTyeps.DIV100, TX300CommandDirect.CLIENT, doosan.ALL, "meter" , false),
            
            // 0x4000: S-T
            new TX300CommandFiled(0x4000, "重置状态", 1, TX300DataTyeps.RESET, TX300CommandDirect.SERVER, doosan.ALL, "" , false),
            new TX300CommandFiled(0x4000, "重置动作时间", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.SERVER, doosan.ALL, "在此时间后重置，单位：分" , false),
            // 0x5000: T-S
            new TX300CommandFiled(0x5000, "发电机电压", 2, TX300DataTyeps.VOLTAGE, TX300CommandDirect.CLIENT, doosan.ALL, "V" , true),
            new TX300CommandFiled(0x5000, "后备电池电压", 1, TX300DataTyeps.VOLTAGE, TX300CommandDirect.CLIENT, doosan.ALL, "V" , true),
            new TX300CommandFiled(0x5000, "定位时间", 4, TX300DataTyeps.DATETIME, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x5000, "经度", 4, TX300DataTyeps.POSITION, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x5000, "东西指示", 1, TX300DataTyeps.STRING, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x5000, "纬度", 4, TX300DataTyeps.POSITION, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x5000, "南北指示", 1, TX300DataTyeps.STRING, TX300CommandDirect.CLIENT, doosan.ALL, "" , false),
            new TX300CommandFiled(0x5000, "当前速度", 2, TX300DataTyeps.DIV100, TX300CommandDirect.CLIENT, doosan.ALL, "km/h" , false),
            new TX300CommandFiled(0x5000, "当前行进方向", 2, TX300DataTyeps.DIV100, TX300CommandDirect.CLIENT, doosan.ALL, "单位：度(正北为0°)" , false),
            new TX300CommandFiled(0x5000, "海拔高度", 4, TX300DataTyeps.DIV100, TX300CommandDirect.CLIENT, doosan.ALL, "meter" , false),
            new TX300CommandFiled(0x5000, "总运转时间", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.ALL, "meter" , false),
            // 0x5000: T-S(LD)
            new TX300CommandFiled(0x5000, "总运转时间：小时数", 2, TX300DataTyeps.USHORT, TX300CommandDirect.CLIENT, doosan.Loader, "" , false),
            new TX300CommandFiled(0x5000, "总运转时间：分钟数", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.Loader, "" , false),
            new TX300CommandFiled(0x5000, "开/关标记", 1, TX300DataTyeps.STATUS, TX300CommandDirect.CLIENT, doosan.Loader, "" , false),
            new TX300CommandFiled(0x5000, "发电机电压", 2, TX300DataTyeps.VOLTAGE, TX300CommandDirect.CLIENT, doosan.Loader, "V" , false),
            new TX300CommandFiled(0x5000, "蓄电池电压", 1, TX300DataTyeps.VOLTAGE, TX300CommandDirect.CLIENT, doosan.Loader, "V" , false),
            new TX300CommandFiled(0x5000, "燃油剩余量", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.Loader, "" , false),
            new TX300CommandFiled(0x5000, "冷却水温", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.Loader, "" , false),
            new TX300CommandFiled(0x5000, "定位时间", 4, TX300DataTyeps.DATETIME, TX300CommandDirect.CLIENT, doosan.Loader, "" , false),
            new TX300CommandFiled(0x5000, "经度", 4, TX300DataTyeps.POSITION, TX300CommandDirect.CLIENT, doosan.Loader, "" , false),
            new TX300CommandFiled(0x5000, "东西指示", 1, TX300DataTyeps.STRING, TX300CommandDirect.CLIENT, doosan.Loader, "" , false),
            new TX300CommandFiled(0x5000, "纬度", 4, TX300DataTyeps.POSITION, TX300CommandDirect.CLIENT, doosan.Loader, "" , false),
            new TX300CommandFiled(0x5000, "南北指示", 1, TX300DataTyeps.STRING, TX300CommandDirect.CLIENT, doosan.Loader, "" , false),
            new TX300CommandFiled(0x5000, "当前速度", 2, TX300DataTyeps.DIV100, TX300CommandDirect.CLIENT, doosan.Loader, "km/h" , false),
            new TX300CommandFiled(0x5000, "当前行进方向", 2, TX300DataTyeps.DIV100, TX300CommandDirect.CLIENT, doosan.Loader, "单位：度(正北为0°)" , false),
            new TX300CommandFiled(0x5000, "海拔高度", 4, TX300DataTyeps.DIV100, TX300CommandDirect.CLIENT, doosan.Loader, "meter" , false),
            // 0x6000(DH)
            new TX300CommandFiled(0x6000, "T-E状态", 1, TX300DataTyeps.EPOS_STATUS, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6000, "转速", 2, TX300DataTyeps.USHORT, TX300CommandDirect.CLIENT, doosan.DH, "rpm(1100～2100)" , false),
            new TX300CommandFiled(0x6000, "蓄电池电压", 2, TX300DataTyeps.EPOS_MONITOR_BATTERY_VOLTAGE, TX300CommandDirect.CLIENT, doosan.DH, "24～28.8" , false),
            new TX300CommandFiled(0x6000, "前泵压力", 2, TX300DataTyeps.EPOS_EMD_PUMP, TX300CommandDirect.CLIENT, doosan.DH, "0～350" , false),
            new TX300CommandFiled(0x6000, "后泵压力", 2, TX300DataTyeps.EPOS_EMD_PUMP, TX300CommandDirect.CLIENT, doosan.DH, "0～350" , false),
            new TX300CommandFiled(0x6000, "冷却水温", 1, TX300DataTyeps.EPOS_EMD_WATER_TEMP, TX300CommandDirect.CLIENT, doosan.DH, "0～105" , false),
            new TX300CommandFiled(0x6000, "燃油剩余量", 1, TX300DataTyeps.EPOS_MONITOR_OIL_LEFT, TX300CommandDirect.CLIENT, doosan.DH, "5～100" , false),
            new TX300CommandFiled(0x6000, "输入", 2, TX300DataTyeps.EPOS_MONITOR_INPUT, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6000, "模式", 1, TX300DataTyeps.EPOS_MONITOR_MODULE, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6000, "输出", 1, TX300DataTyeps.EPOS_MONITOR_OUTPUT, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6000, "比例阀电流", 2, TX300DataTyeps.EPOS_MONITOR_EEPRC, TX300CommandDirect.CLIENT, doosan.DH, "0～700" , false),
            new TX300CommandFiled(0x6000, "TPS电压", 2, TX300DataTyeps.EPOS_MONITOR_TPS, TX300CommandDirect.CLIENT, doosan.DH, "4000～950" , false),
            new TX300CommandFiled(0x6000, "油门旋转电压", 2, TX300DataTyeps.EPOS_MONITOR_DIALVOL, TX300CommandDirect.CLIENT, doosan.DH, "4000～950" , false),
            new TX300CommandFiled(0x6000, "发动机输出控制", 1, TX300DataTyeps.EPOS_MONITOR_ENG_CONTROL, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6000, "大臂油缸压力", 2, TX300DataTyeps.EPOS_MONITOR_BOOMPRE, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6000, "变速箱", 2, TX300DataTyeps.EPOS_MONITOR_TMPPRE, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6000, "液压油温度", 1, TX300DataTyeps.EPOS_EMD_OIL_TEMP, TX300CommandDirect.CLIENT, doosan.DH, "0～95" , false),
            new TX300CommandFiled(0x6000, "先导灯", 1, TX300DataTyeps.EPOS_MONITOR_PILOT, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6000, "E-T校验和", 1, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            // 0x6000(DX)
            new TX300CommandFiled(0x6000, "T-E状态", 1, TX300DataTyeps.EPOS_STATUS, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6000, "未知数据", 4, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DX, "没有分析方法" , false),
            new TX300CommandFiled(0x6000, "发动机转速", 2, TX300DataTyeps.USHORT, TX300CommandDirect.CLIENT, doosan.DX, "rpm(1100～2100)" , false),
            new TX300CommandFiled(0x6000, "TPS 电压", 2, TX300DataTyeps.EPOS_MONITOR_TPS, TX300CommandDirect.CLIENT, doosan.DX, "4000~950" , false),
            new TX300CommandFiled(0x6000, "前泵压力", 2, TX300DataTyeps.EPOS_EMD_PUMP, TX300CommandDirect.CLIENT, doosan.DX, "0～350" , false),
            new TX300CommandFiled(0x6000, "后泵压力", 2, TX300DataTyeps.EPOS_EMD_PUMP, TX300CommandDirect.CLIENT, doosan.DX, "0～350" , false),
            new TX300CommandFiled(0x6000, "未知数据", 1, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DX, "没有分析方法" , false),
            new TX300CommandFiled(0x6000, "比例阀电流", 2, TX300DataTyeps.EPOS_MONITOR_EEPRC, TX300CommandDirect.CLIENT, doosan.DX, "0～700" , false),
            new TX300CommandFiled(0x6000, "未知数据", 1, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DX, "没有分析方法" , false),
            new TX300CommandFiled(0x6000, "油门旋钮电压", 2, TX300DataTyeps.EPOS_MONITOR_DIALVOL, TX300CommandDirect.CLIENT, doosan.DX, "4000~950" , false),
            new TX300CommandFiled(0x6000, "未知数据", 2, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DX, "没有分析方法" , false),
            new TX300CommandFiled(0x6000, "冷却水温度", 2, TX300DataTyeps.EPOS_EMD_WATER_TEMP, TX300CommandDirect.CLIENT, doosan.DX, "0～105" , false),
            new TX300CommandFiled(0x6000, "变速箱", 2, TX300DataTyeps.EPOS_MONITOR_TMPPRE, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6000, "蓄电池电压", 2, TX300DataTyeps.EPOS_MONITOR_BATTERY_VOLTAGE, TX300CommandDirect.CLIENT, doosan.DX, "24～28.8" , false),
            new TX300CommandFiled(0x6000, "大臂油缸", 2, TX300DataTyeps.EPOS_MONITOR_BOOMPRE, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6000, "燃油剩余量", 2, TX300DataTyeps.EPOS_MONITOR_OIL_LEFT, TX300CommandDirect.CLIENT, doosan.DX, "5～100" , false),
            new TX300CommandFiled(0x6000, "液压油温度", 2, TX300DataTyeps.EPOS_EMD_OIL_TEMP, TX300CommandDirect.CLIENT, doosan.DX, "0～95" , false),
            new TX300CommandFiled(0x6000, "未知数据", 20, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DX, "没有分析方法" , false),
            new TX300CommandFiled(0x6000, "输入", 4, TX300DataTyeps.EPOS_MONITOR_INPUT, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6000, "发动机输出控制", 1, TX300DataTyeps.EPOS_MONITOR_ENG_CONTROL, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6000, "输出", 1, TX300DataTyeps.EPOS_MONITOR_OUTPUT, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6000, "模式", 1, TX300DataTyeps.EPOS_MONITOR_MODULE, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6000, "先导灯输出", 1, TX300DataTyeps.EPOS_MONITOR_PILOT, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6000, "E-T校验和", 1, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            // 0x6001(DH)
            new TX300CommandFiled(0x6001, "信息类别", 1, TX300DataTyeps.STATUS, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6001, "T-E状态", 1, TX300DataTyeps.EPOS_STATUS, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6001, "DefectFlag1", 1, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6001, "DefectFlag2", 1, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6001, "DefectFlag3", 1, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6001, "DefectFlag4", 1, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6001, "E-T校验和", 1, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            // 0x6001(DX)
            new TX300CommandFiled(0x6001, "信息类别", 1, TX300DataTyeps.STATUS, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6001, "T-E状态", 1, TX300DataTyeps.EPOS_STATUS, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6001, "故障数据长度", 1, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6001, "未知数据", 1, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6001, "故障数量", 1, TX300DataTyeps.EPOS_ETF_ERR_COUNT, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6001, "故障代码", 1, TX300DataTyeps.EPOS_ERR_CODE, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6001, "故障描述信息(FMI)", 1, TX300DataTyeps.EPOS_ERR_FMI, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6001, "E-T校验和", 1, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            // 0x6003(DH)
            new TX300CommandFiled(0x6003, "信息类别", 1, TX300DataTyeps.STATUS, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6003, "T-E状态", 1, TX300DataTyeps.EPOS_STATUS, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6003, "故障数量", 1, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6003, "故障代码", 1, TX300DataTyeps.EPOS_TRUBLE, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6003, "故障次数", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6003, "运转时间", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6003, "E-T校验和", 1, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            // 0x6003(DX)
            new TX300CommandFiled(0x6003, "信息类别", 1, TX300DataTyeps.STATUS, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6003, "T-E状态", 1, TX300DataTyeps.EPOS_STATUS, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6003, "未知数据", 2, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DX, "没有分析方法" , false),
            new TX300CommandFiled(0x6003, "故障数量", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6003, "计数索引", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.DX, "--------------------" , false),
            new TX300CommandFiled(0x6003, "故障代码", 1, TX300DataTyeps.EPOS_ERR_CODE, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6003, "故障FMI", 1, TX300DataTyeps.EPOS_ERR_FMI, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6003, "故障次数", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6003, "故障运行时间", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6003, "E-T校验和", 1, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            // 0x6004(DH)
            new TX300CommandFiled(0x6004, "信息类别", 1, TX300DataTyeps.STATUS, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "T-E状态", 1, TX300DataTyeps.EPOS_STATUS, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "总运转时间", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "driving time", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "working time", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "eng speed hrs 1", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "eng speed hrs 2", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "power mode hrs 3", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "power mode hrs 2", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "work mode 1", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "work mode 2", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "auto idle mode", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "travel speed 1", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "travel speed 2", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "travel speed 3", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "opr water temp 1", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "opr water temp 2", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "opr water temp 3", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "opr water temp 4", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "opr water temp 5", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "opr water temp 6", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "opr hydoil temp 1", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "opr hydoil temp 2", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "opr hydoil temp 3", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "opr hydoil temp 4", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "opr hydoil temp 5", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "opr hydoil temp 6", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6004, "E-T校验和", 1, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            // 0x6004(LD)
            new TX300CommandFiled(0x6004, "信息类别", 1, TX300DataTyeps.STATUS, TX300CommandDirect.CLIENT, doosan.Loader, "" , false),
            new TX300CommandFiled(0x6004, "总运转时间：小时数", 2, TX300DataTyeps.USHORT, TX300CommandDirect.CLIENT, doosan.Loader, "" , false),
            new TX300CommandFiled(0x6004, "总运转时间：分钟数", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.Loader, "" , false),
            // 0x6004(DX)
            new TX300CommandFiled(0x6004, "信息类别", 1, TX300DataTyeps.STATUS, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "T-E状态", 1, TX300DataTyeps.EPOS_STATUS, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "未知数据", 3, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DX, "没有分析方法" , false),
            new TX300CommandFiled(0x6004, "总运转时间", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "power md", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "standard md", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "digging md", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "trenching_md", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "travel_I_spd", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "travel_II_spd", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "auto idle md", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "travle", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "work", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "press up", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "breaker", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "shear", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "eng speed 2000", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "eng speed 1900", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "end speed 1800", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "eng speed 1700", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "eng speed 1600", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "eng speed 1200", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "eng speed 1200D", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "hyd oil tmp 30", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "hyd oil tmp 31", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "hyd oil tmp 51", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "hyd oil tmp 76", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "hyd oil tmp 86", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "hyd oil tmp 96", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "coolant tmp 40", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "coolant tmp 41", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "coolant tmp 61", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "coolant tmp 86", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "coolant tmp 96", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "coolant tmp 106", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "total fule", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "power fuel", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "standard fule", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6004, "E-T校验和", 1, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            // 0x6005(DH)
            new TX300CommandFiled(0x6005, "信息类别", 1, TX300DataTyeps.STATUS, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6005, "T-E状态", 1, TX300DataTyeps.EPOS_STATUS, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6005, "燃油滤清器", 4, TX300DataTyeps.UINT, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6005, "空气滤清器", 4, TX300DataTyeps.UINT, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6005, "发动机油滤清器", 4, TX300DataTyeps.UINT, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6005, "回油滤清器", 4, TX300DataTyeps.UINT, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6005, "先导滤清器", 4, TX300DataTyeps.UINT, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6005, "发动机油", 4, TX300DataTyeps.UINT, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6005, "液压油", 4, TX300DataTyeps.UINT, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6005, "冷却水", 4, TX300DataTyeps.UINT, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6005, "E-T校验和", 1, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            // 0x6005(DX)
            new TX300CommandFiled(0x6005, "信息类别", 1, TX300DataTyeps.STATUS, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6005, "T-E状态", 1, TX300DataTyeps.EPOS_STATUS, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6005, "未知数据", 2, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6005, "燃油滤清器", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6005, "空气滤清器", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6005, "发动机油滤清器", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6005, "回油滤清器", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6005, "先导滤清器", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6005, "发动机油", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6005, "液压油", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6005, "冷却水", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6005, "E-T校验和", 1, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            /*
            new fileds(0x6005, "未知数据", 16, type.BYTE, direct.CLIENT, doosan.DX, "" , false),
            new fileds(0x6005, "oil filter ex", 2, type.USHORT, direct.CLIENT, doosan.DX, "" , false),
            new fileds(0x6005, "air cleaner ex", 2, type.USHORT, direct.CLIENT, doosan.DX, "" , false),
            new fileds(0x6005, "eng oil filter ex", 2, type.USHORT, direct.CLIENT, doosan.DX, "" , false),
            new fileds(0x6005, "ret filter ex", 2, type.USHORT, direct.CLIENT, doosan.DX, "" , false),
            new fileds(0x6005, "pilot filter ex", 2, type.USHORT, direct.CLIENT, doosan.DX, "" , false),
            new fileds(0x6005, "eng oil ex", 2, type.USHORT, direct.CLIENT, doosan.DX, "" , false),
            new fileds(0x6005, "work oil ex", 2, type.USHORT, direct.CLIENT, doosan.DX, "" , false),
            new fileds(0x6005, "coolant ex", 2, type.USHORT, direct.CLIENT, doosan.DX, "" , false),
            new fileds(0x6005, "未知数据", 2, type.BYTE, direct.CLIENT, doosan.DX, "" , false),
            new fileds(0x6005, "E-T校验和", 1, type.BYTE, direct.CLIENT, doosan.DX, "" , false),*/
            // 0x6006(DH)
            new TX300CommandFiled(0x6006, "T-E状态", 1, TX300DataTyeps.EPOS_STATUS, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6006, "燃油剩余量", 1, TX300DataTyeps.EPOS_MONITOR_OIL_LEFT, TX300CommandDirect.CLIENT, doosan.DH, "百分比剩余量" , false),
            new TX300CommandFiled(0x6006, "蓄电池电压", 2, TX300DataTyeps.EPOS_MONITOR_BATTERY_VOLTAGE, TX300CommandDirect.CLIENT, doosan.DH, "V" , false),
            new TX300CommandFiled(0x6006, "警报", 1, TX300DataTyeps.EPOS_ALARM, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6006, "燃油滤清器", 1, TX300DataTyeps.EPOS_ALARM, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6006, "空气滤清器", 1, TX300DataTyeps.EPOS_ALARM, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6006, "发动机油滤清器", 1, TX300DataTyeps.EPOS_ALARM, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6006, "回油滤清器", 1, TX300DataTyeps.EPOS_ALARM, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6006, "先导滤清器", 1, TX300DataTyeps.EPOS_ALARM, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6006, "发动机油", 1, TX300DataTyeps.EPOS_ALARM, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6006, "液压油", 1, TX300DataTyeps.EPOS_ALARM, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6006, "冷却水", 1, TX300DataTyeps.EPOS_ALARM, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6006, "总运转时间", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            // 0x6006(DX)
            new TX300CommandFiled(0x6006, "T-E状态", 1, TX300DataTyeps.EPOS_STATUS, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6006, "燃油剩余量", 2, TX300DataTyeps.EPOS_MONITOR_OIL_LEFT, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6006, "蓄电池电压", 2, TX300DataTyeps.EPOS_MONITOR_BATTERY_VOLTAGE, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6006, "警报", 1, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6006, "滤清器警报", 1, TX300DataTyeps.EPOS_STATUS_FILTER, TX300CommandDirect.CLIENT, doosan.DX, "8 bit" , false),
            new TX300CommandFiled(0x6006, "总运转时间", 4, TX300DataTyeps.EPOS_RUNTIME, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6006, "E-T校验和", 1, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            // 0x6007(DH): T-S
            new TX300CommandFiled(0x6007, "T-E状态", 1, TX300DataTyeps.EPOS_STATUS, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            new TX300CommandFiled(0x6007, "保安命令", 1, TX300DataTyeps.SECURITY_SINGAL, TX300CommandDirect.CLIENT, doosan.DH, "" , false),
            // 0x6007(DH): S-T
            new TX300CommandFiled(0x6007, "保安命令", 1, TX300DataTyeps.SECURITY_SINGAL, TX300CommandDirect.SERVER, doosan.DH, "" , false),
            // 0x6007(DX): T-S
            new TX300CommandFiled(0x6007, "T-E状态", 1, TX300DataTyeps.EPOS_STATUS, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6007, "保安命令", 1, TX300DataTyeps.SECURITY_SINGAL, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6007, "处理结果", 9, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            // 0x6007(DX): S-T
            new TX300CommandFiled(0x6007, "时间参数", 5, TX300DataTyeps.BYTE, TX300CommandDirect.SERVER, doosan.DX, "年月日时分" , false),
            new TX300CommandFiled(0x6007, "保安命令", 1, TX300DataTyeps.SECURITY_SINGAL, TX300CommandDirect.SERVER, doosan.DX, "" , false),
            // 0x6008(DX)
            new TX300CommandFiled(0x6008, "T-E状态", 1, TX300DataTyeps.EPOS_STATUS, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6008, "主机信息", 10, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            // 0x6009(DX)
            new TX300CommandFiled(0x6009, "信息类型", 1, TX300DataTyeps.STATUS, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6009, "T-E状态", 1, TX300DataTyeps.EPOS_STATUS, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x6009, "每日运转时间", 12, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            // 0x600A(DX)
            new TX300CommandFiled(0x600A, "T-E状态", 1, TX300DataTyeps.EPOS_STATUS, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            new TX300CommandFiled(0x600A, "默认信息", 19, TX300DataTyeps.BYTE, TX300CommandDirect.CLIENT, doosan.DX, "" , false),
            // 0x600B
            new TX300CommandFiled(0x600B, "总运转时间：小时数", 2, TX300DataTyeps.USHORT, TX300CommandDirect.SERVER, doosan.Loader, "" , false),
            new TX300CommandFiled(0x600B, "总运转时间：分钟数", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.SERVER, doosan.Loader, "" , false),
            new TX300CommandFiled(0x600B, "总运转时间：小时数", 2, TX300DataTyeps.USHORT, TX300CommandDirect.CLIENT, doosan.Loader, "" , false),
            new TX300CommandFiled(0x600B, "总运转时间：分钟数", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.Loader, "" , false),
            // 0x600C
            new TX300CommandFiled(0x600C, "信息类型", 1, TX300DataTyeps.STATUS, TX300CommandDirect.CLIENT, doosan.Loader, "" , false),
            new TX300CommandFiled(0x600C, "时间：年月日", 3, TX300DataTyeps.LD_RUN_DATETIME, TX300CommandDirect.CLIENT, doosan.Loader, "" , false),
            new TX300CommandFiled(0x600C, "日运转时间：小时数", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.Loader, "" , false),
            new TX300CommandFiled(0x600C, "日运转时间：分钟数", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.Loader, "" , false),
            // 0x7000
            new TX300CommandFiled(0x7000, "信号强度", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.TX10G, "" , false),
            new TX300CommandFiled(0x7000, "F/W版本", 7, TX300DataTyeps.STRING, TX300CommandDirect.CLIENT, doosan.TX10G, "" , false),
            new TX300CommandFiled(0x7000, "基站号码", 4, TX300DataTyeps.STRING, TX300CommandDirect.CLIENT, doosan.TX10G, "" , false),
            new TX300CommandFiled(0x7000, "终端内部时间", 4, TX300DataTyeps.DATETIME, TX300CommandDirect.CLIENT, doosan.TX10G, "" , false),
            // 0x7010 T-S
            new TX300CommandFiled(0x7010, "命令状态", 1, TX300DataTyeps.STATUS, TX300CommandDirect.CLIENT, doosan.TX10G, "" , false),
            new TX300CommandFiled(0x7010, "信号强度", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.TX10G, "" , false),
            new TX300CommandFiled(0x7010, "F/W 版本", 7, TX300DataTyeps.STRING, TX300CommandDirect.CLIENT, doosan.TX10G, "" , false),
            new TX300CommandFiled(0x7010, "定期报告时间", 2, TX300DataTyeps.USHORT, TX300CommandDirect.CLIENT, doosan.TX10G, "秒" , false),
            new TX300CommandFiled(0x7010, "停车超时限制", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.TX10G, "秒" , false),
            new TX300CommandFiled(0x7010, "心跳时间间隔", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.TX10G, "" , false),
            new TX300CommandFiled(0x7010, "最低信号强度", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.TX10G, "" , false),
            new TX300CommandFiled(0x7010, "SMS 信号强度", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.TX10G, "" , false),
            new TX300CommandFiled(0x7010, "后备电池电压", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.TX10G, "" , false),
            new TX300CommandFiled(0x7010, "GPRS 服务器地址", 4, TX300DataTyeps.IP, TX300CommandDirect.CLIENT, doosan.TX10G, "" , false),
            new TX300CommandFiled(0x7010, "GPRS 服务器端口", 2, TX300DataTyeps.USHORT, TX300CommandDirect.CLIENT, doosan.TX10G, "" , false),
            new TX300CommandFiled(0x7010, "SMS 服务地址", 1, TX300DataTyeps.SMS_SERVER_ADDRESS, TX300CommandDirect.CLIENT, doosan.TX10G, "" , false),
            new TX300CommandFiled(0x7010, "终端内部时间", 4, TX300DataTyeps.DATETIME, TX300CommandDirect.CLIENT, doosan.TX10G, "" , false),
            // 0x7010 S-T
            new TX300CommandFiled(0x7010, "命令状态", 1, TX300DataTyeps.STATUS, TX300CommandDirect.SERVER, doosan.TX10G, "" , false),
            new TX300CommandFiled(0x7010, "定期报告时间", 2, TX300DataTyeps.USHORT, TX300CommandDirect.SERVER, doosan.TX10G, "秒" , false),
            new TX300CommandFiled(0x7010, "停车超时限制", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.SERVER, doosan.TX10G, "秒" , false),
            new TX300CommandFiled(0x7010, "心跳时间间隔", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.SERVER, doosan.TX10G, "" , false),
            new TX300CommandFiled(0x7010, "最低信号强度", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.SERVER, doosan.TX10G, "" , false),
            new TX300CommandFiled(0x7010, "SMS 信号强度", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.SERVER, doosan.TX10G, "" , false),
            new TX300CommandFiled(0x7010, "后备电池电压", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.SERVER, doosan.TX10G, "" , false),
            new TX300CommandFiled(0x7010, "GPRS 服务器地址", 4, TX300DataTyeps.IP, TX300CommandDirect.SERVER, doosan.TX10G, "" , false),
            new TX300CommandFiled(0x7010, "GPRS 服务器端口", 2, TX300DataTyeps.USHORT, TX300CommandDirect.SERVER, doosan.TX10G, "" , false),
            new TX300CommandFiled(0x7010, "SMS 服务地址", 1, TX300DataTyeps.SMS_SERVER_ADDRESS, TX300CommandDirect.SERVER, doosan.TX10G, "" , false),
            // 0x7020
            new TX300CommandFiled(0x7020, "信号强度", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.TX10G, "" , false),
            new TX300CommandFiled(0x7020, "报警内容", 1, TX300DataTyeps.TX10G_ALARMS, TX300CommandDirect.CLIENT, doosan.TX10G, "" , false),
            new TX300CommandFiled(0x7020, "定位信息", 1, TX300DataTyeps.TX10G_GPS, TX300CommandDirect.CLIENT, doosan.TX10G, "" , false),
            //new TX300CommandFiled(0x7020, "纬度", 4, TX300DataTyeps.POSITION, TX300CommandDirect.CLIENT, doosan.TX10G, "" , false),
            //new TX300CommandFiled(0x7020, "经度", 4, TX300DataTyeps.POSITION, TX300CommandDirect.CLIENT, doosan.TX10G, "" , false),
            //new TX300CommandFiled(0x7020, "定位时间", 4, TX300DataTyeps.DATETIME, TX300CommandDirect.CLIENT, doosan.TX10G, "" , false),
            // 0x7030
            new TX300CommandFiled(0x7030, "信号强度", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.TX10G, "" , false),
            new TX300CommandFiled(0x7030, "定位信息数量", 1, TX300DataTyeps.TX10G_GPS_COUNT, TX300CommandDirect.CLIENT, doosan.TX10G, "" , false),
            // 0x7031 TX10G GPS 信息（非终端发送的信息）
            new TX300CommandFiled(0x7031, "经度", 4, TX300DataTyeps.POSITION, TX300CommandDirect.CLIENT, doosan.TX10G, "" , false),
            new TX300CommandFiled(0x7031, "纬度", 4, TX300DataTyeps.POSITION, TX300CommandDirect.CLIENT, doosan.TX10G, "" , false),
            new TX300CommandFiled(0x7031, "定位时间", 4, TX300DataTyeps.DATETIME, TX300CommandDirect.CLIENT, doosan.TX10G, "" , false),
            // 0x7040
            new TX300CommandFiled(0x7040, "信号强度", 1, TX300DataTyeps.UBYTE, TX300CommandDirect.CLIENT, doosan.TX10G, "", false)
        };
        /// <summary>
        /// 通过 command_id 获取这个命令的描述。
        /// </summary>
        /// <param name="cmd">命令代码。</param>
        /// <returns>返回该命令的详细描述。</returns>
        public static string get_description(string cmd)
        {
            string str = "";
            for (int i = commands.GetLowerBound(0); i <= commands.GetUpperBound(0); i++)
            {
                if ((commands[i].command_name.IndexOf(cmd) >= 0))// && (commands[i].command_name == "")
                {
                    str = commands[i].command_name;
                    break;
                }
            }
            return str;
        }
        /// <summary>
        /// 查找命令码。
        /// </summary>
        /// <param name="descript">命令码的描述。</param>
        /// <returns>返回命令码。</returns>
        public static ushort get_command_id(string descript)
        {
            ushort cmd = 0x0000;
            for (int i = commands.GetLowerBound(0); i <= commands.GetUpperBound(0); i++)
            {
                if (commands[i].command_name == "")
                    break;
                if (commands[i].command_name.Equals(descript))
                {
                    cmd = commands[i].command_id;
                    break;
                }
            }
            return cmd;
        }
        
        /// <summary>
        /// 通过字段类型和字段值获取相应的描述或值内容。
        /// </summary>
        /// <param name="b">字段的值，16 进制。</param>
        /// <param name="type">字段的类型。</param>
        /// <returns>返回字段对应的值。</returns>
        public static string GetValues(byte[] b, TX300 tx300, TX300DataTyeps type)
        {
            string ss = "";
            switch (type)
            {
                case TX300DataTyeps.BYTE:
                    ss = ProtocolItems.GetHex(b);
                    break;
                case TX300DataTyeps.CONNECT_STATUS:
                    ss = ConnectStatus.GetConnectStatus(b[0]);
                    break;
                case TX300DataTyeps.UBYTE:
                    ss = b[0].ToString();
                    break;
                case TX300DataTyeps.DATETIME:
                    WbsDateTime.WbsDateTime wd = new WbsDateTime.WbsDateTime(b);
                    ss = wd.ByteToDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                    break;
                case TX300DataTyeps.STRING:
                    ss = Encoding.ASCII.GetString(b);
                    break;
                case TX300DataTyeps.UINT:
                    ss = BitConverter.ToUInt32(b, 0).ToString();
                    break;
                case TX300DataTyeps.USHORT:
                    ss = BitConverter.ToUInt16(b, 0).ToString();
                    break;
                case TX300DataTyeps.IP:
                    for (int k = 0; k < b.Length; k++)
                        ss += b[k].ToString() + ".";
                    ss = ss.Substring(0, ss.Length - 1);
                    break;
                case TX300DataTyeps.VOLTAGE:
                    int v = 0;
                    if (b.Length > 1)
                        v = BitConverter.ToUInt16(b, 0);
                    else
                        v = b[0];
                    double vol = v / 10.0;
                    ss = vol.ToString("0.0") + " V";
                    break;
                case TX300DataTyeps.STATUS:
                    if (tx300.CommandID == 0xDD00)
                    {
                        switch (b[0])
                        {
                            case 0x00:
                                ss = "采集全局设定数据";
                                break;
                            case 0xFF:
                                ss = "设置全局设定信息";
                                break;
                        }
                    }
                    else if (tx300.CommandID == 0x5000)
                    {
                        switch (b[0])
                        {
                            case 0x00:
                                ss = "ON（开机）";
                                break;
                            case 0xFF:
                                ss = "OFF（关机）";
                                break;
                        }
                    }
                    else
                    {
                        switch (b[0])
                        {
                            case 0x00:
                                ss = "命令回复";
                                break;
                            case 0xFF:
                                ss = "定期报告";
                                break;
                        }
                    }
                    break;
                case TX300DataTyeps.POSITION:
                    uint ii = BitConverter.ToUInt32(b, 0);
                    double p = ii / 10000.0;
                    ss = ProtocolItems.GPRMC2DDMMMM(p).ToString("00.000000");
                    break;
                case TX300DataTyeps.DIV100:
                    ushort iii = BitConverter.ToUInt16(b, 0);
                    double d = iii / 100.00;
                    ss = d.ToString("0.00");
                    break;
                case TX300DataTyeps.SECURITY_SINGAL:
                    ss = SecuritySignals.GetSecuritySignal(b[0]);
                    break;
                case TX300DataTyeps.CONTROL:
                    switch (b[0])
                    {
                        case 0x0F: ss = "远程控制：关机"; break;
                        case 0xF0: ss = "远程控制：开机"; break;
                        default: ss = "N/A"; break;
                    }
                    break;
                case TX300DataTyeps.RESET:
                    ss = ResetTypes.GetResetTypes(b[0]);
                    break;
                case TX300DataTyeps.ALARM:
                    ss = AlarmsStatus.GetAlarmsValue(b);
                    break;
                case TX300DataTyeps.EPOS_TRUBLE:
                    ss = AlarmsDescription.GetDescription(b[0]);
                    break;
                case TX300DataTyeps.EPOS_ALARM:
                    ss = AlarmsStatus.GetAlarmsStatus(b[0]);
                    break;
                case TX300DataTyeps.EPOS_RUNTIME:
                    uint total;
                    if (b.Length == 2)
                        total = BitConverter.ToUInt16(b, 0);
                    else
                        total = BitConverter.ToUInt32(b, 0);
                    uint hours = total / 60;
                    uint minutes = total % 60;
                    ss = string.Format("{0:0} 小时 {1:0} 分", hours, minutes);
                    break;
                case TX300DataTyeps.EPOS_EMD_PUMP:
                    ushort pump = BitConverter.ToUInt16(b, 0);
                    double r_pump = pump;
                    if (tx300.TerminalType == TerminalTypes.DH)
                    {
                        if (pump > 1070)
                            r_pump = (ushort)((r_pump - 1000) / 8.16);
                        ss = string.Format("{0:0}", r_pump);
                    }
                    else
                    {
                        r_pump = (ushort)((r_pump * 5000 / 1024 - 1000) / 8.16);
                        ss = string.Format("{0:0}", r_pump);
                    }
                    ss += " bar";
                    break;
                case TX300DataTyeps.EPOS_EMD_WATER_TEMP:
                    ss = tx300.TerminalType == TerminalTypes.DH ? WaterTempers.GetWaterTemp(b[0]) :
                        WaterTempers.GetWaterTempDX(BitConverter.ToUInt16(b, 0));
                    ss += " ℃";
                    break;
                case TX300DataTyeps.EPOS_EMD_OIL_TEMP:
                    ss = tx300.TerminalType == TerminalTypes.DH ? OilTempers.GetOilTemp(b[0]) :
                        OilTempers.GetOilTempDX((ushort)(BitConverter.ToUInt16(b, 0) * 890 / 1024));
                    ss += " ℃";
                    break;
                case TX300DataTyeps.EPOS_STATUS:
                    ss = EposStatus.GetEposStatus(b[0]);
                    break;
                case TX300DataTyeps.ERROR_TYPE:
                    ss = ErrorTypes.GetErrorTypes(b[0]);
                    break;
                case TX300DataTyeps.SMS_SERVER_ADDRESS:
                    ss = ProtocolItems.GetHex(b);
                    break;
                case TX300DataTyeps.SIM_LOCK_STATUS:
                    ss = SimLockStatus.GetLockStatus(b[0]);
                    break;
                case TX300DataTyeps.EE00_PARAM:
                    ss = b[0].ToString();
                    if (b[0] == 0x00)
                    {
                        ss = "无参数(参数长度为0)";
                    }
                    break;
                case TX300DataTyeps.LD_RUN_DATETIME:
                    ss = (b[0] + 2000).ToString() + "-" + b[1].ToString("00") + "-" + b[2].ToString("00");
                    break;
                case TX300DataTyeps.RESP_COMMAND:
                    ss = "0x" + b[1].ToString("X2") + b[0].ToString("X2");
                    break;
                case TX300DataTyeps.RESP_STATUS:
                    ss = ResponseStatus.GetResponseStatus(b[0]);
                    break;
                case TX300DataTyeps.INT:
                    ss = BitConverter.ToInt32(b, 0).ToString();
                    break;
                case TX300DataTyeps.EPOS_ERR_CODE:
                    ss = AlarmsDescription.GetDescriptionDX(b[0]);
                    break;
                case TX300DataTyeps.EPOS_ERR_FMI:
                    ss = AlarmsDescription.GetFMIDescription(b[0]);
                    break;
                case TX300DataTyeps.EPOS_ETF_ERR_COUNT:
                    ss = b[0].ToString();
                    break;
                case TX300DataTyeps.EPOS_MONITOR_ENG_CONTROL:
                    // 只有 1 个byte
                    if (tx300.TerminalType == TerminalTypes.DH)
                        ss = MonitorEngineControl.GetEngControl(b[0]);
                    else
                        ss = MonitorEngineControl.GetEngControlDX(b[0]);
                    break;
                case TX300DataTyeps.EPOS_MONITOR_INPUT:
                    ss = MonitorInput.GetInput(b);
                    break;
                case TX300DataTyeps.EPOS_MONITOR_MODULE:
                    ss = MonitorModule.GetModule(b[0], tx300.TerminalType);
                    break;
                case TX300DataTyeps.EPOS_MONITOR_OUTPUT:
                    ss = MonitorOutput.GetOutput(b[0]);
                    break;
                case TX300DataTyeps.EPOS_MONITOR_PILOT:
                    ss = MonitorPilot.GetPilot(b[0], tx300.TerminalType);
                    break;
                case TX300DataTyeps.EPOS_MONITOR_EEPRC:
                    ss = ((ushort)(BitConverter.ToUInt16(b, 0) * 890 / 1024)).ToString() + " mA";
                    break;
                case TX300DataTyeps.EPOS_MONITOR_TPS:
                    ss = tx300.TerminalType == TerminalTypes.DH ? (BitConverter.ToUInt16(b, 0).ToString()) : (BitConverter.ToUInt16(b, 0) * 5000 / 1024).ToString();
                    ss += " mV";
                    break;
                case TX300DataTyeps.EPOS_MONITOR_DIALVOL:
                    ss = tx300.TerminalType == TerminalTypes.DH ? BitConverter.ToUInt16(b, 0).ToString() : (BitConverter.ToUInt16(b, 0) * 5000 / 1024).ToString();
                    ss += " mV";
                    break;
                case TX300DataTyeps.EPOS_MONITOR_TMPPRE:
                    ss = tx300.TerminalType == TerminalTypes.DH ? BitConverter.ToUInt16(b, 0).ToString() :
                        ((ushort)(((BitConverter.ToUInt16(b, 0) * 5000 / 1024 * 0.1275) - 63.74) / 10)).ToString();
                    ss += " w";
                    break;
                case TX300DataTyeps.EPOS_MONITOR_BOOMPRE:
                    ss = tx300.TerminalType == TerminalTypes.DH ? BitConverter.ToUInt16(b, 0).ToString() :
                        ((ushort)((BitConverter.ToUInt16(b, 0) * 5000 / 1024 - 1000) / 8.16)).ToString();
                    ss += " bar";
                    break;
                case TX300DataTyeps.EPOS_MONITOR_BATTERY_VOLTAGE:
                    ss = tx300.TerminalType == TerminalTypes.DH ? (BitConverter.ToUInt16(b, 0) / 10.0).ToString() :
                        ((BitConverter.ToUInt16(b, 0) * 6336 / 16384 + 11) / 10.0).ToString();
                    ss += " V";
                    break;
                case TX300DataTyeps.EPOS_MONITOR_OIL_LEFT:
                    ss = tx300.TerminalType == TerminalTypes.DH ? b[0].ToString() + "0" :
                        OilTempers.GetOilLeftDX((ushort)(BitConverter.ToUInt16(b, 0) * 500 / 1024)).ToString();
                    ss += "%";
                    break;
                case TX300DataTyeps.EPOS_STATUS_FILTER:
                    ss = StatusFilters.GetStatusDX(b[0]);
                    break;
                case TX300DataTyeps.BLIND_UNLOCK_PASSWORD:
                    ss = BitConverter.ToUInt32(b, 0).ToString("00000000");
                    break;
                case TX300DataTyeps.TX10G_ALARMS:
                    ss = tx10g_alarms.GetAlarms(b[0]);
                    break;
                case TX300DataTyeps.TX10G_GPS_COUNT:
                    ss = b[0].ToString();
                    break;
            }
            return ss;
        }
    }
}
