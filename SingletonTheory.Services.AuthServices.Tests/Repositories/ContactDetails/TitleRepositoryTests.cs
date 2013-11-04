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
	public class TitleRepositoryTests
	{
		#region Test Methods

		[Test]
		public void ShouldThrowArgumentNullExceptionOnConstructor()
		{
			// Act
			try
			{
				TitleRepository repository = new TitleRepository(null);
				Assert.Fail("This should not happen");
			}
			catch (Exception ex)
			{
				// Arrange
				Assert.IsInstanceOf<ArgumentNullException>(ex);
			}
		}

		[Test]
		public void ShouldCreateTitle()
		{
			// Arrange
			TitleRepository repository = new TitleRepository(ConfigSettings.MySqlDatabaseConnectionName);
			TitleEntity entity = TitleData.GetItemForInsert();
			repository.ClearCollection();

			// Act
			entity = repository.Create(entity);

			// Assert
			Assert.IsNotNull(entity);
			Assert.AreNotEqual(0, entity.Id);
		}

		[Test]
		public void ShouldCreateTitles()
		{
			// Arrange
			TitleRepository repository = new TitleRepository(ConfigSettings.MySqlDatabaseConnectionName);
			List<TitleEntity> entities = TitleData.GetItemsForInsert();
			repository.ClearCollection();

			// Act
			entities = repository.Create(entities);

			// Assert
			Assert.IsNotNull(entities);
			Assert.AreEqual(2, entities.Count);
		}

		[Test]
		public void ShouldReadTitleWithId()
		{
			// Arrange
			TitleRepository repository = new TitleRepository(ConfigSettings.MySqlDatabaseConnectionName);
			TitleEntity entity = TitleData.GetItemForInsert();
			repository.ClearCollection();

			// Act
			entity = repository.Create(entity);

			// Act
			var actual = repository.Read(entity.Id);

			// Assert
			Assert.AreEqual(entity.Description, actual.Description);
		}

		[Test]
		public void ShouldUpdateTitle()
		{
			// Arrange
			TitleRepository repository = new TitleRepository(ConfigSettings.MySqlDatabaseConnectionName);
			TitleEntity entity = TitleData.GetItemForInsert();
			repository.ClearCollection();
			entity = repository.Create(entity);
			entity.Description = "Dhr";

			// Act
			TitleEntity actual = repository.Update(entity);

			// Assert
			Assert.AreEqual(entity.Description, actual.Description);
		}

		[Test]
		public void ShouldDeleteTitle()
		{
			// Arrange
			TitleRepository repository = new TitleRepository(ConfigSettings.MySqlDatabaseConnectionName);
			TitleEntity entity = TitleData.GetItemForInsert();
			repository.ClearCollection();
			entity = repository.Create(entity);

			// Act
			TitleEntity actual = repository.Delete(entity);

			// Assert
			Assert.AreEqual(entity.Description, actual.Description);
		}

		#endregion Test Methods
	}
}
