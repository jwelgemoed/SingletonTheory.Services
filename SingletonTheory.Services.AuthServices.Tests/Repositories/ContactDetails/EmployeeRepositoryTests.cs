using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SingletonTheory.Services.AuthServices.Config;
using SingletonTheory.Services.AuthServices.Entities.ContactDetails;
using SingletonTheory.Services.AuthServices.Helpers.Data;
using SingletonTheory.Services.AuthServices.Helpers.Helpers;
using SingletonTheory.Services.AuthServices.Repositories.ContactDetails;
using SingletonTheory.Services.AuthServices.Tests.Data;
using SingletonTheory.Services.AuthServices.Tests.Helpers;

namespace SingletonTheory.Services.AuthServices.Tests.Repositories.ContactDetails
{
	public class EmployeeRepositoryTests
	{
		private EmployeeRepository _repository;

		#region Setup and Teardown

		[SetUp]
		public void Init()
		{
			_repository = new EmployeeRepository(ConfigSettings.MySqlDatabaseConnectionName);
		}

		[TearDown]
		public void Dispose()
		{
			try
			{
				_repository.ClearCollection();
				ContactDetailsHelpers.ClearEntity();
				ContactDetailsHelpers.ClearPerson();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}

		#endregion Setup and Teardown

		#region Test Methods

		[Test]
		public void ShouldThrowArgumentNullExceptionOnConstructor()
		{
			// Act
			try
			{
				EmployeeRepository repository = new EmployeeRepository(null);
				Assert.Fail("This should not happen");
			}
			catch (Exception ex)
			{
				// Arrange
				Assert.IsInstanceOf<ArgumentNullException>(ex);
			}
		}

		[Test]
		public void ShouldCreateEmployee()
		{
			// Arrange
			EmployeeEntity entity = EmployeeData.GetItemForInsert();
			_repository.ClearCollection();

			// Act
			entity = _repository.Create(entity);

			// Assert
			Assert.IsNotNull(entity);
			Assert.AreNotEqual(0, entity.Id);
		}

		[Test]
		public void ShouldCreateEmployees()
		{
			// Arrange
			EmployeeRepository repository = new EmployeeRepository(ConfigSettings.MySqlDatabaseConnectionName);
			List<EmployeeEntity> entities = EmployeeData.GetItemsForInsert();
			repository.ClearCollection();

			// Act
			entities = repository.Create(entities);

			// Assert
			Assert.IsNotNull(entities);
			Assert.AreEqual(2, entities.Count);
		}

		[Test]
		public void ShouldReadEmployeeWithId()
		{
			// Arrange
			EmployeeRepository repository = new EmployeeRepository(ConfigSettings.MySqlDatabaseConnectionName);
			EmployeeEntity entity = EmployeeData.GetItemForInsert();
			repository.ClearCollection();

			// Act
			entity = repository.Create(entity);

			// Act
			var actual = repository.Read(entity.Id);

			// Assert
			Assert.AreEqual(entity.EntityId, actual.EntityId);
		}

		[Test]
		public void ShouldUpdateEmployee()
		{
			// Arrange
			EmployeeRepository repository = new EmployeeRepository(ConfigSettings.MySqlDatabaseConnectionName);
			EmployeeEntity entity = EmployeeData.GetItemForInsert();
			repository.ClearCollection();
			entity = repository.Create(entity);
			entity.HasVehicle = true;

			// Act
			EmployeeEntity actual = repository.Update(entity);

			// Assert
			Assert.AreEqual(entity.HasVehicle, actual.HasVehicle);
		}

		[Test]
		public void ShouldDeleteEmployee()
		{
			// Arrange
			EmployeeRepository repository = new EmployeeRepository(ConfigSettings.MySqlDatabaseConnectionName);
			EmployeeEntity entity = EmployeeData.GetItemForInsert();
			repository.ClearCollection();
			entity = repository.Create(entity);

			// Act
			EmployeeEntity actual = repository.Delete(entity);

			// Assert
			Assert.AreEqual(entity.EntityId, actual.EntityId);
		}

		#endregion Test Methods
	}
}
