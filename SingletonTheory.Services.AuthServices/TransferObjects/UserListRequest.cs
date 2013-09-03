using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.Auth;
using System.Collections.Generic;

namespace SingletonTheory.Services.AuthServices.TransferObjects
{
	[Route("/UserRequest", Verbs = "GET")]
	public class UserListRequest : IReturn<List<UserAuth>>
	{
	}
}