using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.Common;
using ServiceStack.ServiceInterface;
using SingletonTheory.Services.AuthServices.Entities.Hours;
using SingletonTheory.Services.AuthServices.Repositories.Hours;
using SingletonTheory.Services.AuthServices.TransferObjects.Hours;

namespace SingletonTheory.Services.AuthServices.Services
{
	public class HoursService : Service
	{
		#region CRUD

		public ItemHoursEntry Post(ItemHoursEntry request)
		{
			var repository = GetItemHoursRepository();
			var returnEntity = repository.Create(TranslateToItemHoursEntity(request));
			return TranslateToItemHoursResponse(returnEntity);
		}

		#endregion CRUD

		#region Private Methods
		private ItemHoursRepository GetItemHoursRepository()
		{
			var repository = base.GetResolver().TryResolve<ItemHoursRepository>();
			if (repository == null)
				throw new InvalidOperationException("ItemHoursRepository not defined in IoC Container");

			return repository;
		}

		private ItemHoursEntry TranslateToItemHoursResponse(ItemHoursEntity entity)
		{
			ItemHoursEntry response = entity.TranslateTo<ItemHoursEntry>();

			return response;
		}

		private ItemHoursEntity TranslateToItemHoursEntity(ItemHoursEntry request)
		{
			ItemHoursEntity response = request.TranslateTo<ItemHoursEntity>();

			return response;
		}

		#endregion Private Methods
	}
}