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

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.TB_Alarm")]
    [global::System.Runtime.Serialization.DataContractAttribute()]
    public partial class TB_Alarm : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private long _id;

        private System.Nullable<int> _Equipment;

        private System.Nullable<int> _StoreTimes;

        private System.Nullable<long> _Position;

        private string _Code;

        private EntityRef<TB_Position> _TB_Position;

        private EntityRef<TB_Equipment> _TB_Equipment;

        #region 可扩展性方法定义
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnidChanging(long value);
        partial void OnidChanged();
        partial void OnEquipmentChanging(System.Nullable<int> value);
        partial void OnEquipmentChanged();
        partial void OnStoreTimesChanging(System.Nullable<int> value);
        partial void OnStoreTimesChanged();
        partial void OnPositionChanging(System.Nullable<long> value);
        partial void OnPositionChanged();
        partial void OnCodeChanging(string value);
        partial void OnCodeChanged();
        #endregion

        public TB_Alarm()
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Equipment", DbType = "Int")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 2)]
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
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 3)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Position", DbType = "BigInt")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 4)]
        public System.Nullable<long> Position
        {
            get
            {
                return this._Position;
            }
            set
            {
                if ((this._Position != value))
                {
                    if (this._TB_Position.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnPositionChanging(value);
                    this.SendPropertyChanging();
                    this._Position = value;
                    this.SendPropertyChanged("Position");
                    this.OnPositionChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Code", DbType = "Char(16)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 5)]
        public string Code
        {
            get
            {
                return this._Code;
            }
            set
            {
                if ((this._Code != value))
                {
                    this.OnCodeChanging(value);
                    this.SendPropertyChanging();
                    this._Code = value;
                    this.SendPropertyChanged("Code");
                    this.OnCodeChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "TB_Position_TB_Alarm", Storage = "_TB_Position", ThisKey = "Position", OtherKey = "id", IsForeignKey = true)]
        public TB_Position TB_Position
        {
            get
            {
                return this._TB_Position.Entity;
            }
            set
            {
                TB_Position previousValue = this._TB_Position.Entity;
                if (((previousValue != value)
                            || (this._TB_Position.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._TB_Position.Entity = null;
                        previousValue.TB_Alarm.Remove(this);
                    }
                    this._TB_Position.Entity = value;
                    if ((value != null))
                    {
                        value.TB_Alarm.Add(this);
                        this._Position = value.id;
                    }
                    else
                    {
                        this._Position = default(Nullable<long>);
                    }
                    this.SendPropertyChanged("TB_Position");
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "TB_Equipment_TB_Alarm", Storage = "_TB_Equipment", ThisKey = "Equipment", OtherKey = "id", IsForeignKey = true)]
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
                        previousValue.TB_Alarm.Remove(this);
                    }
                    this._TB_Equipment.Entity = value;
                    if ((value != null))
                    {
                        value.TB_Alarm.Add(this);
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

        private void Initialize()
        {
            this._TB_Position = default(EntityRef<TB_Position>);
            this._TB_Equipment = default(EntityRef<TB_Equipment>);
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
