using System.Collections.Generic;

namespace Bridge.AuthenticationServices.TransferObjects
{
	public class UserRoleResponse
	{
		public List<string> Roles { get; set; }
		public string UserName { get; set; }
	}
}
