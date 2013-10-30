using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ServiceStack.DataAnnotations;
using SingletonTheory.OrmLite.Interfaces;

namespace SingletonTheory.Services.AuthServices.Entities.Hours
{
	[Alias("KostenPlaats")]
	public class CostCentreEntity : IIdentifiable
	{
		#region Fields & Properties

		[AutoIncrement]
		public long Id { get; set; }

		[Required]
		[Index(Unique = true)]
		[StringLength(2)]
		public string Code { get; set; }

		[Alias("Omschrijving")]
		[StringLength(30)]
		public string Description { get; set; }

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