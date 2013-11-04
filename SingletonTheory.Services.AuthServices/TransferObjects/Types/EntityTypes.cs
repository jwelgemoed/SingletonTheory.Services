using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace SingletonTheory.Services.AuthServices.TransferObjects.Types
{
	[Route("/entitytypes")]
	//[RequiredPermission(ApplyTo.Get, "EntityTypes_Get")]
	public class EntityTypes : IReturn<List<EntityType>>
	{
	}
}