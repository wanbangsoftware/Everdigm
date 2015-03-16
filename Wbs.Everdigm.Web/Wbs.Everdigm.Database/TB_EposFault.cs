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

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.TB_EposFault")]
    [global::System.Runtime.Serialization.DataContractAttribute()]
    public partial class TB_EposFault : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private long _id;

        private System.Nullable<System.DateTime> _ReceiveTime;

        private System.Nullable<int> _Equipment;

        private System.Nullable<int> _StoreTimes;

        private System.Nullable<int> _Count;

        private string _CodeHex;

        private string _FMIHex;

        private string _CodeName;

        private string _FMIName;

        private EntityRef<TB_Equipment> _TB_Equipment;

        #region 可扩展性方法定义
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnidChanging(long value);
        partial void OnidChanged();
        partial void OnReceiveTimeChanging(System.Nullable<System.DateTime> value);
        partial void OnReceiveTimeChanged();
        partial void OnEquipmentChanging(System.Nullable<int> value);
        partial void OnEquipmentChanged();
        partial void OnStoreTimesChanging(System.Nullable<int> value);
        partial void OnStoreTimesChanged();
        partial void OnCountChanging(System.Nullable<int> value);
        partial void OnCountChanged();
        partial void OnCodeHexChanging(string value);
        partial void OnCodeHexChanged();
        partial void OnFMIHexChanging(string value);
        partial void OnFMIHexChanged();
        partial void OnCodeNameChanging(string value);
        partial void OnCodeNameChanged();
        partial void OnFMINameChanging(string value);
        partial void OnFMINameChanged();
        #endregion

        public TB_EposFault()
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Equipment", DbType = "Int")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 3)]
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
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 4)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Count", DbType = "Int")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 5)]
        public System.Nullable<int> Count
        {
            get
            {
                return this._Count;
            }
            set
            {
                if ((this._Count != value))
                {
                    this.OnCountChanging(value);
                    this.SendPropertyChanging();
                    this._Count = value;
                    this.SendPropertyChanged("Count");
                    this.OnCountChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CodeHex", DbType = "Char(2)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 6)]
        public string CodeHex
        {
            get
            {
                return this._CodeHex;
            }
            set
            {
                if ((this._CodeHex != value))
                {
                    this.OnCodeHexChanging(value);
                    this.SendPropertyChanging();
                    this._CodeHex = value;
                    this.SendPropertyChanged("CodeHex");
                    this.OnCodeHexChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FMIHex", DbType = "Char(2)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 7)]
        public string FMIHex
        {
            get
            {
                return this._FMIHex;
            }
            set
            {
                if ((this._FMIHex != value))
                {
                    this.OnFMIHexChanging(value);
                    this.SendPropertyChanging();
                    this._FMIHex = value;
                    this.SendPropertyChanged("FMIHex");
                    this.OnFMIHexChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CodeName", DbType = "VarChar(150)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 8)]
        public string CodeName
        {
            get
            {
                return this._CodeName;
            }
            set
            {
                if ((this._CodeName != value))
                {
                    this.OnCodeNameChanging(value);
                    this.SendPropertyChanging();
                    this._CodeName = value;
                    this.SendPropertyChanged("CodeName");
                    this.OnCodeNameChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FMIName", DbType = "VarChar(150)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 9)]
        public string FMIName
        {
            get
            {
                return this._FMIName;
            }
            set
            {
                if ((this._FMIName != value))
                {
                    this.OnFMINameChanging(value);
                    this.SendPropertyChanging();
                    this._FMIName = value;
                    this.SendPropertyChanged("FMIName");
                    this.OnFMINameChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "TB_Equipment_TB_EposFault", Storage = "_TB_Equipment", ThisKey = "Equipment", OtherKey = "id", IsForeignKey = true, DeleteRule = "SET NULL")]
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
                        previousValue.TB_EposFault.Remove(this);
                    }
                    this._TB_Equipment.Entity = value;
                    if ((value != null))
                    {
                        value.TB_EposFault.Add(this);
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
