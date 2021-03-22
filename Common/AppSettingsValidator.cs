using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;

namespace Common
{
    public class AppSettingsValidator
    {
        private readonly IValidatorHelper _validatorHelper;
        public AppSettingsValidator(IValidatorHelper validatorHelper)
        {
            _validatorHelper = validatorHelper;
        }

        public bool IsFileStorePathValid(string fileStorePath)
        {
            if (fileStorePath.Equals("") || fileStorePath is null)
                throw new Exception();
            if (!Directory.Exists(fileStorePath))
                throw new Exception();
            if (!_validatorHelper.HaveRequiredPermissionsToFileStore(fileStorePath))
                throw new Exception();
            
            return true;
        }

        public bool IsLocalApiUrlValid(string localApiUrl)
        {
            if (localApiUrl.Equals("") || localApiUrl is null)
                throw new Exception();
            if (!_validatorHelper.IsLocalAddress(localApiUrl))
                throw new Exception("Invalid API address in configuration appsetings.json");

            return true;
        }

        public bool IsConnectionStringValid(string connectionString)
        {
            if (connectionString.Equals("") || connectionString is null)
                throw new Exception();
            if (!_validatorHelper.CanEstablishConnectionWithDatabase(connectionString))
                throw new Exception();

            return true;
        }
    }
}
