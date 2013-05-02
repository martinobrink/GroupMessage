using System.IO;
using Nancy;

namespace GroupMessage.Server.Module
{
    public static class ResponseExtensions
    {
        public static Response SetBody(this Response response, string body)
        {
            response.Contents = s =>
                {
                    var streamWriter = new StreamWriter(s);
                    streamWriter.Write(body);
                    streamWriter.Flush();
                };

            return response;
        }
    }
}