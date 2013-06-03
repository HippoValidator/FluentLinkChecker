# FluentLinkChecker

A fluent api for doing broken link checks of web resources. Checking a resource for broken links is easy:

    var result =
        LinkCheck
            .On(src => src.Url(new Uri("http://www.hippovalidator.com")))
            .Start();

    foreach (var link in result)
    {
        Console.WriteLine("Url: {0}, Status Code: {1}", link.Url, link.StatusCode);
    }

## Bots
FluentLinkChecker doesn't set any HTTP request headers as default, but sometimes you want to present yourself as a bot. Out of the box Googlebot and Bingbot is implemented, but you can easily add your own. Checking links as a bot looks something like this:

    var result =
        LinkCheck
            .On(src => src.Url(new Uri("http://www.hippovalidator.com")))
            .AsBot(bot => bot.Google())
            .Start();

## Callbacks
FluentLinkChecker supports two hooks: OnRequest and OnResponse. The method provided in OnRequest, is called just before executing each HTTP request. The method provided in OnResponse, is called just after executing each HTTP request. Implementing callbacks looks like this:

    var result =
        LinkCheck
            .On(src => src.Url(new Uri("http://www.hippovalidator.com")))
            .OnRequest(req => req.Accept = "application/json")
            .OnResponse(resp => resp.Cookies.Add(new Cookie("X-MyCookie", "MyValue")))
            .Start();
