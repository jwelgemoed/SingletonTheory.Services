using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using System.Collections.Generic;

namespace SingletonTheory.Services.AuthServices.TransferObjects.Localization
{
	[Route("/localize")]
	[Route("/localize/{Locale}")]
	[RequiredPermission(ApplyTo.Put, "LocalizationDictionary_Put")]
	[RequiredPermission(ApplyTo.Post, "LocalizationDictionary_Post")]
	[RequiredPermission(ApplyTo.Delete, "LocalizationDictionary_Delete")]
	public class LocalizationDictionary : IReturn<LocalizationDictionary>
	{
		#region Fields & Properties

		public long Id { get; set; }
		public string Locale { get; set; }
		public List<LocalizationItem> LocalizationData { get; set; }

		#endregion Fields & Properties

		#region Constructors

		public LocalizationDictionary()
		{
			LocalizationData = new List<LocalizationItem>();
		}

		#endregion Constructors
	}
}