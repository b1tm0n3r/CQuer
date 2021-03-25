using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace CommonServices.HttpWebProxy
{
    class HttpWebClientProxy : IHttpWebClientProxy
    {
        private static readonly string HTTP_GET_METHOD = "GET";
        private readonly ILogger<HttpWebClientProxy> _logger;

        public HttpWebClientProxy(ILogger<HttpWebClientProxy> logger)
        {
            _logger = logger;
        }

        public void DownloadFileFromUrl(string sourceUrl, string destinationPath)
        {
            using var webClient = new WebClient();
            _logger.LogInformation("Downloading file from {0} & saving as {1}", sourceUrl, destinationPath);
            webClient.DownloadFile(sourceUrl, destinationPath);
        }

        public HttpWebResponse GetWebsiteContent(string url)
        {
            HttpWebRequest request = WebRequest.CreateHttp(url);
            request.Method = HTTP_GET_METHOD;
            return (HttpWebResponse)request.GetResponse();
        }
    }
}
