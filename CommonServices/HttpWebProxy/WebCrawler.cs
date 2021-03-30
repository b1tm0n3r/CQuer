using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CommonServices.HttpWebProxy
{
    public class WebCrawler
    {
        private readonly string baseUrl;
        private readonly HtmlWeb _htmlWeb;
        public WebCrawler(string url)
        {
            _htmlWeb = new HtmlWeb();
            baseUrl = ExtractBaseUrl(url);
        }
        public List<HtmlDocument> Crawl(List<string> urls)
        {
            List<HtmlDocument> results = new List<HtmlDocument>();
            urls.ForEach(url =>
            {
                if(url.StartsWith(baseUrl))
                    results.Add(_htmlWeb.Load(url));
            });
            return results;
        }
        public HtmlDocument Crawl(string url)
        {
            return url.StartsWith(baseUrl) ? _htmlWeb.Load(url) : null;
        }

        //TODO: Add recursive crawling for given depth

        public string ExtractBaseUrl(string url)
        {
            var regex = new Regex(@"^http(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)");
            var result = regex.Match(url).Value;
            return result.EndsWith("/") ? result : result + "/";
        }
    }
}
