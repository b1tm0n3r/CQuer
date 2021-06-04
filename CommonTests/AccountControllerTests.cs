using System.Threading.Tasks;
using Common.DTOs;
using CommonServices.AccountServices;
using CQuerAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CommonTests
{
    [TestClass]
    public class AccountControllerTests
    {
        [TestMethod]
        public void Register_CheckingCreatedResult()
        {
            var testData = new RegisterDto
            {
                Username = "test",
            };
            var mockService = new Mock<IAccountService>();
            mockService.Setup(service => service.Register(testData)).Returns(Task.FromResult(1));
            
            var objectUnderTest = new AccountController(mockService.Object);
            
            var result = objectUnderTest.RegisterAccount(testData).Result;
            Assert.IsInstanceOfType(result, typeof(CreatedResult));
        }
        
        [TestMethod]
        public void Register_CheckingBadRequest()
        {
            var testData = new RegisterDto
            {
                Username = "test",
            };
            var mockService = new Mock<IAccountService>();
            mockService.Setup(service => service.AccountExists("test")).Returns(Task.FromResult(true));
            
            var objectUnderTest = new AccountController(mockService.Object);
            
            var result = objectUnderTest.RegisterAccount(testData).Result;
            
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }
    }
}