using ServiceStack.Common;
using ServiceStack.ServiceInterface;
using SingletonTheory.Services.AuthServices.Entities;
using SingletonTheory.Services.AuthServices.Repositories;
using SingletonTheory.Services.AuthServices.TransferObjects;
using System;

namespace SingletonTheory.Services.AuthServices.Services
{
	public class LocalizationService : Service
	{
		#region Public Methods

		public LocalizationDictionaryResponse Get(LocalizationDictionaryRequest request)
		{
			LocalizationRepository repository = GetRepository();
			LocalizationCollectionEntity collection = TranslateToEntity(request);

			if (request.LocalizationDictionary.Count == 0)
			{
				collection = repository.GetLocalizationDictionary(request.Locale);
				collection.Locale += "1";
			}
			else
			{
				LocalizationCollectionEntity collection2 = repository.GetLocalizationDictionary(collection);
				collection2.Locale += request.LocalizationDictionary.Count.ToString();
			}

			return TranslateToResponse(collection);
		}

		private LocalizationDictionaryResponse TranslateToResponse(LocalizationCollectionEntity collection)
		{
			LocalizationDictionaryResponse response = collection.TranslateTo<LocalizationDictionaryResponse>();

			collection.LocalizationItems.ForEach(x => response.LocalizationItems.Add(x.TranslateTo<LocalizationItem>()));

			return response;
		}

		private static LocalizationCollectionEntity TranslateToEntity(LocalizationDictionaryRequest request)
		{
			LocalizationCollectionEntity response = request.TranslateTo<LocalizationCollectionEntity>();

			request.LocalizationDictionary.ForEach(x => response.LocalizationItems.Add(x.TranslateTo<LocalizationEntity>()));

			return response;
		}

		public LocalizationDictionaryResponse Post(LocalizationDictionaryRequest request)
		{
			return PutPostLocalizationDictionary(request);
		}

		public LocalizationDictionaryResponse Put(LocalizationDictionaryRequest request)
		{
			return PutPostLocalizationDictionary(request);
		}

		#endregion Public Methods

		#region Private Methods

		private LocalizationDictionaryResponse PutPostLocalizationDictionary(LocalizationDictionaryRequest request)
		{
			LocalizationCollectionEntity collection = TranslateToEntity(request);
			LocalizationRepository repository = GetRepository();

			return TranslateToResponse(repository.Add(collection));
		}

		private LocalizationRepository GetRepository()
		{
			LocalizationRepository repository = base.GetResolver().TryResolve<LocalizationRepository>();
			if (repository == null)
				throw new InvalidOperationException("Localization Repository not defined in IoC Container");
			return repository;
		}

		#endregion Private Methods
	}
}