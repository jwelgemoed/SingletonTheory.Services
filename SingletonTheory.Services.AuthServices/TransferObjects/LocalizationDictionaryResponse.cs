﻿using MongoDB.Bson;
using System.Collections.Generic;

namespace SingletonTheory.Services.AuthServices.TransferObjects
{
	public class LocalizationDictionaryResponse
	{
		#region Fields & Properties

		public ObjectId Id { get; set; }
		public string Locale { get; set; }
		public List<LocalizationItem> LocalizationItems { get; set; }

		#endregion Fields & Properties

		#region Constructors

		public LocalizationDictionaryResponse()
		{
			LocalizationItems = new List<LocalizationItem>();
		}

		#endregion Constructors
	}
}