using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;

namespace SingletonTheory.Services.AuthServices.TransferObjects.ContactDetail
{
	[Route("/contactdetails/contactdetail")]
	[Route("/contactdetails/contactdetail/id/{Id}")]
	//[RequiredPermission(ApplyTo.Get, "Contact_Get")]
	//[RequiredPermission(ApplyTo.Put, "Contact_Put")]
	//[RequiredPermission(ApplyTo.Post, "Contact_Post")]
	//[RequiredPermission(ApplyTo.Delete, "Contact_Delete")]
	public class ContactDetail : IReturn<ContactDetail>
	{

		//Contact
		public long Id { get; set; }
		public long ContactTypeId { get; set; }
		public string Value { get; set; }
		public bool Preffered { get; set; }
		public DateTime DeletedDate { get; set; }

		//Entity
		public long EntityId { get; set; }
		public string EntityName { get; set; }
		public long EntityTypeId { get; set; }

		//Person
		public long PersonId { get; set; }
		public long OccupationNameId { get; set; }
		public long TitleId { get; set; }
		public string SurnamePrefix { get; set; }
		public string Surname { get; set; }
		public string MaidenNamePrefix { get; set; }
		public string Nationality { get; set; }
		public DateTime DateOfBirth { get; set; }
		public string PlaceOfBirth { get; set; }

	}
}