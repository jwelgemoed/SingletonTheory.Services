using SingletonTheory.Library.IO;
using SingletonTheory.OrmLite;
using SingletonTheory.Services.AuthServices.Config;
using SingletonTheory.Services.AuthServices.Entities;
using SingletonTheory.Services.AuthServices.Repositories;
using System;

namespace SingletonTheory.Services.AuthServices.Tests.Data
{
	internal static class LocalizationDataProvider
	{
		internal static LocalizationCollectionEntity PreInsertArrange(LocalizationRepository repository)
		{
			repository.ClearCollection();
			return LocalizationDataProvider.InsertDefaultLocale();
		}

		internal static LocalizationCollectionEntity InsertDefaultLocale()
		{
			LocalizationCollectionEntity entity = GetDefaultLocalizationCollection();
			ProviderFactory.GetProvider(ConfigSettings.LocalizationDatabaseConnectionName).Insert<LocalizationCollectionEntity>(entity);

			return entity;
		}

		private static LocalizationCollectionEntity GetDefaultLocalizationCollection()
		{
			string filePath = ConfigSettings.LocalizationFilePath + @"\default.json";
			LocalizationCollectionEntity entity = SerializationUtilities.ReadFile<LocalizationCollectionEntity>(filePath);
			return entity;
		}

		internal static LocalizationCollectionEntity GetLocalizationCollectionToCreate()
		{
			string filePath = ConfigSettings.LocalizationFilePath + @"\en-US.json";
			return SerializationUtilities.ReadFile<LocalizationCollectionEntity>(filePath);
		}

		internal static LocalizationKeyCollectionEntity GetLocalizationKeyCollectionToCreate()
		{
			LocalizationKeyCollectionEntity collection = new LocalizationKeyCollectionEntity();
			collection.Key = Guid.NewGuid().ToString();

			LocalizationKeyEntity keyEntity = new LocalizationKeyEntity();
			keyEntity.Description = "Some Description";
			keyEntity.Locale = "default";
			keyEntity.Value = "SomeNewKey";

			return collection;
		}

		internal static LocalizationCollectionEntity GetLocalizationCollectionToDelete()
		{
			return GetDefaultLocalizationCollection();
		}
	}
}
