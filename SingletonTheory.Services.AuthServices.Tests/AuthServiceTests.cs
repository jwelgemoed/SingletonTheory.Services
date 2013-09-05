using NUnit.Framework;
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceInterface.Auth;
using SingletonTheory.Services.AuthServices.Tests.Helpers;
using SingletonTheory.Services.AuthServices.TransferObjects;
using System;
using System.Collections.Generic;
using System.Net;

namespace SingletonTheory.Services.AuthServices.Tests
{
	[TestFixture]
	public class AuthServiceTests
	{
		#region Fields & Properties

		private JsonServiceClient _client;
		private int _userId;
		private string _currentRole = "Admin";
		private bool _currentActivitySetting = true;

		#endregion Fields & Properties

		#region Setup & Teardown

		[SetUp]
		public void SetUp()
		{
			MongoHelpers.DeleteAllTestUserEntries();
			_client = HTTPClientHelpers.GetClient(HTTPClientHelpers.RootUrl, HTTPClientHelpers.UserName, HTTPClientHelpers.Password);
			AuthResponse authResponse = HTTPClientHelpers.Login();
			UserRequest request = new UserRequest { UserName = MongoHelpers.MongoTestUsername, Password = MongoHelpers.MongoTestUserPassword, Role = _currentRole, Active = _currentActivitySetting };
			List<UserAuth> response = _client.Post(request);
			_userId = response[0].Id;
		}

		[TearDownAttribute]
		public void TearDown()
		{
			MongoHelpers.DeleteAllTestUserEntries();
			_client.Dispose();
			_client = null;
		}

		#endregion Setup & Teardown

		#region Add User Tests and Validation Tests

		[Test]
		public void ShouldAddUser()
		{
			// Arrange
			var request = new UserRequest { Id = _userId };

			// Act
			var response = _client.Get(request);

			// Assert
			Assert.AreNotEqual(response[0].Id, 0, "Unable to find test user in database.");
			Assert.AreEqual(response[0].UserName, MongoHelpers.MongoTestUsername, "Test username does not match that of expected user entry.");
			Assert.AreEqual(response[0].Roles[0], _currentRole, "Current role of user does not match expected");
			Assert.AreEqual(response[0].Meta["Active"], _currentActivitySetting.ToString(), "Active value does not match expected");
		}

		[Test]
		public void ShouldNotAddDuplicateUser()
		{
			//Arrange
			Exception webException = null;
			var request = new UserRequest { UserName = MongoHelpers.MongoTestUsername, Password = MongoHelpers.MongoTestUserPassword, Role = _currentRole, Active = _currentActivitySetting };

			//Act
			try
			{
				var response = _client.Post(request);
			}
			catch (Exception ex)
			{
				webException = ex;
			}

			//Assert
			Assert.IsNotNull(webException, "Duplicate user should generate a web error");
			Assert.That(((WebServiceException)webException).StatusCode, Is.EqualTo((int)HttpStatusCode.Conflict), "Incorrect status code for duplicate user");
			Assert.AreEqual(webException.Message, String.Format("User {0} already exists", MongoHelpers.MongoTestUsername));
		}

		[Test]
		public void ShouldValidateUserNameOnAdd()
		{
			//Arrange
			WebServiceException webExceptionForEmptyUserName = null;
			WebServiceException webExceptionForNullUserName = null;
			var requestWithEmptyUserName = new UserRequest { UserName = "", Password = MongoHelpers.MongoTestUserPassword, Role = _currentRole, Active = _currentActivitySetting };
			var requestWithNullUserName = new UserRequest { UserName = null, Password = MongoHelpers.MongoTestUserPassword, Role = _currentRole, Active = _currentActivitySetting };

			//Act
			try
			{
				var response = _client.Post(requestWithEmptyUserName);
			}
			catch (WebServiceException ex)
			{
				webExceptionForEmptyUserName = ex;
			}
			try
			{
				var response = _client.Post(requestWithNullUserName);
			}
			catch (WebServiceException ex)
			{
				webExceptionForNullUserName = ex;
			}

			//Assert
			if (webExceptionForEmptyUserName == null || webExceptionForNullUserName == null)
				Assert.Fail("Validation of incorrect values did not work correctly.");
			Assert.AreEqual(webExceptionForEmptyUserName.ErrorCode, "NotEmpty", "Wrong error code returned.");
			Assert.AreEqual(webExceptionForEmptyUserName.ErrorMessage, "'User Name' should not be empty.", "Wrong error messsage returned.");
			Assert.AreEqual(webExceptionForEmptyUserName.StatusCode, 400, "Wrong status code returned");
			Assert.AreEqual(webExceptionForNullUserName.ErrorCode, "NotEmpty", "Wrong error code returned.");
			Assert.AreEqual(webExceptionForNullUserName.ErrorMessage, "'User Name' should not be empty.", "Wrong error messsage returned.");
			Assert.AreEqual(webExceptionForNullUserName.StatusCode, 400, "Wrong status code returned");
		}

