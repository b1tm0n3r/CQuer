using Common.DataModels.IdentityManagement;
using Common.DTOs;
using CommonServices.ClientService.FileClient;
using CommonServices.ClientService.TicketClient;
using CQuerMVC.Helpers;
using CQuerMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CQuerMVC.Controllers
{
    [Authorize]
    public class FilesController : Controller
    {
        private readonly IFileClientService _fileClientService;
        private readonly ITicketClientService _ticketClientService;

        public FilesController(IFileClientService fileClientService, ITicketClientService ticketClientService)
        {
            _fileClientService = fileClientService;
            _ticketClientService = ticketClientService;
        }

        public async Task<IActionResult> Index()
        {
            var fileReferences = await _fileClientService.GetFileReferences();
            var viewModel = new FilesViewModel()
            {
                Files = fileReferences
            };
            return View(viewModel);
        }
        [HttpPost]
        [EnumAuthorizeRole(AccountType.Administrator)]
        public async Task<IActionResult> Resolver(DownloadReferenceDto downloadReferenceDto)
        {
            var ticketReference = await _ticketClientService.GetTicket(downloadReferenceDto.TicketId);
            if (ticketReference is null || !IsProcessedFileReferenceValid(ticketReference, downloadReferenceDto))
                return RedirectToAction("Error");

            _ = Task.Run(() => ResolveFileReference(downloadReferenceDto));

            return RedirectToAction("Index");
        }

        [HttpPost]
        [EnumAuthorizeRole(AccountType.Administrator)]
        public IActionResult ValidateWithCrawler(int id)
        {
            _ = Task.Run(() => _fileClientService.ValidateFileChecksumWithCrawler(id));

            return RedirectToAction("Index");
        }

        private async Task ResolveFileReference(DownloadReferenceDto downloadReferenceDto)
        {
            var response = await _fileClientService.DownloadFileFromRemote(downloadReferenceDto);
            await _ticketClientService.FinalizeTicket(downloadReferenceDto.TicketId);
            await _fileClientService.ValidateFileChecksum(int.Parse(response.Content));
        }

        public async Task<FileResult> Download(int id, string fileName)
        {
            var fileStream = await _fileClientService.DownloadFileFromLocal(id);
            return File(fileStream, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        public IActionResult Delete(int id)
        {
            return View(new IdViewModel(id));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(IdViewModel idViewModel)
        {
            await _fileClientService.RemoveFile(idViewModel.Id);
            return RedirectToAction("Index");
        }

        public IActionResult Error()
        {
            return View();
        }

        private static bool IsProcessedFileReferenceValid(TicketDto ticketDto, DownloadReferenceDto downloadReferenceDto)
        {
            if(ticketDto.Sha256Checksum is not null)
                return ticketDto.Id.Equals(downloadReferenceDto.TicketId) &&
                    ticketDto.Sha256Checksum.Equals(downloadReferenceDto.Sha256Checksum) &&
                    ticketDto.DownloadUrl.Equals(downloadReferenceDto.DownloadUrl);

            return ticketDto.Id.Equals(downloadReferenceDto.TicketId) &&
                    ticketDto.DownloadUrl.Equals(downloadReferenceDto.DownloadUrl);
        }
    }
}