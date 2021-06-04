using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CommonServices.HttpWebProxy
{
    public interface IHttpWebClientProxy
    {
        public Task DownloadFileFromUrl(string sourceUrl, string destinationPath);
        public bool TryDownloadSha256ChecksumFromFile(string sourceUrl, out string sha256Checksum);
        public bool TryExtractSha256ChecksumFromPage(string sourceUrl, string directDownloadUrl, out string sha256Checksum);
        public Task<string> TryFindChecksumWithCrawler(string sourceUrl, string directDownloadUrl);
    }
}
