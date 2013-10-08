using MongoDB.Bson;
using ServiceStack.ServiceHost;
using System.Collections.Generic;

namespace SingletonTheory.Services.AuthServices.TransferObjects
{
	[Route("/localize")]
	[Route("/localize/{Locale}")]
	//[RequiredPermission(ApplyTo.Get, "LocalizationDictionaryRequest_Get")]
	//[RequiredPermission(ApplyTo.Put, "LocalizationDictionaryRequest_Put")]
	//[RequiredPermission(ApplyTo.Post, "LocalizationDictionaryRequest_Post")]
	public class LocalizationDictionary : IReturn<LocalizationDictionary>
	{
		#region Fields & Properties

		public ObjectId Id { get; set; }
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