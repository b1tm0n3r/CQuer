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

        public bool TryDownloadSha256ChecksumFromFile(string sourceUrl, out string sha256Checksum)
        {
            HttpWebRequest request = WebRequest.CreateHttp(sourceUrl);
            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                using var streamReader = new StreamReader(response.GetResponseStream());
                var responseText = streamReader.ReadToEnd();

                sha256Checksum = HtmlParser.GetSha256ChecksumFromString(responseText);

                return true;
            } 
            catch
            {
                sha256Checksum = string.Empty;
                return false;
            }
        }

        public bool TryExtractSha256ChecksumFromPage(string sourceUrl, string directDownloadUrl, out string sha256Checksum)
        {
            SimpleWebScraper simpleWebScraper = new SimpleWebScraper();
            var baseDownloadPage = simpleWebScraper.GetWebsiteDocument(sourceUrl);
            if(simpleWebScraper.TryGetSha256FromHtml(baseDownloadPage, directDownloadUrl, out string result))
            {
                sha256Checksum = result;
                return true;
            }
            else
            {
                sha256Checksum = string.Empty;
                return false;
            }
        }
    }
}
