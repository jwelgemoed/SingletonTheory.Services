﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceInterface.Auth;
using SingletonTheory.Services.AuthServices.Interfaces;
using SingletonTheory.Services.AuthServices.TransferObjects;

namespace SingletonTheory.Services.AuthServices.Repositories
{
    public class CustomInMemoryAuthRepository : InMemoryAuthRepository, ICustomUserAuthRepository
    {
        public override void LoadUserAuth(IAuthSession session, IOAuthTokens tokens)
        {
            base.LoadUserAuth(session, tokens);
        }

        public List<UserAuth> GetAllUserAuths()
        {
            throw new NotImplementedException();
        }

	    public LocalizationDictionaryResponse GetLocalizationDictionary(string locale)
	    {
		    throw new NotImplementedException();
	    }

	    public void InsertLocalizationDictionary(LocalizationDictionaryRequest record)
	    {
		    throw new NotImplementedException();
	    }
    }
}