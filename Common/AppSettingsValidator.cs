using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Diagnostics;

namespace Common
{
    public class AppSettingsValidator
    {
        private readonly IConfiguration _configuration;
        private readonly IValidatorHelper _validatorHelper;

        public string ErrorMessageContainer { get; set; }

        public AppSettingsValidator(IConfiguration configuration, IValidatorHelper validatorHelper)
        {
            _validatorHelper = validatorHelper;
            _configuration = configuration;
        }
        public bool IsFileStorePathValid()
        {
            var fileStorePath = _configuration.GetValue<string>("DefaultFileStorePath");

            if (fileStorePath.Equals("") || fileStorePath is null)
            {
                ErrorMessageContainer = "Default File Store Path from appsettings.json is empty!";
                return false;
            }
            if (!Directory.Exists(fileStorePath))
            {
                ErrorMessageContainer = "FileStore path from appsettings.json points to non-existing directory!";
                return false;
            }
            if (!_validatorHelper.HaveRequiredPermissionsToFileStore(fileStorePath))
            {
                ErrorMessageContainer = "Missing Read/Write privileges to directory set as FileStore in appsettings.json";
                return false;
            }
            
            return true;
        }

        public bool IsLocalApiUrlValid()
        {
            var localApiUrl = _configuration.GetValue<string>("CQuerLocalAPIURL");

            if (localApiUrl.Equals("") || localApiUrl is null)
            {
                ErrorMessageContainer = "CQuer Local API URL from appsettings.json is empty!";
                return false;
            }
            if (!_validatorHelper.IsLocalAddress(localApiUrl))
            {
                ErrorMessageContainer = "CQuer local API URL from appsettings.json is not valid! It should point to localhost or loopback IP address i.e. https://127.0.0.1:4444";
                return false;
            }

            return true;
        }

        public bool IsConnectionStringValid()
        {
            var connectionString = _configuration.GetConnectionString("CQuerDB");

            if (connectionString.Equals("") || connectionString is null)
            {
                ErrorMessageContainer = "Connection string from appsettings.json is empty";
                return false;
            }
            if (!_validatorHelper.CanEstablishConnectionWithDatabase(connectionString))
            {
                ErrorMessageContainer = "Cannot establish connection with database using connection string from appsettings.json";
                return false;
            }

            return true;
        }
    }
}
