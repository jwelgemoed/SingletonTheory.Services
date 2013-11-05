using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Testing;
using SingletonTheory.Services.AuthServices.Entities.ContactDetails;
using SingletonTheory.Services.AuthServices.Repositories.ContactDetails;
using SingletonTheory.Services.AuthServices.TransferObjects.ContactDetail;

namespace SingletonTheory.Services.AuthServices.Services
{
	public class ContactDetailsService : Service
	{

		#region Contact

		public List<Contact> Get(Contacts request)
		{
			ContactRepository repository = GetContactRepository();
			List<ContactEntity> entities = repository.Read();

			List<Contact> returnList = new List<Contact>();
			int x = 1;
			for (int index = 0; index < 3; index++)
			{
				Contact contact = new Contact();
				contact.Id = index + 1;
				contact.EntityName = "test" + index;
				contact.EntityId = index;
				contact.Surname = "surname" + index;
				returnList.Add(contact);
			}
			return returnList;
			//return entities.TranslateToResponse();
		}

		#endregion Contact

		#region Private Methods

		private ContactRepository GetContactRepository()
		{
			ContactRepository repository = base.GetResolver().TryResolve<ContactRepository>();
			if (repository == null)
				throw new InvalidOperationException("ContactRepository not defined in IoC Container");

			return repository;
		}

		#endregion Private Methods
	}
}