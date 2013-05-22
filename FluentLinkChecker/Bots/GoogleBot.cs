using System.Net;

namespace FluentLinkChecker.Bots
{
    public class GoogleBot : IBot
    {
        public void OnRequest(HttpWebRequest webRequest)
        {
            webRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            webRequest.Headers.Add("From", "googlebot(at)googlebot.com");
            webRequest.UserAgent = "Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)";
        }
    }
}