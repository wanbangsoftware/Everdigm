using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wbs.Utilities;

namespace Wbs.Everdigm.Desktop
{
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
            var len = data.Length;
            Length += (ushort)data.Length;
            content = CustomConvert.expand(content, Length + Iridium.HEADER_SIZE);
            var total = content.Length;
            Buffer.BlockCopy(data, 0, content, total - len, len);
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
            return string.Format("IEI: {0}({1}), Length: {2}", CustomConvert.GetHex(IEI), GetIEI(), Length);
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
            get { return CustomConvert.JavascriptDateToDateTime(TimeOfSession); }
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
            return string.Format("{0}, CDR: {1}, IMEI: {2}, Status: {3}, MOMSN: {4}, MTMSN: {5}, TimeOfSession: {6}, Time:",
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
            return string.Format("{0}, Payload: {1}", base.ToString(), CustomConvert.GetHex(Payload));
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
        /// <summary>
        /// 解包经纬度信息
        /// </summary>
        private void unpackage()
        {
            var nsew = CustomConvert.IntToDigit(LatLng[0], CustomConvert.BIN, 8);
            NSI = nsew[6] == '1' ? 'S' : 'N';
            EWI = nsew[7] == '1' ? 'W' : 'E';

            Latitude = LatLng[1];
            byte[] tmp = new byte[2];
            Buffer.BlockCopy(LatLng, 2, tmp, 0, 2);
            tmp = CustomConvert.reserve(tmp);
            double mm = double.Parse("0." + BitConverter.ToUInt16(tmp, 0));
            Latitude += mm;

            Longitude = LatLng[4];
            Buffer.BlockCopy(LatLng, 5, tmp, 0, 2);
            tmp = CustomConvert.reserve(tmp);
            mm = double.Parse("0." + BitConverter.ToUInt16(tmp, 0));
            Longitude += mm;
        }
        /// <summary>
        /// 解包
        /// </summary>
        public override void Unpackage()
        {
            base.Unpackage();

            LatLng = new byte[7];
            Buffer.BlockCopy(Content, index, LatLng, 0, 7);
            unpackage();
            index += 7;

            byte[] temp = new byte[4];
            Buffer.BlockCopy(Content, index, temp, 0, 4);
            temp = CustomConvert.reserve(temp);
            CEPRadius = BitConverter.ToUInt32(temp, 0);
        }
        public override string ToString()
        {
            return string.Format("{0}, LatLng: {1}, Latitude: {2}, NSI: {3}, Longitude: {4}, EWI: {5}, CEPRadius: {6}",
                base.ToString(), CustomConvert.GetHex(LatLng), Latitude.ToString(), NSI, Longitude.ToString(), EWI, CEPRadius);
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
        public override string ToString()
        {
            return base.ToString();
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
            Package(temp);

            temp = ASCIIEncoding.ASCII.GetBytes(IMEI);
            temp = CustomConvert.expand(temp, IMEI_LEN);
            Package(temp);

            temp = GetBytes(DispositionFlags);
            Package(temp);

            PackageLength();
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
        public byte[] Payload { get; set; }

        public override void Package()
        {
            base.Package();
            Package(Payload);

            PackageLength();
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
    }
}
