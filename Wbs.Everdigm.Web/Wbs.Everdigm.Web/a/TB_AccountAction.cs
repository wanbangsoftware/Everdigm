using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wbs.Everdigm.Database
{
    using System.ComponentModel;
    using System.Data.Linq;
    using System.Data.Linq.Mapping;
    using System.Runtime.Serialization;

    /// <summary>
    /// 用户操作类型
    /// </summary>
    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.TB_AccountAction")]
    [global::System.Runtime.Serialization.DataContractAttribute()]
    public partial class TB_AccountAction : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _id;

        private string _Name;

        private string _Description;

        private EntitySet<TB_AccountHistory> _TB_AccountHistory;

        private bool serializing;

        #region 可扩展性方法定义
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnidChanging(int value);
        partial void OnidChanged();
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        partial void OnDescriptionChanging(string value);
        partial void OnDescriptionChanged();
        #endregion

        public TB_AccountAction()
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Name", DbType = "NVarChar(50)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 2)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Description", DbType = "NVarChar(50)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 3)]
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

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "TB_AccountAction_TB_AccountHistory", Storage = "_TB_AccountHistory", ThisKey = "id", OtherKey = "ActionId")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 4, EmitDefaultValue = false)]
        public EntitySet<TB_AccountHistory> TB_AccountHistory
        {
            get
            {
                if ((this.serializing
                            && (this._TB_AccountHistory.HasLoadedOrAssignedValues == false)))
                {
                    return null;
                }
                return this._TB_AccountHistory;
            }
            set
            {
                this._TB_AccountHistory.Assign(value);
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

        private void attach_TB_AccountHistory(TB_AccountHistory entity)
        {
            this.SendPropertyChanging();
            entity.TB_AccountAction = this;
        }

        private void detach_TB_AccountHistory(TB_AccountHistory entity)
        {
            this.SendPropertyChanging();
            entity.TB_AccountAction = null;
        }

        private void Initialize()
        {
            this._TB_AccountHistory = new EntitySet<TB_AccountHistory>(new Action<TB_AccountHistory>(this.attach_TB_AccountHistory), new Action<TB_AccountHistory>(this.detach_TB_AccountHistory));
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
