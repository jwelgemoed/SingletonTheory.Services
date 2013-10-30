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
using SingletonTheory.Services.AuthServices.Tests.Helpers;

namespace SingletonTheory.Services.AuthServices.Tests.Repositories.ContactDetails
{
	public class AddressRepositoryTests
	{
		#region Test Methods

		[Test]
		public void ShouldThrowArgumentNullExceptionOnConstructor()
		{
			// Act
			try
			{
				AddressRepository repository = new AddressRepository(null);
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
			AddressRepository repository = new AddressRepository(ConfigSettings.MySqlDatabaseConnectionName);
			AddressEntity entity = AddressData.GetAddressForInsert();
			repository.ClearCollection();
			AddressTypeEntity addressType = ContactDetailsHelpers.CreateAddressType();
			// Act
			entity.AddressTypeId = addressType.Id;
			entity = repository.Create(entity);

			// Assert
			Assert.IsNotNull(entity);
			Assert.AreNotEqual(0, entity.Id);
		}

		[Test]
		public void ShouldCreateAddresses()
		{
			// Arrange
			AddressRepository repository = new AddressRepository(ConfigSettings.MySqlDatabaseConnectionName);
			List<AddressEntity> entities = AddressData.GetAddressesForInsert();
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
			AddressRepository repository = new AddressRepository(ConfigSettings.MySqlDatabaseConnectionName);
			AddressEntity entity = AddressData.GetAddressForInsert();
			repository.ClearCollection();

			// Act
			entity = repository.Create(entity);

			// Act
			var actual = repository.Read(entity.Id);

			// Assert
			Assert.AreEqual(entity.Street, actual.Street);
		}

		[Test]
		public void ShouldUpdateAddress()
		{
			// Arrange
			AddressRepository repository = new AddressRepository(ConfigSettings.MySqlDatabaseConnectionName);
			AddressEntity entity = AddressData.GetAddressForInsert();
			repository.ClearCollection();
			entity = repository.Create(entity);
			entity.Street = "DeHoop";

			// Act
			AddressEntity actual = repository.Update(entity);

			// Assert
			Assert.AreEqual(entity.Street, actual.Street);
		}

		[Test]
		public void ShouldDeleteAddress()
		{
			// Arrange
			AddressRepository repository = new AddressRepository(ConfigSettings.MySqlDatabaseConnectionName);
			AddressEntity entity = AddressData.GetAddressForInsert();
			repository.ClearCollection();
			entity = repository.Create(entity);

			// Act
			AddressEntity actual = repository.Delete(entity);

			// Assert
			Assert.AreEqual(entity.Street, actual.Street);
		}

		#endregion Test Methods
	}
}
