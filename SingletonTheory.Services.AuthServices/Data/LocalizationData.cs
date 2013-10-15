using SingletonTheory.Library.IO;
using SingletonTheory.Services.AuthServices.Entities;
using SingletonTheory.Services.AuthServices.Repositories;
using System.IO;

namespace SingletonTheory.Services.AuthServices.Data
{
	public static class LocalizationData
	{
		#region Public Methods

		public static void CreateLanguageFiles(LocalizationRepository repository, string filePath)
		{
			FileUtilities.AssertDirectoryExists(filePath);

			repository.ClearCollection();
			string[] fileNames = Directory.GetFiles(filePath);

			for (int i = 0; i < fileNames.Length; i++)
			{
				FileUtilities.AssertFileExists(fileNames[i]);
				LocalizationCollectionEntity locale = SerializationUtilities.ReadFile<LocalizationCollectionEntity>(fileNames[i]);

				repository.CreateFromStatic(locale);
			}
		}

		#endregion Public Methods
	}
}