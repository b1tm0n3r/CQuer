using AutoMapper;
using Common.DataModels.StandardEntities;
using Common.DTOs;
using CommonServices.DatabaseOperator;
using CommonServices.HttpWebProxy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CommonServices.FileManager
{
    public class FileManagerService : IFileManagerService
    {
        private readonly string BASE_FILESTORE_PATH;
        private readonly ICQuerDbContext _dbContext;
        private readonly IHttpWebClientProxy _httpWebClientProxy;
        private readonly IMapper _mapper;
        public FileManagerService(ICQuerDbContext dbContext, IHttpWebClientProxy httpWebClientProxy,
            IMapper mapper, string filestorePath)
        {
            _dbContext = dbContext;
            _httpWebClientProxy = httpWebClientProxy;
            _mapper = mapper;
            BASE_FILESTORE_PATH = filestorePath;
        }
        public async Task<int> DownloadFileFromSource(DownloadReferenceDto downloadReferenceDto)
        {
            string fileName = downloadReferenceDto.DownloadUrl.Split("/").Last();
            string filePath = BASE_FILESTORE_PATH + Path.DirectorySeparatorChar + fileName;
            _httpWebClientProxy.DownloadFileFromUrl(downloadReferenceDto.DownloadUrl, filePath);
            var result = await _dbContext.FileReferences.AddAsync(new FileReference()
            {
                TicketId = downloadReferenceDto.TicketId,
                FileName = fileName,
                Path = filePath,
                Sha256Checksum = ComputeFileSHA256Checksum(filePath),
                UploadDate = DateTime.Now,
                ChecksumMatchWithDeclared = false,
                ChecksumMatchWithRemote = false
            });
            await _dbContext.SaveChangesAsync();
            return result.Entity.Id;
        }

        public async Task<int> RemoveFileWithReference(int id)
        {
            var fileReference = _dbContext.FileReferences.FirstOrDefault(x => x.Id == id);
            RemoveFile(fileReference.Path);
            _dbContext.FileReferences.Remove(fileReference);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> RemoveAssociatedFilesWithReferences(int ticketId)
        {
            var fileReferences = _dbContext.FileReferences.Where(x => x.TicketId == ticketId).ToList();
            fileReferences.ForEach(file => RemoveFile(file.Path));
            _dbContext.FileReferences.RemoveRange(fileReferences);
            return await _dbContext.SaveChangesAsync();
        }

        public IEnumerable<FileReferenceDto> GetFileReferences()
        {
            var fileReferences = _dbContext.FileReferences.ToList();
            var fileReferenceDtos = _mapper.Map<IEnumerable<FileReferenceDto>>(fileReferences);

            return fileReferenceDtos;
        }

        public async Task<bool> VerifyChecksum(int fileId)
        {
            var processedFileReference = _dbContext.FileReferences.FirstOrDefault(x => x.Id == fileId);
            var processedTicket = _dbContext.Tickets.FirstOrDefault(x => x.Id == processedFileReference.TicketId);

            var directDownloadUrl = processedTicket.DownloadUrl;

            if (processedTicket.Sha256Checksum != null)
                processedFileReference.ChecksumMatchWithDeclared = processedFileReference.Sha256Checksum.Equals(processedTicket.Sha256Checksum);

            TryValidateAgainstRemoteChecksum(processedFileReference, directDownloadUrl);
            var recordChanged = processedFileReference.ChecksumMatchWithDeclared || processedFileReference.ChecksumMatchWithRemote;

            if (recordChanged)
                await _dbContext.SaveChangesAsync();
            
            return recordChanged;
        }

        private void TryValidateAgainstRemoteChecksum(FileReference processedFileReference, string directDownloadUrl)
        {
            var baseDownloadPageUrl = directDownloadUrl.Substring(0, directDownloadUrl.LastIndexOf("/") + 1);
            string sha256Checksum;
            if (_httpWebClientProxy.TryDownloadSha256ChecksumFromFile(directDownloadUrl + ".sha256", out sha256Checksum) ||
                _httpWebClientProxy.TryDownloadSha256ChecksumFromFile(directDownloadUrl + ".sha256sum", out sha256Checksum))
            {
                processedFileReference.ChecksumMatchWithRemote = processedFileReference.Sha256Checksum.Equals(sha256Checksum);
            }
            else if (_httpWebClientProxy.TryExtractSha256ChecksumFromPage(baseDownloadPageUrl, directDownloadUrl, out sha256Checksum))
            {
                processedFileReference.ChecksumMatchWithRemote = processedFileReference.Sha256Checksum.Equals(sha256Checksum);
            }
        }
        public byte[] GetFileByReferenceId(int referenceId)
        {
            var filePath = _dbContext.FileReferences.FirstOrDefault(x => x.Id == referenceId).Path;
            return GetFile(filePath);
        }
        private byte[] GetFile(string filePath)
        {
            return File.ReadAllBytes(filePath);
        }
        private void RemoveFile(string filePath)
        {
            File.Delete(filePath);
        }
        public string ComputeFileSHA256Checksum(string filePath)
        {
            using var sha256 = SHA256.Create();
            using var processedFile = File.OpenRead(filePath);
            return BitConverter.ToString(sha256.ComputeHash(processedFile)).Replace("-", string.Empty);
        }
    }
}
