using MongoDB.Driver;
using MongoDB.Driver.Builders;
using SingletonTheory.OrmLite.Interfaces;
using System;

namespace SingletonTheory.OrmLite.Providers
{
	public class MongoProvider : IDatabaseProvider
	{
		#region Fields & Properties

		private MongoDatabase _mongoDatabase;

		#endregion Fields & Properties

		#region IDatabaseProvider Members

		public void DropAndCreate(Type modelType)
		{
			_mongoDatabase.DropCollection(modelType.Name);
		}

		public IHasId SelectById<T>(object idValue) where T : IHasId
		{
			MongoCollection<T> locales = _mongoDatabase.GetCollection<T>(typeof(T).Name);
			IMongoQuery localeQuery = Query<T>.EQ(e => e.Id, idValue);
			IHasId collection = locales.FindOne(localeQuery);

			return collection;
		}

		public System.Collections.Generic.List<T> Select<T>()
		{
			throw new System.NotImplementedException();
		}

		public System.Collections.Generic.List<T> Select<T>(string sqlFilter, params object[] filterParams)
		{
			throw new System.NotImplementedException();
		}

		public System.Collections.Generic.List<T> Select<T>(System.Type fromTableType, string sqlFilter, params object[] filterParams)
		{
			throw new System.NotImplementedException();
		}

		public T Single<T>(string filter, params object[] filterParams)
		{
			throw new System.NotImplementedException();
		}

		public long Insert<T>(params T[] objectsToInsert) where T : new()
		{
			throw new System.NotImplementedException();
		}

		public void Update<T>(params T[] objs) where T : new()
		{
			throw new System.NotImplementedException();
		}

		public void Delete<T>(params T[] objs) where T : new()
		{
			throw new System.NotImplementedException();
		}

		public void Delete<T>(System.Linq.Expressions.Expression<System.Func<T, bool>> where)
		{
			throw new System.NotImplementedException();
		}

		public void DeleteAll<T>()
		{
			throw new System.NotImplementedException();
		}

		public void Rollback()
		{
			throw new System.NotImplementedException();
		}

		#endregion IDatabaseProvider Members

		#region IDisposable Members

		public void Dispose()
		{
			throw new System.NotImplementedException();
		}

		#endregion IDisposable Members
	}
}
