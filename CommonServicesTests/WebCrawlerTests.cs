using CommonServices.HttpWebProxy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonServicesTests
{
    [TestClass]
    public class WebCrawlerTests
    {
        private static readonly string BASE_URL = "http://test.com/";
        private static readonly string BASE_URL_NOT_CLOSED = "http://test.com";
        private static readonly string BASE_URL_WITH_SUBDIRECTORY = "http://test.com/example/";
        private static readonly string BASE_URL_WITH_2_SUBDIRECTORIES = "http://test.com/example/test/";
        private static readonly string BASE_URL_WITH_FILE = "http://test.com/example.php";
        private static readonly string BASE_URL_WITH_FILE_IN_SUBDIRECTORY = "http://test.com/example/test.php";
        private static readonly string INVALID_BASE_URL = "http://test./test";
        private static readonly string READABLE_INVALID_BASE_URL = "http://test/";
        private static readonly string URL_WITH_PORT = "http://localhost:8000/";
        
        [TestMethod]
        public void CAN_EXTRACT_BASE_URL()
        {
            var objectUnderTest = new WebCrawler(BASE_URL);
            var result = objectUnderTest.ExtractBaseUrl(BASE_URL);
            Assert.AreEqual(BASE_URL, result);
        }
        [TestMethod]
        public void CAN_EXTRACT_NOT_CLOSED_BASE_URL()
        {
            var objectUnderTest = new WebCrawler(BASE_URL_NOT_CLOSED);
            var result = objectUnderTest.ExtractBaseUrl(BASE_URL_NOT_CLOSED);
            Assert.AreEqual(BASE_URL, result);
        }
        [TestMethod]
        public void CAN_EXTRACT_BASE_URL_FROM_URL_WITH_DIRECTORY()
        {
            var objectUnderTest = new WebCrawler(BASE_URL_WITH_SUBDIRECTORY);
            var result = objectUnderTest.ExtractBaseUrl(BASE_URL_WITH_SUBDIRECTORY);
            Assert.AreEqual(BASE_URL, result);
        }
        [TestMethod]
        public void CAN_EXTRACT_BASE_URL_FROM_URL_WITH_MULTIPLE_SUBDIRECTORIES()
        {
            var objectUnderTest = new WebCrawler(BASE_URL_WITH_2_SUBDIRECTORIES);
            var result = objectUnderTest.ExtractBaseUrl(BASE_URL_WITH_2_SUBDIRECTORIES);
            Assert.AreEqual(BASE_URL, result);
        }
        [TestMethod]
        public void CAN_EXTRACT_BASE_URL_FROM_URL_WITH_FILE()
        {
            var objectUnderTest = new WebCrawler(BASE_URL_WITH_FILE);
            var result = objectUnderTest.ExtractBaseUrl(BASE_URL_WITH_FILE);
            Assert.AreEqual(BASE_URL, result);
        }
        [TestMethod]
        public void CAN_EXTRACT_BASE_URL_FROM_URL_WITH_FILE_IN_SUBDIRECTORY()
        {
            var objectUnderTest = new WebCrawler(BASE_URL_WITH_FILE_IN_SUBDIRECTORY);
            var result = objectUnderTest.ExtractBaseUrl(BASE_URL_WITH_FILE_IN_SUBDIRECTORY);
            Assert.AreEqual(BASE_URL, result);
        }
        [TestMethod]
        public void RETURNS_READABLE_URL_IN_CASE_OF_INVALID_URL()
        {
            var objectUnderTest = new WebCrawler(INVALID_BASE_URL);
            var result = objectUnderTest.ExtractBaseUrl(INVALID_BASE_URL);
            Assert.AreEqual(READABLE_INVALID_BASE_URL, result);
        }
        [TestMethod]
        public void RETURNS_VALID_URL_WITH_PORT()
        {
            var objectUnderTest = new WebCrawler(URL_WITH_PORT);
            var result = objectUnderTest.ExtractBaseUrl(URL_WITH_PORT);
            Assert.AreEqual(URL_WITH_PORT, result);
        }
    }
}
