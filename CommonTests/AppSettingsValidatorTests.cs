using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;
using System.Collections.Generic;

namespace CommonTests
{
    [TestClass]
    public class AppSettingsValidatorTests
    {
        private static readonly string RESOURCES_DIRECTORY = "resources";
        private static readonly string APPSETTINGS_WITH_VALID_DATA = "ValidAppsettings.json";
        private static readonly string APPSETTINGS_WITH_INVALID_DATA = "InvalidAppsettings.json";
        private static readonly string APPSETTINGS_WITH_EMPTY_ENTITIES = "EmptyEntitiesAppsettings.json";

        [TestMethod]
        public void RETURN_TRUE_IF_CONNECTION_STRING_IS_VALID()
        {
            IConfiguration configuration = CreateMockConfigurationFromFile(APPSETTINGS_WITH_VALID_DATA);
            var connectionString = configuration.GetConnectionString("CQuerDB");
            var result = AppSettingsValidator.IsConnectionStringValid(connectionString);
            Assert.IsNotNull(result);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void THROWS_EXCEPTION_IF_CANNOT_CONNECT_TO_DATABASE()
        {
            IConfiguration configuration = CreateMockConfigurationFromFile(APPSETTINGS_WITH_INVALID_DATA);
            var connectionString = configuration.GetConnectionString("CQuerDB");
            Assert.ThrowsException<Exception>(() => { AppSettingsValidator.IsConnectionStringValid(connectionString); }, 
                "Cannot establish connection with database using connection string from appsettings.json");
        }
        [TestMethod]
        public void THROWS_EXCEPTION_WITH_EMPTY_CONNECTION_STRING()
        {
            IConfiguration configuration = CreateMockConfigurationFromFile(APPSETTINGS_WITH_EMPTY_ENTITIES);
            var connectionString = configuration.GetConnectionString("CQuerDB");
            Assert.ThrowsException<Exception>(() => { AppSettingsValidator.IsConnectionStringValid(connectionString); },
                "Empty Connection String in appsettings.json");
        }

        [TestMethod]
        public void RETURN_TRUE_IF_FILESTORE_PATH_IS_VALID()
        {
            IConfiguration configuration = CreateConfigurationWithFileStorePath();
            var fileStorePath = configuration.GetValue<string>("DefaultFileStorePath");
            var result = AppSettingsValidator.IsFileStorePathValid(fileStorePath);
            Assert.IsNotNull(result);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void THROWS_EXCEPTION_WITHOUT_READ_WRITE_ACCESS_TO_FILESTORE()
        {
            IConfiguration configuration = CreateConfigurationWithFileStorePath();
            var fileStorePath = configuration.GetValue<string>("DefaultFileStorePath");
            Assert.ThrowsException<Exception>(() => { AppSettingsValidator.IsFileStorePathValid(fileStorePath); },
                "Missing Read/Write privileges to directory set as FileStore in appsettings.json");
        }
        [TestMethod]
        public void THROWS_EXCEPTION_IF_FILESTORE_PATH_POINTS_NON_EXISTING_DIRECTORY()
        {
            IConfiguration configuration = CreateConfigurationWithFileStorePath();
            var fileStorePath = configuration.GetValue<string>("DefaultFileStorePath");
            Assert.ThrowsException<Exception>(() => { AppSettingsValidator.IsFileStorePathValid(fileStorePath); },
                "FileStore path from appsettings.json points to non-existing directory!");
        }

        [TestMethod]
        public void RETURN_TRUE_IF_LOCAL_API_URL_IS_VALID()
        {
            IConfiguration configuration = CreateMockConfigurationFromFile(APPSETTINGS_WITH_VALID_DATA);
            var localApiUrl = configuration.GetValue<string>("CQuerLocalAPIURL");
            var result = AppSettingsValidator.IsLocalApiUrlValid(localApiUrl);
            Assert.IsNotNull(result);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void THROWS_EXCEPTION_IF_LOCAL_API_URL_IS_INVALID()
        {
            IConfiguration configuration = CreateMockConfigurationFromFile(APPSETTINGS_WITH_INVALID_DATA);
            var localApiUrl = configuration.GetValue<string>("CQuerLocalAPIURL");
            Assert.ThrowsException<Exception>(() => { AppSettingsValidator.IsLocalApiUrlValid(localApiUrl); },
                "CQuer local API URL from appsettings.json is not valid!");
        }
        [TestMethod]
        public void THROWS_EXCEPTION_IF_LOCAL_API_URL_IS_EMPTY()
        {
            IConfiguration configuration = CreateMockConfigurationFromFile(APPSETTINGS_WITH_INVALID_DATA);
            var localApiUrl = configuration.GetValue<string>("CQuerLocalAPIURL");
            Assert.ThrowsException<Exception>(() => { AppSettingsValidator.IsLocalApiUrlValid(localApiUrl); },
                "CQuer local API URL from appsettings.json is empty!");
        }
        private static IConfiguration CreateMockConfigurationFromFile(string configurationFile)
        {
            string workingDirectory = Path.GetFullPath(Directory.GetCurrentDirectory());
            string configurationFilePath = workingDirectory + Path.DirectorySeparatorChar + RESOURCES_DIRECTORY + Path.DirectorySeparatorChar + configurationFile;

            return new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile(configurationFilePath)
                .Build();
        }

        private static IConfiguration CreateConfigurationWithFileStorePath(string pathAddition = "")
        {
            string tempDirectory = Path.GetTempPath() + pathAddition;
            var testConfiguration = new Dictionary<string, string>
            {
                { "DefaultFileStorePath", tempDirectory }
            };
            return new ConfigurationBuilder().AddInMemoryCollection(testConfiguration).Build();
        }
    }
}
