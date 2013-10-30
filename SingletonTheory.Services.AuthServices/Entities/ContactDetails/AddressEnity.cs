using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ServiceStack.DataAnnotations;
using SingletonTheory.OrmLite.Annotations;
using SingletonTheory.OrmLite.Interfaces;
using SingletonTheory.Services.AuthServices.Entities.Hours;

namespace SingletonTheory.Services.AuthServices.Entities.ContactDetails
{
	[Alias("Adres")]
	public class AddressEnity : IIdentifiable
	{
		#region Fields & Properties

		[AutoIncrement]
		public long Id { get; set; }

		[Alias("AdresTypeId")]
		[Required]
		[References(typeof(AddressTypeEntity))]
		public long AddressTypeId { get; set; }

		[Ignore()]
		[ReferencedEntity(typeof(AddressTypeEntity), "AddressTypeId")]
		public AddressTypeEntity AddressTypeEntity { get; set; }

		[Alias("EntiteitId")]
		[Required]
		[References(typeof(EntityEntity))]
		public long EntityId { get; set; }

		[Ignore()]
		[ReferencedEntity(typeof(EntityEntity), "EntityId")]
		public EntityEntity EntityEntity { get; set; }

		[Required]
		[Alias("Straat")]
		[StringLength(30)]
		public string Street { get; set; }

		[Required]
		[Alias("Nummer")]
		[StringLength(5)]
		public string StreetNumber { get; set; }

		[Required]
		[Alias("NummerAanvoegsel")]
		[StringLength(6)]
		public string StreetNumberAddition { get; set; }

		[Alias("Postcode")]
		[Required]
		[StringLength(6)]
		public string PostalCode { get; set; }

		[Alias("Stad")]
		[Required]
		[StringLength(30)]
		public string City { get; set; }

		[Alias("Landcode")]
		[Required]
		[StringLength(4)]
		public string CountryCode { get; set; }

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