using CommonServices.DatabaseOperator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace CommonServices
{
    class FileManager : IFileManager
    {
        private DatabaseConnector databaseConnector;
        private string basePath;
        public FileManager(DatabaseConnector databaseConnector, string basePath)
        {
            this.databaseConnector = databaseConnector;
            this.basePath = basePath;
        }
        public void DownloadFileFromSource(string source, string destinationPath)
        {
            using var webClient = new WebClient();

            webClient.DownloadFile(source, destinationPath);
        }
        public byte[] GetFileByName(string fileName)
        {
            return GetFile(GetFilePath(fileName));
        }
        public string GetFilePath(string fileName)
        {
            return databaseConnector.GetLocalFilePath(fileName);
        }
        private byte[] GetFile(string filePath)
        {
            return File.ReadAllBytes(filePath);
        }
    }
}
