using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SingletonTheory.OrmLite.Interfaces
{
	public interface IDatabaseProvider : IDisposable
	{
		bool HasTransactionSupport { get; }

		void CreateCollection(Type modelType);
		void DropAndCreate(Type modelType);
		bool CollectionExists(Type modelType);

		T SelectById<T>(long idValue) where T : IIdentifiable;
		List<T> Select<T>() where T : IIdentifiable;
		List<T> Select<T>(Expression<Func<T, bool>> predicate) where T : IIdentifiable;

		T Insert<T>(T objectToInsert) where T : IIdentifiable, new();
		void Update<T>(T objectToUpdate) where T : IIdentifiable, new();

		void Delete<T>(T objectToDelete) where T : IIdentifiable, new();
		void Delete<T>(Expression<Func<T, bool>> where) where T : IIdentifiable;
		void DeleteAll<T>();

		void Rollback();
	}
}