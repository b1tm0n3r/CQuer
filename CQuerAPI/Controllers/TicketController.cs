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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketDto>>> GetAllTickets()
        {
            var tickets = await _ticketService.GetTickets();
            return tickets.ToList();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketDto>> GetTicketById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                BadRequest(ModelState);

            var ticketDto = await _ticketService.GetTicket(id);
            if (ticketDto is null)
                return NotFound();
            return ticketDto;
        }
        [HttpPost("create")]
        public async Task<ActionResult> Create(TicketDto ticketDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = await _ticketService.Create(ticketDto);

            return Created($"/api/ticket/{id}", null);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<TicketDto>> Update([FromRoute] int id, TicketDto ticketDto)
        {
            if (!ModelState.IsValid)
                BadRequest(ModelState);

            var updateStatus = await _ticketService.Update(id, ticketDto);
            if (!updateStatus)
                return NotFound();
            return Ok();
        }
        [HttpPut("{id}/finalize")]
        public async Task<ActionResult<TicketDto>> Finalize([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                BadRequest(ModelState);

            var finalizeStatus = await _ticketService.Finalize(id);
            if (!finalizeStatus)
                return NotFound();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<TicketDto>> Delete(int id)
        {
            if (!ModelState.IsValid)
                BadRequest(ModelState);

            var deleteStatus = await _ticketService.Delete(id);
            if (!deleteStatus)
                return NotFound();
            return Ok();
        }
    }
}
