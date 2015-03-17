﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.34209
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Wbs.Everdigm.Database
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Runtime.Serialization;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="GpsSystem")]
	public partial class EverdigmDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region 可扩展性方法定义
    partial void OnCreated();
    partial void InsertCT_00000(CT_00000 instance);
    partial void UpdateCT_00000(CT_00000 instance);
    partial void DeleteCT_00000(CT_00000 instance);
    partial void InsertTB_AccountAction(TB_AccountAction instance);
    partial void UpdateTB_AccountAction(TB_AccountAction instance);
    partial void DeleteTB_AccountAction(TB_AccountAction instance);
    partial void InsertTB_EquipmentStatusCode(TB_EquipmentStatusCode instance);
    partial void UpdateTB_EquipmentStatusCode(TB_EquipmentStatusCode instance);
    partial void DeleteTB_EquipmentStatusCode(TB_EquipmentStatusCode instance);
    partial void InsertTB_AccountHistory(TB_AccountHistory instance);
    partial void UpdateTB_AccountHistory(TB_AccountHistory instance);
    partial void DeleteTB_AccountHistory(TB_AccountHistory instance);
    partial void InsertTB_Permission(TB_Permission instance);
    partial void UpdateTB_Permission(TB_Permission instance);
    partial void DeleteTB_Permission(TB_Permission instance);
    partial void InsertTB_Role(TB_Role instance);
    partial void UpdateTB_Role(TB_Role instance);
    partial void DeleteTB_Role(TB_Role instance);
    partial void InsertTB_Department(TB_Department instance);
    partial void UpdateTB_Department(TB_Department instance);
    partial void DeleteTB_Department(TB_Department instance);
    partial void InsertTB_Account(TB_Account instance);
    partial void UpdateTB_Account(TB_Account instance);
    partial void DeleteTB_Account(TB_Account instance);
    partial void InsertTB_EquipmentStockHistory(TB_EquipmentStockHistory instance);
    partial void UpdateTB_EquipmentStockHistory(TB_EquipmentStockHistory instance);
    partial void DeleteTB_EquipmentStockHistory(TB_EquipmentStockHistory instance);
    partial void InsertTB_EquipmentStatusName(TB_EquipmentStatusName instance);
    partial void UpdateTB_EquipmentStatusName(TB_EquipmentStatusName instance);
    partial void DeleteTB_EquipmentStatusName(TB_EquipmentStatusName instance);
    partial void InsertTB_EquipmentModel(TB_EquipmentModel instance);
    partial void UpdateTB_EquipmentModel(TB_EquipmentModel instance);
    partial void DeleteTB_EquipmentModel(TB_EquipmentModel instance);
    partial void InsertTB_EquipmentType(TB_EquipmentType instance);
    partial void UpdateTB_EquipmentType(TB_EquipmentType instance);
    partial void DeleteTB_EquipmentType(TB_EquipmentType instance);
    partial void InsertTB_Warehouse(TB_Warehouse instance);
    partial void UpdateTB_Warehouse(TB_Warehouse instance);
    partial void DeleteTB_Warehouse(TB_Warehouse instance);
    partial void InsertTB_Alarm(TB_Alarm instance);
    partial void UpdateTB_Alarm(TB_Alarm instance);
    partial void DeleteTB_Alarm(TB_Alarm instance);
    partial void InsertTB_Position(TB_Position instance);
    partial void UpdateTB_Position(TB_Position instance);
    partial void DeleteTB_Position(TB_Position instance);
    partial void InsertTB_Equipment(TB_Equipment instance);
    partial void UpdateTB_Equipment(TB_Equipment instance);
    partial void DeleteTB_Equipment(TB_Equipment instance);
    partial void InsertTB_EposFault(TB_EposFault instance);
    partial void UpdateTB_EposFault(TB_EposFault instance);
    partial void DeleteTB_EposFault(TB_EposFault instance);
    partial void InsertTB_Customer(TB_Customer instance);
    partial void UpdateTB_Customer(TB_Customer instance);
    partial void DeleteTB_Customer(TB_Customer instance);
    partial void InsertTB_Terminal(TB_Terminal instance);
    partial void UpdateTB_Terminal(TB_Terminal instance);
    partial void DeleteTB_Terminal(TB_Terminal instance);
    partial void InsertTB_Satellite(TB_Satellite instance);
    partial void UpdateTB_Satellite(TB_Satellite instance);
    partial void DeleteTB_Satellite(TB_Satellite instance);
    #endregion
		
		public EverdigmDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public EverdigmDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public EverdigmDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public EverdigmDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<CT_00000> CT_00000
		{
			get
			{
				return this.GetTable<CT_00000>();
			}
		}
		
		public System.Data.Linq.Table<TB_AccountAction> TB_AccountAction
		{
			get
			{
				return this.GetTable<TB_AccountAction>();
			}
		}
		
		public System.Data.Linq.Table<TB_EquipmentStatusCode> TB_EquipmentStatusCode
		{
			get
			{
				return this.GetTable<TB_EquipmentStatusCode>();
			}
		}
		
		public System.Data.Linq.Table<TB_AccountHistory> TB_AccountHistory
		{
			get
			{
				return this.GetTable<TB_AccountHistory>();
			}
		}
		
		public System.Data.Linq.Table<TB_Permission> TB_Permission
		{
			get
			{
				return this.GetTable<TB_Permission>();
			}
		}
		
		public System.Data.Linq.Table<TB_Role> TB_Role
		{
			get
			{
				return this.GetTable<TB_Role>();
			}
		}
		
		public System.Data.Linq.Table<TB_Department> TB_Department
		{
			get
			{
				return this.GetTable<TB_Department>();
			}
		}
		
		public System.Data.Linq.Table<TB_Account> TB_Account
		{
			get
			{
				return this.GetTable<TB_Account>();
			}
		}
		
		public System.Data.Linq.Table<TB_EquipmentStockHistory> TB_EquipmentStockHistory
		{
			get
			{
				return this.GetTable<TB_EquipmentStockHistory>();
			}
		}
		
		public System.Data.Linq.Table<TB_EquipmentStatusName> TB_EquipmentStatusName
		{
			get
			{
				return this.GetTable<TB_EquipmentStatusName>();
			}
		}
		
		public System.Data.Linq.Table<TB_EquipmentModel> TB_EquipmentModel
		{
			get
			{
				return this.GetTable<TB_EquipmentModel>();
			}
		}
		
		public System.Data.Linq.Table<TB_EquipmentType> TB_EquipmentType
		{
			get
			{
				return this.GetTable<TB_EquipmentType>();
			}
		}
		
		public System.Data.Linq.Table<TB_Warehouse> TB_Warehouse
		{
			get
			{
				return this.GetTable<TB_Warehouse>();
			}
		}
		
		public System.Data.Linq.Table<TB_HISTORIES> TB_HISTORIES
		{
			get
			{
				return this.GetTable<TB_HISTORIES>();
			}
		}
		
		public System.Data.Linq.Table<TB_Alarm> TB_Alarm
		{
			get
			{
				return this.GetTable<TB_Alarm>();
			}
		}
		
		public System.Data.Linq.Table<TB_Position> TB_Position
		{
			get
			{
				return this.GetTable<TB_Position>();
			}
		}
		
		public System.Data.Linq.Table<TB_Equipment> TB_Equipment
		{
			get
			{
				return this.GetTable<TB_Equipment>();
			}
		}
		
		public System.Data.Linq.Table<TB_EposFault> TB_EposFault
		{
			get
			{
				return this.GetTable<TB_EposFault>();
			}
		}
		
		public System.Data.Linq.Table<TB_Customer> TB_Customer
		{
			get
			{
				return this.GetTable<TB_Customer>();
			}
		}
		
		public System.Data.Linq.Table<TB_Terminal> TB_Terminal
		{
			get
			{
				return this.GetTable<TB_Terminal>();
			}
		}
		
		public System.Data.Linq.Table<TB_Satellite> TB_Satellite
		{
			get
			{
				return this.GetTable<TB_Satellite>();
			}
		}
	}
	
}
#pragma warning restore 1591
