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

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.TB_Satellite")]
    [global::System.Runtime.Serialization.DataContractAttribute()]
    public partial class TB_Satellite : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _id;

        private System.Nullable<bool> _Bound;

        private System.Nullable<System.DateTime> _RegisterDate;

        private string _CardNo;

        private EntitySet<TB_Terminal> _TB_Terminal;

        private bool serializing;

        #region 可扩展性方法定义
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnidChanging(int value);
        partial void OnidChanged();
        partial void OnBoundChanging(System.Nullable<bool> value);
        partial void OnBoundChanged();
        partial void OnRegisterDateChanging(System.Nullable<System.DateTime> value);
        partial void OnRegisterDateChanged();
        partial void OnCardNoChanging(string value);
        partial void OnCardNoChanged();
        #endregion

        public TB_Satellite()
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Bound", DbType = "Bit")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 2)]
        public System.Nullable<bool> Bound
        {
            get
            {
                return this._Bound;
            }
            set
            {
                if ((this._Bound != value))
                {
                    this.OnBoundChanging(value);
                    this.SendPropertyChanging();
                    this._Bound = value;
                    this.SendPropertyChanged("Bound");
                    this.OnBoundChanged();
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_CardNo", DbType = "VarChar(10)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 4)]
        public string CardNo
        {
            get
            {
                return this._CardNo;
            }
            set
            {
                if ((this._CardNo != value))
                {
                    this.OnCardNoChanging(value);
                    this.SendPropertyChanging();
                    this._CardNo = value;
                    this.SendPropertyChanged("CardNo");
                    this.OnCardNoChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "TB_Satellite_TB_Terminal", Storage = "_TB_Terminal", ThisKey = "id", OtherKey = "Satellite")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 5, EmitDefaultValue = false)]
        public EntitySet<TB_Terminal> TB_Terminal
        {
            get
            {
                if ((this.serializing
                            && (this._TB_Terminal.HasLoadedOrAssignedValues == false)))
                {
                    return null;
                }
                return this._TB_Terminal;
            }
            set
            {
                this._TB_Terminal.Assign(value);
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

        private void attach_TB_Terminal(TB_Terminal entity)
        {
            this.SendPropertyChanging();
            entity.TB_Satellite = this;
        }

        private void detach_TB_Terminal(TB_Terminal entity)
        {
            this.SendPropertyChanging();
            entity.TB_Satellite = null;
        }

        private void Initialize()
        {
            this._TB_Terminal = new EntitySet<TB_Terminal>(new Action<TB_Terminal>(this.attach_TB_Terminal), new Action<TB_Terminal>(this.detach_TB_Terminal));
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
