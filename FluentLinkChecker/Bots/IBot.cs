using System.Net;

namespace FluentLinkChecker.Bots
{
    public interface IBot
    {
        void OnRequest(HttpWebRequest webRequest);
    }
}