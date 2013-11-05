using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace SingletonTheory.Services.AuthServices.TransferObjects.Types
{
	[Route("/types/titles")]
	//[RequiredPermission(ApplyTo.Get, "Titles_Get")]
	public class Titles : IReturn<List<Title>>
	{
	}
}