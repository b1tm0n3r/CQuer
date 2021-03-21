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

        public static bool IsConnectionStringValid(string connectionString)
        {
            CanEstablishConnectionWithDatabase(connectionString);
            return true;
        }
        private static bool CanEstablishConnectionWithDatabase(string connectionString)
        {
            return true;
        }
    }
}
