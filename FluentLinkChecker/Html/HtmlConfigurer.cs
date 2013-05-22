using System;
using FluentLinkChecker.Configuration;

namespace FluentLinkChecker.Html
{
    public class HtmlConfigurer : BaseConfigurer
    {
        internal HtmlConfigurer(FluentConfiguration configuration) : base(configuration)
        {
        }

        public void Relative(Uri baseUrl)
        {
            Configuration.BaseUrl = baseUrl;
            Configuration.UriKind = UriKind.Relative;
        }

        public void Absolute()
        {
            Configuration.UriKind = UriKind.Absolute;
        }

        public void RelativeOrAbsolute(Uri baseUrl)
        {
            Configuration.BaseUrl = baseUrl;
            Configuration.UriKind = UriKind.RelativeOrAbsolute;
        }
    }
}