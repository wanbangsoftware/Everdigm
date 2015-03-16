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
    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.TB_Customer")]
    [global::System.Runtime.Serialization.DataContractAttribute()]
    public partial class TB_Customer : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _id;

        private System.Nullable<bool> _Delete;

        private System.Nullable<System.DateTime> _RegisterDate;

        private System.Nullable<System.DateTime> _SignInTime;

        private string _SignInIP;

        private string _SignInDevice;

        private string _Code;

        private string _Phone;

        private string _Password;

        private string _Answer;

        private string _IdCard;

        private string _Name;

        private string _Address;

        private string _Question;

        private EntitySet<TB_Equipment> _TB_Equipment;

        private bool serializing;

        #region 可扩展性方法定义
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnidChanging(int value);
        partial void OnidChanged();
        partial void OnDeleteChanging(System.Nullable<bool> value);
        partial void OnDeleteChanged();
        partial void OnRegisterDateChanging(System.Nullable<System.DateTime> value);
        partial void OnRegisterDateChanged();
        partial void OnSignInTimeChanging(System.Nullable<System.DateTime> value);
        partial void OnSignInTimeChanged();
        partial void OnSignInIPChanging(string value);
        partial void OnSignInIPChanged();
        partial void OnSignInDeviceChanging(string value);
        partial void OnSignInDeviceChanged();
        partial void OnCodeChanging(string value);
        partial void OnCodeChanged();
        partial void OnPhoneChanging(string value);
        partial void OnPhoneChanged();
        partial void OnPasswordChanging(string value);
        partial void OnPasswordChanged();
        partial void OnAnswerChanging(string value);
        partial void OnAnswerChanged();
        partial void OnIdCardChanging(string value);
        partial void OnIdCardChanged();
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        partial void OnAddressChanging(string value);
        partial void OnAddressChanged();
        partial void OnQuestionChanging(string value);
        partial void OnQuestionChanged();
        #endregion

        public TB_Customer()
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RegisterDate", DbType = "DateTime")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 3)]
        public System.Nullable<System.DateTime> RegisterDate
        {
            get
            {
                return this._RegisterDate;
            }
            set
            {
                if ((this._RegisterDate != value))
                {
                    this.OnRegisterDateChanging(value);
                    this.SendPropertyChanging();
                    this._RegisterDate = value;
                    this.SendPropertyChanged("RegisterDate");
                    this.OnRegisterDateChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SignInTime", DbType = "DateTime")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 4)]
        public System.Nullable<System.DateTime> SignInTime
        {
            get
            {
                return this._SignInTime;
            }
            set
            {
                if ((this._SignInTime != value))
                {
                    this.OnSignInTimeChanging(value);
                    this.SendPropertyChanging();
                    this._SignInTime = value;
                    this.SendPropertyChanged("SignInTime");
                    this.OnSignInTimeChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SignInIP", DbType = "VarChar(20)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 5)]
        public string SignInIP
        {
            get
            {
                return this._SignInIP;
            }
            set
            {
                if ((this._SignInIP != value))
                {
                    this.OnSignInIPChanging(value);
                    this.SendPropertyChanging();
                    this._SignInIP = value;
                    this.SendPropertyChanged("SignInIP");
                    this.OnSignInIPChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SignInDevice", DbType = "VarChar(20)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 6)]
        public string SignInDevice
        {
            get
            {
                return this._SignInDevice;
            }
            set
            {
                if ((this._SignInDevice != value))
                {
                    this.OnSignInDeviceChanging(value);
                    this.SendPropertyChanging();
                    this._SignInDevice = value;
                    this.SendPropertyChanged("SignInDevice");
                    this.OnSignInDeviceChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Code", DbType = "VarChar(20)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Phone", DbType = "VarChar(20)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 8)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Password", DbType = "VarChar(32)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 9)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Answer", DbType = "VarChar(32)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 10)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IdCard", DbType = "VarChar(50)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 11)]
        public string IdCard
        {
            get
            {
                return this._IdCard;
            }
            set
            {
                if ((this._IdCard != value))
                {
                    this.OnIdCardChanging(value);
                    this.SendPropertyChanging();
                    this._IdCard = value;
                    this.SendPropertyChanged("IdCard");
                    this.OnIdCardChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Name", DbType = "NVarChar(50)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 12)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Address", DbType = "NVarChar(50)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 13)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Question", DbType = "NVarChar(50)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 14)]
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

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "TB_Customer_TB_Equipment", Storage = "_TB_Equipment", ThisKey = "id", OtherKey = "Customer")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 15, EmitDefaultValue = false)]
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
            entity.TB_Customer = this;
        }

        private void detach_TB_Equipment(TB_Equipment entity)
        {
            this.SendPropertyChanging();
            entity.TB_Customer = null;
        }

        private void Initialize()
        {
            this._TB_Equipment = new EntitySet<TB_Equipment>(new Action<TB_Equipment>(this.attach_TB_Equipment), new Action<TB_Equipment>(this.detach_TB_Equipment));
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
