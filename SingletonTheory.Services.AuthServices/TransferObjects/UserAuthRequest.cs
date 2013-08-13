using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.Auth;

namespace SingletonTheory.Services.AuthServices.TransferObjects
{
	public class UserAuthRequest : IReturn<UserAuth> { }
}