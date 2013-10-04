using NUnit.Framework;
using SingletonTheory.Library.IO;
using SingletonTheory.Services.AuthServices.Config;
using SingletonTheory.Services.AuthServices.Data;
using SingletonTheory.Services.AuthServices.Entities;
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

		[Test]
		public void ShouldWriteFileFromDataStore()
		{
			// Arrange
			string filePath = ConfigSettings.LocalizationFilePath;
			LocalizationRepository repository = new LocalizationRepository(MongoHelpers.GetLocalizationDatabase());
			LocalizationData.CreateLanguageFiles(repository, filePath);

			// Act
			LocalizationCollectionEntity entity = repository.Read("default");
			entity.LocalizationItems[50].Value = "Please enter a minimum of {0} characters.";
			entity.LocalizationItems[51].Value = "Please do not exceed {0} characters.";

			// Assert
			SerializationUtilities.SerializeToJson(entity, false);
			repository.ClearCollection();
		}

		#endregion Test Methods
	}
}
