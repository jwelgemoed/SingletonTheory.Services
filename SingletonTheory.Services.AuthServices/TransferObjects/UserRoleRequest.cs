using ServiceStack.ServiceHost;

namespace SingletonTheory.Services.AuthServices.TransferObjects
{
	public class UserRoleRequest : IReturn<UserRoleResponse>
	{
		public string UserName { get; set; }
		public string SessionId { get; set; }
	}
}