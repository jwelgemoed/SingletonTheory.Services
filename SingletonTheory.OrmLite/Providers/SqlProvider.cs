using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using SingletonTheory.OrmLite.Annotations;
using SingletonTheory.OrmLite.Config;
using SingletonTheory.OrmLite.Extensions;
using SingletonTheory.OrmLite.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;

namespace SingletonTheory.OrmLite.Providers
{
	public class SqlProvider : IDatabaseProvider
	{
		#region Fields & Properties

		private bool _dropAndRecreate;
		private IDbConnection _databaseConnection;
		private IDbTransaction _transaction;
		private Dictionary<string, ModelDefinition> _modelDefinitions = new Dictionary<string, ModelDefinition>();

		public bool HasTransactionSupport
		{
			get
			{
				return true;
			}
		}

		#endregion Fields & Properties

		#region Constructors

		public SqlProvider(string connectionString, bool useTransation = false)
		{
			if (string.IsNullOrEmpty(connectionString))
				throw new ArgumentNullException("connectionString");

			OrmLiteConfig.DialectProvider = SqlServerOrmLiteDialectProvider.Instance;

			_databaseConnection = connectionString.OpenDbConnection();
			if (useTransation)
				_transaction = _databaseConnection.OpenTransaction();
		}

		#endregion Constructors

		#region Public Methods

		public bool TableExists(Type modelType)
		{
			ModelDefinition modelDefinition = modelType.GetModelDefinition();

			return _databaseConnection.TableExists(modelDefinition.Alias ?? modelType.Name);
		}

		public void DropAndCreate(Type modelType)
		{
			ModelDefinition modelDefinition = modelType.GetModelDefinition();
			List<Type> associatesToAdd = new List<Type>();
			if (modelDefinition == null)
				return;

			for (int i = 0; i < modelDefinition.AllFieldDefinitionsArray.Length; i++)
			{
				FieldDefinition fieldDefinition = modelDefinition.AllFieldDefinitionsArray[i];
				Type fieldType = fieldDefinition.FieldType;
				if (fieldDefinition.HasAttribute(typeof(AssociatedEntityAttribute)))
				{
					if (fieldType.IsListType())
					{
						fieldType = fieldType.GetGenericType();
						if (fieldType.GetModelDefinition() != null)
							associatesToAdd.Add(fieldType);
					}
				}

				if (fieldDefinition.HasAttribute(typeof(ReferencedEntityAttribute)))
				{
					DropAndCreate(fieldType);
				}
			}

			_databaseConnection.DropAndCreateTable(modelType);

			// Add associates after current type.
			for (int i = 0; i < associatesToAdd.Count; i++)
			{
				DropAndCreate(associatesToAdd[i]);
			}
		}

		public T SelectById<T>(long idValue) where T : IIdentifiable
		{
			try
			{
				ModelDefinition modelDefinition = typeof(T).GetModelDefinition();
				List<Type> associates = new List<Type>();
				if (modelDefinition == null)
					throw new InvalidOperationException("Model Type is not a valid model type.");

				T response = _databaseConnection.GetById<T>(idValue);

				for (int i = 0; i < modelDefinition.AllFieldDefinitionsArray.Length; i++)
				{
					FieldDefinition fieldDefinition = modelDefinition.AllFieldDefinitionsArray[i];
					Type fieldType = fieldDefinition.FieldType;
					if (fieldDefinition.HasAttribute(typeof(AssociatedEntityAttribute)))
					{
						if (fieldType.IsListType())
						{
							SetChildListValue<T>(response, idValue, fieldDefinition.PropertyInfo, fieldType);
						}
						else
						{
							SetChildSingleValue<T>(response, idValue, fieldDefinition.PropertyInfo, fieldType);
						}
					}
				}

				return response;
			}
			catch (ArgumentNullException)
			{
				return default(T);
			}
		}

