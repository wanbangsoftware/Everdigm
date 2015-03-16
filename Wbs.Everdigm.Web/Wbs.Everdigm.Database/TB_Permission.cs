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

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.TB_Permission")]
    [global::System.Runtime.Serialization.DataContractAttribute()]
    public partial class TB_Permission : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _id;

        private System.Nullable<bool> _Delete;

        private System.Nullable<bool> _IsDefault;

        private System.Nullable<int> _Parent;

        private System.Nullable<int> _DisplayOrder;

        private System.Nullable<System.DateTime> _AddTime;

        private string _Name;

        private string _Url;

        private string _Image;

        private string _Description;

        #region 可扩展性方法定义
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnidChanging(int value);
        partial void OnidChanged();
        partial void OnDeleteChanging(System.Nullable<bool> value);
        partial void OnDeleteChanged();
        partial void OnIsDefaultChanging(System.Nullable<bool> value);
        partial void OnIsDefaultChanged();
        partial void OnParentChanging(System.Nullable<int> value);
        partial void OnParentChanged();
        partial void OnDisplayOrderChanging(System.Nullable<int> value);
        partial void OnDisplayOrderChanged();
        partial void OnAddTimeChanging(System.Nullable<System.DateTime> value);
        partial void OnAddTimeChanged();
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        partial void OnUrlChanging(string value);
        partial void OnUrlChanged();
        partial void OnImageChanging(string value);
        partial void OnImageChanged();
        partial void OnDescriptionChanging(string value);
        partial void OnDescriptionChanged();
        #endregion

        public TB_Permission()
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsDefault", DbType = "Bit")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 3)]
        public System.Nullable<bool> IsDefault
        {
            get
            {
                return this._IsDefault;
            }
            set
            {
                if ((this._IsDefault != value))
                {
                    this.OnIsDefaultChanging(value);
                    this.SendPropertyChanging();
                    this._IsDefault = value;
                    this.SendPropertyChanged("IsDefault");
                    this.OnIsDefaultChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Parent", DbType = "Int")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 4)]
        public System.Nullable<int> Parent
        {
            get
            {
                return this._Parent;
            }
            set
            {
                if ((this._Parent != value))
                {
                    this.OnParentChanging(value);
                    this.SendPropertyChanging();
                    this._Parent = value;
                    this.SendPropertyChanged("Parent");
                    this.OnParentChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_DisplayOrder", DbType = "Int")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 5)]
        public System.Nullable<int> DisplayOrder
        {
            get
            {
                return this._DisplayOrder;
            }
            set
            {
                if ((this._DisplayOrder != value))
                {
                    this.OnDisplayOrderChanging(value);
                    this.SendPropertyChanging();
                    this._DisplayOrder = value;
                    this.SendPropertyChanged("DisplayOrder");
                    this.OnDisplayOrderChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AddTime", DbType = "DateTime")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 6)]
        public System.Nullable<System.DateTime> AddTime
        {
            get
            {
                return this._AddTime;
            }
            set
            {
                if ((this._AddTime != value))
                {
                    this.OnAddTimeChanging(value);
                    this.SendPropertyChanging();
                    this._AddTime = value;
                    this.SendPropertyChanged("AddTime");
                    this.OnAddTimeChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Name", DbType = "NVarChar(50)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 7)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Url", DbType = "VarChar(50)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 8)]
        public string Url
        {
            get
            {
                return this._Url;
            }
            set
            {
                if ((this._Url != value))
                {
                    this.OnUrlChanging(value);
                    this.SendPropertyChanging();
                    this._Url = value;
                    this.SendPropertyChanged("Url");
                    this.OnUrlChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Image", DbType = "VarChar(50)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 9)]
        public string Image
        {
            get
            {
                return this._Image;
            }
            set
            {
                if ((this._Image != value))
                {
                    this.OnImageChanging(value);
                    this.SendPropertyChanging();
                    this._Image = value;
                    this.SendPropertyChanged("Image");
                    this.OnImageChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Description", DbType = "NVarChar(50)")]
        [global::System.Runtime.Serialization.DataMemberAttribute(Order = 10)]
        public string Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                if ((this._Description != value))
                {
                    this.OnDescriptionChanging(value);
                    this.SendPropertyChanging();
                    this._Description = value;
                    this.SendPropertyChanged("Description");
                    this.OnDescriptionChanged();
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

        private void Initialize()
        {
            OnCreated();
        }

        [global::System.Runtime.Serialization.OnDeserializingAttribute()]
        [global::System.ComponentModel.EditorBrowsableAttribute(EditorBrowsableState.Never)]
        public void OnDeserializing(StreamingContext context)
        {
            this.Initialize();
        }
    }
	
}
