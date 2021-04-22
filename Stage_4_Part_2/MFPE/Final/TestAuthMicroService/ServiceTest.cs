using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using AuthMicroService.Controllers;
using AuthMicroService.Models;
using AuthMicroService.Repositories;
using AuthMicroService.Services;
using Castle.DynamicProxy.Contributors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;

namespace TestAuthMicroService
{
    public class Tests
    {
        private Mock<IConfiguration> _configMock;
        private Mock<IAuthService> _authServiceMock;
        private Mock<IAuthRepo> _authRepoMock;
        private AuthService _authService;
        private AuthRepo _authRepo;
        private Credentials _credentials = new Credentials() {UserName = "user", Password = "user"};

        [SetUp]
        public void Setup()
        {
            _configMock = new Mock<IConfiguration>();
            _configMock.Setup(x => x["Token:Key"]).Returns("This is the dummy key and is a dummy key key");

            _authRepoMock = new Mock<IAuthRepo>();
            _authRepoMock.Setup(x => x.GetCredentials()).Returns(_credentials);

            _authService = new AuthService(_authRepoMock.Object);
        }

        //Test GenerateJWT When Credentials is null
        [Test]
        public void GenerateJWT_InvalidData_ReturnsNoContent()
        {

            Credentials userCredentials = null;

            var token = _authService.GenerateJWT(userCredentials, _configMock.Object);

            Assert.IsNull(token);
        }

        //Test for Token Generation When Credentials are passed
        [TestCase("user", "user")]
        public void GenerateJWT_ValidData_ReturnsToken(string username, string password)
        {
            Credentials userCredentials = new Credentials() { UserName = username, Password = password };

            var token = _authService.GenerateJWT(userCredentials, _configMock.Object);

            Assert.IsNotNull(token);
        }

        [TestCase("user22", "user")]
        public void GenerateJWT_ValidData_ReturnsNoContent(string username, string password)
        {
            Credentials userCredentials = new Credentials() { UserName = username, Password = password };

            Mock<IConfiguration> configMock = new Mock<IConfiguration>();
            Mock<IAuthService> authServiceMock = new Mock<IAuthService>();

            _authRepoMock.Setup(x => x.GetCredentials()).Returns((Credentials)null);
            _authService = new AuthService(_authRepoMock.Object);

            authServiceMock.Setup(p => p.AuthenticateUser(userCredentials)).Returns((Credentials)null);

            AuthService service = new AuthService(_authRepoMock.Object);

            var token = _authService.GenerateJWT(null, _configMock.Object);

            Assert.IsNull(token);
        }

        //Test Authenticate User When Credentials are Correct
        [TestCase("user", "user")]
        public void AuthenticateUser_ValidData_ReturnsUser(string username, string password)
        {
            Credentials userCredentials = new Credentials() { UserName = username, Password = password };

            var user = _authService.AuthenticateUser(userCredentials);

            Assert.IsNotNull(user);
        }

        //Test wrong User
        [TestCase("user1", "user123")]
        public void AuthenticateUser_InvalidData_ReturnsNoContent(string username, string password)
        {
            Credentials userCredentials = new Credentials() { UserName = username, Password = password };

            var user = _authService.AuthenticateUser(userCredentials);

            Assert.IsNull(user);
        }

        [TestCase("user", "user")]
        public void AuthenticateUser_ValidData_ReturnsNoContent(string username, string password)
        {
            Credentials userCredentials = new Credentials() { UserName = username, Password = password };

            Mock<IConfiguration> configMock = new Mock<IConfiguration>();
            Mock<IAuthService> authServiceMock = new Mock<IAuthService>();

            _authRepoMock.Setup(x => x.GetCredentials()).Returns((Credentials)null);
            _authService = new AuthService(_authRepoMock.Object);

            authServiceMock.Setup(p => p.AuthenticateUser(userCredentials)).Returns((Credentials)null);


            AuthService service = new AuthService(_authRepoMock.Object);
            var user = service.AuthenticateUser(userCredentials);

            Assert.IsNull((Credentials)user);
        }

        

    }
}