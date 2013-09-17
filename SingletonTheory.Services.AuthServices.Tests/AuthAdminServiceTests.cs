using MongoDB.Bson;
﻿using NUnit.Framework;
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceInterface.Auth;
using SingletonTheory.Services.AuthServices.Services;
using SingletonTheory.Services.AuthServices.Tests.Helpers;
using SingletonTheory.Services.AuthServices.TransferObjects;
using System;
using System.Collections.Generic;

namespace SingletonTheory.Services.AuthServices.Tests
{
	[TestFixture]
	public class AuthAdminServiceTests
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
			var put = new Permission { Id = 1, Name = "Permission_CanPut" };
			var get = new Permission { Id = 2, Name = "Permission_CanGet" };
			var delete = new Permission { Id = 3, Name = "Permission_CanDelete" };
			var toBeDeleted = new Permission { Id = 4, Name = "wrongnamedeleteme" };

			// Act
			var postR = _client.Post(post);
			var putR = _client.Post(put);
			var getR = _client.Post(get);
			var deleteR = _client.Post(delete);
			var toBeDeletedR = _client.Post(toBeDeleted);

			// Assert
			Assert.AreEqual(postR[0].Id, 0, "Unable to find ... in database.");
			Assert.AreEqual(putR[0].Id, 1, "Unable to find ... in database.");
			Assert.AreEqual(getR[0].Id, 2, "Unable to find ... in database.");
			Assert.AreEqual(deleteR[0].Id, 3, "Unable to find ... in database.");
		}

		[Test]
		public void ShouldGetAllPermissions()
		{
			// Arrange
			Permission request = new Permission();
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
			Permission request = new Permission();
			AuthAdminService service = new AuthAdminService();
			List<Permission> response = _client.Get(request);

			// Act
			request = response[response.Count - 1];
			var responseB = _client.Delete(request);

			// Assert
			Assert.AreNotEqual(responseB.Count, 4);
		}

		#endregion Permission CRUD

		#region GroupLvl1 CRUD

		[Test]
		public void ShouldAddGroupLvl1()
		{
			// Arrange
			var obj1 = new GroupLvl1 { Id = 0, Name = "UserAdmin" };
			var obj2 = new GroupLvl1 { Id = 1, Name = "AuthAdmin" };

			// Act
			var obj1R = _client.Post(obj1);
			var obj2R = _client.Post(obj2);


			// Assert
			Assert.AreEqual(obj1R[0].Id, 0, "Unable to find ... in database.");
			Assert.AreEqual(obj2R[0].Id, 1, "Unable to find ... in database.");
		}

		[Test]
		public void ShouldGetAllGroupLvl1()
		{
			// Arrange
			GroupLvl1 request = new GroupLvl1();
			AuthAdminService service = new AuthAdminService();

			// Act
			List<GroupLvl1> response = _client.Get(request);

			// Assert
			Assert.AreNotEqual(response.Count, 0);
		}

		//TODO: add get and delete

		#endregion GroupLvl1 CRUD

		#region GroupLvl2 CRUD

		[Test]
		public void ShouldAddGroupLvl2()
		{
			// Arrange
			var obj1 = new GroupLvl2 { Id = 0, Name = "UserAdminGrp2", Description = "Groupies for life" };
			var obj2 = new GroupLvl2 { Id = 1, Name = "AuthAdminGrp2", Description = "Groupei 2 for live" };

			// Act
			var obj1R = _client.Post(obj1);
			var obj2R = _client.Post(obj2);


			// Assert
			Assert.AreEqual(obj1R[0].Id, 0, "Unable to find ... in database.");
			Assert.AreEqual(obj2R[0].Id, 1, "Unable to find ... in database.");
		}

		[Test]
		public void ShouldGetAllGroupLvl2()
		{
			// Arrange
			GroupLvl2 request = new GroupLvl2();
			AuthAdminService service = new AuthAdminService();

			// Act
			List<GroupLvl2> response = _client.Get(request);

			// Assert
			Assert.AreNotEqual(response.Count, 0);
		}

		//TODO: add get and delete

		#endregion GroupLvl2 CRUD

		#region Role CRUD

		[Test]
		public void ShouldAddRole()
		{
			// Arrange
			var admin = new Role { Id = 0, Name = "Admin", Description = "Just a wuser" };
			var user = new Role { Id = 1, Name = "User", Description = "Admin can do just about anything" };

			// Act
			var adminR = _client.Post(admin);
			var userR = _client.Post(user);


			// Assert
			Assert.AreEqual(adminR[0].Id, 0, "Unable to find ... in database.");
			Assert.AreEqual(userR[0].Id, 1, "Unable to find ... in database.");
		}

		[Test]
		public void ShouldGetAllRoles()
		{
			// Arrange
			Role request = new Role();
			AuthAdminService service = new AuthAdminService();

			// Act
			List<Role> response = _client.Get(request);

			// Assert
			Assert.AreNotEqual(response.Count, 0);
		}

		//TODO: add get and delete

		#endregion Role CRUD
	}
}
