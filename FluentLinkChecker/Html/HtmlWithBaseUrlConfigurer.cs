using System;
using FluentLinkChecker.Configuration;

namespace FluentLinkChecker.Html
{
    public class HtmlWithBaseUrlConfigurer : BaseConfigurer
    {
        private readonly Uri _baseUrl;

        internal HtmlWithBaseUrlConfigurer(Uri baseUrl, FluentConfiguration configuration) : base(configuration)
        {
            _baseUrl = baseUrl;
        }

        public void Relative()
        {
            Configuration.BaseUrl = _baseUrl;
            Configuration.UriKind = UriKind.Relative;
        }

        public void Absolute()
        {
            Configuration.UriKind = UriKind.Absolute;
        }

        public void RelativeOrAbsolute()
        {
            Configuration.BaseUrl = _baseUrl;
            Configuration.UriKind = UriKind.RelativeOrAbsolute;
        }
    }
}