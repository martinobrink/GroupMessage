using System.Collections.Generic;
using Nancy.Json;

namespace GroupMessage.Server.Test.Module
{
    public static class BrowserResponseBodyWrapperExtensions
    {
        public static TModel UnmarshallJson<TModel>(this Nancy.Testing.BrowserResponseBodyWrapper bodyWrapper)
        {
            var serializer = new JavaScriptSerializer();

            return serializer.Deserialize<TModel>(AsString(bodyWrapper));
        }

        public static string AsStringNonNancy(this Nancy.Testing.BrowserResponseBodyWrapper body)
        {
            return System.Text.Encoding.UTF8.GetString(System.Linq.Enumerable.ToArray<byte>((IEnumerable<byte>)body));
        }

        private static string AsString(Nancy.Testing.BrowserResponseBodyWrapper body)
        {
            return System.Text.Encoding.UTF8.GetString(System.Linq.Enumerable.ToArray<byte>((IEnumerable<byte>)body));
        }
    }
}