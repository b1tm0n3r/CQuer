using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CommonServices.FileServices.FileManager
{
    public interface IFileManagerService
    {
        void DownloadFileFromSource(string source, string destinationPath);
        string GetFilePath(string fileName);
        byte[] GetFileByName(string filePath);
        string ComputeFileSHA256Checksum(string filePath);
    }
}
