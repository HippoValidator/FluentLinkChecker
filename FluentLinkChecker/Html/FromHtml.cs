namespace FluentLinkChecker.Html
{
    public class FromHtml : IHtml
    {
        private readonly string _html;

        public FromHtml(string html)
        {
            _html = html;
        }

        public string GetHtml()
        {
            return _html;
        }
    }
}