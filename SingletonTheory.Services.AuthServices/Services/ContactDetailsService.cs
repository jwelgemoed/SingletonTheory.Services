﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.Common;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Testing;
using SingletonTheory.Services.AuthServices.Config;
using SingletonTheory.Services.AuthServices.Entities.ContactDetails;
using SingletonTheory.Services.AuthServices.Extensions;
using SingletonTheory.Services.AuthServices.Repositories.ContactDetails;
using SingletonTheory.Services.AuthServices.TransferObjects.ContactDetail;
using SingletonTheory.Services.AuthServices.Utilities;

namespace SingletonTheory.Services.AuthServices.Services
{
	public class ContactDetailsService : Service
	{

		#region Contact

		public ContactDetail Get(ContactDetail request)
		{
			PersonRepository personRepository = GetPersonRepository();
			ContactRepository contactRepository = GetContactRepository();
			EntityRepository entityRepository = GetEntityRepository();

			ContactEntity contactEntity = contactRepository.Read(request.Id);
		

			List<PersonEntity> personEntities = personRepository.Read();

			return CreateGetContact(contactEntity, entityRepository, personEntities);
		}

		public ContactDetail Put(ContactDetail request)
		{
			PersonRepository personRepository = GetPersonRepository();
			ContactRepository contactRepository = GetContactRepository();
			EntityRepository entityRepository = GetEntityRepository();

			ContactEntity contactEntity = contactRepository.Read(request.Id);
			PersonEntity personEntity = personRepository.Read(request.PersonId);
			EntityEntity entity = entityRepository.Read(request.EntityId);


			contactEntity.ContactTypeId = request.ContactTypeId;
			contactEntity.Value = request.Value;
			contactEntity.Preffered = request.Preffered;
			contactEntity.DeletedDate = request.DeletedDate;
			contactEntity.EntityId = request.EntityId;


			entity.Name = request.EntityName;
			entity.EntityTypeId = request.EntityTypeId;


			personEntity.OccupationNameId = request.OccupationNameId;
			personEntity.TitleId = request.TitleId;
			personEntity.SurnamePrefix = request.SurnamePrefix;
			personEntity.Surname = request.Surname;
			personEntity.MaidenNamePrefix = request.MaidenNamePrefix;
			personEntity.Nationality = request.Nationality;
			personEntity.DateOfBirth = request.DateOfBirth;
			personEntity.PlaceOfBirth = request.PlaceOfBirth;

			contactRepository.Update(contactEntity);
			personRepository.Update(personEntity);
			entityRepository.Update(entity);

			List<PersonEntity> personEntities = personRepository.Read();
		
			return CreateGetContact(contactEntity, entityRepository, personEntities);
		}

		public ContactDetail Post(ContactDetail request)
		{
			PersonRepository personRepository = GetPersonRepository();
			ContactRepository contactRepository = GetContactRepository();
			EntityRepository entityRepository = GetEntityRepository();

			ContactEntity contactEntity = new ContactEntity();
			PersonEntity personEntity = new PersonEntity();
			EntityEntity entity = new EntityEntity();

			//Create entity 
			entity.Name = request.EntityName;
			entity.EntityTypeId = request.EntityTypeId;
			entity.DeletedDate = DateTime.MinValue;

			entity = entityRepository.Create(entity);

			contactEntity.EntityId = entity.Id;
			contactEntity.ContactTypeId = request.ContactTypeId == null ? -1 : request.ContactTypeId;
		  contactEntity.Value = request.Value;
		  contactEntity.Preffered = request.Preffered;
			contactEntity.DeletedDate = DateTime.MinValue;

			contactEntity = contactRepository.Create(contactEntity);

			personEntity.EntityId = entity.Id;
			personEntity.OccupationNameId = request.OccupationNameId;
			personEntity.TitleId = request.TitleId;
			personEntity.SurnamePrefix = request.SurnamePrefix;
			personEntity.Surname = request.Surname;
			personEntity.MaidenNamePrefix = request.MaidenNamePrefix;
			personEntity.Nationality = request.Nationality;
			personEntity.DateOfBirth = request.DateOfBirth;
			personEntity.PlaceOfBirth = request.PlaceOfBirth;
			personEntity.DeletedDate = DateTime.MinValue;

			try
			{
				personEntity = personRepository.Create(personEntity);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
			
			List<PersonEntity> personEntities = personRepository.Read();

			return CreateGetContact(contactEntity, entityRepository, personEntities);
		}

		public List<ContactDetail> Get(ContactDetails request)
		{
			return GetContacts();
		}

		private List<ContactDetail> GetContacts()
		{
			List<ContactDetail> contactReturnList = new List<ContactDetail>();
			PersonRepository personRepository = GetPersonRepository();
			ContactRepository contactRepository = GetContactRepository();
			EntityRepository entityRepository = GetEntityRepository();

			List<ContactEntity> contactEntities = contactRepository.Read();
			List<PersonEntity> personEntities = personRepository.Read();

			foreach (var contactEntity in contactEntities)
			{
				var contact = CreateGetContact(contactEntity, entityRepository, personEntities);

				contactReturnList.Add(contact);
			}

			return contactReturnList;
		}

		private static ContactDetail CreateGetContact(ContactEntity contactEntity, EntityRepository entityRepository,
			List<PersonEntity> personEntities)
		{
			ContactDetail contact = new ContactDetail
			{
				Id = contactEntity.Id,
				ContactTypeId = contactEntity.ContactTypeId,
				Value = contactEntity.Value,
				Preffered = contactEntity.Preffered,
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
			return contact;
		}

		#endregion Contact

		#region Address

		public Address Get(Address request)
		{
			AddressRepository addressRepository = GetAddressRepository();
			AddressEntity addressEntity = addressRepository.Read(request.Id);

			return addressEntity.TranslateToResponse();
		}

		public List<Address> Get(Addresses request)
		{
			AddressRepository addressRepository = GetAddressRepository();
			List<AddressEntity> addressEntities = addressRepository.Read();
			List<AddressEntity> addressEntityResponse = new List<AddressEntity>();

			if (request.EntityId != -1)
			{
				foreach (var addressEntity in addressEntities)
				{
					if (request.EntityId == addressEntity.EntityId)
					{
						addressEntityResponse.Add(addressEntity);
					}
				}
			}

			return request.EntityId != -1 ? addressEntityResponse.TranslateToResponse() : addressEntities.TranslateToResponse();
		}

		#endregion Address

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

		private AddressRepository GetAddressRepository()
		{
			AddressRepository repository = base.GetResolver().TryResolve<AddressRepository>();
			if (repository == null)
				throw new InvalidOperationException("AddressRepository not defined in IoC Container");

			return repository;
		}

		#endregion Private Methods
	}
}