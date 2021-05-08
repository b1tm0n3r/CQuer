using Common.DTOs;
using CommonServices.ClientService.FileClient;
using CommonServices.ClientService.TicketClient;
using CQuerMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQuerMVC.Controllers
{
    public class FilesController : Controller
    {
        private IFileClientService _fileClientService;
        private ITicketClientService _ticketClientService;

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
        public async Task<IActionResult> Resolver(DownloadReferenceDto downloadReferenceDto)
        {
            var ticketReference = await _ticketClientService.GetTicket(downloadReferenceDto.TicketId);
            if (ticketReference is null || !IsProcessedFileReferenceValid(ticketReference, downloadReferenceDto))
                return RedirectToAction("Error");

            var response = await _fileClientService.DownloadFile(downloadReferenceDto);
            await _fileClientService.ValidateFileChecksum(int.Parse(response.Content));

            return RedirectToAction("Index");
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

        private bool IsProcessedFileReferenceValid(TicketDto ticketDto, DownloadReferenceDto downloadReferenceDto)
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