		[Test]
		public void ShouldValidatePasswordOnAdd()
		{
			//Arrange
			WebServiceException webExceptionForEmptyPassword = null;
			WebServiceException webExceptionForNullPassword = null;
			var requestWithEmptyPassword = new UserRequest { UserName = MongoHelpers.MongoTestUsername, Password = "", Role = _currentRole, Active = _currentActivitySetting };
			var requestWithNullPassword = new UserRequest { UserName = MongoHelpers.MongoTestUsername, Password = null, Role = _currentRole, Active = _currentActivitySetting };

			//Act
			try
			{
				var response = _client.Post(requestWithEmptyPassword);
			}
			catch (WebServiceException ex)
			{
				webExceptionForEmptyPassword = ex;
			}
			try
			{
				var response = _client.Post(requestWithNullPassword);
			}
			catch (WebServiceException ex)
			{
				webExceptionForNullPassword = ex;
			}

			//Assert
			if (webExceptionForEmptyPassword == null || webExceptionForNullPassword == null)
				Assert.Fail("Validation of incorrect values did not work correctly.");
			Assert.AreEqual(webExceptionForEmptyPassword.ErrorCode, "NotEmpty", "Wrong error code returned.");
			Assert.AreEqual(webExceptionForEmptyPassword.ErrorMessage, "'Password' should not be empty.", "Wrong error messsage returned.");
			Assert.AreEqual(webExceptionForEmptyPassword.StatusCode, 400, "Wrong status code returned");
			Assert.AreEqual(webExceptionForNullPassword.ErrorCode, "NotEmpty", "Wrong error code returned.");
			Assert.AreEqual(webExceptionForNullPassword.ErrorMessage, "'Password' should not be empty.", "Wrong error messsage returned.");
			Assert.AreEqual(webExceptionForNullPassword.StatusCode, 400, "Wrong status code returned");
		}

		[Test]
		public void ShouldValidateRoleOnAdd()
		{
			//Arrange
			WebServiceException webExceptionForEmptyRole = null;
			WebServiceException webExceptionForNullRole = null;
			var requestWithEmptyRole = new UserRequest { UserName = MongoHelpers.MongoTestUsername, Password = MongoHelpers.MongoTestUserPassword, Role = "", Active = _currentActivitySetting };
			var requestWithNullRole = new UserRequest { UserName = MongoHelpers.MongoTestUsername, Password = MongoHelpers.MongoTestUserPassword, Role = null, Active = _currentActivitySetting };

			//Act
			try
			{
				var response = _client.Post(requestWithEmptyRole);
			}
			catch (WebServiceException ex)
			{
				webExceptionForEmptyRole = ex;
			}
			try
			{
				var response = _client.Post(requestWithNullRole);
			}
			catch (WebServiceException ex)
			{
				webExceptionForNullRole = ex;
			}

			//Assert
			if (webExceptionForEmptyRole == null || webExceptionForNullRole == null)
				Assert.Fail("Validation of incorrect values did not work correctly.");
			Assert.AreEqual(webExceptionForEmptyRole.ErrorCode, "NotEmpty", "Wrong error code returned.");
			Assert.AreEqual(webExceptionForEmptyRole.ErrorMessage, "'Role' should not be empty.", "Wrong error messsage returned.");
			Assert.AreEqual(webExceptionForEmptyRole.StatusCode, 400, "Wrong status code returned");
			Assert.AreEqual(webExceptionForNullRole.ErrorCode, "NotEmpty", "Wrong error code returned.");
			Assert.AreEqual(webExceptionForNullRole.ErrorMessage, "'Role' should not be empty.", "Wrong error messsage returned.");
			Assert.AreEqual(webExceptionForNullRole.StatusCode, 400, "Wrong status code returned");
		}

		#endregion Add User Tests and Validation Tests

		#region Add User Tests and Validation Tests

		[Test]
		public void ShouldUpdateUser()
		{
			// Arrange
			var request = new UserRequest() { Id = _userId };

			// Act
			_currentRole = request.Role = "user";
			_currentActivitySetting = request.Active = false;
			var response = _client.Put(request);

			//Assert
			var checkResponse = _client.Get(request);
			Assert.AreEqual(checkResponse[0].Roles[0], _currentRole, "Current role of user does not match expected");
			Assert.AreEqual(checkResponse[0].Meta["Active"], _currentActivitySetting.ToString(), "Active value does not match expected");
		}

