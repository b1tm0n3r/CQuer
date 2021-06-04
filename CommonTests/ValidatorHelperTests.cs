using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common;
using Microsoft.Extensions.Configuration;

namespace CommonTests
{
    [TestClass]
    public class ValidatorHelperTests
    {
        private static readonly string APPSETTINGS_WITH_VALID_DATA = "ValidAppsettings.json";
        private static readonly string APPSETTINGS_WITH_INVALID_DATA = "InvalidAppsettings.json";

        [TestMethod]
        public void RETURN_TRUE_IF_HAVE_REQUIRED_PERMISSIONS_TO_FILESTORE()
        {
            IConfiguration configuration = CommonMethods.CreateConfigurationWithFileStorePath();
            var fileStorePath = configuration.GetValue<string>("DefaultFileStorePath");

            var objectUnderTest = new ValidatorHelper();
            var result = objectUnderTest.HaveRequiredPermissionsToFileStore(fileStorePath);

            Assert.IsNotNull(result);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void RETURN_TRUE_IF_LOCAL_API_URL_IS_LOCAL_ADDRESS()
        {
            IConfiguration configuration = CommonMethods.CreateMockConfigurationFromFile(APPSETTINGS_WITH_VALID_DATA);
            var localApiUrl = configuration.GetValue<string>("CQuerLocalAPIURL");

            var objectUnderTest = new ValidatorHelper();
            var result = objectUnderTest.IsLocalAddress(localApiUrl);

            Assert.IsNotNull(result);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void RETURN_FALSE_IF_LOCAL_API_URL_IS_INVALID()
        {
            IConfiguration configuration = CommonMethods.CreateMockConfigurationFromFile(APPSETTINGS_WITH_INVALID_DATA);
            var localApiUrl = configuration.GetValue<string>("CQuerLocalAPIURL");

            var objectUnderTest = new ValidatorHelper();
            var result = objectUnderTest.IsLocalAddress(localApiUrl);

            Assert.IsNotNull(result);
            Assert.IsFalse(result);
        }
    }
}
