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

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.TB_Position")]
    [global::System.Runtime.Serialization.DataContractAttribute()]
    public partial class TB_Position : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private long _id;

        private System.Nullable<System.DateTime> _ReceiveTime;

        private System.Nullable<System.DateTime> _GpsTime;

        private System.Nullable<byte> _Updated;

        private System.Nullable<int> _Equipment;

        private System.Nullable<int> _StoreTimes;

        private System.Nullable<byte> _Csq;

        private System.Nullable<byte> _Ber;

        private System.Nullable<double> _Latitude;

        private System.Nullable<char> _NS;

        private System.Nullable<double> _Longitude;

        private System.Nullable<char> _EW;

        private System.Nullable<double> _Speed;

        private System.Nullable<double> _Direction;

        private System.Nullable<double> _Altitude;

        private string _StationCode;

        private string _SectorCode;

        private string _Type;

        private string _Address;

        private EntitySet<TB_Alarm> _TB_Alarm;

        private EntityRef<TB_Equipment> _TB_Equipment;

        private bool serializing;

        #region 可扩展性方法定义
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnidChanging(long value);
        partial void OnidChanged();
        partial void OnReceiveTimeChanging(System.Nullable<System.DateTime> value);
        partial void OnReceiveTimeChanged();
        partial void OnGpsTimeChanging(System.Nullable<System.DateTime> value);
        partial void OnGpsTimeChanged();
        partial void OnUpdatedChanging(System.Nullable<byte> value);
        partial void OnUpdatedChanged();
        partial void OnEquipmentChanging(System.Nullable<int> value);
        partial void OnEquipmentChanged();
        partial void OnStoreTimesChanging(System.Nullable<int> value);
        partial void OnStoreTimesChanged();
        partial void OnCsqChanging(System.Nullable<byte> value);
        partial void OnCsqChanged();
        partial void OnBerChanging(System.Nullable<byte> value);
        partial void OnBerChanged();
        partial void OnLatitudeChanging(System.Nullable<double> value);
        partial void OnLatitudeChanged();
        partial void OnNSChanging(System.Nullable<char> value);
        partial void OnNSChanged();
        partial void OnLongitudeChanging(System.Nullable<double> value);
        partial void OnLongitudeChanged();
        partial void OnEWChanging(System.Nullable<char> value);
        partial void OnEWChanged();
        partial void OnSpeedChanging(System.Nullable<double> value);
        partial void OnSpeedChanged();
        partial void OnDirectionChanging(System.Nullable<double> value);
        partial void OnDirectionChanged();
        partial void OnAltitudeChanging(System.Nullable<double> value);
        partial void OnAltitudeChanged();
        partial void OnStationCodeChanging(string value);
        partial void OnStationCodeChanged();
        partial void OnSectorCodeChanging(string value);
        partial void OnSectorCodeChanged();
        partial void OnTypeChanging(string value);
        partial void OnTypeChanged();
        partial void OnAddressChanging(string value);
        partial void OnAddressChanged();
        #endregion

        public TB_Position()
        {
            this.Initialize();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_id", AutoSync = AutoSync.OnInsert, DbType = "BigInt NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 1)]
        public long id
        {
            get
            {
                return this._id;
            }
            set
            {
                if ((this._id != value))
                {
                    this.OnidChanging(value);
                    this.SendPropertyChanging();
                    this._id = value;
                    this.SendPropertyChanged("id");
                    this.OnidChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ReceiveTime", DbType = "DateTime")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 2)]
        public System.Nullable<System.DateTime> ReceiveTime
        {
            get
            {
                return this._ReceiveTime;
            }
            set
            {
                if ((this._ReceiveTime != value))
                {
                    this.OnReceiveTimeChanging(value);
                    this.SendPropertyChanging();
                    this._ReceiveTime = value;
                    this.SendPropertyChanged("ReceiveTime");
                    this.OnReceiveTimeChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_GpsTime", DbType = "DateTime")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 3)]
        public System.Nullable<System.DateTime> GpsTime
        {
            get
            {
                return this._GpsTime;
            }
            set
            {
                if ((this._GpsTime != value))
                {
                    this.OnGpsTimeChanging(value);
                    this.SendPropertyChanging();
                    this._GpsTime = value;
                    this.SendPropertyChanged("GpsTime");
                    this.OnGpsTimeChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Updated", DbType = "TinyInt")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 4)]
        public System.Nullable<byte> Updated
        {
            get
            {
                return this._Updated;
            }
            set
            {
                if ((this._Updated != value))
                {
                    this.OnUpdatedChanging(value);
                    this.SendPropertyChanging();
                    this._Updated = value;
                    this.SendPropertyChanged("Updated");
                    this.OnUpdatedChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Equipment", DbType = "Int")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 5)]
        public System.Nullable<int> Equipment
        {
            get
            {
                return this._Equipment;
            }
            set
            {
                if ((this._Equipment != value))
                {
                    if (this._TB_Equipment.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnEquipmentChanging(value);
                    this.SendPropertyChanging();
                    this._Equipment = value;
                    this.SendPropertyChanged("Equipment");
                    this.OnEquipmentChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_StoreTimes", DbType = "Int")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 6)]
        public System.Nullable<int> StoreTimes
        {
            get
            {
                return this._StoreTimes;
            }
            set
            {
                if ((this._StoreTimes != value))
                {
                    this.OnStoreTimesChanging(value);
                    this.SendPropertyChanging();
                    this._StoreTimes = value;
                    this.SendPropertyChanged("StoreTimes");
                    this.OnStoreTimesChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Csq", DbType = "TinyInt")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 7)]
        public System.Nullable<byte> Csq
        {
            get
            {
                return this._Csq;
            }
            set
            {
                if ((this._Csq != value))
                {
                    this.OnCsqChanging(value);
                    this.SendPropertyChanging();
                    this._Csq = value;
                    this.SendPropertyChanged("Csq");
                    this.OnCsqChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Ber", DbType = "TinyInt")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 8)]
        public System.Nullable<byte> Ber
        {
            get
            {
                return this._Ber;
            }
            set
            {
                if ((this._Ber != value))
                {
                    this.OnBerChanging(value);
                    this.SendPropertyChanging();
                    this._Ber = value;
                    this.SendPropertyChanged("Ber");
                    this.OnBerChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Latitude", DbType = "Float")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 9)]
        public System.Nullable<double> Latitude
        {
            get
            {
                return this._Latitude;
            }
            set
            {
                if ((this._Latitude != value))
                {
                    this.OnLatitudeChanging(value);
                    this.SendPropertyChanging();
                    this._Latitude = value;
                    this.SendPropertyChanged("Latitude");
                    this.OnLatitudeChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_NS", DbType = "Char(1)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 10)]
        public System.Nullable<char> NS
        {
            get
            {
                return this._NS;
            }
            set
            {
                if ((this._NS != value))
                {
                    this.OnNSChanging(value);
                    this.SendPropertyChanging();
                    this._NS = value;
                    this.SendPropertyChanged("NS");
                    this.OnNSChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Longitude", DbType = "Float")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 11)]
        public System.Nullable<double> Longitude
        {
            get
            {
                return this._Longitude;
            }
            set
            {
                if ((this._Longitude != value))
                {
                    this.OnLongitudeChanging(value);
                    this.SendPropertyChanging();
                    this._Longitude = value;
                    this.SendPropertyChanged("Longitude");
                    this.OnLongitudeChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_EW", DbType = "Char(1)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 12)]
        public System.Nullable<char> EW
        {
            get
            {
                return this._EW;
            }
            set
            {
                if ((this._EW != value))
                {
                    this.OnEWChanging(value);
                    this.SendPropertyChanging();
                    this._EW = value;
                    this.SendPropertyChanged("EW");
                    this.OnEWChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Speed", DbType = "Float")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 13)]
        public System.Nullable<double> Speed
        {
            get
            {
                return this._Speed;
            }
            set
            {
                if ((this._Speed != value))
                {
                    this.OnSpeedChanging(value);
                    this.SendPropertyChanging();
                    this._Speed = value;
                    this.SendPropertyChanged("Speed");
                    this.OnSpeedChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Direction", DbType = "Float")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 14)]
        public System.Nullable<double> Direction
        {
            get
            {
                return this._Direction;
            }
            set
            {
                if ((this._Direction != value))
                {
                    this.OnDirectionChanging(value);
                    this.SendPropertyChanging();
                    this._Direction = value;
                    this.SendPropertyChanged("Direction");
                    this.OnDirectionChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Altitude", DbType = "Float")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 15)]
        public System.Nullable<double> Altitude
        {
            get
            {
                return this._Altitude;
            }
            set
            {
                if ((this._Altitude != value))
                {
                    this.OnAltitudeChanging(value);
                    this.SendPropertyChanging();
                    this._Altitude = value;
                    this.SendPropertyChanged("Altitude");
                    this.OnAltitudeChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_StationCode", DbType = "Char(4)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 16)]
        public string StationCode
        {
            get
            {
                return this._StationCode;
            }
            set
            {
                if ((this._StationCode != value))
                {
                    this.OnStationCodeChanging(value);
                    this.SendPropertyChanging();
                    this._StationCode = value;
                    this.SendPropertyChanged("StationCode");
                    this.OnStationCodeChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SectorCode", DbType = "Char(4)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 17)]
        public string SectorCode
        {
            get
            {
                return this._SectorCode;
            }
            set
            {
                if ((this._SectorCode != value))
                {
                    this.OnSectorCodeChanging(value);
                    this.SendPropertyChanging();
                    this._SectorCode = value;
                    this.SendPropertyChanged("SectorCode");
                    this.OnSectorCodeChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Type", DbType = "VarChar(10)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 18)]
        public string Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                if ((this._Type != value))
                {
                    this.OnTypeChanging(value);
                    this.SendPropertyChanging();
                    this._Type = value;
                    this.SendPropertyChanged("Type");
                    this.OnTypeChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Address", DbType = "NVarChar(100)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 19)]
        public string Address
        {
            get
            {
                return this._Address;
            }
            set
            {
                if ((this._Address != value))
                {
                    this.OnAddressChanging(value);
                    this.SendPropertyChanging();
                    this._Address = value;
                    this.SendPropertyChanged("Address");
                    this.OnAddressChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "TB_Position_TB_Alarm", Storage = "_TB_Alarm", ThisKey = "id", OtherKey = "Position")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 20, EmitDefaultValue = false)]
        public EntitySet<TB_Alarm> TB_Alarm
        {
            get
            {
                if ((this.serializing
                            && (this._TB_Alarm.HasLoadedOrAssignedValues == false)))
                {
                    return null;
                }
                return this._TB_Alarm;
            }
            set
            {
                this._TB_Alarm.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "TB_Equipment_TB_Position", Storage = "_TB_Equipment", ThisKey = "Equipment", OtherKey = "id", IsForeignKey = true, DeleteRule = "SET NULL")]
        public TB_Equipment TB_Equipment
        {
            get
            {
                return this._TB_Equipment.Entity;
            }
            set
            {
                TB_Equipment previousValue = this._TB_Equipment.Entity;
                if (((previousValue != value)
                            || (this._TB_Equipment.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._TB_Equipment.Entity = null;
                        previousValue.TB_Position.Remove(this);
                    }
                    this._TB_Equipment.Entity = value;
                    if ((value != null))
                    {
                        value.TB_Position.Add(this);
                        this._Equipment = value.id;
                    }
                    else
                    {
                        this._Equipment = default(Nullable<int>);
                    }
                    this.SendPropertyChanged("TB_Equipment");
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

        private void attach_TB_Alarm(TB_Alarm entity)
        {
            this.SendPropertyChanging();
            entity.TB_Position = this;
        }

        private void detach_TB_Alarm(TB_Alarm entity)
        {
            this.SendPropertyChanging();
            entity.TB_Position = null;
        }

        private void Initialize()
        {
            this._TB_Alarm = new EntitySet<TB_Alarm>(new Action<TB_Alarm>(this.attach_TB_Alarm), new Action<TB_Alarm>(this.detach_TB_Alarm));
            this._TB_Equipment = default(EntityRef<TB_Equipment>);
            OnCreated();
        }

        [global::System.Runtime.Serialization.OnDeserializingAttribute()]
        [global::System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
        public void OnDeserializing(StreamingContext context)
        {
            this.Initialize();
        }

        [global::System.Runtime.Serialization.OnSerializingAttribute()]
        [global::System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
        public void OnSerializing(StreamingContext context)
        {
            this.serializing = true;
        }

        [global::System.Runtime.Serialization.OnSerializedAttribute()]
        [global::System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
        public void OnSerialized(StreamingContext context)
        {
            this.serializing = false;
        }
    }
}
