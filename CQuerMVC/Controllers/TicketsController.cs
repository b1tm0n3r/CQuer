﻿using Common.DataModels.IdentityManagement;
using Common.DTOs;
using CommonServices.ClientService.TicketClient;
using CQuerMVC.Helpers;
using CQuerMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CQuerMVC.Controllers
{
    [Authorize]
    public class TicketsController : Controller
    {
        private readonly ITicketClientService _ticketClientService;
        public TicketsController(ITicketClientService ticketClientService)
        {
            _ticketClientService = ticketClientService;
        }
        public async Task<IActionResult> Index()
        {
            var tickets = await _ticketClientService.GetTickets();
            var vm = new TicketsViewModel()
            {
                Tickets = tickets
            };
            return View(vm);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(TicketDto ticketDto)
        {
            var result = await _ticketClientService.CreateTicket(ticketDto);
            if (!result.IsSuccessful)
                return RedirectToAction("Error");

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            var processedTicket = await _ticketClientService.GetTicket(id);
            return View(processedTicket);
        }
        [HttpPost]
        public async Task<IActionResult> Update(TicketDto ticketDto)
        {
            var result = await _ticketClientService.UpdateTicket(ticketDto);
            if (!result.IsSuccessful)
                return RedirectToAction("Error");

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            return View(new IdViewModel(id));
        }

        [HttpPost]
        [EnumAuthorizeRole(AccountType.Administrator)]
        public async Task<IActionResult> Delete(IdViewModel idViewModel) 
        {
            var result = await _ticketClientService.DeleteTicket(idViewModel.Id);
            if (!result.IsSuccessful)
                return RedirectToAction("Error");

            return RedirectToAction("Index");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
