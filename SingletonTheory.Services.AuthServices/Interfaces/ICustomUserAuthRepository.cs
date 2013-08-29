using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceInterface.Auth;
using SingletonTheory.Services.AuthServices.TransferObjects;

namespace SingletonTheory.Services.AuthServices.Interfaces
{
	public interface ICustomUserAuthRepository : IUserAuthRepository
	{
		List<UserAuth> GetAllUserAuths();
		LocalizationDictionaryResponse GetLocalizationDictionary(string locale);
		LocalizationDictionaryResponse InsertLocalizationDictionary(LocalizationDictionaryRequest record);
	}
}