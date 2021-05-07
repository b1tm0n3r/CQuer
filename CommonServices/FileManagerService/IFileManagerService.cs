using Common.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CommonServices.FileManager
{
    public interface IFileManagerService
    {
        public Task<int> DownloadFileFromSource(string source);
        public Task<int> RemoveFile(int id);
        public IEnumerable<FileReferenceDto> GetFileReferences();
        public string GetFilePath(string fileName);
        public byte[] GetFileByName(string filePath);
        public string ComputeFileSHA256Checksum(string filePath);
    }
}
