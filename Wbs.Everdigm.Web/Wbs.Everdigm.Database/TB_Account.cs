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
    /// 用户信息
    /// </summary>
    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.TB_Account")]
    [global::System.Runtime.Serialization.DataContractAttribute()]
    public partial class TB_Account : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _id;

        private System.Nullable<bool> _Delete;

        private System.Nullable<bool> _Locked;

        private System.Nullable<int> _Role;

        private System.Nullable<int> _Department;

        private System.Nullable<int> _LoginTimes;

        private System.Nullable<System.DateTime> _RegisterTime;

        private System.Nullable<System.DateTime> _LastLoginTime;

        private string _LastLoginIp;

        private string _Code;

        private string _LandlineNumber;

        private string _Phone;

        private string _Email;

        private string _Password;

        private string _Name;

        private string _Question;

        private string _Answer;

        private EntitySet<TB_AccountHistory> _TB_AccountHistory;

        private EntityRef<TB_Department> _TB_Department;

        private EntityRef<TB_Role> _TB_Role;

        private bool serializing;

        #region 可扩展性方法定义
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnidChanging(int value);
        partial void OnidChanged();
        partial void OnDeleteChanging(System.Nullable<bool> value);
        partial void OnDeleteChanged();
        partial void OnLockedChanging(System.Nullable<bool> value);
        partial void OnLockedChanged();
        partial void OnRoleChanging(System.Nullable<int> value);
        partial void OnRoleChanged();
        partial void OnDepartmentChanging(System.Nullable<int> value);
        partial void OnDepartmentChanged();
        partial void OnLoginTimesChanging(System.Nullable<int> value);
        partial void OnLoginTimesChanged();
        partial void OnRegisterTimeChanging(System.Nullable<System.DateTime> value);
        partial void OnRegisterTimeChanged();
        partial void OnLastLoginTimeChanging(System.Nullable<System.DateTime> value);
        partial void OnLastLoginTimeChanged();
        partial void OnLastLoginIpChanging(string value);
        partial void OnLastLoginIpChanged();
        partial void OnCodeChanging(string value);
        partial void OnCodeChanged();
        partial void OnLandlineNumberChanging(string value);
        partial void OnLandlineNumberChanged();
        partial void OnPhoneChanging(string value);
        partial void OnPhoneChanged();
        partial void OnEmailChanging(string value);
        partial void OnEmailChanged();
        partial void OnPasswordChanging(string value);
        partial void OnPasswordChanged();
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        partial void OnQuestionChanging(string value);
        partial void OnQuestionChanged();
        partial void OnAnswerChanging(string value);
        partial void OnAnswerChanged();
        #endregion

        public TB_Account()
        {
            this.Initialize();
        }
        /// <summary>
        /// 用户ID，主键，自动增长
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
        /// 标记用户是否已删除，已删除的用户不能再继续登陆，也不能再在用户列表里查看到到记录
        /// </summary>
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
        /// <summary>
        /// 标记用户是否已锁定，已锁定的用户不能再登陆，但能在系统用户列表里查看到记录
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Locked", DbType = "Bit")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 3)]
        public System.Nullable<bool> Locked
        {
            get
            {
                return this._Locked;
            }
            set
            {
                if ((this._Locked != value))
                {
                    this.OnLockedChanging(value);
                    this.SendPropertyChanging();
                    this._Locked = value;
                    this.SendPropertyChanged("Locked");
                    this.OnLockedChanged();
                }
            }
        }
        /// <summary>
        /// 用户所属角色的ID
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Role", DbType = "Int")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 4)]
        public System.Nullable<int> Role
        {
            get
            {
                return this._Role;
            }
            set
            {
                if ((this._Role != value))
                {
                    if (this._TB_Role.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnRoleChanging(value);
                    this.SendPropertyChanging();
                    this._Role = value;
                    this.SendPropertyChanged("Role");
                    this.OnRoleChanged();
                }
            }
        }
        /// <summary>
        /// 用户所属部门的ID
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Department", DbType = "Int")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 5)]
        public System.Nullable<int> Department
        {
            get
            {
                return this._Department;
            }
            set
            {
                if ((this._Department != value))
                {
                    if (this._TB_Department.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnDepartmentChanging(value);
                    this.SendPropertyChanging();
                    this._Department = value;
                    this.SendPropertyChanged("Department");
                    this.OnDepartmentChanged();
                }
            }
        }
        /// <summary>
        /// 用户登录次数，每次成功登陆之后增加1
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LoginTimes", DbType = "Int")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 6)]
        public System.Nullable<int> LoginTimes
        {
            get
            {
                return this._LoginTimes;
            }
            set
            {
                if ((this._LoginTimes != value))
                {
                    this.OnLoginTimesChanging(value);
                    this.SendPropertyChanging();
                    this._LoginTimes = value;
                    this.SendPropertyChanged("LoginTimes");
                    this.OnLoginTimesChanged();
                }
            }
        }
        /// <summary>
        /// 用户注册时间
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RegisterTime", DbType = "DateTime")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 7)]
        public System.Nullable<System.DateTime> RegisterTime
        {
            get
            {
                return this._RegisterTime;
            }
            set
            {
                if ((this._RegisterTime != value))
                {
                    this.OnRegisterTimeChanging(value);
                    this.SendPropertyChanging();
                    this._RegisterTime = value;
                    this.SendPropertyChanged("RegisterTime");
                    this.OnRegisterTimeChanged();
                }
            }
        }
        /// <summary>
        /// 用户最后登陆系统的时间
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LastLoginTime", DbType = "DateTime")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 8)]
        public System.Nullable<System.DateTime> LastLoginTime
        {
            get
            {
                return this._LastLoginTime;
            }
            set
            {
                if ((this._LastLoginTime != value))
                {
                    this.OnLastLoginTimeChanging(value);
                    this.SendPropertyChanging();
                    this._LastLoginTime = value;
                    this.SendPropertyChanged("LastLoginTime");
                    this.OnLastLoginTimeChanged();
                }
            }
        }
        /// <summary>
        /// 用户最后登陆系统所用电脑的IP地址
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LastLoginIp", DbType = "VarChar(20)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 9)]
        public string LastLoginIp
        {
            get
            {
                return this._LastLoginIp;
            }
            set
            {
                if ((this._LastLoginIp != value))
                {
                    this.OnLastLoginIpChanging(value);
                    this.SendPropertyChanging();
                    this._LastLoginIp = value;
                    this.SendPropertyChanged("LastLoginIp");
                    this.OnLastLoginIpChanged();
                }
            }
        }
        /// <summary>
        /// 用户的登录账号
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Code", DbType = "VarChar(20)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 10)]
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
        /// <summary>
        /// 用户的座机电话号码
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LandlineNumber", DbType = "VarChar(20)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 11)]
        public string LandlineNumber
        {
            get
            {
                return this._LandlineNumber;
            }
            set
            {
                if ((this._LandlineNumber != value))
                {
                    this.OnLandlineNumberChanging(value);
                    this.SendPropertyChanging();
                    this._LandlineNumber = value;
                    this.SendPropertyChanged("LandlineNumber");
                    this.OnLandlineNumberChanged();
                }
            }
        }
        /// <summary>
        /// 用户的手机号码
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Phone", DbType = "VarChar(20)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 12)]
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
        /// <summary>
        /// 用户的电子邮件地址
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Email", DbType = "VarChar(50)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 13)]
        public string Email
        {
            get
            {
                return this._Email;
            }
            set
            {
                if ((this._Email != value))
                {
                    this.OnEmailChanging(value);
                    this.SendPropertyChanging();
                    this._Email = value;
                    this.SendPropertyChanged("Email");
                    this.OnEmailChanged();
                }
            }
        }
        /// <summary>
        /// 用户的登陆密码，加密后的密文
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Password", DbType = "VarChar(50)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 14)]
        public string Password
        {
            get
            {
                return this._Password;
            }
            set
            {
                if ((this._Password != value))
                {
                    this.OnPasswordChanging(value);
                    this.SendPropertyChanging();
                    this._Password = value;
                    this.SendPropertyChanged("Password");
                    this.OnPasswordChanged();
                }
            }
        }
        /// <summary>
        /// 用户的姓名
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Name", DbType = "NVarChar(50)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 15)]
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
        /// <summary>
        /// 用户的密码寻回问题
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Question", DbType = "NVarChar(50)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 16)]
        public string Question
        {
            get
            {
                return this._Question;
            }
            set
            {
                if ((this._Question != value))
                {
                    this.OnQuestionChanging(value);
                    this.SendPropertyChanging();
                    this._Question = value;
                    this.SendPropertyChanged("Question");
                    this.OnQuestionChanged();
                }
            }
        }
        /// <summary>
        /// 用户的密码寻回问题答案，加密后的密文
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Answer", DbType = "NVarChar(50)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 17)]
        public string Answer
        {
            get
            {
                return this._Answer;
            }
            set
            {
                if ((this._Answer != value))
                {
                    this.OnAnswerChanging(value);
                    this.SendPropertyChanging();
                    this._Answer = value;
                    this.SendPropertyChanged("Answer");
                    this.OnAnswerChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "TB_Account_TB_AccountHistory", Storage = "_TB_AccountHistory", ThisKey = "id", OtherKey = "Account")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 18, EmitDefaultValue = false)]
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
        /// <summary>
        /// 用户所属部门的基本信息
        /// </summary>
        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "TB_Department_TB_Account", Storage = "_TB_Department", ThisKey = "Department", OtherKey = "id", IsForeignKey = true, DeleteRule = "SET DEFAULT")]
        public TB_Department TB_Department
        {
            get
            {
                return this._TB_Department.Entity;
            }
            set
            {
                TB_Department previousValue = this._TB_Department.Entity;
                if (((previousValue != value)
                            || (this._TB_Department.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._TB_Department.Entity = null;
                        previousValue.TB_Account.Remove(this);
                    }
                    this._TB_Department.Entity = value;
                    if ((value != null))
                    {
                        value.TB_Account.Add(this);
                        this._Department = value.id;
                    }
                    else
                    {
                        this._Department = default(Nullable<int>);
                    }
                    this.SendPropertyChanged("TB_Department");
                }
            }
        }
        /// <summary>
        /// 用户所属角色的基本信息
        /// </summary>
        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "TB_Role_TB_Account", Storage = "_TB_Role", ThisKey = "Role", OtherKey = "id", IsForeignKey = true, DeleteRule = "SET DEFAULT")]
        public TB_Role TB_Role
        {
            get
            {
                return this._TB_Role.Entity;
            }
            set
            {
                TB_Role previousValue = this._TB_Role.Entity;
                if (((previousValue != value)
                            || (this._TB_Role.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._TB_Role.Entity = null;
                        previousValue.TB_Account.Remove(this);
                    }
                    this._TB_Role.Entity = value;
                    if ((value != null))
                    {
                        value.TB_Account.Add(this);
                        this._Role = value.id;
                    }
                    else
                    {
                        this._Role = default(Nullable<int>);
                    }
                    this.SendPropertyChanged("TB_Role");
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

        private void attach_TB_AccountHistory(TB_AccountHistory entity)
        {
            this.SendPropertyChanging();
            entity.TB_Account = this;
        }

        private void detach_TB_AccountHistory(TB_AccountHistory entity)
        {
            this.SendPropertyChanging();
            entity.TB_Account = null;
        }

        private void Initialize()
        {
            this._TB_AccountHistory = new EntitySet<TB_AccountHistory>(new Action<TB_AccountHistory>(this.attach_TB_AccountHistory), new Action<TB_AccountHistory>(this.detach_TB_AccountHistory));
            this._TB_Department = default(EntityRef<TB_Department>);
            this._TB_Role = default(EntityRef<TB_Role>);
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
