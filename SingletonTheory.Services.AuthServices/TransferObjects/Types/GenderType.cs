using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;

namespace SingletonTheory.Services.AuthServices.TransferObjects.Types
{
  [Route("/types/gendertype")]
	public class GenderType
	{
		public long Id { get; set; }
		public string Description { get; set; }
		public DateTime DeletedDate { get; set; }
	}
}