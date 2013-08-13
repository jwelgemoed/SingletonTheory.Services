using ServiceStack.ServiceHost;

namespace Bridge.AuthenticationServices.TransferObjects
{
	public class UserRoleRequest : IReturn<UserRoleResponse>
	{
		public string UserName { get; set; }
		public string SessionId { get; set; }
	}
}