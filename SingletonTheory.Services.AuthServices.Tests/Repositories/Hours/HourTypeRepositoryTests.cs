using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using NUnit.Framework;
using SingletonTheory.Services.AuthServices.Config;
using SingletonTheory.Services.AuthServices.Entities.Hours;
using SingletonTheory.Services.AuthServices.Repositories.Hours;
using SingletonTheory.Services.AuthServices.Tests.Data;

namespace SingletonTheory.Services.AuthServices.Tests.Repositories.Hours
{
	public class HourTypeRepositoryTests
	{
		#region Test Methods

		[Test]
		public void ShouldThrowArgumentNullExceptionOnConstructor()
		{
			// Act
			try
			{
				HourTypeRepository repository = new HourTypeRepository(null);
				Assert.Fail("This should not happen");
			}
			catch (Exception ex)
			{
				// Arrange
				Assert.IsInstanceOf<ArgumentNullException>(ex);
			}
		}

		[Test]
		public void ShouldCreateHourType()
		{
			// Arrange
			HourTypeRepository repository = new HourTypeRepository(ConfigSettings.MySqlDatabaseConnectionName);
			HourTypeEntity entity = HoursData.GetHourTypeForInsert();
			repository.ClearCollection();
			// Act
			entity = repository.Create(entity);

			// Assert
			Assert.IsNotNull(entity);
			Assert.AreNotEqual(0, entity.Id);
		}

		[Test]
		public void ShouldCreateHourTypes()
		{
			// Arrange
			HourTypeRepository repository = new HourTypeRepository(ConfigSettings.MySqlDatabaseConnectionName);
			List<HourTypeEntity> entities = HoursData.GetHourTypesForInsert();
			repository.ClearCollection();

			// Act
			entities = repository.Create(entities);

			// Assert
			Assert.IsNotNull(entities);
			Assert.AreEqual(2, entities.Count);
		}

		[Test]
		public void ShouldReadHourTypeWithId()
		{
			// Arrange
			HourTypeRepository repository = new HourTypeRepository(ConfigSettings.MySqlDatabaseConnectionName);
			HourTypeEntity entity = HoursData.GetHourTypeForInsert();
			repository.ClearCollection();

			// Act
			entity = repository.Create(entity);

			// Act
			var actual = repository.Read(entity.Id);

			// Assert
			Assert.AreEqual(entity.Description, actual.Description);
		}

		[Test]
		public void ShouldUpdateHourType()
		{
			// Arrange
			HourTypeRepository repository = new HourTypeRepository(ConfigSettings.MySqlDatabaseConnectionName);
			HourTypeEntity entity = HoursData.GetHourTypeForInsert();
			repository.ClearCollection();
			entity = repository.Create(entity);
			entity.Description = "Unplanned";

			// Act
			HourTypeEntity actual = repository.Update(entity);

			// Assert
			Assert.AreEqual(entity.Description, actual.Description);
		}

		[Test]
		public void ShouldDeleteHourType()
		{
			// Arrange
			HourTypeRepository repository = new HourTypeRepository(ConfigSettings.MySqlDatabaseConnectionName);
			HourTypeEntity entity = HoursData.GetHourTypeForInsert();
			repository.ClearCollection();
			entity = repository.Create(entity);

			// Act
			HourTypeEntity actual = repository.Delete(entity);

			// Assert
			Assert.AreNotEqual(actual.DeletedDate, DateTime.MinValue);
		}

		[Test]
		public void ShouldNotInsertDuplicateHourType()
		{
			// Arrange
			HourTypeRepository repository = new HourTypeRepository(ConfigSettings.MySqlDatabaseConnectionName);
			HourTypeEntity entity = HoursData.GetHourTypeForInsert();
			repository.ClearCollection();
			entity = repository.Create(entity);
			try
			{
			// Act
			  entity = repository.Create(entity);
				Assert.Fail("This should not happen");
			}
			catch (Exception ex)
			{
				// Arrange
				Assert.IsInstanceOf<MySqlException>(ex);
			}
			// Assert
		}

		#endregion Test Methods
	}
}
