using System.Collections.Generic;
using Newtonsoft.Json;

namespace GroupMessage.Server.Test.Module
{
    public static class BrowserResponseBodyWrapperExtensions
    {
        public static TModel UnmarshallJson<TModel>(this Nancy.Testing.BrowserResponseBodyWrapper bodyWrapper)
        {
            return JsonConvert.DeserializeObject<TModel>(AsString(bodyWrapper));
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