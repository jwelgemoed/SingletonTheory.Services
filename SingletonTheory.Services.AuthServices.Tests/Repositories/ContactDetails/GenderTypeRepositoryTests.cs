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
	public class GenderTypeRepositoryTests
	{
		#region Test Methods

		[Test]
		public void ShouldThrowArgumentNullExceptionOnConstructor()
		{
			// Act
			try
			{
				GenderTypeRepository repository = new GenderTypeRepository(null);
				Assert.Fail("This should not happen");
			}
			catch (Exception ex)
			{
				// Arrange
				Assert.IsInstanceOf<ArgumentNullException>(ex);
			}
		}

		[Test]
		public void ShouldCreateGenderType()
		{
			// Arrange
			GenderTypeRepository repository = new GenderTypeRepository(ConfigSettings.MySqlDatabaseConnectionName);
			GenderTypeEntity entity = GenderTypeData.GetItemForInsert();
			repository.ClearCollection();

			// Act
			entity = repository.Create(entity);

			// Assert
			Assert.IsNotNull(entity);
			Assert.AreNotEqual(0, entity.Id);
		}

		[Test]
		public void ShouldCreateGenderTypes()
		{
			// Arrange
			GenderTypeRepository repository = new GenderTypeRepository(ConfigSettings.MySqlDatabaseConnectionName);
			List<GenderTypeEntity> entities = GenderTypeData.GetItemsForInsert();
			repository.ClearCollection();

			// Act
			entities = repository.Create(entities);

			// Assert
			Assert.IsNotNull(entities);
			Assert.AreEqual(2, entities.Count);
		}

		[Test]
		public void ShouldReadGenderTypeWithId()
		{
			// Arrange
			GenderTypeRepository repository = new GenderTypeRepository(ConfigSettings.MySqlDatabaseConnectionName);
			GenderTypeEntity entity = GenderTypeData.GetItemForInsert();
			repository.ClearCollection();

			// Act
			entity = repository.Create(entity);

			// Act
			var actual = repository.Read(entity.Id);

			// Assert
			Assert.AreEqual(entity.Description, actual.Description);
		}

		[Test]
		public void ShouldUpdateGenderType()
		{
			// Arrange
			GenderTypeRepository repository = new GenderTypeRepository(ConfigSettings.MySqlDatabaseConnectionName);
			GenderTypeEntity entity = GenderTypeData.GetItemForInsert();
			repository.ClearCollection();
			entity = repository.Create(entity);
			entity.Description = "Male";

			// Act
			GenderTypeEntity actual = repository.Update(entity);

			// Assert
			Assert.AreEqual(entity.Description, actual.Description);
		}

		[Test]
		public void ShouldDeleteGenderType()
		{
			// Arrange
			GenderTypeRepository repository = new GenderTypeRepository(ConfigSettings.MySqlDatabaseConnectionName);
			GenderTypeEntity entity = GenderTypeData.GetItemForInsert();
			repository.ClearCollection();
			entity = repository.Create(entity);

			// Act
			GenderTypeEntity actual = repository.Delete(entity);

			// Assert
			Assert.AreEqual(entity.Description, actual.Description);
		}

		#endregion Test Methods
	}
}
