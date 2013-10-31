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
	public class EntityRepositoryTests
	{
		#region Test Methods

		[Test]
		public void ShouldThrowArgumentNullExceptionOnConstructor()
		{
			// Act
			try
			{
				EntityRepository repository = new EntityRepository(null);
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
			EntityRepository repository = new EntityRepository(ConfigSettings.MySqlDatabaseConnectionName);
			EntityEntity entity = EntityData.GetItemForInsert();
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
			EntityRepository repository = new EntityRepository(ConfigSettings.MySqlDatabaseConnectionName);
			List<EntityEntity> entities = EntityData.GetItemsForInsert();
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
			EntityRepository repository = new EntityRepository(ConfigSettings.MySqlDatabaseConnectionName);
			EntityEntity entity = EntityData.GetItemForInsert();
			repository.ClearCollection();

			// Act
			entity = repository.Create(entity);

			// Act
			var actual = repository.Read(entity.Id);

			// Assert
			Assert.AreEqual(entity.Name, actual.Name);
		}

		[Test]
		public void ShouldUpdateContact()
		{
			// Arrange
			EntityRepository repository = new EntityRepository(ConfigSettings.MySqlDatabaseConnectionName);
			EntityEntity entity = EntityData.GetItemForInsert();
			repository.ClearCollection();
			entity = repository.Create(entity);
			entity.Name = "Name1";

			// Act
			EntityEntity actual = repository.Update(entity);

			// Assert
			Assert.AreEqual(entity.Name, actual.Name);
		}

		[Test]
		public void ShouldDeleteContact()
		{
			// Arrange
			EntityRepository repository = new EntityRepository(ConfigSettings.MySqlDatabaseConnectionName);
			EntityEntity entity = EntityData.GetItemForInsert();
			repository.ClearCollection();
			entity = repository.Create(entity);

			// Act
			EntityEntity actual = repository.Delete(entity);

			// Assert
			Assert.AreEqual(entity.Name, actual.Name);
		}

		#endregion Test Methods
	}
}
