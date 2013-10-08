using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;

namespace SingletonTheory.Services.AuthServices.Entities
{
	public class LocalizationKeyEntity
	{
		public ObjectId LocaleId { get; set; }
		public string Locale { get; set; }
		public string Value { get; set; }
		public string Description { get; set; }
	}
}