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
        public async Task<ActionResult> DownloadFile(DownloadReferenceDto downloadReferenceDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _fileManagerService.DownloadFileFromSource(downloadReferenceDto.DownloadUrl);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TicketDto>> RemoveFile(int id)
        {
            if (!ModelState.IsValid)
                BadRequest(ModelState);

            await _fileManagerService.RemoveFile(id);
            return Ok();
        }

    }
}
