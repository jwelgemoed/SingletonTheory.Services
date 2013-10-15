using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace SingletonTheory.Services.AuthServices.TransferObjects.Localization
{
	[Route("/localize/keys/{Key}")]
	[RequiredPermission(ApplyTo.Get, "LocalizationKeyDictionary_Get")]
	[RequiredPermission(ApplyTo.Post, "LocalizationKeyDictionary_Post")]
	[RequiredPermission(ApplyTo.Put, "LocalizationKeyDictionary_Put")]
	[RequiredPermission(ApplyTo.Delete, "LocalizationKeyDictionary_Delete")]
	public class LocalizationKeyDictionary : IReturn<LocalizationKeyDictionary>
	{
		#region Fields & Properties

		public string Key { get; set; }
		public List<LocalizationKeyItem> KeyValues { get; set; }

		#endregion Fields & Properties

		#region Constructors

		public LocalizationKeyDictionary()
		{
			KeyValues = new List<LocalizationKeyItem>();
		}

		#endregion Constructors
	}
}