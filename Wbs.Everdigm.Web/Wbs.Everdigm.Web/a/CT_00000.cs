using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;

namespace Wbs.Everdigm.Database
{
    /// <summary>
    /// 发送命令
    /// </summary>
    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.CT_00000")]
    [global::System.Runtime.Serialization.DataContractAttribute()]
    public partial class CT_00000 : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _u_sms_id;

        private System.Nullable<byte> _u_sms_data_type;

        private System.Nullable<byte> _u_sms_status;

        private System.Nullable<byte> _u_sms_mobile_type;

        private System.Nullable<int> _u_sms_retry_times;

        private System.Nullable<System.DateTime> _u_sms_schedule_time;

        private System.Nullable<System.DateTime> _u_sms_confirm_send_time;

        private System.Nullable<System.DateTime> _u_sms_actual_send_time;

        private System.Nullable<System.DateTime> _u_sms_send_status_time;

        private string _u_sms_command;

        private string _u_sms_send_status;

        private string _u_sms_mobile_no;

        private string _u_sms_content;

        #region 可扩展性方法定义
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void Onu_sms_idChanging(int value);
        partial void Onu_sms_idChanged();
        partial void Onu_sms_data_typeChanging(System.Nullable<byte> value);
        partial void Onu_sms_data_typeChanged();
        partial void Onu_sms_statusChanging(System.Nullable<byte> value);
        partial void Onu_sms_statusChanged();
        partial void Onu_sms_mobile_typeChanging(System.Nullable<byte> value);
        partial void Onu_sms_mobile_typeChanged();
        partial void Onu_sms_retry_timesChanging(System.Nullable<int> value);
        partial void Onu_sms_retry_timesChanged();
        partial void Onu_sms_schedule_timeChanging(System.Nullable<System.DateTime> value);
        partial void Onu_sms_schedule_timeChanged();
        partial void Onu_sms_confirm_send_timeChanging(System.Nullable<System.DateTime> value);
        partial void Onu_sms_confirm_send_timeChanged();
        partial void Onu_sms_actual_send_timeChanging(System.Nullable<System.DateTime> value);
        partial void Onu_sms_actual_send_timeChanged();
        partial void Onu_sms_send_status_timeChanging(System.Nullable<System.DateTime> value);
        partial void Onu_sms_send_status_timeChanged();
        partial void Onu_sms_commandChanging(string value);
        partial void Onu_sms_commandChanged();
        partial void Onu_sms_send_statusChanging(string value);
        partial void Onu_sms_send_statusChanged();
        partial void Onu_sms_mobile_noChanging(string value);
        partial void Onu_sms_mobile_noChanged();
        partial void Onu_sms_contentChanging(string value);
        partial void Onu_sms_contentChanged();
        #endregion

