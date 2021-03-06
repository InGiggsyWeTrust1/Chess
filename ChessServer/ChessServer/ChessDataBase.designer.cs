﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ChessServer
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="ChessDB")]
	public partial class ChessDataBaseDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertT_Course(T_Course instance);
    partial void UpdateT_Course(T_Course instance);
    partial void DeleteT_Course(T_Course instance);
    partial void InsertT_User(T_User instance);
    partial void UpdateT_User(T_User instance);
    partial void DeleteT_User(T_User instance);
    partial void InsertT_Game(T_Game instance);
    partial void UpdateT_Game(T_Game instance);
    partial void DeleteT_Game(T_Game instance);
    #endregion
		
		public ChessDataBaseDataContext() : 
				base(global::ChessServer.Properties.Settings.Default.ChessDBConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public ChessDataBaseDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ChessDataBaseDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ChessDataBaseDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ChessDataBaseDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<T_Course> T_Courses
		{
			get
			{
				return this.GetTable<T_Course>();
			}
		}
		
		public System.Data.Linq.Table<T_User> T_Users
		{
			get
			{
				return this.GetTable<T_User>();
			}
		}
		
		public System.Data.Linq.Table<T_Game> T_Games
		{
			get
			{
				return this.GetTable<T_Game>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.T_Course")]
	public partial class T_Course : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private int _Number;
		
		private string _Course;
		
		private int _GameID;
		
		private int _WhoGone;
		
		private EntityRef<T_Game> _T_Game;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnNumberChanging(int value);
    partial void OnNumberChanged();
    partial void OnCourseChanging(string value);
    partial void OnCourseChanged();
    partial void OnGameIDChanging(int value);
    partial void OnGameIDChanged();
    partial void OnWhoGoneChanging(int value);
    partial void OnWhoGoneChanged();
    #endregion
		
		public T_Course()
		{
			this._T_Game = default(EntityRef<T_Game>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Number", DbType="Int NOT NULL")]
		public int Number
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Course", DbType="NVarChar(10) NOT NULL", CanBeNull=false)]
		public string Course
		{
			get
			{
				return this._Course;
			}
			set
			{
				if ((this._Course != value))
				{
					this.OnCourseChanging(value);
					this.SendPropertyChanging();
					this._Course = value;
					this.SendPropertyChanged("Course");
					this.OnCourseChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_GameID", DbType="Int NOT NULL")]
		public int GameID
		{
			get
			{
				return this._GameID;
			}
			set
			{
				if ((this._GameID != value))
				{
					if (this._T_Game.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnGameIDChanging(value);
					this.SendPropertyChanging();
					this._GameID = value;
					this.SendPropertyChanged("GameID");
					this.OnGameIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_WhoGone", DbType="Int NOT NULL")]
		public int WhoGone
		{
			get
			{
				return this._WhoGone;
			}
			set
			{
				if ((this._WhoGone != value))
				{
					this.OnWhoGoneChanging(value);
					this.SendPropertyChanging();
					this._WhoGone = value;
					this.SendPropertyChanged("WhoGone");
					this.OnWhoGoneChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="T_Game_T_Course", Storage="_T_Game", ThisKey="GameID", OtherKey="Id", IsForeignKey=true)]
		public T_Game T_Game
		{
			get
			{
				return this._T_Game.Entity;
			}
			set
			{
				T_Game previousValue = this._T_Game.Entity;
				if (((previousValue != value) 
							|| (this._T_Game.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._T_Game.Entity = null;
						previousValue.T_Courses.Remove(this);
					}
					this._T_Game.Entity = value;
					if ((value != null))
					{
						value.T_Courses.Add(this);
						this._GameID = value.Id;
					}
					else
					{
						this._GameID = default(int);
					}
					this.SendPropertyChanged("T_Game");
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.T_User")]
	public partial class T_User : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private string _Nickname;
		
		private EntitySet<T_Game> _T_Games;
		
		private EntitySet<T_Game> _T_Games1;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnNicknameChanging(string value);
    partial void OnNicknameChanged();
    #endregion
		
		public T_User()
		{
			this._T_Games = new EntitySet<T_Game>(new Action<T_Game>(this.attach_T_Games), new Action<T_Game>(this.detach_T_Games));
			this._T_Games1 = new EntitySet<T_Game>(new Action<T_Game>(this.attach_T_Games1), new Action<T_Game>(this.detach_T_Games1));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Nickname", DbType="NVarChar(20) NOT NULL", CanBeNull=false)]
		public string Nickname
		{
			get
			{
				return this._Nickname;
			}
			set
			{
				if ((this._Nickname != value))
				{
					this.OnNicknameChanging(value);
					this.SendPropertyChanging();
					this._Nickname = value;
					this.SendPropertyChanged("Nickname");
					this.OnNicknameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="T_User_T_Game", Storage="_T_Games", ThisKey="Id", OtherKey="Black")]
		public EntitySet<T_Game> T_Games
		{
			get
			{
				return this._T_Games;
			}
			set
			{
				this._T_Games.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="T_User_T_Game1", Storage="_T_Games1", ThisKey="Id", OtherKey="White")]
		public EntitySet<T_Game> T_Games1
		{
			get
			{
				return this._T_Games1;
			}
			set
			{
				this._T_Games1.Assign(value);
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
		
		private void attach_T_Games(T_Game entity)
		{
			this.SendPropertyChanging();
			entity.T_User = this;
		}
		
		private void detach_T_Games(T_Game entity)
		{
			this.SendPropertyChanging();
			entity.T_User = null;
		}
		
		private void attach_T_Games1(T_Game entity)
		{
			this.SendPropertyChanging();
			entity.T_User1 = this;
		}
		
		private void detach_T_Games1(T_Game entity)
		{
			this.SendPropertyChanging();
			entity.T_User1 = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.T_Game")]
	public partial class T_Game : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private int _White;
		
		private int _Black;
		
		private EntitySet<T_Course> _T_Courses;
		
		private EntityRef<T_User> _T_User;
		
		private EntityRef<T_User> _T_User1;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnWhiteChanging(int value);
    partial void OnWhiteChanged();
    partial void OnBlackChanging(int value);
    partial void OnBlackChanged();
    #endregion
		
		public T_Game()
		{
			this._T_Courses = new EntitySet<T_Course>(new Action<T_Course>(this.attach_T_Courses), new Action<T_Course>(this.detach_T_Courses));
			this._T_User = default(EntityRef<T_User>);
			this._T_User1 = default(EntityRef<T_User>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_White", DbType="Int NOT NULL")]
		public int White
		{
			get
			{
				return this._White;
			}
			set
			{
				if ((this._White != value))
				{
					if (this._T_User1.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnWhiteChanging(value);
					this.SendPropertyChanging();
					this._White = value;
					this.SendPropertyChanged("White");
					this.OnWhiteChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Black", DbType="Int NOT NULL")]
		public int Black
		{
			get
			{
				return this._Black;
			}
			set
			{
				if ((this._Black != value))
				{
					if (this._T_User.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnBlackChanging(value);
					this.SendPropertyChanging();
					this._Black = value;
					this.SendPropertyChanged("Black");
					this.OnBlackChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="T_Game_T_Course", Storage="_T_Courses", ThisKey="Id", OtherKey="GameID")]
		public EntitySet<T_Course> T_Courses
		{
			get
			{
				return this._T_Courses;
			}
			set
			{
				this._T_Courses.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="T_User_T_Game", Storage="_T_User", ThisKey="Black", OtherKey="Id", IsForeignKey=true)]
		public T_User T_User
		{
			get
			{
				return this._T_User.Entity;
			}
			set
			{
				T_User previousValue = this._T_User.Entity;
				if (((previousValue != value) 
							|| (this._T_User.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._T_User.Entity = null;
						previousValue.T_Games.Remove(this);
					}
					this._T_User.Entity = value;
					if ((value != null))
					{
						value.T_Games.Add(this);
						this._Black = value.Id;
					}
					else
					{
						this._Black = default(int);
					}
					this.SendPropertyChanged("T_User");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="T_User_T_Game1", Storage="_T_User1", ThisKey="White", OtherKey="Id", IsForeignKey=true)]
		public T_User T_User1
		{
			get
			{
				return this._T_User1.Entity;
			}
			set
			{
				T_User previousValue = this._T_User1.Entity;
				if (((previousValue != value) 
							|| (this._T_User1.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._T_User1.Entity = null;
						previousValue.T_Games1.Remove(this);
					}
					this._T_User1.Entity = value;
					if ((value != null))
					{
						value.T_Games1.Add(this);
						this._White = value.Id;
					}
					else
					{
						this._White = default(int);
					}
					this.SendPropertyChanged("T_User1");
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
		
		private void attach_T_Courses(T_Course entity)
		{
			this.SendPropertyChanging();
			entity.T_Game = this;
		}
		
		private void detach_T_Courses(T_Course entity)
		{
			this.SendPropertyChanging();
			entity.T_Game = null;
		}
	}
}
#pragma warning restore 1591
