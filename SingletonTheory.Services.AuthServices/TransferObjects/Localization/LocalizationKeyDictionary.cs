using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SingletonTheory.Services.AuthServices.TransferObjects.Localization
{
	public class LocalizationKeyDictionary
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