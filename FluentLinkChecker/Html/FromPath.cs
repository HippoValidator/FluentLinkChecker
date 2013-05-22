using System.IO;
using System.Text;

namespace FluentLinkChecker.Html
{
    public class FromPath : IHtml
    {
        private readonly string _path;

        public FromPath(string path)
        {
            _path = path;
        }

        public string GetHtml()
        {
            return File.ReadAllText(_path, Encoding.Default);
        }
    }
}