using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentLinkChecker.Bots;
using FluentLinkChecker.Extensions;
using FluentLinkChecker.Html;

namespace FluentLinkChecker.Configuration
{
    public class FluentLinkCheckerConfiguration
    {
        private readonly Action<OnConfigurer> _onConfigurer;
        private Action<AsBotConfigurer> _botConfigurer;
        private readonly List<Action<HttpWebRequest>> _beforeConfigurers;
        private readonly List<Action<HttpWebResponse>> _afterConfigurers;

        public FluentLinkCheckerConfiguration(Action<OnConfigurer> configurer)
        {
            _onConfigurer = configurer;
            _beforeConfigurers = new List<Action<HttpWebRequest>>();
            _afterConfigurers = new List<Action<HttpWebResponse>>();
        }

        public FluentLinkCheckerConfiguration AsBot(Action<AsBotConfigurer> configurer)
        {
            _botConfigurer = configurer;
            return this;
        }

        public FluentLinkCheckerConfiguration Before(Action<HttpWebRequest> configurer)
        {
            _beforeConfigurers.Add(configurer);
            return this;
        }

        public FluentLinkCheckerConfiguration After(Action<HttpWebResponse> configurer)
        {
            _afterConfigurers.Add(configurer);
            return this;
        }

        public List<LinkResult> Start()
        {
            var fluentConfiguration = new FluentConfiguration();

            _onConfigurer(new OnConfigurer(fluentConfiguration));

            if (_botConfigurer != null)
            {
                _botConfigurer(new AsBotConfigurer(fluentConfiguration));
            }

            var markup = fluentConfiguration.Html.GetHtml();

            var links = new List<string>();
            var tasks = new List<Task<LinkResult>>();

            foreach (Match m in Regex.Matches(markup, @"(<a.*?>.*?</a>)", RegexOptions.Singleline))
            {
                var value = m.Groups[1].Value;
                var m2 = Regex.Match(value, @"href=\""(.*?)\""", RegexOptions.Singleline);
                if (!m2.Success) continue;
                var href = m2.Groups[1].Value;
                
                if (links.Any(li => li == href) || !Uri.IsWellFormedUriString(href, fluentConfiguration.UriKind))
                    continue;
                links.Add(href);

                var webRequest = (HttpWebRequest)WebRequest.Create(href.AsAbsoluteUrl(fluentConfiguration.BaseUrl));
                tasks.Add(CreateTask(fluentConfiguration, webRequest, href));
            }

            return tasks.Select(x => x.Result).ToList();
        }

        private Task<LinkResult> CreateTask(FluentConfiguration fluentConfiguration, HttpWebRequest webRequest, string originalUrl)
        {
            return Task<LinkResult>.Factory.StartNew(() =>
                {
                    if (fluentConfiguration.Bot != null)
                    {
                        fluentConfiguration.Bot.OnRequest(webRequest);
                    }

                    _beforeConfigurers.ForEach(x => x(webRequest));

                    var result = new LinkResult {Url = new Uri(originalUrl, UriKind.RelativeOrAbsolute)};
                    HttpWebResponse webResponse = null;
                    try
                    {
                        webResponse = (HttpWebResponse) webRequest.GetResponse();
                        result.StatusCode = webResponse.StatusCode;
                    }
                    catch (WebException we)
                    {
                        result.StatusCode = we.Response != null ? ((HttpWebResponse) we.Response).StatusCode : HttpStatusCode.NotFound;
                    }
                    finally
                    {
                        if (webResponse != null)
                        {
                            _afterConfigurers.ForEach(x => x(webResponse));
                            webResponse.Close();
                        }
                    }

                    return result;
                });
        }
    }
}