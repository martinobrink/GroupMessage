using System.IO;

namespace GroupMessage.Server.Module
{
    public static class RequestStreamExtensions
    {
        public static string GetAsString(this Nancy.IO.RequestStream stream)
        {
            stream.Position = 0;
            using (var streamReader = new StreamReader(stream))
            {
                return streamReader.ReadToEnd();
            }
        }
    }
}