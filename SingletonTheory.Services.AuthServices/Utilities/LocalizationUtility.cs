using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceClient.Web;
using SingletonTheory.Services.AuthServices.Config;
using SingletonTheory.Services.AuthServices.Interfaces;
using SingletonTheory.Services.AuthServices.Services;
using SingletonTheory.Services.AuthServices.TransferObjects;

namespace SingletonTheory.Services.AuthServices.Utilities
{
	public static class LocalizationUtility
	{
		#region Fields

		private static JsonServiceClient _client;

		#endregion Fields

		#region Constructor

		static LocalizationUtility()
		{
			_client = new JsonServiceClient(ConfigSettings.ServiceRootUrl)
			{
				UserName = ConfigSettings.ServiceUserName,
				Password = ConfigSettings.ServicePassword
			};
		}

		#endregion Constructor

		#region Methods

		public static void ApplyLanguagingToLabels(List<INameLabel> responseList)
		{
			//Add l18n here
			AuthService authService = new AuthService();
			var language = "default";
			try
			{
				var currentUser = authService.Get(new CurrentUserAuthRequest());
				language = currentUser.Language;
			}
			catch (Exception) { }

			LocalizationDictionaryRequest request = new LocalizationDictionaryRequest();
			request.Locale = language;
			LocalizationDictionaryResponse localizationList = _client.Get<LocalizationDictionaryResponse>(request);

			foreach (var obj in responseList)
			{
				obj.Label = GetLabelFromLocalizationList(localizationList, "_" + obj.Name + "_");
			}
		}

		public static string GetL18nLabel(string name)
		{
			name = "_" + name + "_";

			//Add l18n here
			AuthService authService = new AuthService();
			var language = "default";
			try
			{
				var currentUser = authService.Get(new CurrentUserAuthRequest());
				language = currentUser.Language;
			}
			catch (Exception) { }

			LocalizationDictionaryRequest request = new LocalizationDictionaryRequest();
			request.LocalizationDictionary.Add(new LocalizationItem() { Key = name });
			request.Locale = language;
			LocalizationDictionaryResponse localizationList = _client.Get<LocalizationDictionaryResponse>(request);

			return localizationList.LocalizationItems[0].Value ?? name;
		}

		public static string GetLabelFromLocalizationList(LocalizationDictionaryResponse localizationList, string name)
		{
			foreach (var obj in localizationList.LocalizationItems)
			{
				if (obj.Key.Equals(name))
				{
					return obj.Value;
				}
			}
			return name;
		}

		#endregion Methods
	}
}