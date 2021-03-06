﻿using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using System.Collections.Generic;

namespace SingletonTheory.Services.AuthServices.TransferObjects.AuthAdmin
{
	[Route("/auth/admin/functionalpermissions")]
	[RequiredPermission(ApplyTo.Get, "FunctionalPermissions_Get")]
	public class FunctionalPermissions : IReturn<List<FunctionalPermission>>
	{
	}
}