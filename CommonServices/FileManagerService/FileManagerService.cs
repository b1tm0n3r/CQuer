using CommonServices.DatabaseOperator;
using CommonServices.HttpWebProxy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace CommonServices.FileManager
{
    public class FileManagerService : IFileManagerService
    {
        private readonly IDatabaseConnector _databaseConnector;
        private readonly IHttpWebClientProxy _httpWebClientProxy;
        public FileManagerService(IConfiguration configuration, IDatabaseConnector databaseConnector, IHttpWebClientProxy httpWebClientProxy)
        {
            _databaseConnector = databaseConnector;
            _httpWebClientProxy = httpWebClientProxy;
            var basePath = configuration.GetValue<string>("DefaultFileStorePath");
            if(!FilestoreExists(basePath))
            {
                throw new Exception("Filestore doesn't exist!");
            }
        }
        public void DownloadFileFromSource(string source, string destinationPath)
        {
            _httpWebClientProxy.DownloadFileFromUrl(source, destinationPath);
        }
        public byte[] GetFileByName(string fileName)
        {
            return GetFile(GetFilePath(fileName));
        }
        public string GetFilePath(string fileName)
        {
            return _databaseConnector.GetLocalFilePath(fileName);
        }
        private byte[] GetFile(string filePath)
        {
            return File.ReadAllBytes(filePath);
        }
        private bool FilestoreExists(string defaultFilestorePath)
        {
            return Directory.Exists(@defaultFilestorePath);
        }
        public string ComputeFileSHA256Checksum(string filePath)
        {
            using var sha256 = SHA256.Create();
            using var processedFile = File.OpenRead(filePath);
            return BitConverter.ToString(sha256.ComputeHash(processedFile)).Replace("-", String.Empty);
        }
    }
}
