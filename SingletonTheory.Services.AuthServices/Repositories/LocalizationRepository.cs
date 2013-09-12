using MongoDB.Driver;
using MongoDB.Driver.Builders;
using ServiceStack.DataAccess;
using SingletonTheory.Services.AuthServices.TransferObjects;
using System;

namespace SingletonTheory.Services.AuthServices.Repositories
{
	public class LocalizationRepository
	{
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

		public LocalizationDictionaryResponse GetLocalizationDictionary(string locale)
		{
			try
			{
				MongoCollection<LocalizationDictionaryRequest> locales = _mongoDatabase.GetCollection<LocalizationDictionaryRequest>("LocaleFiles");
				IMongoQuery localeQuery = Query<LocalizationDictionaryRequest>.EQ(e => e.Locale, locale);
				LocalizationDictionaryRequest dictionary = locales.FindOne(localeQuery);
				if (dictionary == null)
				{
					localeQuery = Query<LocalizationDictionaryRequest>.EQ(e => e.Locale, "default");
					dictionary = locales.FindOne(localeQuery);
				}

				if (dictionary != null)
				{
					return new LocalizationDictionaryResponse()
					{
						Id = dictionary.Id,
						Locale = dictionary.Locale,
						LocalizationDictionary = dictionary.LocalizationDictionary
					};
				}

				return null;
			}
			catch (Exception ex)
			{
				throw new DataAccessException("Error querying Mongo Database: " + ex.Message);
			}
		}

		public LocalizationDictionaryResponse InsertLocalizationDictionary(LocalizationDictionaryRequest record)
		{
			try
			{
				var locales = _mongoDatabase.GetCollection<LocalizationDictionaryRequest>("LocaleFiles");
				var localeQuery = Query<LocalizationDictionaryRequest>.EQ(e => e.Locale, record.Locale);
				var dictionary = locales.FindOne(localeQuery);
				if (dictionary == null)
				{
					locales.Insert(record);
					dictionary = record;
				}
				else
				{
					dictionary.LocalizationDictionary = record.LocalizationDictionary;
					locales.Save(dictionary);
				}
				return new LocalizationDictionaryResponse()
				{
					Id = dictionary.Id,
					Locale = dictionary.Locale,
					LocalizationDictionary = dictionary.LocalizationDictionary
				};
			}
			catch (Exception ex)
			{
				throw new DataAccessException("Unable to insert record in the Mongo Database: " + ex.Message);
			}
		}

		#endregion Public Methods
	}
}