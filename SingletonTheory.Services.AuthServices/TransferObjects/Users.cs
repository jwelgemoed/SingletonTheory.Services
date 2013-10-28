using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using System.Collections.Generic;

namespace SingletonTheory.Services.AuthServices.TransferObjects
{
	[Route("/users")]
	[RequiredPermission(ApplyTo.Get, "Users_Get")]
	[RequiredPermission(ApplyTo.Post, "Users_Post")]
	public class Users : IReturn<List<User>>
	{
		public List<long> UserIds { get; set; }
		public List<string> UserNames { get; set; }
	}
}