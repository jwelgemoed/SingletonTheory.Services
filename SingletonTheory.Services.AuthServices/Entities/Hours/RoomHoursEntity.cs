using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ServiceStack.DataAnnotations;
using SingletonTheory.OrmLite.Annotations;
using SingletonTheory.OrmLite.Interfaces;

namespace SingletonTheory.Services.AuthServices.Entities.Hours
{
	[Alias("RuimteUren")]
	public class RoomHoursEntity : IIdentifiable
	{
		#region Fields & Properties

		[AutoIncrement]
		public long Id { get; set; }

		[Required]
		[References(typeof(HourTypeEntity))]
		public long HourTypeId { get; set; }

		[Ignore()]
		[ReferencedEntity(typeof(HourTypeEntity), "HourTypeId")]
		public HourTypeEntity HourTypeEntity { get; set; }

		[Required]
		[References(typeof(CostCentreEntity))]
		public long CostCentreId { get; set; }

		[Ignore()]
		[ReferencedEntity(typeof(CostCentreEntity), "CostCentreId")]
		public CostCentreEntity CostCentreEntity { get; set; }

		[Alias("AanvraagNummer")]
		[Required]
		public int ConceptNumber { get; set; }


		[Alias("OrderNummer")]
		[Required]
		public int OrderNumber { get; set; }

		[Alias("RuimteNummer")]
		[Required]
		public int RoomNumber { get; set; }

		[Alias("PersoonNummer")]
		[Required]
		public int PersonNumber { get; set; }

		[Alias("Omschrijving")]
		[StringLength(22)]
		public string Description { get; set; }

		[Alias("Datum")]
		public DateTime Date { get; set; }

		[Alias("Verwijderd")]
		public bool Deleted { get; set; }
		
		#endregion Fields & Properties

		#region IIdentifiable Members

		public void SetId(long id)
		{
			Id = id;
		}

		#endregion IIdentifiable Members
	}
}