using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;

namespace SingletonTheory.Services.AuthServices.TransferObjects.ContactDetail
{
	[Route("/contactdetails/address")]
	[Route("/contactdetails/address/id/{Id}")]
	public class Address : IReturn<Address>
	{
		public long Id { get; set; }
		public long AddressTypeId { get; set; }
		public long EntityId { get; set; }
		public string Street { get; set; }
		public string StreetNumber { get; set; }
		public string StreetNumberAddition { get; set; }
		public string PostalCode { get; set; }
		public string City { get; set; }
		public string CountryCode { get; set; }
		public bool Preferred { get; set; }
		public DateTime DeletedDate { get; set; }
	}
}