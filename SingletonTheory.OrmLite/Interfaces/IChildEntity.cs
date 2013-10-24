namespace SingletonTheory.OrmLite.Interfaces
{
	public interface IChildEntity<T> : IIdentifiable
	{
		T ParentId { get; set; }
	}
}
