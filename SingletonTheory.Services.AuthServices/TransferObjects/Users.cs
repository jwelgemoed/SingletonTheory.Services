using MongoDB.Bson;
using ServiceStack.ServiceHost;
using System.Collections.Generic;

namespace SingletonTheory.Services.AuthServices.TransferObjects
{
	[Route("/users")]
	public class Users : IReturn<List<User>>
	{
		public List<ObjectId> UserIds { get; set; }
		public List<string> UserNames { get; set; }
	}
}