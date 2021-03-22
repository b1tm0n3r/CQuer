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
        private static readonly string TEST_FILE_NAME = "FileStore_Check.txt";
        private static readonly string TEST_FILE_DATA = "Temporary FileStore File";

        public bool IsLocalAddress(string localApiUrl)
        {
            var regex = new Regex(@"^https?://(127\.0\.0\.1|localhost):\d{1,5}");
            return regex.IsMatch(localApiUrl);
        }
        public bool CanEstablishConnectionWithDatabase(string connectionString)
        {
            try
            {
                using SqlConnection connection = new SqlConnection(connectionString);
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
    }
}
