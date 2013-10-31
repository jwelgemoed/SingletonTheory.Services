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
	public class EntityRelationshipRepositoryTests
	{
		#region Test Methods

		[Test]
		public void ShouldThrowArgumentNullExceptionOnConstructor()
		{
			// Act
			try
			{
				EntityRelationshipRepository repository = new EntityRelationshipRepository(null);
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
			EntityRelationshipRepository repository = new EntityRelationshipRepository(ConfigSettings.MySqlDatabaseConnectionName);
			EntityRelationshipEntity entity = EntityRelationshipData.GetItemForInsert();
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
			EntityRelationshipRepository repository = new EntityRelationshipRepository(ConfigSettings.MySqlDatabaseConnectionName);
			List<EntityRelationshipEntity> entities = EntityRelationshipData.GetItemsForInsert();
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
			EntityRelationshipRepository repository = new EntityRelationshipRepository(ConfigSettings.MySqlDatabaseConnectionName);
			EntityRelationshipEntity entity = EntityRelationshipData.GetItemForInsert();
			repository.ClearCollection();

			// Act
			entity = repository.Create(entity);

			// Act
			var actual = repository.Read(entity.Id);

			// Assert
			Assert.AreEqual(entity.Entity1Id, actual.Entity1Id);
		}

		[Test]
		public void ShouldUpdateContact()
		{
			// Arrange
			EntityRelationshipRepository repository = new EntityRelationshipRepository(ConfigSettings.MySqlDatabaseConnectionName);
			EntityRelationshipEntity entity = EntityRelationshipData.GetItemForInsert();
			repository.ClearCollection();
			entity = repository.Create(entity);
			entity.Preffered = true;

			// Act
			EntityRelationshipEntity actual = repository.Update(entity);

			// Assert
			Assert.AreEqual(entity.Preffered, actual.Preffered);
		}

		[Test]
		public void ShouldDeleteContact()
		{
			// Arrange
			EntityRelationshipRepository repository = new EntityRelationshipRepository(ConfigSettings.MySqlDatabaseConnectionName);
			EntityRelationshipEntity entity = EntityRelationshipData.GetItemForInsert();
			repository.ClearCollection();
			entity = repository.Create(entity);

			// Act
			EntityRelationshipEntity actual = repository.Delete(entity);

			// Assert
			Assert.AreEqual(entity.Entity1Id, actual.Entity1Id);
		}

		#endregion Test Methods
	}
}
