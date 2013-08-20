using System;
using System.Globalization;
using System.Net;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using NUnit.Framework;
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.Auth;
using SingletonTheory.Services.AuthServices.Tests.Helpers;
using SingletonTheory.Services.AuthServices.TransferObjects;
using SingletonTheory.Data;
using System.Collections.Generic;
using MongoDB.Driver;

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
            UserAuth response = _client.Post(request);
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


        [Test]
        public void ShouldGetAllUsers()
        {
            // Arrange
            UserListRequest request = new UserListRequest();
            AuthService service = new AuthService();

            // Act
            List<UserAuth> response = _client.Get(request);

            // Assert
            Assert.AreNotEqual(response.Count, 0);
        }

        [Test]
        public void ShouldAddUser()
        {
            // Arrange
            UserRequest request = new UserRequest() { Id = _userId };

            // Act
            UserAuth response = _client.Get(request);

            // Assert
            Assert.AreNotEqual(response.Id, 0, "Unable to find test user in database.");
            Assert.AreEqual(response.UserName, MongoHelpers.MongoTestUsername, "Test username does not match that of expected user entry.");
            Assert.AreEqual(response.Roles[0], _currentRole, "Current role of user does not match expected");
            Assert.AreEqual(response.Meta["Active"], _currentActivitySetting.ToString(), "Active value does not match expected");
        }

        [Test]
        public void ShouldNotAddDuplicateUser()
        {
            //Arrange
            Exception webException = null;
            UserRequest request = new UserRequest { UserName = MongoHelpers.MongoTestUsername, Password = MongoHelpers.MongoTestUserPassword, Role = _currentRole, Active = _currentActivitySetting };
           
            //Act
            try
            {
                UserAuth response = _client.Post(request);
            }
            catch (Exception ex)
            {
                webException = ex;
            }

            //Assert
            Assert.IsNotNull(webException,"Duplicate user should generate a web error");
            Assert.That(((WebServiceException)webException).StatusCode, Is.EqualTo((int)HttpStatusCode.Conflict),"Incorrect status code for duplicate user");
            Assert.AreEqual(webException.Message, String.Format("User {0} already exists", MongoHelpers.MongoTestUsername));
        }

        [Test]
        public void ShouldUpdateUser()
        {
            // Arrange
            UserRequest request = new UserRequest() { Id = _userId };

            // Act
            _currentRole = request.Role = "user";
            _currentActivitySetting = request.Active = false;
            UserAuth response = _client.Put(request);

            //Assert
            UserAuth checkResponse = _client.Get(request);
            Assert.AreEqual(checkResponse.Roles[0], _currentRole, "Current role of user does not match expected");
            Assert.AreEqual(checkResponse.Meta["Active"], _currentActivitySetting.ToString(), "Active value does not match expected");
        }










        [Test]
        public void ShouldGetUserRoles()
        {
            // Arrange
            //AuthResponse authResponse = HTTPClientHelpers.Login();
            UserRoleRequest request = new UserRoleRequest { };
            AuthService service = new AuthService();

            // Act
            UserRoleResponse response = _client.Get(request);

            // Assert
            Assert.AreNotEqual(response.Roles.Count, 0);
        }

        [Test]
        public void ShouldGetUsers()
        {
            // Arrange
            //	AuthResponse authResponse = HTTPClientHelpers.Login();
            UserAuthRequest request = new UserAuthRequest();
            AuthService service = new AuthService();


            // Act
            UserAuth response = _client.Get(request);

            // Assert
            Assert.AreNotEqual(response.Roles.Count, 0);
        }

    }
}
