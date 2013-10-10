using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SingletonTheory.Services.AuthServices.TransferObjects.Localization;

namespace SingletonTheory.Services.AuthServices.Entities
{
	public class LocalizationKeyCollectionEntity
	{
			#region Fields & Properties

		public string Key { get; set; }
		public List<LocalizationKeyEntity> KeyValues { get; set; }

		#endregion Fields & Properties

		#region Constructors

		public LocalizationKeyCollectionEntity()
		{
			KeyValues = new List<LocalizationKeyEntity>();
		}

		#endregion Constructors
	}
}