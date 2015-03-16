using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Wbs.Everdigm.Database
{
    /// <summary>
    /// 客户信息
    /// </summary>
    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.TB_Customer")]
    public partial class TB_Customer : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _id;

        private string _Name;

        private string _Phone;

        private string _IdCard;

        private string _Address;

        private EntitySet<TB_Equipment> _TB_Equipment;

        #region 可扩展性方法定义
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnidChanging(int value);
        partial void OnidChanged();
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        partial void OnPhoneChanging(string value);
        partial void OnPhoneChanged();
        partial void OnIdCardChanging(string value);
        partial void OnIdCardChanged();
        partial void OnAddressChanging(string value);
        partial void OnAddressChanged();
        #endregion

        public TB_Customer()
        {
            this._TB_Equipment = new EntitySet<TB_Equipment>(new Action<TB_Equipment>(this.attach_TB_Equipment), new Action<TB_Equipment>(this.detach_TB_Equipment));
            OnCreated();
        }
        /// <summary>
        /// 客户ID，主键
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_id", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
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
        /// 客户名称
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Name", DbType = "NVarChar(50)")]
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
        /// 客户电话
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Phone", DbType = "VarChar(20)")]
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
        /// 客户身份号码
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IdCard", DbType = "VarChar(50)")]
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
        /// <summary>
        /// 客户地址
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Address", DbType = "NVarChar(50)")]
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

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "TB_Customer_TB_Equipment", Storage = "_TB_Equipment", ThisKey = "id", OtherKey = "Customer")]
        public EntitySet<TB_Equipment> TB_Equipment
        {
            get
            {
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
    }
	
}
