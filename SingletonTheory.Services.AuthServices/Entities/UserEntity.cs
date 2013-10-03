using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace SingletonTheory.Services.AuthServices.Entities
{
	public class UserEntity
	{
		#region Fields & Properties

		public virtual ObjectId Id { get; set; }
		public virtual string UserName { get; set; }
		public virtual DateTime ModifiedDate { get; set; }
		public virtual string PasswordHash { get; set; }
		public virtual string Salt { get; set; }
		public virtual List<DomainPermissionObject> DomainPermissions { get; set; } 
		public virtual List<string> Permissions { get; set; }
		public virtual List<int> Roles { get; set; }
		public virtual string Language { get; set; }
		public virtual bool Active { get; set; }
		public virtual Dictionary<string, string> Meta { get; set; }

		#endregion Fields & Properties

		#region Constructors

		public UserEntity()
		{
			DomainPermissions = new List<DomainPermissionObject>();
			Permissions = new List<string>();
			Roles = new List<int>();
			Meta = new Dictionary<string, string>();
		}

		#endregion Constructors
	}
}