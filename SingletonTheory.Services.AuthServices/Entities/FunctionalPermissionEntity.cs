using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SingletonTheory.Services.AuthServices.Entities
{
	public class FunctionalPermissionEntity
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int[] PermissionIds { get; set; }
	}
}