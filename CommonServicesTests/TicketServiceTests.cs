using AutoMapper;
using Common.DataModels.StandardEntities;
using Common.DTOs;
using CommonServices.TicketServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;
using Common.DataModels.IdentityManagement;

namespace CommonServicesTests
{
    [TestClass]
    public class TicketServiceTests
    {
        private static List<Ticket> TICKETS;
        private static List<Account> ACCOUNTS;

        [TestInitialize()]
        public void StartUp()
        {
            TICKETS = new List<Ticket>
            {
                new Ticket {
                    CreatorId = 1,
                    Description = "lorem ipsum",
                    DownloadUrl = "test.pl",
                    Severity = 4,
                    Sha256Checksum = "abcabc123123",
                    Solved = false
                }
            };
            ACCOUNTS = new List<Account>
            {
                new Account { Name = "test" }
            };
        }
        private static IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }

        [TestMethod]
        public void Create_FailsWithEmptyDescription()
        {
            var mapperMock = new Mock<IMapper>();
            var ticketMock = TICKETS.AsQueryable().BuildMockDbSet();
            var accountMock = ACCOUNTS.AsQueryable().BuildMockDbSet();
            DbContextStub dbContextStub = new DbContextStub(accountMock.Object, ticketMock.Object);
            TicketService objectUnderTest = new TicketService(dbContextStub, mapperMock.Object);

            var testData = new TicketDto
            {
                Username = "test",
                DownloadUrl = "test.pl",
                Severity = 3,
                Sha256Checksum = "testtest123",
                Solved = false
            };
            Assert.IsTrue(ValidateModel(testData).Any(v => v.MemberNames.Contains("Description") && v.ErrorMessage.Contains("required")));
        }

        [TestMethod]
        public void Create_FailsWithEmptyDownloadUrl()
        {
            var mapperMock = new Mock<IMapper>();
            var ticketMock = TICKETS.AsQueryable().BuildMockDbSet();
            var accountMock = ACCOUNTS.AsQueryable().BuildMockDbSet();
            DbContextStub dbContextStub = new DbContextStub(accountMock.Object, ticketMock.Object);
            TicketService objectUnderTest = new TicketService(dbContextStub, mapperMock.Object);

            var testData = new TicketDto
            {
                Username = "user",
                Description = "lorem ipsum",
                Severity = 3,
                Sha256Checksum = "testtest123",
                Solved = false
            };
            Assert.IsTrue(ValidateModel(testData).Any(v => v.MemberNames.Contains("DownloadUrl") && v.ErrorMessage.Contains("required")));
        }

        [TestMethod]
        public void Create_FailsWhenSeverityIsHigherThan5()
        {
            var mapperMock = new Mock<IMapper>();
            var ticketMock = TICKETS.AsQueryable().BuildMockDbSet();
            var accountMock = ACCOUNTS.AsQueryable().BuildMockDbSet();
            DbContextStub dbContextStub = new DbContextStub(accountMock.Object, ticketMock.Object);
            TicketService objectUnderTest = new TicketService(dbContextStub, mapperMock.Object);

            var testData = new TicketDto
            {
                Username = "user",
                DownloadUrl = "test.pl",
                Description = "lorem ipsum",
                Sha256Checksum = "testtest123",
                Severity = 13,
                Solved = false
            };
            Assert.IsTrue(ValidateModel(testData).Any(v => v.MemberNames.Contains("Severity") && v.ErrorMessage.Contains("must be between 1 and 5")));
        }

        [TestMethod]
        public void Create_FailsWithEmptySeverity()
        {
            var mapperMock = new Mock<IMapper>();
            var ticketMock = TICKETS.AsQueryable().BuildMockDbSet();
            var accountMock = ACCOUNTS.AsQueryable().BuildMockDbSet();
            DbContextStub dbContextStub = new DbContextStub(accountMock.Object, ticketMock.Object);
            TicketService objectUnderTest = new TicketService(dbContextStub, mapperMock.Object);

            var testData = new TicketDto
            {
                Username = "user",
                DownloadUrl = "test.pl",
                Description = "lorem ipsum",
                Sha256Checksum = "testtest123",
                Solved = false
            };
            Assert.IsTrue(ValidateModel(testData).Any(v => v.MemberNames.Contains("Severity") && v.ErrorMessage.Contains("must be between 1 and 5")));
        }

