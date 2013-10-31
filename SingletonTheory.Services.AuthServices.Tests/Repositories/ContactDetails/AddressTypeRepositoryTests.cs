using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ServiceStack.Text;
using SingletonTheory.Services.AuthServices.Config;
using SingletonTheory.Services.AuthServices.Entities.ContactDetails;
using SingletonTheory.Services.AuthServices.Repositories.ContactDetails;
using SingletonTheory.Services.AuthServices.Tests.Data;
using SingletonTheory.Services.AuthServices.Tests.Helpers;

namespace SingletonTheory.Services.AuthServices.Tests.Repositories.ContactDetails
{
	public class AddressTypeRepositoryTests
	{
		#region Test Methods

		[Test]
		public void ShouldThrowArgumentNullExceptionOnConstructor()
		{
			// Act
			try
			{
				AddressTypeRepository repository = new AddressTypeRepository(null);
				Assert.Fail("This should not happen");
			}
			catch (Exception ex)
			{
				// Arrange
				Assert.IsInstanceOf<ArgumentNullException>(ex);
			}
		}

		[Test]
		public void ShouldCreateAddress()
		{
			// Arrange
			AddressTypeRepository repository = new AddressTypeRepository(ConfigSettings.MySqlDatabaseConnectionName);
			AddressTypeEntity entity = AddressTypeData.GetItemForInsert();
			repository.ClearCollection();

			// Act
			entity = repository.Create(entity);

			// Assert
			Assert.IsNotNull(entity);
			Assert.AreNotEqual(0, entity.Id);
		}

		[Test]
		public void ShouldCreateAddresses()
		{
			// Arrange
			AddressTypeRepository repository = new AddressTypeRepository(ConfigSettings.MySqlDatabaseConnectionName);
			List<AddressTypeEntity> entities = AddressTypeData.GetItemsForInsert();
			repository.ClearCollection();

			// Act
			entities = repository.Create(entities);

			// Assert
			Assert.IsNotNull(entities);
			Assert.AreEqual(2, entities.Count);
		}

		[Test]
		public void ShouldReadAddressWithId()
		{
			// Arrange
			AddressTypeRepository repository = new AddressTypeRepository(ConfigSettings.MySqlDatabaseConnectionName);
			AddressTypeEntity entity = AddressTypeData.GetItemForInsert();
			repository.ClearCollection();

			// Act
			entity = repository.Create(entity);

			// Act
			var actual = repository.Read(entity.Id);

			// Assert
			Assert.AreEqual(entity.Description, actual.Description);
		}

		[Test]
		public void ShouldUpdateAddress()
		{
			// Arrange
			AddressTypeRepository repository = new AddressTypeRepository(ConfigSettings.MySqlDatabaseConnectionName);
			AddressTypeEntity entity = AddressTypeData.GetItemForInsert();
			repository.ClearCollection();
			entity = repository.Create(entity);
			entity.Description = "Postal";

			// Act
			AddressTypeEntity actual = repository.Update(entity);

			// Assert
			Assert.AreEqual(entity.Description, actual.Description);
		}

		[Test]
		public void ShouldDeleteAddress()
		{
			// Arrange
			AddressTypeRepository repository = new AddressTypeRepository(ConfigSettings.MySqlDatabaseConnectionName);
			AddressTypeEntity entity = AddressTypeData.GetItemForInsert();
			repository.ClearCollection();
			entity = repository.Create(entity);

			// Act
			AddressTypeEntity actual = repository.Delete(entity);

			// Assert
			Assert.AreEqual(entity.Description, actual.Description);
		}

		#endregion Test Methods
	}
}
