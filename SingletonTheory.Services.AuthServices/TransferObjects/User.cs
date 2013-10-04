using MongoDB.Bson;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using SingletonTheory.Services.AuthServices.Entities;
using System;
using System.Collections.Generic;

namespace SingletonTheory.Services.AuthServices.TransferObjects
{
	[Route("/user")]
	[Route("/user/id/{Id}")]
	[Route("/user/username/{UserName}")]
	[RequiredPermission(ApplyTo.Get, "User_Get")]
	[RequiredPermission(ApplyTo.Put, "User_Put")]
	[RequiredPermission(ApplyTo.Post, "User_Post")]
	public class User : IReturn<User>
	{
		#region Fields & Properties

		public virtual ObjectId Id { get; set; }
		public virtual string UserName { get; set; }
		public virtual DateTime ModifiedDate { get; set; }
		public virtual string Password { get; set; }
		public virtual List<DomainPermissionObject> DomainPermissions { get; set; }
		public virtual List<string> Permissions { get; set; }
		public virtual List<int> Roles { get; set; }
		public virtual string Language { get; set; }
		public virtual bool Active { get; set; }
		public virtual Dictionary<string, string> Meta { get; set; }

		#endregion Fields & Properties

		#region Constructors

		public User()
		{
			DomainPermissions = new List<DomainPermissionObject>();
			Permissions = new List<string>();
			Roles = new List<int>();
			Meta = new Dictionary<string, string>();
		}

		#endregion Constructors
	}
}