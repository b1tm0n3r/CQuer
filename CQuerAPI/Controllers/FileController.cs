using Common.DTOs;
using CommonServices.FileManager;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQuerAPI.Controllers
{
    public class FileController : BaseController
    {
        private IFileManagerService _fileManagerService;
        public FileController(IFileManagerService fileManagerService)
        {
            _fileManagerService = fileManagerService;
        }
        [HttpGet]
        public ActionResult<IEnumerable<FileReferenceDto>> GetFileReferences()
        {
            var result = _fileManagerService.GetFileReferences();
            return result.ToList();
        }

        [HttpPost]
        public async Task<ActionResult<int>> DownloadFileFromRemote(DownloadReferenceDto downloadReferenceDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var fileReferenceId = await _fileManagerService.DownloadFileFromSource(downloadReferenceDto);
            return Ok(fileReferenceId);
        }
        [HttpGet("{id}")]
        public FileStreamResult ShareFile(int id)
        {
            var fileBytes = _fileManagerService.GetFileByReferenceId(id);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveFile(int id)
        {
            if (!ModelState.IsValid)
                BadRequest(ModelState);

            await _fileManagerService.RemoveFileWithReference(id);
            return Ok();
        }

        [HttpPut("{id}/validate")]
        public async Task<ActionResult> ValidateChecksum(int id)
        {
            if (!ModelState.IsValid)
                BadRequest(ModelState);

            await _fileManagerService.VerifyChecksum(id);
            return Ok();
        }
    }
}
