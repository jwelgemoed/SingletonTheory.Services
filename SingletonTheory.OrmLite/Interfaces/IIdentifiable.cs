using ServiceStack.DesignPatterns.Model;

namespace SingletonTheory.OrmLite.Interfaces
{
	public interface IIdentifiable : IHasId<long>
	{
		void SetId(long id);
	}
}
