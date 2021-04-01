using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Common.DataModels.IdentityManagement;
using Common.DTOs;
using CommonServices.AccountServices;
using CQuerAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockQueryable.Moq;
using Moq;

namespace CommonServicesTests
{
    [TestClass]
    public class AccountServiceTest
    {
        private static List<Account> ACCOUNTS;

        [TestInitialize()]
        public void StartUp()
        {
            ACCOUNTS = new List<Account>
            {
                new Account { Name = "test" }
            };
        }
        [TestMethod]
        public void Register_CheckingBadRequest()
        {
            var mapperMock = new Mock<IMapper>();
            var accountMock = ACCOUNTS.AsQueryable().BuildMockDbSet();
            DbContextStub dbContextStub = new DbContextStub(accountMock.Object);
            AccountService objectUnderTest = new AccountService(dbContextStub, mapperMock.Object);
            var controller = new AccountController(objectUnderTest);
            
            var testData = new RegisterDto
            {
                Username = "test",
            };
            
            var result = controller.RegisterAccount(testData).Result;
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            Assert.IsNotNull(result);
        }
        
        [TestMethod]
        public void Register_CheckingCreateRequest()
        {
            var mapperMock = new Mock<IMapper>();
            var accountMock = ACCOUNTS.AsQueryable().BuildMockDbSet();
            DbContextStub dbContextStub = new DbContextStub(accountMock.Object);
            AccountService objectUnderTest = new AccountService(dbContextStub, mapperMock.Object);
            var controller = new AccountController(objectUnderTest);
            
            var testData = new RegisterDto
            {
                Username = "user2", //type unique username here
                Password = "password",
            };
            
            var result = controller.RegisterAccount(testData).Result;
            Assert.IsInstanceOfType(result, typeof(CreatedResult));
            Assert.IsNotNull(result);
        }
    }
}