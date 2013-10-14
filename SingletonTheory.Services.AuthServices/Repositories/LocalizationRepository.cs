using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using ServiceStack.DataAccess;
using SingletonTheory.Library.IO;
using SingletonTheory.Services.AuthServices.Config;
using SingletonTheory.Services.AuthServices.Entities;
using System;

namespace SingletonTheory.Services.AuthServices.Repositories
{
	public class LocalizationRepository
	{
		#region Constants

		private string CollectionName = "LocaleFiles";

		#endregion Constants

		#region Fields & Properties

		private MongoDatabase _mongoDatabase;

		#endregion Fields & Properties

		#region Constructors

		public LocalizationRepository(MongoDatabase mongoDatabase)
		{
			_mongoDatabase = mongoDatabase;
		}

		#endregion Constructors

		#region Public Methods

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
			try
			{
				MongoCollection<LocalizationCollectionEntity> locales = _mongoDatabase.GetCollection<LocalizationCollectionEntity>(CollectionName);
				IMongoQuery localeQuery = Query<LocalizationCollectionEntity>.EQ(e => e.Locale, locale);
				LocalizationCollectionEntity collection = locales.FindOne(localeQuery);
				if (collection == null)
				{
					localeQuery = Query<LocalizationCollectionEntity>.EQ(e => e.Locale, "default");
					collection = locales.FindOne(localeQuery);
				}

				return collection.LocalizationItems.Count == 0 ? null : collection;
			}
			catch (Exception ex)
			{
				throw new DataAccessException("Error querying Mongo Database: " + ex.Message);
			}
		}

		public void DeleteAllKeyValues(string keyName)
		{
			try
			{
				var allLocales = GetAllLocaleCodes();
				foreach (var locale in allLocales)
				{
					DeleteKeyByNameAndLocale(locale, keyName);
				}
			}
			catch (Exception ex)
			{
				throw new DataAccessException("Error updating Mongo Database: " + ex.Message);
			}
		}

		public LocalizationKeyCollectionEntity GetAllKeyValues(string keyName)
		{
			try
			{
				var keyValues = new LocalizationKeyCollectionEntity
												{
													Key = keyName
												};
				var allLocales = GetAllLocaleCodes();
				foreach (var locale in allLocales)
				{
					var keyEntry = GetKeyValueByNameAndLocale(locale, keyName);
					if (keyEntry != null)
						keyValues.KeyValues.Add(GetKeyValueByNameAndLocale(locale, keyName));
				}
				if (keyValues.KeyValues.Count == 0)
					return null;
				return keyValues;
			}
			catch (Exception ex)
			{
				throw new DataAccessException("Error querying Mongo Database: " + ex.Message);
			}
		}

		public LocalizationKeyCollectionEntity PostAllKeyValues(LocalizationKeyCollectionEntity keyValueSet)
		{
			try
			{
				var allLocales = GetAllLocaleCodes();
				foreach (var locale in allLocales)
				{
					CreateKeyEntryByNameAndLocale(locale, keyValueSet.Key, keyValueSet.Key, "");
				}
				return GetAllKeyValues(keyValueSet.Key);
			}
			catch (Exception ex)
			{
				throw new DataAccessException("Error updating Mongo Database: " + ex.Message);
			}
		}

		public LocalizationKeyCollectionEntity PutAllKeyValues(LocalizationKeyCollectionEntity keyValueSet)
		{
			try
			{
				foreach (var localizationKeyEntity in keyValueSet.KeyValues)
				{
					SetKeyValueByNameAndLocale(localizationKeyEntity.Locale, keyValueSet.Key, localizationKeyEntity.Value, localizationKeyEntity.Description);
				}
				return GetAllKeyValues(keyValueSet.Key);
			}
			catch (Exception ex)
			{
				throw new DataAccessException("Error querying Mongo Database: " + ex.Message);
			}
		}



		public LocalizationCollectionEntity Create(LocalizationCollectionEntity record)
		{
			try
			{
				var locales = _mongoDatabase.GetCollection<LocalizationCollectionEntity>(CollectionName);
				var localeQuery = Query<LocalizationCollectionEntity>.EQ(e => e.Locale, record.Locale);
				var dictionary = locales.FindOne(localeQuery);
				if (dictionary == null)
				{
					locales.Insert(record);
					dictionary = record;
				}
				else
				{
					dictionary.LocalizationItems = record.LocalizationItems;
					locales.Save(dictionary);
				}

				record.Id = dictionary.Id;

				return record;
			}
			catch (Exception ex)
			{
				throw new DataAccessException("Unable to insert record in the Mongo Database: " + ex.Message);
			}
		}

