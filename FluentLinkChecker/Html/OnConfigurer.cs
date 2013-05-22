using System;
using FluentLinkChecker.Configuration;

namespace FluentLinkChecker.Html
{
    public class OnConfigurer : BaseConfigurer
    {
        internal OnConfigurer(FluentConfiguration configuration)
            : base(configuration)
        {
        }

        public HtmlConfigurer Html(string html)
        {
            Configuration.UriKind = UriKind.Absolute; // Default to Absolute
            Configuration.Html = new FromHtml(html);
            return new HtmlConfigurer(Configuration);
        }

        public HtmlWithBaseUrlConfigurer Url(Uri url)
        {
            Configuration.UriKind = UriKind.RelativeOrAbsolute; // Default to RelativeOrAbsolute
            Configuration.BaseUrl = new UriBuilder(url.Scheme, url.Host, url.Port).Uri;
            Configuration.Html = new FromUrl(url);
            return new HtmlWithBaseUrlConfigurer(url, Configuration);
        }

        public HtmlConfigurer File(string path)
        {
            Configuration.UriKind = UriKind.Absolute; // Default to Absolute
            Configuration.Html = new FromPath(path);
            return new HtmlConfigurer(Configuration);
        }
    }
}