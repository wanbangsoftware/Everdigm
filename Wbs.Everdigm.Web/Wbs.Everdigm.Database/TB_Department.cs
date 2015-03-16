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

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.TB_Department")]
    [global::System.Runtime.Serialization.DataContractAttribute()]
    public partial class TB_Department : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _id;

        private System.Nullable<bool> _Delete;

        private System.Nullable<bool> _IsDefault;

        private System.Nullable<int> _Parent;

        private string _Code;

        private string _Room;

        private string _Phone;

        private string _Fax;

        private string _Name;

        private string _Address;

        private EntitySet<TB_Account> _TB_Account;

        private bool serializing;

        #region 可扩展性方法定义
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnidChanging(int value);
        partial void OnidChanged();
        partial void OnDeleteChanging(System.Nullable<bool> value);
        partial void OnDeleteChanged();
        partial void OnIsDefaultChanging(System.Nullable<bool> value);
        partial void OnIsDefaultChanged();
        partial void OnParentChanging(System.Nullable<int> value);
        partial void OnParentChanged();
        partial void OnCodeChanging(string value);
        partial void OnCodeChanged();
        partial void OnRoomChanging(string value);
        partial void OnRoomChanged();
        partial void OnPhoneChanging(string value);
        partial void OnPhoneChanged();
        partial void OnFaxChanging(string value);
        partial void OnFaxChanged();
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        partial void OnAddressChanging(string value);
        partial void OnAddressChanged();
        #endregion

        public TB_Department()
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Delete]", Storage = "_Delete", DbType = "Bit")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 2)]
        public System.Nullable<bool> Delete
        {
            get
            {
                return this._Delete;
            }
            set
            {
                if ((this._Delete != value))
                {
                    this.OnDeleteChanging(value);
                    this.SendPropertyChanging();
                    this._Delete = value;
                    this.SendPropertyChanged("Delete");
                    this.OnDeleteChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsDefault", DbType = "Bit")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 3)]
        public System.Nullable<bool> IsDefault
        {
            get
            {
                return this._IsDefault;
            }
            set
            {
                if ((this._IsDefault != value))
                {
                    this.OnIsDefaultChanging(value);
                    this.SendPropertyChanging();
                    this._IsDefault = value;
                    this.SendPropertyChanged("IsDefault");
                    this.OnIsDefaultChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Parent", DbType = "Int")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 4)]
        public System.Nullable<int> Parent
        {
            get
            {
                return this._Parent;
            }
            set
            {
                if ((this._Parent != value))
                {
                    this.OnParentChanging(value);
                    this.SendPropertyChanging();
                    this._Parent = value;
                    this.SendPropertyChanged("Parent");
                    this.OnParentChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Code", DbType = "VarChar(10)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Room", DbType = "VarChar(20)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 6)]
        public string Room
        {
            get
            {
                return this._Room;
            }
            set
            {
                if ((this._Room != value))
                {
                    this.OnRoomChanging(value);
                    this.SendPropertyChanging();
                    this._Room = value;
                    this.SendPropertyChanged("Room");
                    this.OnRoomChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Phone", DbType = "VarChar(20)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 7)]
        public string Phone
        {
            get
            {
                return this._Phone;
            }
            set
            {
                if ((this._Phone != value))
                {
                    this.OnPhoneChanging(value);
                    this.SendPropertyChanging();
                    this._Phone = value;
                    this.SendPropertyChanged("Phone");
                    this.OnPhoneChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Fax", DbType = "VarChar(20)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 8)]
        public string Fax
        {
            get
            {
                return this._Fax;
            }
            set
            {
                if ((this._Fax != value))
                {
                    this.OnFaxChanging(value);
                    this.SendPropertyChanging();
                    this._Fax = value;
                    this.SendPropertyChanged("Fax");
                    this.OnFaxChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Name", DbType = "NVarChar(50)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 9)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Address", DbType = "NVarChar(100)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 10)]
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

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "TB_Department_TB_Account", Storage = "_TB_Account", ThisKey = "id", OtherKey = "Department")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 11, EmitDefaultValue = false)]
        public EntitySet<TB_Account> TB_Account
        {
            get
            {
                if ((this.serializing
                            && (this._TB_Account.HasLoadedOrAssignedValues == false)))
                {
                    return null;
                }
                return this._TB_Account;
            }
            set
            {
                this._TB_Account.Assign(value);
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

        private void attach_TB_Account(TB_Account entity)
        {
            this.SendPropertyChanging();
            entity.TB_Department = this;
        }

        private void detach_TB_Account(TB_Account entity)
        {
            this.SendPropertyChanging();
            entity.TB_Department = null;
        }

        private void Initialize()
        {
            this._TB_Account = new EntitySet<TB_Account>(new Action<TB_Account>(this.attach_TB_Account), new Action<TB_Account>(this.detach_TB_Account));
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
