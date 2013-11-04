using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SingletonTheory.Services.AuthServices.Config;
using SingletonTheory.Services.AuthServices.Entities.ContactDetails;
using SingletonTheory.Services.AuthServices.Helpers.Data;
using SingletonTheory.Services.AuthServices.Helpers.Helpers;
using SingletonTheory.Services.AuthServices.Repositories.ContactDetails;
using SingletonTheory.Services.AuthServices.Tests.Data;
using SingletonTheory.Services.AuthServices.Tests.Helpers;

namespace SingletonTheory.Services.AuthServices.Tests.Repositories.ContactDetails
{
	public class ContactRepositoryTests
	{
		private ContactRepository _repository;

		#region Setup and Teardown

		[SetUp]
		public void Init()
		{
			try
			{
				_repository = new ContactRepository(ConfigSettings.MySqlDatabaseConnectionName);
				_repository.ClearCollection();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}

		[TearDown]
		public void Dispose()
		{
			try
			{
				_repository.ClearCollection();

				ContactDetailsHelpers.ClearContactType();
				ContactDetailsHelpers.ClearEntity();
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
			ContactEntity entity = ContactData.GetItemForInsert();

			// Act
			try
			{
				entity = _repository.Create(entity);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}

			// Assert
			Assert.IsNotNull(entity);
			Assert.AreNotEqual(0, entity.Id);
		}

		[Test]
		public void ShouldCreateContacts()
		{
			// Arrange
			List<ContactEntity> entities = ContactData.GetItemsForInsert();

			// Act
			entities = _repository.Create(entities);

			// Assert
			Assert.IsNotNull(entities);
			Assert.AreEqual(2, entities.Count);
		}

		[Test]
		public void ShouldReadContactWithId()
		{
			// Arrange
			ContactEntity entity = ContactData.GetItemForInsert();

			// Act
			entity = _repository.Create(entity);

			// Act
			var actual = _repository.Read(entity.Id);

			// Assert
			Assert.AreEqual(entity.Value, actual.Value);
		}

		[Test]
		public void ShouldUpdateContact()
		{
			// Arrange
			ContactEntity entity = ContactData.GetItemForInsert();
			entity = _repository.Create(entity);
			entity.Value = "Contact";

			// Act
			ContactEntity actual = _repository.Update(entity);

			// Assert
			Assert.AreEqual(entity.Value, actual.Value);
		}

		[Test]
		public void ShouldDeleteContact()
		{
			// Arrange
			ContactEntity entity = ContactData.GetItemForInsert();
			entity = _repository.Create(entity);

			// Act
			ContactEntity actual = _repository.Delete(entity);

			// Assert
			Assert.AreEqual(entity.Value, actual.Value);
		}

		#endregion Test Methods
	}
}