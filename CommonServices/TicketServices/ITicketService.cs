using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.DTOs;

namespace CommonServices.TicketServices
{
    interface ITicketService
    {
        Task<IEnumerable<TicketDto>> GetTickets();
        Task<int> Create(TicketDto ticketDto);
        Task<bool> Edit(int id, TicketDto ticketDto);
        Task<bool> Delete(int id, TicketDto ticketDto);
        Task<bool> Sumbit(int id);

    }
}
