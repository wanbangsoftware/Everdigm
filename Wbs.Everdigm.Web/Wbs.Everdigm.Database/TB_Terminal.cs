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

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.TB_Terminal")]
    [global::System.Runtime.Serialization.DataContractAttribute()]
    public partial class TB_Terminal : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _id;

        private System.Nullable<bool> _Delete;

        private System.Nullable<bool> _HasBound;

        private System.Nullable<byte> _Revision;

        private System.Nullable<int> _Satellite;

        private System.Nullable<System.DateTime> _ProductionDate;

        private string _Firmware;

        private string _Number;

        private string _Type;

        private string _Sim;

        private EntitySet<TB_Equipment> _TB_Equipment;

        private EntityRef<TB_Satellite> _TB_Satellite;

        private bool serializing;

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
        partial void OnSatelliteChanging(System.Nullable<int> value);
        partial void OnSatelliteChanged();
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
        #endregion

        public TB_Terminal()
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_HasBound", DbType = "Bit")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 3)]
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
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 4)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Satellite", DbType = "Int")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 5)]
        public System.Nullable<int> Satellite
        {
            get
            {
                return this._Satellite;
            }
            set
            {
                if ((this._Satellite != value))
                {
                    if (this._TB_Satellite.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnSatelliteChanging(value);
                    this.SendPropertyChanging();
                    this._Satellite = value;
                    this.SendPropertyChanged("Satellite");
                    this.OnSatelliteChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ProductionDate", DbType = "DateTime")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 6)]
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
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 7)]
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
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 8)]
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
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 9)]
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
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 10)]
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

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "TB_Terminal_TB_Equipment", Storage = "_TB_Equipment", ThisKey = "id", OtherKey = "Terminal")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 11, EmitDefaultValue = false)]
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

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "TB_Satellite_TB_Terminal", Storage = "_TB_Satellite", ThisKey = "Satellite", OtherKey = "id", IsForeignKey = true)]
        public TB_Satellite TB_Satellite
        {
            get
            {
                return this._TB_Satellite.Entity;
            }
            set
            {
                TB_Satellite previousValue = this._TB_Satellite.Entity;
                if (((previousValue != value)
                            || (this._TB_Satellite.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._TB_Satellite.Entity = null;
                        previousValue.TB_Terminal.Remove(this);
                    }
                    this._TB_Satellite.Entity = value;
                    if ((value != null))
                    {
                        value.TB_Terminal.Add(this);
                        this._Satellite = value.id;
                    }
                    else
                    {
                        this._Satellite = default(Nullable<int>);
                    }
                    this.SendPropertyChanged("TB_Satellite");
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

        private void Initialize()
        {
            this._TB_Equipment = new EntitySet<TB_Equipment>(new Action<TB_Equipment>(this.attach_TB_Equipment), new Action<TB_Equipment>(this.detach_TB_Equipment));
            this._TB_Satellite = default(EntityRef<TB_Satellite>);
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
