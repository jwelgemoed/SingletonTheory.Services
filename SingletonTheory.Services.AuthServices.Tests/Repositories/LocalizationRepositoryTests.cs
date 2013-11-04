using NUnit.Framework;
using SingletonTheory.OrmLite;
using SingletonTheory.OrmLite.Interfaces;
using SingletonTheory.Services.AuthServices.Config;
using SingletonTheory.Services.AuthServices.Entities;
using SingletonTheory.Services.AuthServices.Repositories;
using SingletonTheory.Services.AuthServices.Tests.Data;
using SingletonTheory.Services.AuthServices.Tests.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace SingletonTheory.Services.AuthServices.Tests.Repositories
{
	[TestFixture]
	public class LocalizationRepositoryTests
	{
		[Test]
		public void ShouldClearCollection()
		{
			// Arrange
			LocalizationRepository repository = new LocalizationRepository(ConfigSettings.LocalizationDatabaseConnectionName);

			// Act
			repository.ClearCollection();

			// Assert
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(ConfigSettings.LocalizationDatabaseConnectionName))
			{
				List<LocalizationCollectionEntity> collection = provider.Select<LocalizationCollectionEntity>();
				Assert.IsEmpty(collection);
			}
		}

		[Test]
		public void ShouldReadDefaultLocale()
		{
			// Arrange
			LocalizationRepository repository = new LocalizationRepository(ConfigSettings.LocalizationDatabaseConnectionName);
			LocalizationDataProvider.PreInsertArrange(repository);
			string defaultLocale = "default";

			// Act
			LocalizationCollectionEntity entityToAssert = repository.Read(defaultLocale);

			// Assert
			Assert.IsNotNull(entityToAssert);
			Assert.AreEqual(entityToAssert.Locale, defaultLocale);
		}

		[Test]
		public void ShouldCreateLocale()
		{
			// Arrange
			LocalizationRepository repository = new LocalizationRepository(ConfigSettings.LocalizationDatabaseConnectionName);
			LocalizationDataProvider.PreInsertArrange(repository);
			LocalizationCollectionEntity entityToInsert = LocalizationDataProvider.GetLocalizationCollectionToCreate();

			// Act
			repository.Create(entityToInsert);

			// Assert
			LocalizationCollectionEntity entityToAssert = repository.Read(entityToInsert.Locale);
			Assert.IsNotNull(entityToAssert);
			Assert.AreEqual(entityToAssert.Locale, entityToInsert.Locale);
		}

		[Test]
		public void ShouldReadAllLocales()
		{
			// Arrange
			LocalizationRepository repository = new LocalizationRepository(ConfigSettings.LocalizationDatabaseConnectionName);
			LocalizationCollectionEntity localizationCollection = LocalizationDataProvider.PreInsertArrange(repository);
			LocalizationCollectionEntity entityToInsert = LocalizationDataProvider.GetLocalizationCollectionToCreate();
			repository.Create(entityToInsert, false);

			// Act
			List<LocalizationCollectionEntity> locales = repository.Read();

			// Assert
			Assert.IsNotNull(locales);
			Assert.AreEqual(2, locales.Count());
		}

		[Test]
		public void ShouldReadAllKeyValues()
		{
			// Arrange
			LocalizationRepository repository = new LocalizationRepository(ConfigSettings.LocalizationDatabaseConnectionName);
			LocalizationCollectionEntity localizationCollection = LocalizationDataProvider.PreInsertArrange(repository);
			string key = localizationCollection.LocalizationItems[0].Key;

			// Act
			LocalizationKeyCollectionEntity keyValueSetToAssert = repository.ReadAllKeyValues(key);

			// Assert
			Assert.IsNotNull(keyValueSetToAssert);
		}

		[Test]
		public void ShouldCreateAllKeyValues()
		{
			// Arrange
			LocalizationRepository repository = new LocalizationRepository(ConfigSettings.LocalizationDatabaseConnectionName);
			LocalizationDataProvider.PreInsertArrange(repository);
			LocalizationKeyCollectionEntity keyValueSetToCreate = LocalizationDataProvider.GetLocalizationKeyCollectionToCreate();

			// Act
			repository.CreateAllKeyValues(keyValueSetToCreate, false);

			// Assert
			LocalizationKeyCollectionEntity entityToAssert = repository.ReadAllKeyValues(keyValueSetToCreate.Key);
			Assert.IsNotNull(entityToAssert);
		}

		[Test]
		public void ShouldUpdateAllKeyValues()
		{
			// Arrange
			LocalizationRepository repository = new LocalizationRepository(ConfigSettings.LocalizationDatabaseConnectionName);
			LocalizationDataProvider.PreInsertArrange(repository);
			LocalizationKeyCollectionEntity created = LocalizationDataProvider.GetLocalizationKeyCollectionToCreate();
			created = repository.CreateAllKeyValues(created, false);
			created.KeyValues[0].Description += " Changed";

			// Act
			LocalizationKeyCollectionEntity keyValueSetToAssert = repository.UpdateAllKeyValues(created, false);

			// Assert
			LocalizationKeyCollectionEntity entityToAssert = repository.ReadAllKeyValues(created.Key);
			Assert.IsNotNull(entityToAssert);
		}

		[Test]
		public void ShouldDeleteAllKeyValues()
		{
			// Arrange
			LocalizationRepository repository = new LocalizationRepository(ConfigSettings.LocalizationDatabaseConnectionName);
			LocalizationCollectionEntity localizationCollection = LocalizationDataProvider.PreInsertArrange(repository);
			string key = localizationCollection.LocalizationItems[0].Key;

			// Act
			repository.DeleteAllKeyValues(key, false);

			// Assert
			LocalizationKeyCollectionEntity entityToAssert = repository.ReadAllKeyValues(key);
			Assert.IsNull(entityToAssert);
		}

		[Test]
		public void ShouldDeleteLocale()
		{
			// Arrange
			LocalizationRepository repository = new LocalizationRepository(ConfigSettings.LocalizationDatabaseConnectionName);
			LocalizationCollectionEntity entity = LocalizationDataProvider.PreInsertArrange(repository);

			// Act
			repository.Delete(entity.Locale);

			// Assert
			Assert.IsNull(repository.Read(entity.Locale));
		}

		[Test]
		public void ShouldReadLocalizationCollectionForSpecificItem()
		{
			// Arrange
			LocalizationRepository repository = new LocalizationRepository(ConfigSettings.LocalizationDatabaseConnectionName);
			LocalizationCollectionEntity assertItem = LocalizationDataProvider.PreInsertArrange(repository);
			LocalizationCollectionEntity request = new LocalizationCollectionEntity();
			request.LocalizationItems.Add(new LocalizationEntity() { Key = assertItem.LocalizationItems[0].Key });
			request.Locale = assertItem.Locale;

			// Act
			LocalizationCollectionEntity response = repository.Read(request);

			// Assert
			Assert.IsNotNull(response);
			Assert.That(response.LocalizationItems.Count, Is.EqualTo(1));
			Assert.That(response.LocalizationItems[0].Value, Is.EqualTo(assertItem.LocalizationItems[0].Value));
		}

		[Test]
		public void ShouldReadLocalizationCollectionForAllItems()
		{
			// Arrange
			LocalizationRepository repository = new LocalizationRepository(ConfigSettings.LocalizationDatabaseConnectionName);
			LocalizationCollectionEntity assertItem = LocalizationDataProvider.PreInsertArrange(repository);
			LocalizationCollectionEntity request = new LocalizationCollectionEntity();
			request.Locale = assertItem.Locale;

			// Act
			LocalizationCollectionEntity response = repository.Read(request.Locale);

			// Assert
			Assert.IsNotNull(response);
			Assert.That(response.LocalizationItems.Count, Is.GreaterThan(0));
		}
	}
}
