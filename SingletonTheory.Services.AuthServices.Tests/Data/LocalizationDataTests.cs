using NUnit.Framework;
using SingletonTheory.Services.AuthServices.Config;
using SingletonTheory.Services.AuthServices.Data;
using SingletonTheory.Services.AuthServices.Repositories;
using SingletonTheory.Services.AuthServices.Tests.Helpers;

namespace SingletonTheory.Services.AuthServices.Tests.Data
{
	[TestFixture]
	public class LocalizationDataTests
	{
		#region Test Methods

		[Test]
		public void ShouldPopulateDataStoreFromLanguageFiles()
		{
			// Arrange
			string filePath = ConfigSettings.LocalizationFilePath;
			LocalizationRepository repository = new LocalizationRepository(MongoHelpers.GetLocalizationDatabase());

			// Act
			LocalizationData.CreateLanguageFiles(repository, filePath);

			// Assert
			Assert.IsNotNull(repository.Read("default"));
			Assert.IsNotNull(repository.Read("en-US"));
			Assert.IsNotNull(repository.Read("nl-nl"));
			repository.ClearCollection();
		}

		#endregion Test Methods
	}
}
