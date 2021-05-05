using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonServices.ClientService
{
    public abstract class ClientService
    {
        protected readonly IRestClient _restClient;
        public ClientService(IRestClient restClient, string baseUrl)
        {
            _restClient = restClient;
            _restClient.BaseUrl = new Uri(baseUrl);
            _restClient.AddDefaultHeader("Content-Type", "application/json");
        }
    }
}
