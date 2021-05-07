using AutoMapper;
using CommonServices;
using CommonServices.DatabaseOperator;
using CommonServices.FileManager;
using CommonServices.HttpWebProxy;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CommonServicesTests
{
    [TestClass]
    public class FileManagerServiceTests
    {
        private static readonly string TEST_FILESTORE = "TEST";
        const string RESOURCES_DIRECTORY = "resources";
        const string TEST_FILE = "testFile.txt";
        const string MODIFIED_TEST_FILE = "testFile2.txt";
        const string EXPECTED_TEST_FILE_SHA256_CHECKSUM = "1F0D0DFE1F134E4FDEB01A0627B127CE6BAC8D171220B0A9EF14D56E755A460A";
        [TestMethod]
        public void Can_Get_Correct_SHA256_Checksum()
        {
            var mockDbContext = new Mock<DbContextStub>();
            var mockHttpWebClientProxy = new Mock<IHttpWebClientProxy>();
            var mockMapper = new Mock<IMapper>();
            FileManagerService objectUnderTest = new FileManagerService(mockDbContext.Object, 
                mockHttpWebClientProxy.Object, mockMapper.Object, TEST_FILESTORE);

            string workingDirectory = Path.GetFullPath(Directory.GetCurrentDirectory());
            string testFilePath = workingDirectory + Path.DirectorySeparatorChar + RESOURCES_DIRECTORY + Path.DirectorySeparatorChar + TEST_FILE;
            var result = objectUnderTest.ComputeFileSHA256Checksum(testFilePath);

            Assert.IsNotNull(result);
            Assert.AreEqual(EXPECTED_TEST_FILE_SHA256_CHECKSUM, result);
        }

        [TestMethod]
        public void Modified_File_Has_Incorrect_Checksum()
        {
            var mockDbContext = new Mock<DbContextStub>();
            var mockHttpWebClientProxy = new Mock<IHttpWebClientProxy>();
            var mockMapper = new Mock<IMapper>();
            FileManagerService objectUnderTest = new FileManagerService(mockDbContext.Object,
                mockHttpWebClientProxy.Object, mockMapper.Object, TEST_FILESTORE);

            string workingDirectory = Path.GetFullPath(Directory.GetCurrentDirectory());
            string testFilePath = workingDirectory + Path.DirectorySeparatorChar + RESOURCES_DIRECTORY + Path.DirectorySeparatorChar + TEST_FILE;
            string modifiedTestFilePath = workingDirectory + Path.DirectorySeparatorChar + RESOURCES_DIRECTORY + Path.DirectorySeparatorChar + MODIFIED_TEST_FILE;

            var testFileResult = objectUnderTest.ComputeFileSHA256Checksum(testFilePath);
            var modifiedTestFileResult = objectUnderTest.ComputeFileSHA256Checksum(modifiedTestFilePath);

            Assert.IsNotNull(testFileResult);
            Assert.IsNotNull(modifiedTestFileResult);
            Assert.AreNotEqual(modifiedTestFileResult, testFileResult);
        }
    }
}