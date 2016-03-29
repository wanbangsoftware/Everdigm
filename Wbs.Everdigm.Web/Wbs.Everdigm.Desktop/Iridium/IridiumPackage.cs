using System;
using System.Text;

using Wbs.Utilities;
using System.Globalization;

namespace Wbs.Everdigm.Desktop
{
    /// <summary>
    /// MT时计算MTMSN的方法
    /// </summary>
    public class IririumMTMSN
    {
        /// <summary>
        /// 计算指定日期的MTMSN
        /// </summary>
        /// <param name="date">当前日期</param>
        /// <param name="ExistsNumber">已存在了的号码</param>
        /// <returns></returns>
        public static ushort CalculateMTMSN(DateTime date, ushort ExistsNumber)
        {
            var month = CustomConvert.IntToDigit(date.Month, CustomConvert.BIN, 4);
            var num = CustomConvert.IntToDigit((ExistsNumber + 1) % 4096, CustomConvert.BIN, 12);
            var n = CustomConvert.DigitToInt(month + num, CustomConvert.BIN);
            return (ushort)n;
        }
    }
    /// <summary>
    /// 铱星数据包
    /// </summary>
    public class Iridium
    {
        /// <summary>
        /// 包头长度
        /// </summary>
        public const byte HEADER_SIZE = 3;
        /// <summary>
        /// 初始化铱星数据包
        /// </summary>
        public Iridium()
        {
            ProtocolRevisionNumber = 0x01;
            OverallMessageLength = 0;
            // 初始化数据包为3byte
            package = new byte[HEADER_SIZE];
        }
        /// <summary>
        /// 协议校定版本
        /// </summary>
        public byte ProtocolRevisionNumber { get; set; }
        protected int index;
        /// <summary>
        /// 整包数据的长度
        /// </summary>
        public ushort OverallMessageLength { get; set; }

        protected void SetValue(byte[] data, out byte[] target)
        {
            if (null == data) target = null;
            else
            {
                var len = data.Length;
                target = new byte[len];
                Buffer.BlockCopy(data, 0, target, 0, len);
            }
        }

        private byte[] package = null;
        /// <summary>
        /// 整个package内容
        /// </summary>
        public byte[] PackageContent
        {
            get { return package; }
            set
            {
                SetValue(value, out package);
            }
        }
        private byte[] content = null;
        /// <summary>
        /// 数据内容
        /// </summary>
        public byte[] Content
        {
            get { return content; }
            set { SetValue(value, out content); }
        }
        /// <summary>
        /// 打包过程
        /// </summary>
        public virtual void Package()
        {
            package[0] = ProtocolRevisionNumber;

            // 打包内容
            Package(content);

            PackageLength();
        }
        /// <summary>
        /// 加入整包长度数据
        /// </summary>
        public void PackageLength()
        {
            var data = CustomConvert.reserve(BitConverter.GetBytes(OverallMessageLength));
            Buffer.BlockCopy(data, 0, package, 1, 2);
        }
        /// <summary>
        /// 解包过程
        /// </summary>
        public virtual void Unpackage()
        {
            if (package.Length < 3)
                throw new MissingMemberException("Not enough members.");
            index = 0;
            ProtocolRevisionNumber = package[index];
            index += 1;

            var temp = new byte[2];
            Buffer.BlockCopy(package, index, temp, 0, 2);
            temp = CustomConvert.reserve(temp);
            OverallMessageLength = BitConverter.ToUInt16(temp, 0);
            index += 2;

            if (package.Length < OverallMessageLength + HEADER_SIZE)
                throw new MissingFieldException("Not enough fields.");

            content = new byte[OverallMessageLength];
            Buffer.BlockCopy(package, index, content, 0, OverallMessageLength);
            index += OverallMessageLength;
        }
        /// <summary>
        /// 打包一个字节数据到package中
        /// </summary>
        /// <param name="data"></param>
        public void Package(byte data)
        {
            Package(new byte[] { data });
        }
        /// <summary>
        /// 打包一个字节数组到package中
        /// </summary>
        /// <param name="data"></param>
        public void Package(byte[] data)
        {
            var len = data.Length;
            OverallMessageLength += (ushort)len;
            package = CustomConvert.expand(package, OverallMessageLength + HEADER_SIZE);
            Buffer.BlockCopy(data, 0, package, package.Length - len, len);
        }
    }
    /// <summary>
    /// InformationElement
    /// </summary>
    public class IE
    {
        /// <summary>
        /// MO头标记
        /// </summary>
        public const byte MO_HEADER = 0x01;
        /// <summary>
        /// MO数据体标记
        /// </summary>
        public const byte MO_PAYLOAD = 0x02;
        /// <summary>
        /// MO位置信息标记
        /// </summary>
        public const byte MO_LOCATION = 0x03;
        /// <summary>
        /// MO回执标记
        /// </summary>
        public const byte MO_CONFIRMATION = 0x05;
        /// <summary>
        /// MT头标记
        /// </summary>
        public const byte MT_HEADER = 0x41;
        /// <summary>
        /// MT数据体标记
        /// </summary>
        public const byte MT_PAYLOAD = 0x42;
        /// <summary>
        /// MT回执标记
        /// </summary>
        public const byte MT_CONFIRMATION = 0x44;
        /// <summary>
        /// MT优先级标记
        /// </summary>
        public const byte MT_PRIORITY = 0x46;
        /// <summary>
        /// IMEI长度
        /// </summary>
        public const byte IMEI_LEN = 15;

