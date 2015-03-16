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
    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.TB_EquipmentStatusCode")]
    [global::System.Runtime.Serialization.DataContractAttribute()]
    public partial class TB_EquipmentStatusCode : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _id;

        private System.Nullable<int> _Status;

        private string _Code;

        private string _Name;

        private EntitySet<TB_Equipment> _TB_Equipment;

        private EntitySet<TB_EquipmentStockHistory> _TB_EquipmentStockHistory;

        private EntityRef<TB_EquipmentStatusName> _TB_EquipmentStatusName;

        private bool serializing;

        #region 可扩展性方法定义
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnidChanging(int value);
        partial void OnidChanged();
        partial void OnStatusChanging(System.Nullable<int> value);
        partial void OnStatusChanged();
        partial void OnCodeChanging(string value);
        partial void OnCodeChanged();
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        #endregion

        public TB_EquipmentStatusCode()
        {
            this.Initialize();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_id", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 1)]
        public int id
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Status", DbType = "Int")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 2)]
        public System.Nullable<int> Status
        {
            get
            {
                return this._Status;
            }
            set
            {
                if ((this._Status != value))
                {
                    if (this._TB_EquipmentStatusName.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnStatusChanging(value);
                    this.SendPropertyChanging();
                    this._Status = value;
                    this.SendPropertyChanged("Status");
                    this.OnStatusChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Code", DbType = "VarChar(5)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 3)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Name", DbType = "VarChar(10)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 4)]
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                if ((this._Name != value))
                {
                    this.OnNameChanging(value);
                    this.SendPropertyChanging();
                    this._Name = value;
                    this.SendPropertyChanged("Name");
                    this.OnNameChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "TB_EquipmentStatusCode_TB_Equipment", Storage = "_TB_Equipment", ThisKey = "id", OtherKey = "Status")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 5, EmitDefaultValue = false)]
        public EntitySet<TB_Equipment> TB_Equipment
        {
            get
            {
                if ((this.serializing
                            && (this._TB_Equipment.HasLoadedOrAssignedValues == false)))
                {
                    return null;
                }
                return this._TB_Equipment;
            }
            set
            {
                this._TB_Equipment.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "TB_EquipmentStatusCode_TB_EquipmentStockHistory", Storage = "_TB_EquipmentStockHistory", ThisKey = "id", OtherKey = "Status")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 6, EmitDefaultValue = false)]
        public EntitySet<TB_EquipmentStockHistory> TB_EquipmentStockHistory
        {
            get
            {
                if ((this.serializing
                            && (this._TB_EquipmentStockHistory.HasLoadedOrAssignedValues == false)))
                {
                    return null;
                }
                return this._TB_EquipmentStockHistory;
            }
            set
            {
                this._TB_EquipmentStockHistory.Assign(value);
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "TB_EquipmentStatusName_TB_EquipmentStatusCode", Storage = "_TB_EquipmentStatusName", ThisKey = "Status", OtherKey = "id", IsForeignKey = true, DeleteRule = "SET DEFAULT")]
        public TB_EquipmentStatusName TB_EquipmentStatusName
        {
            get
            {
                return this._TB_EquipmentStatusName.Entity;
            }
            set
            {
                TB_EquipmentStatusName previousValue = this._TB_EquipmentStatusName.Entity;
                if (((previousValue != value)
                            || (this._TB_EquipmentStatusName.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._TB_EquipmentStatusName.Entity = null;
                        previousValue.TB_EquipmentStatusCode.Remove(this);
                    }
                    this._TB_EquipmentStatusName.Entity = value;
                    if ((value != null))
                    {
                        value.TB_EquipmentStatusCode.Add(this);
                        this._Status = value.id;
                    }
                    else
                    {
                        this._Status = default(Nullable<int>);
                    }
                    this.SendPropertyChanged("TB_EquipmentStatusName");
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

        private void attach_TB_Equipment(TB_Equipment entity)
        {
            this.SendPropertyChanging();
            entity.TB_EquipmentStatusCode = this;
        }

        private void detach_TB_Equipment(TB_Equipment entity)
        {
            this.SendPropertyChanging();
            entity.TB_EquipmentStatusCode = null;
        }

        private void attach_TB_EquipmentStockHistory(TB_EquipmentStockHistory entity)
        {
            this.SendPropertyChanging();
            entity.TB_EquipmentStatusCode = this;
        }

        private void detach_TB_EquipmentStockHistory(TB_EquipmentStockHistory entity)
        {
            this.SendPropertyChanging();
            entity.TB_EquipmentStatusCode = null;
        }

        private void Initialize()
        {
            this._TB_Equipment = new EntitySet<TB_Equipment>(new Action<TB_Equipment>(this.attach_TB_Equipment), new Action<TB_Equipment>(this.detach_TB_Equipment));
            this._TB_EquipmentStockHistory = new EntitySet<TB_EquipmentStockHistory>(new Action<TB_EquipmentStockHistory>(this.attach_TB_EquipmentStockHistory), new Action<TB_EquipmentStockHistory>(this.detach_TB_EquipmentStockHistory));
            this._TB_EquipmentStatusName = default(EntityRef<TB_EquipmentStatusName>);
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
