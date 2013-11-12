using System;
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

		#region Contact Details

		public ContactDetail Get(ContactDetail request)
		{
			PersonRepository personRepository = GetPersonRepository();
			EntityRepository entityRepository = GetEntityRepository();

			EntityEntity entityEntity = entityRepository.Read(request.EntityId);
		

			List<PersonEntity> personEntities = personRepository.Read();

			return CreateGetContactDetails(entityEntity, personEntities);
		}

		public ContactDetail Put(ContactDetail request)
		{
			PersonRepository personRepository = GetPersonRepository();
			EntityRepository entityRepository = GetEntityRepository();


			PersonEntity personEntity = personRepository.Read(request.PersonId);
			EntityEntity entity = entityRepository.Read(request.EntityId);

			entity.Name = request.EntityName;
			entity.EntityTypeId = request.EntityTypeId;

			personEntity.OccupationNameId = request.OccupationNameId;
			personEntity.TitleId = request.TitleId;
			personEntity.GenderTypeId = request.GenderTypeId;
			personEntity.SurnamePrefix = request.SurnamePrefix;
			personEntity.Surname = request.Surname;
			personEntity.MaidenNamePrefix = request.MaidenNamePrefix;
			personEntity.Nationality = request.Nationality;
			personEntity.DateOfBirth = request.DateOfBirth;
			personEntity.PlaceOfBirth = request.PlaceOfBirth;

			personRepository.Update(personEntity);
			entityRepository.Update(entity);

			List<PersonEntity> personEntities = personRepository.Read();

			return CreateGetContactDetails(entity, personEntities);
		}

		public ContactDetail Post(ContactDetail request)
		{
			PersonRepository personRepository = GetPersonRepository();
			EntityRepository entityRepository = GetEntityRepository();

			PersonEntity personEntity = new PersonEntity();
			EntityEntity entity = new EntityEntity();

			//Create entity 
			entity.Name = request.EntityName;
			entity.EntityTypeId = request.EntityTypeId;
			entity.DeletedDate = DateTime.MinValue;

			entity = entityRepository.Create(entity);

			personEntity.EntityId = entity.Id;
			personEntity.OccupationNameId = request.OccupationNameId;
			personEntity.TitleId = request.TitleId;
			personEntity.GenderTypeId = request.GenderTypeId;
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

			return CreateGetContactDetails(entity, personEntities);
		}

		public List<ContactDetail> Get(ContactDetails request)
		{
			return GetContacts();
		}

		private List<ContactDetail> GetContacts()
		{
			List<ContactDetail> contactReturnList = new List<ContactDetail>();
			PersonRepository personRepository = GetPersonRepository();
			EntityRepository entityRepository = GetEntityRepository();

			List<EntityEntity> entityEntities = entityRepository.Read();
			List<PersonEntity> personEntities = personRepository.Read();

			foreach (var entityEntity in entityEntities)
			{
				var contact = CreateGetContactDetails(entityEntity, personEntities);

				contactReturnList.Add(contact);
			}

			return contactReturnList;
		}

		private static ContactDetail CreateGetContactDetails(EntityEntity entityEntity, 
			List<PersonEntity> personEntities)
		{
			ContactDetail contact = new ContactDetail
			{
				EntityId = entityEntity.Id
			};

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
				contact.GenderTypeId = personEntity.GenderTypeId;
				contact.SurnamePrefix = personEntity.SurnamePrefix;
				contact.Surname = personEntity.Surname;
				contact.MaidenNamePrefix = personEntity.MaidenNamePrefix;
				contact.Nationality = personEntity.Nationality;
				contact.DateOfBirth = personEntity.DateOfBirth;
				contact.PlaceOfBirth = personEntity.PlaceOfBirth;
			}
			return contact;
		}

		#endregion Contact Details

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

		public Address Put(Address request)
		{
			AddressRepository addressRepository = GetAddressRepository();

			AddressEntity addressEntity = request.TranslateToEntity();
			try
			{
				addressEntity = addressRepository.Update(addressEntity);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}

			return addressEntity.TranslateToResponse();
		}

		public Address Post(Address request)
		{
			AddressRepository addressRepository = GetAddressRepository();

			AddressEntity addressEntity = request.TranslateToEntity();
			try
			{
				addressEntity = addressRepository.Create(addressEntity);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}

			return addressEntity.TranslateToResponse();
		}

		#endregion Address

		#region Contact

		public Contact Get(Contact request)
		{
			ContactRepository repository = GetContactRepository();
			ContactEntity entity = repository.Read(request.Id);

			return entity.TranslateToResponse();
		}

		public List<Contact> Get(Contacts request)
		{
			ContactRepository repository = GetContactRepository();
			List<ContactEntity> entities = repository.Read();
			List<ContactEntity> entityResponse = new List<ContactEntity>();

			if (request.EntityId != -1)
			{
				foreach (var entity in entities)
				{
					if (request.EntityId == entity.EntityId)
					{
						entityResponse.Add(entity);
					}
				}
			}

			return request.EntityId != -1 ? entityResponse.TranslateToResponse() : entities.TranslateToResponse();
		}

		public Contact Put(Contact request)
		{
			ContactRepository repository = GetContactRepository();

			ContactEntity entity = request.TranslateToEntity();
			try
			{
				entity = repository.Update(entity);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}

			return entity.TranslateToResponse();
		}

		public Contact Post(Contact request)
		{
			ContactRepository repository = GetContactRepository();

			ContactEntity entity = request.TranslateToEntity();
			try
			{
				entity = repository.Create(entity);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}

			return entity.TranslateToResponse();
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