        [TestMethod]
        public void Create_Success()
        {
            var mapperMock = new Mock<IMapper>();
            var ticketMock = TICKETS.AsQueryable().BuildMockDbSet();
            var accountMock = ACCOUNTS.AsQueryable().BuildMockDbSet();
            DbContextStub dbContextStub = new DbContextStub(accountMock.Object, ticketMock.Object);
            TicketService objectUnderTest = new TicketService(dbContextStub, mapperMock.Object);
            
            var testData = new TicketDto
            {
                Username = "test",
                Description = "lorem ipsum",
                DownloadUrl = "test.pl",
                Severity = 3,
                Sha256Checksum = "testtest123",
                Solved = false
            };

            var result = objectUnderTest.Create(testData).Result;
            Assert.IsInstanceOfType(result, typeof(int));
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Delete_Success()
        {
            var mapperMock = new Mock<IMapper>();
            var ticketMock = TICKETS.AsQueryable().BuildMockDbSet();
            var accountMock = ACCOUNTS.AsQueryable().BuildMockDbSet();
            DbContextStub dbContextStub = new DbContextStub(accountMock.Object, ticketMock.Object);
            TicketService objectUnderTest = new TicketService(dbContextStub, mapperMock.Object);

            var testData = new TicketDto
            {
                Username = "test",
                Description = "lorem ipsum",
                DownloadUrl = "test.pl",
                Severity = 3,
                Sha256Checksum = "testtest123",
                Solved = false
            };
            var idToDelete = objectUnderTest.Create(testData).Result;
            var result = objectUnderTest.Delete(idToDelete).Result;
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Delete_TicketDoesntExist()
        {
            var mapperMock = new Mock<IMapper>();
            var ticketMock = TICKETS.AsQueryable().BuildMockDbSet();
            var accountMock = ACCOUNTS.AsQueryable().BuildMockDbSet();
            DbContextStub dbContextStub = new DbContextStub(accountMock.Object, ticketMock.Object);
            TicketService objectUnderTest = new TicketService(dbContextStub, mapperMock.Object);

            var testData = new TicketDto
            {
                Username = "test",
                Description = "lorem ipsum",
                DownloadUrl = "test.pl",
                Severity = 3,
                Sha256Checksum = "testtest123",
                Solved = false
            };

            var invalidId = objectUnderTest.Create(testData).Result+1;
            var result = objectUnderTest.Delete(invalidId).Result;
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Update_Success()
        {
            var mapperMock = new Mock<IMapper>();
            var ticketMock = TICKETS.AsQueryable().BuildMockDbSet();
            var accountMock = ACCOUNTS.AsQueryable().BuildMockDbSet();
            DbContextStub dbContextStub = new DbContextStub(accountMock.Object, ticketMock.Object);
            TicketService objectUnderTest = new TicketService(dbContextStub, mapperMock.Object);

            var testData = new TicketDto
            {
                Username = "test",
                Description = "lorem ipsum",
                DownloadUrl = "test.pl",
                Severity = 3,
                Sha256Checksum = "testtest123",
                Solved = false
            };

            var idToUpdate = objectUnderTest.Create(testData).Result;
            var result = objectUnderTest.Update(idToUpdate,testData).Result;
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Update_TicketDoesntExist()
        {
            var mapperMock = new Mock<IMapper>();
            var ticketMock = TICKETS.AsQueryable().BuildMockDbSet();
            var accountMock = ACCOUNTS.AsQueryable().BuildMockDbSet();
            DbContextStub dbContextStub = new DbContextStub(accountMock.Object, ticketMock.Object);
            TicketService objectUnderTest = new TicketService(dbContextStub, mapperMock.Object);

            var testData = new TicketDto
            {
                Username = "test",
                Description = "lorem ipsum",
                DownloadUrl = "test.pl",
                Severity = 3,
                Sha256Checksum = "testtest123",
                Solved = false
            };

            var invalidId = objectUnderTest.Create(testData).Result + 1;
            var result = objectUnderTest.Update(invalidId,testData).Result;
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Finalize_Success()
        {
            var mapperMock = new Mock<IMapper>();
            var ticketMock = TICKETS.AsQueryable().BuildMockDbSet();
            var accountMock = ACCOUNTS.AsQueryable().BuildMockDbSet();
            DbContextStub dbContextStub = new DbContextStub(accountMock.Object, ticketMock.Object);
            TicketService objectUnderTest = new TicketService(dbContextStub, mapperMock.Object);

            var testData = new TicketDto
            {
                Username = "test",
                Description = "lorem ipsum",
                DownloadUrl = "test.pl",
                Severity = 3,
                Sha256Checksum = "testtest123",
                Solved = false
            };

            var idToUpdate = objectUnderTest.Create(testData).Result;
            var result = objectUnderTest.Finalize(idToUpdate).Result;
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Finalize_TicketDoesntExist()
        {
            var mapperMock = new Mock<IMapper>();
            var ticketMock = TICKETS.AsQueryable().BuildMockDbSet();
            var accountMock = ACCOUNTS.AsQueryable().BuildMockDbSet();
            DbContextStub dbContextStub = new DbContextStub(accountMock.Object, ticketMock.Object);
            TicketService objectUnderTest = new TicketService(dbContextStub, mapperMock.Object);

            var testData = new TicketDto
            {
                Username = "test",
                Description = "lorem ipsum",
                DownloadUrl = "test.pl",
                Severity = 3,
                Sha256Checksum = "testtest123",
                Solved = false
            };

            var invalidId = objectUnderTest.Create(testData).Result + 1;
            var result = objectUnderTest.Finalize(invalidId).Result;
            Assert.IsFalse(result);
        }
    }
}

