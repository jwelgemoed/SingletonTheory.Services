using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SingletonTheory.Services.AuthServices.Entities
{
	public class DomainPermissionObject
	{
		public int DomainPermissionId { get; set; }
		public ActiveTimeSpan ActiveTimeSpan { get; set; }
	}

	public class ActiveTimeSpan
	{
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
	}
}