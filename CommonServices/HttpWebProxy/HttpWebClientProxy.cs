using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CommonServices.HttpWebProxy
{
    class HttpWebClientProxy : IHttpWebClientProxy
    {
        private static readonly int CRAWLER_BASE_DEPTH = 3;
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

        public async Task<string> TryFindChecksumWithCrawler(string sourceUrl, string directDownloadUrl)
        {
            WebCrawler webCrawler = new WebCrawler(sourceUrl);
            SimpleWebScraper simpleWebScraper = new SimpleWebScraper();

            var crawledPages = await webCrawler.Crawl(CRAWLER_BASE_DEPTH);

            foreach (var page in crawledPages)
            {
                if (simpleWebScraper.TryGetSha256FromHtml(page, directDownloadUrl, out string result))
                {
                    return result;
                }
            }

            return string.Empty;
        }
    }
}
