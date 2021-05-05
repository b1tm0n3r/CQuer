using Common.DataModels.StandardEntities;
using Common.DTOs;
using CommonServices.TicketServices;
using CQuerAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTests
{
    [TestClass]
    public class TicketControllerTests
    {
        [TestMethod]
        public void Create_Success()
        {
            var testData = new TicketDto
            {
                Username = "user",
                Description = "lorem ipsum",
                DownloadUrl = "test.pl",
                Severity = 3,
                Sha256Checksum = "testtest123",
                Solved = false
            };
            var serviceMock = new Mock<ITicketService>();
            serviceMock.Setup(x => x.Create(testData)).Returns(Task.FromResult(1));

            var objectUnderTest = new TicketController(serviceMock.Object);

            var result = objectUnderTest.Create(testData).Result;
            Assert.IsInstanceOfType(result, typeof(CreatedResult));
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Update_Success()
        {
            var testData = new TicketDto
            {
                Username = "user",
                Description = "lorem ipsum",
                DownloadUrl = "test.pl",
                Severity = 3,
                Sha256Checksum = "testtest123",
                Solved = false
            };
            var serviceMock = new Mock<ITicketService>();
            
            serviceMock.Setup(x => x.Update(It.IsAny<int>(), testData)).Returns(Task.FromResult(true));

            var objectUnderTest = new TicketController(serviceMock.Object);

            var result = objectUnderTest.Update(1, testData).Result;
            
            Assert.IsInstanceOfType(result.Result, typeof(OkResult));
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Update_Unsuccessful()
        {
            var testData = new TicketDto
            {
                Username = "user",
                Description = "lorem ipsum",
                DownloadUrl = "test.pl",
                Severity = 3,
                Sha256Checksum = "testtest123",
                Solved = false
            };
            var serviceMock = new Mock<ITicketService>();

            serviceMock.Setup(x => x.Update(It.IsAny<int>(), testData)).Returns(Task.FromResult(false));

            var objectUnderTest = new TicketController(serviceMock.Object);

            var result = objectUnderTest.Update(1, testData).Result;

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Finalize_Success()
        {
            var testData = new TicketDto
            {
                Username = "user",
                Description = "lorem ipsum",
                DownloadUrl = "test.pl",
                Severity = 3,
                Sha256Checksum = "testtest123",
                Solved = false
            };
            var serviceMock = new Mock<ITicketService>();

            serviceMock.Setup(x => x.Finalize(It.IsAny<int>())).Returns(Task.FromResult(true));

            var objectUnderTest = new TicketController(serviceMock.Object);

            var result = objectUnderTest.Finalize(1).Result;

            Assert.IsInstanceOfType(result.Result, typeof(OkResult));
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void Finalize_Unsuccessful()
        {
            var testData = new TicketDto
            {
                Username = "user",
                Description = "lorem ipsum",
                DownloadUrl = "test.pl",
                Severity = 3,
                Sha256Checksum = "testtest123",
                Solved = false
            };
            var serviceMock = new Mock<ITicketService>();

            serviceMock.Setup(x => x.Finalize(It.IsAny<int>())).Returns(Task.FromResult(false));

            var objectUnderTest = new TicketController(serviceMock.Object);

            var result = objectUnderTest.Finalize(1).Result;

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Delete_Success()
        {
            var testData = new TicketDto
            {
                Username = "user",
                Description = "lorem ipsum",
                DownloadUrl = "test.pl",
                Severity = 3,
                Sha256Checksum = "testtest123",
                Solved = false
            };
            var serviceMock = new Mock<ITicketService>();

            serviceMock.Setup(x => x.Delete(It.IsAny<int>())).Returns(Task.FromResult(true));

            var objectUnderTest = new TicketController(serviceMock.Object);

            var result = objectUnderTest.Delete(1).Result;

            Assert.IsInstanceOfType(result.Result, typeof(OkResult));
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Delete_Unsuccessful()
        {
            var testData = new TicketDto
            {
                Username = "user",
                Description = "lorem ipsum",
                DownloadUrl = "test.pl",
                Severity = 3,
                Sha256Checksum = "testtest123",
                Solved = false
            };
            var serviceMock = new Mock<ITicketService>();

            serviceMock.Setup(x => x.Delete(It.IsAny<int>())).Returns(Task.FromResult(false));

            var objectUnderTest = new TicketController(serviceMock.Object);

            var result = objectUnderTest.Delete(1).Result;

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
            Assert.IsNotNull(result);
        }
    }
}
