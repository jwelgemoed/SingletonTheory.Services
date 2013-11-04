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
	public class OccupationNameRepositoryTests
	{
		private OccupationNameRepository _repository;

		#region Setup and Teardown

		[SetUp]
		public void Init()
		{
			_repository = new OccupationNameRepository(ConfigSettings.MySqlDatabaseConnectionName);
		}

		[TearDown]
		public void Dispose()
		{
			try
			{
				_repository.ClearCollection();
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
				OccupationNameRepository repository = new OccupationNameRepository(null);
				Assert.Fail("This should not happen");
			}
			catch (Exception ex)
			{
				// Arrange
				Assert.IsInstanceOf<ArgumentNullException>(ex);
			}
		}

		[Test]
		public void ShouldCreateOccupationName()
		{
			// Arrange
			OccupationNameEntity entity = OccupationNameData.GetItemForInsert();

			// Act
			entity = _repository.Create(entity);

			// Assert
			Assert.IsNotNull(entity);
			Assert.AreNotEqual(0, entity.Id);
		}

		[Test]
		public void ShouldCreateOccupationNames()
		{
			// Arrange
			List<OccupationNameEntity> entities = OccupationNameData.GetItemsForInsert();

			// Act
			entities = _repository.Create(entities);

			// Assert
			Assert.IsNotNull(entities);
			Assert.AreEqual(2, entities.Count);
		}

		[Test]
		public void ShouldReadOccupationNameWithId()
		{
			// Arrange
			OccupationNameEntity entity = OccupationNameData.GetItemForInsert();

			// Act
			entity = _repository.Create(entity);

			// Act
			var actual = _repository.Read(entity.Id);

			// Assert
			Assert.AreEqual(entity.Description, actual.Description);
		}

		[Test]
		public void ShouldUpdateOccupationName()
		{
			// Arrange
			OccupationNameEntity entity = OccupationNameData.GetItemForInsert();

			entity = _repository.Create(entity);
			entity.Description = "Tekenaar";

			// Act
			OccupationNameEntity actual = _repository.Update(entity);

			// Assert
			Assert.AreEqual(entity.Description, actual.Description);
		}

		[Test]
		public void ShouldDeleteOccupationName()
		{
			// Arrange
			OccupationNameEntity entity = OccupationNameData.GetItemForInsert();
			entity = _repository.Create(entity);

			// Act
			OccupationNameEntity actual = _repository.Delete(entity);

			// Assert
			Assert.AreEqual(entity.Description, actual.Description);
		}

		#endregion Test Methods
	}
}
