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
	public class EmployeeEntity : IIdentifiable
	{
		#region Fields & Properties

		[AutoIncrement]
		public long Id { get; set; }

		[Alias("EntiteitId")]
		[Required]
		[References(typeof(EntityEntity))]
		public long EntityId { get; set; }

		[Ignore()]
		[ReferencedEntity(typeof(EntityEntity), "EntityId")]
		public EntityEntity EntityEntity { get; set; }

		[Alias("PersoonId")]
		[Required]
		[References(typeof(PersonEntity))]
		public long PersonId { get; set; }

		[Ignore()]
		[ReferencedEntity(typeof(PersonEntity), "PersonId")]
		public PersonEntity PersonEntity { get; set; }

		[Alias("IndienstDatum")]
		[Required]
		public DateTime EmploymentStartDate { get; set; }

		[Alias("UitdienstDatum")]
		[Required]
		public DateTime EmploymentEndDate { get; set; }

		[Alias("Rijbewijs")]
		[Required]
		[StringLength(16)]
		public string DriversLicence { get; set; }

		[Alias("Paspoort")]
		[Required]
		[StringLength(30)]
		public string Passport { get; set; }

		[Alias("HeeftAuto")]
		[Required]
		public bool HasVehicle { get; set; }

		[Alias("PV")]
		[Required]
		public bool StaffAssociation { get; set; }

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