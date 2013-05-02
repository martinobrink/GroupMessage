using System.Linq;
using GroupMessage.Server.Model;
using GroupMessage.Server.Repository;
using Nancy.ModelBinding;
using MongoDB.Driver.Linq;
using System.IO;
using MongoDB.Driver.Builders;
using System;
using Nancy;

namespace GroupMessage.Server.Module
{
    public class TwilioModule : ModuleBase
    {
        public TwilioModule(MessageService _messageService) : base("twilio")
        {
            Post ["/sms"] = parameters =>
            {
                Console.WriteLine("Received from Twilio: " + Request.Body.GetAsString());
                return new Response().SetContentType("application/xml").SetBody("<?xml version='1.0' encoding='UTF-8'?><Response></Response>"); // I don't know why, but Twilio requires a Response document in order to acknowledge the receive
            };
        }
    }
}
