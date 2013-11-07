using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.Common;
using ServiceStack.ServiceInterface;
using SingletonTheory.Services.AuthServices.Entities.Hours;
using SingletonTheory.Services.AuthServices.Repositories.Hours;
using SingletonTheory.Services.AuthServices.TransferObjects.Hours;
using SingletonTheory.Services.AuthServices.TransferObjects.Types;

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

		public List<CostCentre> Get(CostCentres request)
		{
			var repository = GetCostCentreRepository();
			var returnEntity = repository.Read();
			return TranslateToCostCentreResponse(returnEntity);
		}

		public List<HourType> Get(HourTypes request)
		{
			var repository = GetHourTypeRepository();
			var returnEntity = repository.Read();
			return TranslateToHourTypeResponse(returnEntity);
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

		private CostCentreRepository GetCostCentreRepository()
		{
			var repository = base.GetResolver().TryResolve<CostCentreRepository>();
			if (repository == null)
				throw new InvalidOperationException("CostCentreRepository not defined in IoC Container");

			return repository;
		}

		private HourTypeRepository GetHourTypeRepository()
		{
			var repository = base.GetResolver().TryResolve<HourTypeRepository>();
			if (repository == null)
				throw new InvalidOperationException("HourTypeRepository not defined in IoC Container");

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

		private List<CostCentre> TranslateToCostCentreResponse(List<CostCentreEntity> collection)
		{
			var response = new List<CostCentre>();
			foreach (var costCentreEntity in collection)
			{
				response.Add(costCentreEntity.TranslateTo<CostCentre>());
			}

			return response;
		}

		private List<HourType> TranslateToHourTypeResponse(List<HourTypeEntity> collection)
		{
			var response = new List<HourType>();
			foreach (var hourTypeEntity in collection)
			{
				response.Add(hourTypeEntity.TranslateTo<HourType>());
			}

			return response;
		}

		#endregion Private Methods
	}
}