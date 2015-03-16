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
    /// 设备库存状态
    /// </summary>
    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.TB_EquipmentStatusName")]
    public partial class TB_EquipmentStatusName : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _id;

        private string _Name;

        private string _Code;

        private EntitySet<TB_EquipmentStatusCode> _TB_EquipmentStatusCode;

        #region 可扩展性方法定义
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnidChanging(int value);
        partial void OnidChanged();
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        partial void OnCodeChanging(string value);
        partial void OnCodeChanged();
        #endregion

        public TB_EquipmentStatusName()
        {
            this._TB_EquipmentStatusCode = new EntitySet<TB_EquipmentStatusCode>(new Action<TB_EquipmentStatusCode>(this.attach_TB_EquipmentStatusCode), new Action<TB_EquipmentStatusCode>(this.detach_TB_EquipmentStatusCode));
            OnCreated();
        }
        /// <summary>
        /// 主键ID
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
        /// 状态名称，如待入库、入库、出库、检修等
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
        /// 状态码，如W、I、S、O等
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Code", DbType = "VarChar(5)")]
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
        public EntitySet<TB_EquipmentStatusCode> TB_EquipmentStatusCode
        {
            get
            {
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
    }
	
}
