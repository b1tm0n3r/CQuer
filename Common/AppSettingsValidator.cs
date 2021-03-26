﻿using System;
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
                ErrorMessageContainer = Resources.ConfigValidatorErrorMessages.EmptyFS;
                return false;
            }
            if (!Directory.Exists(fileStorePath))
            {
                ErrorMessageContainer = Resources.ConfigValidatorErrorMessages.FSNotExists;
                return false;
            }
            if (!_validatorHelper.HaveRequiredPermissionsToFileStore(fileStorePath))
            {
                ErrorMessageContainer = Resources.ConfigValidatorErrorMessages.FSInvalidPrivileges;
                return false;
            }
            
            return true;
        }

        public bool IsLocalApiUrlValid()
        {
            var localApiUrl = _configuration.GetValue<string>("CQuerLocalAPIURL");

            if (localApiUrl.Equals("") || localApiUrl is null)
            {
                ErrorMessageContainer = Resources.ConfigValidatorErrorMessages.EmptyAPI;
                return false;
            }
            if (!_validatorHelper.IsLocalAddress(localApiUrl))
            {
                ErrorMessageContainer = Resources.ConfigValidatorErrorMessages.APINotLocal;
                return false;
            }

            return true;
        }

        public bool IsConnectionStringValid()
        {
            var connectionString = _configuration.GetConnectionString("CQuerDB");

            if (connectionString.Equals("") || connectionString is null)
            {
                ErrorMessageContainer = Resources.ConfigValidatorErrorMessages.EmptyConnectionString;
                return false;
            }
            if (!_validatorHelper.CanEstablishConnectionWithDatabase(connectionString))
            {
                ErrorMessageContainer = Resources.ConfigValidatorErrorMessages.DBConnFailed;
                return false;
            }

            return true;
        }
    }
}
