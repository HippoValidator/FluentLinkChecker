using System;

namespace FluentLinkChecker.Extensions
{
    public static class StringExtensions
    {
        public static string AsAbsoluteUrl(this string url, Uri baseUrl)
        {
            return Uri.IsWellFormedUriString(url, UriKind.Relative)
                       ? new Uri(baseUrl, url).ToString()
                       : url;
        }
    }
}