        public string GetIEI()
        {
            var ret = "";
            switch (IEI)
            {
                case MO_CONFIRMATION: ret = "MO Confirmation"; break;
                case MO_HEADER: ret = "MO Header"; break;
                case MO_LOCATION: ret = "MO Location"; break;
                case MO_PAYLOAD: ret = "MO Payload"; break;
                case MT_CONFIRMATION: ret = "MT Confirmation"; break;
                case MT_HEADER: ret = "MT Header"; break;
                case MT_PAYLOAD: ret = "MT Payload"; break;
                case MT_PRIORITY: ret = "MT Priority"; break;
            }
            return ret;
        }

        public IE()
        {
            Length = 0;
            content = new byte[Iridium.HEADER_SIZE];
        }
        /// <summary>
        /// 生成一个新的IE实体并指定IEI
        /// </summary>
        /// <param name="iei"></param>
        public IE(byte iei)
        {
            IEI = iei;
            Length = 0;
            content = new byte[Iridium.HEADER_SIZE];
        }
        /// <summary>
        /// IE Identifiers
        /// </summary>
        public byte IEI { get; set; }
        /// <summary>
        /// IE的长度
        /// </summary>
        public ushort Length { get; set; }
        protected void SetValue(byte[] data, out byte[] target)
        {
            if (null == data) target = null;
            else
            {
                var len = data.Length;
                target = new byte[len];
                Buffer.BlockCopy(data, 0, target, 0, len);
            }
        }
        protected int index = 0;
        private byte[] content = null;
        /// <summary>
        /// 数据整包内容
        /// </summary>
        public byte[] Content
        {
            get { return content; }
            set { SetValue(value, out content); }
        }
        //private byte[] data = null;
        ///// <summary>
        ///// 具体数据内容
        ///// </summary>
        //public byte[] Data
        //{
        //    set { SetValue(value, data); }
        //    get { return data; }
        //}
        public void Package(byte data)
        {
            Package(new byte[] { data });
        }
        public void Package(byte[] data)
        {
            var len = null == data ? 0 : data.Length;
            Length += (ushort)len;
            content = CustomConvert.expand(content, Length + Iridium.HEADER_SIZE);
            var total = content.Length;
            if (null != data)
            {
                Buffer.BlockCopy(data, 0, content, total - len, len);
            }
        }
        /// <summary>
        /// 打包长度
        /// </summary>
        public void PackageLength()
        {
            var data = GetBytes(Length);
            Buffer.BlockCopy(data, 0, content, 1, 2);
        }
        /// <summary>
        /// 打包
        /// </summary>
        public virtual void Package()
        {
            content[0] = IEI;
            PackageLength();
            index = 3;
        }
        /// <summary>
        /// 解包
        /// </summary>
        public virtual void Unpackage()
        {
            IEI = content[0];

            Buffer.BlockCopy(content, 1, two, 0, 2);
            two = CustomConvert.reserve(two);
            Length = BitConverter.ToUInt16(two, 0);
            index = 3;
        }
        /// <summary>
        /// 双字节数据
        /// </summary>
        protected byte[] two = new byte[2];
        /// <summary>
        /// 获取双字节数据
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        public ushort GetUshort(int start)
        {
            Buffer.BlockCopy(content, start, two, 0, 2);
            two = CustomConvert.reserve(two);
            return BitConverter.ToUInt16(two, 0);
        }
        /// <summary>
        /// 获取ushort的字节流，低位在前
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public byte[] GetBytes(ushort value)
        {
            return CustomConvert.reserve(BitConverter.GetBytes(value));
        }
        /// <summary>
        /// 四字节数据
        /// </summary>
        protected byte[] four = new byte[4];
        /// <summary>
        /// 获取uint数据
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        public uint GetUint(int start)
        {
            Buffer.BlockCopy(content, start, four, 0, 4);
            four = CustomConvert.reserve(four);
            return BitConverter.ToUInt32(four, 0);
        }
        /// <summary>
        /// 获取四字节int类型的二进制
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public byte[] GetBytes(uint value)
        {
            return CustomConvert.reserve(BitConverter.GetBytes(value));
        }
        public override string ToString()
        {
            return format("IEI: {0}({1}), Length: {2}", CustomConvert.GetHex(IEI), GetIEI(), Length);
        }
        protected string format(string format, params object[] args)
        {
            return DataHandler.format(format, args);
        }
    }
    /// <summary>
    /// MO Header
    /// </summary>
    public class MOHeader : IE
    {
        /// <summary>
        /// Auto ID
        /// </summary>
        public uint CDR { get; set; }
        /// <summary>
        /// 终端的IMEI
        /// </summary>
        public string IMEI { get; set; }
        /// <summary>
        /// 状态码
        /// </summary>
        public byte SessionStatus { get; set; }
        /// <summary>
        /// MO序列流水号
        /// </summary>
        public ushort MOMSN { get; set; }
        /// <summary>
        /// MT序列流水号
        /// </summary>
        public ushort MTMSN { get; set; }
        /// <summary>
        /// 时间戳，自1970-01-01 00:00:00起始的毫秒数
        /// </summary>
        public uint TimeOfSession { get; set; }
        /// <summary>
        /// DateTime
        /// </summary>
        public DateTime Time
        {
            get
            {
                var date = new DateTime(1970, 1, 1);
                return date.AddSeconds(TimeOfSession).ToLocalTime();
                // return CustomConvert.JavascriptDateToDateTime(TimeOfSession); 
            }
            set { TimeOfSession = (uint)CustomConvert.DateTimeToJavascriptDate(value); }
        }
        /// <summary>
        /// 解包
        /// </summary>
        public override void Unpackage()
        {
            base.Unpackage();
            if (IEI != MO_HEADER)
                throw new Exception("Can not analyse MO_HEADER with IEI: " + IEI);

            CDR = GetUint(index);
            index += 4;

            IMEI = ASCIIEncoding.ASCII.GetString(Content, index, IMEI_LEN);
            index += IMEI_LEN;

            SessionStatus = Content[index];
            index += 1;

            MOMSN = GetUshort(index);
            index += 2;

            MTMSN = GetUshort(index);
            index += 2;

            TimeOfSession = GetUint(index);
            index += 4;
        }
        public override string ToString()
        {
            return format("{0}, CDR: {1}, IMEI: {2}, Status: {3}, MOMSN: {4}, MTMSN: {5}, TimeOfSession: {6}, Time: {7}",
                base.ToString(), CDR, IMEI, SessionStatus, MOMSN, MTMSN, TimeOfSession, Time.ToString("yyyy-MM-dd HH:mm:ss")); ;
        }
    }
    /// <summary>
    /// MO Payload 数
    /// </summary>
    public class MOPayload : IE
    {
        public byte[] Payload { get; private set; }

