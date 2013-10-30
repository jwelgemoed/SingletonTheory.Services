using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ServiceStack.DataAnnotations;
using SingletonTheory.OrmLite.Interfaces;

namespace SingletonTheory.Services.AuthServices.Entities.ContactDetails
{
	[Alias("Titel")]
	public class TitleEnity : IIdentifiable
	{
		#region Fields & Properties

		[AutoIncrement]
		public long Id { get; set; }

		[Alias("Omschrijving")]
		[Required]
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