using System;
using FluentLinkChecker.Configuration;
using FluentLinkChecker.Html;

namespace FluentLinkChecker
{
    public static class LinkCheck
    {
        public static FluentLinkCheckerConfiguration On(Action<OnConfigurer> configurer)
        {
            return new FluentLinkCheckerConfiguration(configurer);
        }
    }
}
