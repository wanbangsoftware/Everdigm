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
    /// 设备信息
    /// </summary>
    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.TB_Equipment")]
    public partial class TB_Equipment : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _id;

        private System.Nullable<int> _Model;

        private System.Nullable<int> _Terminal;

        private System.Nullable<int> _Port;

        private System.Nullable<int> _Socket;

        private System.Nullable<int> _Warehouse;

        private System.Nullable<int> _Customer;

        private System.Nullable<int> _Runtime;

        private System.Nullable<int> _Status;

        private System.Nullable<byte> _Signal;

        private System.Nullable<byte> _OnlineStyle;

        private System.Nullable<bool> _GpsUpdated;

        private System.Nullable<double> _Latitude;

        private System.Nullable<double> _Longitude;

        private System.Nullable<System.DateTime> _RegisterTime;

        private System.Nullable<System.DateTime> _OnlineTime;

        private System.Nullable<System.DateTime> _LastActionTime;

        private string _LockStatus;

        private string _LastActionBy;

        private string _Voltage;

        private string _LastAction;

        private string _Number;

        private string _ServerName;

        private string _IP;

        private string _GpsAddress;

        private EntitySet<TB_EquipmentStockHistory> _TB_EquipmentStockHistory;

        private EntityRef<TB_Customer> _TB_Customer;

        private EntityRef<TB_Terminal> _TB_Terminal;

        private EntityRef<TB_EquipmentModel> _TB_EquipmentModel;

        private EntityRef<TB_EquipmentStatusCode> _TB_EquipmentStatusCode;

        private EntityRef<TB_Warehouse> _TB_Warehouse;

        #region 可扩展性方法定义
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnidChanging(int value);
        partial void OnidChanged();
        partial void OnModelChanging(System.Nullable<int> value);
        partial void OnModelChanged();
        partial void OnTerminalChanging(System.Nullable<int> value);
        partial void OnTerminalChanged();
        partial void OnPortChanging(System.Nullable<int> value);
        partial void OnPortChanged();
        partial void OnSocketChanging(System.Nullable<int> value);
        partial void OnSocketChanged();
        partial void OnWarehouseChanging(System.Nullable<int> value);
        partial void OnWarehouseChanged();
        partial void OnCustomerChanging(System.Nullable<int> value);
        partial void OnCustomerChanged();
        partial void OnRuntimeChanging(System.Nullable<int> value);
        partial void OnRuntimeChanged();
        partial void OnStatusChanging(System.Nullable<int> value);
        partial void OnStatusChanged();
        partial void OnSignalChanging(System.Nullable<byte> value);
        partial void OnSignalChanged();
        partial void OnOnlineStyleChanging(System.Nullable<byte> value);
        partial void OnOnlineStyleChanged();
        partial void OnGpsUpdatedChanging(System.Nullable<bool> value);
        partial void OnGpsUpdatedChanged();
        partial void OnLatitudeChanging(System.Nullable<double> value);
        partial void OnLatitudeChanged();
        partial void OnLongitudeChanging(System.Nullable<double> value);
        partial void OnLongitudeChanged();
        partial void OnRegisterTimeChanging(System.Nullable<System.DateTime> value);
        partial void OnRegisterTimeChanged();
        partial void OnOnlineTimeChanging(System.Nullable<System.DateTime> value);
        partial void OnOnlineTimeChanged();
        partial void OnLastActionTimeChanging(System.Nullable<System.DateTime> value);
        partial void OnLastActionTimeChanged();
        partial void OnLockStatusChanging(string value);
        partial void OnLockStatusChanged();
        partial void OnLastActionByChanging(string value);
        partial void OnLastActionByChanged();
        partial void OnVoltageChanging(string value);
        partial void OnVoltageChanged();
        partial void OnLastActionChanging(string value);
        partial void OnLastActionChanged();
        partial void OnNumberChanging(string value);
        partial void OnNumberChanged();
        partial void OnServerNameChanging(string value);
        partial void OnServerNameChanged();
        partial void OnIPChanging(string value);
        partial void OnIPChanged();
        partial void OnGpsAddressChanging(string value);
        partial void OnGpsAddressChanged();
        #endregion

        public TB_Equipment()
        {
            this._TB_EquipmentStockHistory = new EntitySet<TB_EquipmentStockHistory>(new Action<TB_EquipmentStockHistory>(this.attach_TB_EquipmentStockHistory), new Action<TB_EquipmentStockHistory>(this.detach_TB_EquipmentStockHistory));
            this._TB_Customer = default(EntityRef<TB_Customer>);
            this._TB_Terminal = default(EntityRef<TB_Terminal>);
            this._TB_EquipmentModel = default(EntityRef<TB_EquipmentModel>);
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
        /// 设备型号ID，外键
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Model", DbType = "Int")]
        public System.Nullable<int> Model
        {
            get
            {
                return this._Model;
            }
            set
            {
                if ((this._Model != value))
                {
                    if (this._TB_EquipmentModel.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnModelChanging(value);
                    this.SendPropertyChanging();
                    this._Model = value;
                    this.SendPropertyChanged("Model");
                    this.OnModelChanged();
                }
            }
        }
        /// <summary>
        /// 终端ID，外键
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terminal", DbType = "Int")]
        public System.Nullable<int> Terminal
        {
            get
            {
                return this._Terminal;
            }
            set
            {
                if ((this._Terminal != value))
                {
                    if (this._TB_Terminal.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnTerminalChanging(value);
                    this.SendPropertyChanging();
                    this._Terminal = value;
                    this.SendPropertyChanged("Terminal");
                    this.OnTerminalChanged();
                }
            }
        }
        /// <summary>
        /// 终端连接的端口号码
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Port", DbType = "Int")]
        public System.Nullable<int> Port
        {
            get
            {
                return this._Port;
            }
            set
            {
                if ((this._Port != value))
                {
                    this.OnPortChanging(value);
                    this.SendPropertyChanging();
                    this._Port = value;
                    this.SendPropertyChanged("Port");
                    this.OnPortChanged();
                }
            }
        }
        /// <summary>
        /// 终端连接的socket
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Socket", DbType = "Int")]
        public System.Nullable<int> Socket
        {
            get
            {
                return this._Socket;
            }
            set
            {
                if ((this._Socket != value))
                {
                    this.OnSocketChanging(value);
                    this.SendPropertyChanging();
                    this._Socket = value;
                    this.SendPropertyChanged("Socket");
                    this.OnSocketChanged();
                }
            }
        }
        /// <summary>
        /// 仓库ID，外键
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
        /// 客户信息ID，外键
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Customer", DbType = "Int")]
        public System.Nullable<int> Customer
        {
            get
            {
                return this._Customer;
            }
            set
            {
                if ((this._Customer != value))
                {
                    if (this._TB_Customer.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnCustomerChanging(value);
                    this.SendPropertyChanging();
                    this._Customer = value;
                    this.SendPropertyChanged("Customer");
                    this.OnCustomerChanged();
                }
            }
        }
        /// <summary>
        /// 总运转时间，单位分钟
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Runtime", DbType = "Int")]
        public System.Nullable<int> Runtime
        {
            get
            {
                return this._Runtime;
            }
            set
            {
                if ((this._Runtime != value))
                {
                    this.OnRuntimeChanging(value);
                    this.SendPropertyChanging();
                    this._Runtime = value;
                    this.SendPropertyChanged("Runtime");
                    this.OnRuntimeChanged();
                }
            }
        }
        /// <summary>
        /// 设备状态ID(包括入库、出库、检修等)，外键
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
        /// 信号强度
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Signal", DbType = "TinyInt")]
        public System.Nullable<byte> Signal
        {
            get
            {
                return this._Signal;
            }
            set
            {
                if ((this._Signal != value))
                {
                    this.OnSignalChanging(value);
                    this.SendPropertyChanging();
                    this._Signal = value;
                    this.SendPropertyChanged("Signal");
                    this.OnSignalChanged();
                }
            }
        }
        /// <summary>
        /// 在线状态
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_OnlineStyle", DbType = "TinyInt")]
        public System.Nullable<byte> OnlineStyle
        {
            get
            {
                return this._OnlineStyle;
            }
            set
            {
                if ((this._OnlineStyle != value))
                {
                    this.OnOnlineStyleChanging(value);
                    this.SendPropertyChanging();
                    this._OnlineStyle = value;
                    this.SendPropertyChanged("OnlineStyle");
                    this.OnOnlineStyleChanged();
                }
            }
        }
        /// <summary>
        /// 标记GPS信息是否已更新0=需要更新
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_GpsUpdated", DbType = "Bit")]
        public System.Nullable<bool> GpsUpdated
        {
            get
            {
                return this._GpsUpdated;
            }
            set
            {
                if ((this._GpsUpdated != value))
                {
                    this.OnGpsUpdatedChanging(value);
                    this.SendPropertyChanging();
                    this._GpsUpdated = value;
                    this.SendPropertyChanged("GpsUpdated");
                    this.OnGpsUpdatedChanged();
                }
            }
        }
        /// <summary>
        /// 纬度值
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Latitude", DbType = "Float")]
        public System.Nullable<double> Latitude
        {
            get
            {
                return this._Latitude;
            }
            set
            {
                if ((this._Latitude != value))
                {
                    this.OnLatitudeChanging(value);
                    this.SendPropertyChanging();
                    this._Latitude = value;
                    this.SendPropertyChanged("Latitude");
                    this.OnLatitudeChanged();
                }
            }
        }
        /// <summary>
        /// 经度值
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Longitude", DbType = "Float")]
        public System.Nullable<double> Longitude
        {
            get
            {
                return this._Longitude;
            }
            set
            {
                if ((this._Longitude != value))
                {
                    this.OnLongitudeChanging(value);
                    this.SendPropertyChanging();
                    this._Longitude = value;
                    this.SendPropertyChanged("Longitude");
                    this.OnLongitudeChanged();
                }
            }
        }
        /// <summary>
        /// 设备登记时间
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_RegisterTime", DbType = "DateTime")]
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
        /// 在线刷新时间
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_OnlineTime", DbType = "DateTime")]
        public System.Nullable<System.DateTime> OnlineTime
        {
            get
            {
                return this._OnlineTime;
            }
            set
            {
                if ((this._OnlineTime != value))
                {
                    this.OnOnlineTimeChanging(value);
                    this.SendPropertyChanging();
                    this._OnlineTime = value;
                    this.SendPropertyChanged("OnlineTime");
                    this.OnOnlineTimeChanged();
                }
            }
        }
        /// <summary>
        /// 最近一条信息的接受时间
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LastActionTime", DbType = "DateTime")]
        public System.Nullable<System.DateTime> LastActionTime
        {
            get
            {
                return this._LastActionTime;
            }
            set
            {
                if ((this._LastActionTime != value))
                {
                    this.OnLastActionTimeChanging(value);
                    this.SendPropertyChanging();
                    this._LastActionTime = value;
                    this.SendPropertyChanged("LastActionTime");
                    this.OnLastActionTimeChanged();
                }
            }
        }
        /// <summary>
        /// 保安命令状态(00/10/20/40)，服务器按照此状态来约束下发的保安命令，有些命令在一些状态下是禁止发送的
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LockStatus", DbType = "Char(2)")]
        public string LockStatus
        {
            get
            {
                return this._LockStatus;
            }
            set
            {
                if ((this._LockStatus != value))
                {
                    this.OnLockStatusChanging(value);
                    this.SendPropertyChanging();
                    this._LockStatus = value;
                    this.SendPropertyChanged("LockStatus");
                    this.OnLockStatusChanged();
                }
            }
        }
        /// <summary>
        /// 最近一条信息的发送方式
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LastActionBy", DbType = "Char(3)")]
        public string LastActionBy
        {
            get
            {
                return this._LastActionBy;
            }
            set
            {
                if ((this._LastActionBy != value))
                {
                    this.OnLastActionByChanging(value);
                    this.SendPropertyChanging();
                    this._LastActionBy = value;
                    this.SendPropertyChanged("LastActionBy");
                    this.OnLastActionByChanged();
                }
            }
        }
        /// <summary>
        /// 当前电压状态
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Voltage", DbType = "Char(5)")]
        public string Voltage
        {
            get
            {
                return this._Voltage;
            }
            set
            {
                if ((this._Voltage != value))
                {
                    this.OnVoltageChanging(value);
                    this.SendPropertyChanging();
                    this._Voltage = value;
                    this.SendPropertyChanged("Voltage");
                    this.OnVoltageChanged();
                }
            }
        }
        /// <summary>
        /// 最近一条命令代码
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LastAction", DbType = "Char(6)")]
        public string LastAction
        {
            get
            {
                return this._LastAction;
            }
            set
            {
                if ((this._LastAction != value))
                {
                    this.OnLastActionChanging(value);
                    this.SendPropertyChanging();
                    this._LastAction = value;
                    this.SendPropertyChanged("LastAction");
                    this.OnLastActionChanged();
                }
            }
        }
        /// <summary>
        /// 设备号码
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Number", DbType = "VarChar(10)")]
        public string Number
        {
            get
            {
                return this._Number;
            }
            set
            {
                if ((this._Number != value))
                {
                    this.OnNumberChanging(value);
                    this.SendPropertyChanging();
                    this._Number = value;
                    this.SendPropertyChanged("Number");
                    this.OnNumberChanged();
                }
            }
        }
        /// <summary>
        /// 链接的服务器名
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ServerName", DbType = "VarChar(50)")]
        public string ServerName
        {
            get
            {
                return this._ServerName;
            }
            set
            {
                if ((this._ServerName != value))
                {
                    this.OnServerNameChanging(value);
                    this.SendPropertyChanging();
                    this._ServerName = value;
                    this.SendPropertyChanged("ServerName");
                    this.OnServerNameChanged();
                }
            }
        }
        /// <summary>
        /// 终端连接的IP地址
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IP", DbType = "VarChar(20)")]
        public string IP
        {
            get
            {
                return this._IP;
            }
            set
            {
                if ((this._IP != value))
                {
                    this.OnIPChanging(value);
                    this.SendPropertyChanging();
                    this._IP = value;
                    this.SendPropertyChanged("IP");
                    this.OnIPChanged();
                }
            }
        }
        /// <summary>
        /// 最新地理位置
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_GpsAddress", DbType = "NVarChar(100)")]
        public string GpsAddress
        {
            get
            {
                return this._GpsAddress;
            }
            set
            {
                if ((this._GpsAddress != value))
                {
                    this.OnGpsAddressChanging(value);
                    this.SendPropertyChanging();
                    this._GpsAddress = value;
                    this.SendPropertyChanged("GpsAddress");
                    this.OnGpsAddressChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "TB_Equipment_TB_EquipmentStockHistory", Storage = "_TB_EquipmentStockHistory", ThisKey = "id", OtherKey = "Equipment")]
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

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "TB_Customer_TB_Equipment", Storage = "_TB_Customer", ThisKey = "Customer", OtherKey = "id", IsForeignKey = true, DeleteRule = "SET DEFAULT")]
        public TB_Customer TB_Customer
        {
            get
            {
                return this._TB_Customer.Entity;
            }
            set
            {
                TB_Customer previousValue = this._TB_Customer.Entity;
                if (((previousValue != value)
                            || (this._TB_Customer.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._TB_Customer.Entity = null;
                        previousValue.TB_Equipment.Remove(this);
                    }
                    this._TB_Customer.Entity = value;
                    if ((value != null))
                    {
                        value.TB_Equipment.Add(this);
                        this._Customer = value.id;
                    }
                    else
                    {
                        this._Customer = default(Nullable<int>);
                    }
                    this.SendPropertyChanged("TB_Customer");
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "TB_Terminal_TB_Equipment", Storage = "_TB_Terminal", ThisKey = "Terminal", OtherKey = "id", IsForeignKey = true, DeleteRule = "SET DEFAULT")]
        public TB_Terminal TB_Terminal
        {
            get
            {
                return this._TB_Terminal.Entity;
            }
            set
            {
                TB_Terminal previousValue = this._TB_Terminal.Entity;
                if (((previousValue != value)
                            || (this._TB_Terminal.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._TB_Terminal.Entity = null;
                        previousValue.TB_Equipment.Remove(this);
                    }
                    this._TB_Terminal.Entity = value;
                    if ((value != null))
                    {
                        value.TB_Equipment.Add(this);
                        this._Terminal = value.id;
                    }
                    else
                    {
                        this._Terminal = default(Nullable<int>);
                    }
                    this.SendPropertyChanged("TB_Terminal");
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "TB_EquipmentModel_TB_Equipment", Storage = "_TB_EquipmentModel", ThisKey = "Model", OtherKey = "id", IsForeignKey = true, DeleteRule = "SET DEFAULT")]
        public TB_EquipmentModel TB_EquipmentModel
        {
            get
            {
                return this._TB_EquipmentModel.Entity;
            }
            set
            {
                TB_EquipmentModel previousValue = this._TB_EquipmentModel.Entity;
                if (((previousValue != value)
                            || (this._TB_EquipmentModel.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._TB_EquipmentModel.Entity = null;
                        previousValue.TB_Equipment.Remove(this);
                    }
                    this._TB_EquipmentModel.Entity = value;
                    if ((value != null))
                    {
                        value.TB_Equipment.Add(this);
                        this._Model = value.id;
                    }
                    else
                    {
                        this._Model = default(Nullable<int>);
                    }
                    this.SendPropertyChanged("TB_EquipmentModel");
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "TB_EquipmentStatusCode_TB_Equipment", Storage = "_TB_EquipmentStatusCode", ThisKey = "Status", OtherKey = "id", IsForeignKey = true, DeleteRule = "SET DEFAULT")]
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
                        previousValue.TB_Equipment.Remove(this);
                    }
                    this._TB_EquipmentStatusCode.Entity = value;
                    if ((value != null))
                    {
                        value.TB_Equipment.Add(this);
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

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "TB_Warehouse_TB_Equipment", Storage = "_TB_Warehouse", ThisKey = "Warehouse", OtherKey = "id", IsForeignKey = true, DeleteRule = "SET DEFAULT")]
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
                        previousValue.TB_Equipment.Remove(this);
                    }
                    this._TB_Warehouse.Entity = value;
                    if ((value != null))
                    {
                        value.TB_Equipment.Add(this);
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

        private void attach_TB_EquipmentStockHistory(TB_EquipmentStockHistory entity)
        {
            this.SendPropertyChanging();
            entity.TB_Equipment = this;
        }

        private void detach_TB_EquipmentStockHistory(TB_EquipmentStockHistory entity)
        {
            this.SendPropertyChanging();
            entity.TB_Equipment = null;
        }
    }

}