		public void ClearCollection()
		{
			_mongoDatabase.DropCollection(CollectionName);
		}

		#endregion Public Methods

		#region Private Methods

		private LocalizationKeyEntity GetKeyValueByNameAndLocale(string locale, string keyName)
		{
			try
			{
				var locales = _mongoDatabase.GetCollection<LocalizationCollectionEntity>(CollectionName);
				var localeCollection = locales.FindOne(Query<LocalizationCollectionEntity>.EQ(e => e.Locale, locale));
				if (!localeCollection.LocalizationItems.Any(e => e.Key == keyName))
					return null;
				var value = localeCollection.LocalizationItems.First(e => e.Key == keyName);
				return new LocalizationKeyEntity { LocaleId = localeCollection.Id, Locale = localeCollection.Locale, Description = value.Description, Value = value.Value };
			}
			catch (Exception ex)
			{
				throw new DataAccessException("Error querying Mongo Database: " + ex.Message);
			}
		}

		private void SetKeyValueByNameAndLocale(string locale, string keyName, string keyValue, string keyDescription)
		{
			try
			{
				var locales = _mongoDatabase.GetCollection<LocalizationCollectionEntity>(CollectionName);
				var localeCollection = locales.FindOne(Query<LocalizationCollectionEntity>.EQ(e => e.Locale, locale));
				var value = localeCollection.LocalizationItems.First(e => e.Key == keyName);
				value.Value = keyValue;
				value.Description = keyDescription;
				locales.Save(localeCollection);
				SerializationUtilities.ReplaceFile(ConfigSettings.LocalizationFilePath + @"\" + locale + ".json", localeCollection);
			}
			catch (Exception ex)
			{
				throw new DataAccessException("Error updating Mongo Database: " + ex.Message);
			}
		}

		private void DeleteKeyByNameAndLocale(string locale, string keyName)
		{
			try
			{
				var locales = _mongoDatabase.GetCollection<LocalizationCollectionEntity>(CollectionName);
				var localeCollection = locales.FindOne(Query<LocalizationCollectionEntity>.EQ(e => e.Locale, locale));
				var value = localeCollection.LocalizationItems.First(e => e.Key == keyName);
				localeCollection.LocalizationItems.Remove(value);
				locales.Save(localeCollection);
				SerializationUtilities.ReplaceFile(ConfigSettings.LocalizationFilePath + @"\" + locale + ".json", localeCollection);
			}
			catch (Exception ex)
			{
				throw new DataAccessException("Error updating Mongo Database: " + ex.Message);
			}
		}

		private void CreateKeyEntryByNameAndLocale(string locale, string keyName, string keyValue, string keyDescription)
		{
			try
			{
				var locales = _mongoDatabase.GetCollection<LocalizationCollectionEntity>(CollectionName);
				var localeCollection = locales.FindOne(Query<LocalizationCollectionEntity>.EQ(e => e.Locale, locale));
				if (localeCollection.LocalizationItems.Any(e => e.Key == keyName))
				{
					throw new DataException("The langauge key already exists:");
				}
				localeCollection.LocalizationItems.Add(new LocalizationEntity { Key = keyName, Value = keyValue, Description = keyDescription });
				locales.Save(localeCollection);
				SerializationUtilities.ReplaceFile(ConfigSettings.LocalizationFilePath + @"\" + locale + ".json", localeCollection);
			}
			catch (Exception ex)
			{
				throw new DataAccessException("Error adding data to Mongo Database: " + ex.Message);
			}
		}

		public IEnumerable<string> GetAllLocaleCodes()
		{
			try
			{
				var returnCodes = new List<string>();
				var locales = _mongoDatabase.GetCollection<LocalizationCollectionEntity>(CollectionName);
				var collection = locales.FindAll();
				foreach (var localizationCollectionEntity in collection)
				{
					returnCodes.Add(localizationCollectionEntity.Locale);
				}
				return returnCodes;
			}
			catch (Exception ex)
			{
				throw new DataAccessException("Error querying Mongo Database: " + ex.Message);
			}
		}

		#endregion

	}
}