﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceClient.Web;
using SingletonTheory.Services.AuthServices.Config;
using SingletonTheory.Services.AuthServices.Interfaces;
using SingletonTheory.Services.AuthServices.Services;
using SingletonTheory.Services.AuthServices.TransferObjects;
using SingletonTheory.Services.AuthServices.TransferObjects.Localization;

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
			var language = "nl-nl";
			try
			{
				var currentUser = authService.Get(new CurrentUserAuthRequest());
				language = currentUser.Language;
			}
			catch (Exception) { }

			LocalizationDictionary request = new LocalizationDictionary();
			request.Locale = language;
			LocalizationDictionary localizationList = _client.Get<LocalizationDictionary>(request);

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
			var language = "nl-nl";
			try
			{
				var currentUser = authService.Get(new CurrentUserAuthRequest());
				language = currentUser.Language;
			}
			catch (Exception) { }

			LocalizationDictionary request = new LocalizationDictionary();
			request.LocalizationData.Add(new LocalizationItem() { Key = name });
			request.Locale = language;
			LocalizationDictionary localizationList = _client.Get<LocalizationDictionary>(request);

			return GetLabelFromLocalizationList(localizationList, name);
		}

		public static string GetLabelFromLocalizationList(LocalizationDictionary localizationList, string name)
		{
			foreach (var obj in localizationList.LocalizationData)
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