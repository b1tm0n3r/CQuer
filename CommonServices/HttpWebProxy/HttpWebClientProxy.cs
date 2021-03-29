using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace CommonServices.HttpWebProxy
{
    class HttpWebClientProxy : IHttpWebClientProxy
    {
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

        public bool TryDownloadSHA256ChecksumFromFile(string sourceUrl, out string sha256Checksum)
        {
            HttpWebRequest request = WebRequest.CreateHttp(sourceUrl);
            var response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode != HttpStatusCode.OK)
            {
                sha256Checksum = "";
                return false;
            }
            else
            {
                using var streamReader = new StreamReader(response.GetResponseStream());
                var responseText = streamReader.ReadToEnd();
                //In case if sha256 file contant is in format <hash> <filename>
                sha256Checksum = responseText.Split(" ")[0];
                return true;
            }
        }

    }
}
