using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SingletonTheory.Services.AuthServices.Entities
{
	public class DomainPermissionEntity
	{
		public int Id { get; set; }
		public string Label { get; set; }
		public string Description { get; set; }
		public int[] FunctionalPermissionIds { get; set; }
	}
}