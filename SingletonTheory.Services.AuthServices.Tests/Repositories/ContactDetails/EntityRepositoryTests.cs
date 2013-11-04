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
	public class EntityRepositoryTests
	{
		private EntityRepository _repository;

		#region Setup and Teardown

		[SetUp]
		public void Init()
		{
			_repository = new EntityRepository(ConfigSettings.MySqlDatabaseConnectionName);
		}

		[TearDown]
		public void Dispose()
		{
			try
			{
				_repository.ClearCollection();
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
		public void ShouldCreateEntity()
		{
			// Arrange
			EntityEntity entity = EntityData.GetItemForInsert();

			// Act
			entity = _repository.Create(entity);

			// Assert
			Assert.IsNotNull(entity);
			Assert.AreNotEqual(0, entity.Id);
		}

		[Test]
		public void ShouldCreateEntities()
		{
			// Arrange
			List<EntityEntity> entities = EntityData.GetItemsForInsert();

			// Act
			entities = _repository.Create(entities);

			// Assert
			Assert.IsNotNull(entities);
			Assert.AreEqual(2, entities.Count);
		}

		[Test]
		public void ShouldReadEntityWithId()
		{
			// Arrange
			EntityEntity entity = EntityData.GetItemForInsert();

			// Act
			entity = _repository.Create(entity);

			// Act
			var actual = _repository.Read(entity.Id);

			// Assert
			Assert.AreEqual(entity.Name, actual.Name);
		}

		[Test]
		public void ShouldUpdateEntity()
		{
			// Arrange
			EntityEntity entity = EntityData.GetItemForInsert();
			entity = _repository.Create(entity);
			entity.Name = "Name1";

			// Act
			EntityEntity actual = _repository.Update(entity);

			// Assert
			Assert.AreEqual(entity.Name, actual.Name);
		}

		[Test]
		public void ShouldDeleteEntity()
		{
			// Arrange
			EntityEntity entity = EntityData.GetItemForInsert();
			entity = _repository.Create(entity);

			// Act
			EntityEntity actual = _repository.Delete(entity);

			// Assert
			Assert.AreEqual(entity.Name, actual.Name);
		}

		#endregion Test Methods
	}
}