        /// <summary>
        /// 解包
        /// </summary>
        public override void Unpackage()
        {
            base.Unpackage();
            if (IEI != MO_PAYLOAD)
                throw new Exception("Can not analyse MO_PAYLOAD with IEI: " + IEI);

            Payload = new byte[Length];
            Buffer.BlockCopy(Content, index, Payload, 0, Length);
            index += Length;
        }
        public override string ToString()
        {
            return format("{0}, Payload: {1}", base.ToString(), CustomConvert.GetHex(Payload));
        }
    }
    /// <summary>
    /// 铱星定位信息
    /// </summary>
    public class IridiumLocation : IDisposable
    {
        public const int SIZE = 7;
        private byte[] latlng = null;
        public byte[] LatLng
        {
            get { return latlng; }
            set
            {
                if (null == value || value.Length < SIZE)
                    latlng = null;
                else
                {
                    latlng = new byte[SIZE];
                    Buffer.BlockCopy(value, 0, latlng, 0, SIZE);
                }
            }
        }
        /// <summary>
        /// 标记本条GPS信息是否有效
        /// </summary>
        public bool Available
        {
            get
            {
                // 0<=经度值<=90 且 0<=维度值<=180
                return (Latitude > 0 && Latitude < 90) && (Longitude > 0 && Longitude < 180);
            }
        }
        /// <summary>
        /// 纬度
        /// </summary>
        public double Latitude { get; private set; }
        /// <summary>
        /// 南北指示
        /// </summary>
        public char NSI { get; private set; }
        /// <summary>
        /// 经度
        /// </summary>
        public double Longitude { get; private set; }
        /// <summary>
        /// 东西指示
        /// </summary>
        public char EWI { get; private set; }
        private byte report = 0;
        /// <summary>
        /// 获取卫星数据的汇报方式
        /// </summary>
        public string Report
        {
            get { return report == 0 ? "Period" : "Command"; }
        }
        /// <summary>
        /// 信息汇报方式0=自动汇报1=命令回复
        /// </summary>
        public byte ReportType { get { return report; } }
        private byte eng = 0;
        /// <summary>
        /// 获取Eng状态
        /// </summary>
        public string EngFlag
        { get { return eng == 0 ? "Off" : "On"; } }
        /// <summary>
        /// 解包定位信息
        /// </summary>
        public void Unpackate()
        {
            var nsew = CustomConvert.IntToDigit(latlng[0], CustomConvert.BIN, 8);
            //nsew = CustomConvert.reserve(nsew);
            report = (byte)(nsew[7] == '1' ? 1 : 0);
            eng = (byte)(nsew[6] == '1' ? 1 : 0);
            NSI = nsew[1] == '1' ? 'S' : 'N';
            EWI = nsew[0] == '1' ? 'W' : 'E';

            Latitude = latlng[1];
            byte[] tmp = new byte[2];
            Buffer.BlockCopy(latlng, 2, tmp, 0, 2);
            //tmp = CustomConvert.reserve(tmp);

            var u16 = BitConverter.ToUInt16(tmp, 0);
            double mm = u16 / Math.Pow(10, u16.ToString().Length);
            // 将ddmmmm转换成dd.dddd
            //mm = Wbs.Protocol.TX300.Analyse.GPSInfo.DDMM2DDDD(mm);
            Latitude += mm;

            Longitude = latlng[4];
            Buffer.BlockCopy(latlng, 5, tmp, 0, 2);
            //tmp = CustomConvert.reserve(tmp);
            u16 = BitConverter.ToUInt16(tmp, 0);
            mm = u16 / Math.Pow(10, u16.ToString().Length);
            // 将ddmmmm转换成dd.dddd
            //mm = Wbs.Protocol.TX300.Analyse.GPSInfo.DDMM2DDDD(mm);
            Longitude += mm;
        }

