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
	[Alias("Persoon")]
	public class PersonEntity : IIdentifiable
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

		[Alias("BeroepsnaamId")]
		[Required]
		[References(typeof(OccupationNameEntity))]
		public long OccupationNameId { get; set; }

		[Ignore()]
		[ReferencedEntity(typeof(OccupationNameEntity), "OccupationNameId")]
		public OccupationNameEntity OccupationNameEntity { get; set; }

		[Alias("TitelId")]
		[Required]
		[References(typeof(TitleEnity))]
		public long TitleId { get; set; }

		[Ignore()]
		[ReferencedEntity(typeof(TitleEnity), "TitleId")]
		public TitleEnity TitleEnity { get; set; }

		[Alias("Voorvoegsel")]
		[Required]
		[StringLength(10)]
		public string SurnamePrefix { get; set; }

		[Alias("Achternaam")]
		[Required]
		[StringLength(30)]
		public string Surname { get; set; }

		[Alias("MeisjesVoorvoegsel")]
		[Required]
		[StringLength(30)]
		public string MaidenNamePrefix { get; set; }

		[Alias("Nationaliteit")]
		[Required]
		[StringLength(4)]
		public string Nationality { get; set; }

		[Alias("GeboorteDatum")]
		[Required]
		public DateTime DateOfBirth { get; set; }

		[Alias("GeboortePlaats")]
		[Required]
		[StringLength(30)]
		public string PlaceOfBirth { get; set; }

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