using MongoDB.Bson;
﻿using NUnit.Framework;
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceInterface.Auth;
using SingletonTheory.Services.AuthServices.Services;
using SingletonTheory.Services.AuthServices.Tests.Helpers;
using SingletonTheory.Services.AuthServices.TransferObjects;
using System;
using System.Collections.Generic;

namespace SingletonTheory.Services.AuthServices.Tests.Services
{
	[TestFixture]
	public class DomainPermissionServiceTests
	{
		#region Fields & Properties

		private JsonServiceClient _client;
		private ObjectId _userId;
		private int _currentRole = 1;
		private bool _currentActivitySetting = true;

		#endregion Fields & Properties

		#region Setup & Teardown

		[SetUp]
		public void SetUp()
		{
			try
			{
				MongoHelpers.DeleteAllTestUserEntries();
				_client = HTTPClientHelpers.GetClient(HTTPClientHelpers.RootUrl, HTTPClientHelpers.UserName, HTTPClientHelpers.Password);
				AuthResponse authResponse = HTTPClientHelpers.Login();
				User request = new User { UserName = MongoHelpers.MongoTestUsername, Password = MongoHelpers.MongoTestUserPassword };
				request.Active = _currentActivitySetting;
				request.Roles.Add(_currentRole);
				User response = _client.Post(request);
				_userId = response.Id;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				//throw;
			}
		}

		[TearDownAttribute]
		public void TearDown()
		{
			MongoHelpers.DeleteAllTestUserEntries();
			_client.Dispose();
			_client = null;
		}

		#endregion Setup & Teardown

		#region DomainPermission CRUD

		[Test]
		public void ShouldAddDomainPermission()
		{
			// Arrange
			var obj1 = new DomainPermission { Id = 0, Label = "DomainPermission1", Description = "Groupies for life", FunctionalPermissionIds = new[] { 1, 2 } };
			var obj2 = new DomainPermission { Id = 0, Label = "DomainPermission2", Description = "Groupei 2 for live", FunctionalPermissionIds = new[] { 1, 2 } };
			var obj3 = new DomainPermission { Id = 0, Label = "todelete", Description = "delete me", FunctionalPermissionIds = new[] { 1, 2 } };

			// Act
			var obj1R = _client.Post(obj1);
			var obj2R = _client.Post(obj2);
			var obj3R = _client.Post(obj3);
			
			// Assert
			Assert.AreEqual(obj1R.Id, 1, "Unable to find ... in database.");
			Assert.AreEqual(obj2R.Id, 2, "Unable to find ... in database.");
		}

		[Test]
		public void ShouldGetAllDomainPermissions()
		{
			// Arrange
			DomainPermissions request = new DomainPermissions();
			AuthAdminService service = new AuthAdminService();

			// Act
			List<DomainPermission> response = _client.Get(request);

			// Assert
			Assert.AreNotEqual(response.Count, 0);
		}

		[Test]
		public void ShouldDeleteDomainPermission()
		{
			// Arrange
			DomainPermissions request = new DomainPermissions();
			List<DomainPermission> response = _client.Get(request);

			// Act
			var toDelete = response[response.Count - 1];
			var responseB = _client.Delete(toDelete);

			// Assert
			Assert.AreEqual(responseB, null);
		}

		#endregion DomainPermission CRUD
	}
}
