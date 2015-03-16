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
    /// 仓库信息
    /// </summary>
    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.TB_Warehouse")]
    public partial class TB_Warehouse : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _id;

        private string _Code;

        private string _Name;

        private string _Address;

        private EntitySet<TB_Equipment> _TB_Equipment;

        private EntitySet<TB_EquipmentStockHistory> _TB_EquipmentStockHistory;

        #region 可扩展性方法定义
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnidChanging(int value);
        partial void OnidChanged();
        partial void OnCodeChanging(string value);
        partial void OnCodeChanged();
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        partial void OnAddressChanging(string value);
        partial void OnAddressChanged();
        #endregion

        public TB_Warehouse()
        {
            this._TB_Equipment = new EntitySet<TB_Equipment>(new Action<TB_Equipment>(this.attach_TB_Equipment), new Action<TB_Equipment>(this.detach_TB_Equipment));
            this._TB_EquipmentStockHistory = new EntitySet<TB_EquipmentStockHistory>(new Action<TB_EquipmentStockHistory>(this.attach_TB_EquipmentStockHistory), new Action<TB_EquipmentStockHistory>(this.detach_TB_EquipmentStockHistory));
            OnCreated();
        }
        /// <summary>
        /// 仓库id，主键
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
        /// 仓库简码，如WH1、WH2等
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Code", DbType = "VarChar(10)")]
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
        /// 仓库全名称
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Name", DbType = "NVarChar(100)")]
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
        /// 仓库地址
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Address", DbType = "NVarChar(100)")]
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

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "TB_Warehouse_TB_Equipment", Storage = "_TB_Equipment", ThisKey = "id", OtherKey = "Warehouse")]
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

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "TB_Warehouse_TB_EquipmentStockHistory", Storage = "_TB_EquipmentStockHistory", ThisKey = "id", OtherKey = "Warehouse")]
        public EntitySet<TB_EquipmentStockHistory> TB_EquipmentStockHistory
        {
            get
            {
                return this._TB_EquipmentStockHistory;
            }
            set
            {
                this._TB_EquipmentStockHistory.Assign(value);
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
            entity.TB_Warehouse = this;
        }

        private void detach_TB_Equipment(TB_Equipment entity)
        {
            this.SendPropertyChanging();
            entity.TB_Warehouse = null;
        }

        private void attach_TB_EquipmentStockHistory(TB_EquipmentStockHistory entity)
        {
            this.SendPropertyChanging();
            entity.TB_Warehouse = this;
        }

        private void detach_TB_EquipmentStockHistory(TB_EquipmentStockHistory entity)
        {
            this.SendPropertyChanging();
            entity.TB_Warehouse = null;
        }
    }
}
