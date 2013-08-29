using System.ComponentModel;
using System.Diagnostics;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using ServiceStack.DataAccess;
using ServiceStack.ServiceInterface.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SingletonTheory.Services.AuthServices.Interfaces;
using SingletonTheory.Services.AuthServices.TransferObjects;

namespace SingletonTheory.Services.AuthServices.Repositories
{
	public class CustomMongoDBAuthRepository : MongoDBAuthRepository, ICustomUserAuthRepository
	{
		private MongoDatabase _mongoDatabase;
		public CustomMongoDBAuthRepository(MongoDatabase mongoDatabase, bool createMissingCollections)
			: base(mongoDatabase, createMissingCollections)
		{
			_mongoDatabase = mongoDatabase;
		}

		public List<UserAuth> GetAllUserAuths()
		{
			var users = _mongoDatabase.GetCollection<UserAuth>("UserAuth");
			MongoCursor<UserAuth> cursor = users.FindAllAs<UserAuth>();
			return cursor.ToList();
		}

		public LocalizationDictionaryResponse GetLocalizationDictionary(string locale)
		{
			try
			{
				var locales = _mongoDatabase.GetCollection<LocalizationDictionaryRequest>("LocaleFiles");
				var localeQuery = Query<LocalizationDictionaryRequest>.EQ(e => e.Locale, locale);
				var dictionary = locales.FindOne(localeQuery);
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

		public void InsertLocalizationDictionary(LocalizationDictionaryRequest record)
		{
			try
			{
				var locales = _mongoDatabase.GetCollection<LocalizationDictionaryRequest>("LocaleFiles");
				var localeQuery = Query<LocalizationDictionaryRequest>.EQ(e => e.Locale, record.Locale);
				var dictionary = locales.FindOne(localeQuery);
				if (dictionary == null)
				{
					locales.Insert(record);
				}
				else
				{
					dictionary.LocalizationDictionary = record.LocalizationDictionary;
					locales.Save(dictionary);
				}
			}
			catch (Exception ex)
			{
				throw new DataAccessException("Unable to insert record in the Mongo Database: " + ex.Message);
			}
		}

	}
}