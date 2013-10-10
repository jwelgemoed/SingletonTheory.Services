using SingletonTheory.OrmLite.Interfaces;

namespace SingletonTheory.OrmLite.Providers
{
	public class MongoProvider : IDatabaseProvider
	{
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

		public void DropAndCreate<T>() where T : new()
		{
			throw new System.NotImplementedException();
		}

		public T GetById<T>(object idValue) where T : new()
		{
			throw new System.NotImplementedException();
		}

		public long Insert<T>(params T[] objectsToInsert) where T : new()
		{
			throw new System.NotImplementedException();
		}

		public void Rollback()
		{
			throw new System.NotImplementedException();
		}

		public System.Collections.Generic.List<T> Select<T>()
		{
			throw new System.NotImplementedException();
		}

		public System.Collections.Generic.List<T> Select<T>(string sqlFilter, params object[] filterParams)
		{
			throw new System.NotImplementedException();
		}

		public System.Collections.Generic.List<TModel> Select<TModel>(System.Type fromTableType, string sqlFilter, params object[] filterParams)
		{
			throw new System.NotImplementedException();
		}

		public T Single<T>(string filter, params object[] filterParams)
		{
			throw new System.NotImplementedException();
		}

		public void Update<T>(params T[] objs) where T : new()
		{
			throw new System.NotImplementedException();
		}
	}
}
