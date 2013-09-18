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
	public class PermissionServiceTests
	{
		#region Fields & Properties

		private JsonServiceClient _client;
		private ObjectId _userId;
		private string _currentRole = "Admin";
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

		#region Permission CRUD

		[Test]
		public void ShouldAddPermission()
		{
			// Arrange
			var post = new Permission { Id = 0, Name = "Permission_CanPost" };
			var put = new Permission { Id = 0, Name = "Permission_CanPut" };
			var get = new Permission { Id = 0, Name = "Permission_CanGet" };
			var delete = new Permission { Id = 0, Name = "Permission_CanDelete" };
			var toBeDeleted = new Permission { Id = 0, Name = "wrongnamedeleteme" };

			// Act
			var postR = _client.Post(post);
			var putR = _client.Post(put);
			var getR = _client.Post(get);
			var deleteR = _client.Post(delete);
			var toBeDeletedR = _client.Post(toBeDeleted);

			// Assert
			Assert.AreEqual(postR.Id, 1, "Unable to find ... in database.");
			Assert.AreEqual(putR.Id, 2, "Unable to find ... in database.");
			Assert.AreEqual(getR.Id, 3, "Unable to find ... in database.");
			Assert.AreEqual(deleteR.Id, 4, "Unable to find ... in database.");
		}

		[Test]
		public void ShouldGetAllPermissions()
		{
			// Arrange
			Permissions request = new Permissions();
			AuthAdminService service = new AuthAdminService();

			// Act
			List<Permission> response = _client.Get(request);

			// Assert
			Assert.AreNotEqual(response.Count, 0);
		}

		[Test]
		public void ShouldDeletePermissions()
		{
			// Arrange
			Permissions request = new Permissions();
			AuthAdminService service = new AuthAdminService();
			List<Permission> response = _client.Get(request);

			// Act
			var toDelete = response[response.Count - 1];
			var responseB = _client.Delete(toDelete);

			// Assert
			Assert.AreEqual(responseB, null);
		}

		#endregion Permission CRUD
	}
}
