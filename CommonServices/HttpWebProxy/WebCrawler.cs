using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace CommonServices.HttpWebProxy
{
    public class WebCrawler
    {
        private static readonly string HTTP_HEAD = "HEAD";
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
        public HtmlDocument Crawl(string url)
        {
            return url.StartsWith(baseUrl) ? _htmlWeb.Load(url) : null;
        }
        public List<HtmlDocument> Crawl(string url, int depth)
        {
            List<HtmlDocument> results = new List<HtmlDocument>();
            var urlsToCrawl = _htmlParser.GetUrlsFromHtmlDocumentLinks(Crawl(url));
            Crawl(urlsToCrawl, depth - 1, results);
            return results;
        }

        private void Crawl(List<string> urls, int depth, List<HtmlDocument> results)
        {
            if (depth == 0)
                return;
            urls.ForEach(url =>
            {
                WebResponse response = RequestHeadFromUrl(url);
                if (url.StartsWith(baseUrl) && response.ContentType.Contains(HTML_RESPONSE_TYPE))
                {
                    var crawledSite = Crawl(url);
                    results.Add(crawledSite);
                    var urlsToCrawl = _htmlParser.GetUrlsFromHtmlDocumentLinks(crawledSite);
                    Crawl(urlsToCrawl, depth - 1, results);
                }
                else
                    return;
            });
        }

        //TODO: Might want to change this one to HttpClient
        private static WebResponse RequestHeadFromUrl(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = HTTP_HEAD;
            return request.GetResponse();
        }

        public string ExtractBaseUrl(string url)
        {
            var regex = new Regex(@"^http(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)");
            var result = regex.Match(url).Value;
            return result.EndsWith("/") ? result : result + "/";
        }
    }
}
