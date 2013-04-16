using System;
using Nancy;
using Nancy.Hosting.Self;
using System.Linq;
using System.Threading;

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

			if(Environment.OSVersion.Platform == PlatformID.Win32NT)
			{
				Console.ReadKey();    
			}
			else
			{
				//Under mono if you deamonize a process a Console.ReadLine with cause an EOF 
				//so we need to block another way
				while(true) Thread.Sleep(10000000);	
			}
			
			server.Stop();  // stop hosting
		}
    }
}
