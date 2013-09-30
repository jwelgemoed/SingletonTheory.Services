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
	public class RoleServiceTests
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

		#region Role CRUD

		[Test]
		public void ShouldAddRole()
		{
			// Arrange
			var admin = new Role { Id = 0, Label = "Admin", Description = "Just a wuser", DomainPermissionIds = new[] { 1, 2 } };
			var user = new Role { Id = 0, Label = "User", Description = "Admin can do just about anything", DomainPermissionIds = new[] { 1, 2 } };
			var toDelete = new Role { Id = 0, Label = "User", Description = "Gona delete this", DomainPermissionIds = new[] { 1, 2 } };

			// Act
			var adminR = _client.Post(admin);
			var userR = _client.Post(user);
			var toDeleteR = _client.Post(toDelete);

			// Assert
			Assert.AreEqual(adminR.Id, 0, "Unable to find ... in database.");
			Assert.AreEqual(userR.Id, 0, "Unable to find ... in database.");
		}

		[Test]
		public void ShouldGetAllRoles()
		{
			// Arrange
			Roles request = new Roles();

			// Act
			List<Role> response = _client.Get(request);

			// Assert
			Assert.AreNotEqual(response.Count, 0);
		}

		[Test]
		public void ShouldDeleteRole()
		{
			// Arrange
			Roles request = new Roles();
			List<Role> response = _client.Get(request);

			// Act
			var toDelete = response[response.Count - 1];
			var responseB = _client.Delete(toDelete);

			// Assert
			Assert.AreEqual(responseB, null);
		}

		#endregion Role CRUD
	}
}
