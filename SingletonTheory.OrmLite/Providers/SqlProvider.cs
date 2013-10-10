using ServiceStack.OrmLite;
using SingletonTheory.OrmLite.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace SingletonTheory.OrmLite.Providers
{
	public class SqlProvider : IDatabaseProvider, IDisposable
	{
		#region Fields & Properties

		private bool _dropAndRecreate;
		private IDbConnection _databaseConnection;
		private IDbTransaction _transaction;

		#endregion Fields & Properties

		#region Constructors

		public SqlProvider(string connectionString, bool useTransation = false, bool dropAndRecreate = false)
		{
			_dropAndRecreate = dropAndRecreate;
			_databaseConnection = connectionString.OpenDbConnection();
			if (useTransation)
				_transaction = _databaseConnection.OpenTransaction();
		}

		static SqlProvider()
		{
			OrmLiteConfig.DialectProvider = ServiceStack.OrmLite.SqlServer.SqlServerOrmLiteDialectProvider.Instance;
		}

		#endregion Constructors

		#region Public Methods

		public T GetById<T>(object idValue) where T : new()
		{
			try
			{
				return _databaseConnection.GetById<T>(idValue);
			}
			catch (ArgumentNullException)
			{
				return default(T);
			}
		}

		public void DropAndCreate<T>() where T : new()
		{
			_databaseConnection.DropAndCreateTable<T>();
		}

		public void Delete<T>(Expression<Func<T, bool>> where)
		{
			_databaseConnection.Delete<T>(where);
		}

		public void Delete<T>(params T[] objs) where T : new()
		{
			_databaseConnection.Delete<T>(objs);
		}

		public void DeleteAll<T>()
		{
			_databaseConnection.DeleteAll<T>();
		}

		public long Insert<T>(params T[] objectsToInsert) where T : new()
		{
			_databaseConnection.Insert(objectsToInsert);

			return _databaseConnection.GetLastInsertId();
		}

		public void Update<T>(params T[] objs) where T : new()
		{
			_databaseConnection.Update(objs);
		}

		public List<T> Select<T>()
		{
			return _databaseConnection.Select<T>();
		}

		public List<T> Select<T>(string sqlFilter, params object[] filterParams)
		{
			return _databaseConnection.Select<T>(sqlFilter, filterParams);
		}

		public List<TModel> Select<TModel>(Type fromTableType, string sqlFilter, params object[] filterParams)
		{
			return _databaseConnection.Select<TModel>(fromTableType, "ShipperTypeId = {0}", filterParams);
		}

		public T Single<T>(string filter, params object[] filterParams)
		{
			return _databaseConnection.Single<T>(filter, filterParams);
		}

		public void Rollback()
		{
			if (_transaction == null || _transaction.Connection == null)
				throw new InvalidOperationException("Transaction not started or closed.");

			_transaction.Rollback();
		}

		#endregion Public Methods

		#region IDisposable Members

		public void Dispose()
		{
			if (_transaction != null && _transaction.Connection != null)
			{
				_transaction.Commit();
				_transaction.Dispose();
				_transaction = null;
			}

			if (_databaseConnection != null)
			{
				_databaseConnection.Dispose();
				_databaseConnection = null;
			}
		}

		#endregion IDisposable Members
	}
}
