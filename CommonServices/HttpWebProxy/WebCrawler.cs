using HtmlAgilityPack;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommonServices.HttpWebProxy
{
    public class WebCrawler
    {
        private static readonly string HTML_RESPONSE_TYPE = "text/html";
        private readonly string baseUrl;
        private readonly HtmlWeb _htmlWeb;
        private readonly HtmlParser _htmlParser;
        public WebCrawler(string url)
        {
            _htmlWeb = new HtmlWeb();
            _htmlParser = new HtmlParser();
            baseUrl = ExtractBaseUrl(url);
        }
        public async Task<List<HtmlDocument>> Crawl(int depth)
        {
            List<HtmlDocument> results = new List<HtmlDocument>();
            var urlsToCrawl = _htmlParser.GetUrlsFromHtmlDocumentLinks(Crawl(baseUrl));
            await Crawl(urlsToCrawl, depth - 1, results);
            return results;
        }
        public HtmlDocument Crawl(string url)
        {
            return url.StartsWith(baseUrl) ? _htmlWeb.Load(url) : null;
        }
        public async Task<List<HtmlDocument>> Crawl(string url, int depth)
        {
            List<HtmlDocument> results = new List<HtmlDocument>();
            var urlsToCrawl = _htmlParser.GetUrlsFromHtmlDocumentLinks(Crawl(url));
            await Crawl(urlsToCrawl, depth - 1, results);
            return results;
        }

        private async Task Crawl(List<string> urls, int depth, List<HtmlDocument> results)
        {
            if (depth == 0)
                return;

            foreach (var url in urls)
            {
                var response = await RequestHeadFromUrl(url);
                if (url.StartsWith(baseUrl) && response != null && response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var crawledSite = new HtmlDocument();
                    crawledSite.LoadHtml(responseContent);
                    results.Add(crawledSite);
                    var urlsToCrawl = _htmlParser.GetUrlsFromHtmlDocumentLinks(crawledSite);
                    await Crawl(urlsToCrawl, depth - 1, results);
                }
                else
                    continue;
            }
        }

        //TODO: Might want to change this one to HttpClient
        private async Task<HttpResponseMessage> RequestHeadFromUrl(string url, bool useHeadMethod = false)
        {
            var client = new HttpClient();
            if (!useHeadMethod)
            {
                var message = new HttpRequestMessage(HttpMethod.Get, url);
                var response = await client.SendAsync(message, HttpCompletionOption.ResponseHeadersRead);
                if(!response.Content.Headers.ContentType.MediaType.Equals(HTML_RESPONSE_TYPE))
                {
                    response.Dispose();
                    return null;
                }
                
                return response;
            }
            else
            {
                var message = new HttpRequestMessage(HttpMethod.Head, url);
                var response = await client.SendAsync(message, HttpCompletionOption.ResponseHeadersRead);
                if (!response.Content.Headers.ContentType.Equals(HTML_RESPONSE_TYPE))
                {
                    response.Dispose();
                    return null;
                }
                return response;
            }
        }

        public string ExtractBaseUrl(string url)
        {
            var regex = new Regex(@"^http(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*((:(0-9)*)\d{2,5}){0,1}(\/?)");
            var result = regex.Match(url).Value;
            return result.EndsWith("/") ? result : result + "/";
        }
    }
}
