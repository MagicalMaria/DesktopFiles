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
    class RepoTest
    {
        private Mock<IConfiguration> _configMock;
        private Mock<IAuthService> _authServiceMock;
        private Mock<IAuthRepo> _authRepoMock;
        private AuthService _authService;
        private AuthRepo _authRepo;
        private Credentials _credentials = new Credentials() { UserName = "user", Password = "user" };

        [SetUp]
        public void Setup()
        {
            _configMock = new Mock<IConfiguration>();
            _configMock.Setup(x => x["Token:Key"]).Returns("This is the dummy key and is a dummy key key");

            _authRepoMock = new Mock<IAuthRepo>();
            _authRepoMock.Setup(x => x.GetCredentials()).Returns(_credentials);

            _authService = new AuthService(_authRepoMock.Object);
        }

        //Test for Exception
        [TestCase("user", "user")]
        public void GetCredentials_InvalidData_ReturnsException(string username, string password)
        {
            Credentials userCredentials = new Credentials() { UserName = username, Password = password };

            _authRepoMock.Setup(x => x.GetCredentials()).Throws(new Exception());
            _authService = new AuthService(_authRepoMock.Object);

            var user = _authService.AuthenticateUser(userCredentials);
            Assert.IsNull(user);
        }

        //Test for Exception
        [TestCase("user", "user")]
        public void GetCredentials_ValidData_ReturnsUser(string username, string password)
        {
            Credentials userCredentials = new Credentials() { UserName = username, Password = password };

            _authRepoMock.Setup(x => x.GetCredentials()).Returns(_credentials);
            _authService = new AuthService(_authRepoMock.Object);

            var user = _authService.AuthenticateUser(userCredentials);
            Assert.IsNotNull(user);
        }

        [TestCase("user", "user")]
        public void GetCredentials_ValidData_ReturnsNoContent(string username, string password)
        {
            Credentials userCredentials = new Credentials() { UserName = username, Password = password };

            _authRepoMock.Setup(x => x.GetCredentials()).Returns((Credentials)null);
            _authService = new AuthService(_authRepoMock.Object);

            var user = _authService.AuthenticateUser(userCredentials);
            Assert.IsNull(user);
        }
    }
}
