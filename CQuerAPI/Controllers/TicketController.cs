using Common.DTOs;
using CommonServices.TicketServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQuerAPI.Controllers
{
    public class TicketController : BaseController
    {
        private readonly ITicketService _ticketService;
        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }
        public async Task<ActionResult<IEnumerable<TicketDto>>> GetAllTickets()
        {
            var tickets = await _ticketService.GetTickets();
            return tickets.ToList();
        }
        public async Task<ActionResult> Create(TicketDto ticketDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = await _ticketService.Create(ticketDto);

            return Created($"/api/account/{id}", null);
        }
        public async Task<ActionResult<TicketDto>> Edit([FromRoute]int id, TicketDto ticketDto)
        {
            if (!ModelState.IsValid)
                BadRequest(ModelState);

            var ticket = await _ticketService.Edit(id, ticketDto);
            if (!ticket)
                return NotFound();
            return Ok();
        }
        public async Task<ActionResult<TicketDto>> Delete(int id, TicketDto ticketDto)
        {
            if (!ModelState.IsValid)
                BadRequest(ModelState);

            var ticket = await _ticketService.Delete(id, ticketDto);
            if (!ticket)
                return NotFound();
            return Ok();
        }

        public async Task<ActionResult<TicketDto>> Sumbit(int id)
        {
            if (!ModelState.IsValid)
                BadRequest(ModelState);

            var ticket = await _ticketService.Sumbit(id);
            if (!ticket)
                return NotFound();
            return Ok();
        }
    }
}
