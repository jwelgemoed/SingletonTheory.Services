using MongoDB.Bson;
using NUnit.Framework;
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceInterface.Auth;
using SingletonTheory.Services.AuthServices.Tests.Helpers;
using SingletonTheory.Services.AuthServices.TransferObjects;
using System;
using System.Net;

namespace SingletonTheory.Services.AuthServices.Tests
{
	[TestFixture]
	public class AuthServiceTests
	{
		#region Fields & Properties

		private JsonServiceClient _client;
		private long _userId;
		private int _currentRole = 1;
		private bool _currentActivitySetting = true;

		#endregion Fields & Properties

		#region Setup & Teardown

		[SetUp]
		public void SetUp()
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
		public void ShouldUpdateUser()
		{
			// Arrange
			var request = new User() { Id = _userId };

			// Act
			_currentRole = 2;
			request.Roles.Add(_currentRole);
			_currentActivitySetting = false;
			request.Active = _currentActivitySetting;
			var response = _client.Put(request);

			//Assert
			var checkResponse = _client.Get(request);
			Assert.AreEqual(checkResponse.Roles[0], _currentRole, "Current role of user does not match expected");
			Assert.AreEqual(checkResponse.Active, _currentActivitySetting.ToString(), "Active value does not match expected");
		}

		[Test]
		public void ShouldReportCorrectlyOnUpdateIfUserDoesNotExist()
		{
			// Arrange
			Exception webException = null;
			var request = new User();

			// Act
			_currentRole = 2;
			request.Roles.Add(_currentRole);
			_currentActivitySetting = false;
			request.Active = _currentActivitySetting;
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
			var request = new User();

			//Act
			request.Roles.Add(2);
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
			var requestForEmptyRole = new User { Id = _userId };
			var requestForNullRole = new User { Id = _userId };

			//Act
			requestForEmptyRole.Roles.Add(0);
			requestForEmptyRole.Active = false;
			try
			{
				var response = _client.Put(requestForEmptyRole);
			}
			catch (WebServiceException ex)
			{
				webExceptionForEmptyRole = ex;
			}
			requestForNullRole.Roles = null;
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
		public void ShouldGetUserRoles()
		{
			// Arrange
			//AuthResponse authResponse = HTTPClientHelpers.Login();
			CurrentUserAuthRequest request = new CurrentUserAuthRequest { };
			AuthService service = new AuthService();

			// Act
			UserAuth response = _client.Get(request);

			// Assert
			Assert.AreNotEqual(response.Roles.Count, 0);
		}

		#endregion Other Tests
	}
}
