using Common.DTOs;
using CommonServices.TicketServices;
using CQuerMVC.Models;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQuerMVC.Controllers
{
    public class TicketController : Controller
    {
        private readonly IRestClient _restClient;
        public TicketController(IRestClient restClient)
        {
            _restClient = restClient;
            _restClient.BaseUrl = new Uri("https://localhost:44356/api/ticket/");
            _restClient.AddDefaultHeader("Content-Type", "application/json");
        }
        public async Task<IActionResult> Index()
        {
            var request = new RestRequest("", Method.GET);
            var response = await _restClient.ExecuteAsync(request);
            var tickets = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TicketDto>>(response.Content);
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
            var request = new RestRequest("Create", Method.POST);
            request.AddJsonBody(ticketDto);
            await _restClient.ExecuteAsync(request);
                  
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Update(TicketDto ticketDto)
        {
            var request = new RestRequest(ticketDto.Id.ToString(), Method.PUT);
            request.AddJsonBody(ticketDto);

            await _restClient.ExecuteAsync(request);

            return RedirectToAction("Index");
        }
        public IActionResult Finalize(int id)
        {
            return View(new TicketIdViewModel(id));
        }
        [HttpPost]
        public async Task<IActionResult> Finalize(TicketIdViewModel ticketIdViewModel)
        {
            var request = new RestRequest(ticketIdViewModel.Id.ToString() + "/finalize", Method.PUT);
            await _restClient.ExecuteAsync(request);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            return View(new TicketIdViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(TicketIdViewModel ticketIdViewModel) 
        {
            var request = new RestRequest(ticketIdViewModel.Id.ToString(), Method.DELETE);
            request.AddJsonBody(ticketIdViewModel.Id);
            await _restClient.ExecuteAsync(request);

            return RedirectToAction("Index");
        }
    }
}
