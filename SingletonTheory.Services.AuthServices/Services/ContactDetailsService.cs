using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Testing;
using SingletonTheory.Services.AuthServices.Config;
using SingletonTheory.Services.AuthServices.Entities.ContactDetails;
using SingletonTheory.Services.AuthServices.Repositories.ContactDetails;
using SingletonTheory.Services.AuthServices.TransferObjects.ContactDetail;
using SingletonTheory.Services.AuthServices.Utilities;

namespace SingletonTheory.Services.AuthServices.Services
{
	public class ContactDetailsService : Service
	{

		#region Contact

		public List<Contact> Get(Contacts request)
		{
			return GetContacts();
		}

		private  List<Contact> GetContacts()
		{
			List<Contact> contactReturnList = new List<Contact>();
			PersonRepository personRepository = GetPersonRepository();
			ContactRepository contactRepository = GetContactRepository();
			EntityRepository entityRepository = GetEntityRepository();

			List<ContactEntity> contactEntities = contactRepository.Read();
			List<PersonEntity> personEntities = personRepository.Read();

			foreach (var contactEntity in contactEntities)
			{
				Contact contact = new Contact
				{
					Id = contactEntity.Id,
					ContactTypeId = contactEntity.ContactTypeId,
					Value = contactEntity.Value,
					DeletedDate = contactEntity.DeletedDate,
					EntityId = contactEntity.EntityId
				};
				EntityEntity entityEntity = entityRepository.Read(contactEntity.EntityId);
				PersonEntity personEntity = null;
				if (entityEntity != null)
				{
					contact.EntityName = entityEntity.Name;
					contact.EntityTypeId = entityEntity.EntityTypeId;

					personEntity = personEntities.FirstOrDefault(x => x.EntityId == entityEntity.Id);
				}

				if (personEntity != null)
				{
					contact.PersonId = personEntity.Id;
					contact.OccupationNameId = personEntity.OccupationNameId;
					contact.TitleId = personEntity.TitleId;
					contact.SurnamePrefix = personEntity.SurnamePrefix;
					contact.Surname = personEntity.Surname;
					contact.MaidenNamePrefix = personEntity.MaidenNamePrefix;
					contact.Nationality = personEntity.Nationality;
					contact.DateOfBirth = personEntity.DateOfBirth;
					contact.PlaceOfBirth = personEntity.PlaceOfBirth;
				}

				contactReturnList.Add(contact);
			}

			return contactReturnList;
		}

		#endregion Contact

		#region Private Methods

		private EntityRepository GetEntityRepository()
		{
			EntityRepository repository = base.GetResolver().TryResolve<EntityRepository>();
			if (repository == null)
				throw new InvalidOperationException("EntityRepository not defined in IoC Container");

			return repository;
		}

		private PersonRepository GetPersonRepository()
		{
			PersonRepository repository = base.GetResolver().TryResolve<PersonRepository>();
			if (repository == null)
				throw new InvalidOperationException("PersonRepository not defined in IoC Container");

			return repository;
		}

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