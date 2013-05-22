using System;
using System.Net;

namespace FluentLinkChecker.Html
{
    public class FromUrl : IHtml
    {
        private readonly Uri _url;

        public FromUrl(Uri url)
        {
            _url = url;
        }

        public string GetHtml()
        {
            var webClient = new WebClient();
            return webClient.DownloadString(_url);
        }
    }
}