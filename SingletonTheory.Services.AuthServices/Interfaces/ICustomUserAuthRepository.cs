using ServiceStack.ServiceInterface.Auth;
using System.Collections.Generic;

namespace SingletonTheory.Services.AuthServices.Interfaces
{
	public interface ICustomUserAuthRepository : IUserAuthRepository
	{
		List<UserAuth> GetAllUserAuths();
		void ClearUserAuths();
	}
}