﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SingletonTheory.Services.AuthServices.Entities
{
	public class RoleEntity
	{
		public int Id { get; set; }
		public string Label { get; set; }
		public string Description { get; set; }
		public int[] DomainPermissionIds { get; set; }
	}
}