using SingletonTheory.Library.IO;

namespace SingletonTheory.Services.AuthServices.Repositories
{
	public class GenericFileRepository
	{
		#region Fields & Properties

		private string _filePath;

		#endregion Fields & Properties

		#region Constructors

		public GenericFileRepository(string filePath)
		{
			_filePath = filePath;
		}

		#endregion Constructors

		#region Public Properties

		public T Create<T>(T obj)
		{
			T item = SerializationUtilities.ReadFile<T>(_filePath);

			return item;
		}

		#endregion Public Properties
	}
}