        public void Dispose()
        {
            latlng = null;
        }
    }
    /// <summary>
    /// MO Location定位信息
    /// </summary>
    public class MOLocation : IE
    {
        public byte[] LatLng { get; private set; }
        public uint CEPRadius { get; private set; }
        /// <summary>
        /// 定位信息
        /// </summary>
        public IridiumLocation Location { get; private set; }
        
        /// <summary>
        /// 解包
        /// </summary>
        public override void Unpackage()
        {
            base.Unpackage();

            Location = new IridiumLocation();
            Location.LatLng = new byte[IridiumLocation.SIZE];
            Buffer.BlockCopy(Content, index, Location.LatLng, 0, IridiumLocation.SIZE);
            Location.Unpackate();
            index += IridiumLocation.SIZE;

            byte[] temp = new byte[4];
            Buffer.BlockCopy(Content, index, temp, 0, 4);
            temp = CustomConvert.reserve(temp);
            CEPRadius = BitConverter.ToUInt32(temp, 0);
        }
        public override string ToString()
        {
            return format("{0}, LatLng: {1}, Latitude: {2}, NSI: {3}, Longitude: {4}, EWI: {5}, CEPRadius: {6}",
                base.ToString(), CustomConvert.GetHex(LatLng), Location.Latitude.ToString(CultureInfo.InvariantCulture.NumberFormat),
                Location.NSI, Location.Longitude.ToString(CultureInfo.InvariantCulture.NumberFormat), Location.EWI, CEPRadius);
        }
    }
    /// <summary>
    /// MO Confirmation
    /// </summary>
    public class MOConfirmation : IE
    {
        public MOConfirmation()
        {
            IEI = MO_CONFIRMATION;
            Status = 1;
        }
        /// <summary>
        /// MO 接收状态
        /// </summary>
        public byte Status { get; set; }
        /// <summary>
        /// 打包
        /// </summary>
        public override void Package()
        {
            base.Package();

            Package(Status);

            PackageLength();
        }
        public override void Unpackage()
        {
            base.Unpackage();

            Status = Content[index];
            index += 1;
        }
        public override string ToString()
        {
            return format("{0}, Status: {1}", base.ToString(), Status);
        }
    }
    /// <summary>
    /// MT 标头
    /// </summary>
    public class MTHeader : IE
    {
        public MTHeader()
        {
            IEI = MT_HEADER;
        }
        /// <summary>
        /// 发送方流水号
        /// </summary>
        public uint UniqueID { get; set; }
        /// <summary>
        /// 目的设备的IMEI
        /// </summary>
        public string IMEI { get; set; }

