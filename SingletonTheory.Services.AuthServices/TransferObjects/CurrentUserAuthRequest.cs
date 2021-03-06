﻿using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.Auth;

namespace SingletonTheory.Services.AuthServices.TransferObjects
{
	[Route("/auth/currentuser")]
	public class CurrentUserAuthRequest : IReturn<UserAuth> { }
}