using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wbs.Everdigm.Database
{
    /// <summary>
    /// 设备历史记录表
    /// </summary>
    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.TB_HISTORIES")]
    public partial class TB_HISTORIES
    {

        private System.Nullable<System.DateTime> _receive_time;

        private System.Nullable<short> _server_port;

        private System.Nullable<short> _total_length;

        private System.Nullable<byte> _protocol_type;

        private System.Nullable<byte> _terminal_type;

        private System.Nullable<byte> _message_type;

        private System.Nullable<byte> _protocol_version;

        private System.Nullable<byte> _package_id;

        private System.Nullable<byte> _total_package;

        private string _sequence_id;

        private string _command_id;

        private string _terminal_id;

        private string _mac_id;

        private string _message_content;

        public TB_HISTORIES()
        {
        }
        /// <summary>
        /// 信息接收时间(服务器系统时间)
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_receive_time", DbType = "DateTime")]
        public System.Nullable<System.DateTime> receive_time
        {
            get
            {
                return this._receive_time;
            }
            set
            {
                if ((this._receive_time != value))
                {
                    this._receive_time = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_server_port", DbType = "SmallInt")]
        public System.Nullable<short> server_port
        {
            get
            {
                return this._server_port;
            }
            set
            {
                if ((this._server_port != value))
                {
                    this._server_port = value;
                }
            }
        }
        /// <summary>
        /// 数据包长度
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_total_length", DbType = "SmallInt")]
        public System.Nullable<short> total_length
        {
            get
            {
                return this._total_length;
            }
            set
            {
                if ((this._total_length != value))
                {
                    this._total_length = value;
                }
            }
        }
        /// <summary>
        /// 数据包协议类型(TCP/UDP/SMS等)
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_protocol_type", DbType = "TinyInt")]
        public System.Nullable<byte> protocol_type
        {
            get
            {
                return this._protocol_type;
            }
            set
            {
                if ((this._protocol_type != value))
                {
                    this._protocol_type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_terminal_type", DbType = "TinyInt")]
        public System.Nullable<byte> terminal_type
        {
            get
            {
                return this._terminal_type;
            }
            set
            {
                if ((this._terminal_type != value))
                {
                    this._terminal_type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_message_type", DbType = "TinyInt")]
        public System.Nullable<byte> message_type
        {
            get
            {
                return this._message_type;
            }
            set
            {
                if ((this._message_type != value))
                {
                    this._message_type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_protocol_version", DbType = "TinyInt")]
        public System.Nullable<byte> protocol_version
        {
            get
            {
                return this._protocol_version;
            }
            set
            {
                if ((this._protocol_version != value))
                {
                    this._protocol_version = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_package_id", DbType = "TinyInt")]
        public System.Nullable<byte> package_id
        {
            get
            {
                return this._package_id;
            }
            set
            {
                if ((this._package_id != value))
                {
                    this._package_id = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_total_package", DbType = "TinyInt")]
        public System.Nullable<byte> total_package
        {
            get
            {
                return this._total_package;
            }
            set
            {
                if ((this._total_package != value))
                {
                    this._total_package = value;
                }
            }
        }
        /// <summary>
        /// 流水号
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sequence_id", DbType = "VarChar(5)")]
        public string sequence_id
        {
            get
            {
                return this._sequence_id;
            }
            set
            {
                if ((this._sequence_id != value))
                {
                    this._sequence_id = value;
                }
            }
        }
        /// <summary>
        /// 命令字
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_command_id", DbType = "VarChar(10)")]
        public string command_id
        {
            get
            {
                return this._command_id;
            }
            set
            {
                if ((this._command_id != value))
                {
                    this._command_id = value;
                }
            }
        }
        /// <summary>
        /// 终端号码(一般为sim卡号码)
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_terminal_id", DbType = "VarChar(15)")]
        public string terminal_id
        {
            get
            {
                return this._terminal_id;
            }
            set
            {
                if ((this._terminal_id != value))
                {
                    this._terminal_id = value;
                }
            }
        }
        /// <summary>
        /// 设备号码
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_mac_id", DbType = "VarChar(20)")]
        public string mac_id
        {
            get
            {
                return this._mac_id;
            }
            set
            {
                if ((this._mac_id != value))
                {
                    this._mac_id = value;
                }
            }
        }
        /// <summary>
        /// 数据内容
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_message_content", DbType = "VarChar(3000)")]
        public string message_content
        {
            get
            {
                return this._message_content;
            }
            set
            {
                if ((this._message_content != value))
                {
                    this._message_content = value;
                }
            }
        }
    }

}
