using System;
using System.IO;
using CommonServices;
using CommonServices.DatabaseOperator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CommonServicesTests
{
    [TestClass]
    public class FileManagerServiceTests
    {
        const string RESOURCES_DIRECTORY = "resources";
        const string TEST_FILE = "testFile.txt";
        const string EXPECTED_TEST_FILE_SHA256_CHECKSUM = "1F0D0DFE1F134E4FDEB01A0627B127CE6BAC8D171220B0A9EF14D56E755A460A";        
        [TestMethod]
        public void Can_Get_Correct_SHA256_Checksum()
        {
            var mockConfiguration = CommonMethods.CreateMockConfiguration();
            var mockDatabaseConnector = new Mock<IDatabaseConnector>();
            FileManagerService objectUnderTest = new FileManagerService(mockConfiguration, mockDatabaseConnector.Object);

            string workingDirectory = Path.GetFullPath(Directory.GetCurrentDirectory());
            Console.WriteLine(workingDirectory);
            var result = objectUnderTest.ComputeFileSHA256Checksum(workingDirectory + Path.DirectorySeparatorChar + RESOURCES_DIRECTORY + Path.DirectorySeparatorChar + TEST_FILE);

            Assert.AreEqual(EXPECTED_TEST_FILE_SHA256_CHECKSUM, result);
        }
    }
}