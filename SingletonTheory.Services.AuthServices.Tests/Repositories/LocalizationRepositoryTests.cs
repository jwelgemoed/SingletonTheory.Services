using NUnit.Framework;
using SingletonTheory.Services.AuthServices.Entities;
using SingletonTheory.Services.AuthServices.Repositories;
using SingletonTheory.Services.AuthServices.Tests.Helpers;

namespace SingletonTheory.Services.AuthServices.Tests.Repositories
{
	[TestFixture]
	public class LocalizationRepositoryTests
	{
		[Test]
		public void ShouldGetLocalizationCollectionForSpecificItems()
		{
			// Arrange
			LocalizationRepository repository = new LocalizationRepository(MongoHelpers.GetMongoDatabase());
			LocalizationCollectionEntity request = new LocalizationCollectionEntity();
			request.LocalizationItems.Add(new LocalizationEntity() { Key = "_MainTitle_" });
			request.Locale = "nl-nl";

			// Act
			LocalizationCollectionEntity response = repository.GetLocalizationDictionary(request);

			// Assert
			Assert.IsNotNull(response);
			Assert.That(response.LocalizationItems.Count, Is.EqualTo(1));
			Assert.That(response.LocalizationItems[0].Value, Is.EqualTo("Singleton Theory Toegangsapplicatie."), "Does not returned correct value");
		}

		[Test]
		public void ShouldGetLocalizationCollectionForAllItems()
		{
			// Arrange
			LocalizationRepository repository = new LocalizationRepository(MongoHelpers.GetMongoDatabase());
			LocalizationCollectionEntity request = new LocalizationCollectionEntity();
			request.Locale = "nl-nl";

			// Act
			LocalizationCollectionEntity response = repository.GetLocalizationDictionary(request.Locale);

			// Assert
			Assert.IsNotNull(response);
			Assert.That(response.LocalizationItems.Count, Is.EqualTo(31));
		}
	}
}
