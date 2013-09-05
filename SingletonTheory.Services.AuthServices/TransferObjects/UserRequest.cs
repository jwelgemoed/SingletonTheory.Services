using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.Auth;
using System.Collections.Generic;

namespace SingletonTheory.Services.AuthServices.TransferObjects
{
	[Route("/userapi")]
	[Route("/userapi/id/{Id}")]
	[Route("/userapi/username/{UserName}")]
	public class UserRequest : IReturn<List<UserAuth>>
	{
		public int Id { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public string Role { get; set; }
		public bool Active { get; set; }
		public string Language { get; set; }
	}
}