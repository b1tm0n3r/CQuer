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
        public Task<int> DownloadFileFromSource(DownloadReferenceDto downloadReferenceDto);
        public Task<int> RemoveFileWithReference(int id);
        public Task<int> RemoveAssociatedFilesWithReferences(int ticketId);
        public IEnumerable<FileReferenceDto> GetFileReferences();
        public Task<bool> VerifyChecksum(int fileId);
        public FileStream GetFileByReferenceId(int referenceId);
        public string ComputeFileSHA256Checksum(string filePath);
    }
}
