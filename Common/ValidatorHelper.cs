using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Common
{
    public class ValidatorHelper : IValidatorHelper
    {
        private static readonly string CONNECTION_STRING_DATABASE = "Database";
        private static readonly string CONNECTION_STRING_INITIAL_CATALOG = "Initial Catalog";
        private static readonly string TEST_FILE_NAME = "FileStore_Check.txt";
        private static readonly string TEST_FILE_DATA = "Temporary FileStore File";

        public bool IsLocalAddress(string localApiUrl)
        {
            var regex = new Regex(@"^https?://(127\.0\.0\.1|localhost):\d{1,5}/api/$");
            return regex.IsMatch(localApiUrl);
        }
        public bool CanEstablishConnectionWithDatabase(string connectionString)
        {
            try
            {
                /*
                 * Default timeout value is 15 seconds and cannot be changed.
                 * Exception will be thrown in first 15 seconds from app startup 
                 * Note: applicable when invalid connection string is located in appsettings.json 
                 */
                var directSQLServerConnectionString = GetDirectSQLServerConnectionString(connectionString);
                using SqlConnection connection = new SqlConnection(directSQLServerConnectionString);
                connection.Open();
                connection.Dispose();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool HaveRequiredPermissionsToFileStore(string fileStorePath)
        {
            try
            {
                var filePath = Path.Combine(fileStorePath, TEST_FILE_NAME);
                File.WriteAllText(filePath, TEST_FILE_DATA);
                File.ReadAllText(filePath);
                File.Delete(filePath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private string GetDirectSQLServerConnectionString(string connectionString)
        {
            var splittedConnectionString = connectionString.Split(";");
            var directSQLServerConnectionStringBuilder = new StringBuilder();
            for (int i = 0; i < splittedConnectionString.Length; i++)
            {
                if (splittedConnectionString[i].StartsWith(CONNECTION_STRING_DATABASE) 
                    || splittedConnectionString[i].StartsWith(CONNECTION_STRING_INITIAL_CATALOG))
                    continue;

                directSQLServerConnectionStringBuilder.Append(splittedConnectionString[i]);

                if (i != splittedConnectionString.Length - 1)
                    directSQLServerConnectionStringBuilder.Append(";");
            }
            return directSQLServerConnectionStringBuilder.ToString();
        }
    }
}
