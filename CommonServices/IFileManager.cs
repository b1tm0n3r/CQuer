using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CommonServices
{
    interface IFileManager
    {
        void DownloadFileFromSource(string source, string destinationPath);
        string GetFilePath(string fileName);
        byte[] GetFileByName(string filePath);
    }
}
