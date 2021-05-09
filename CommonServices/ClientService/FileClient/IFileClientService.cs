using Common.DTOs;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CommonServices.ClientService.FileClient
{
    public interface IFileClientService
    {
        public Task<IRestResponse> DownloadFileFromRemote(DownloadReferenceDto downloadReferenceDto);
        public Task<Stream> DownloadFileFromLocal(int fileId);
        public Task<IRestResponse> RemoveFile(int id);
        public Task<IRestResponse> ValidateFileChecksum(int id);
        public Task<IEnumerable<FileReferenceDto>> GetFileReferences();
    }
}
