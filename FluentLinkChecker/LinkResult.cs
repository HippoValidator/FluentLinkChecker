using System;
using System.Net;

namespace FluentLinkChecker
{
    public class LinkResult
    {
        public Uri Url { get; set; }

        public HttpStatusCode StatusCode { get; set; }
    }
}