using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Wbs.Utilities;

namespace Satellite
{
    /// <summary>
    /// 卫星通信数据包
    /// </summary>
    public class SatellitePackage
    {
        /// <summary>
        /// 命令字长度固定为5
        /// </summary>
        protected static int CommandSize = 5;
        /// <summary>
        /// 用户地址长度固定为3
        /// </summary>
        protected static int UserAddressSize = 3;
        /// <summary>
        /// 命令字
        /// </summary>
        public string Command { get; set; }
        /// <summary>
        /// 数据总长度(总字节数量)
        /// </summary>
        public ushort TotalSize { get; set; }

        private byte[] _origin = new byte[UserAddressSize];
        /// <summary>
        /// 用户地址的二进制表示方式
        /// </summary>
        public byte[] Origin { get { return _origin; } }
        /// <summary>
        /// 将3byte的用户地址转换成字符串形式的用户地址
        /// </summary>
        /// <param name="addr"></param>
        /// <returns></returns>
        protected string GetUserAddress(byte[] addr)
        {
            var data = new byte[4];
            Buffer.BlockCopy(addr, 0, data, 1, UserAddressSize);
            data = CustomConvert.reserve(data);
            var num = BitConverter.ToUInt32(data, 0);
            return num.ToString();
        }
        /// <summary>
        /// 将字符串形式的用户地址转换成3byte的用户地址
        /// </summary>
        /// <param name="addr"></param>
        /// <returns></returns>
        protected byte[] GetUserAddress(string addr)
        {
            var num = uint.Parse(addr);
            var data = BitConverter.GetBytes(num);
            data = CustomConvert.reserve(data);
            var b = new byte[UserAddressSize];
            Buffer.BlockCopy(data, 1, b, 0, UserAddressSize);
            data = null;
            return b;
        }
        /// <summary>
        /// 用户地址的字符串表示方式
        /// </summary>
        public string OriginAddress {
            get { return GetUserAddress(_origin); }
            set { _origin = GetUserAddress(value); }
        }
        /// <summary>
        /// 数据包整体内容
        /// </summary>
        private byte[] _content = null;
        /// <summary>
        /// 数据包package整体内容
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
                }
            }
        }
        /// <summary>
        /// 数据内容
        /// </summary>
        private byte[] _data = null;
        /// <summary>
        /// 数据内容
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
                }
            }
        }
        /// <summary>
        /// 异或校验和
        /// </summary>
        public byte Xor { get; set; }
        /// <summary>
        /// 生成一个新的卫星通讯数据包实体
        /// </summary>
        public SatellitePackage() { }
        /// <summary>
        /// 生成一个新的卫星通讯数据包实体并指定命令字
        /// </summary>
        /// <param name="command"></param>
        public SatellitePackage(string command)
        { Command = command; }
        /// <summary>
        /// 生成一个新的卫星通信数据包实体并制定数据包内容以便拆解
        /// </summary>
        /// <param name="data"></param>
        public SatellitePackage(byte[] data)
        {
            Content = data;
            Unpackage();
        }
        /// <summary>
        /// 组合一个字节
        /// </summary>
        /// <param name="data"></param>
        protected void Package(byte data)
        { Package(new byte[] { data }); }

        /// <summary>
        /// 组合一个字节数组
        /// </summary>
        /// <param name="data"></param>
        protected void Package(byte[] data)
        {
            var old = 0;
            var len = data.Length;
            if (null == Content)
            {
                Content = new byte[len];
            }
            else
            {
                old = Content.Length;
                Content = CustomConvert.expand(Content, Content.Length + len);
            }
            Buffer.BlockCopy(data, 0, Content, old, len);
        }
        /// <summary>
        /// 打包数据长度
        /// </summary>
        /// <param name="len"></param>
        protected void PackageTotalSize(ushort len)
        {
            var data = BitConverter.GetBytes(len);
            data = CustomConvert.reserve(data);
            Buffer.BlockCopy(data, 0, Content, 5, 2);
        }
        /// <summary>
        /// 组合数据包
        /// </summary>
        public virtual void Package()
        {
            Content = null;

            byte[] data = ASCIIEncoding.ASCII.GetBytes(Command);
            if (data.Length != CommandSize)
                data = CustomConvert.expand(data, CommandSize);
            Package(data);

            data = BitConverter.GetBytes(TotalSize);
            data = CustomConvert.reserve(data);
            Package(data);

            Package(_origin);

            TotalSize = (ushort)Content.Length;
            PackageTotalSize(TotalSize);
            //Package()
        }
        /// <summary>
        /// 解析数据包
        /// </summary>
        public virtual void Unpackage()
        {
            if (null == _content) throw new Exception("Cannot unpackage null data.");
            var len = _content.Length;
            if (len < (CommandSize + UserAddressSize + Marshal.SizeOf(TotalSize))) throw new Exception("Data length is to short.");
            var index = 0;

            Command = ASCIIEncoding.ASCII.GetString(_content, index, CommandSize);
            index += CommandSize;

            var data = new byte[2];
            Buffer.BlockCopy(_content, index, data, 0, 2);
            data = CustomConvert.reserve(data);
            TotalSize = BitConverter.ToUInt16(data, 0);
            index += Marshal.SizeOf(TotalSize);

            _origin = new byte[UserAddressSize];
            Buffer.BlockCopy(_content, index, _origin, 0, UserAddressSize);
            index += UserAddressSize;

            // 数据长度=总长度-命令字长度-总长度size-用户地址长度-校验和长度
            _data = new byte[TotalSize - CommandSize - Marshal.SizeOf(TotalSize) - UserAddressSize - 1];
            Buffer.BlockCopy(_content, index, _data, 0, _data.Length);
            index += _data.Length;

            Xor = _content[index];
        }
        /// <summary>
        /// 将卫星通信数据包的各个字段显示成字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("数据指令：").Append(Command).Append(Environment.NewLine);
            sb.Append("数据长度：").Append(TotalSize).Append(Environment.NewLine);
            sb.Append("用户地址：").Append(OriginAddress).Append(Environment.NewLine);
            return sb.ToString();
        }
    }
    /// <summary>
    /// 通信申请信息类别
    /// </summary>
    public class TXSQ_Type
    {
        private byte _type = 0;
        /// <summary>
        /// 生成一个新的通信申请信息类别
        /// </summary>
        public TXSQ_Type() {
            Packet = (byte)CustomConvert.DigitToInt("010", CustomConvert.BIN);
            CipherCode = 0;
            CommunicateType = (byte)CustomConvert.DigitToInt("01", CustomConvert.BIN);
            TransferType = 1;
            Flag = 0;
        }
        /// <summary>
        /// 生成一个新的通信申请信息类别并制定类别字节以便拆解各个字段
        /// </summary>
        /// <param name="type"></param>
        public TXSQ_Type(byte type)
        {
            _type = type;
            Unpackage();
        }
        /// <summary>
        /// 获取或设置通信申请信息类别字节
        /// </summary>
        public byte Type
        {
            get
            {
                Package();
                return _type;
            }
            set
            {
                _type = value;
                Unpackage();
            }
        }
        /// <summary>
        /// 报文通信,3bit，固定为010
        /// </summary>
        public byte Packet { get; private set; }
        /// <summary>
        /// 密钥，1bit，固定为0
        /// </summary>
        public byte CipherCode { get; private set; }
        /// <summary>
        /// 通信类别，2bit，固定为01
        /// </summary>
        public byte CommunicateType { get; private set; }
        /// <summary>
        /// 传输方式，1bit，0=汉字1=代码
        /// </summary>
        public byte TransferType { get; set; }
        /// <summary>
        /// 标识，1bit，固定为0
        /// </summary>
        public byte Flag { get; private set; }
        /// <summary>
        /// 组包
        /// </summary>
        private void Package()
        {
            var bin = CustomConvert.BIN;
            var str = string.Format("{0}{1}{2}{3}{4}",
                CustomConvert.IntToDigit(Packet, bin, 3), CipherCode,
                CustomConvert.IntToDigit(CommunicateType, bin, 2), TransferType, Flag);
            _type = (byte)CustomConvert.DigitToInt(str, bin);
        }
        /// <summary>
        /// 拆解
        /// </summary>
        private void Unpackage()
        {
            var bin = CustomConvert.IntToDigit(_type, CustomConvert.BIN, 8);
            var str = bin.Substring(0, 3);
            Packet = (byte)CustomConvert.DigitToInt(str, CustomConvert.BIN);

            CipherCode = byte.Parse(bin.Substring(3, 1));

            str = bin.Substring(4, 2);
            CommunicateType = (byte)CustomConvert.DigitToInt(str, CustomConvert.BIN);

            TransferType = byte.Parse(bin.Substring(6, 1));

            Flag = byte.Parse(bin.Substring(7));
        }
        /// <summary>
        /// 将通信申请信息类别的各个字段转换成字符串显示
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var bin = CustomConvert.BIN;
            StringBuilder sb = new StringBuilder();
            sb.Append("\t报文通信：").Append(Packet).Append("\tBin：")
                .Append(CustomConvert.IntToDigit(Packet, bin, 3)).Append(Environment.NewLine);
            sb.Append("\t密　　钥：").Append(CipherCode).Append("\tBin：")
                .Append(CustomConvert.IntToDigit(CipherCode, bin, 1)).Append(Environment.NewLine);
            sb.Append("\t通信类别：").Append(CommunicateType).Append("\tBin：")
                .Append(CustomConvert.IntToDigit(CommunicateType, bin, 2)).Append(Environment.NewLine);
            sb.Append("\t传输方式：").Append(TransferType).Append("\tBin：")
                .Append(CustomConvert.IntToDigit(TransferType, bin, 1)).Append("(0汉字,1代码)").Append(Environment.NewLine);
            sb.Append("\t标　　志：").Append(Flag).Append("\tBin：")
                .Append(CustomConvert.IntToDigit(Flag, bin, 1)).Append(Environment.NewLine);
            return sb.ToString();
        }
    }
    /// <summary>
    /// 通信申请数据包
    /// </summary>
    public class TXSQ : SatellitePackage
    {
        /// <summary>
        /// 生成一个新的通信申请数据包实体
        /// </summary>
        public TXSQ()
            : base()
        {
            Command = "$TXSQ";
        }
        private TXSQ_Type _type = new TXSQ_Type();
        /// <summary>
        /// 通信申请信息类别
        /// </summary>
        public TXSQ_Type Type
        {
            get { return _type; }
            set { _type = value; }
        }
        /// <summary>
        /// 目的用户地址
        /// </summary>
        private byte[] _target = new byte[UserAddressSize];
        /// <summary>
        /// 目的用户地址的2进制表现形式
        /// </summary>
        public byte[] Target
        {
            get { return _target; }
        }
        /// <summary>
        /// 目的用户地址的字符串表现形式
        /// </summary>
        public string TargetAddress {
            get { return GetUserAddress(_target); }
            set { _target = GetUserAddress(value); }
        }
        /// <summary>
        /// 电文长度（2进制电文内容的长度，非字节长度）
        /// </summary>
        public ushort MessageSize { get; set; }
        /// <summary>
        /// 是否应答
        /// </summary>
        public byte Reply { get; set; }
        /// <summary>
        /// 电文内容
        /// </summary>
        private byte[] _message = null;
        /// <summary>
        /// 电文内容，最大1680bit（也即210Bytes）
        /// </summary>
        public byte[] Message
        {
            get { return _message; }
            set
            {
                if (null == value) { 
                    _message = null;
                    MessageSize = 0;
                }
                else
                {
                    var len = value.Length;
                    _message = new byte[len];
                    Buffer.BlockCopy(value, 0, _message, 0, len);
                    MessageSize = (ushort)(len * 8);
                }
            }
        }
        /// <summary>
        /// 解析通信申请数据包
        /// </summary>
        public override void Unpackage()
        {
            base.Unpackage();
            var index = 0;

            if (null == _type) _type = new TXSQ_Type();
            _type.Type = Data[index];
            index += 1;

            //_target = new byte[UserAddressSize];
            Buffer.BlockCopy(Data, index, _target, 0, UserAddressSize);
            index += UserAddressSize;

            var data = new byte[2];
            Buffer.BlockCopy(Data, index, data, 0, 2);
            data = CustomConvert.reserve(data);
            MessageSize = BitConverter.ToUInt16(data, 0);
            index += Marshal.SizeOf(MessageSize);

            Reply = Data[index];
            index += 1;

            var len = MessageSize / 8;
            _message = new byte[len];
            Buffer.BlockCopy(Data, index, _message, 0, len);
            index += len;
        }
        /// <summary>
        /// 组合通信申请包
        /// </summary>
        public override void Package()
        {
            base.Package();

            Package(_type.Type);

            Package(_target);

            var data = BitConverter.GetBytes(MessageSize);
            data = CustomConvert.reserve(data);
            Package(data);

            Package(Reply);

            Package(_message);

            TotalSize = (ushort)(Content.Length + 1);
            PackageTotalSize(TotalSize);

            Xor = CustomConvert.GetXor(Content);
            Package(Xor);
        }
        /// <summary>
        /// 将通信申请数据包各属性转换成字符串显示
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToString()).Append("信息类别：").Append(Environment.NewLine).Append(Type.ToString());
            sb.Append("目的地址：").Append(TargetAddress).Append(Environment.NewLine);
            sb.Append("电文长度：").Append(MessageSize).Append("\t字节长度：").Append(MessageSize / 8).Append(Environment.NewLine);
            sb.Append("是否应答：").Append(Reply).Append(Environment.NewLine);
            sb.Append("电文内容：").Append(CustomConvert.GetHex(Message)).Append(Environment.NewLine);
            sb.Append("校验和：").Append(Xor.ToString("X2")).Append(Environment.NewLine);
            return sb.ToString();
        }
    }
    /// <summary>
    /// 通信信息信息类别
    /// </summary>
    public class TXXX_Type
    {
        private byte _type;
        /// <summary>
        /// 生成一个新的通信信息类别实体
        /// </summary>
        public TXXX_Type()
        {
            Communicate = (byte)CustomConvert.DigitToInt("01", CustomConvert.BIN);
            MessageType = 1;
            Receipt = 0;
            CommunicateType = 0;
            CipherCode = 0;
            Allowance = (byte)CustomConvert.DigitToInt("00", CustomConvert.BIN);
        }
        /// <summary>
        /// 生成一个新的通信信息类别实体并指定信息类别
        /// </summary>
        /// <param name="b"></param>
        public TXXX_Type(byte b)
        {
            _type = b;
            Unpackage();
        }
        /// <summary>
        /// 通信，总是固定为01
        /// </summary>
        public byte Communicate { get; private set; }
        /// <summary>
        /// 电文形式：0=汉子1=代码
        /// </summary>
        public byte MessageType { get; set; }
        /// <summary>
        /// 是否回执：固定为0
        /// </summary>
        public byte Receipt { get; private set; }
        /// <summary>
        /// 通信方式：0=通信1=查询
        /// </summary>
        public byte CommunicateType { get; set; }
        /// <summary>
        /// 是否有密钥：0=无1=有
        /// </summary>
        public byte CipherCode { get; set; }
        /// <summary>
        /// 余量，总是为00
        /// </summary>
        public byte Allowance { get; private set; }
        /// <summary>
        /// 解析通信信息信息类别
        /// </summary>
        private void Unpackage()
        {
            var bin = CustomConvert.IntToDigit(_type, CustomConvert.BIN, 8);

            var str = bin.Substring(0, 2);
            Communicate = (byte)CustomConvert.DigitToInt(str, CustomConvert.BIN);

            str = bin.Substring(2, 1);
            MessageType = byte.Parse(str);

            str = bin.Substring(3, 1);
            Receipt = byte.Parse(str);

            str = bin.Substring(4, 1);
            CommunicateType = byte.Parse(str);

            str = bin.Substring(5, 1);
            CipherCode = byte.Parse(str);

            str = bin.Substring(6);
            Allowance = (byte)CustomConvert.DigitToInt(str, CustomConvert.BIN);
        }
        /// <summary>
        /// 将通信信息类别的各个字段打包成一个字节
        /// </summary>
        private void Package()
        {
            var bin = CustomConvert.BIN;
            var tmp = string.Format("{0}{1}{2}{3}{4}{5}",
                CustomConvert.IntToDigit(Communicate, bin, 2),
                MessageType, Receipt,
                CustomConvert.IntToDigit(CommunicateType, bin, 2),
                CipherCode, CustomConvert.IntToDigit(Allowance, bin, 2));
            _type = (byte)CustomConvert.DigitToInt(tmp, bin);
        }
        /// <summary>
        /// 获取通讯信息类别字节
        /// </summary>
        public byte Type
        {
            get
            {
                Package();
                return _type;
            }
            set
            {
                _type = value;
                Unpackage();
            }
        }
        /// <summary>
        /// 将通信信息信息类别的各个字段显示成字符
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var bin = CustomConvert.BIN;
            StringBuilder sb = new StringBuilder();
            sb.Append("\t通信类别：").Append(Communicate).Append("\tBin：")
                .Append(CustomConvert.IntToDigit(Communicate, bin, 2)).Append(Environment.NewLine);
            sb.Append("\t电文形式：").Append(MessageType).Append("\tBin：")
                .Append(CustomConvert.IntToDigit(MessageType, bin, 1)).Append(Environment.NewLine);
            sb.Append("\t是否回执：").Append(Receipt).Append("\tBin：")
                .Append(CustomConvert.IntToDigit(Receipt, bin, 1)).Append(Environment.NewLine);
            sb.Append("\t通信方式：").Append(CommunicateType).Append("\tBin：")
                .Append(CustomConvert.IntToDigit(CommunicateType, bin, 1)).Append(Environment.NewLine);
            sb.Append("\t密　　钥：").Append(CipherCode).Append("\tBin：")
                .Append(CustomConvert.IntToDigit(CipherCode, bin, 1)).Append(Environment.NewLine);
            sb.Append("\t余　　量：").Append(Allowance).Append("\tBin：")
                .Append(CustomConvert.IntToDigit(Allowance, bin, 2)).Append(Environment.NewLine);
            return sb.ToString();
        }
    }
    /// <summary>
    /// 通信信息数据包
    /// </summary>
    public class TXXX : SatellitePackage
    {
        /// <summary>
        /// 生成一个新的通信信息数据包
        /// </summary>
        public TXXX()
            : base()
        {
            Command = "$TXXX";
        }
        public TXXX(byte[] data)
            : base(data)
        {
            Unpackage();
        }
        /// <summary>
        /// 信息类别
        /// </summary>
        //public byte Type { get; set; }
        private TXXX_Type _type = new TXXX_Type();
        /// <summary>
        /// 通信信息类别
        /// </summary>
        public TXXX_Type Type
        {
            get { return _type; }
            set { _type = value; }
        }

        private byte[] _sender =null;
        /// <summary>
        /// 发信方地址的二进制形式
        /// </summary>
        public byte[] Sender
        {
            get { return _sender; }
            set
            {
                if (null == value) _sender = null;
                else
                {
                    var len = value.Length;
                    if (len != UserAddressSize) throw new Exception("Sender address data length error.");
                    _sender = new byte[UserAddressSize];
                    Buffer.BlockCopy(value, 0, _sender, 0, UserAddressSize);
                }
            }
        }
        /// <summary>
        /// 发信时间中的小时
        /// </summary>
        public byte HH { get; set; }
        /// <summary>
        /// 发信时间中的分钟
        /// </summary>
        public byte MM { get; set; }
        /// <summary>
        /// 发信方地址的字符串形式
        /// </summary>
        public string SenderAddress
        {
            get { return GetUserAddress(_sender); }
            set { _sender = GetUserAddress(value); }
        }
        /// <summary>
        /// 电文长度
        /// </summary>
        public ushort MessageSize { get; set; }
        /// <summary>
        /// 电文内容
        /// </summary>
        private byte[] _message = null;
        /// <summary>
        /// 电文内容
        /// </summary>
        public byte[] Message
        {
            get { return _message; }
            set
            {
                if (null == value) {
                    _message = null; 
                    MessageSize = 0;
                }
                else
                {
                    var len = value.Length;
                    _message = new byte[len];
                    Buffer.BlockCopy(value, 0, _message, 0, len);
                    MessageSize = (ushort)len;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public byte CRC { get; set; }
        /// <summary>
        /// 解析通信信息数据包
        /// </summary>
        public override void Unpackage()
        {
            base.Unpackage();
            var index = 0;
            if (null == _type) {
                _type = new TXXX_Type();
            }
            _type.Type = Data[index];
            //Type = Data[index];
            index += 1;

            _sender = new byte[UserAddressSize];
            Buffer.BlockCopy(Data, index, _sender, 0, UserAddressSize);
            index += UserAddressSize;

            HH = Data[index];
            index += 1;

            MM = Data[index];
            index += 1;

            var data = new byte[2];
            Buffer.BlockCopy(Data, index, data, 0, 2);
            data = CustomConvert.reserve(data);
            MessageSize = BitConverter.ToUInt16(data, 0);
            index += Marshal.SizeOf(MessageSize);

            _message = new byte[MessageSize/8];
            Buffer.BlockCopy(Data, index, _message, 0, _message.Length);
            index += _message.Length;

            CRC = Data[index];
            index++;
        }
        /// <summary>
        /// 将通信信息数据包的各个字段用字符串显示
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToString()).Append("信息类别：").Append(Environment.NewLine).Append(Type.ToString());
            sb.Append("发方地址：").Append(SenderAddress).Append(Environment.NewLine);
            sb.Append("发信时间：").Append(HH).Append(Environment.NewLine);
            sb.Append("发信时间：").Append(MM).Append(Environment.NewLine);
            sb.Append("电文长度：").Append(MessageSize).Append("\t字节长度：").Append(MessageSize / 8).Append(Environment.NewLine);
            sb.Append("电文内容：").Append(CustomConvert.GetHex(Message)).Append(Environment.NewLine);
            sb.Append("冗余校验：").Append(CRC).Append(Environment.NewLine);
            sb.Append("校 验 和：").Append(Xor).Append(Environment.NewLine);
            return sb.ToString();
        }
    }
    /// <summary>
    /// 反馈信息数据包
    /// </summary>
    public class FKXX : SatellitePackage
    {
        /// <summary>
        /// 生成一个新的反馈信息数据包实体
        /// </summary>
        public FKXX()
            : base("$FKXX")
        { }
        /// <summary>
        /// 反馈标识
        /// </summary>
        public byte Flag { get; set; }
        /// <summary>
        /// 附加信息
        /// </summary>
        private byte[] _addtional = new byte[4];
        /// <summary>
        /// 附加信息
        /// </summary>
        public byte[] Additional
        {
            get
            {
                return
                    _addtional;
            }
        }
        /// <summary>
        /// 附加信息的字符串形式表示方式
        /// </summary>
        public string AddtionalData
        {
            get
            {
                var ret = "";
                switch (Flag)
                {
                    case 0x00:
                        ret = "成功(0x00)，" + ASCIIEncoding.ASCII.GetString(_addtional);
                        break;
                    case 0x01:
                        ret = "失败(0x01)，" + ASCIIEncoding.ASCII.GetString(_addtional);
                        break;
                    case 0x02:
                        ret = "信号未锁定(0x02)";
                        break;
                    case 0x03:
                        ret = "电量不足(0x03)";
                        break;
                    case 0x04:
                        ret = "发射频度未到(0x04)，还要" + _addtional[0] + "秒";
                        break;
                    case 0x05:
                        ret = "加解密错误(0x05)";
                        break;
                    case 0x06:
                        ret = "CRC错误(0x06)，" + ASCIIEncoding.ASCII.GetString(_addtional); break;
                    default:
                        if (0x09 <= Flag && Flag <= 0xA0) ret = "保留(0x09-0xA0)";
                        else
                            ret = "厂家扩展(0xA1-0xFF)";
                        break;
                }
                return ret;
            }
        }
        /// <summary>
        /// 解析反馈信息数据包
        /// </summary>
        public override void Unpackage()
        {
            base.Unpackage();

            var index = 0;
            Flag = Data[index];
            index += 1;

            Buffer.BlockCopy(Data, index, _addtional, 0, 4);
            index += 4;
        }
        /// <summary>
        /// 将反馈信息数据包的各个字段用字符串显示
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToString());
            sb.Append("反馈标志：").Append(Flag).Append(Environment.NewLine);
            sb.Append("附加信息：").Append(AddtionalData).Append(Environment.NewLine);
            return sb.ToString();
        }
    }
    /// <summary>
    /// 系统自检申请信息数据包
    /// </summary>
    public class XTZJ : SatellitePackage {
        /// <summary>
        /// 生成一个新的系统自检申请信息
        /// </summary>
        public XTZJ() : base("$XTZJ") { }
        /// <summary>
        /// 自检频率，单位：秒。0表示单次检测
        /// </summary>
        public ushort Frequency { get; set; }
        /// <summary>
        /// 解析系统自检申请数据包
        /// </summary>
        public override void Unpackage()
        {
            base.Unpackage();

            var index = 0;

            var data = new byte[2];
            Buffer.BlockCopy(Data, index, data, 0, 2);
            data = CustomConvert.reserve(data);
            Frequency = BitConverter.ToUInt16(data, 0);
            index += Marshal.SizeOf(Frequency);
        }
        /// <summary>
        /// 组合系统自检申请信息的各个字段
        /// </summary>
        public override void Package()
        {
            base.Package();

            var data = BitConverter.GetBytes(Frequency);
            data = CustomConvert.reserve(data);
            Package(data);
            data = null;

            TotalSize = (ushort)(Content.Length + 1);
            PackageTotalSize(TotalSize);

            Xor = CustomConvert.GetXor(Content);
            Package(Xor);
        }
        /// <summary>
        /// 将系统自检数据的各个字段用字符显示
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToString());
            sb.Append("自检频率：").Append(Frequency).Append("秒").Append(Environment.NewLine);
            return sb.ToString();
        }
    }
    /// <summary>
    /// 自检信息数据包
    /// </summary>
    public class ZJXX : SatellitePackage {
        /// <summary>
        /// 生成一个新的自检信息数据包实体
        /// </summary>
        public ZJXX()
            : base("$ZJXX")
        { }
        /// <summary>
        /// IC状态
        /// </summary>
        public byte IC { get; set; }
        /// <summary>
        /// 硬件状态
        /// </summary>
        public byte HW { get; set; }
        /// <summary>
        /// 电池电量，剩余1/n
        /// </summary>
        public byte PW { get; set; }
        /// <summary>
        /// 入站状态
        /// </summary>
        public byte IN { get; set; }
        /// <summary>
        /// 波束1功率
        /// </summary>
        public byte PW1 { get; set; }
        /// <summary>
        /// 波束2功率
        /// </summary>
        public byte PW2 { get; set; }
        /// <summary>
        /// 波束3功率
        /// </summary>
        public byte PW3 { get; set; }
        /// <summary>
        /// 波束4功率
        /// </summary>
        public byte PW4 { get; set; }
        /// <summary>
        /// 波束5功率
        /// </summary>
        public byte PW5 { get; set; }
        /// <summary>
        /// 波束6功率
        /// </summary>
        public byte PW6 { get; set; }
        /// <summary>
        /// 解析系统自检信息数据包
        /// </summary>
        public override void Unpackage()
        {
            base.Unpackage();

            var index = 0;
            IC = Data[index];
            index += 1;

            HW = Data[index];
            index++;

            PW = Data[index];
            index++;

            PW1 = Data[index];
            index++;

            PW2 = Data[index];
            index++;

            PW3 = Data[index];
            index++;

            PW4 = Data[index];
            index++;

            PW5 = Data[index];
            index++;

            PW6 = Data[index];
            index++;
        }
        private string GetInStatus()
        {
            var ret = "";
            var str = CustomConvert.IntToDigit(IN, CustomConvert.BIN, 8);
            ret += "入站许可：" + (str[0] == '1' ? "可以" : "不可以");
            ret += "抑制状态：" + (str[1] == '1' ? "抑制" : "非抑制");
            return ret;
        }
        /// <summary>
        /// 获取波束功率强度
        /// </summary>
        /// <param name="power"></param>
        /// <returns></returns>
        public static string GetPower(byte power)
        {
            var ret = "";
            switch (power)
            {
                case 1: ret = "-156 ～ -157dBW"; break;
                case 2: ret = "-154 ～ -155dBW"; break;
                case 3: ret = "-152 ～ -153dBW"; break;
                case 4: ret = "> -152dBW"; break;
                default: ret = "< -158dBW"; break;
            }
            return ret;
        }
        /// <summary>
        /// 将自检信息的各个字段显示出来
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToString());
            sb.Append("IC卡状态：").Append(IC).Append(Environment.NewLine);
            sb.Append("硬件状态：").Append(HW).Append(Environment.NewLine);
            sb.Append("电池电量：").Append(PW).Append("\t剩余量：1/").Append(PW).Append(Environment.NewLine);
            sb.Append("入站状态：").Append(IN).Append("\t").Append(GetInStatus()).Append(Environment.NewLine);
            sb.Append("波束1功率：").Append(PW1).Append("\t").Append(GetPower(PW1)).Append(Environment.NewLine);
            sb.Append("波束2功率：").Append(PW2).Append("\t").Append(GetPower(PW2)).Append(Environment.NewLine);
            sb.Append("波束3功率：").Append(PW3).Append("\t").Append(GetPower(PW3)).Append(Environment.NewLine);
            sb.Append("波束4功率：").Append(PW4).Append("\t").Append(GetPower(PW4)).Append(Environment.NewLine);
            sb.Append("波束5功率：").Append(PW5).Append("\t").Append(GetPower(PW5)).Append(Environment.NewLine);
            sb.Append("波束6功率：").Append(PW6).Append("\t").Append(GetPower(PW6)).Append(Environment.NewLine);
            return sb.ToString();
        }
    }
    /// <summary>
    /// IC检测数据包
    /// </summary>
    public class ICJC : SatellitePackage
    {
        /// <summary>
        /// 生成一个新的
        /// </summary>
        public ICJC()
            : base("$ICJC")
        {
            OriginAddress = "000000";
            FrameNo = 0;
        }
        /// <summary>
        /// 帧号，默认填0
        /// </summary>
        public byte FrameNo { get; set; }
        /// <summary>
        /// 解析IC检测信息数据包
        /// </summary>
        public override void Unpackage()
        {
            base.Unpackage();
            var index = 0;

            FrameNo = Data[index];
            index += 1;
        }
        /// <summary>
        /// 组合IC检测包
        /// </summary>
        public override void Package()
        {
            base.Package();

            Package(FrameNo);

            TotalSize = (ushort)(Content.Length + 1);
            PackageTotalSize(TotalSize);

            Xor = CustomConvert.GetXor(Content);
            Package(Xor);
        }
        /// <summary>
        /// 将IC检测信息中的各个字段用字符串表示
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToString());
            sb.Append("帧号：").Append(FrameNo).Append(Environment.NewLine);
            return sb.ToString();
        }
    }
    /// <summary>
    /// IC信息数据包
    /// </summary>
    public class ICXX : SatellitePackage
    {
        /// <summary>
        /// 生成一个新的IC信息数据包实体
        /// </summary>
        public ICXX() : base("$ICXX") { }
        /// <summary>
        /// 帧号
        /// </summary>
        public byte FrameNo { get; set; }
        /// <summary>
        /// 通播ID
        /// </summary>
        private byte[] _id = new byte[UserAddressSize];
        /// <summary>
        /// 通播ID地址的2进制表现形式
        /// </summary>
        public byte[] ID
        {
            get { return _id; }
        }
        /// <summary>
        /// 通播ID地址的字符串表现形式
        /// </summary>
        public string IDAddress
        {
            get { return GetUserAddress(_id); }
            set { _id = GetUserAddress(value); }
        }
        /// <summary>
        /// 用户特征
        /// </summary>
        public byte Characteristic { get; set; }
        /// <summary>
        /// 服务频度
        /// </summary>
        public ushort Frequency { get; set; }
        /// <summary>
        /// 通信等级
        /// </summary>
        public byte Level { get; set; }
        /// <summary>
        /// 加密标记
        /// </summary>
        public byte Security { get; set; }
        /// <summary>
        /// 下属用户数量
        /// </summary>
        public ushort Users { get; set; }
        /// <summary>
        /// 解析IC信息数据包
        /// </summary>
        public override void Unpackage()
        {
            base.Unpackage();
            var index = 0;

            FrameNo = Data[index];
            index += 1;
            byte[] data = new byte[2];
            //Buffer.BlockCopy(Data, index, data, 0, 2);
            //data = CustomConvert.reserve(data);
            //FrameNo = BitConverter.ToUInt16(data, 0);
            //index += Marshal.SizeOf(FrameNo);

            Buffer.BlockCopy(Data, index, _id, 0, UserAddressSize);
            index += UserAddressSize;

            Characteristic = Data[index];
            index += 1;

            Buffer.BlockCopy(Data, index, data, 0, 2);
            data = CustomConvert.reserve(data);
            Frequency = BitConverter.ToUInt16(data, 0);
            index += Marshal.SizeOf(Frequency);

            Level = Data[index];
            index += 1;

            Security = Data[index];
            index += 1;

            Buffer.BlockCopy(Data, index, data, 0, 2);
            data = CustomConvert.reserve(data);
            Users = BitConverter.ToUInt16(data, 0);
            index += Marshal.SizeOf(Users);

            data = null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToString());
            sb.Append("帧号：").Append(FrameNo).Append(Environment.NewLine);
            sb.Append("通播ID：").Append(IDAddress).Append(Environment.NewLine);
            sb.Append("用户特征：").Append(Characteristic).Append(Environment.NewLine);
            sb.Append("服务频度：").Append(Frequency).Append("秒").Append(Environment.NewLine);
            sb.Append("通讯等级：").Append(Level).Append(Environment.NewLine);
            sb.Append("加密标记：").Append(Security).Append(Environment.NewLine);
            sb.Append("用户总数：").Append(Users).Append(Environment.NewLine);
            return sb.ToString();
        }
    }
}
