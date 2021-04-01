using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTests
{
    //TODO: merge this with common methods from CommonServicesTests
    public static class CommonMethods
    {
        private static readonly string RESOURCES_DIRECTORY = "resources";

        public static IConfiguration CreateMockConfigurationFromFile(string configurationFile)
        {
            
            string configurationFilePath = GetFilePathFromTestResources(configurationFile);

            return new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile(configurationFilePath)
                .Build();
        }

        public static string GetFilePathFromTestResources(string file)
        {
            string workingDirectory = Path.GetFullPath(Directory.GetCurrentDirectory());
            return workingDirectory + Path.DirectorySeparatorChar + RESOURCES_DIRECTORY + Path.DirectorySeparatorChar + file;
        }

        public static IConfiguration CreateConfigurationWithFileStorePath(string pathAddition = "")
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
