using System.Net;

namespace FluentLinkChecker.Bots
{
    public class BingBot : IBot
    {
        public void OnRequest(HttpWebRequest webRequest)
        {
            webRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            webRequest.Headers.Add("From", "bingbot(at)microsoft.com");
            webRequest.UserAgent = "Mozilla/5.0 (compatible; bingbot/2.0; +http://www.bing.com/bingbot.htm)";
        }
    }
}