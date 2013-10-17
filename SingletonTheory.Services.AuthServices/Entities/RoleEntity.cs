using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SingletonTheory.Services.AuthServices.Entities
{
	public class RoleEntity
	{
		public int RootParentId { get; set; }
		public int ParentId { get; set; }
		public int Id { get; set; }
		public string Label { get; set; }
		public string Description { get; set; }
		public List<int> DomainPermissionIds { get; set; }
		public List<int> ChildRoleIds { get; set; }
		public DateTime DateTimeDeleted { get; set; }

		public RoleEntity()
		{
			DomainPermissionIds = new List<int>();
			ChildRoleIds = new List<int>();
		}
	}
}