﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;

namespace SingletonTheory.Services.AuthServices.TransferObjects.Types
{
	[Route("/types/addresstypes")]
	public class AddressTypes : IReturn<List<AddressType>>
	{
	}
}