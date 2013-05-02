namespace GroupMessage.Server.Test.Module
{
    public static class BrowserExtensions
    {
        public static Nancy.Testing.BrowserResponse Get(this Nancy.Testing.Browser browser, string route)
        {
            return browser.Get(route, with =>
                {
                    with.HttpRequest();
                    with.Header("accept", "application/json");
                });
        }

        public static Nancy.Testing.BrowserResponse Put(this Nancy.Testing.Browser browser, string route, string bodyContent)
        {
            return browser.Put(route, with =>
                {
                    with.HttpRequest();
                    with.Header("accept", "application/json");
                    with.Header("Content-Type", "application/json");
                    with.Body(bodyContent);
                });
        }

        public static Nancy.Testing.BrowserResponse Post(this Nancy.Testing.Browser browser, string route, string bodyContent)
        {
            return browser.Post(route, with =>
                {
                    with.HttpRequest();
                    with.Header("accept", "application/json");
                    with.Header("Content-Type", "application/json");
                    with.Body(bodyContent);
                });
        }

        public static Nancy.Testing.BrowserResponse Delete(this Nancy.Testing.Browser browser, string route)
        {
            return browser.Delete(route, with =>
            {
                with.HttpRequest();
            });
        }
    }
}