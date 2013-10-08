using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using ServiceStack.DataAccess;
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

		public LocalizationKeyCollectionEntity GetAllKeyValues(string keyName)
		{
			try
			{
				var keyValues = new LocalizationKeyCollectionEntity
				                {
					                Key =  keyName
				                };
				var allLocales = GetAllLocaleCodes();
				foreach (var locale in allLocales)
				{
					keyValues.KeyValues.Add(GetKeyValueByNameAndLocale(locale,keyName));
				}
				return keyValues;
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
				var value = localeCollection.LocalizationItems.First(e => e.Key == keyName);
				return new LocalizationKeyEntity { LocaleId = localeCollection.Id, Locale = localeCollection.Locale, Description = value.Description, Value = value.Value };
			}
			catch (Exception ex)
			{
				throw new DataAccessException("Error querying Mongo Database: " + ex.Message);
			}
		}

		private IEnumerable<string> GetAllLocaleCodes()
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