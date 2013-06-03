using System;
using System.Linq;
using System.Net;
using NUnit.Framework;

namespace FluentLinkChecker.Tests
{
    [TestFixture]
    public class LinkCheckTests
    {
        [Test]
        public void CanCheckWithMinimumConfiguration()
        {
            var result =
                LinkCheck
                    .On(x => x.Html("<a href=\"http://www.google.com\">Google</a>"))
                    .Start();
            Assert.That(result != null);
        }

        [Test]
        public void CanCheckWithBeforeAndAfterDecorators()
        {
            var result =
                LinkCheck
                    .On(x => x.Html("<a href=\"http://www.google.com\">Google</a>"))
                    .AsBot(x => x.Bing())
                    .OnRequest(x => x.Headers.Add("X-From", "user@example.com"))
                    .OnResponse(x => Assert.That(x != null))
                    .Start();

            Assert.That(result != null);
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.All(x => x.StatusCode == HttpStatusCode.OK));
        }

        [Test]
        public void CanFindErrors()
        {
            var result =
                LinkCheck
                    .On(x => x.Html("<a href=\"http://unknownhost42.unk/unknown\">Unknown</a>"))
                    .Start();

            Assert.That(result != null);
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().Url.ToString(), Is.EqualTo("http://unknownhost42.unk/unknown"));
            Assert.That(result.First().StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public void CanCheckRelativeUrls()
        {
            var result =
                LinkCheck
                    .On(x => x
                        .Html("<a href=\"/some/relative/url.html\">Relative</a>")
                        .Relative(new Uri("http://www.google.com")))
                    .Start();
            Assert.That(result != null);
            Assert.That(result.Any());
        }

        [Test]
        public void DoNotRequireBaseUrlWhenCheckRelativesOnUrl()
        {
            var result =
                LinkCheck
                    .On(x => x
                        .Url(new Uri("http://www.google.com"))
                        .Relative())
                    .Start();
            Assert.That(result != null);
            Assert.That(result.Any());
        }

        [Test]
        public void CanCheckBothAbsoluteAndRelativeUrls()
        {
            var result =
                LinkCheck
                    .On(x => x
                        .Html("<a href=\"/relative/url\">Relative</a><a href=\"http://absolute.url\">Absolute</a>")
                        .RelativeOrAbsolute(new Uri("http://www.google.com")))
                    .Start();
            Assert.That(result != null);
            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public void CanCheckOnUrlWithRelativesAutomatically()
        {
            var result =
                LinkCheck
                    .On(x => x.Url(new Uri("http://www.google.com")))
                    .Start();

            Assert.That(result != null);
            Assert.That(result.Any(x => x.Url.IsAbsoluteUri));
            Assert.That(result.Any(x => !x.Url.IsAbsoluteUri));
        }

        [Test]
        public void OnlyCheckRelativesWhenSpecified()
        {
            var result =
                LinkCheck
                    .On(
                        x =>
                        x.Html("<a href=\"/relative/url\">Relative</a><a href=\"http://www.google.com\">Absolute</a>")
                         .Relative(new Uri("http://www.bing.com")))
                    .Start();
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(!result.First().Url.IsAbsoluteUri);
        }

        [Test]
        public void OnCheckAbsolutesWhenSpecified()
        {
            var result =
                LinkCheck
                    .On(
                        x =>
                        x.Html("<a href=\"/relative/url\">Relative</a><a href=\"http://www.google.com\">Absolute</a>")
                         .Absolute())
                    .Start();
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().Url.IsAbsoluteUri);
        }

        [Test]
        public void CanAddMultipleBeforeAndAfterDecorators()
        {
            bool one = false, two = false, three = false, four = false;
            LinkCheck
                .On(x => x.Html("<a href=\"http://www.google.com\">Link</a>"))
                .OnRequest(x =>
                    {
                        one = true;
                    })
                .OnRequest(x =>
                    {
                        two = true;
                    })
                .OnResponse(x =>
                    {
                        three = true;
                    })
                .OnResponse(x =>
                    {
                        four = true;
                    })
                .Start();

           Assert.That(one);
           Assert.That(two);
           Assert.That(three);
           Assert.That(four);
        }
    }
}