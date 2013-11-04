using ServiceStack.Common;
using ServiceStack.ServiceInterface;
using SingletonTheory.Services.AuthServices.Entities;
using SingletonTheory.Services.AuthServices.Repositories;
using SingletonTheory.Services.AuthServices.TransferObjects.Localization;
using System;
using System.Collections.Generic;

namespace SingletonTheory.Services.AuthServices.Services
{
	public class LocalizationService : Service
	{
		#region Public Methods

		#region LocalizationLocaleCollection

		public LocalizationLocaleCollection Get(LocalizationLocaleCollection request)
		{
			LocalizationRepository repository = GetRepository();
			LocalizationLocaleCollection returnLocales = new LocalizationLocaleCollection();
			List<LocalizationCollectionEntity> allLocales = repository.Read();
			foreach (LocalizationCollectionEntity localeCode in allLocales)
			{
				returnLocales.Locales.Add(new LocalizationLocaleItem { LocaleKey = localeCode.Locale });
			}

			return returnLocales;
		}

		#endregion LocalizationLocaleCollection

		#region LocalizationKeyDictionary

		public LocalizationKeyDictionary Get(LocalizationKeyDictionary request)
		{
			var repository = GetRepository();
			var returnDictionary = repository.ReadAllKeyValues(request.Key);
			if (returnDictionary == null)
				return null;
			return TranslateToKeyResponse(returnDictionary);
		}

		public LocalizationKeyDictionary Post(LocalizationKeyDictionary request)
		{
			var repository = GetRepository();
			var requestEntity = TranslateToKeyEntity(request);
			var returnEntity = repository.CreateAllKeyValues(requestEntity);
			return TranslateToKeyResponse(returnEntity);
		}

		public LocalizationKeyDictionary Put(LocalizationKeyDictionary request)
		{
			var repository = GetRepository();
			var requestEntity = TranslateToKeyEntity(request);
			var returnEntity = repository.UpdateAllKeyValues(requestEntity);
			return TranslateToKeyResponse(returnEntity);
		}

		public void Delete(LocalizationKeyDictionary request)
		{
			var repository = GetRepository();
			repository.DeleteAllKeyValues(request.Key);
		}

		#endregion LocalizationKeyDictionary

		#region LocalizationDictionary

		public LocalizationDictionary Get(LocalizationDictionary request)
		{
			LocalizationRepository repository = GetRepository();
			if (request.Locale == null)
				return null;

			LocalizationCollectionEntity collection = repository.Read(request.Locale);

			return TranslateToResponse(collection);
		}

		public LocalizationDictionary Post(LocalizationDictionary request)
		{
			return PutPostLocalizationDictionary(request);
		}

		public LocalizationDictionary Put(LocalizationDictionary request)
		{
			return PutPostLocalizationDictionary(request);
		}

		public void Delete(LocalizationDictionary request)
		{
			var repository = GetRepository();
			repository.Delete(request.Locale);
		}

		#endregion LocalizationDictionary

		#endregion Public Methods

		#region Private Methods

		private LocalizationDictionary PutPostLocalizationDictionary(LocalizationDictionary request)
		{
			LocalizationCollectionEntity collection = TranslateToEntity(request);
			LocalizationRepository repository = GetRepository();

			return TranslateToResponse(repository.Create(collection));
		}

		private LocalizationRepository GetRepository()
		{
			LocalizationRepository repository = base.GetResolver().TryResolve<LocalizationRepository>();
			if (repository == null)
				throw new InvalidOperationException("Localization Repository not defined in IoC Container");
			return repository;
		}

		private LocalizationKeyCollectionEntity TranslateToKeyEntity(LocalizationKeyDictionary request)
		{
			LocalizationKeyCollectionEntity response = request.TranslateTo<LocalizationKeyCollectionEntity>();

			request.KeyValues.ForEach(x => response.KeyValues.Add(x.TranslateTo<LocalizationKeyEntity>()));

			return response;
		}

		private LocalizationKeyDictionary TranslateToKeyResponse(LocalizationKeyCollectionEntity collection)
		{
			LocalizationKeyDictionary response = collection.TranslateTo<LocalizationKeyDictionary>();

			collection.KeyValues.ForEach(x => response.KeyValues.Add(x.TranslateTo<LocalizationKeyItem>()));

			return response;
		}

		private LocalizationDictionary TranslateToResponse(LocalizationCollectionEntity collection)
		{
			LocalizationDictionary response = collection.TranslateTo<LocalizationDictionary>();

			collection.LocalizationItems.ForEach(x => response.LocalizationData.Add(x.TranslateTo<LocalizationItem>()));

			return response;
		}

		private static LocalizationCollectionEntity TranslateToEntity(LocalizationDictionary request)
		{
			LocalizationCollectionEntity response = request.TranslateTo<LocalizationCollectionEntity>();

			request.LocalizationData.ForEach(x => response.LocalizationItems.Add(x.TranslateTo<LocalizationEntity>()));

			return response;
		}

		#endregion Private Methods
	}
}