		[Test]
		public void ShouldReportCorrectlyOnUpdateIfUserDoesNotExist()
		{
			// Arrange
			Exception webException = null;
			var request = new UserRequest() { Id = 999999999 };

			// Act
			_currentRole = request.Role = "user";
			_currentActivitySetting = request.Active = false;
			try
			{
				var response = _client.Put(request);
			}
			catch (Exception ex)
			{
				webException = ex;
			}

			//Assert
			Assert.IsNotNull(webException, "Invalid user ID should generate an error");
			Assert.That(((WebServiceException)webException).StatusCode, Is.EqualTo((int)HttpStatusCode.NotFound), "Incorrect status code for NotFound user");
			Assert.AreEqual(webException.Message, "User not found in User Database.");
		}

		[Test]
		public void ShouldValidateIdOnUpdate()
		{
			//Arrange
			WebServiceException webException = null;
			var request = new UserRequest { Id = 0 };

			//Act
			request.Role = "user";
			request.Active = false;
			try
			{
				var response = _client.Put(request);
			}
			catch (WebServiceException ex)
			{
				webException = ex;
			}

			//Assert
			if (webException == null)
				Assert.Fail("Validation of incorrect ID value did not work correctly.");
			Assert.AreEqual(webException.ErrorCode, "GreaterThan", "Wrong error code returned.");
			Assert.AreEqual(webException.ErrorMessage, "'Id' must be greater than '0'.", "Wrong error messsage returned.");
			Assert.AreEqual(webException.StatusCode, 400, "Wrong status code returned");
		}

		[Test]
		public void ShouldValidateRoleOnUpdate()
		{
			//Arrange
			WebServiceException webExceptionForEmptyRole = null;
			WebServiceException webExceptionForNullRole = null;
			var requestForEmptyRole = new UserRequest { Id = _userId };
			var requestForNullRole = new UserRequest { Id = _userId };

			//Act
			requestForEmptyRole.Role = "";
			requestForEmptyRole.Active = false;
			try
			{
				var response = _client.Put(requestForEmptyRole);
			}
			catch (WebServiceException ex)
			{
				webExceptionForEmptyRole = ex;
			}
			requestForNullRole.Role = null;
			requestForNullRole.Active = false;
			try
			{
				var response = _client.Put(requestForNullRole);
			}
			catch (WebServiceException ex)
			{
				webExceptionForNullRole = ex;
			}

			//Assert
			if (webExceptionForEmptyRole == null || webExceptionForNullRole == null)
				Assert.Fail("Validation of incorrect values did not work correctly.");
			Assert.AreEqual(webExceptionForEmptyRole.ErrorCode, "NotEmpty", "Wrong error code returned.");
			Assert.AreEqual(webExceptionForEmptyRole.ErrorMessage, "'Role' should not be empty.", "Wrong error messsage returned.");
			Assert.AreEqual(webExceptionForEmptyRole.StatusCode, 400, "Wrong status code returned");
			Assert.AreEqual(webExceptionForNullRole.ErrorCode, "NotEmpty", "Wrong error code returned.");
			Assert.AreEqual(webExceptionForNullRole.ErrorMessage, "'Role' should not be empty.", "Wrong error messsage returned.");
			Assert.AreEqual(webExceptionForNullRole.StatusCode, 400, "Wrong status code returned");
		}

		#endregion Add User Tests and Validation Tests

		#region Other Tests

		[Test]
		public void ShouldGetAllUsers()
		{
			// Arrange
			UserRequest request = new UserRequest();
			AuthService service = new AuthService();

			// Act
			List<UserAuth> response = _client.Get(request);

			// Assert
			Assert.AreNotEqual(response.Count, 0);
		}

		[Test]
		public void ShouldGetUserRoles()
		{
			// Arrange
			//AuthResponse authResponse = HTTPClientHelpers.Login();
			CurrentUserRequest request = new CurrentUserRequest { };
			AuthService service = new AuthService();

			// Act
			UserAuth response = _client.Get(request);

			// Assert
			Assert.AreNotEqual(response.Roles.Count, 0);
		}

		//[Test]
		//public void ShouldGetUsers()
		//{
		//	// Arrange
		//	//	AuthResponse authResponse = HTTPClientHelpers.Login();
		//	UserAuthRequest request = new UserAuthRequest();
		//	AuthService service = new AuthService();


		//	// Act
		//	UserAuth response = _client.Get(request);

		//	// Assert
		//	Assert.AreNotEqual(response.Roles.Count, 0);
		//}

		#endregion Other Tests
	}
}