        public ushort DispositionFlags { get; set; }
        /// <summary>
        /// 组包
        /// </summary>
        public override void Package()
        {
            base.Package();

            byte[] temp = GetBytes(UniqueID);
            //temp = CustomConvert.reserve(temp);
            temp[0] = 0;
            temp[1] = 0;
            Package(temp);

            temp = ASCIIEncoding.ASCII.GetBytes(IMEI);
            temp = CustomConvert.expand(temp, IMEI_LEN);
            Package(temp);

            temp = GetBytes(DispositionFlags);
            Package(temp);

            PackageLength();
        }
        public override void Unpackage()
        {
            base.Unpackage();

            UniqueID = GetUint(index);
            index += 4;

            IMEI = ASCIIEncoding.ASCII.GetString(Content, index, IMEI_LEN);
            index += IMEI_LEN;

            DispositionFlags = GetUshort(index);
            index += 2;
        }
        public override string ToString()
        {
            return format("{0}, UniqueID: {1}, IMEI: {2}, DispositionFlags: {3}({4})", 
                base.ToString(), UniqueID, IMEI, DispositionFlags, CustomConvert.IntToDigit(DispositionFlags, CustomConvert.HEX, 4));
        }
    }
    /// <summary>
    /// MT Payload 数据
    /// </summary>
    public class MTPayload : IE
    {
        public MTPayload()
        {
            IEI = MT_PAYLOAD;
        }
        private byte[] payload = null;
        public byte[] Payload
        {
            get { return payload; }
            set
            {
                if (null == value)
                {
                    payload = null;
                }
                else
                {
                    payload = new byte[value.Length];
                    Buffer.BlockCopy(value, 0, payload, 0, value.Length);
                }
            }
        }

