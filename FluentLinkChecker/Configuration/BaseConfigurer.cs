namespace FluentLinkChecker.Configuration
{
    public abstract class BaseConfigurer
    {
        private readonly FluentConfiguration _configuration;

        internal protected BaseConfigurer(FluentConfiguration configuration)
        {
            _configuration = configuration;
        }

        internal FluentConfiguration Configuration
        {
            get { return _configuration; }
        }
    }
}