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
    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.TB_EquipmentStatusName")]
    [global::System.Runtime.Serialization.DataContractAttribute()]
    public partial class TB_EquipmentStatusName : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _id;

        private System.Nullable<bool> _IsWaiting;

        private System.Nullable<bool> _IsInventory;

        private System.Nullable<bool> _IsOutstorage;

        private System.Nullable<bool> _IsOverhaul;

        private string _Name;

        private string _Code;

        private EntitySet<TB_EquipmentStatusCode> _TB_EquipmentStatusCode;

        private bool serializing;

        #region 可扩展性方法定义
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnidChanging(int value);
        partial void OnidChanged();
        partial void OnIsWaitingChanging(System.Nullable<bool> value);
        partial void OnIsWaitingChanged();
        partial void OnIsInventoryChanging(System.Nullable<bool> value);
        partial void OnIsInventoryChanged();
        partial void OnIsOutstorageChanging(System.Nullable<bool> value);
        partial void OnIsOutstorageChanged();
        partial void OnIsOverhaulChanging(System.Nullable<bool> value);
        partial void OnIsOverhaulChanged();
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        partial void OnCodeChanging(string value);
        partial void OnCodeChanged();
        #endregion

        public TB_EquipmentStatusName()
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsWaiting", DbType = "Bit")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 2)]
        public System.Nullable<bool> IsWaiting
        {
            get
            {
                return this._IsWaiting;
            }
            set
            {
                if ((this._IsWaiting != value))
                {
                    this.OnIsWaitingChanging(value);
                    this.SendPropertyChanging();
                    this._IsWaiting = value;
                    this.SendPropertyChanged("IsWaiting");
                    this.OnIsWaitingChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsInventory", DbType = "Bit")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 3)]
        public System.Nullable<bool> IsInventory
        {
            get
            {
                return this._IsInventory;
            }
            set
            {
                if ((this._IsInventory != value))
                {
                    this.OnIsInventoryChanging(value);
                    this.SendPropertyChanging();
                    this._IsInventory = value;
                    this.SendPropertyChanged("IsInventory");
                    this.OnIsInventoryChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsOutstorage", DbType = "Bit")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 4)]
        public System.Nullable<bool> IsOutstorage
        {
            get
            {
                return this._IsOutstorage;
            }
            set
            {
                if ((this._IsOutstorage != value))
                {
                    this.OnIsOutstorageChanging(value);
                    this.SendPropertyChanging();
                    this._IsOutstorage = value;
                    this.SendPropertyChanged("IsOutstorage");
                    this.OnIsOutstorageChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsOverhaul", DbType = "Bit")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 5)]
        public System.Nullable<bool> IsOverhaul
        {
            get
            {
                return this._IsOverhaul;
            }
            set
            {
                if ((this._IsOverhaul != value))
                {
                    this.OnIsOverhaulChanging(value);
                    this.SendPropertyChanging();
                    this._IsOverhaul = value;
                    this.SendPropertyChanged("IsOverhaul");
                    this.OnIsOverhaulChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Name", DbType = "NVarChar(50)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 6)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Code", DbType = "VarChar(5)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 7)]
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

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "TB_EquipmentStatusName_TB_EquipmentStatusCode", Storage = "_TB_EquipmentStatusCode", ThisKey = "id", OtherKey = "Status")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 8, EmitDefaultValue = false)]
        public EntitySet<TB_EquipmentStatusCode> TB_EquipmentStatusCode
        {
            get
            {
                if ((this.serializing
                            && (this._TB_EquipmentStatusCode.HasLoadedOrAssignedValues == false)))
                {
                    return null;
                }
                return this._TB_EquipmentStatusCode;
            }
            set
            {
                this._TB_EquipmentStatusCode.Assign(value);
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

        private void attach_TB_EquipmentStatusCode(TB_EquipmentStatusCode entity)
        {
            this.SendPropertyChanging();
            entity.TB_EquipmentStatusName = this;
        }

        private void detach_TB_EquipmentStatusCode(TB_EquipmentStatusCode entity)
        {
            this.SendPropertyChanging();
            entity.TB_EquipmentStatusName = null;
        }

        private void Initialize()
        {
            this._TB_EquipmentStatusCode = new EntitySet<TB_EquipmentStatusCode>(new Action<TB_EquipmentStatusCode>(this.attach_TB_EquipmentStatusCode), new Action<TB_EquipmentStatusCode>(this.detach_TB_EquipmentStatusCode));
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
