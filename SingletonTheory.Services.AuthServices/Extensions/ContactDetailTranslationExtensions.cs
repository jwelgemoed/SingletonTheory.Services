using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.Common;
using SingletonTheory.Services.AuthServices.Entities.ContactDetails;
using SingletonTheory.Services.AuthServices.TransferObjects.ContactDetail;

namespace SingletonTheory.Services.AuthServices.Extensions
{
	public static class ContactDetailTranslationExtensions
	{
		#region Address

		public static Address TranslateToResponse(this AddressEntity entity)
		{
			Address response = entity.TranslateTo<Address>();

			return response;
		}

		public static List<Address> TranslateToResponse(this List<AddressEntity> entities)
		{
			List<Address> response = new List<Address>();
			for (int i = 0; i < entities.Count; i++)
			{
				response.Add(TranslateToResponse(entities[i]));
			}

			return response;
		}

		public static AddressEntity TranslateToEntity(this Address request)
		{
			AddressEntity response = request.TranslateTo<AddressEntity>();

			return response;
		}

		#endregion Role
	}
}