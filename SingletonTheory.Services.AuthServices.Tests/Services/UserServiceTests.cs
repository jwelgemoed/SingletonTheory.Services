using MongoDB.Bson;
using NUnit.Framework;
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceInterface.Auth;
using SingletonTheory.Services.AuthServices.Tests.Helpers;
using SingletonTheory.Services.AuthServices.TransferObjects;
using System;
using System.Net;

namespace SingletonTheory.Services.AuthServices.Tests.Services
{
	[TestFixture]
	public class UserServiceTests
	{
		#region Fields & Properties

		private JsonServiceClient _client;
		private string _currentRole = "Admin";
		private bool _currentActivitySetting = true;
		private ObjectId _userId;

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

		#region Test Methods

		[Test]
		public void ShouldAddUser()
		{
			// Arrange
			var request = new User { Id = _userId };

			// Act
			var response = _client.Get(request);

			// Assert
			Assert.AreNotEqual(response.Id, 0, "Unable to find test user in database.");
			Assert.AreEqual(response.UserName, MongoHelpers.MongoTestUsername, "Test username does not match that of expected user entry.");
			Assert.AreEqual(response.Roles[0], _currentRole, "Current role of user does not match expected");
			Assert.AreEqual(response.Active, _currentActivitySetting.ToString(), "Active value does not match expected");
		}

		[Test]
		public void ShouldNotAddDuplicateUser()
		{
			//Arrange
			Exception webException = null;
			var request = new User { UserName = MongoHelpers.MongoTestUsername, Password = MongoHelpers.MongoTestUserPassword };
			request.Roles.Add(_currentRole);
			request.Active = _currentActivitySetting;
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

			var requestWithEmptyUserName = new User { UserName = "", Password = MongoHelpers.MongoTestUserPassword };
			requestWithEmptyUserName.Roles.Add(_currentRole);
			requestWithEmptyUserName.Active = _currentActivitySetting;

			var requestWithNullUserName = new User { UserName = null, Password = MongoHelpers.MongoTestUserPassword };
			requestWithNullUserName.Roles.Add(_currentRole);
			requestWithNullUserName.Active = _currentActivitySetting;

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

			var requestWithEmptyPassword = new User { UserName = null, Password = "" };
			requestWithEmptyPassword.Roles.Add(_currentRole);
			requestWithEmptyPassword.Active = _currentActivitySetting;

			var requestWithNullPassword = new User { UserName = null, Password = null };
			requestWithNullPassword.Roles.Add(_currentRole);
			requestWithNullPassword.Active = _currentActivitySetting;

			//var requestWithEmptyPassword = new UserRequest { UserName = MongoHelpers.MongoTestUsername, Password = "", Role = _currentRole, Active = _currentActivitySetting };
			//var requestWithNullPassword = new UserRequest { UserName = MongoHelpers.MongoTestUsername, Password = null, Role = _currentRole, Active = _currentActivitySetting };

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

			var requestWithEmptyRole = new User { UserName = MongoHelpers.MongoTestUsername, Password = MongoHelpers.MongoTestUserPassword };
			requestWithEmptyRole.Roles.Add("");
			requestWithEmptyRole.Active = _currentActivitySetting;

			var requestWithNullRole = new User { UserName = MongoHelpers.MongoTestUsername, Password = MongoHelpers.MongoTestUserPassword };
			requestWithNullRole.Roles.Add(null);
			requestWithNullRole.Active = _currentActivitySetting;

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

		#endregion Test Methods
	}
}
