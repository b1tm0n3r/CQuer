using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;

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