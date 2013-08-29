using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using ServiceStack.ServiceHost;

namespace SingletonTheory.Services.AuthServices.TransferObjects
{
	[Route("/localize")]
	[Route("/localize/{Locale}")]
	public class LocalizationDictionaryRequest: IReturn<LocalizationDictionaryResponse>
	{
		public ObjectId Id { get; set; }
		public string Locale { get; set; }
		public List<LocalizationItem> LocalizationDictionary { get; set; } 
	}
}