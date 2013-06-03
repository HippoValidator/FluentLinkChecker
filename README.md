FluentLinkChecker
=================

A fluent api for doing broken link checks of web resources. Checking a resource for broken links is easy:

    var result =
        LinkCheck
            .On(x => x.Url(new Uri("http://www.hippovalidator.com")))
            .AsBot(x => x.Google())
            .OnRequest(x => x.Headers.Add("Referer", "http://mydomain.com"))
            .Start();
