using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SingletonTheory.OrmLite.Interfaces
{
	public interface IDatabaseProvider : IDisposable
	{
		void DropAndCreate(Type modelType);

		T SelectById<T>(long idValue);
		List<T> Select<T>();
		List<T> Select<T>(string sqlFilter, params object[] filterParams);
		List<T> Select<T>(Type fromTableType, string sqlFilter, params object[] filterParams);

		T Insert<T>(T objectToInsert) where T : IIdentifiable, new();
		void Update<T>(T objectToUpdate) where T : IIdentifiable, new();

		void Delete<T>(params T[] objs) where T : new();
		void Delete<T>(Expression<Func<T, bool>> where);
		void DeleteAll<T>();

		void Rollback();
	}
}