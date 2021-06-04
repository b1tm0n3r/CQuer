using Common.DTOs;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CommonServices.ClientService.TicketClient
{
    class TicketClientService : ClientService, ITicketClientService
    {
        private static readonly string TICKET_API_PATH = "ticket/";
        public TicketClientService(IRestClient restClient, string baseUrl) : base(restClient, baseUrl)
        {
            _restClient.BaseUrl = new Uri(_restClient.BaseUrl + TICKET_API_PATH);
        }

        public async Task<IEnumerable<TicketDto>> GetTickets()
        {
            var request = new RestRequest(string.Empty, Method.GET);
            var response = await _restClient.ExecuteAsync(request);
            return JsonConvert.DeserializeObject<IEnumerable<TicketDto>>(response.Content);
        }

        public async Task<TicketDto> GetTicket(int id)
        {
            var request = new RestRequest(id.ToString(), Method.GET);
            var response = await _restClient.ExecuteAsync(request);
            return JsonConvert.DeserializeObject<TicketDto>(response.Content);
        }

        public async Task<IRestResponse> CreateTicket(TicketDto ticketDto)
        {
            var request = new RestRequest("Create", Method.POST);
            request.AddJsonBody(ticketDto);
            var response = await _restClient.ExecuteAsync(request);
            return response;
        }

        public async Task<IRestResponse> UpdateTicket(TicketDto ticketDto)
        {
            var request = new RestRequest(ticketDto.Id.ToString(), Method.PUT);
            request.AddJsonBody(ticketDto);
            var response = await _restClient.ExecuteAsync(request);
            return response;
        }

        public async Task<IRestResponse> FinalizeTicket(int ticketId)
        {
            var request = new RestRequest(ticketId.ToString() + "/finalize", Method.PUT);
            var response = await _restClient.ExecuteAsync(request);
            return response;
        }

        public async Task<IRestResponse> DeleteTicket(int ticketId)
        {
            var request = new RestRequest(ticketId.ToString(), Method.DELETE);
            request.AddJsonBody(ticketId);
            var response = await _restClient.ExecuteAsync(request);
            return response;
        }
    }
}
