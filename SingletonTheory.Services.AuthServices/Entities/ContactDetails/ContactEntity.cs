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
	[Alias("Contact")]
	public class ContactEntity : IIdentifiable
	{
		#region Fields & Properties

		[AutoIncrement]
		public long Id { get; set; }

	  [Alias("ContactTypeId")]
		[Required]
		[References(typeof(ContactTypeEntity))]
		public long ContactTypeId { get; set; }

		[Ignore()]
		[ReferencedEntity(typeof(ContactTypeEntity), "ContactTypeId")]
		public ContactTypeEntity ContactTypeEntity { get; set; }

		[Alias("EntiteitId")]
		[Required]
		[References(typeof(EntityEntity))]
		public long EntityId { get; set; }

		[Ignore()]
		[ReferencedEntity(typeof(EntityEntity), "EntityId")]
		public EntityEntity EntityEntity { get; set; }
		
		[Alias("Waarde")]
		[Required]
		[StringLength(30)]
		public string Value { get; set; }

		[Required]
		public bool Preffered { get; set; }

		[Alias("VerwijderdDatum")]
		[Required]
		public DateTime DeletedDate { get; set; }

		//[Required]
		//public DateTime DateUpdated { get; set; }

		#endregion Fields & Properties

		#region IIdentifiable Members

		public void SetId(long id)
		{
			Id = id;
		}

		#endregion IIdentifiable Members
	}
}