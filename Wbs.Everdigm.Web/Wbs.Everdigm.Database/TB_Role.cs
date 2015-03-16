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

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.TB_Role")]
    [global::System.Runtime.Serialization.DataContractAttribute()]
    public partial class TB_Role : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _id;

        private System.Nullable<bool> _Delete;

        private System.Nullable<bool> _IsDefault;

        private System.Nullable<bool> _IsAdministrator;

        private System.Nullable<System.DateTime> _AddTime;

        private string _Name;

        private string _Description;

        private string _Permission;

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
        partial void OnIsAdministratorChanging(System.Nullable<bool> value);
        partial void OnIsAdministratorChanged();
        partial void OnAddTimeChanging(System.Nullable<System.DateTime> value);
        partial void OnAddTimeChanged();
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        partial void OnDescriptionChanging(string value);
        partial void OnDescriptionChanged();
        partial void OnPermissionChanging(string value);
        partial void OnPermissionChanged();
        #endregion

        public TB_Role()
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsAdministrator", DbType = "Bit")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 4)]
        public System.Nullable<bool> IsAdministrator
        {
            get
            {
                return this._IsAdministrator;
            }
            set
            {
                if ((this._IsAdministrator != value))
                {
                    this.OnIsAdministratorChanging(value);
                    this.SendPropertyChanging();
                    this._IsAdministrator = value;
                    this.SendPropertyChanged("IsAdministrator");
                    this.OnIsAdministratorChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AddTime", DbType = "DateTime")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 5)]
        public System.Nullable<System.DateTime> AddTime
        {
            get
            {
                return this._AddTime;
            }
            set
            {
                if ((this._AddTime != value))
                {
                    this.OnAddTimeChanging(value);
                    this.SendPropertyChanging();
                    this._AddTime = value;
                    this.SendPropertyChanged("AddTime");
                    this.OnAddTimeChanged();
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Description", DbType = "NVarChar(200)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 7)]
        public string Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                if ((this._Description != value))
                {
                    this.OnDescriptionChanging(value);
                    this.SendPropertyChanging();
                    this._Description = value;
                    this.SendPropertyChanged("Description");
                    this.OnDescriptionChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Permission", DbType = "VarChar(2000)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 8)]
        public string Permission
        {
            get
            {
                return this._Permission;
            }
            set
            {
                if ((this._Permission != value))
                {
                    this.OnPermissionChanging(value);
                    this.SendPropertyChanging();
                    this._Permission = value;
                    this.SendPropertyChanged("Permission");
                    this.OnPermissionChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "TB_Role_TB_Account", Storage = "_TB_Account", ThisKey = "id", OtherKey = "Role")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 9, EmitDefaultValue = false)]
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
            entity.TB_Role = this;
        }

        private void detach_TB_Account(TB_Account entity)
        {
            this.SendPropertyChanging();
            entity.TB_Role = null;
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
