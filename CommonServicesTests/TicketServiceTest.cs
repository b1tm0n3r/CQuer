using AutoMapper;
using Common.DataModels.StandardEntities;
using Common.DTOs;
using CommonServices.TicketServices;
using CQuerAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServicesTests
{
    [TestClass]
    public class TicketServiceTest
    {
        private static List<Ticket> TICKETS;

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
        }

        [TestMethod]
        public void Create_CheckingBadRequest()
        {
            var mapperMock = new Mock<IMapper>();
            var ticketMock = TICKETS.AsQueryable().BuildMockDbSet();
            DbContextStub dbContextStub = new DbContextStub(ticketMock.Object);
            TicketService objectUnderTest = new TicketService(dbContextStub, mapperMock.Object);
            var controller = new TicketController(objectUnderTest);

            var testData = new TicketDto
            {
                CreatorId = 2,
                Description = "lorem ipsum",
            };

            var result = controller.Create(testData).Result;
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Create_CheckingOkRequest()
        {
            var mapperMock = new Mock<IMapper>();
            var ticketMock = TICKETS.AsQueryable().BuildMockDbSet();
            DbContextStub dbContextStub = new DbContextStub(ticketMock.Object);
            TicketService objectUnderTest = new TicketService(dbContextStub, mapperMock.Object);
            var controller = new TicketController(objectUnderTest);

            var testData = new TicketDto
            {
                CreatorId = 2,
                Description = "lorem ipsum",
                DownloadUrl = "test.pl",
                Severity = 3,
                Sha256Checksum = "testtest123",
                Solved = false
            };

            var result = controller.Create(testData).Result;
            Assert.IsInstanceOfType(result, typeof(CreatedResult));
            Assert.IsNotNull(result);
        }
    }
}
