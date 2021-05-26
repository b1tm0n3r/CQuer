using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common;
using Microsoft.Extensions.Configuration;
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

            var objectUnderTest = new AppSettingsValidator(configuration, validatorHelperMock.Object);
            var result = objectUnderTest.IsConnectionStringValid();

            Assert.IsNotNull(result);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void RETURN_FALSE_IF_CANNOT_CONNECT_TO_DATABASE()
        {
            IConfiguration configuration = CommonMethods.CreateMockConfigurationFromFile(APPSETTINGS_WITH_INVALID_DATA);

            var validatorHelperMock = new Mock<IValidatorHelper>();
            var objectUnderTest = new AppSettingsValidator(configuration, validatorHelperMock.Object);
            var result = objectUnderTest.IsConnectionStringValid();

            Assert.IsNotNull(result);
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void RETURN_FALSE_WITH_EMPTY_CONNECTION_STRING()
        {
            IConfiguration configuration = CommonMethods.CreateMockConfigurationFromFile(APPSETTINGS_WITH_EMPTY_ENTITIES);

            var validatorHelperMock = new Mock<IValidatorHelper>();
            var objectUnderTest = new AppSettingsValidator(configuration, validatorHelperMock.Object);
            var result = objectUnderTest.IsConnectionStringValid();

            Assert.IsNotNull(result);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void RETURN_TRUE_IF_FILESTORE_PATH_IS_VALID()
        {
            IConfiguration configuration = CommonMethods.CreateConfigurationWithFileStorePath();
            var fileStorePath = configuration.GetValue<string>("DefaultFileStorePath");

            var validatorHelperMock = new Mock<IValidatorHelper>();
            validatorHelperMock.Setup(x => x.HaveRequiredPermissionsToFileStore(fileStorePath)).Returns(true);
            var objectUnderTest = new AppSettingsValidator(configuration, validatorHelperMock.Object);
            var result = objectUnderTest.IsFileStorePathValid();

            Assert.IsNotNull(result);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void RETURN_FALSE_WITHOUT_READ_WRITE_ACCESS_TO_FILESTORE()
        {
            IConfiguration configuration = CommonMethods.CreateConfigurationWithFileStorePath(TEST_DIRECTORY_PATH);
            var fileStorePath = configuration.GetValue<string>("DefaultFileStorePath");

            var validatorHelperMock = new Mock<IValidatorHelper>();
            validatorHelperMock.Setup(x => x.HaveRequiredPermissionsToFileStore(fileStorePath)).Returns(false);
            var objectUnderTest = new AppSettingsValidator(configuration, validatorHelperMock.Object);
            var result = objectUnderTest.IsFileStorePathValid();

            Assert.IsNotNull(result);
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void RETURN_FALSE_IF_FILESTORE_PATH_POINTS_NON_EXISTING_DIRECTORY()
        {
            IConfiguration configuration = CommonMethods.CreateConfigurationWithFileStorePath("/non/existing/directory");

            var validatorHelperMock = new Mock<IValidatorHelper>();
            var objectUnderTest = new AppSettingsValidator(configuration, validatorHelperMock.Object);
            var result = objectUnderTest.IsFileStorePathValid();

            Assert.IsNotNull(result);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void RETURN_TRUE_IF_LOCAL_API_URL_IS_VALID()
        {
            IConfiguration configuration = CommonMethods.CreateMockConfigurationFromFile(APPSETTINGS_WITH_VALID_DATA);
            var localApiUrl = configuration.GetValue<string>("CQuerLocalAPIURL");

            var validatorHelperMock = new Mock<IValidatorHelper>();
            validatorHelperMock.Setup(x => x.IsLocalAddress(localApiUrl)).Returns(true);
            var objectUnderTest = new AppSettingsValidator(configuration, validatorHelperMock.Object);
            var result = objectUnderTest.IsLocalApiUrlValid();

            Assert.IsNotNull(result);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void RETURN_FALSE_IF_LOCAL_API_URL_IS_INVALID()
        {
            IConfiguration configuration = CommonMethods.CreateMockConfigurationFromFile(APPSETTINGS_WITH_INVALID_DATA);

            var validatorHelperMock = new Mock<IValidatorHelper>();
            var objectUnderTest = new AppSettingsValidator(configuration, validatorHelperMock.Object);
            var result = objectUnderTest.IsLocalApiUrlValid();

            Assert.IsNotNull(result);
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void RETURN_FALSE_IF_LOCAL_API_URL_IS_EMPTY()
        {
            IConfiguration configuration = CommonMethods.CreateMockConfigurationFromFile(APPSETTINGS_WITH_INVALID_DATA);

            var validatorHelperMock = new Mock<IValidatorHelper>();
            var objectUnderTest = new AppSettingsValidator(configuration, validatorHelperMock.Object);
            var result = objectUnderTest.IsLocalApiUrlValid();

            Assert.IsNotNull(result);
            Assert.IsFalse(result);
        }
        
        [TestMethod]
        public void RETURN_FALSE_IF_DEFAULT_ADMIN_DETAILS_IS_EMPTY()
        {
            var configuration = CommonMethods.CreateMockConfigurationFromFile(APPSETTINGS_WITH_EMPTY_ENTITIES);
            
            var validatorHelperMock = new Mock<IValidatorHelper>();
            var objectUnderTest = new AppSettingsValidator(configuration, validatorHelperMock.Object);
            var result = objectUnderTest.IsDefaultAdminDetailsValid();

            Assert.IsNotNull(result);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void RETURN_TRUE_IF_DEFAULT_ADMIN_DETAILS_IS_VALID()
        {
            var configuration = CommonMethods.CreateMockConfigurationFromFile(APPSETTINGS_WITH_VALID_DATA);
            
            var validatorHelperMock = new Mock<IValidatorHelper>();
            var objectUnderTest = new AppSettingsValidator(configuration, validatorHelperMock.Object);
            var result = objectUnderTest.IsDefaultAdminDetailsValid();

            Assert.IsNotNull(result);
            Assert.IsTrue(result);
        }
    }
}
