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
        public void Register_Task_Succesful()
        {
            var mapperMock = new Mock<IMapper>();
            var accountMock = ACCOUNTS.AsQueryable().BuildMockDbSet();
            DbContextStub dbContextStub = new DbContextStub(accountMock.Object);
            AccountService objectUnderTest = new AccountService(dbContextStub, mapperMock.Object);
            
            var testData = new RegisterDto
            {
                Username = "test",
                Password = "test"
            };

            var result = objectUnderTest.Register(testData).IsCompleted;
            Assert.IsTrue(result);
        }
        
        [TestMethod]
        public void Register_Task_Faulted()
        {
            var mapperMock = new Mock<IMapper>();
            var accountMock = ACCOUNTS.AsQueryable().BuildMockDbSet();
            DbContextStub dbContextStub = new DbContextStub(accountMock.Object);
            AccountService objectUnderTest = new AccountService(dbContextStub, mapperMock.Object);
            
            var testData = new RegisterDto
            {
                Username = "test",
            };

            var result = objectUnderTest.Register(testData).IsFaulted;
            Assert.IsTrue(result);
        }
        
        [TestMethod]
        public void Register_Return_Id()
        {
            var mapperMock = new Mock<IMapper>();
            var accountMock = ACCOUNTS.AsQueryable().BuildMockDbSet();
            DbContextStub dbContextStub = new DbContextStub(accountMock.Object);
            AccountService objectUnderTest = new AccountService(dbContextStub, mapperMock.Object);
            
            var testData = new RegisterDto
            {
                Username = "test",
                Password = "test"
            };

            var result = objectUnderTest.Register(testData).Result;
            Assert.IsInstanceOfType(result, typeof(int));
        }
        
        
    }
}