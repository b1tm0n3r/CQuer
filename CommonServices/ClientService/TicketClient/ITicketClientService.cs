using Common.DTOs;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CommonServices.ClientService.TicketClient
{
    public interface ITicketClientService
    {
        public Task<IEnumerable<TicketDto>> GetTickets();
        public Task<TicketDto> GetTicket(int id);
        public Task<IRestResponse> CreateTicket(TicketDto ticketDto);
        public Task<IRestResponse> UpdateTicket(TicketDto ticketDto);
        public Task<IRestResponse> FinalizeTicket(int ticketId);
        public Task<IRestResponse> DeleteTicket(int ticketId);
    }
}
