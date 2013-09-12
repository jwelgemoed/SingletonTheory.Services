using ServiceStack.ServiceInterface;
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

			return repository.GetLocalizationDictionary(request.Locale);
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
			LocalizationRepository repository = GetRepository();
			return repository.InsertLocalizationDictionary(request);
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