using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ServiceStack.DataAnnotations;
using SingletonTheory.OrmLite.Interfaces;

namespace SingletonTheory.Services.AuthServices.Entities.ContactDetails
{
  [Alias("EntiteitRelaties")]
	public class EntityRelationshipEntity : IIdentifiable
	{
		#region Fields & Properties

		[AutoIncrement]
		public long Id { get; set; }

		[Alias("Entiteit1Id")]
		[Required]
		[References(typeof(EntityEntity))]
		public long Entity1Id { get; set; }

		[Alias("Entiteit2Id")]
		[Required]
		[References(typeof(EntityEntity))]
		public long Entity2Id { get; set; }
		
		[Alias("InDatum")]
		[Required]
		public DateTime InDate { get; set; }

		[Alias("OutDatum")]
		[Required]
		public DateTime OutDate { get; set; }

		[Required]
		public bool Preferred { get; set; }

		[Alias("VerwijderdDatum")]
		[Required]
		public DateTime DeletedDate { get; set; }

		#endregion Fields & Properties

		#region IIdentifiable Members

		public void SetId(long id)
		{
			Id = id;
		}

		#endregion IIdentifiable Members
	}
}