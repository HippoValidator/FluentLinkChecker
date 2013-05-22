using FluentLinkChecker.Configuration;

namespace FluentLinkChecker.Bots
{
    public class AsBotConfigurer : BaseConfigurer
    {
        internal AsBotConfigurer(FluentConfiguration configuration)
            : base(configuration)
        {
        }

        public void Google()
        {
            Configuration.Bot = new GoogleBot();
        }

        public void Bing()
        {
            Configuration.Bot = new BingBot();
        }
    }
}