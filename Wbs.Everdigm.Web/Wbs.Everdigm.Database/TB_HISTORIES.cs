using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Linq;
using System.ComponentModel;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;

namespace Wbs.Everdigm.Database
{

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.TB_HISTORIES")]
    [global::System.Runtime.Serialization.DataContractAttribute()]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_receive_time", DbType = "DateTime")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 1)]
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
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 2)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_total_length", DbType = "SmallInt")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 3)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_protocol_type", DbType = "TinyInt")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 4)]
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
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 5)]
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
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 6)]
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
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 7)]
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
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 8)]
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
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 9)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_sequence_id", DbType = "VarChar(5)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 10)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_command_id", DbType = "VarChar(10)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 11)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_terminal_id", DbType = "VarChar(15)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 12)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_mac_id", DbType = "VarChar(20)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 13)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_message_content", DbType = "VarChar(3000)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 14)]
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
