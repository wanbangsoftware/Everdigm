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
    /// <summary>
    /// 用户操作历史记录
    /// </summary>
    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.TB_AccountHistory")]
    [global::System.Runtime.Serialization.DataContractAttribute()]
    public partial class TB_AccountHistory : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _id;

        private System.Nullable<int> _Account;

        private System.Nullable<int> _ActionId;

        private System.Nullable<System.DateTime> _ActionTime;

        private string _Ip;

        private string _ObjectA;

        private string _ObjectB;

        private string _ObjectC;

        private EntityRef<TB_AccountAction> _TB_AccountAction;

        private EntityRef<TB_Account> _TB_Account;

        #region 可扩展性方法定义
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnidChanging(int value);
        partial void OnidChanged();
        partial void OnAccountChanging(System.Nullable<int> value);
        partial void OnAccountChanged();
        partial void OnActionIdChanging(System.Nullable<int> value);
        partial void OnActionIdChanged();
        partial void OnActionTimeChanging(System.Nullable<System.DateTime> value);
        partial void OnActionTimeChanged();
        partial void OnIpChanging(string value);
        partial void OnIpChanged();
        partial void OnObjectAChanging(string value);
        partial void OnObjectAChanged();
        partial void OnObjectBChanging(string value);
        partial void OnObjectBChanged();
        partial void OnObjectCChanging(string value);
        partial void OnObjectCChanged();
        #endregion

        public TB_AccountHistory()
        {
            this.Initialize();
        }
        /// <summary>
        /// 历史记录ID，主键，自动增长
        /// </summary>
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
        /// <summary>
        /// 历史记录所属用户的ID，外键
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Account", DbType = "Int")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 2)]
        public System.Nullable<int> Account
        {
            get
            {
                return this._Account;
            }
            set
            {
                if ((this._Account != value))
                {
                    if (this._TB_Account.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnAccountChanging(value);
                    this.SendPropertyChanging();
                    this._Account = value;
                    this.SendPropertyChanged("Account");
                    this.OnAccountChanged();
                }
            }
        }
        /// <summary>
        /// 历史记录所属ActionID，外键
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ActionId", DbType = "Int")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 3)]
        public System.Nullable<int> ActionId
        {
            get
            {
                return this._ActionId;
            }
            set
            {
                if ((this._ActionId != value))
                {
                    if (this._TB_AccountAction.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnActionIdChanging(value);
                    this.SendPropertyChanging();
                    this._ActionId = value;
                    this.SendPropertyChanged("ActionId");
                    this.OnActionIdChanged();
                }
            }
        }
        /// <summary>
        /// 历史记录发生时间
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ActionTime", DbType = "DateTime")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 4)]
        public System.Nullable<System.DateTime> ActionTime
        {
            get
            {
                return this._ActionTime;
            }
            set
            {
                if ((this._ActionTime != value))
                {
                    this.OnActionTimeChanging(value);
                    this.SendPropertyChanging();
                    this._ActionTime = value;
                    this.SendPropertyChanged("ActionTime");
                    this.OnActionTimeChanged();
                }
            }
        }
        /// <summary>
        /// 历史记录发生时用户所用的IP地址
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Ip", DbType = "VarChar(20)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 5)]
        public string Ip
        {
            get
            {
                return this._Ip;
            }
            set
            {
                if ((this._Ip != value))
                {
                    this.OnIpChanging(value);
                    this.SendPropertyChanging();
                    this._Ip = value;
                    this.SendPropertyChanged("Ip");
                    this.OnIpChanged();
                }
            }
        }
        /// <summary>
        /// 用户操作对象
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ObjectA", DbType = "NVarChar(50)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 6)]
        public string ObjectA
        {
            get
            {
                return this._ObjectA;
            }
            set
            {
                if ((this._ObjectA != value))
                {
                    this.OnObjectAChanging(value);
                    this.SendPropertyChanging();
                    this._ObjectA = value;
                    this.SendPropertyChanged("ObjectA");
                    this.OnObjectAChanged();
                }
            }
        }
        /// <summary>
        /// 用户操作对象2
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ObjectB", DbType = "NVarChar(50)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 7)]
        public string ObjectB
        {
            get
            {
                return this._ObjectB;
            }
            set
            {
                if ((this._ObjectB != value))
                {
                    this.OnObjectBChanging(value);
                    this.SendPropertyChanging();
                    this._ObjectB = value;
                    this.SendPropertyChanged("ObjectB");
                    this.OnObjectBChanged();
                }
            }
        }
        /// <summary>
        /// 用户操作对象3
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ObjectC", DbType = "NVarChar(50)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 8)]
        public string ObjectC
        {
            get
            {
                return this._ObjectC;
            }
            set
            {
                if ((this._ObjectC != value))
                {
                    this.OnObjectCChanging(value);
                    this.SendPropertyChanging();
                    this._ObjectC = value;
                    this.SendPropertyChanged("ObjectC");
                    this.OnObjectCChanged();
                }
            }
        }
        /// <summary>
        /// 历史记录Action的详细信息
        /// </summary>
        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "TB_AccountAction_TB_AccountHistory", Storage = "_TB_AccountAction", ThisKey = "ActionId", OtherKey = "id", IsForeignKey = true, DeleteRule = "SET DEFAULT")]
        public TB_AccountAction TB_AccountAction
        {
            get
            {
                return this._TB_AccountAction.Entity;
            }
            set
            {
                TB_AccountAction previousValue = this._TB_AccountAction.Entity;
                if (((previousValue != value)
                            || (this._TB_AccountAction.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._TB_AccountAction.Entity = null;
                        previousValue.TB_AccountHistory.Remove(this);
                    }
                    this._TB_AccountAction.Entity = value;
                    if ((value != null))
                    {
                        value.TB_AccountHistory.Add(this);
                        this._ActionId = value.id;
                    }
                    else
                    {
                        this._ActionId = default(Nullable<int>);
                    }
                    this.SendPropertyChanged("TB_AccountAction");
                }
            }
        }
        /// <summary>
        /// 历史记录所属用户的基本信息
        /// </summary>
        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "TB_Account_TB_AccountHistory", Storage = "_TB_Account", ThisKey = "Account", OtherKey = "id", IsForeignKey = true, DeleteRule = "SET DEFAULT")]
        public TB_Account TB_Account
        {
            get
            {
                return this._TB_Account.Entity;
            }
            set
            {
                TB_Account previousValue = this._TB_Account.Entity;
                if (((previousValue != value)
                            || (this._TB_Account.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._TB_Account.Entity = null;
                        previousValue.TB_AccountHistory.Remove(this);
                    }
                    this._TB_Account.Entity = value;
                    if ((value != null))
                    {
                        value.TB_AccountHistory.Add(this);
                        this._Account = value.id;
                    }
                    else
                    {
                        this._Account = default(Nullable<int>);
                    }
                    this.SendPropertyChanged("TB_Account");
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
            this._TB_AccountAction = default(EntityRef<TB_AccountAction>);
            this._TB_Account = default(EntityRef<TB_Account>);
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
