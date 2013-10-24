using SingletonTheory.OrmLite.Interfaces;
using SingletonTheory.OrmLite.Tests.Data;

namespace SingletonTheory.OrmLite.Tests.Repositories
{
	public class ShippersRepositorySample
	{
		public void DropAndCreate(IDatabaseProvider provider)
		{
			provider.DropAndCreate(typeof(Shipper));
		}

		public T CreateInstance<T>(IDatabaseProvider provider) where T : new()
		{
			return new T();
		}
	}
}
