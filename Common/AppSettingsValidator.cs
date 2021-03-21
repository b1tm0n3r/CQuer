using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public static class AppSettingsValidator
    {
        public static bool IsFileStorePathValid(string fileStorePath)
        {
            return true;
        }
        public static bool IsLocalApiUrlValid(string localApiUrl)
        {
            return true;
        }

        public static bool IsConnectionStringValid(string databaseConnectionString)
        {
            return true;
        }
        private static bool CanEstablishConnectionWithDatabase()
        {
            return true;
        }
    }
}
