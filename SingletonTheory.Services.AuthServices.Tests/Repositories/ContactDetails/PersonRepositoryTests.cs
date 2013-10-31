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
	public class PersonRepositoryTests
	{
		#region Test Methods

		[Test]
		public void ShouldThrowArgumentNullExceptionOnConstructor()
		{
			// Act
			try
			{
				PersonRepository repository = new PersonRepository(null);
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
			PersonRepository repository = new PersonRepository(ConfigSettings.MySqlDatabaseConnectionName);
			PersonEntity entity = PersonData.GetItemForInsert();
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
			PersonRepository repository = new PersonRepository(ConfigSettings.MySqlDatabaseConnectionName);
			List<PersonEntity> entities = PersonData.GetItemsForInsert();
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
			PersonRepository repository = new PersonRepository(ConfigSettings.MySqlDatabaseConnectionName);
			PersonEntity entity = PersonData.GetItemForInsert();
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
			PersonRepository repository = new PersonRepository(ConfigSettings.MySqlDatabaseConnectionName);
			PersonEntity entity = PersonData.GetItemForInsert();
			repository.ClearCollection();
			entity = repository.Create(entity);
			entity.Nationality = "NL";

			// Act
			PersonEntity actual = repository.Update(entity);

			// Assert
			Assert.AreEqual(entity.Nationality, actual.Nationality);
		}

		[Test]
		public void ShouldDeleteContact()
		{
			// Arrange
			PersonRepository repository = new PersonRepository(ConfigSettings.MySqlDatabaseConnectionName);
			PersonEntity entity = PersonData.GetItemForInsert();
			repository.ClearCollection();
			entity = repository.Create(entity);

			// Act
			PersonEntity actual = repository.Delete(entity);

			// Assert
			Assert.AreEqual(entity.EntityId, actual.EntityId);
		}

		#endregion Test Methods
	}
}
