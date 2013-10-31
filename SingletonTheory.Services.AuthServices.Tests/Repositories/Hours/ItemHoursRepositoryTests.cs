using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SingletonTheory.Services.AuthServices.Config;
using SingletonTheory.Services.AuthServices.Entities.Hours;
using SingletonTheory.Services.AuthServices.Repositories.Hours;
using SingletonTheory.Services.AuthServices.Tests.Data;

namespace SingletonTheory.Services.AuthServices.Tests.Repositories.Hours
{
	public class ItemHoursRepositoryTests
	{
		#region Test Methods

		private CostCentreEntity _costCentre;
		private HourTypeEntity _hourType;


		[SetUp]
		public void SetUpRelatedData()
		{
			ItemHoursRepository repository = new ItemHoursRepository(ConfigSettings.MySqlDatabaseConnectionName);
			repository.ClearCollection();

			CostCentreRepository costCentreRepository = new CostCentreRepository(ConfigSettings.MySqlDatabaseConnectionName);
			CostCentreEntity costCentreEntity = HoursData.GetCostCentreForInsert();
			costCentreRepository.ClearCollection();
			_costCentre = costCentreRepository.Create(costCentreEntity);

			HourTypeRepository hourTypeRepository = new HourTypeRepository(ConfigSettings.MySqlDatabaseConnectionName);
			HourTypeEntity hourTypeEntity = HoursData.GetHourTypeForInsert();
			hourTypeRepository.ClearCollection();
			_hourType = hourTypeRepository.Create(hourTypeEntity);
		}

		[TearDown]
		public void TearDownRelatedData()
		{
			ItemHoursRepository repository = new ItemHoursRepository(ConfigSettings.MySqlDatabaseConnectionName);
			repository.ClearCollection();

			CostCentreRepository costCentreRepository = new CostCentreRepository(ConfigSettings.MySqlDatabaseConnectionName);
			costCentreRepository.ClearCollection();

			HourTypeRepository hourTypeRepository = new HourTypeRepository(ConfigSettings.MySqlDatabaseConnectionName);
			hourTypeRepository.ClearCollection();
		}

		[Test]
		public void ShouldThrowArgumentNullExceptionOnConstructor()
		{
			// Act
			try
			{
				ItemHoursRepository repository = new ItemHoursRepository(null);
				Assert.Fail("This should not happen");
			}
			catch (Exception ex)
			{
				// Arrange
				Assert.IsInstanceOf<ArgumentNullException>(ex);
			}
		}

		[Test]
		public void ShouldCreateItemHoursEntry()
		{
			// Arrange
			ItemHoursRepository repository = new ItemHoursRepository(ConfigSettings.MySqlDatabaseConnectionName);
			ItemHoursEntity entity = HoursData.GetItemHoursEntryForInsert(_hourType, _costCentre);
			// Act
			entity = repository.Create(entity);

			// Assert
			Assert.IsNotNull(entity);
			Assert.AreNotEqual(0, entity.Id);
		}

		[Test]
		public void ShouldCreateItemHoursEntries()
		{
			// Arrange
			ItemHoursRepository repository = new ItemHoursRepository(ConfigSettings.MySqlDatabaseConnectionName);
			List<ItemHoursEntity> entities = HoursData.GetItemHoursEntriesForInsert(_hourType, _costCentre);

			// Act
			entities = repository.Create(entities);

			// Assert
			Assert.IsNotNull(entities);
			Assert.AreEqual(2, entities.Count);
		}

		[Test]
		public void ShouldReadItemHoursEntryWithId()
		{
			// Arrange
			ItemHoursRepository repository = new ItemHoursRepository(ConfigSettings.MySqlDatabaseConnectionName);
			ItemHoursEntity entity = HoursData.GetItemHoursEntryForInsert(_hourType,_costCentre);

			// Act
			entity = repository.Create(entity);

			// Act
			var actual = repository.Read(entity.Id);

			// Assert
			Assert.AreEqual(entity.OrderNumber, actual.OrderNumber);
		}

		[Test]
		public void ShouldUpdateItemHoursEntry()
		{
			// Arrange
			ItemHoursRepository repository = new ItemHoursRepository(ConfigSettings.MySqlDatabaseConnectionName);
			ItemHoursEntity entity = HoursData.GetItemHoursEntryForInsert(_hourType, _costCentre);
			entity = repository.Create(entity);
			entity.OrderNumber = 666;

			// Act
			ItemHoursEntity actual = repository.Update(entity);

			// Assert
			Assert.AreEqual(entity.OrderNumber, actual.OrderNumber);
		}

		[Test]
		public void ShouldDeleteItemHoursEntry()
		{
			// Arrange
			ItemHoursRepository repository = new ItemHoursRepository(ConfigSettings.MySqlDatabaseConnectionName);
			ItemHoursEntity entity = HoursData.GetItemHoursEntryForInsert(_hourType,_costCentre);
			entity = repository.Create(entity);

			// Act
			ItemHoursEntity actual = repository.Delete(entity);

			// Assert
			Assert.AreNotEqual(actual.DeletedDate, DateTime.MinValue);
		}

		#endregion Test Methods
	}
}
