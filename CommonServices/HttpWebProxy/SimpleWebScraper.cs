using HtmlAgilityPack;
using System;

namespace CommonServices.HttpWebProxy
{
    public class SimpleWebScraper
    {
        private static readonly string SHA256 = "SHA256";
        private static readonly int SHA256_HEX_LENGTH = 64;
        private readonly string baseUrl;
        private readonly HtmlParser _htmlParser;
        public SimpleWebScraper(string baseUrl)
        {
            this.baseUrl = baseUrl;
            _htmlParser = new HtmlParser();
        }

        public HtmlDocument GetWebsiteContent()
        {
            HtmlWeb htmlWeb = new HtmlWeb();
            return htmlWeb.Load(baseUrl);
        }

        public bool TryGetSha256FromHtml(HtmlDocument htmlDocument, string directDownloadUrl, out string sha256Checksum)
        {
            if (directDownloadUrl.Equals(string.Empty))
                throw new Exception("Empty download URL parameter!");

            sha256Checksum = string.Empty;
            var nodesWithHyperlinks = _htmlParser.GetAllNodesWithHyperlinks(htmlDocument);
            if (nodesWithHyperlinks is null)
                return false;

            HtmlNode fileDownloadNode = _htmlParser.GetFileDownloadNodeByDirectUrl(directDownloadUrl, nodesWithHyperlinks);
            var nodeCollectionWithSha256Substring = _htmlParser.SearchParentsWithSubstring(fileDownloadNode, SHA256);
            if (nodeCollectionWithSha256Substring is null)
                return false;

            if (!_htmlParser.IsNodePartOfTable(fileDownloadNode))
            {
                sha256Checksum = GetSha256FromClosestHtmlParent(fileDownloadNode);
            }
            else
            {
                var nodes = nodeCollectionWithSha256Substring.Nodes();
                foreach (var node in nodes)
                {
                    var tableHeaderCell = _htmlParser.GetInnerNodeWithSubstring(node, SHA256);
                    if (!_htmlParser.IsNodePartOfTableHeader(tableHeaderCell))
                        continue;

                    sha256Checksum = _htmlParser.GetDataFromTableByHeaderAndRow(tableHeaderCell, fileDownloadNode);
                    if (!sha256Checksum.Equals(string.Empty))
                        break;
                }
            }
            return !sha256Checksum.Equals(string.Empty);
        }

        private string GetSha256FromClosestHtmlParent(HtmlNode fileDownloadNode)
        {
            var result = string.Empty;
            var closestParent = _htmlParser.SearchClosestParentWithSubstring(fileDownloadNode, SHA256);
            var textWithSha256String = closestParent.InnerText.Trim();

            if (textWithSha256String.Length > SHA256_HEX_LENGTH)
            {
                var nodeTextBlocks = textWithSha256String.Split(' ', ':');
                foreach (var textBlock in nodeTextBlocks)
                    if (textBlock.Trim().Length == SHA256_HEX_LENGTH)
                        result = textBlock.ToUpper();
            }
            return result;
        }
    }
}
