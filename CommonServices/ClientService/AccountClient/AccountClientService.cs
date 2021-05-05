using System;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Common.DTOs;
using RestSharp;

namespace CommonServices.ClientService.AccountClient
{
    public class AccountClientService : ClientService, IAccountClientService
    {
        private static readonly string ACCOUNT_API_PATH = "account/";
        public AccountClientService(IRestClient restClient, string baseUrl) : base(restClient, baseUrl)
        {
            _restClient.BaseUrl = new Uri(_restClient.BaseUrl + ACCOUNT_API_PATH);
        }
        public async Task<IRestResponse> LoginResponse(LoginDto loginDto)
        {
            var request = new RestRequest("login", Method.POST);
            request.AddJsonBody(loginDto);
            var response = await _restClient.ExecuteAsync(request);
            return response;
        }
    }
}