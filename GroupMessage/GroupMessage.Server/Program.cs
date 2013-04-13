using System;
using Nancy;
using Nancy.Hosting.Self;

namespace GroupMessage.Server
{
    class Program : NancyModule
    {
        static void Main(string[] args)
        {
            Console.Write("Starting server...");
            var server = new NancyHost(new Uri("http://localhost:8282"));
            server.Start();
            Console.WriteLine("started!");
            Console.WriteLine("press any key to exit");
            Console.Read();
        }
    }
}
