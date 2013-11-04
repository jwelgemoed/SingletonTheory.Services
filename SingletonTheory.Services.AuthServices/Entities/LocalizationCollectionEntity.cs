using ServiceStack.DataAnnotations;
using SingletonTheory.OrmLite.Interfaces;
using System.Collections.Generic;

namespace SingletonTheory.Services.AuthServices.Entities
{
	[Alias("LocaleFiles")]
	public class LocalizationCollectionEntity : IIdentifiable
	{
		#region Fields & Properties

		[AutoIncrement]
		public long Id { get; set; }
		public string Locale { get; set; }
		public List<LocalizationEntity> LocalizationItems { get; set; }

		#endregion Fields & Properties

		#region Constructors

		public LocalizationCollectionEntity()
		{
			LocalizationItems = new List<LocalizationEntity>();
		}

		#endregion Constructors

		#region IIdentifiable Members

		public void SetId(long id)
		{
			Id = id;
		}

		#endregion IIdentifiable Members
	}
}