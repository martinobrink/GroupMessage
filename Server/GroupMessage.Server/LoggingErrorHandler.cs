using System;
using Nancy.ErrorHandling;
using System.Net;
using Nancy;

namespace GroupMessage.Server
{
    public class LoggingErrorHandler : IStatusCodeHandler
    {
        public bool HandlesStatusCode (Nancy.HttpStatusCode statusCode, NancyContext context)
        {
            return statusCode == Nancy.HttpStatusCode.InternalServerError;
        }
        
        public void Handle(Nancy.HttpStatusCode statusCode, NancyContext context)
        {
            object errorObject;
            context.Items.TryGetValue(NancyEngine.ERROR_EXCEPTION, out errorObject);
            Exception error = (errorObject as Exception).GetBaseException();
            
            Console.WriteLine("Error occured: " + error.Message + ", " + error.StackTrace);
        }
    }
}

