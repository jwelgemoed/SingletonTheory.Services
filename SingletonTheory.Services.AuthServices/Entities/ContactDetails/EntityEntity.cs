using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ServiceStack.DataAnnotations;
using SingletonTheory.OrmLite.Annotations;
using SingletonTheory.OrmLite.Interfaces;

namespace SingletonTheory.Services.AuthServices.Entities.ContactDetails
{
	[Alias("Entiteit")]
	public class EntityEntity : IIdentifiable
	{
		#region Fields & Properties

		[AutoIncrement]
		public long Id { get; set; }

		[Alias("EntiteitTypeId")]
		[Required]
		[References(typeof(EntityTypeEntity))]
		public long EntityTypeId { get; set; }

		[Ignore()]
		[ReferencedEntity(typeof(EntityTypeEntity), "EntityTypeId")]
		public EntityTypeEntity EntityTypeEntity { get; set; }

		[Alias("Naam")]
		[Required]
		[StringLength(30)]
		public string Name { get; set; }

		#endregion Fields & Properties

		#region IIdentifiable Members

		public void SetId(long id)
		{
			Id = id;
		}

		#endregion IIdentifiable Members
	}
}