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
    /// 设备类别，如装载机、挖掘机等
    /// </summary>
    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.TB_EquipmentType")]
    public partial class TB_EquipmentType : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _id;

        private string _Code;

        private string _Name;

        private EntitySet<TB_EquipmentModel> _TB_EquipmentModel;

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
        #endregion

        public TB_EquipmentType()
        {
            this._TB_EquipmentModel = new EntitySet<TB_EquipmentModel>(new Action<TB_EquipmentModel>(this.attach_TB_EquipmentModel), new Action<TB_EquipmentModel>(this.detach_TB_EquipmentModel));
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
        /// 设备类别代码，如DL/EX等
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
        /// <summary>
        /// 设备类别名称，如装载机、挖掘机等
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Name", DbType = "VarChar(50)")]
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

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "TB_EquipmentType_TB_EquipmentModel", Storage = "_TB_EquipmentModel", ThisKey = "id", OtherKey = "Type")]
        public EntitySet<TB_EquipmentModel> TB_EquipmentModel
        {
            get
            {
                return this._TB_EquipmentModel;
            }
            set
            {
                this._TB_EquipmentModel.Assign(value);
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

        private void attach_TB_EquipmentModel(TB_EquipmentModel entity)
        {
            this.SendPropertyChanging();
            entity.TB_EquipmentType = this;
        }

        private void detach_TB_EquipmentModel(TB_EquipmentModel entity)
        {
            this.SendPropertyChanging();
            entity.TB_EquipmentType = null;
        }
    }
	
}
