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
    /// 设备出入库历史记录
    /// </summary>
    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.TB_EquipmentStockHistory")]
    public partial class TB_EquipmentStockHistory : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _id;

        private System.Nullable<int> _Equipment;

        private System.Nullable<int> _Status;

        private System.Nullable<int> _Warehouse;

        private System.Nullable<System.DateTime> _Stocktime;

        private string _StockNumber;

        private EntityRef<TB_Equipment> _TB_Equipment;

        private EntityRef<TB_EquipmentStatusCode> _TB_EquipmentStatusCode;

        private EntityRef<TB_Warehouse> _TB_Warehouse;

        #region 可扩展性方法定义
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnidChanging(int value);
        partial void OnidChanged();
        partial void OnEquipmentChanging(System.Nullable<int> value);
        partial void OnEquipmentChanged();
        partial void OnStatusChanging(System.Nullable<int> value);
        partial void OnStatusChanged();
        partial void OnWarehouseChanging(System.Nullable<int> value);
        partial void OnWarehouseChanged();
        partial void OnStocktimeChanging(System.Nullable<System.DateTime> value);
        partial void OnStocktimeChanged();
        partial void OnStockNumberChanging(string value);
        partial void OnStockNumberChanged();
        #endregion

        public TB_EquipmentStockHistory()
        {
            this._TB_Equipment = default(EntityRef<TB_Equipment>);
            this._TB_EquipmentStatusCode = default(EntityRef<TB_EquipmentStatusCode>);
            this._TB_Warehouse = default(EntityRef<TB_Warehouse>);
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
        /// 设备ID
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Equipment", DbType = "Int")]
        public System.Nullable<int> Equipment
        {
            get
            {
                return this._Equipment;
            }
            set
            {
                if ((this._Equipment != value))
                {
                    if (this._TB_Equipment.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnEquipmentChanging(value);
                    this.SendPropertyChanging();
                    this._Equipment = value;
                    this.SendPropertyChanged("Equipment");
                    this.OnEquipmentChanged();
                }
            }
        }
        /// <summary>
        /// 出入库状态码ID
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Status", DbType = "Int")]
        public System.Nullable<int> Status
        {
            get
            {
                return this._Status;
            }
            set
            {
                if ((this._Status != value))
                {
                    if (this._TB_EquipmentStatusCode.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnStatusChanging(value);
                    this.SendPropertyChanging();
                    this._Status = value;
                    this.SendPropertyChanged("Status");
                    this.OnStatusChanged();
                }
            }
        }
        /// <summary>
        /// 出入库仓库ID
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Warehouse", DbType = "Int")]
        public System.Nullable<int> Warehouse
        {
            get
            {
                return this._Warehouse;
            }
            set
            {
                if ((this._Warehouse != value))
                {
                    if (this._TB_Warehouse.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnWarehouseChanging(value);
                    this.SendPropertyChanging();
                    this._Warehouse = value;
                    this.SendPropertyChanged("Warehouse");
                    this.OnWarehouseChanged();
                }
            }
        }
        /// <summary>
        /// 出入库时间
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Stocktime", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Stocktime
        {
            get
            {
                return this._Stocktime;
            }
            set
            {
                if ((this._Stocktime != value))
                {
                    this.OnStocktimeChanging(value);
                    this.SendPropertyChanging();
                    this._Stocktime = value;
                    this.SendPropertyChanged("Stocktime");
                    this.OnStocktimeChanged();
                }
            }
        }
        /// <summary>
        /// 出入库号码
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_StockNumber", DbType = "VarChar(50)")]
        public string StockNumber
        {
            get
            {
                return this._StockNumber;
            }
            set
            {
                if ((this._StockNumber != value))
                {
                    this.OnStockNumberChanging(value);
                    this.SendPropertyChanging();
                    this._StockNumber = value;
                    this.SendPropertyChanged("StockNumber");
                    this.OnStockNumberChanged();
                }
            }
        }
        /// <summary>
        /// 设备基本信息
        /// </summary>
        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "TB_Equipment_TB_EquipmentStockHistory", Storage = "_TB_Equipment", ThisKey = "Equipment", OtherKey = "id", IsForeignKey = true)]
        public TB_Equipment TB_Equipment
        {
            get
            {
                return this._TB_Equipment.Entity;
            }
            set
            {
                TB_Equipment previousValue = this._TB_Equipment.Entity;
                if (((previousValue != value)
                            || (this._TB_Equipment.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._TB_Equipment.Entity = null;
                        previousValue.TB_EquipmentStockHistory.Remove(this);
                    }
                    this._TB_Equipment.Entity = value;
                    if ((value != null))
                    {
                        value.TB_EquipmentStockHistory.Add(this);
                        this._Equipment = value.id;
                    }
                    else
                    {
                        this._Equipment = default(Nullable<int>);
                    }
                    this.SendPropertyChanged("TB_Equipment");
                }
            }
        }
        /// <summary>
        /// 出入库状态码信息
        /// </summary>
        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "TB_EquipmentStatusCode_TB_EquipmentStockHistory", Storage = "_TB_EquipmentStatusCode", ThisKey = "Status", OtherKey = "id", IsForeignKey = true)]
        public TB_EquipmentStatusCode TB_EquipmentStatusCode
        {
            get
            {
                return this._TB_EquipmentStatusCode.Entity;
            }
            set
            {
                TB_EquipmentStatusCode previousValue = this._TB_EquipmentStatusCode.Entity;
                if (((previousValue != value)
                            || (this._TB_EquipmentStatusCode.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._TB_EquipmentStatusCode.Entity = null;
                        previousValue.TB_EquipmentStockHistory.Remove(this);
                    }
                    this._TB_EquipmentStatusCode.Entity = value;
                    if ((value != null))
                    {
                        value.TB_EquipmentStockHistory.Add(this);
                        this._Status = value.id;
                    }
                    else
                    {
                        this._Status = default(Nullable<int>);
                    }
                    this.SendPropertyChanged("TB_EquipmentStatusCode");
                }
            }
        }
        /// <summary>
        /// 出入库仓库信息
        /// </summary>
        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "TB_Warehouse_TB_EquipmentStockHistory", Storage = "_TB_Warehouse", ThisKey = "Warehouse", OtherKey = "id", IsForeignKey = true)]
        public TB_Warehouse TB_Warehouse
        {
            get
            {
                return this._TB_Warehouse.Entity;
            }
            set
            {
                TB_Warehouse previousValue = this._TB_Warehouse.Entity;
                if (((previousValue != value)
                            || (this._TB_Warehouse.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._TB_Warehouse.Entity = null;
                        previousValue.TB_EquipmentStockHistory.Remove(this);
                    }
                    this._TB_Warehouse.Entity = value;
                    if ((value != null))
                    {
                        value.TB_EquipmentStockHistory.Add(this);
                        this._Warehouse = value.id;
                    }
                    else
                    {
                        this._Warehouse = default(Nullable<int>);
                    }
                    this.SendPropertyChanged("TB_Warehouse");
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
    }
	
}
