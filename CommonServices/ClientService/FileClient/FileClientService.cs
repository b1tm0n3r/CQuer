using Common.DTOs;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CommonServices.ClientService.FileClient
{
    class FileClientService : ClientService, IFileClientService
    {
        private static readonly string FILE_API_PATH = "file/";
        public FileClientService(IRestClient restClient, string baseUrl) : base(restClient, baseUrl)
        {
            _restClient.BaseUrl = new Uri(_restClient.BaseUrl + FILE_API_PATH);
        }

        public async Task<IRestResponse> DownloadFile(DownloadReferenceDto downloadReferenceDto)
        {
            var request = new RestRequest(string.Empty, Method.POST);
            request.AddJsonBody(downloadReferenceDto);
            var response = await _restClient.ExecuteAsync(request);
            return response;
        }

        public async Task<IEnumerable<FileReferenceDto>> GetFileReferences()
        {
            var request = new RestRequest(string.Empty, Method.GET);
            var response = await _restClient.ExecuteAsync(request);
            return JsonConvert.DeserializeObject<IEnumerable<FileReferenceDto>>(response.Content);
        }

        public async Task<IRestResponse> RemoveFile(int id)
        {
            var request = new RestRequest(id.ToString(), Method.DELETE);
            var response = await _restClient.ExecuteAsync(request);
            return response;
        }
    }
}
