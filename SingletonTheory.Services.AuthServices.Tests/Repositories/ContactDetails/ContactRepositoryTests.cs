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
	public class ContactRepositoryTests
	{
		#region Test Methods

		[Test]
		public void ShouldThrowArgumentNullExceptionOnConstructor()
		{
			// Act
			try
			{
				ContactRepository repository = new ContactRepository(null);
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
			ContactRepository repository = new ContactRepository(ConfigSettings.MySqlDatabaseConnectionName);
			ContactEntity entity = ContactData.GetItemForInsert();
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
			ContactRepository repository = new ContactRepository(ConfigSettings.MySqlDatabaseConnectionName);
			List<ContactEntity> entities = ContactData.GetItemsForInsert();
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
			ContactRepository repository = new ContactRepository(ConfigSettings.MySqlDatabaseConnectionName);
			ContactEntity entity = ContactData.GetItemForInsert();
			repository.ClearCollection();

			// Act
			entity = repository.Create(entity);

			// Act
			var actual = repository.Read(entity.Id);

			// Assert
			Assert.AreEqual(entity.Value, actual.Value);
		}

		[Test]
		public void ShouldUpdateContact()
		{
			// Arrange
			ContactRepository repository = new ContactRepository(ConfigSettings.MySqlDatabaseConnectionName);
			ContactEntity entity = ContactData.GetItemForInsert();
			repository.ClearCollection();
			entity = repository.Create(entity);
			entity.Value = "Contact";

			// Act
			ContactEntity actual = repository.Update(entity);

			// Assert
			Assert.AreEqual(entity.Value, actual.Value);
		}

		[Test]
		public void ShouldDeleteContact()
		{
			// Arrange
			ContactRepository repository = new ContactRepository(ConfigSettings.MySqlDatabaseConnectionName);
			ContactEntity entity = ContactData.GetItemForInsert();
			repository.ClearCollection();
			entity = repository.Create(entity);

			// Act
			ContactEntity actual = repository.Delete(entity);

			// Assert
			Assert.AreEqual(entity.Value, actual.Value);
		}

		#endregion Test Methods
	}
}