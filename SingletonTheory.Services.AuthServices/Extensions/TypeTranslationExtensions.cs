using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.Common;
using SingletonTheory.Services.AuthServices.Entities.ContactDetails;
using SingletonTheory.Services.AuthServices.TransferObjects.Types;

namespace SingletonTheory.Services.AuthServices.Extensions
{
	public static class TypeTranslationExtensions
	{
		#region Address

		public static AddressType TranslateToResponse(this AddressTypeEntity entity)
		{
			AddressType response = entity.TranslateTo<AddressType>();

			return response;
		}

		public static List<AddressType> TranslateToResponse(this List<AddressTypeEntity> entities)
		{
			List<AddressType> response = new List<AddressType>();
			for (int i = 0; i < entities.Count; i++)
			{
				response.Add(TranslateToResponse(entities[i]));
			}

			return response;
		}

		public static AddressTypeEntity TranslateToEntity(this AddressType request)
		{
			AddressTypeEntity response = request.TranslateTo<AddressTypeEntity>();

			return response;
		}

		#endregion Address

		#region Title

		public static Title TranslateToResponse(this TitleEntity entity)
		{
			Title response = entity.TranslateTo<Title>();

			return response;
		}

		public static List<Title> TranslateToResponse(this List<TitleEntity> entities)
		{
			List<Title> response = new List<Title>();
			for (int i = 0; i < entities.Count; i++)
			{
				response.Add(TranslateToResponse(entities[i]));
			}

			return response;
		}

		public static TitleEntity TranslateToEntity(this Title request)
		{
			TitleEntity response = request.TranslateTo<TitleEntity>();

			return response;
		}

		#endregion Title

		#region ContactTypes

		public static ContactType TranslateToResponse(this ContactTypeEntity entity)
		{
			ContactType response = entity.TranslateTo<ContactType>();

			return response;
		}

		public static List<ContactType> TranslateToResponse(this List<ContactTypeEntity> entities)
		{
			List<ContactType> response = new List<ContactType>();
			for (int i = 0; i < entities.Count; i++)
			{
				response.Add(TranslateToResponse(entities[i]));
			}

			return response;
		}

		public static ContactTypeEntity TranslateToEntity(this ContactType request)
		{
			ContactTypeEntity response = request.TranslateTo<ContactTypeEntity>();

			return response;
		}

		#endregion ContactTypes

		#region EntityTypes

		public static EntityType TranslateToResponse(this EntityTypeEntity entity)
		{
			EntityType response = entity.TranslateTo<EntityType>();

			return response;
		}

		public static List<EntityType> TranslateToResponse(this List<EntityTypeEntity> entities)
		{
			List<EntityType> response = new List<EntityType>();
			for (int i = 0; i < entities.Count; i++)
			{
				response.Add(TranslateToResponse(entities[i]));
			}

			return response;
		}

		public static EntityTypeEntity TranslateToEntity(this EntityType request)
		{
			EntityTypeEntity response = request.TranslateTo<EntityTypeEntity>();

			return response;
		}

		#endregion EntityTypes

		#region OccupationNames

		public static OccupationName TranslateToResponse(this OccupationNameEntity entity)
		{
			OccupationName response = entity.TranslateTo<OccupationName>();

			return response;
		}

		public static List<OccupationName> TranslateToResponse(this List<OccupationNameEntity> entities)
		{
			List<OccupationName> response = new List<OccupationName>();
			for (int i = 0; i < entities.Count; i++)
			{
				response.Add(TranslateToResponse(entities[i]));
			}

			return response;
		}

		public static OccupationNameEntity TranslateToEntity(this OccupationName request)
		{
			OccupationNameEntity response = request.TranslateTo<OccupationNameEntity>();

			return response;
		}

		#endregion OccupationNames

		#region GenderTypes

		public static GenderType TranslateToResponse(this GenderTypeEntity entity)
		{
			GenderType response = entity.TranslateTo<GenderType>();

			return response;
		}

		public static List<GenderType> TranslateToResponse(this List<GenderTypeEntity> entities)
		{
			List<GenderType> response = new List<GenderType>();
			for (int i = 0; i < entities.Count; i++)
			{
				response.Add(TranslateToResponse(entities[i]));
			}

			return response;
		}

		public static GenderTypeEntity TranslateToEntity(this GenderType request)
		{
			GenderTypeEntity response = request.TranslateTo<GenderTypeEntity>();

			return response;
		}

		#endregion GenderTypes

	}
}