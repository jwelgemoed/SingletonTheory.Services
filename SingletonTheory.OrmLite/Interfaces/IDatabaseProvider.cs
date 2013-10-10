using System;

namespace SingletonTheory.OrmLite.Interfaces
{
	public interface IDatabaseProvider
	{
		void Delete<T>(params T[] objs) where T : new();
		void Delete<T>(System.Linq.Expressions.Expression<Func<T, bool>> where);
		void DeleteAll<T>();
		void DropAndCreate<T>() where T : new();
		T GetById<T>(object idValue) where T : new();
		long Insert<T>(params T[] objectsToInsert) where T : new();
		void Rollback();
		System.Collections.Generic.List<T> Select<T>();
		System.Collections.Generic.List<T> Select<T>(string sqlFilter, params object[] filterParams);
		System.Collections.Generic.List<TModel> Select<TModel>(Type fromTableType, string sqlFilter, params object[] filterParams);
		T Single<T>(string filter, params object[] filterParams);
		void Update<T>(params T[] objs) where T : new();
	}
}
