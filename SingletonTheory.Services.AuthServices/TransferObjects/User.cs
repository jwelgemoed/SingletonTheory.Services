using MongoDB.Bson;
using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;

namespace SingletonTheory.Services.AuthServices.TransferObjects
{
	[Route("/userapi")]
	[Route("/userapi/id/{Id}")]
	[Route("/userapi/username/{UserName}")]
	public class User : IReturn<List<User>>
	{
		#region Fields & Properties

		public virtual ObjectId Id { get; set; }
		public virtual string UserName { get; set; }
		public virtual DateTime ModifiedDate { get; set; }
		public virtual string Password { get; set; }
		public virtual List<string> Permissions { get; set; }
		public virtual List<string> Roles { get; set; }
		public virtual string Language { get; set; }
		public virtual bool Active { get; set; }
		public virtual Dictionary<string, string> Meta { get; set; }

		#endregion Fields & Properties

		#region Constructors

		public User()
		{
			Permissions = new List<string>();
			Roles = new List<string>();
			Meta = new Dictionary<string, string>();
		}

		#endregion Constructors
	}
}