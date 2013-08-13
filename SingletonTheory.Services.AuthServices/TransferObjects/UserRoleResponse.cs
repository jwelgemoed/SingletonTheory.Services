using System.Collections.Generic;

namespace SingletonTheory.Services.AuthServices.TransferObjects
{
	public class UserRoleResponse
	{
		public List<string> Roles { get; set; }
		public string UserName { get; set; }
	}
}
