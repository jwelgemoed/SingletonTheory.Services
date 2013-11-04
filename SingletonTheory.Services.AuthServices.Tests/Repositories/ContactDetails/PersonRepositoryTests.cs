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
	public class PersonRepositoryTests
	{
		private PersonRepository _repository;

		#region Setup and Teardown

		[SetUp]
		public void Init()
		{
			_repository = new PersonRepository(ConfigSettings.MySqlDatabaseConnectionName);
		}

		[TearDown]
		public void Dispose()
		{
			try
			{
				_repository.ClearCollection();
				ContactDetailsHelpers.ClearEntity();
				ContactDetailsHelpers.ClearOccupationName();
				ContactDetailsHelpers.ClearTitle();
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
		public void ShouldCreatePerson()
		{
			// Arrange
			PersonEntity entity = PersonData.GetItemForInsert();
			_repository.ClearCollection();

			// Act
			entity = _repository.Create(entity);

			// Assert
			Assert.IsNotNull(entity);
			Assert.AreNotEqual(0, entity.Id);
		}

		[Test]
		public void ShouldCreatePersons()
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
		public void ShouldReadPersonWithId()
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
		public void ShouldUpdatePerson()
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
		public void ShouldDeletePerson()
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
