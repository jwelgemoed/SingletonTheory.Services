using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using SingletonTheory.Services.AuthServices.Entities.ContactDetails;
using SingletonTheory.Services.AuthServices.Repositories.ContactDetails;
using SingletonTheory.Services.AuthServices.TransferObjects.Types;
using SingletonTheory.Services.AuthServices.Utilities;
using SingletonTheory.Services.AuthServices.Extensions;

namespace SingletonTheory.Services.AuthServices.Services
{
	public class TypeService : Service
	{
		#region Titles

		public List<Title> Get(Titles request)
		{
			TitleRepository repository = GetTitleRepository();
			List<TitleEntity> entities = repository.Read();
			
			return entities.TranslateToResponse();
		}

		#endregion Titles

		#region OccupationNames

		public List<OccupationName> Get(OccupationNames request)
		{
			OccupationNameRepository repository = GetOccupationNameRepository();
			List<OccupationNameEntity> entities = repository.Read();

			return entities.TranslateToResponse();
		}

		#endregion OccupationNames

		#region EntityTypes

		public List<EntityType> Get(EntityTypes request)
		{
			EntityTypeRepository repository = GetEntityTypeRepository();
			List<EntityTypeEntity> entities = repository.Read();

			return entities.TranslateToResponse();
		}

		#endregion EntityTypes

		#region ContactTypes

		public List<ContactType> Get(ContactTypes request)
		{
			ContactTypeRepository repository = GetContactTypeRepository();
			List<ContactTypeEntity> entities = repository.Read();

			return entities.TranslateToResponse();
		}

		#endregion ContactTypes

		#region AddressTypes

		public List<AddressType> Get(AddressTypes request)
		{
			AddressTypeRepository repository = GetAddressTypeRepository();
			List<AddressTypeEntity> entities = repository.Read();

			return entities.TranslateToResponse();
		}

		#endregion AddressTypes

		#region GenderTypes

		public List<GenderType> Get(GenderTypes request)
		{
			GenderTypeRepository repository = GetGenderTypeRepository();
			List<GenderTypeEntity> entities = repository.Read();

			return entities.TranslateToResponse();
		}

		#endregion GenderTypes

		#region Private Methods

		private TitleRepository GetTitleRepository()
		{
			TitleRepository repository = base.GetResolver().TryResolve<TitleRepository>();
			if (repository == null)
				throw new InvalidOperationException("Title Repository not defined in IoC Container");

			return repository;
		}

		private OccupationNameRepository GetOccupationNameRepository()
		{
			OccupationNameRepository repository = base.GetResolver().TryResolve<OccupationNameRepository>();
			if (repository == null)
				throw new InvalidOperationException("OccupationName Repository not defined in IoC Container");

			return repository;
		}

		private EntityTypeRepository GetEntityTypeRepository()
		{
			EntityTypeRepository repository = base.GetResolver().TryResolve<EntityTypeRepository>();
			if (repository == null)
				throw new InvalidOperationException("EntityType Repository not defined in IoC Container");

			return repository;
		}

		private ContactTypeRepository GetContactTypeRepository()
		{
			ContactTypeRepository repository = base.GetResolver().TryResolve<ContactTypeRepository>();
			if (repository == null)
				throw new InvalidOperationException("ContactType Repository not defined in IoC Container");

			return repository;
		}

		private AddressTypeRepository GetAddressTypeRepository()
		{
			AddressTypeRepository repository = base.GetResolver().TryResolve<AddressTypeRepository>();
			if (repository == null)
				throw new InvalidOperationException("AddressType Repository not defined in IoC Container");

			return repository;
		}

		private GenderTypeRepository GetGenderTypeRepository()
		{
			GenderTypeRepository repository = base.GetResolver().TryResolve<GenderTypeRepository>();
			if (repository == null)
				throw new InvalidOperationException("GenderType Repository not defined in IoC Container");

			return repository;
		}

		#endregion Private Methods
	}
}