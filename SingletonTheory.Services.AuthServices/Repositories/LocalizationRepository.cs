using ServiceStack.DataAccess;
using SingletonTheory.Library.IO;
using SingletonTheory.OrmLite;
using SingletonTheory.OrmLite.Interfaces;
using SingletonTheory.Services.AuthServices.Config;
using SingletonTheory.Services.AuthServices.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SingletonTheory.Services.AuthServices.Repositories
{
	public class LocalizationRepository
	{
		#region Fields & Properties

		private string _connectionName;

		#endregion Fields & Properties

		#region Constructors

		public LocalizationRepository(string connectionName)
		{
			_connectionName = connectionName;
		}

		#endregion Constructors

		#region Public Methods

		public LocalizationCollectionEntity Create(LocalizationCollectionEntity record, bool writeToFile = true)
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionName))
			{
				LocalizationCollectionEntity collection = Read(record.Locale);
				if (collection == null)
				{
					collection = CreateCleanLocalizationDictionary(record.Locale);
				}
				else
				{
					collection.LocalizationItems = record.LocalizationItems;
					provider.Update(collection);
				}

				if (writeToFile)
					SerializationUtilities.ReplaceFile(ConfigSettings.LocalizationFilePath + @"\" + record.Locale + ".json", collection);

				return collection;
			}
		}

		public LocalizationKeyCollectionEntity CreateAllKeyValues(LocalizationKeyCollectionEntity keyValueSet, bool writeFile = true)
		{
			List<LocalizationCollectionEntity> allLocales = Read();
			foreach (LocalizationCollectionEntity locale in allLocales)
			{
				CreateKeyEntryByNameAndLocale(locale.Locale, keyValueSet.Key, keyValueSet.Key, "", writeFile);
			}

			return ReadAllKeyValues(keyValueSet.Key);
		}

		public List<LocalizationCollectionEntity> Read()
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionName))
			{
				return provider.Select<LocalizationCollectionEntity>();
			}
		}

		public LocalizationCollectionEntity Read(LocalizationCollectionEntity collection)
		{
			try
			{
				LocalizationCollectionEntity fullCollection = Read(collection.Locale);

				if (fullCollection != null)
				{
					for (int i = 0; i < collection.LocalizationItems.Count; i++)
					{
						LocalizationEntity item = fullCollection.LocalizationItems.Find(x => x.Key == collection.LocalizationItems[i].Key);
						if (item == null)
						{
							continue;
						}

						collection.LocalizationItems[i] = item;
					}

					return collection;
				}

				return null;
			}
			catch (Exception ex)
			{
				throw new DataAccessException("Error querying Mongo Database: " + ex.Message);
			}
		}

		public LocalizationCollectionEntity Read(string locale)
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionName))
			{
				return provider.Select<LocalizationCollectionEntity>(x => x.Locale == locale).FirstOrDefault();
			}
		}

		public LocalizationKeyCollectionEntity ReadAllKeyValues(string keyName)
		{
			LocalizationKeyCollectionEntity keyValues = new LocalizationKeyCollectionEntity
			{
				Key = keyName
			};

			List<LocalizationCollectionEntity> allLocales = Read();
			foreach (LocalizationCollectionEntity locale in allLocales)
			{
				var keyEntry = GetKeyValueByNameAndLocale(locale, keyName);
				if (keyEntry != null)
					keyValues.KeyValues.Add(GetKeyValueByNameAndLocale(locale, keyName));
			}

			if (keyValues.KeyValues.Count == 0)
				return null;

			return keyValues;
		}

		public LocalizationKeyCollectionEntity UpdateAllKeyValues(LocalizationKeyCollectionEntity keyValueSet, bool writeFile = true)
		{
			try
			{
				foreach (LocalizationKeyEntity localizationKeyEntity in keyValueSet.KeyValues)
				{
					SetKeyValueByNameAndLocale(localizationKeyEntity.Locale, keyValueSet.Key, localizationKeyEntity.Value, localizationKeyEntity.Description);
				}

				return ReadAllKeyValues(keyValueSet.Key);
			}
			catch (Exception ex)
			{
				throw new DataAccessException("Error querying Mongo Database: " + ex.Message);
			}
		}

		public void DeleteAllKeyValues(string keyName, bool writeFile = true)
		{
			List<LocalizationCollectionEntity> allLocales = Read();
			foreach (LocalizationCollectionEntity locale in allLocales)
			{
				DeleteKeyByNameAndLocale(locale.Locale, keyName, writeFile);
			}
		}

		public void Delete(string locale)
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionName))
			{
				provider.Delete<LocalizationCollectionEntity>(x => x.Locale == locale);
			}
		}

		public void ClearCollection()
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionName))
			{
				provider.DeleteAll<LocalizationCollectionEntity>();
			}
		}

		#endregion Public Methods

		#region Private Methods

		private LocalizationCollectionEntity CreateCleanLocalizationDictionary(string localeName)
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionName))
			{
				LocalizationCollectionEntity collection = Read("default");
				if (collection == null)
				{
					throw new DataException("Unable to find the default locale");
				}

				LocalizationCollectionEntity newLocale = new LocalizationCollectionEntity();
				newLocale.Locale = localeName;
				foreach (LocalizationEntity localizationEntity in collection.LocalizationItems)
				{
					newLocale.LocalizationItems.Add(new LocalizationEntity { Key = localizationEntity.Key, Value = localizationEntity.Key, Description = "" });
				}

				return provider.Insert(newLocale);
			}
		}

		private LocalizationKeyEntity GetKeyValueByNameAndLocale(LocalizationCollectionEntity collection, string keyName)
		{
			if (!collection.LocalizationItems.Any(e => e.Key == keyName))
				return null;

			LocalizationEntity value = collection.LocalizationItems.First(e => e.Key == keyName);

			return new LocalizationKeyEntity { LocaleId = collection.Id, Locale = collection.Locale, Description = value.Description, Value = value.Value };
		}

		private void SetKeyValueByNameAndLocale(string locale, string keyName, string keyValue, string keyDescription, bool writeFile = true)
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionName))
			{
				LocalizationCollectionEntity localeCollection = Read(locale);
				LocalizationEntity value = localeCollection.LocalizationItems.First(e => e.Key == keyName);

				value.Value = keyValue;
				value.Description = keyDescription;

				provider.Update(localeCollection);
			}
		}

		private void DeleteKeyByNameAndLocale(string locale, string keyName, bool writeFile = true)
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionName))
			{
				LocalizationCollectionEntity localeCollection = Read(locale);
				LocalizationEntity value = localeCollection.LocalizationItems.First(e => e.Key == keyName);

				localeCollection.LocalizationItems.Remove(value);
				provider.Update(localeCollection);
			}
		}

		private void CreateKeyEntryByNameAndLocale(string locale, string keyName, string keyValue, string keyDescription, bool writeFile = true)
		{
			using (IDatabaseProvider provider = ProviderFactory.GetProvider(_connectionName))
			{
				LocalizationCollectionEntity localeCollection = Read(locale);
				if (localeCollection.LocalizationItems.Any(e => e.Key == keyName))
				{
					throw new DataException("The langauge key already exists:");
				}

				localeCollection.LocalizationItems.Add(new LocalizationEntity { Key = keyName, Value = keyValue, Description = keyDescription });
				provider.Update(localeCollection);
				if (writeFile)
					SerializationUtilities.ReplaceFile(ConfigSettings.LocalizationFilePath + @"\" + locale + ".json", localeCollection);
			}
		}

		#endregion
	}
}