using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SingletonTheory.Services.AuthServices.Config;
using SingletonTheory.Services.AuthServices.Entities.ContactDetails;
using SingletonTheory.Services.AuthServices.Helpers.Data;
using SingletonTheory.Services.AuthServices.Repositories.ContactDetails;
using SingletonTheory.Services.AuthServices.Tests.Data;

namespace SingletonTheory.Services.AuthServices.Tests.Repositories.ContactDetails
{
	public class ContactTypeRepositoryTests
	{
		#region Test Methods

		[Test]
		public void ShouldThrowArgumentNullExceptionOnConstructor()
		{
			// Act
			try
			{
				ContactTypeRepository repository = new ContactTypeRepository(null);
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
			ContactTypeRepository repository = new ContactTypeRepository(ConfigSettings.MySqlDatabaseConnectionName);
			ContactTypeEntity entity = ContactTypeData.GetItemForInsert();
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
			ContactTypeRepository repository = new ContactTypeRepository(ConfigSettings.MySqlDatabaseConnectionName);
			List<ContactTypeEntity> entities = ContactTypeData.GetItemsForInsert();
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
			ContactTypeRepository repository = new ContactTypeRepository(ConfigSettings.MySqlDatabaseConnectionName);
			ContactTypeEntity entity = ContactTypeData.GetItemForInsert();
			repository.ClearCollection();

			// Act
			entity = repository.Create(entity);

			// Act
			var actual = repository.Read(entity.Id);

			// Assert
			Assert.AreEqual(entity.Description, actual.Description);
		}

		[Test]
		public void ShouldUpdateContact()
		{
			// Arrange
			ContactTypeRepository repository = new ContactTypeRepository(ConfigSettings.MySqlDatabaseConnectionName);
			ContactTypeEntity entity = ContactTypeData.GetItemForInsert();
			repository.ClearCollection();
			entity = repository.Create(entity);
			entity.Description = "Employee";

			// Act
			ContactTypeEntity actual = repository.Update(entity);

			// Assert
			Assert.AreEqual(entity.Description, actual.Description);
		}

		[Test]
		public void ShouldDeleteContact()
		{
			// Arrange
			ContactTypeRepository repository = new ContactTypeRepository(ConfigSettings.MySqlDatabaseConnectionName);
			ContactTypeEntity entity = ContactTypeData.GetItemForInsert();
			repository.ClearCollection();
			entity = repository.Create(entity);

			// Act
			ContactTypeEntity actual = repository.Delete(entity);

			// Assert
			Assert.AreEqual(entity.Description, actual.Description);
		}

		#endregion Test Methods
	}
}