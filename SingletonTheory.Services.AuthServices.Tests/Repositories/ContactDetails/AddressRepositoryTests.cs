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
		private AddressRepository _addressRepository;

		#region Setup and Teardown

		[SetUp]
		public void Init()
		{
			_addressRepository = new AddressRepository(ConfigSettings.MySqlDatabaseConnectionName);
		}

		[TearDown]
		public void Dispose()
		{
			try
			{
				_addressRepository.ClearCollection();
				ContactDetailsHelpers.ClearAddressType();
				ContactDetailsHelpers.ClearEntity();
				ContactDetailsHelpers.ClearEntityType();
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
			AddressEntity entity = AddressData.GetItemForInsert();

			// Act
			entity = _addressRepository.Create(entity);

			// Assert
			Assert.IsNotNull(entity);
			Assert.AreNotEqual(0, entity.Id);
		}

		[Test]
		public void ShouldCreateAddresses()
		{
			// Arrange
			List<AddressEntity> entities = AddressData.GetItemsForInsert();
		
			// Act
			entities = _addressRepository.Create(entities);

			// Assert
			Assert.IsNotNull(entities);
			Assert.AreEqual(2, entities.Count);
		}

		[Test]
		public void ShouldReadAddressWithId()
		{
			// Arrange
			AddressEntity entity = AddressData.GetItemForInsert();
		
			// Act
			entity = _addressRepository.Create(entity);

			// Act
			var actual = _addressRepository.Read(entity.Id);

			// Assert
			Assert.AreEqual(entity.Street, actual.Street);
		}

		[Test]
		public void ShouldUpdateAddress()
		{
			// Arrange
			AddressEntity entity = AddressData.GetItemForInsert();

			entity = _addressRepository.Create(entity);
			entity.Street = "DeHoop";

			// Act
			AddressEntity actual = _addressRepository.Update(entity);

			// Assert
			Assert.AreEqual(entity.Street, actual.Street);
		}

		[Test]
		public void ShouldDeleteAddress()
		{
			// Arrange
			AddressEntity entity = AddressData.GetItemForInsert();

			entity = _addressRepository.Create(entity);

			// Act
			AddressEntity actual = _addressRepository.Delete(entity);

			// Assert
			Assert.AreEqual(entity.Street, actual.Street);
		}

		#endregion Test Methods
	}
}
