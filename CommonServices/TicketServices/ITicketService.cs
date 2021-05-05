using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.DTOs;

namespace CommonServices.TicketServices
{
    public interface ITicketService
    {
        Task<IEnumerable<TicketDto>> GetTickets();
        Task<int> Create(TicketDto ticketDto);
        Task<bool> Update(int id, TicketDto ticketDto);
        Task<bool> Delete(int id);
        Task<bool> Finalize(int id);
    }
}
