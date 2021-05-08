using CommonServices.HttpWebProxy;
using CommonTests;
using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServicesTests
{
    [TestClass]
    public class WebScraperTests
    {
        private static readonly string DOCUMENT_WITH_INLINE_CHECKSUMS = "documentWithInlineChecksum.html";
        private static readonly string DOCUMENT_WITHOUT_CHECKSUMS = "documentWithoutChecksum.html";
        private static readonly string FIRST_INLINE_URL = "http://127.0.0.1/test.exe";
        private static readonly string BASIC_TABLE_URL = "http://127.0.0.1/test2.exe";
        private static readonly string SECOND_INLINE_URL = "http://127.0.0.1/test3.exe";
        private static readonly string TAG_IN_TABLE_URL = "http://127.0.0.1/test4.exe";

        [TestMethod]
        public void CAN_RESOLVE_INLINE_SHA256_CHECKSUM()
        {
            SimpleWebScraper objectUnderTest = new SimpleWebScraper();
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.Load(CommonMethods.GetFilePathFromTestResources(DOCUMENT_WITH_INLINE_CHECKSUMS));

            var result = objectUnderTest.TryGetSha256FromHtml(htmlDocument, FIRST_INLINE_URL, out string checksum);

            Assert.IsNotNull(result);
            Assert.IsNotNull(checksum);
            Assert.IsTrue(result);
            Assert.AreEqual("11A7432AE485206B8AC935BB5A02917B1CFB12D1F48D97143A211ED0F418095C", checksum);
        }

        [TestMethod]
        public void CAN_RESOLVE_INLINE_SHA256_CHECKSUM_FROM_TABLE()
        {
            SimpleWebScraper objectUnderTest = new SimpleWebScraper();
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.Load(CommonMethods.GetFilePathFromTestResources(DOCUMENT_WITH_INLINE_CHECKSUMS));

            var result = objectUnderTest.TryGetSha256FromHtml(htmlDocument, BASIC_TABLE_URL, out string checksum);

            Assert.IsNotNull(result);
            Assert.IsNotNull(checksum);
            Assert.IsTrue(result);
            Assert.AreEqual("8C5488AA64E12AA01D5A3B8CD3E5BCE09CE04860DFA0A2D0F47DD9E330D43EB7", checksum);
        }

        [TestMethod]
        public void CAN_RESOLVE_SECOND_INLINE_SHA256_CHECKSUM_FROM_TABLE()
        {
            SimpleWebScraper objectUnderTest = new SimpleWebScraper();
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.Load(CommonMethods.GetFilePathFromTestResources(DOCUMENT_WITH_INLINE_CHECKSUMS));

            var result = objectUnderTest.TryGetSha256FromHtml(htmlDocument, SECOND_INLINE_URL, out string checksum);

            Assert.IsNotNull(result);
            Assert.IsNotNull(checksum);
            Assert.IsTrue(result);
            Assert.AreEqual("0C4824D1EFB19CCDCC4A896BDE56373FCBFA9350507FAED27297BE764EDCDA99", checksum);
        }

        [TestMethod]
        public void CAN_RESOLVE_INLINE_SHA256_CHECKSUM_IN_ADDITIONAL_TAG_FROM_TABLE()
        {
            SimpleWebScraper objectUnderTest = new SimpleWebScraper();
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.Load(CommonMethods.GetFilePathFromTestResources(DOCUMENT_WITH_INLINE_CHECKSUMS));

            var result = objectUnderTest.TryGetSha256FromHtml(htmlDocument, TAG_IN_TABLE_URL, out string checksum);

            Assert.IsNotNull(result);
            Assert.IsNotNull(checksum);
            Assert.IsTrue(result);
            Assert.AreEqual("98F718C661BB03FD00D0F191C6F2F55083AE4CCE61964964E50309D2416FDD48", checksum);
        }

        [TestMethod]
        public void RETURN_FALSE_IF_DOCUMENT_WITH_NO_CHECKSUMS()
        {
            SimpleWebScraper objectUnderTest = new SimpleWebScraper();
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.Load(CommonMethods.GetFilePathFromTestResources(DOCUMENT_WITHOUT_CHECKSUMS));

            var result = objectUnderTest.TryGetSha256FromHtml(htmlDocument, TAG_IN_TABLE_URL, out string checksum);

            Assert.IsFalse(result);
            Assert.AreEqual(string.Empty, checksum);
        }
    }
}
