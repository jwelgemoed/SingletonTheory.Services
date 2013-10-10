using ServiceStack.Common;
using ServiceStack.ServiceInterface;
using SingletonTheory.Services.AuthServices.Entities;
using SingletonTheory.Services.AuthServices.Repositories;
using SingletonTheory.Services.AuthServices.TransferObjects;
using System;
using SingletonTheory.Services.AuthServices.TransferObjects.Localization;

namespace SingletonTheory.Services.AuthServices.Services
{
	public class LocalizationService : Service
	{
		#region Public Methods

		public LocalizationKeyDictionary Get(LocalizationKeyDictionary request)
		{
			LocalizationRepository repository = GetRepository();
			return TranslateToKeyResponse(repository.GetAllKeyValues(request.Key));
		}

		public LocalizationDictionary Get(LocalizationDictionary request)
		{
			LocalizationRepository repository = GetRepository();
			LocalizationCollectionEntity collection = TranslateToEntity(request);

			//if (request.LocalizationData.Count == 0)
		//	{
				collection = repository.Read(request.Locale);
		//		collection.Locale += "1";
		//	}
		//	else
		//	{
		//		LocalizationCollectionEntity collection2 = repository.Read(collection);
	//			collection2.Locale += request.LocalizationData.Count.ToString();
		//	}

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