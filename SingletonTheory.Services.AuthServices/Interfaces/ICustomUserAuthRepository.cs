using ServiceStack.ServiceInterface.Auth;
using SingletonTheory.Services.AuthServices.TransferObjects;
using System.Collections.Generic;

namespace SingletonTheory.Services.AuthServices.Interfaces
{
	public interface ICustomUserAuthRepository : IUserAuthRepository
	{
		List<UserAuth> GetAllUserAuths();
		void ClearUserAuths();
		LocalizationDictionaryResponse GetLocalizationDictionary(string locale);
		LocalizationDictionaryResponse InsertLocalizationDictionary(LocalizationDictionaryRequest record);
	}
}