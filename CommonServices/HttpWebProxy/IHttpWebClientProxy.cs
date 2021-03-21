using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace CommonServices.HttpWebProxy
{
    public interface IHttpWebClientProxy
    {
        public void DownloadFileFromUrl(string sourceUrl, string destinationPath);
        public HttpWebResponse GetWebsiteContent(string url);
    }
}
