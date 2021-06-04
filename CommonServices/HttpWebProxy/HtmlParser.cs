using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace CommonServices.HttpWebProxy
{
    public class HtmlParser
    {
        private static readonly int SHA256_HEX_LENGTH = 64;
        private static readonly string HTML_TABLE_ROW_TAG = "tr";
        private static readonly string HTML_TABLE_DATA_TAG = "td";
        private static readonly string HTML_TABLE_HEADER_TAG = "th";
        private static readonly string HTML_TABLE_TAG = "table";
        private static readonly string HTML_BODY_TAG = "body";
        private static readonly string HREF_ATTRIBUTE = "href";

        public HtmlNode GetFileDownloadNodeByDirectUrl(string directDownloadUrl, HtmlNodeCollection nodesWithHyperlinks)
        {
            HtmlNode fileDownloadNode = null;
            foreach (var processedNode in nodesWithHyperlinks)
            {
                var processedNodeHref = processedNode.GetAttributeValue(HREF_ATTRIBUTE, "NotFound");
                if (processedNodeHref.Equals(directDownloadUrl))
                    fileDownloadNode = processedNode;
            }

            return fileDownloadNode;
        }

        public List<string> GetUrlsFromHtmlDocumentLinks(HtmlDocument htmlDocument)
        {
            List<string> gatheredUrls = new List<string>();
            var nodesWithHyperlinks = GetAllNodesWithHyperlinks(htmlDocument);
            if (nodesWithHyperlinks is null)
                return gatheredUrls;

            foreach(var processedNode in nodesWithHyperlinks)
            {
                gatheredUrls.Add(processedNode.GetAttributeValue(HREF_ATTRIBUTE, ""));
            }
            return gatheredUrls;
        }

        public HtmlNodeCollection GetAllNodesWithHyperlinks(HtmlDocument htmlDocument)
        {
            return htmlDocument.DocumentNode.SelectNodes("//a");
        }

        public HtmlNodeCollection GetSubnodesByParentName(HtmlNode htmlNode, string parentName)
        {
            return SearchParentForNode(htmlNode, parentName).ChildNodes;
        }

        public List<HtmlNode> FilterNodesByNameToList(HtmlNodeCollection htmlNodes, string filter)
        {
            return htmlNodes.Where(x => x.Name.Equals(filter)).ToList();
        }

        public HtmlNode GetInnerNodeWithSubstring(HtmlNode node, string substring)
        {
            return node.SelectSingleNode("//*[text()[contains(., '" + substring + "')]]");
        }

        public HtmlNodeCollection SearchParentsWithSubstring(HtmlNode node, string substringToSearchFor)
        {
            if (node.Name.Equals(HTML_BODY_TAG))
                return null;

            var parentNode = node.ParentNode;
            var foundNode = parentNode.SelectNodes(parentNode.XPath + "/*[contains(., '" + substringToSearchFor + "')]");

            return foundNode is null ? SearchParentsWithSubstring(parentNode, substringToSearchFor) : foundNode;
        }

        public HtmlNode SearchClosestParentWithSubstring(HtmlNode node, string substringToSearchFor)
        {
            if (node.Name.Equals(HTML_BODY_TAG))
                return null;

            var parentNode = node.ParentNode;

            HtmlNode foundNode = parentNode.SelectSingleNode(parentNode.XPath + "[text()[contains(., '" + substringToSearchFor + "')]]");
            if(foundNode is null)
                foundNode = parentNode.SelectSingleNode(parentNode.XPath + "/*[text()[contains(., '" + substringToSearchFor + "')]]");

            return foundNode is null ? SearchClosestParentWithSubstring(parentNode, substringToSearchFor) : foundNode;
        }

        public bool IsNodePartOfTable(HtmlNode node)
        {
            if (node.Name.Equals(HTML_BODY_TAG))
                return false;
            if (node.Name.Equals(HTML_TABLE_HEADER_TAG) ||
                node.Name.Equals(HTML_TABLE_DATA_TAG) ||
                node.Name.Equals(HTML_TABLE_TAG))
                return true;
            else
                return IsNodePartOfTable(node.ParentNode);
        }
        public bool IsNodePartOfTableHeader(HtmlNode node)
        {
            if (node.Name.Equals(HTML_BODY_TAG))
                return false;

            return node.Name.Equals(HTML_TABLE_HEADER_TAG) || IsNodePartOfTableHeader(node.ParentNode);
        }

        public HtmlNode SearchParentForNode(HtmlNode node, string tagToSearchFor)
        {
            if (node.Name.Equals(HTML_BODY_TAG))
                return null;
            var parentNode = node.ParentNode;

            return parentNode.Name.Equals(tagToSearchFor) ? parentNode : SearchParentForNode(parentNode, tagToSearchFor);
        }

        public string GetDataFromTableByHeaderAndRow(HtmlNode tableHeaderCell, HtmlNode tableDataRowIndicator)
        {
            var processedTableHeader = GetSubnodesByParentName(tableHeaderCell, HTML_TABLE_ROW_TAG);
            var processedTableColumn = GetSubnodesByParentName(tableDataRowIndicator, HTML_TABLE_ROW_TAG);

            var tableHeaderCells = FilterNodesByNameToList(processedTableHeader, HTML_TABLE_HEADER_TAG);
            var dataRowCells = FilterNodesByNameToList(processedTableColumn, HTML_TABLE_DATA_TAG);

            var indexOfHeaderCell = tableHeaderCells.IndexOf(tableHeaderCell);
            return dataRowCells[indexOfHeaderCell].InnerText.Trim().ToUpper();
        }

        public static string GetSha256ChecksumFromString(string stringToProcess)
        {
            var nodeTextBlocks = stringToProcess.Split(' ', ':');
            foreach (var textBlock in nodeTextBlocks)
                if (textBlock.Trim().Length == SHA256_HEX_LENGTH)
                    return textBlock.Trim().ToUpper();
            return string.Empty;
        }
    }
}
