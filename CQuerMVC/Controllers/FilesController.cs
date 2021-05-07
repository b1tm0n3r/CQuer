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
            //TODO: add client implementation
            var fileReferences = await _fileClientService.GetFileReferences();
            var viewModel = new FilesViewModel()
            {
                Files = fileReferences
            };
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Resolver(FileReferenceViewModel fileReferenceViewModel)
        {
            var ticketReference = await _ticketClientService.GetTicket(fileReferenceViewModel.TicketId);
            if (ticketReference is null || !IsProcessedFileReferenceValid(ticketReference, fileReferenceViewModel))
                return RedirectToAction("Error");

            var downloadReferenceDto = new DownloadReferenceDto(fileReferenceViewModel.DownloadUrl, fileReferenceViewModel.Sha256Checksum);
            await _fileClientService.DownloadFile(downloadReferenceDto);

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

        private bool IsProcessedFileReferenceValid(TicketDto ticketDto, FileReferenceViewModel fileReferenceViewModel)
        {
            return ticketDto.Id.Equals(fileReferenceViewModel.TicketId) &&
                ticketDto.Sha256Checksum.Equals(fileReferenceViewModel.Sha256Checksum) &&
                ticketDto.DownloadUrl.Equals(fileReferenceViewModel.DownloadUrl);
        }
    }
}