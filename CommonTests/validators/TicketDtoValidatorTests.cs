using Common.DTOs;
using Common.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonTests.validators
{
    [TestClass]
    public class TicketDtoValidatorTests
    {
        [TestMethod]
        public void RETURN_TRUE_WITH_VALID_DATA()
        {
            TicketDtoValidator objectUnderTest = new TicketDtoValidator();

            TicketDto testData = new TicketDto()
            {
                Description = "test",
                DownloadUrl = "http://127.0.0.1/test",
                Severity = 2,
                Sha256Checksum = "",
                Solved = false,
                Username = "test"
            };

            var result = objectUnderTest.Validate(testData);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
        }
        [TestMethod]
        public void RETURN_FALSE_WITH_SEVERITY_LESS_THAN_ONE()
        {
            TicketDtoValidator objectUnderTest = new TicketDtoValidator();

            TicketDto testData = new TicketDto()
            {
                Description = "test",
                DownloadUrl = "http://127.0.0.1/test",
                Severity = -1,
                Sha256Checksum = "",
                Solved = false,
                Username = "test"
            };

            var result = objectUnderTest.Validate(testData);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
        }
        [TestMethod]
        public void RETURN_FALSE_WITH_SEVERITY_GREATER_THAN_5()
        {
            TicketDtoValidator objectUnderTest = new TicketDtoValidator();

            TicketDto testData = new TicketDto()
            {
                Description = "test",
                DownloadUrl = "http://127.0.0.1/test",
                Severity = 6,
                Sha256Checksum = "",
                Solved = false,
                Username = "test"
            };

            var result = objectUnderTest.Validate(testData);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
        }
        [TestMethod]
        public void RETURN_FALSE_IF_DESCRIPTION_CONTAINS_BAD_CHAR()
        {
            TicketDtoValidator objectUnderTest = new TicketDtoValidator();

            TicketDto testData = new TicketDto()
            {
                Description = "<test>test|$test</test>",
                DownloadUrl = "http://127.0.0.1/test",
                Severity = -1,
                Sha256Checksum = "",
                Solved = false,
                Username = "test"
            };

            var result = objectUnderTest.Validate(testData);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
        }
        [TestMethod]
        public void RETURN_FALSE_ON_EMPTY_DOWNLOAD_URL()
        {
            TicketDtoValidator objectUnderTest = new TicketDtoValidator();

            TicketDto testData = new TicketDto()
            {
                Description = "test",
                DownloadUrl = "",
                Severity = -1,
                Sha256Checksum = "",
                Solved = false,
                Username = "test"
            };

            var result = objectUnderTest.Validate(testData);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
        }
    }
}
