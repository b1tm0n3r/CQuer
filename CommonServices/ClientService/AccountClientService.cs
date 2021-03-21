using System;
using System.Threading.Tasks;
using Common.DTOs;
using RestSharp;

namespace CommonServices.ClientService
{
    public class AccountClientService : IAccountClientService
    {
        private readonly IRestClient _restClient;
        
        public AccountClientService(IRestClient restClient)
        {
             _restClient = restClient;
             _restClient.BaseUrl = new Uri("https://localhost:6001/api/account/");
             _restClient.AddDefaultHeader("Content-Type", "application/json");
        }
        
        public async Task<bool> LoginResponse(LoginDto loginDto)
        {
            var request = new RestRequest("login", Method.POST);
            request.AddJsonBody(loginDto);
            var response = await _restClient.ExecuteAsync(request);
            
            return response.IsSuccessful;
        }
    }
}