using Common.DTOs;
using Common.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonTests.validators
{
    [TestClass]
    public class DownloadReferenceDtoValidatorTests
    {
        [TestMethod]
        public void RETURN_TRUE_IF_DATA_IS_VALID()
        {
            DownloadReferenceDtoValidator objectUnderTest = new DownloadReferenceDtoValidator();

            var testData = new DownloadReferenceDto()
            {
                DownloadUrl = "http://127.0.0.1/test",
                Sha256Checksum = "test",
                TicketId = 1
            };

            var result = objectUnderTest.Validate(testData);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void RETURN_FALSE_IF_DOWNLOAD_URL_IS_EMPTY()
        {
            DownloadReferenceDtoValidator objectUnderTest = new DownloadReferenceDtoValidator();

            var testData = new DownloadReferenceDto()
            {
                DownloadUrl = "",
                Sha256Checksum = "test",
                TicketId = 1
            };

            var result = objectUnderTest.Validate(testData);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public void RETURN_FALSE_IF_TICKET_ID_EQUALS_ZERO()
        {
            DownloadReferenceDtoValidator objectUnderTest = new DownloadReferenceDtoValidator();

            var testData = new DownloadReferenceDto()
            {
                DownloadUrl = "http://127.0.0.1/test",
                Sha256Checksum = "test",
                TicketId = 0
            };

            var result = objectUnderTest.Validate(testData);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
        }
    }
}