        public override void Package()
        {
            base.Package();
            Package(Payload);

            PackageLength();
        }
        public override void Unpackage()
        {
            base.Unpackage();

            payload = new byte[Length];
            Buffer.BlockCopy(Content, index, payload, 0, Length);
            index += Length;
        }
        public override string ToString()
        {
            return format("{0}, Payload: {1}", base.ToString(), CustomConvert.GetHex(Payload));
        }
    }
    /// <summary>
    /// MT Confirmation
    /// </summary>
    public class MTConfirmation : IE
    {
        public uint UniqueID { get; set; }
        public string IMEI { get; set; }
        public uint AutoID { get; set; }
        public short Status { get; set; }

        public override void Unpackage()
        {
            base.Unpackage();

            byte[] temp = new byte[4];
            Buffer.BlockCopy(Content, index, temp, 0, 4);
            temp = CustomConvert.reserve(temp);
            UniqueID = BitConverter.ToUInt32(temp, 0);
            index += 4;

            IMEI = ASCIIEncoding.ASCII.GetString(Content, index, IMEI_LEN);
            index += IMEI_LEN;

            Buffer.BlockCopy(Content, index, temp, 0, 4);
            temp = CustomConvert.reserve(temp);
            AutoID = BitConverter.ToUInt32(temp,0);
            index += 4;

            Buffer.BlockCopy(Content, index, temp, 0, 2);
            temp = CustomConvert.reserve(temp);
            Status = BitConverter.ToInt16(temp, 2);
            index += 2;
        }
        public override string ToString()
        {
            return format("{0}, UniqueID: {1}, IMEI: {2}, AutoID: {3}, Status: {4}({5})",
                base.ToString(), UniqueID, IMEI, AutoID, Status, GetStatus());
        }
        public string GetStatus()
        { return GetStatus(Status); }
        public string GetStatus(short status)
        {
            if (status > 0)
                return format("Successful, order of message in the MT message queue: {0}", status);
            var ret="";
            switch (status)
            {
                case -11: ret = "MTMSN value is out of range(1-65535)"; break;
                case -10: ret = "Source IP address rejected by MT filter"; break;
                case -9: ret = "The given IMEI is not attached"; break;
                case -8: ret = "Ring alerts to the given IMEI are disabled"; break;
                case -7: ret = "Violation of MT DirectIP protocol error"; break;
                case -6: ret = "MT resources unavailable"; break;
                case -5: ret = "MT message queue full (max of 50)"; break;
                case -4: ret = "Payload expected, but none received"; break;
                case -3: ret = "Payload size exceeded maximum allowed"; break;
                case -2: ret = "Unknown IMEI - not provisioned on the Iridium Gateway"; break;
                case -1: ret = "Invalid IMEI – too few characters, non-numeric characters"; break;
                case 0: ret = "Successful, no payload in message"; break;
            }
            return ret;
        }
    }
}
