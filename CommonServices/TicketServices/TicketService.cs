﻿using AutoMapper;
using Common.DataModels.StandardEntities;
using Common.DTOs;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServices.TicketServices
{
    public class TicketService : ITicketService
    {
        private readonly ICQuerDbContext _dbContext;
        private readonly IMapper _mapper;
        public TicketService(ICQuerDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> Create(TicketDto ticketDto)
        {
            var ticket = new Ticket
            {
                CreatorId = ticketDto.CreatorId, 
                Description = ticketDto.Description,
                DownloadUrl = ticketDto.DownloadUrl,
                Severity = ticketDto.Severity,
                Sha256Checksum = ticketDto.Sha256Checksum,
                Solved = false
            };

            await _dbContext.Tickets.AddAsync(ticket);
            await _dbContext.SaveChangesAsync();

            return ticket.Id;
        }

        public async Task<bool> Delete(int id, TicketDto ticketDto)
        {
            var ticket = await _dbContext.Tickets.FirstOrDefaultAsync(x => x.Id == id);

            if (ticket is null)
                return false;

            _dbContext.Tickets.Remove(ticket);

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Edit(int id, TicketDto ticketDto)
        {
            var ticket = _dbContext.Tickets.FirstOrDefault(x => x.Id == id);

            if (ticket == null)
                return false;

            ticket.Description = ticketDto.Description;
            ticket.DownloadUrl = ticketDto.DownloadUrl;
            ticket.Severity = ticketDto.Severity;
            ticket.Sha256Checksum = ticketDto.Sha256Checksum;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Sumbit(int id)
        {
            var ticket = _dbContext.Tickets.FirstOrDefault(x => x.Id == id);

            if (ticket == null)
                return false;

            ticket.Solved = true;

            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<TicketDto>> GetTickets()
        {
            var tickets = await _dbContext.Tickets.ToListAsync();
            var ticketsDto = _mapper.Map<IEnumerable<TicketDto>>(tickets);

            return ticketsDto;
        }
    }
}