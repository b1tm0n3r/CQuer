using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using Moq;

namespace CommonTests
{
    [TestClass]
    public class AppSettingsValidatorTests
    {
        private static readonly string TEST_DIRECTORY_PATH = "/test";
        private static readonly string APPSETTINGS_WITH_VALID_DATA = "ValidAppsettings.json";
        private static readonly string APPSETTINGS_WITH_INVALID_DATA = "InvalidAppsettings.json";
        private static readonly string APPSETTINGS_WITH_EMPTY_ENTITIES = "EmptyEntitiesAppsettings.json";

        [TestMethod]
        public void RETURN_TRUE_IF_CONNECTION_STRING_IS_VALID()
        {
            IConfiguration configuration = CommonMethods.CreateMockConfigurationFromFile(APPSETTINGS_WITH_VALID_DATA);
            var connectionString = configuration.GetConnectionString("CQuerDB");
            var validatorHelperMock = new Mock<IValidatorHelper>();
            validatorHelperMock.Setup(x => x.CanEstablishConnectionWithDatabase(connectionString)).Returns(true);

            var objectUnderTest = new AppSettingsValidator(validatorHelperMock.Object);
            var result = objectUnderTest.IsConnectionStringValid(connectionString);

            Assert.IsNotNull(result);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void THROWS_EXCEPTION_IF_CANNOT_CONNECT_TO_DATABASE()
        {
            IConfiguration configuration = CommonMethods.CreateMockConfigurationFromFile(APPSETTINGS_WITH_INVALID_DATA);
            var connectionString = configuration.GetConnectionString("CQuerDB");

            var validatorHelperMock = new Mock<IValidatorHelper>();
            var objectUnderTest = new AppSettingsValidator(validatorHelperMock.Object);

            Assert.ThrowsException<Exception>(() => { objectUnderTest.IsConnectionStringValid(connectionString); }, 
                "Cannot establish connection with database using connection string from appsettings.json");
        }
        [TestMethod]
        public void THROWS_EXCEPTION_WITH_EMPTY_CONNECTION_STRING()
        {
            IConfiguration configuration = CommonMethods.CreateMockConfigurationFromFile(APPSETTINGS_WITH_EMPTY_ENTITIES);
            var connectionString = configuration.GetConnectionString("CQuerDB");

            var validatorHelperMock = new Mock<IValidatorHelper>();
            var objectUnderTest = new AppSettingsValidator(validatorHelperMock.Object);

            Assert.ThrowsException<Exception>(() => { objectUnderTest.IsConnectionStringValid(connectionString); },
                "Empty Connection String in appsettings.json");
        }

        [TestMethod]
        public void RETURN_TRUE_IF_FILESTORE_PATH_IS_VALID()
        {
            IConfiguration configuration = CommonMethods.CreateConfigurationWithFileStorePath();
            var fileStorePath = configuration.GetValue<string>("DefaultFileStorePath");

            var validatorHelperMock = new Mock<IValidatorHelper>();
            validatorHelperMock.Setup(x => x.HaveRequiredPermissionsToFileStore(fileStorePath)).Returns(true);
            var objectUnderTest = new AppSettingsValidator(validatorHelperMock.Object);
            var result = objectUnderTest.IsFileStorePathValid(fileStorePath);

            Assert.IsNotNull(result);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void THROWS_EXCEPTION_WITHOUT_READ_WRITE_ACCESS_TO_FILESTORE()
        {
            IConfiguration configuration = CommonMethods.CreateConfigurationWithFileStorePath(TEST_DIRECTORY_PATH);
            var fileStorePath = configuration.GetValue<string>("DefaultFileStorePath");

            var validatorHelperMock = new Mock<IValidatorHelper>();
            validatorHelperMock.Setup(x => x.HaveRequiredPermissionsToFileStore(fileStorePath)).Returns(false);
            var objectUnderTest = new AppSettingsValidator(validatorHelperMock.Object);

            Assert.ThrowsException<Exception>(() => { objectUnderTest.IsFileStorePathValid(fileStorePath); },
                "Missing Read/Write privileges to directory set as FileStore in appsettings.json");
        }
        [TestMethod]
        public void THROWS_EXCEPTION_IF_FILESTORE_PATH_POINTS_NON_EXISTING_DIRECTORY()
        {
            IConfiguration configuration = CommonMethods.CreateConfigurationWithFileStorePath("/non/existing/directory");
            var fileStorePath = configuration.GetValue<string>("DefaultFileStorePath");

            var validatorHelperMock = new Mock<IValidatorHelper>();
            var objectUnderTest = new AppSettingsValidator(validatorHelperMock.Object);

            Assert.ThrowsException<Exception>(() => { objectUnderTest.IsFileStorePathValid(fileStorePath); },
                "FileStore path from appsettings.json points to non-existing directory!");
        }

        [TestMethod]
        public void RETURN_TRUE_IF_LOCAL_API_URL_IS_VALID()
        {
            IConfiguration configuration = CommonMethods.CreateMockConfigurationFromFile(APPSETTINGS_WITH_VALID_DATA);
            var localApiUrl = configuration.GetValue<string>("CQuerLocalAPIURL");

            var validatorHelperMock = new Mock<IValidatorHelper>();
            validatorHelperMock.Setup(x => x.IsLocalAddress(localApiUrl)).Returns(true);
            var objectUnderTest = new AppSettingsValidator(validatorHelperMock.Object);
            var result = objectUnderTest.IsLocalApiUrlValid(localApiUrl);

            Assert.IsNotNull(result);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void THROWS_EXCEPTION_IF_LOCAL_API_URL_IS_INVALID()
        {
            IConfiguration configuration = CommonMethods.CreateMockConfigurationFromFile(APPSETTINGS_WITH_INVALID_DATA);
            var localApiUrl = configuration.GetValue<string>("CQuerLocalAPIURL");

            var validatorHelperMock = new Mock<IValidatorHelper>();
            var objectUnderTest = new AppSettingsValidator(validatorHelperMock.Object);

            Assert.ThrowsException<Exception>(() => { objectUnderTest.IsLocalApiUrlValid(localApiUrl); },
                "CQuer local API URL from appsettings.json is not valid!");
        }
        [TestMethod]
        public void THROWS_EXCEPTION_IF_LOCAL_API_URL_IS_EMPTY()
        {
            IConfiguration configuration = CommonMethods.CreateMockConfigurationFromFile(APPSETTINGS_WITH_INVALID_DATA);
            var localApiUrl = configuration.GetValue<string>("CQuerLocalAPIURL");

            var validatorHelperMock = new Mock<IValidatorHelper>();
            var objectUnderTest = new AppSettingsValidator(validatorHelperMock.Object);

            Assert.ThrowsException<Exception>(() => { objectUnderTest.IsLocalApiUrlValid(localApiUrl); },
                "CQuer local API URL from appsettings.json is empty!");
        }
    }
}
