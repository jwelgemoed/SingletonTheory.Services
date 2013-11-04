using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SingletonTheory.Services.AuthServices.Config;
using SingletonTheory.Services.AuthServices.Entities.Hours;
using SingletonTheory.Services.AuthServices.Repositories.Hours;
using SingletonTheory.Services.AuthServices.Tests.Data;

namespace SingletonTheory.Services.AuthServices.Tests.Repositories.Hours
{
	public class CostCentreRepositoryTests
	{
		#region Test Methods

		[Test]
		public void ShouldThrowArgumentNullExceptionOnConstructor()
		{
			// Act
			try
			{
				CostCentreRepository repository = new CostCentreRepository(null);
				Assert.Fail("This should not happen");
			}
			catch (Exception ex)
			{
				// Arrange
				Assert.IsInstanceOf<ArgumentNullException>(ex);
			}
		}

		[Test]
		public void ShouldCreateCostCentre()
		{
			// Arrange
			CostCentreRepository repository = new CostCentreRepository(ConfigSettings.MySqlDatabaseConnectionName);
			CostCentreEntity entity = HoursData.GetCostCentreForInsert();
			repository.ClearCollection();
			// Act
			entity = repository.Create(entity);

			// Assert
			Assert.IsNotNull(entity);
			Assert.AreNotEqual(0, entity.Id);
		}

		[Test]
		public void ShouldCreateCostCentres()
		{
			// Arrange
			CostCentreRepository repository = new CostCentreRepository(ConfigSettings.MySqlDatabaseConnectionName);
			List<CostCentreEntity> entities = HoursData.GetCostCentresForInsert();
			repository.ClearCollection();

			// Act
			entities = repository.Create(entities);

			// Assert
			Assert.IsNotNull(entities);
			Assert.AreEqual(2, entities.Count);
		}

		[Test]
		public void ShouldReadCostCentreWithId()
		{
			// Arrange
			CostCentreRepository repository = new CostCentreRepository(ConfigSettings.MySqlDatabaseConnectionName);
			CostCentreEntity entity = HoursData.GetCostCentreForInsert();
			repository.ClearCollection();

			// Act
			entity = repository.Create(entity);

			// Act
			var actual = repository.Read(entity.Id);

			// Assert
			Assert.AreEqual(entity.Code, actual.Code);
		}

		[Test]
		public void ShouldUpdateCostCentre()
		{
			// Arrange
			CostCentreRepository repository = new CostCentreRepository(ConfigSettings.MySqlDatabaseConnectionName);
			CostCentreEntity entity = HoursData.GetCostCentreForInsert();
			repository.ClearCollection();
			entity = repository.Create(entity);
			entity.Code = "87";

			// Act
			CostCentreEntity actual = repository.Update(entity);

			// Assert
			Assert.AreEqual(entity.Code, actual.Code);
		}

		[Test]
		public void ShouldDeleteCostCentre()
		{
			// Arrange
			CostCentreRepository repository = new CostCentreRepository(ConfigSettings.MySqlDatabaseConnectionName);
			CostCentreEntity entity = HoursData.GetCostCentreForInsert();
			repository.ClearCollection();
			entity = repository.Create(entity);

			// Act
			CostCentreEntity actual = repository.Delete(entity);

			// Assert
			Assert.AreNotEqual(actual.DeletedDate,DateTime.MinValue);
		}

		#endregion Test Methods
	}
}
