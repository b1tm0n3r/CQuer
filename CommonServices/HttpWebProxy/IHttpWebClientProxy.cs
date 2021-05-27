using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace CommonServices.HttpWebProxy
{
    public interface IHttpWebClientProxy
    {
        public void DownloadFileFromUrl(string sourceUrl, string destinationPath);
        public bool TryDownloadSha256ChecksumFromFile(string sourceUrl, out string sha256Checksum);
        public bool TryExtractSha256ChecksumFromPage(string sourceUrl, string directDownloadUrl, out string sha256Checksum);
        public bool TryFindChecksumWithCrawler(string sourceUrl, string directDownloadUrl, out string sha256Checksum);
    }
}