		private void SetChildListValue<T>(T response, long parentId, PropertyInfo propertyInfo, Type fieldType)
		{
			fieldType = fieldType.GetGenericType();
			if (fieldType.HasInterface(typeof(IChildEntity<>)))
			{
				IEnumerable childList = this.CallGenericSelectMethod("SelectByParentId", fieldType, new object[] { fieldType, parentId });
				if (childList != null)
					propertyInfo.SetValue(response, childList);
			}
		}

		private void SetChildSingleValue<T>(T response, long parentId, PropertyInfo propertyInfo, Type fieldType)
		{
			fieldType = fieldType.GetGenericType();
			if (fieldType.HasInterface(typeof(IChildEntity<>)))
			{
				IEnumerable childList = this.CallGenericSelectMethod("SelectSingleByParentId", fieldType, new object[] { fieldType, parentId });
				if (childList != null)
					propertyInfo.SetValue(response, childList);
			}
		}

		public List<T> Select<T>() where T : IIdentifiable
		{
			try
			{
				ModelDefinition modelDefinition = typeof(T).GetModelDefinition();
				List<Type> associates = new List<Type>();
				if (modelDefinition == null)
					throw new InvalidOperationException("Model Type is not a valid model type.");

				List<T> response = _databaseConnection.Select<T>();
				for (int x = 0; x < response.Count; x++)
				{
					T item = response[x];
					for (int i = 0; i < modelDefinition.AllFieldDefinitionsArray.Length; i++)
					{
						FieldDefinition fieldDefinition = modelDefinition.AllFieldDefinitionsArray[i];
						Type fieldType = fieldDefinition.FieldType;
						if (fieldDefinition.HasAttribute(typeof(AssociatedEntityAttribute)))
						{
							if (fieldType.IsListType())
							{
								SetChildListValue<T>(item, item.Id, fieldDefinition.PropertyInfo, fieldType);
							}
							else
							{
								SetChildSingleValue<T>(item, item.Id, fieldDefinition.PropertyInfo, fieldType);
							}
						}
					}
				}

				return response;
			}
			catch (ArgumentNullException)
			{
				return default(List<T>);
			}
		}

		public List<T> Select<T>(Expression<Func<T, bool>> predicate) where T : IIdentifiable
		{
			try
			{
				ModelDefinition modelDefinition = typeof(T).GetModelDefinition();
				List<Type> associates = new List<Type>();
				if (modelDefinition == null)
					throw new InvalidOperationException("Model Type is not a valid model type.");

				List<T> response = _databaseConnection.Select<T>(predicate);
				for (int x = 0; x < response.Count; x++)
				{
					T item = response[x];
					for (int i = 0; i < modelDefinition.AllFieldDefinitionsArray.Length; i++)
					{
						FieldDefinition fieldDefinition = modelDefinition.AllFieldDefinitionsArray[i];
						Type fieldType = fieldDefinition.FieldType;
						if (fieldDefinition.HasAttribute(typeof(AssociatedEntityAttribute)))
						{
							if (fieldType.IsListType())
							{
								SetChildListValue<T>(item, item.Id, fieldDefinition.PropertyInfo, fieldType);
							}
							else
							{
								SetChildSingleValue<T>(item, item.Id, fieldDefinition.PropertyInfo, fieldType);
							}
						}
					}
				}

				return response;
			}
			catch (ArgumentNullException)
			{
				return default(List<T>);
			}
		}

		private List<T> SelectByParentId<T>(Type fromTableType, long parentId)
		{
			ModelDefinition modelDefinition = typeof(T).GetModelDefinition();

			if (modelDefinition == null)
				throw new InvalidOperationException("Model Type is not a valid model type.");

			List<Type> associates = new List<Type>();
			List<T> response = _databaseConnection.Select<T>(fromTableType, "ParentId = {0}", parentId);

			for (int i = 0; i < modelDefinition.AllFieldDefinitionsArray.Length; i++)
			{
				FieldDefinition fieldDefinition = modelDefinition.AllFieldDefinitionsArray[i];
				Type fieldType = fieldDefinition.FieldType;
				if (fieldDefinition.HasAttribute(typeof(AssociatedEntityAttribute)))
				{
					for (int x = 0; x < response.Count; x++)
					{
						if (fieldType.IsListType())
						{
							SetChildListValue<T>(response[x], parentId, fieldDefinition.PropertyInfo, fieldType);
						}
						else
						{
							SetChildListValue<T>(response[x], parentId, fieldDefinition.PropertyInfo, fieldType);
						}
					}
				}
			}

			return response;
		}

