using System;
using FluentLinkChecker.Bots;
using FluentLinkChecker.Html;

namespace FluentLinkChecker.Configuration
{
    public class FluentConfiguration
    {
        public UriKind UriKind { get; set; }

        public IBot Bot { get; set; }

        public IHtml Html { get; set; }

        public Uri BaseUrl { get; set; }
    }
}