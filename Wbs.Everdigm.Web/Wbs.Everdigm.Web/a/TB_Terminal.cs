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
    /// 终端信息
    /// </summary>
    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.TB_Terminal")]
    public partial class TB_Terminal : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _id;

        private System.Nullable<bool> _Delete;

        private System.Nullable<bool> _HasBound;

        private System.Nullable<byte> _Revision;

        private System.Nullable<System.DateTime> _ProductionDate;

        private string _Firmware;

        private string _Number;

        private string _Type;

        private string _Sim;

        private string _Satellite;

        private EntitySet<TB_Equipment> _TB_Equipment;

        #region 可扩展性方法定义
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnidChanging(int value);
        partial void OnidChanged();
        partial void OnDeleteChanging(System.Nullable<bool> value);
        partial void OnDeleteChanged();
        partial void OnHasBoundChanging(System.Nullable<bool> value);
        partial void OnHasBoundChanged();
        partial void OnRevisionChanging(System.Nullable<byte> value);
        partial void OnRevisionChanged();
        partial void OnProductionDateChanging(System.Nullable<System.DateTime> value);
        partial void OnProductionDateChanged();
        partial void OnFirmwareChanging(string value);
        partial void OnFirmwareChanged();
        partial void OnNumberChanging(string value);
        partial void OnNumberChanged();
        partial void OnTypeChanging(string value);
        partial void OnTypeChanged();
        partial void OnSimChanging(string value);
        partial void OnSimChanged();
        partial void OnSatelliteChanging(string value);
        partial void OnSatelliteChanged();
        #endregion

        public TB_Terminal()
        {
            this._TB_Equipment = new EntitySet<TB_Equipment>(new Action<TB_Equipment>(this.attach_TB_Equipment), new Action<TB_Equipment>(this.detach_TB_Equipment));
            OnCreated();
        }

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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "[Delete]", Storage = "_Delete", DbType = "Bit")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_HasBound", DbType = "Bit")]
        public System.Nullable<bool> HasBound
        {
            get
            {
                return this._HasBound;
            }
            set
            {
                if ((this._HasBound != value))
                {
                    this.OnHasBoundChanging(value);
                    this.SendPropertyChanging();
                    this._HasBound = value;
                    this.SendPropertyChanged("HasBound");
                    this.OnHasBoundChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Revision", DbType = "TinyInt")]
        public System.Nullable<byte> Revision
        {
            get
            {
                return this._Revision;
            }
            set
            {
                if ((this._Revision != value))
                {
                    this.OnRevisionChanging(value);
                    this.SendPropertyChanging();
                    this._Revision = value;
                    this.SendPropertyChanged("Revision");
                    this.OnRevisionChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ProductionDate", DbType = "DateTime")]
        public System.Nullable<System.DateTime> ProductionDate
        {
            get
            {
                return this._ProductionDate;
            }
            set
            {
                if ((this._ProductionDate != value))
                {
                    this.OnProductionDateChanging(value);
                    this.SendPropertyChanging();
                    this._ProductionDate = value;
                    this.SendPropertyChanged("ProductionDate");
                    this.OnProductionDateChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Firmware", DbType = "VarChar(7)")]
        public string Firmware
        {
            get
            {
                return this._Firmware;
            }
            set
            {
                if ((this._Firmware != value))
                {
                    this.OnFirmwareChanging(value);
                    this.SendPropertyChanging();
                    this._Firmware = value;
                    this.SendPropertyChanged("Firmware");
                    this.OnFirmwareChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Number", DbType = "Char(10)")]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Type", DbType = "VarChar(10)")]
        public string Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                if ((this._Type != value))
                {
                    this.OnTypeChanging(value);
                    this.SendPropertyChanging();
                    this._Type = value;
                    this.SendPropertyChanged("Type");
                    this.OnTypeChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Sim", DbType = "VarChar(11)")]
        public string Sim
        {
            get
            {
                return this._Sim;
            }
            set
            {
                if ((this._Sim != value))
                {
                    this.OnSimChanging(value);
                    this.SendPropertyChanging();
                    this._Sim = value;
                    this.SendPropertyChanged("Sim");
                    this.OnSimChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Satellite", DbType = "VarChar(20)")]
        public string Satellite
        {
            get
            {
                return this._Satellite;
            }
            set
            {
                if ((this._Satellite != value))
                {
                    this.OnSatelliteChanging(value);
                    this.SendPropertyChanging();
                    this._Satellite = value;
                    this.SendPropertyChanged("Satellite");
                    this.OnSatelliteChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "TB_Terminal_TB_Equipment", Storage = "_TB_Equipment", ThisKey = "id", OtherKey = "Terminal")]
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
            entity.TB_Terminal = this;
        }

        private void detach_TB_Equipment(TB_Equipment entity)
        {
            this.SendPropertyChanging();
            entity.TB_Terminal = null;
        }
    }
}