		private T SelectSingleByParentId<T>(Type fromTableType, long parentId)
		{
			List<T> childEntity = SelectByParentId<T>(fromTableType, parentId);
			if (childEntity != null && childEntity.Count != 0)
				return childEntity[0];

			return default(T);
		}

		public T Insert<T>(T objectToInsert) where T : IIdentifiable, new()
		{
			_databaseConnection.Insert(objectToInsert);
			objectToInsert.SetId(_databaseConnection.GetLastInsertId());

			ModelDefinition modelDefinition = typeof(T).GetModelDefinition();
			List<Type> associatesToAdd = new List<Type>();
			if (modelDefinition == null)
				throw new InvalidOperationException("The object being inserted is not a valid Entity Type.");

			for (int i = 0; i < modelDefinition.AllFieldDefinitionsArray.Length; i++)
			{
				FieldDefinition fieldDefinition = modelDefinition.AllFieldDefinitionsArray[i];
				Type fieldType = fieldDefinition.FieldType;
				if (fieldDefinition.HasAttribute(typeof(AssociatedEntityAttribute)))
				{
					if (fieldType.IsListType() && fieldType.IsChildType())
					{
						IEnumerable items = fieldDefinition.PropertyInfo.GetValue(objectToInsert) as IEnumerable;
						if (items != null)
						{
							foreach (var item in items)
							{
								IChildEntity<long> childItem = item as IChildEntity<long>;
								if (childItem != null)
									childItem.ParentId = objectToInsert.Id;

								this.CallGenericMethod("Insert", item.GetType(), new object[] { item });
							}
						}
					}
					else
					{
						object item = fieldDefinition.PropertyInfo.GetValue(objectToInsert);
						this.CallGenericMethod("Insert", item.GetType(), new object[] { item });
					}
				}
			}

			return objectToInsert;
		}

		public void Update<T>(T objectToUpdate) where T : IIdentifiable, new()
		{
			_databaseConnection.Update(objectToUpdate);

			ModelDefinition modelDefinition = typeof(T).GetModelDefinition();
			List<Type> associatesToAdd = new List<Type>();
			if (modelDefinition == null)
				throw new InvalidOperationException("The object being inserted is not a valid Entity Type.");

			for (int i = 0; i < modelDefinition.AllFieldDefinitionsArray.Length; i++)
			{
				FieldDefinition fieldDefinition = modelDefinition.AllFieldDefinitionsArray[i];
				Type fieldType = fieldDefinition.FieldType;
				if (fieldDefinition.HasAttribute(typeof(AssociatedEntityAttribute)))
				{
					if (fieldType.IsListType() && fieldType.IsChildType())
					{
						IEnumerable items = fieldDefinition.PropertyInfo.GetValue(objectToUpdate) as IEnumerable;
						if (items != null)
						{
							foreach (var item in items)
							{
								IChildEntity<long> childItem = item as IChildEntity<long>;
								if (childItem != null)
									childItem.ParentId = objectToUpdate.Id;

								this.CallGenericMethod("Update", item.GetType(), new object[] { item });
							}
						}
					}
					else
					{
						object item = fieldDefinition.PropertyInfo.GetValue(objectToUpdate);
						this.CallGenericMethod("Update", item.GetType(), new object[] { item });
					}
				}
			}
		}

		public void Delete<T>(Expression<Func<T, bool>> where)
		{
			_databaseConnection.Delete<T>(where);
		}

		public void Delete<T>(T objectToDelete) where T : IIdentifiable, new()
		{
			_databaseConnection.Delete<T>(objectToDelete);
		}

		public void DeleteAll<T>()
		{
			_databaseConnection.DeleteAll<T>();
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
