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
        public async Task<int> DownloadFileFromSource(string source)
        {
            string fileName = source.Split("/").Last();
            string filePath = BASE_FILESTORE_PATH + Path.DirectorySeparatorChar + fileName;
            _httpWebClientProxy.DownloadFileFromUrl(source, filePath);
            await _dbContext.FileReferences.AddAsync(new FileReference()
            {
                FileName = fileName,
                Path = filePath,
                SHA256Hash = ComputeFileSHA256Checksum(filePath),
                UploadDate = DateTime.Now
            });
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> RemoveFile(int id)
        {
            var fileReference = _dbContext.FileReferences.FirstOrDefault(x => x.Id == id);
            RemoveFile(fileReference.Path);
            _dbContext.FileReferences.Remove(fileReference);
            return await _dbContext.SaveChangesAsync();
        }

        public IEnumerable<FileReferenceDto> GetFileReferences()
        {
            var fileReferences = _dbContext.FileReferences.ToList();
            var fileReferenceDtos = _mapper.Map<IEnumerable<FileReferenceDto>>(fileReferences);

            return fileReferenceDtos;
        }

        public byte[] GetFileByName(string fileName)
        {
            return GetFile(GetFilePath(fileName));
        }
        public string GetFilePath(string fileName)
        {
            return _dbContext.FileReferences.FirstOrDefault(x => x.FileName.Equals(fileName)).Path;
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
