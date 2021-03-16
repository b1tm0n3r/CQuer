using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace CommonServicesTests
{
    public static class CommonMethods
    {
        public static IConfiguration CreateMockConfiguration()
        {
            string tempDirectory = Path.GetTempPath();
            var testConfiguration = new Dictionary<string, string>
            {
                { "DefaultFileStorePath", tempDirectory }
            };
            return new ConfigurationBuilder().AddInMemoryCollection(testConfiguration).Build();
        }
    }
}