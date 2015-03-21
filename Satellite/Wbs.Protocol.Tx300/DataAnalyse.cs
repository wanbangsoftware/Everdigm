using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wbs.Protocol;
using Wbs.Protocol.WbsDateTime;

namespace Wbs.Protocol.TX300.Analyse
{
    /// <summary>
    /// 数据包分析基类
    /// </summary>
    public class Analyse
    {
        /// <summary>
        /// 数据类型（DH、DX等）
        /// </summary>
        public doosan DataType { get; set; }
        /// <summary>
        /// 服务器接收到数据的时间
        /// </summary>
        public DateTime ReceiveTime { get; set; }
        /// <summary>
        /// 挖掘机号码
        /// </summary>
        public string MacID { get; set; }
        /// <summary>
        /// 命令字
        /// </summary>
        public string Command { get; set; }
        /// <summary>
        /// 终端Sim卡号码
        /// </summary>
        public string SimNo { get; set; }
        /// <summary>
        /// 数据包的具体内容
        /// </summary>
        protected byte[] _content = null;
        /// <summary>
        /// 数据包的二进制内容
        /// </summary>
        public byte[] Content
        {
            get { return _content; }
            set
            {
                if (null == value) _content = null;
                else
                {
                    var len = value.Length;
                    _content = new byte[len];
                    Buffer.BlockCopy(value, 0, _content, 0, len);

                    Unpackage();
                }
            }
        }
        /// <summary>
        /// 数据包内容（去掉包头包尾的数据包）
        /// </summary>
        public string HexContent
        {
            get { return ProtocolItems.GetHex(_content); }
            set
            {
                if (string.IsNullOrEmpty(value))
                    Content = null;
                else
                {
                    Content = ProtocolItems.GetBytes(value);
                }
            }
        }
        /// <summary>
        /// 分析数据包内容
        /// </summary>
        public virtual void Unpackage()
        {
            if (null == _content)
                throw new Exception("Could not analyse null data.");
        }
    }
    /// <summary>
    /// 定位信息类
    /// </summary>
    public class GPSInfo
    {
        private int index = 0;
        /// <summary>
        /// 数据长度
        /// </summary>
        public int Length { get; private set; }
        /// <summary>
        /// 创建一个定位信息类实体
        /// </summary>
        public GPSInfo() {
            index = 0;
        }
        /// <summary>
        /// 创建一个定位信息类实体并指定数据内容以供解析
        /// </summary>
        /// <param name="data"></param>
        public GPSInfo(byte[] data)
        {
            index = 0;
            Content = data;
        }
        /// <summary>
        /// 创建一个定位信息类实体并指定数据内容以供解析
        /// </summary>
        /// <param name="data"></param>
        /// <param name="index"></param>
        public GPSInfo(byte[] data, int index)
        {
            this.index = index;
            Content = data;
        }
        /// <summary>
        /// 数据内容
        /// </summary>
        private byte[] _content = null;
        /// <summary>
        /// 数据内容
        /// </summary>
        public byte[] Content { 
            set {
                if (null == value) _content = null;
                else
                {
                    var len = value.Length;
                    _content = new byte[len];
                    Buffer.BlockCopy(value, 0, _content, 0, len);
                }
            } 
        }
        /// <summary>
        /// 时间
        /// </summary>
        private WbsDateTime.WbsDateTime _gpstime = new WbsDateTime.WbsDateTime();
        /// <summary>
        /// 定位时间
        /// </summary>
        public DateTime GPSTime { get { return _gpstime.ByteToDateTime; } }
        /// <summary>
        /// 纬度
        /// </summary>
        public double Latitude { get; private set; }
        /// <summary>
        /// 南北指示
        /// </summary>
        public string NS { get; private set; }
        /// <summary>
        /// 经度
        /// </summary>
        public double Longitude { get; private set; }
        /// <summary>
        /// 东西指示
        /// </summary>
        public string EW { get; private set; }
        /// <summary>
        /// 速度km/h
        /// </summary>
        public double Speed { get; private set; }
        /// <summary>
        /// 方向，正北为0°
        /// </summary>
        public double Direction { get; private set; }
        /// <summary>
        /// 海拔高度，单位m
        /// </summary>
        public double Altitude { get; private set; }
        /// <summary>
        /// 解包定位信息
        /// </summary>
        public void Unpackage()
        {
            Length = 0;

            var date = new byte[4];
            Buffer.BlockCopy(_content, index, date, 0, 4);
            _gpstime = new WbsDateTime.WbsDateTime(date);
            index += 4;
            Length += 4;

            Longitude = (BitConverter.ToUInt32(_content, index)) / 10000.0;
            index += 4;
            Length += 4;

            EW = ASCIIEncoding.ASCII.GetString(_content, index, 1);
            index += 1;
            Length += 1;

            Latitude = (BitConverter.ToUInt32(_content, index)) / 10000.0;
            index += 4;
            Length += 4;

            NS = ASCIIEncoding.ASCII.GetString(_content, index, 1);
            index += 1;
            Length += 1;

            Speed = (BitConverter.ToUInt16(_content, index)) / 100.0;
            index += 2;
            Length += 2;

            Direction = (BitConverter.ToUInt16(_content, index)) / 100.0;
            index += 2;
            Length += 2;

            Altitude = (BitConverter.ToUInt32(_content, index)) / 100.0;
            index += 4;
            Length += 4;
        }
    }
    /// <summary>
    /// 0xBB0F命令
    /// </summary>
    public class _0xBB0F : Analyse
    {
        // 383938363030393631353134354132323831373455
        /// <summary>
        /// 终端回传信息中Sim卡的ICCID号码
        /// </summary>
        public string ICCID { get; private set; }
        /// <summary>
        /// 解析0xBB0F数据包中的各个属性值
        /// </summary>
        public override void Unpackage()
        {
            base.Unpackage();
            var index = 0;
            ICCID = ASCIIEncoding.ASCII.GetString(_content);
            index += 20;
        }
    }
    /// <summary>
    /// 0xCC00命令
    /// </summary>
    public class _0xCC00 : Analyse
    {
        /// <summary>
        /// 终端固件版本
        /// </summary>
        public string Firmware { get; private set; }
        /// <summary>
        /// 基站号码
        /// </summary>
        public string Station { get; private set; }
        /// <summary>
        /// 链接原因
        /// </summary>
        private byte reason;
        /// <summary>
        /// 链接原因
        /// </summary>
        public string Reason
        {
            get
            {
                string s = "";
                switch (reason)
                {
                    case 0x00:
                    case 0x10:
                    case 0x20:
                    case 0x40: s = "Command reset"; break;
                    case 0x01: s = "Send data error"; break;
                    case 0x02: s = "Code is invalid"; break;
                    case 0x03: s = "Module no response"; break;
                    case 0x04: s = "SMS Receiving error"; break;
                    case 0x05: s = "CPU-Module has no communicat > 7 min"; break;
                    case 0x06: s = "CPU no check network > 12 min"; break;
                    case 0x07: s = "To much SMS messages"; break;
                    case 0x08: s = "Initialize fail: > 2 min"; break;
                    case 0x09: s = "模块初始化时，超过按命令阶段回复"; break;
                    case 0x0A: s = "超过定期报告时间但没有上传报告"; break;
                    case 0x0B: s = "Battery low"; break;
                    case 0x0C: s = "No simno when sending 0xCC00 command"; break;
                    case 0x0D: s = "5 小时内发送命令一次都没成功"; break;
                    case 0x0E: s = "5 分钟内 CSQ > limit 但没有网络"; break;
                    case 0x0F: s = "Security command: Sim card"; break;
                    case 0x11: s = "Connect GPRS error"; break;
                    case 0x12: s = "Module power down"; break;
                    case 0x13: s = "GPS 拆除报警后 GPS 依然可以收信"; break;
                    case 0x14: s = "To much backup data"; break;
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
        /// 总运转时间
        /// </summary>
        public uint WorkTime { get; private set; }
        /// <summary>
        /// 终端系统时钟
        /// </summary>
        private WbsDateTime.WbsDateTime systime = new WbsDateTime.WbsDateTime();
        /// <summary>
        /// 终端系统内时钟
        /// </summary>
        public DateTime SystemTime { get { return systime.ByteToDateTime; } }
        /// <summary>
        /// 解析0xCC00命令
        /// </summary>
        public override void Unpackage()
        {
            base.Unpackage();
            var index = 0;

            Firmware = ASCIIEncoding.ASCII.GetString(_content, index, 7);
            index += 7;

            Station = ASCIIEncoding.ASCII.GetString(_content, index, 4);
            index += 4;

            reason = _content[index];
            index += 1;

            WorkTime = BitConverter.ToUInt32(_content, index);
            index += 4;

            var data = new byte[4];
            Buffer.BlockCopy(_content, index, data, 0, 4);
            systime = new WbsDateTime.WbsDateTime(data);
            index += 4;
        }
    }
    /// <summary>
    /// 0xDD00命令
    /// </summary>
    public class _0xDD00 : Analyse
    {
        // 00483130433130381C1E0014000D25280000000000000000000007837C0A8D8002061065730562813C7AB1FA9F
        /// <summary>
        /// 终端固件版本
        /// </summary>
        public string Firmware { get; private set; }
        /// <summary>
        /// 信号强度
        /// </summary>
        public byte CSQ { get; private set; }
        /// <summary>
        /// 速度限制km/h
        /// </summary>
        public byte LimitSpeed { get; private set; }
        /// <summary>
        /// 越界半径单位：m
        /// </summary>
        public ushort Radius { get; private set; }
        /// <summary>
        /// 越界中心点纬度
        /// </summary>
        public double Latitude { get; private set; }
        /// <summary>
        /// 越界中心点经度
        /// </summary>
        public double Longitude { get; private set; }
        /// <summary>
        /// 解析0xDD00命令
        /// </summary>
        public override void Unpackage()
        {
            base.Unpackage();
            var index = 1;
            Firmware = ASCIIEncoding.ASCII.GetString(_content, index, 7);
            index += 7;

            CSQ = _content[index];
            index += 7;

            LimitSpeed = _content[index];
            index += 1;

            Radius = BitConverter.ToUInt16(_content, index);
            index += 2;

            Longitude = (BitConverter.ToUInt32(_content, index)) / 10000.0;
            index += 4;

            Latitude = (BitConverter.ToUInt32(_content, index)) / 10000.0;
            index += 4;
        }
    }
    /// <summary>
    /// 0xEE00命令
    /// </summary>
    public class _0xEE00 : Analyse
    {
        // 483130533130372000CC005F
        /// <summary>
        /// 终端固件版本
        /// </summary>
        public string Firmware { get; private set; }
        private byte error;
        /// <summary>
        /// 错误类型
        /// </summary>
        public string Type
        {
            get {
                string ss = "";
                switch (error)
                {
                    case 0x10: ss = "Firmware is too old"; break;
                    case 0x20: ss = "EPOS no response"; break;
                    case 0x30: ss = "Eng. not start"; break;
                    default: ss = "N/A"; break;
                }
                return ss;
            }
        }
        /// <summary>
        /// 出错的命令字
        /// </summary>
        public string ErrorCommand { get; private set; }
        /// <summary>
        /// 解析0xEE00命令
        /// </summary>
        public override void Unpackage()
        {
            base.Unpackage();
            var index = 0;

            Firmware = ASCIIEncoding.ASCII.GetString(_content, index, 7);
            index += 7;

            error = _content[index];
            index += 1;

            ErrorCommand = "0x" + ProtocolItems.GetHex(_content[index + 1]) + ProtocolItems.GetHex(_content[index]);
            index += 2;
        }
    }
    /// <summary>
    /// 0xFF00命令
    /// </summary>
    public class _0xFF00 : Analyse
    {
        // 253C7A50DD96232C0745629EC3014E06000000BE05000077
        /// <summary>
        /// 后备电池剩余电压
        /// </summary>
        public double BatteryVoltage { get; private set; }
        /// <summary>
        /// 定位信息
        /// </summary>
        public GPSInfo GPSInfo { get; private set; }
        /// <summary>
        /// 解析0xFF00命令
        /// </summary>
        public override void Unpackage()
        {
            base.Unpackage();
            var index = 0;

            BatteryVoltage = _content[index] / 10.0;
            index += 1;

            GPSInfo = new GPSInfo(_content, index);
            index += GPSInfo.Length;
        }
    }
    /// <summary>
    /// 0x1000命令
    /// </summary>
    public class _0x1000 : Analyse
    {
        // 00013C78CE55A19A5206457AECAD014E000044368EE301009F
        /// <summary>
        /// 命令类别0x00=命令回复0xFF=自动汇报
        /// </summary>
        public byte Type { get; private set; }
        /// <summary>
        /// 数据条数
        /// </summary>
        public byte Count { get; private set; }
        /// <summary>
        /// 定位信息
        /// </summary>
        public GPSInfo GPSInfo { get; private set; }
        /// <summary>
        /// 分析0x1000命令的数据包内容
        /// </summary>
        public override void Unpackage()
        {
            base.Unpackage();
            var index = 0;
            Type = _content[index];
            index += 1;

            Count = _content[index];
            index += 1;

            GPSInfo = new GPSInfo(_content, index);
            index += GPSInfo.Length;
        }
    }
    /// <summary>
    /// 0x1001命令
    /// </summary>
    public class _0x1001 : Analyse
    {
        // FF3C7972F51000DCA97101642128061100F0A97101502128064831304331303701864479E10000000000000049
        private WbsDateTime.WbsDateTime gpstime = new WbsDateTime.WbsDateTime();
        /// <summary>
        /// 定位时间，第二条以此时间加30分钟即可
        /// </summary>
        public DateTime GPSTime { get { return gpstime.ByteToDateTime; } }
        /// <summary>
        /// 第一条定位时的信号强度
        /// </summary>
        public byte CSQ_1 { get; private set; }
        /// <summary>
        /// 第一条定位的纬度
        /// </summary>
        public double Latitude_1 { get; private set; }
        /// <summary>
        /// 第一条定位的经度
        /// </summary>
        public double Longitude_1 { get; private set; }
        /// <summary>
        /// 第二条定位时的信号强度
        /// </summary>
        public byte CSQ_2 { get; private set; }
        /// <summary>
        /// 第二条定位的纬度
        /// </summary>
        public double Latitude_2 { get; private set; }
        /// <summary>
        /// 第二条定位的经度
        /// </summary>
        public double Longitude_2 { get; private set; }
        /// <summary>
        /// 终端固件版本
        /// </summary>
        public string Firmware { get; private set; }
        /// <summary>
        /// 固件版本修正版本号
        /// </summary>
        public byte Revision { get; private set; }
        /// <summary>
        /// 基站号码
        /// </summary>
        public string Station { get; private set; }
        /// <summary>
        /// 扇区号码
        /// </summary>
        public string Sector { get; private set; }
        /// <summary>
        /// 工作时间
        /// </summary>
        public uint WorkTime { get; private set; }
        /// <summary>
        /// 燃油剩余量
        /// </summary>
        public ushort Fuel { get; private set; }
        /// <summary>
        /// 解析0x1001数据的各个属性值
        /// </summary>
        public override void Unpackage()
        {
            base.Unpackage();
            var index = 1;
            var data = new byte[4];
            Buffer.BlockCopy(_content, index, data, 0, 4);
            gpstime = new WbsDateTime.WbsDateTime(data);
            index += 4;

            CSQ_1 = _content[index];
            index += 2;

            Latitude_1 = (BitConverter.ToUInt32(_content, index)) / 10000.0;
            index += 4;

            Longitude_1 = (BitConverter.ToUInt32(_content, index)) / 10000.0;
            index += 4;

            CSQ_2 = _content[index];
            index += 2;

            Latitude_2 = (BitConverter.ToUInt32(_content, index)) / 10000.0;
            index += 4;

            Longitude_2 = (BitConverter.ToUInt32(_content, index)) / 10000.0;
            index += 4;

            Firmware = ASCIIEncoding.ASCII.GetString(_content, index, 7);
            index += 7;

            Revision = _content[index];
            index += 1;

            Station = ASCIIEncoding.ASCII.GetString(_content, index, 4);
            index += 4;

            Sector = ASCIIEncoding.ASCII.GetString(_content, index, 4);
            index += 4;

            WorkTime = BitConverter.ToUInt32(_content, index);
            index += 4;

            Fuel = BitConverter.ToUInt16(_content, index);
            index += 2;
        }
    }
    /// <summary>
    /// 0x2000命令
    /// </summary>
    public class _0x2000 : Analyse
    {
        //00043C7A2699BECB4A064514D8DA014E7011EE71BE7D0000CC
        private string alarm = "0000000000000000";
        /// <summary>
        /// 报警信息的二进制字符串
        /// </summary>
        public string AlarmBIN { get;private set; }
        /// <summary>
        /// 获取报警信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetAlarm(string code) {
            string ss = "";
            ss += code[0] == '1' ? "Main Power Lose," : "";
            ss += code[4] == '1' ? "GSM Blind," : "";
            ss += code[5] == '1' ? "Case Opened," : "";
            ss += code[6] == '1' ? "GSM Ant. Short-Circuit," : "";
            ss += code[7] == '1' ? "GSM Ant. Grounded," : "";
            ss += code[8] == '1' ? "GSM Ant. Disconnected," : "";
            ss += code[9] == '1' ? "EPOS Error," : "";
            ss += code[10] == '1' ? "Sim Card Losing," : "";
            ss += code[11] == '1' ? "Position Break," : "";
            ss += code[12] == '1' ? "Sat. Ant. Disconnected," : "";
            ss += code[13] == '1' ? "Over Speed," : "";
            ss += code[14] == '1' ? "GPS Ant. Disconnected," : "";
            ss += code[15] == '1' ? "External Power Lose," : "";
            if (ss.Length > 0)
                ss = ss.Substring(0, ss.Length - 1);
            else
                ss = "No Alarm";
            return ss;
        }
        /// <summary>
        /// 报警信息描述
        /// </summary>
        public string Alarm
        {
            get
            {
                return GetAlarm(alarm);
            }
        }
        /// <summary>
        /// 报警发生时的定位信息
        /// </summary>
        public GPSInfo GPSInfo { get;private set; }
        
        /// <summary>
        /// 解析0x2000数据包的内容
        /// </summary>
        public override void Unpackage()
        {
            base.Unpackage();
            int index = 0;
            alarm = ProtocolItems.IntToDigit(BitConverter.ToUInt16(_content, index), ProtocolItems.BIN, 8);

            GPSInfo = new GPSInfo(_content, index);
            index += GPSInfo.Length;
        }
    }
    /// <summary>
    /// 0x5000命令
    /// </summary>
    public class _0x5000 : Analyse
    {
        /// <summary>
        /// 发电机电压
        /// </summary>
        public double GeneratorVoltage { get; private set; }
        /// <summary>
        /// 后备电池电压
        /// </summary>
        public double BatteryVoltage { get; private set; }
        /// <summary>
        /// 开关机时的定位信息
        /// </summary>
        public GPSInfo GPSInfo { get; private set; }
        /// <summary>
        /// 分解0x5000数据
        /// </summary>
        public override void Unpackage()
        {
            base.Unpackage();
            var index = 0;

            GeneratorVoltage = (BitConverter.ToUInt16(_content, index)) / 10.0;
            index += 2;

            BatteryVoltage = _content[index] / 10.0;
            index += 1;

            GPSInfo = new GPSInfo(_content, index);
            index += GPSInfo.Length;
        }
    }
    /// <summary>
    /// 0x6000仪表盘数据命令
    /// </summary>
    public class _0x6000 : Analyse
    {
        // DH: 01BF03E3003E055D050007010004080000370CAB0F170000D30B010051DC
        // DX: 013B901017BC023003FD00FC0005FE00002E03000000000600B9021100A5028303000000000000120000000000000000000000200800C100081200D400F8AE
        /// <summary>
        /// 转速
        /// </summary>
        public ushort RPM { get; private set; }
        /// <summary>
        /// 发动机蓄电池电压
        /// </summary>
        public double Battery { get; private set; }
        private ushort fpump;
        /// <summary>
        /// 前泵压力
        /// </summary>
        public string FrontPump
        {
            get { return MonitorPump.GetPump(fpump, DataType); }
        }
        private ushort rpump;
        /// <summary>
        /// 后泵压力
        /// </summary>
        public string RearPump { get { return MonitorPump.GetPump(rpump, DataType); } }
        private ushort coolant;
        /// <summary>
        /// 冷却水温
        /// </summary>
        public string Coolant
        {
            get
            {
                var ret="";
                switch (DataType)
                { 
                    case doosan.DH:
                        ret = WaterTempers.GetWaterTemp((byte)coolant);
                        break;
                    case doosan.DX:
                        ret = WaterTempers.GetWaterTempDX(coolant);
                        break;
                }
                return ret;
            }
        }
        private ushort fuel;
        /// <summary>
        /// 燃油剩余量
        /// </summary>
        public string Fuel
        {
            get
            {
                var ret = "";
                switch (DataType)
                {
                    case doosan.DH: ret = string.Format("{0:00}", fuel); break;
                    case doosan.DX:
                        ret = OilTempers.GetOilLeftDX((ushort)(fuel * 500 / 1024)).ToString();
                        break;
                }
                ret += "%";
                return ret;
            }
        }
        private ushort hyd;
        /// <summary>
        /// 液压油温度
        /// </summary>
        public string HydOilTemp
        {
            get
            {
                var ret = "";
                switch (DataType)
                {
                    case doosan.DH: 
                        ret=OilTempers.GetOilTemp((byte)hyd); break;
                    case doosan.DX:
                        ret = OilTempers.GetOilTempDX((ushort)(hyd * 890 / 1024));
                        break;
                }
                ret += "℃";
                return ret;
            }
        }
        /// <summary>
        /// 解析0x6000数据包
        /// </summary>
        public override void Unpackage()
        {
            base.Unpackage();
            if (DataType == doosan.DH) {
                RPM = BitConverter.ToUInt16(_content, 1);
                Battery = BitConverter.ToUInt16(_content, 2) / 10.0;
                fpump = BitConverter.ToUInt16(_content, 5);
                rpump = BitConverter.ToUInt16(_content, 7);
                coolant = _content[9];
                fuel = _content[10];
                hyd = _content[26];
            }
            else if (DataType == doosan.DX) {
                RPM = BitConverter.ToUInt16(_content, 5);
                Battery = BitConverter.ToUInt16(_content, 27) / 10.0;
                fpump = BitConverter.ToUInt16(_content, 9);
                rpump = BitConverter.ToUInt16(_content, 11);
                coolant = BitConverter.ToUInt16(_content, 21);
                fuel = BitConverter.ToUInt16(_content, 29);
                hyd = BitConverter.ToUInt16(_content, 31);
            }
        }
    }
    /// <summary>
    /// EPOS故障信息
    /// </summary>
    public class EposFault {
        /// <summary>
        /// 故障信息里的数据长度固定为3字节
        /// </summary>
        public static int FaultSize = 3;
        /// <summary>
        /// 故障数据内容
        /// </summary>
        private byte[] _data = null;
        /// <summary>
        /// 故障次数
        /// </summary>
        public byte Count { get; private set; }
        /// <summary>
        /// 故障代码
        /// </summary>
        public byte Code { get; private set; }
        /// <summary>
        /// 获取故障代码的说明
        /// </summary>
        public string CodeDescription
        {
            get { return AlarmsDescription.GetDescriptionDX(Code); }
        }
        /// <summary>
        /// 故障FMI
        /// </summary>
        public byte FMI { get; private set; }
        /// <summary>
        /// 获取故障FMI代码的详细说明
        /// </summary>
        public string FMIDescription
        {
            get { return AlarmsDescription.GetFMIDescription(FMI); }
        }
        /// <summary>
        /// 实例化一个空的EPOS故障类实体
        /// </summary>
        public EposFault() { }
        /// <summary>
        /// 实例化一个EPOS故障类实体并指定故障内容以供分解
        /// </summary>
        /// <param name="data"></param>
        public EposFault(byte[] data)
        {
            Data = data;
        }
        /// <summary>
        /// 实例化一个EPOS故障类实体并指定故障内容和开始截取数据的索引
        /// </summary>
        /// <param name="data"></param>
        /// <param name="start"></param>
        public EposFault(byte[] data, int start)
        {
            var len = data.Length;
            if (start + FaultSize > len) throw new Exception("Not enough data.");
            _data = new byte[FaultSize];
            Buffer.BlockCopy(data, start, _data, 0, FaultSize);
            Unpackage();
        }
        /// <summary>
        /// 故障内容
        /// </summary>
        public byte[] Data
        {
            get { return _data; }
            set
            {
                if (null == value) _data = null;
                else
                {
                    var len = value.Length;
                    _data = new byte[len];
                    Buffer.BlockCopy(value, 0, _data, 0, len);
                    Unpackage();
                }
            }
        }
        /// <summary>
        /// 解析故障代码
        /// </summary>
        private void Unpackage()
        {
            if (_data.Length < FaultSize) return;
            Count = _data[0];
            Code = _data[1];
            FMI = _data[3];
        }
    }
    /// <summary>
    /// 0x6001命令。EPOS故障报警信息
    /// </summary>
    public class _0x6001 : Analyse
    {
        // 0001068D0211041204C03B
        // 00010A8D04190B110412040105F03B
        // 00010C8D0511041204010603060D03E975
        private List<EposFault> _faults = new List<EposFault>();
        /// <summary>
        /// 故障列表
        /// </summary>
        public List<EposFault> Faults { get { return _faults; } }
        /// <summary>
        /// 解析0x6001数据，只解析DX终端的
        /// </summary>
        public override void Unpackage()
        {
            base.Unpackage();
            // DH的不处理(length==8)，DX的无故障的也不处理(length==7)
            if (_content.Length <= 8) return;

            var index = 2;
            if (_content[index] == 0) index += 1;

            var last = _content[index];
            index += 2;

            if (last < EposFault.FaultSize) return;

            var cnt = (last - 1) / EposFault.FaultSize;

            while (cnt > 0)
            {
                _faults.Add(new EposFault(_content, index));
                index += EposFault.FaultSize;
                cnt--;
            }
        }
    }
    /// <summary>
    /// 0x6004命令DH
    /// </summary>
    public class _0x6004DH : Analyse
    {
        public uint TotalWorkTime { get; private set; }
        public uint DrivingTime { get; private set; }
        public uint WorkingTime { get; private set; }
        public uint EngSeppdHrs1 { get; private set; }
        public uint EngSpeedHrs2 { get; private set; }
        public uint PowerModeHrs1 { get; private set; }
        public uint PowerModeHrs2 { get; private set; }
        public uint WorkMode1 { get; private set; }
        public uint WorkMode2 { get; private set; }
        public uint AutoIdleMode { get; private set; }
        public uint TravelSpeed1 { get; private set; }
        public uint TravelSpeed2 { get; private set; }
        public uint TravelSpeed3 { get; private set; }
        public uint WaterTemp1 { get; private set; }
        public uint WaterTemp2 { get; private set; }
        public uint WaterTemp3 { get; private set; }
        public uint WaterTemp4 { get; private set; }
        public uint WaterTemp5 { get; private set; }
        public uint WaterTemp6 { get; private set; }
        public uint HydOilTemp1 { get; private set; }
        public uint HydOilTemp2 { get; private set; }
        public uint HydOilTemp3 { get; private set; }
        public uint HydOilTemp4 { get; private set; }
        public uint HydOilTemp5 { get; private set; }
        public uint HydOilTemp6 { get; private set; }

        public override void Unpackage()
        {
            base.Unpackage();
            var index = 2;
            TotalWorkTime = BitConverter.ToUInt32(_content, index);
            index += 4;
            DrivingTime = BitConverter.ToUInt32(_content, index);
            index += 4;
            WorkingTime = BitConverter.ToUInt32(_content, index);
            index += 4;
            EngSeppdHrs1 = BitConverter.ToUInt32(_content, index);
            index += 4;
            EngSpeedHrs2 = BitConverter.ToUInt32(_content, index);
            index += 4;
            PowerModeHrs1 = BitConverter.ToUInt32(_content, index);
            index += 4;
            PowerModeHrs2 = BitConverter.ToUInt32(_content, index);
            index += 4;
            WorkMode1 = BitConverter.ToUInt32(_content, index);
            index += 4;
            WorkMode2 = BitConverter.ToUInt32(_content, index);
            index += 4;
            AutoIdleMode = BitConverter.ToUInt32(_content, index);
            index += 4;
            TravelSpeed1 = BitConverter.ToUInt32(_content, index);
            index += 4;
            TravelSpeed2 = BitConverter.ToUInt32(_content, index);
            index += 4;
            TravelSpeed3 = BitConverter.ToUInt32(_content, index);
            index += 4;
            WaterTemp1 = BitConverter.ToUInt32(_content, index);
            index += 4;
            WaterTemp2 = BitConverter.ToUInt32(_content, index);
            index += 4;
            WaterTemp3 = BitConverter.ToUInt32(_content, index);
            index += 4;
            WaterTemp4 = BitConverter.ToUInt32(_content, index);
            index += 4;
            WaterTemp5 = BitConverter.ToUInt32(_content, index);
            index += 4;
            WaterTemp6 = BitConverter.ToUInt32(_content, index);
            index += 4;
            HydOilTemp1 = BitConverter.ToUInt32(_content, index);
            index += 4;
            HydOilTemp2 = BitConverter.ToUInt32(_content, index);
            index += 4;
            HydOilTemp3 = BitConverter.ToUInt32(_content, index);
            index += 4;
            HydOilTemp4 = BitConverter.ToUInt32(_content, index);
            index += 4;
            HydOilTemp5 = BitConverter.ToUInt32(_content, index);
            index += 4;
            HydOilTemp6 = BitConverter.ToUInt32(_content, index);
            index += 4;
        }
    }
    /// <summary>
    /// DX的0x6004命令
    /// </summary>
    public class _0x6004DX : Analyse
    {
        public uint TotalWorkTime { get; private set; }
        public uint PowerMode { get; private set; }
        public uint StandardMode { get; private set; }
        public uint DiggingMode { get; private set; }
        public uint TrenchingMode { get; private set; }
        public uint TravelSpeedI { get; private set; }
        public uint TravelSpeedII { get; private set; }
        public uint AutoIdleMode { get; private set; }
        public uint Travel { get; private set; }
        public uint Work { get; private set; }
        public uint PressUp { get; private set; }
        public uint Breaker { get; private set; }
        public uint Shear { get; private set; }
        public uint EngSpeed2000 { get; private set; }
        public uint EngSpeed1900 { get; private set; }
        public uint EngSpeed1800 { get; private set; }
        public uint EngSpeed1700 { get; private set; }
        public uint EngSpeed1600 { get; private set; }
        public uint EngSpeed1200 { get; private set; }
        public uint EngSpeed1200D { get; private set; }
        public uint HydOilTemp30 { get; private set; }
        public uint HydOilTemp31 { get; private set; }
        public uint HydOilTemp51 { get; private set; }
        public uint HydOilTemp76 { get; private set; }
        public uint HydOilTemp86 { get; private set; }
        public uint HydOilTemp96 { get; private set; }
        public uint CoolantTemp40 { get; private set; }
        public uint CoolantTemp41 { get; private set; }
        public uint CoolantTemp61 { get; private set; }
        public uint CoolantTemp86 { get; private set; }
        public uint CoolantTemp96 { get; private set; }
        public uint CoolantTemp106 { get; private set; }
        public uint TotalFuel { get; private set; }
        public uint PowerFuel { get; private set; }
        public uint StandardFuel { get; private set; }
        /// <summary>
        /// 拆包
        /// </summary>
        public override void Unpackage()
        {
            base.Unpackage();
            var index = 5;
            TotalWorkTime = BitConverter.ToUInt32(_content, index);
            index += 4;
            PowerMode = BitConverter.ToUInt32(_content, index);
            index += 4;
            StandardMode = BitConverter.ToUInt32(_content, index);
            index += 4;
            DiggingMode = BitConverter.ToUInt32(_content, index);
            index += 4;
            TrenchingMode = BitConverter.ToUInt32(_content, index);
            index += 4;
            TravelSpeedI = BitConverter.ToUInt32(_content, index);
            index += 4;
            TravelSpeedII = BitConverter.ToUInt32(_content, index);
            index += 4;
            AutoIdleMode = BitConverter.ToUInt32(_content, index);
            index += 4;
            Travel = BitConverter.ToUInt32(_content, index);
            index += 4;
            Work = BitConverter.ToUInt32(_content, index);
            index += 4;
            PressUp = BitConverter.ToUInt32(_content, index);
            index += 4;
            Breaker = BitConverter.ToUInt32(_content, index);
            index += 4;
            Shear = BitConverter.ToUInt32(_content, index);
            index += 4;
            EngSpeed2000 = BitConverter.ToUInt32(_content, index);
            index += 4;
            EngSpeed1900 = BitConverter.ToUInt32(_content, index);
            index += 4;
            EngSpeed1800 = BitConverter.ToUInt32(_content, index);
            index += 4;
            EngSpeed1700 = BitConverter.ToUInt32(_content, index);
            index += 4;
            EngSpeed1600 = BitConverter.ToUInt32(_content, index);
            index += 4;
            EngSpeed1200 = BitConverter.ToUInt32(_content, index);
            index += 4;
            EngSpeed1200D = BitConverter.ToUInt32(_content, index);
            index += 4;
            HydOilTemp30 = BitConverter.ToUInt32(_content, index);
            index += 4;
            HydOilTemp31 = BitConverter.ToUInt32(_content, index);
            index += 4;
            HydOilTemp51 = BitConverter.ToUInt32(_content, index);
            index += 4;
            HydOilTemp76 = BitConverter.ToUInt32(_content, index);
            index += 4;
            HydOilTemp86 = BitConverter.ToUInt32(_content, index);
            index += 4;
            HydOilTemp96 = BitConverter.ToUInt32(_content, index);
            index += 4;
            CoolantTemp40 = BitConverter.ToUInt32(_content, index);
            index += 4;
            CoolantTemp41 = BitConverter.ToUInt32(_content, index);
            index += 4;
            CoolantTemp61 = BitConverter.ToUInt32(_content, index);
            index += 4;
            CoolantTemp86 = BitConverter.ToUInt32(_content, index);
            index += 4;
            CoolantTemp96 = BitConverter.ToUInt32(_content, index);
            index += 4;
            CoolantTemp106 = BitConverter.ToUInt32(_content, index);
            index += 4;
            TotalFuel = BitConverter.ToUInt32(_content, index);
            index += 4;
            PowerFuel = BitConverter.ToUInt32(_content, index);
            index += 4;
            StandardFuel = BitConverter.ToUInt32(_content, index);
            index += 4;
        }
    }
    /// <summary>
    /// 0x6005命令：滤油信息
    /// </summary>
    public class _0x6005 : Analyse
    {
        /// <summary>
        /// 燃油滤清器
        /// </summary>
        public uint FuelFilter { get; private set; }
        /// <summary>
        /// 空气滤清器
        /// </summary>
        public uint AirFilter { get; private set; }
        /// <summary>
        /// 发动机机油滤清器
        /// </summary>
        public uint EngOilFilter { get; private set; }
        /// <summary>
        /// 回油滤清器
        /// </summary>
        public uint ReturnFilter { get; private set; }
        /// <summary>
        /// 先导滤清器
        /// </summary>
        public uint PilotFilter { get; private set; }
        /// <summary>
        /// 发动机机油使用时间
        /// </summary>
        public uint EngOil { get; private set; }
        /// <summary>
        /// 液压油使用时间
        /// </summary>
        public uint HydOil { get; private set; }
        /// <summary>
        /// 冷却液使用时间
        /// </summary>
        public uint Coolant { get; private set; }
        /// <summary>
        /// 解包滤油信息数据包0x6005
        /// </summary>
        public override void Unpackage()
        {
            base.Unpackage();
            var index = 2;
            if (DataType == doosan.DX)
                index = 4;

            FuelFilter = BitConverter.ToUInt32(_content, index);
            index += 4;
            AirFilter = BitConverter.ToUInt32(_content, index);
            index += 4;
            EngOilFilter = BitConverter.ToUInt32(_content, index);
            index += 4;
            ReturnFilter = BitConverter.ToUInt32(_content, index);
            index += 4;
            PilotFilter = BitConverter.ToUInt32(_content, index);
            index += 4;
            EngOil = BitConverter.ToUInt32(_content, index);
            index += 4;
            HydOil = BitConverter.ToUInt32(_content, index);
            index += 4;
            Coolant = BitConverter.ToUInt32(_content, index);
            index += 4;
        }
    }
    /// <summary>
    /// 0x6007保安命令
    /// </summary>
    public class _0x6007 : Analyse
    {
        private byte _code;
        /// <summary>
        /// 当前安保状态
        /// </summary>
        public string Security
        {
            get
            {
                var ret = "";
                switch (_code)
                {
                    case 0x00: ret = "Enable/Clear"; break;
                    case 0x10: ret = "Disabled"; break;
                    case 0x20: ret = "Agent Lock"; break;
                    case 0x40: ret = "Full Lock"; break;
                    default: ret = "Unknown(0x" + ProtocolItems.GetHex(_code) + ")"; break;
                }
                return ret;
            }
        }
        /// <summary>
        /// 解包0x6007命令
        /// </summary>
        public override void Unpackage()
        {
            base.Unpackage();
            _code = _content[DataType == doosan.DH ? 0 : 5];
        }
    }
}
