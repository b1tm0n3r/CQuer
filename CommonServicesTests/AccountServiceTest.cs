using CQuerMVC;
using CQuerMVC.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonServicesTests
{
    [TestClass]
    public class AccountServiceTest
    {
        private AccountController _controller;
        private AccountServiceFake _service;

        public AccountServiceTest()
        {
            _service = new AccountServiceFake();
            _controller = new AccountController(_service);
        }

        [TestMethod]
        public void Register_CheckingBadRequest()
        {
            var controller = new AccountController(_service);
            var user = new RegisterDto
            {
                Username = "user",
                Password = "password"
            };
            var createdResult = controller.RegisterAccount(user).Result;
            Assert.IsInstanceOfType(createdResult, typeof(BadRequestObjectResult));
        }
        
        [TestMethod]
        public void Register_CheckingCreateRequest()
        {
            var controller = new AccountController(_service);
            var user = new RegisterDto
            {
                Username = "user2", //type unique username here
                Password = "password",
            };
            var createdResult = controller.RegisterAccount(user).Result;
            Assert.IsInstanceOfType(createdResult, typeof(CreatedResult));
        }
        
    }
}