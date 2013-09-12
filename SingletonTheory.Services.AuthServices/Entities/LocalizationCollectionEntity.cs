using MongoDB.Bson;
using System.Collections.Generic;

namespace SingletonTheory.Services.AuthServices.Entities
{
	public class LocalizationCollectionEntity
	{
		#region Fields & Properties

		public ObjectId Id { get; set; }
		public string Locale { get; set; }
		public List<LocalizationEntity> LocalizationItems { get; set; }

		#endregion Fields & Properties

		#region Constructors

		public LocalizationCollectionEntity()
		{
			LocalizationItems = new List<LocalizationEntity>();
		}

		#endregion Constructors
	}
}