using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace SingletonTheory.Services.AuthServices.TransferObjects.Localization
{
	[Route("/localize/locales")]
	[RequiredPermission(ApplyTo.Get, "LocalizationLocaleCollection_Get")]
	public class LocalizationLocaleCollection: IReturn<LocalizationLocaleCollection>
	{
		public List<LocalizationLocaleItem> Locales { get; set; }

		public LocalizationLocaleCollection()
		{
			Locales = new List<LocalizationLocaleItem>();
		}
	}
}