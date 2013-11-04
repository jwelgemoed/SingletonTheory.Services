using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SingletonTheory.Services.AuthServices.Config;
using SingletonTheory.Services.AuthServices.Entities.Hours;
using SingletonTheory.Services.AuthServices.Repositories.Hours;
using SingletonTheory.Services.AuthServices.Tests.Data;

namespace SingletonTheory.Services.AuthServices.Tests.Repositories.Hours
{
	class RoomHoursRepositoryTests
	{
		#region Test Methods

		private CostCentreEntity _costCentre;
		private HourTypeEntity _hourType;


		[SetUp]
		public void SetUpRelatedData()
		{
			RoomHoursRepository repository = new RoomHoursRepository(ConfigSettings.MySqlDatabaseConnectionName);
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
			RoomHoursRepository repository = new RoomHoursRepository(ConfigSettings.MySqlDatabaseConnectionName);
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
				RoomHoursRepository repository = new RoomHoursRepository(null);
				Assert.Fail("This should not happen");
			}
			catch (Exception ex)
			{
				// Arrange
				Assert.IsInstanceOf<ArgumentNullException>(ex);
			}
		}

		[Test]
		public void ShouldCreateRoomHoursEntry()
		{
			// Arrange
			RoomHoursRepository repository = new RoomHoursRepository(ConfigSettings.MySqlDatabaseConnectionName);
			RoomHoursEntity entity = HoursData.GetRoomHoursEntryForInsert(_hourType, _costCentre);
			// Act
			entity = repository.Create(entity);

			// Assert
			Assert.IsNotNull(entity);
			Assert.AreNotEqual(0, entity.Id);
		}

		[Test]
		public void ShouldCreateRoomHoursEntries()
		{
			// Arrange
			RoomHoursRepository repository = new RoomHoursRepository(ConfigSettings.MySqlDatabaseConnectionName);
			List<RoomHoursEntity> entities = HoursData.GetRoomHoursEntriesForInsert(_hourType, _costCentre);

			// Act
			entities = repository.Create(entities);

			// Assert
			Assert.IsNotNull(entities);
			Assert.AreEqual(2, entities.Count);
		}

		[Test]
		public void ShouldReadRoomHoursEntryWithId()
		{
			// Arrange
			RoomHoursRepository repository = new RoomHoursRepository(ConfigSettings.MySqlDatabaseConnectionName);
			RoomHoursEntity entity = HoursData.GetRoomHoursEntryForInsert(_hourType, _costCentre);

			// Act
			entity = repository.Create(entity);

			// Act
			var actual = repository.Read(entity.Id);

			// Assert
			Assert.AreEqual(entity.OrderNumber, actual.OrderNumber);
		}

		[Test]
		public void ShouldUpdateRoomHoursEntry()
		{
			// Arrange
			RoomHoursRepository repository = new RoomHoursRepository(ConfigSettings.MySqlDatabaseConnectionName);
			RoomHoursEntity entity = HoursData.GetRoomHoursEntryForInsert(_hourType, _costCentre);
			entity = repository.Create(entity);
			entity.OrderNumber = 666;

			// Act
			RoomHoursEntity actual = repository.Update(entity);

			// Assert
			Assert.AreEqual(entity.OrderNumber, actual.OrderNumber);
		}

		[Test]
		public void ShouldDeleteRoomHoursEntry()
		{
			// Arrange
			RoomHoursRepository repository = new RoomHoursRepository(ConfigSettings.MySqlDatabaseConnectionName);
			RoomHoursEntity entity = HoursData.GetRoomHoursEntryForInsert(_hourType, _costCentre);
			entity = repository.Create(entity);

			// Act
			RoomHoursEntity actual = repository.Delete(entity);

			// Assert
			Assert.AreNotEqual(actual.DeletedDate, DateTime.MinValue);
		}

		#endregion Test Methods
	}
}
