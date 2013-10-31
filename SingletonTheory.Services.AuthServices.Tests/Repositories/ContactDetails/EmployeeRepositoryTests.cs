using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SingletonTheory.Services.AuthServices.Config;
using SingletonTheory.Services.AuthServices.Entities.ContactDetails;
using SingletonTheory.Services.AuthServices.Repositories.ContactDetails;
using SingletonTheory.Services.AuthServices.Tests.Data;

namespace SingletonTheory.Services.AuthServices.Tests.Repositories.ContactDetails
{
	public class EmployeeRepositoryTests
	{
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
		public void ShouldCreateContact()
		{
			// Arrange
			EmployeeRepository repository = new EmployeeRepository(ConfigSettings.MySqlDatabaseConnectionName);
			EmployeeEntity entity = EmployeeData.GetItemForInsert();
			repository.ClearCollection();

			// Act
			entity = repository.Create(entity);

			// Assert
			Assert.IsNotNull(entity);
			Assert.AreNotEqual(0, entity.Id);
		}

		[Test]
		public void ShouldCreateContacts()
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
		public void ShouldReadContactWithId()
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
		public void ShouldUpdateContact()
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
		public void ShouldDeleteContact()
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