        public CT_00000()
        {
            this.Initialize();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_u_sms_id", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 1)]
        public int u_sms_id
        {
            get
            {
                return this._u_sms_id;
            }
            set
            {
                if ((this._u_sms_id != value))
                {
                    this.Onu_sms_idChanging(value);
                    this.SendPropertyChanging();
                    this._u_sms_id = value;
                    this.SendPropertyChanged("u_sms_id");
                    this.Onu_sms_idChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_u_sms_data_type", DbType = "TinyInt")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 2)]
        public System.Nullable<byte> u_sms_data_type
        {
            get
            {
                return this._u_sms_data_type;
            }
            set
            {
                if ((this._u_sms_data_type != value))
                {
                    this.Onu_sms_data_typeChanging(value);
                    this.SendPropertyChanging();
                    this._u_sms_data_type = value;
                    this.SendPropertyChanged("u_sms_data_type");
                    this.Onu_sms_data_typeChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_u_sms_status", DbType = "TinyInt")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 3)]
        public System.Nullable<byte> u_sms_status
        {
            get
            {
                return this._u_sms_status;
            }
            set
            {
                if ((this._u_sms_status != value))
                {
                    this.Onu_sms_statusChanging(value);
                    this.SendPropertyChanging();
                    this._u_sms_status = value;
                    this.SendPropertyChanged("u_sms_status");
                    this.Onu_sms_statusChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_u_sms_mobile_type", DbType = "TinyInt")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 4)]
        public System.Nullable<byte> u_sms_mobile_type
        {
            get
            {
                return this._u_sms_mobile_type;
            }
            set
            {
                if ((this._u_sms_mobile_type != value))
                {
                    this.Onu_sms_mobile_typeChanging(value);
                    this.SendPropertyChanging();
                    this._u_sms_mobile_type = value;
                    this.SendPropertyChanged("u_sms_mobile_type");
                    this.Onu_sms_mobile_typeChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_u_sms_retry_times", DbType = "Int")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 5)]
        public System.Nullable<int> u_sms_retry_times
        {
            get
            {
                return this._u_sms_retry_times;
            }
            set
            {
                if ((this._u_sms_retry_times != value))
                {
                    this.Onu_sms_retry_timesChanging(value);
                    this.SendPropertyChanging();
                    this._u_sms_retry_times = value;
                    this.SendPropertyChanged("u_sms_retry_times");
                    this.Onu_sms_retry_timesChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_u_sms_schedule_time", DbType = "DateTime")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 6)]
        public System.Nullable<System.DateTime> u_sms_schedule_time
        {
            get
            {
                return this._u_sms_schedule_time;
            }
            set
            {
                if ((this._u_sms_schedule_time != value))
                {
                    this.Onu_sms_schedule_timeChanging(value);
                    this.SendPropertyChanging();
                    this._u_sms_schedule_time = value;
                    this.SendPropertyChanged("u_sms_schedule_time");
                    this.Onu_sms_schedule_timeChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_u_sms_confirm_send_time", DbType = "DateTime")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 7)]
        public System.Nullable<System.DateTime> u_sms_confirm_send_time
        {
            get
            {
                return this._u_sms_confirm_send_time;
            }
            set
            {
                if ((this._u_sms_confirm_send_time != value))
                {
                    this.Onu_sms_confirm_send_timeChanging(value);
                    this.SendPropertyChanging();
                    this._u_sms_confirm_send_time = value;
                    this.SendPropertyChanged("u_sms_confirm_send_time");
                    this.Onu_sms_confirm_send_timeChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_u_sms_actual_send_time", DbType = "DateTime")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 8)]
        public System.Nullable<System.DateTime> u_sms_actual_send_time
        {
            get
            {
                return this._u_sms_actual_send_time;
            }
            set
            {
                if ((this._u_sms_actual_send_time != value))
                {
                    this.Onu_sms_actual_send_timeChanging(value);
                    this.SendPropertyChanging();
                    this._u_sms_actual_send_time = value;
                    this.SendPropertyChanged("u_sms_actual_send_time");
                    this.Onu_sms_actual_send_timeChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_u_sms_send_status_time", DbType = "DateTime")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 9)]
        public System.Nullable<System.DateTime> u_sms_send_status_time
        {
            get
            {
                return this._u_sms_send_status_time;
            }
            set
            {
                if ((this._u_sms_send_status_time != value))
                {
                    this.Onu_sms_send_status_timeChanging(value);
                    this.SendPropertyChanging();
                    this._u_sms_send_status_time = value;
                    this.SendPropertyChanged("u_sms_send_status_time");
                    this.Onu_sms_send_status_timeChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_u_sms_command", DbType = "Char(6)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 10)]
        public string u_sms_command
        {
            get
            {
                return this._u_sms_command;
            }
            set
            {
                if ((this._u_sms_command != value))
                {
                    this.Onu_sms_commandChanging(value);
                    this.SendPropertyChanging();
                    this._u_sms_command = value;
                    this.SendPropertyChanged("u_sms_command");
                    this.Onu_sms_commandChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_u_sms_send_status", DbType = "VarChar(10)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 11)]
        public string u_sms_send_status
        {
            get
            {
                return this._u_sms_send_status;
            }
            set
            {
                if ((this._u_sms_send_status != value))
                {
                    this.Onu_sms_send_statusChanging(value);
                    this.SendPropertyChanging();
                    this._u_sms_send_status = value;
                    this.SendPropertyChanged("u_sms_send_status");
                    this.Onu_sms_send_statusChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_u_sms_mobile_no", DbType = "VarChar(11)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 12)]
        public string u_sms_mobile_no
        {
            get
            {
                return this._u_sms_mobile_no;
            }
            set
            {
                if ((this._u_sms_mobile_no != value))
                {
                    this.Onu_sms_mobile_noChanging(value);
                    this.SendPropertyChanging();
                    this._u_sms_mobile_no = value;
                    this.SendPropertyChanged("u_sms_mobile_no");
                    this.Onu_sms_mobile_noChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_u_sms_content", DbType = "NVarChar(1000)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 13)]
        public string u_sms_content
        {
            get
            {
                return this._u_sms_content;
            }
            set
            {
                if ((this._u_sms_content != value))
                {
                    this.Onu_sms_contentChanging(value);
                    this.SendPropertyChanging();
                    this._u_sms_content = value;
                    this.SendPropertyChanged("u_sms_content");
                    this.Onu_sms_contentChanged();
                }
            }
        }

        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SendPropertyChanging()
        {
            if ((this.PropertyChanging != null))
            {
                this.PropertyChanging(this, emptyChangingEventArgs);
            }
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void Initialize()
        {
            OnCreated();
        }

        [global::System.Runtime.Serialization.OnDeserializingAttribute()]
        [global::System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
        public void OnDeserializing(StreamingContext context)
        {
            this.Initialize();
        }
    }
	
}
