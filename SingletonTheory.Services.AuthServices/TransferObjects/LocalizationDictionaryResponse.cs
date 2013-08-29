using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;

namespace SingletonTheory.Services.AuthServices.TransferObjects
{
	public class LocalizationDictionaryResponse
	{
		public ObjectId Id { get; set; }
		public string Locale { get; set; }
		public List<LocalizationItem> LocalizationDictionary { get; set; } 
	}
}