using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace SingletonTheory.Services.AuthServices.TransferObjects.Types
{
	[Route("/types/title")]
	//[RequiredPermission(ApplyTo.Get, "Title_Get")]
	public class Title
	{
		public long Id { get; set; }
		public string Description { get; set; }
		public DateTime DeletedDate { get; set; }
	}
}