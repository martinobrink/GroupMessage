using System;
using Nancy;
using Nancy.Hosting.Self;
using System.Linq;
using System.Threading;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace GroupMessage.Server
{
    class Program : NancyModule
    {
        public static bool AcceptAnyCertificate (object sender, X509Certificate certificate, X509Chain chain, 
                                      SslPolicyErrors sslPolicyErrors)
        {
            // We would benefit from implementing some certificate pinning here, but we leave that open for now
            return true;
        }

        static void Main(string[] args)
        {
            Console.Write("Starting server...");

            ServicePointManager.ServerCertificateValidationCallback = AcceptAnyCertificate;

            var server = new NancyHost(new Uri("http://localhost:8282"));
            server.Start();
            Console.WriteLine("started!");

			if(Environment.OSVersion.Platform == PlatformID.Win32NT)
			{
				Console.WriteLine("press any key to exit");
				Console.ReadKey();    
			}
			else
			{
				//Under mono if you deamonize a process a Console.ReadLine with cause an EOF 
				//so we need to block another way
				Console.WriteLine("kill to exit");
				while(true) Thread.Sleep(10000000);	
			}
			
			server.Stop();  // stop hosting
		}
    }
}
