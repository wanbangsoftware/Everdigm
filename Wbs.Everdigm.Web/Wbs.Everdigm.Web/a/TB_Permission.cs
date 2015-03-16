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
    /// 系统页面访问权限
    /// </summary>
    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.TB_Permission")]
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
        /// 是否删除标记
        /// </summary>
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
        /// <summary>
        /// 是否默认页面标记，默认页面所有角色都可以访问
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsDefault", DbType = "Bit")]
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
        /// <summary>
        /// 父级菜单ID
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Parent", DbType = "Int")]
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
        /// <summary>
        /// 显示顺序，由0开始
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_DisplayOrder", DbType = "Int")]
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
        /// <summary>
        /// 菜单项的添加时间
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AddTime", DbType = "DateTime")]
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
        /// <summary>
        /// 菜单项的名称
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
        /// 菜单的URL
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Url", DbType = "VarChar(50)")]
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
        /// <summary>
        /// 菜单前置图片URL
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Image", DbType = "VarChar(50)")]
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
        /// <summary>
        /// 菜单描述
        /// </summary>
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Description", DbType = "NVarChar(50)")]
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
    }
}
